using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.Departments.Query
{
	public record GetDepartmentNamesQuery() : IRequest<List<string>>;
	public class GetDepartmentNamesHandler : IRequestHandler<GetDepartmentNamesQuery, List<string>>
	{
		private readonly IRepository<Department> _departmentRepository;

		public GetDepartmentNamesHandler(IRepository<Department> departmentRepository)
		{
			_departmentRepository = departmentRepository;
		}
		public async Task<List<string>> Handle(GetDepartmentNamesQuery request, CancellationToken cancellationToken)
		{
			List<string> departmnetNames = new List<string>();
			var res = await _departmentRepository.AllAsNoTracking();
			if (res.Success && res.Value != null)
			{
				departmnetNames = res.Value
					.Select(d => d.NameEnglish.Trim())
					.Concat(res.Value.Select(d => d.NameArabic.Trim()))
					.ToHashSet(StringComparer.OrdinalIgnoreCase)
					.ToList();

			}
			return departmnetNames;
		}
	
	}
}
