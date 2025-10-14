using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Permissions;
using GeneralModules = TaskyRevamp.Domain.Models.Permissions.GeneralModule;

namespace TaskyRevamp.Services.Permission.GeneralModulePrivillege.Query
{
	public record GetGeneralModulesQuery() : IRequest<List<GeneralModuleDto>>;
	public class GetGeneralModulesHandler : IRequestHandler<GetGeneralModulesQuery, List<GeneralModuleDto>>
	{
		private readonly IRepository<GeneralModules> _generalModuleRepository;
		public GetGeneralModulesHandler(IRepository<GeneralModules> generalModuleRepository)
		{
			_generalModuleRepository = generalModuleRepository;
		}
		public async Task<List<GeneralModuleDto>> Handle(GetGeneralModulesQuery request, CancellationToken cancellationToken)
		{
			string currentLanguage = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
			List<GeneralModuleDto> _generalModuleDtos = new List<GeneralModuleDto>();
			var res = await _generalModuleRepository.AllAsNoTracking();
			if (res.Success && res != null && res.Value != null)
			{
				_generalModuleDtos = res.Value.Select(g => g.CopyToDto()).ToList();
			}

			return _generalModuleDtos;
		}
	}
}
