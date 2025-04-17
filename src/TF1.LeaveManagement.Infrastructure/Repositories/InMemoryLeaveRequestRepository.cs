using TF1.LeaveManagement.Application.LeaveRequests.Interfaces;
using TF1.LeaveManagement.Domain.LeaveRequests;

namespace TF1.LeaveManagement.Infrastructure.Repositories
{
    public class InMemoryLeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly Dictionary<Guid, LeaveRequest> _inMemoryDatabase = new();

        public Task<LeaveRequest?> GetByIdAsync(Guid id)
        {
            _inMemoryDatabase.TryGetValue(id, out var request);
            return Task.FromResult(request);
        }

        public Task SaveAsync(LeaveRequest leaveRequest)
        {
            _inMemoryDatabase[leaveRequest.Id] = leaveRequest;
            return Task.CompletedTask;
        }
    }
}