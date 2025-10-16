using ConferenceApp.Domain.Interfaces;
using System.Collections.Generic;

namespace ConferenceApp.Domain.Common.ValueObjects
{
    public record class Password
    {
        public string Hash { get; }
        public bool IsHashed { get; }

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
            => IsHashed && hasher.VerifyPassword(plainPassword, Hash);

        public override string ToString() => "[PROTECTED]";
    }
}
