namespace TF1.LeaveManagement.Domain.Common
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        protected Entity(Guid id)
        {
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType()) return false;
            var other = (Entity)obj;
            return Id.Equals(other.Id);
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}