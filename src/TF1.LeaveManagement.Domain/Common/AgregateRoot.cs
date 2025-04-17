namespace TF1.LeaveManagement.Domain.Common
{
    public abstract class AggregateRoot : Entity
    {
        protected AggregateRoot(Guid id) : base(id) { }
    }
}