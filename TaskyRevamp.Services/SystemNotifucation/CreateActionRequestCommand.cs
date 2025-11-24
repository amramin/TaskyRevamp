using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Interfaces.Notification;
using TaskyRevamp.Dto.Notification;
using TaskyRevamp.Dtos.NotificationDtos;


namespace TaskyRevamp.Services.ActionService.ActionRequestService.Command
{
    public record CreateActionRequestCommand(NotificationTypeTemplateDto ActionRequest) : IRequest<bool>;

    public class CreateActionRequestHandler : IRequestHandler<CreateActionRequestCommand, bool>
    {
        //private readonly IRepository<ActionRequest> _actionRequestRepo;
        private readonly INotificationService _notificationService;
        public CreateActionRequestHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<bool> Handle(CreateActionRequestCommand request, CancellationToken cancellationToken)
        {

            //var actionRequestDto = request.ActionRequest;

            //var actionRequest = new ActionRequest
            //{
            //   Id = Guid.NewGuid(), 
            //   ActionComment = actionRequestDto.ActionComment,
            //   ActionDate = actionRequestDto.ActionDate,
            //   ActionFileId = actionRequestDto.ActionFileId,
            //   ActionStatusId = actionRequestDto.ActionStatusId,
            //   ActionTypeId = actionRequestDto.ActionTypeId,
            //   ActionUserId = actionRequestDto.ActionUserId,
            //   AssigneeComment = actionRequestDto.AssigneeComment,
            //   AssigneeFileId = actionRequestDto.AssigneeFileId,
            //   AssigneeId = actionRequestDto.AssigneeId,
            //   AssigneeType = actionRequestDto.AssigneeType,
            //   //CreateDate = actionRequestDto.CreateDate,
            //   //CreatedById = actionRequestDto.CreatedById,
            //   IsApprovalRequired = actionRequestDto.IsApprovalRequired,
            //   IsMandatory = actionRequestDto.IsMandatory,
            //   RelatedActionId = actionRequestDto.RelatedActionId,
            //   TaskId = actionRequestDto.TaskId,
            //   Title = actionRequestDto.Title,
            //   ActionApprovalId = actionRequestDto.ActionApprovalId,
            //   ActionRequestApprovals = actionRequestDto.ActionRequestApprovals.Select(x => new ActionRequestApproval
            //   {
            //        AssigneeId = x.AssigneeId,
            //        AssigneeType = x.AssigneeType,
            //        Order = x.Order
            //   }).ToList(),
            //   ActionRequestStatuses = actionRequestDto.ActionRequestStatuses.Select(x => new ActionRequestStatus
            //   {
            //       ActionStatusId = x.ActionStatusId
            //   }).ToList()
            //};

            //await _actionRequestRepo.Insert(actionRequest);

            await _notificationService.SendNotification(new SendNotificationDto
            {
                NotificationTemplateId = null,
                Icon = "@Icons.Material.Filled.PendingActions",
                Notification = new NotificationDto
                {
                    MessageEnglish = "New Action",
                    MessageArabic = "إجراء جديد",
                    TitleEnglish = "New action: ",
                    TitleArabic = "تم تعيينك لإتمام الإجراء الجديد: ",
                },
                // URL = "/InstanceDetails/" + actionRequest.TaskId.ToString() + "?origin=2&actionId="+actionRequest.Id,
                IsSendEmail = false,
                IsSendSms = false,
                IsSendSystemNotification = true,
                //ReceiverId = actionRequest.AssigneeId==null ? null : actionRequest.AssigneeId,
                //ReceiverType = actionRequest.AssigneeType,
                IsRefreshListRequired = false
            });

            return true;

        }
    }
}
