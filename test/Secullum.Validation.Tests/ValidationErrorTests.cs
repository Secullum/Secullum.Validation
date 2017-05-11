using Xunit;

namespace Secullum.Validation.Tests
{
    public class ValidationErrorTests
    {
        [Fact]
        public void ValidationError_PascalCase_GivenPropertyAndMessage_SetIt()
        {
            // Arrange
            ValidationError.CamelCase = false;

            // Act
            var error = new ValidationError("Name", "Invalid name.");

            // Assert
            Assert.Equal("Name", error.Property);
        }

        [Fact]
        public void ValidationError_CamelCase_GivenPropertyAndMessage_SetIt()
        {
            // Arrange
            ValidationError.CamelCase = true;

            // Act
            var error = new ValidationError("Name", "Invalid name.");

            // Assert
            Assert.Equal("name", error.Property);
        }
    }
}
