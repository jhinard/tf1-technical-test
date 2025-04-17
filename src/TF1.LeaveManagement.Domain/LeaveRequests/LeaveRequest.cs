using TF1.LeaveManagement.Domain.Common;
using TF1.LeaveManagement.Domain.LeaveRequests.Enums;
using TF1.LeaveManagement.Domain.LeaveRequests.ValueObjects;

namespace TF1.LeaveManagement.Domain.LeaveRequests
{
    public class LeaveRequest : AggregateRoot
    {
        public Guid EmployeeId { get; }
        public LeavePeriod Period { get; }
        public LeaveType Type { get; }
        public string? Comment { get; }
        public LeaveStatus Status { get; private set; }
        public string? ManagerComment { get; private set; }
        public DateTime CreatedAt { get; }

        public LeaveRequest(Guid employeeId, LeavePeriod period, LeaveType type, string? comment) : base(Guid.NewGuid())
        {
            EmployeeId = employeeId;
            Period = period;
            Type = type;
            Comment = comment;
            Status = LeaveStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        public void Approve(string? managerComment = null)
        {
            Status = LeaveStatus.Approved;
            ManagerComment = managerComment;
        }

        public void Reject(string? managerComment = null)
        {
            Status = LeaveStatus.Rejected;
            ManagerComment = managerComment;
        }
    }
}