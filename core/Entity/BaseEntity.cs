namespace Core.Entity
{
    using System.ComponentModel.DataAnnotations;
    using Core.Abstractions;
    using MediatR;
    using T = Guid;

    public abstract class BaseEntity : IAggregateRoot
    {
        public T Id { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public T CreatedBy { get; private set; }
        public DateTime UpdatedOn { get; private set; }
        public T UpdatedBy { get; private set; }
        public bool IsActive { get; private set; } = false;

        [Timestamp]
        public byte[] RowVersion { get; private set; }

        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public bool IsTransient()
        {
            return this.Id == default;
        }

        public Guid SetId()
        {
            Id = Guid.NewGuid();
            return Id;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is BaseEntity))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            BaseEntity item = (BaseEntity)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id == this.Id;
        }

        public BaseEntity()
        {
            Create();
        }

        public void DeActive()
        {
            IsActive = false;
        }

        public void Active()
        {
            IsActive = true;
        }

        private void Create()
        {
            if (IsGuid())
                Id = Guid.NewGuid();
        }

        private static bool IsGuid()
        {
            return typeof(T) == typeof(Guid);
        }
    }
}
