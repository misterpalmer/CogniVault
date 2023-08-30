namespace CogniVault.Platform.Core.Entities;

public interface IAuditableEntity
{
    public string CreatedBy { get; set; }
    public DateTimeOffset CreatedOnUtc { get; set; }
    public string ModifiedBy { get; set; }
    public DateTimeOffset ModifiedOnUtc { get; set; }
}