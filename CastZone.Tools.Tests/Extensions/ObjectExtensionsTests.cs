using System;
using Xunit;

namespace CastZone.Tools.Tests.Extensions
{
    public class ObjectExtensionsTests
    {
        [Fact]
        public void ObjectExtensions_IsDefault_WhenValueIsNotDefaultReturnFalse()
        {
            // Arrange
            const int value = 123;

            // Act
            var result = value.IsDefault();

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void ObjectExtensions_IsDefault_WhenValueIsDefaultReturnTrue()
        {
            // Arrange
            const int value = 0;

            // Act
            var result = value.IsDefault();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ObjectExtensions_IsNull_WhenValueIsNotNullReturnFalse()
        {
            // Arrange
            const int value = 123;

            // Act
            var result = value.IsNull();

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void ObjectExtensions_IsNull_WhenValueIsNullReturnTrue()
        {
            // Arrange
            object value = null;

            // Act
            var result = value.IsNull();

            // Assert
            Assert.True(result);
        }

    }
}
