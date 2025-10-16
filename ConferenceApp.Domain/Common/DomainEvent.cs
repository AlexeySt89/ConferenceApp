namespace ConferenceApp.Domain.Common
{
    public abstract class DomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
        public bool IsPublished { get; set; } = false;
    }
}
