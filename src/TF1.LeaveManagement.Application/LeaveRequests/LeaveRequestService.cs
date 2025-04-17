using TF1.LeaveManagement.Application.LeaveRequests.Interfaces;
using TF1.LeaveManagement.Domain.LeaveRequests.Enums;
using TF1.LeaveManagement.Domain.LeaveRequests.ValueObjects;
using TF1.LeaveManagement.Domain.LeaveRequests;

namespace TF1.LeaveManagement.Application.LeaveRequests
{
    public class LeaveRequestService
    {
        private readonly ILeaveRequestRepository _repository;

        public LeaveRequestService(ILeaveRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> SubmitRequest(Guid employeeId, DateTime startDate, DateTime endDate, LeaveType type, string? comment)
        {
            var period = new LeavePeriod(startDate, endDate);
            var request = new LeaveRequest(employeeId, period, type, comment);
            await _repository.SaveAsync(request);
            return request.Id;
        }

        public async Task ApproveRequest(Guid requestId, string? managerComment)
        {
            var request = await _repository.GetByIdAsync(requestId) ?? throw new InvalidOperationException("Request not found");
            request.Approve(managerComment);
            await _repository.SaveAsync(request);
        }

        public async Task RejectRequest(Guid requestId, string? managerComment)
        {
            var request = await _repository.GetByIdAsync(requestId) ?? throw new InvalidOperationException("Request not found");
            request.Reject(managerComment);
            await _repository.SaveAsync(request);
        }
    }
}