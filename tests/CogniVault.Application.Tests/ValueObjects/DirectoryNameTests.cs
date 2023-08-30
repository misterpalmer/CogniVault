using Xunit;
using FluentValidation;
using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Tests.ValueObjects;

public class DirectoryNameTests
{
    [Theory]
    [InlineData("test-dir")]
    [InlineData("another-test-dir")]
    public void Should_Create_DirectoryName(string name)
    {
        // Act
        var directoryName = new DirectoryName(name);

        // Assert
        directoryName.Value.Should().Be(name);
    }

    [Theory]
    [InlineData("invalid/directory/name")]
    [InlineData("another\\invalid\\directory\\name")]
    public void Should_Throw_Exception_For_Invalid_DirectoryName(string name)
    {
        // Act
        Action act = () => new DirectoryName(name);

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Theory]
    [InlineData("test-dir", "test-dir", true)]
    [InlineData("test-dir1", "test-dir2", false)]
    public void Should_Evaluate_DirectoryNames_As_Equal(string name1, string name2, bool expected)
    {
        // Arrange
        var dirName1 = new DirectoryName(name1);
        var dirName2 = new DirectoryName(name2);

        // Act & Assert
        (dirName1 == dirName2).Should().Be(expected);
        (dirName1 != dirName2).Should().Be(!expected);
        dirName1.Equals(dirName2).Should().Be(expected);
    }

    [Theory]
    [InlineData("test-dir")]
    [InlineData("another-test-dir")]
    public void Should_Return_Correct_Copy(string name)
    {
        // Arrange
        var original = new DirectoryName(name);

        // Act
        var copy = original.Copy();

        // Assert
        copy.Should().BeEquivalentTo(original);
        copy.Should().NotBeSameAs(original);
    }
}
