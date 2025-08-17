using MediatR;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Dto.GeneralDto;
namespace TaskyRevamp.Services.Account.Query;

public record GetUsersQuery(int PageNumber, int PageSize) : IRequest<PagedResult<UserDto>>;

public class GetUsersHandler : IRequestHandler<GetUsersQuery, PagedResult<UserDto>>
{
    private readonly IRepository<User> _userRepository;

    public GetUsersHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<PagedResult<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var pagedUsers = await _userRepository.GetPagedAsync(
            request.PageNumber,
            request.PageSize,
            orderBy: q => q.OrderBy(u => u.NameEnglish) // Always order before paging
        );

        return new PagedResult<UserDto>
        {
            Items = pagedUsers.Items.Select(u => u.CopyToDto()).ToList(),
            TotalCount = pagedUsers.TotalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}

