using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.ChangeEndDateRequest;
using TaskyRevamp.Dto.TaskDto;
using TaskItems = TaskyRevamp.Domain.Models.Task.TaskItem;

namespace TaskyRevamp.Services.Tasks.Commands
{
    public record UpdateRequestStatusCommand(Guid RequestId, ChangeRequestStatus Status) : IRequest<bool>;
    public class UpdateRequestStatusHandler : IRequestHandler<UpdateRequestStatusCommand, bool>
    {
        private readonly IRepository<TaskItems> _taskRepository;
        private readonly IRepository<ChangeEndDateRequest> _changeEndDateRequestRepository;
        public UpdateRequestStatusHandler(IRepository<TaskItems> taskRepository, IRepository<ChangeEndDateRequest> changeEndDateRequestRepository)
        {
            _taskRepository = taskRepository;
            _changeEndDateRequestRepository = changeEndDateRequestRepository;
        }

        public async Task<bool> Handle(UpdateRequestStatusCommand request, CancellationToken cancellationToken)
        {
            var Changerequest = await _changeEndDateRequestRepository.FindByKey(request.RequestId);
            if (Changerequest == null)
            {
                return false;
            }
            var Req = Changerequest.Value;
            Req.Status = request.Status;
            await _changeEndDateRequestRepository.Update(Req);
            var res = await _changeEndDateRequestRepository.SaveChangesAsync();
            if (res.Success)
            {
                if (Req.Status == ChangeRequestStatus.Approved)
                {
                    var taskres = await _taskRepository.FindByKey(Req.TaskItemId);
                    if (taskres is not null)
                    {
                        var task = taskres.Value;
                        task.EndDate = Req.NewEndDate;
                        await _taskRepository.Update(task);
                        await _taskRepository.SaveChangesAsync();
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
