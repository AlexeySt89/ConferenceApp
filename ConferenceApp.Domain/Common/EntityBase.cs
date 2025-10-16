namespace ConferenceApp.Domain.Common
{
    public abstract class EntityBase
    {
        public Guid Id { get; protected set; }

        private readonly List<DomainEvent> _domainEvents = new();
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected EntityBase() => Id = Guid.NewGuid();
        protected EntityBase(Guid id) => Id = id;

        public void AddDomainEvent(DomainEvent eventItem) => _domainEvents.Add(eventItem);
        public void RemoveDomainEvent(DomainEvent eventItem) => _domainEvents.Remove(eventItem);
        public void ClearDomainEvents() => _domainEvents.Clear();

        public override bool Equals(object? obj)
        {
            if (obj is not EntityBase other) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;
            return Id == other.Id;
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}
