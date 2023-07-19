using CogniVault.Application.Interfaces;


namespace CogniVault.Application.ValueObjects.UnitTests;


public class GroupNameTests
{
    private Mock<IGroupNameValidator> validatorMock;

    public GroupNameTests()
    {
        validatorMock = new Mock<IGroupNameValidator>();
    }

    [Theory]
    [InlineData("Admins", true)]
    [InlineData("InvalidName", false)]
    public void GroupName_Constructor_Should_Use_Validator(string groupName, bool isValid)
    {
        // Arrange
        validatorMock.Setup(v => v.Validate(groupName)).Returns(isValid);

        if (isValid)
        {
            // Act
            var group = new GroupName(groupName, validatorMock.Object);

            // Assert
            group.Value.Should().Be(groupName);
        }
        else
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new GroupName(groupName, validatorMock.Object));
        }

        validatorMock.Verify(v => v.Validate(groupName), Times.Once);
    }

    [Theory]
    [InlineData("Admins", "Admins", true)]
    [InlineData("Admins", "admins", true)] // Case doesn't matters
    [InlineData("Admins", "Users", false)]
    public void GroupName_Equals_Should_Compare_Value_Correctly(string name1, string name2, bool expected)
    {
        // Arrange
        validatorMock.Setup(v => v.Validate(It.IsAny<string>())).Returns(true);
        var groupName1 = new GroupName(name1, validatorMock.Object);
        var groupName2 = new GroupName(name2, validatorMock.Object);

        // Act
        var result = groupName1.Equals(groupName2);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("Admins", "Admins", true)]
    [InlineData("Admins", "admins", true)] // Case doesn't matters
    [InlineData("Admins", "Users", false)]
    public void GroupName_HashCode_Should_Match_If_Values_Are_Equal(string name1, string name2, bool expected)
    {
        // Arrange
        validatorMock.Setup(v => v.Validate(It.IsAny<string>())).Returns(true);
        var groupName1 = new GroupName(name1, validatorMock.Object);
        var groupName2 = new GroupName(name2, validatorMock.Object);

        // Act
        var hash1 = groupName1.GetHashCode();
        var hash2 = groupName2.GetHashCode();

        // Assert
        var areHashCodesEqual = hash1 == hash2;
        areHashCodesEqual.Should().Be(expected);
    }
}

