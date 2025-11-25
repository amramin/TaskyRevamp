using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Models;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Dto.UserDelegation;

namespace UserDelegations;

public class UserDelegation : Entity, IHasCreationMetaData, IHasUpdateMetaData
{
    public Guid FromUserId { get; set; }
    public Guid ToUserId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public Guid CreatedById { get; set; }
    public DateTime CreateDate { get; set; }
    public User CreatedBy { get; set; }
    public Guid? UpdatedById { get; set; }
    public DateTime? UpdateDate { get; set; }
    public User? UpdatedBy { get; set; }
    public User? FromUser { get; set; }
    public User? Touser { get; set; }
    public DelegationOptions DelegationOption { get; set; }

    public virtual UserDelegationDto CopyToDto()
    {
        UserDelegationDto dto = new UserDelegationDto()
        {
            Id = Id,
            FromDate = FromDate,
            ToDate = ToDate,
            FromUserId = FromUserId,
            ToUserId = ToUserId,
            FromUserEn = FromUser.NameEnglish,
            FromUserAr = FromUser.NameArabic,
            ToUserAr = Touser.NameArabic,
            ToUserEn = Touser.NameEnglish,
            CreatedBy = CreatedById,
            UpdatedBy = UpdatedById,
            CreateDate = CreateDate,
            UpdateDate = UpdateDate,
            DelegationOption = DelegationOption
        };

        return dto;
    }


}
