using TF1.LeaveManagement.Domain.Common;

namespace TF1.LeaveManagement.Domain.LeaveRequests.ValueObjects
{
    public class  LeaveRequestId : ValueObject<LeaveRequestId>
    {
        public Guid Value { get; private set; }

        public LeaveRequestId()
        {
            Value = Guid.NewGuid();
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Value;
        }
    }
}