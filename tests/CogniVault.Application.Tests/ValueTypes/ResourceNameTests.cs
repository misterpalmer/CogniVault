using CogniVault.Application.Validators;
using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Tests.ValueTypes;

//  public class ResourceNameTests
// {
//     [Theory]
//     [InlineData("Test", "test", true)]
//     [InlineData("Test1", "Test2", false)]
//     public void Equals_ReturnsExpectedResult(string value1, string value2, bool expectedResult)
//     {
//         // Arrange
//         var name1 = new ResourceName(value1, new FileNameValidator());
//         var name2 = new ResourceName(value2, new FileNameValidator());
//          // Act
//         var result = name1.Equals(name2);
//          // Assert
//         result.Should().Be(expectedResult);
//     }
//      [Theory]
//     [InlineData("Test", "test", true)]
//     [InlineData("Test1", "Test2", false)]
//     public void OperatorEquals_ReturnsExpectedResult(string value1, string value2, bool expectedResult)
//     {
//         // Arrange
//         var name1 = new ResourceName(value1, new FileNameValidator());
//         var name2 = new ResourceName(value2, new FileNameValidator());
//          // Act
//         var result = name1 == name2;
//          // Assert
//         result.Should().Be(expectedResult);
//     }
//      [Theory]
//     [InlineData("Test", "test", false)]
//     [InlineData("Test1", "Test2", true)]
//     public void OperatorNotEquals_ReturnsExpectedResult(string value1, string value2, bool expectedResult)
//     {
//         // Arrange
//         var name1 = new ResourceName(value1, new FileNameValidator());
//         var name2 = new ResourceName(value2, new FileNameValidator());
//          // Act
//         var result = name1 != name2;
//          // Assert
//         result.Should().Be(expectedResult);
//     }
//      [Fact]
//     public void GetHashCode_ReturnsSameValue_WhenValuesAreEqual()
//     {
//         // Arrange
//         var name1 = new ResourceName("Test", new FileNameValidator());
//         var name2 = new ResourceName("test", new FileNameValidator());
//          // Act
//         var hashCode1 = name1.GetHashCode();
//         var hashCode2 = name2.GetHashCode();
//          // Assert
//         hashCode1.Should().Be(hashCode2);
//     }
//      [Fact]
//     public void GetHashCode_ReturnsDifferentValue_WhenValuesAreNotEqual()
//     {
//         // Arrange
//         var name1 = new ResourceName("Test1", new FileNameValidator());
//         var name2 = new ResourceName("Test2", new FileNameValidator());
//          // Act
//         var hashCode1 = name1.GetHashCode();
//         var hashCode2 = name2.GetHashCode();
//          // Assert
//         hashCode1.Should().NotBe(hashCode2);
//     }
//      [Fact]
//     public void ToString_ReturnsValue()
//     {
//         // Arrange
//         var value = "Test";
//         var name = new ResourceName(value, new FileNameValidator());
//          // Act
//         var result = name.ToString();
//          // Assert
//         result.Should().Be(value);
//     }
// }