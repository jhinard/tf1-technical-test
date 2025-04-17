using TF1.LeaveManagement.Domain.LeaveRequests.Enums;

namespace TF1.LeaveManagement.API.Dtos
{
    public class SubmitLeaveRequestDto
    {
        public Guid EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveType Type { get; set; }
        public string? Comment { get; set; }
    }
}
