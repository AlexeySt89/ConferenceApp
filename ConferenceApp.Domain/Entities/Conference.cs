namespace ConferenceApp.Domain.Entities
{
    public class Conference
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public List<Participant> Participants { get; set; } = new();
    }
}
