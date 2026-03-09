using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Enums;

namespace TaskyRevamp.Services.BackgroundJobs
{
	public class RecycleBinCleanupService
	{
		private readonly IRepository<TaskItem> _taskRepository;
		private readonly IRepository<RecycleBinSettings> _recycleBinSettingsRepository;
		public RecycleBinCleanupService(IRepository<TaskItem> taskRepository, IRepository<RecycleBinSettings> recycleBinSettingsRepository)
		{
			_taskRepository = taskRepository;
			_recycleBinSettingsRepository = recycleBinSettingsRepository;
		}
		public async Task DeleteExpiredTasks()
		{
			var settingsRes = await _recycleBinSettingsRepository.AllAsNoTracking();
			if (!settingsRes.Success || settingsRes.Value == null)
				return;
			var settings = settingsRes.Value.FirstOrDefault();
			if (settings == null)
				return;
			long retentionDays = settings.PeriodType switch
			{
				PeriodType.Never => -1,
				PeriodType.Custom => settings.CustomDays ?? 0,
				_ => (long)settings.PeriodType
			};
			if (retentionDays <= 0)
				return;
			var expirationDate = DateTime.UtcNow.AddDays(-retentionDays);
			var expiredTasksRes = await _taskRepository.FindBy(t => t.IsDeleted && t.DeleteDate != null && t.DeleteDate <= expirationDate);
			if (!expiredTasksRes.Success || expiredTasksRes.Value == null)
				return;
			foreach (var task in expiredTasksRes.Value)
				await _taskRepository.Delete(task.Id);
		}
	}
}
