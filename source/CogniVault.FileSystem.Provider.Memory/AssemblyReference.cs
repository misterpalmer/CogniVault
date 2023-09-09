using System.Reflection;

namespace CogniVault.FileSystem.Provider.Memory;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}