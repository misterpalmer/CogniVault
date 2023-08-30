namespace CogniVault.Platform.Core.Entities;

public class AuditInfo
{
    public string CreatedBy { get; private set; }
    public DateTimeOffset CreatedOnUtc { get; private set; }
    public string ModifiedBy { get; private set; }
    public DateTimeOffset ModifiedOnUtc { get; private set; }

    public AuditInfo(string createdBy, DateTimeOffset createdOnUtc, string modifiedBy, DateTimeOffset modifiedOnUtc)
    {
        CreatedBy = createdBy;
        CreatedOnUtc = createdOnUtc;
        ModifiedBy = modifiedBy;
        ModifiedOnUtc = modifiedOnUtc;
    }

    public void UpdateModification(string modifiedBy, DateTimeOffset modifiedOnUtc)
    {
        ModifiedBy = modifiedBy;
        ModifiedOnUtc = modifiedOnUtc;
    }
}
