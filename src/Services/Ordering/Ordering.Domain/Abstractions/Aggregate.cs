namespace Ordering.Domain.Abstractions
{
    public abstract class Aggregate<T> : Entity<T>, IAggregate<T> 
    {
        public readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
        public IDomainEvent[] ClearDomainEvents()
        {
           var domainEventsToClear = _domainEvents.ToArray();

            _domainEvents.Clear();

            return domainEventsToClear; 
        }
    }
}
