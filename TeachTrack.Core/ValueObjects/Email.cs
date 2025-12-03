
using TeachTrack.Core.Exceptions;

namespace TeachTrack.Core.ValueObjects;

public record Email {
    public string Value { get; }
    private Email(string value) => Value = value;

    public static Email Create(string email) {
        if (string.IsNullOrWhiteSpace(email)) throw new DomainException("Email is required.");
        if (!email.Contains("@")) throw new DomainException("Invalid email format.");
        return new Email(email);
    }
    
    public static implicit operator string(Email email) => email.Value;
}