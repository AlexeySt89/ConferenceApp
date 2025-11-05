using ConferenceApp.Domain.Interfaces.Services;

namespace ConferenceApp.Domain.Common.ValueObjects
{
    public record class Password
    {
        public string Hash { get; }
        public bool IsHashed { get; }

        private Password() { }

        private Password(string hash, bool isHashed)
        {
            Hash = hash;
            IsHashed = isHashed;
        }

        public static Password CreateFromPlainText(string plainPassword, IPasswordHasher hasher)
        {
            if (string.IsNullOrWhiteSpace(plainPassword) || plainPassword.Length < 6)
                throw new ArgumentException("Password must be at least 6 characters");

            var hash = hasher.HashPassword(plainPassword);
            return new Password(hash, true);
        }

        public static Password CreateFromHash(string hash) => new(hash, true);

        public bool Verify(string plainPassword, IPasswordHasher hasher)
            => hasher.VerifyPassword(plainPassword, Hash);// && IsHashed

        public override string ToString() => "[PROTECTED]";
    }
}
