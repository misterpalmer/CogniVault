namespace CogniVault.Platform.Core.RestApi.Controllers.Swagger;

// The attribute for custom example values in Swagger.
[AttributeUsage(
    AttributeTargets.Class |
    AttributeTargets.Struct |
    AttributeTargets.Parameter |
    AttributeTargets.Property |
    AttributeTargets.Enum)]
public class SchemaExampleAttribute : Attribute
{
    public SchemaExampleAttribute(string value)
    {
        ExampleText = value;
    }

    public string ExampleText { get; set; }
}