namespace BuildingBlocks.Messaging.Events;

public record IntegrationEvent
{
    Guid EventId => Guid.NewGuid();
    public DateTime OccuredOn => DateTime.Now;

    public string Eventtype => GetType().AssemblyQualifiedName;
}
