namespace CogniVault.Application.ValueObjects.UnitTests;

 public class ResourceNameTests
{
    [Theory]
    [InlineData("Test", "test", true)]
    [InlineData("Test1", "Test2", false)]
    public void Equals_ReturnsExpectedResult(string value1, string value2, bool expectedResult)
    {
        // Arrange
        var name1 = new ResourceName(value1);
        var name2 = new ResourceName(value2);
         // Act
        var result = name1.Equals(name2);
         // Assert
        result.Should().Be(expectedResult);
    }
     [Theory]
    [InlineData("Test", "test", true)]
    [InlineData("Test1", "Test2", false)]
    public void OperatorEquals_ReturnsExpectedResult(string value1, string value2, bool expectedResult)
    {
        // Arrange
        var name1 = new ResourceName(value1);
        var name2 = new ResourceName(value2);
         // Act
        var result = name1 == name2;
         // Assert
        result.Should().Be(expectedResult);
    }
     [Theory]
    [InlineData("Test", "test", false)]
    [InlineData("Test1", "Test2", true)]
    public void OperatorNotEquals_ReturnsExpectedResult(string value1, string value2, bool expectedResult)
    {
        // Arrange
        var name1 = new ResourceName(value1);
        var name2 = new ResourceName(value2);
         // Act
        var result = name1 != name2;
         // Assert
        result.Should().Be(expectedResult);
    }
     [Fact]
    public void GetHashCode_ReturnsSameValue_WhenValuesAreEqual()
    {
        // Arrange
        var name1 = new ResourceName("Test");
        var name2 = new ResourceName("test");
         // Act
        var hashCode1 = name1.GetHashCode();
        var hashCode2 = name2.GetHashCode();
         // Assert
        hashCode1.Should().Be(hashCode2);
    }
     [Fact]
    public void GetHashCode_ReturnsDifferentValue_WhenValuesAreNotEqual()
    {
        // Arrange
        var name1 = new ResourceName("Test1");
        var name2 = new ResourceName("Test2");
         // Act
        var hashCode1 = name1.GetHashCode();
        var hashCode2 = name2.GetHashCode();
         // Assert
        hashCode1.Should().NotBe(hashCode2);
    }
     [Fact]
    public void ToString_ReturnsValue()
    {
        // Arrange
        var value = "Test";
        var name = new ResourceName(value);
         // Act
        var result = name.ToString();
         // Assert
        result.Should().Be(value);
    }
}