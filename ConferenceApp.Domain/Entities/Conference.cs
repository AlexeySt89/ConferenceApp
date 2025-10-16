using ConferenceApp.Domain.Common;
using ConferenceApp.Domain.Events;

namespace ConferenceApp.Domain.Entities
{
    public class Conference : EntityBase
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime Date { get; private set; }

        private readonly List<Participant> _participants = new();
        public IReadOnlyCollection<Participant> Participants => _participants.AsReadOnly();

        private Conference() { }

        public Conference(string title, string description, DateTime date)
        {
            SetTitle(title);
            SetDescription(description);
            SetDate(date);
        }

        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Conference title cannot be empty");

            if (title.Length > 200)
                throw new ArgumentException("Conference title is too long");

            Title = title.Trim();
        }

        public void SetDescription(string description)
        {
            Description = description?.Trim() ?? string.Empty;
        }

        public void SetDate(DateTime date)
        {
            if (date < DateTime.UtcNow.AddDays(-1))
                throw new ArgumentException("Conference date cannot be in the past");

            Date = date;
        }
        
        public void AddParticipant(Participant participant)
        {
            if (participant == null) 
                throw new ArgumentNullException(nameof(participant));

            if(_participants.Any(p => p.Email.Value == participant.Email.Value)) 
                throw new InvalidOperationException("Participant already registered for this conference");

            _participants.Add(participant);

            AddDomainEvent(new ParticipantRegisteredEvent(participant.Id, Id, participant.Email.Value));
        }

        public bool RemoveParticipant(Guid participantId)
        {
            var participant = _participants.FirstOrDefault(p => p.Id == participantId);
            return participant != null && _participants.Remove(participant);
        }

        public int GetParticipantsCount() => _participants.Count;
        public bool HasParticipants => _participants.Any();
    }
}
