namespace ConferenceApp.Domain.Common.ValueObjects
{
    public record class Email
    {
        public string Value { get; }

        private Email() { }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !value.Contains('@'))
                throw new ArgumentException("Invalid email format");

            Value = value.Trim().ToLower();
        }

        public static implicit operator string(Email email) => email.Value;
        public static explicit operator Email(string value) => new(value);

        public override string ToString() => Value;
    }
}
