using System.ComponentModel.DataAnnotations;
using TF1.LeaveManagement.Domain.LeaveRequests.Enums;

namespace TF1.LeaveManagement.API.Dtos
{
    public class SubmitLeaveRequestDto
    {
        [Required]
        public Guid? EmployeeId { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }
        [Required]
        [EnumDataType(typeof(LeaveType))]
        public LeaveType? Type { get; set; }
        public string? Comment { get; set; }
    }
}
