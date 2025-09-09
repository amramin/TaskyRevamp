using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.ChangeEndDateRequest;

namespace ChangeEndDateRequestyRevamp.Services.ChangeEndDateRequests.Commands;

public record CreateChangeEndDateRequestCommand(ChangeEndDateRequestDto ChangeEndDateRequestDto) : IRequest<Guid>;

public class CreateChangeEndDateRequestHandler : IRequestHandler<CreateChangeEndDateRequestCommand, Guid>
{
    private readonly IRepository<ChangeEndDateRequest> _ChangeEndDateRequestRepository;

    public CreateChangeEndDateRequestHandler(IRepository<ChangeEndDateRequest> ChangeEndDateRequestRepository) => _ChangeEndDateRequestRepository = ChangeEndDateRequestRepository;

    public async Task<Guid> Handle(CreateChangeEndDateRequestCommand request, CancellationToken cancellationToken)
    {
        ChangeEndDateRequest ChangeEndDateRequest = new ChangeEndDateRequest(
            new Guid(), request.ChangeEndDateRequestDto.TaskId, request.ChangeEndDateRequestDto.NewEndDate, request.ChangeEndDateRequestDto.Reason, request.ChangeEndDateRequestDto.CreatedById);
           
        await _ChangeEndDateRequestRepository.Insert(ChangeEndDateRequest);
        await _ChangeEndDateRequestRepository.SaveChangesAsync();
        return ChangeEndDateRequest.Id;

    }
}
