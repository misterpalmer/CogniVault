namespace CogniVault.Application.Tests.ValueTypes;

public class EmailTests
{
    [Theory]
    [InlineData("test@example.com")]
    [InlineData("another@example.com")]
    public void Email_WithValidValue_ShouldSetCorrectValue(string value)
    {
        // Arrange

        // Act
        var email = new Email(value);

        // Assert
        email.Value.Should().Be(value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("invalidemail")]
    public void Email_WithInvalidValue_ShouldThrowArgumentException(string value)
    {
        // Arrange

        // Act
        var action = new Action(() => new Email(value));

        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("Invalid email address (Parameter 'value')");
    }

    [Fact]
    public void Email_ImplicitConversionToString_ShouldReturnCorrectValue()
    {
        // Arrange
        var email = new Email("test@example.com");

        // Act
        string result = email;

        // Assert
        result.Should().Be("test@example.com");
    }

    [Fact]
    public void Email_ExplicitConversionFromString_ShouldCreateEmailInstance()
    {
        // Arrange

        // Act
        Email email = (Email)"test@example.com";

        // Assert
        email.Value.Should().Be("test@example.com");
    }

    [Fact]
    public void Email_Equals_ShouldReturnTrueForEqualEmails()
    {
        // Arrange
        var email1 = new Email("test@example.com");
        var email2 = new Email("test@example.com");

        // Act
        bool result = email1.Equals(email2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Email_Equals_ShouldReturnFalseForDifferentEmails()
    {
        // Arrange
        var email1 = new Email("test@example.com");
        var email2 = new Email("another@example.com");

        // Act
        bool result = email1.Equals(email2);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Email_Equals_ShouldReturnFalseForNull()
    {
        // Arrange
        var email = new Email("test@example.com");

        // Act
        bool result = email.Equals(null);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Email_Equals_ShouldReturnFalseForDifferentObjectType()
    {
        // Arrange
        var email = new Email("test@example.com");
        var otherObject = new object();

        // Act
        bool result = email.Equals(otherObject);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Email_GetHashCode_ShouldReturnSameHashCodeForEqualEmails()
    {
        // Arrange
        var email1 = new Email("test@example.com");
        var email2 = new Email("test@example.com");

        // Act
        int hashCode1 = email1.GetHashCode();
        int hashCode2 = email2.GetHashCode();

        // Assert
        hashCode1.Should().Be(hashCode2);
    }

    [Fact]
    public void Email_ToString_ShouldReturnCorrectValue()
    {
        // Arrange
        var email = new Email("test@example.com");

        // Act
        string result = email.ToString();

        // Assert
        result.Should().Be("test@example.com");
    }
}