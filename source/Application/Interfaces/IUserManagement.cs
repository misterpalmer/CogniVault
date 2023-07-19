namespace CogniVault.Application.Interfaces;

public interface IUserManagement
{
    bool IsSuperUser { get; }
    long Quota { get; }
    void ChangeQuota(long newQuota);
}