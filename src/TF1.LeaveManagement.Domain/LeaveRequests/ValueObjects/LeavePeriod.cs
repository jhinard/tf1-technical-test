using TF1.LeaveManagement.Domain.Common;

namespace TF1.LeaveManagement.Domain.LeaveRequests.ValueObjects
{
    public class LeavePeriod : ValueObject<LeavePeriod>
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public LeavePeriod(DateTime start, DateTime end)
        {
            if (end < start)
                throw new ArgumentException("End date cannot be earlier than start date.");

            Start = start;
            End = end;
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Start;
            yield return End;
        }
    }
}