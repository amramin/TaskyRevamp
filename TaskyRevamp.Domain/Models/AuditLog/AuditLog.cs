using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.Enums;

namespace TaskyRevamp.Domain.Models.AuditLog
{
    public class AuditLog : Entity
    {
        [ForeignKey(nameof(User))]
        public Guid UserId {  get; set; }
        public DateTime CreateDate {  get; set; }
        public DateTime? FirstLogin {  get; set; }
        public DateTime? LastLogin { get; set; }
        public AuditAction AuditAction { get; set; }
        public string? IpAddress {  get; set; }
        public User User { get; set; }
    }
}
