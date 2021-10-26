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

        [Fact]
        public void ValidationError_WithData_GivenDataByProperty_SetIt()
        {
            // Arrange
            var data = new { Id = 0, Name = "John" };

            // Act
            var error = new ValidationError("Name", "Invalid name.", data);

            // Assert
            Assert.Equal(data, error.Data);
        }
    }
}
