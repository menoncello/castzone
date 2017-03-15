using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CastZone.Tools.Tests.Extensions
{
    [TestClass]
    public class ObjectExtensionsTests
    {
        [TestMethod]
        public void ObjectExtensions_IsDefault_WhenValueIsNotDefaultReturnFalse()
        {
            // Arrange
            var value = 123;

            // Act
            var result = value.IsDefault();

            // Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void ObjectExtensions_IsDefault_WhenValueIsDefaultReturnTrue()
        {
            // Arrange
            var value = 0;

            // Act
            var result = value.IsDefault();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ObjectExtensions_IsNull_WhenValueIsNotNullReturnFalse()
        {
            // Arrange
            var value = 123;

            // Act
            var result = value.IsNull();

            // Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void ObjectExtensions_IsNull_WhenValueIsNullReturnTrue()
        {
            // Arrange
            object value = null;

            // Act
            var result = value.IsNull();

            // Assert
            Assert.IsTrue(result);
        }

    }
}
