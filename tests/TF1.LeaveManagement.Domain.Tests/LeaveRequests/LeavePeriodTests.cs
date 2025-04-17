using TF1.LeaveManagement.Domain.LeaveRequests.ValueObjects;

namespace TF1.LeaveManagement.Domain.Tests.LeaveRequests
{
    public class LeavePeriodTests
    {
        [Fact]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            // Arrange
            var start = DateTime.UtcNow;
            var end = start.AddDays(5);

            // Act
            var leavePeriod = new LeavePeriod(start, end);

            // Assert
            Assert.Equal(start, leavePeriod.Start);
            Assert.Equal(end, leavePeriod.End);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenEndDateIsBeforeStartDate()
        {
            // Arrange
            var start = DateTime.UtcNow;
            var end = start.AddDays(-1);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new LeavePeriod(start, end));
        }
    }
}