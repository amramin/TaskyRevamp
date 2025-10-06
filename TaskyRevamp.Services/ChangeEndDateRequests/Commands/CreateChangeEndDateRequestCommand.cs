using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.ChangeEndDateRequest;

namespace TaskyRevamp.Services.ChangeEndDateRequests.Commands;

public record CreateChangeEndDateRequestCommand(ChangeEndDateRequestDto ChangeEndDateRequestDto) : IRequest<Guid>;

public class CreateChangeEndDateRequestHandler : IRequestHandler<CreateChangeEndDateRequestCommand, Guid>
{
    private readonly IRepository<ChangeEndDateRequest> _changeEndDateRequestRepository;

    public CreateChangeEndDateRequestHandler(
        IRepository<ChangeEndDateRequest> changeEndDateRequestRepository) =>
        _changeEndDateRequestRepository = changeEndDateRequestRepository;

    public async Task<Guid> Handle(CreateChangeEndDateRequestCommand request, CancellationToken cancellationToken)
    {
        var changeEndDateRequest = new ChangeEndDateRequest(
            Guid.NewGuid(), request.ChangeEndDateRequestDto.TaskId, request.ChangeEndDateRequestDto.NewEndDate,
            request.ChangeEndDateRequestDto.Reason, request.ChangeEndDateRequestDto.CreatedById);

        await _changeEndDateRequestRepository.Insert(changeEndDateRequest);
        await _changeEndDateRequestRepository.SaveChangesAsync();
        return changeEndDateRequest.Id;
    }
}