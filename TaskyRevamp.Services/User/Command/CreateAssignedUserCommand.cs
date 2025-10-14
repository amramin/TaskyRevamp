using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Localization.Resources;
using TaskyRevamp.Services.Exceptions;
namespace DepartmentyRevamp.Services.Departments.Commands;

public record CreateAssignedUserCommand(AssignedUserDto AssignedUserDto) : IRequest<Guid>;

public class CreateAssignedUserHandler : IRequestHandler<CreateAssignedUserCommand, Guid>
{
    private readonly IRepository<User> _userRepository;

    public CreateAssignedUserHandler(IRepository<User> userRepository) { _userRepository= userRepository; }

    public async Task<Guid> Handle(CreateAssignedUserCommand request, CancellationToken cancellationToken)
    {

    var res=    await _userRepository.FindBy(k => request.AssignedUserDto.UsrIds.Contains(k.Id));
        var all = res.Value.ToList();
        all.ForEach(k => k.DepartmentId = request.AssignedUserDto.DepartmentId);
        await _userRepository.UpdateRange(all);
        await _userRepository.SaveChangesAsync();
        return request.AssignedUserDto.DepartmentId;
    }

}
