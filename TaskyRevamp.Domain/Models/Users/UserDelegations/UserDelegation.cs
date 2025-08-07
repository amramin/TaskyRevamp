using TaskyRevamp.Domain.Interfaces;

namespace TaskyRevamp.Domain.Models.Users.UserDelegations;

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
    public User? UpdateddBy { get; set; }

    //public virtual UserDelegationDto CopyToDto()
    //{
    //    UserDelegationDto dto = new UserDelegationDto()
    //    {
    //        Id=Id,
    //        FromDate= FromDate,
    //        ToDate= ToDate,
    //        FromUserId= FromUserId,
    //        ToUserId= ToUserId
    //    };

    //    return dto;
    //}

}
