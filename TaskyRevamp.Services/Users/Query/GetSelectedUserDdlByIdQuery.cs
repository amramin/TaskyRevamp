using MediatR;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Localization.Resources;


namespace TaskyRevamp.Services.User.Query;

public record GetSelectedUserDdlByIdQuery(Guid id) : IRequest<DdlDto>;

public class GetSelectedUserDdlByIdQueryHandler : IRequestHandler<GetSelectedUserDdlByIdQuery, DdlDto>
{
    private readonly IRepository<Domain.Models.Users.User> _userRepository;

    public GetSelectedUserDdlByIdQueryHandler(IRepository<Domain.Models.Users.User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<DdlDto> Handle(GetSelectedUserDdlByIdQuery request, CancellationToken cancellationToken)
    {
        var userDbRet = await _userRepository.FindByIdWithSelector(request.id, x => new DdlDto
        {
            Id = x.Id,
            NameAr = x.NameArabic,
            NameEn = x.NameEnglish
        }) ?? throw new Exception(SharedResources.Errordatabase);
        return userDbRet;
    }
}
