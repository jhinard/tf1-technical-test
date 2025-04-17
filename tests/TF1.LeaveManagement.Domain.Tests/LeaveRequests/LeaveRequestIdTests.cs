using TF1.LeaveManagement.Domain.LeaveRequests.ValueObjects;

namespace TF1.LeaveManagement.Domain.Tests.LeaveRequests
{
    public class LeaveRequestIdTests
    {
        [Fact]
        public void Constructor_ShouldGenerateNewGuid()
        {
            // Act
            var leaveRequestId = new LeaveRequestId();

            // Assert
            Assert.NotEqual(Guid.Empty, leaveRequestId.Value);
        }
    }
}