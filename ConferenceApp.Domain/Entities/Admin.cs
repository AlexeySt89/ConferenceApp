using ConferenceApp.Domain.Common;
using ConferenceApp.Domain.Common.ValueObjects;

public class Admin : EntityBase
{
    public Email Email { get; private set; }
    public Password Password { get; private set; } 

    private Admin() { }

    public Admin(Email email, Password password)
    {
        Email = email;
        Password = password;
    }

    public void ChangePassword(Password newPassword)
    {
        Password = newPassword;
    }

    public bool CanManageParticipants => true;
    public bool CanManageConferences => true;

}
