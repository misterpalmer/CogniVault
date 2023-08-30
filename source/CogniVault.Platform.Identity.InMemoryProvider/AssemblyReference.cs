using System.Reflection;

namespace CogniVault.Platform.Identity.InMemoryProvider;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}