using Moq;
using TF1.LeaveManagement.Application.LeaveRequests;
using TF1.LeaveManagement.Application.LeaveRequests.Interfaces;
using TF1.LeaveManagement.Domain.LeaveRequests;
using TF1.LeaveManagement.Domain.LeaveRequests.Enums;
using TF1.LeaveManagement.Domain.LeaveRequests.ValueObjects;

namespace TF1.LeaveManagement.Application.Tests.LeaveRequests
{
    public class LeaveRequestServiceTests
    {
        private readonly Mock<ILeaveRequestRepository> _repositoryMock;
        private readonly LeaveRequestService _service;

        public LeaveRequestServiceTests()
        {
            _repositoryMock = new Mock<ILeaveRequestRepository>();
            _service = new LeaveRequestService(_repositoryMock.Object);
        }

        [Fact]
        public async Task SubmitRequest_ShouldSaveRequestAndReturnId()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var startDate = DateTime.UtcNow;
            var endDate = startDate.AddDays(5);
            var type = LeaveType.Vacation;
            var comment = "Family vacation";

            _repositoryMock.Setup(repo => repo.SaveAsync(It.IsAny<LeaveRequest>())).Returns(Task.CompletedTask);

            // Act
            var result = await _service.SubmitRequest(employeeId, startDate, endDate, type, comment);

            // Assert
            _repositoryMock.Verify(repo => repo.SaveAsync(It.IsAny<LeaveRequest>()), Times.Once);
            Assert.NotEqual(Guid.Empty, result);
        }

        [Fact]
        public async Task ApproveRequest_ShouldApproveAndSaveRequest()
        {
            // Arrange
            var requestId = Guid.NewGuid();
            var managerComment = "Approved !";
            var leaveRequest = new LeaveRequest(Guid.NewGuid(), new LeavePeriod(DateTime.UtcNow, DateTime.UtcNow.AddDays(5)), LeaveType.Vacation, null);

            _repositoryMock.Setup(repo => repo.GetByIdAsync(requestId)).ReturnsAsync(leaveRequest);
            _repositoryMock.Setup(repo => repo.SaveAsync(It.IsAny<LeaveRequest>())).Returns(Task.CompletedTask);

            // Act
            await _service.ApproveRequest(requestId, managerComment);

            // Assert
            Assert.Equal(LeaveStatus.Approved, leaveRequest.Status);
            Assert.Equal(managerComment, leaveRequest.ManagerComment);
            _repositoryMock.Verify(repo => repo.SaveAsync(leaveRequest), Times.Once);
        }

        [Fact]
        public async Task ApproveRequest_ShouldThrowIfRequestNotFound()
        {
            // Arrange
            var requestId = Guid.NewGuid();
            _repositoryMock.Setup(repo => repo.GetByIdAsync(requestId)).ReturnsAsync((LeaveRequest?)null);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.ApproveRequest(requestId, "Manager comment"));
        }

        [Fact]
        public async Task RejectRequest_ShouldRejectAndSaveRequest()
        {
            // Arrange
            var requestId = Guid.NewGuid();
            var managerComment = "Rejected !";
            var leaveRequest = new LeaveRequest(Guid.NewGuid(), new LeavePeriod(DateTime.UtcNow, DateTime.UtcNow.AddDays(5)), LeaveType.Vacation, null);

            _repositoryMock.Setup(repo => repo.GetByIdAsync(requestId)).ReturnsAsync(leaveRequest);
            _repositoryMock.Setup(repo => repo.SaveAsync(It.IsAny<LeaveRequest>())).Returns(Task.CompletedTask);

            // Act
            await _service.RejectRequest(requestId, managerComment);

            // Assert
            Assert.Equal(LeaveStatus.Rejected, leaveRequest.Status);
            Assert.Equal(managerComment, leaveRequest.ManagerComment);
            _repositoryMock.Verify(repo => repo.SaveAsync(leaveRequest), Times.Once);
        }

        [Fact]
        public async Task RejectRequest_ShouldThrowIfRequestNotFound()
        {
            // Arrange
            var requestId = Guid.NewGuid();
            _repositoryMock.Setup(repo => repo.GetByIdAsync(requestId)).ReturnsAsync((LeaveRequest?)null);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.RejectRequest(requestId, "Manager comment"));
        }
    }
}