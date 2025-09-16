using System;

namespace CleanArchitecture.Domain.Entities;

public class User : BaseAuditableEntity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;
    public UserRole Role { get; private set; }
    public bool IsActive { get; private set; } = true;

    // Navigation properties
    public ICollection<Order> Orders { get; private set; } = [];

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    private User() { } // EF Constructor

    public static User Create(string firstName, string lastName, string email, UserRole role = UserRole.Customer)
    {
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = Email.Create(email),
            Role = role
        };

        user._domainEvents.Add(new UserCreatedEvent(user.Id, email, firstName, lastName));
        return user;
    }

    public void UpdateProfile(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        SetUpdatedAt();
    }

    public void Deactivate()
    {
        IsActive = false;
        SetUpdatedAt();
    }

    public void Activate()
    {
        IsActive = true;
        SetUpdatedAt();
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
}