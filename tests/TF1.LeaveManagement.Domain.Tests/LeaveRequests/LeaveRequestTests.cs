using TF1.LeaveManagement.Domain.LeaveRequests;
using TF1.LeaveManagement.Domain.LeaveRequests.Enums;
using TF1.LeaveManagement.Domain.LeaveRequests.ValueObjects;

namespace TF1.LeaveManagement.Domain.Tests.LeaveRequests
{
    public class LeaveRequestTests
    {
        [Fact]
        public void New_LeaveRequest_ShouldInitializePropertiesCorrectly()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var period = new LeavePeriod(DateTime.Today, DateTime.Today.AddDays(5));
            var type = LeaveType.Vacation;
            var comment = "Family trip";

            // Act
            var leaveRequest = new LeaveRequest(employeeId, period, type, comment);

            // Assert
            Assert.Equal(employeeId, leaveRequest.EmployeeId);
            Assert.Equal(period, leaveRequest.Period);
            Assert.Equal(type, leaveRequest.Type);
            Assert.Equal(comment, leaveRequest.Comment);
            Assert.Equal(LeaveStatus.Pending, leaveRequest.Status);
            Assert.True(leaveRequest.CreatedAt > DateTime.MinValue);
        }

        [Fact]
        public void Approve_ShouldSetStatusToApproved()
        {
            // Arrange
            var leaveRequest = new LeaveRequest(Guid.NewGuid(), new LeavePeriod(DateTime.UtcNow, DateTime.UtcNow.AddDays(5)), LeaveType.Vacation, null);

            // Act
            leaveRequest.Approve("Approved by manager");

            // Assert
            Assert.Equal(LeaveStatus.Approved, leaveRequest.Status);
            Assert.Equal("Approved by manager", leaveRequest.ManagerComment);
        }

        [Fact]
        public void Reject_ShouldSetStatusToRejected()
        {
            // Arrange
            var leaveRequest = new LeaveRequest(Guid.NewGuid(), new LeavePeriod(DateTime.UtcNow, DateTime.UtcNow.AddDays(5)), LeaveType.Vacation, null);

            // Act
            leaveRequest.Reject("Rejected due to workload");

            // Assert
            Assert.Equal(LeaveStatus.Rejected, leaveRequest.Status);
            Assert.Equal("Rejected due to workload", leaveRequest.ManagerComment);
        }
    }
}