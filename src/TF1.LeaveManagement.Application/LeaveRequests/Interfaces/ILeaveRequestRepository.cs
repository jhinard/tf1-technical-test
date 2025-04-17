using TF1.LeaveManagement.Domain.LeaveRequests;

namespace TF1.LeaveManagement.Application.LeaveRequests.Interfaces
{
    public interface ILeaveRequestRepository
    {
        Task SaveAsync(LeaveRequest leaveRequest);
        Task<LeaveRequest?> GetByIdAsync(Guid id);
    }
}