using System.Collections.Generic;
using CastZone.Tools.Pipes;
using Xunit;

namespace CastZone.Tools.Tests.Pipes
{
    public class ValidatorTests
    {
        [Fact]
        public void Validator_IsNotNull_FailWhenPassingANullObject()
        {
            // Arrange
            object obj = null;

            // Act
            var result = Validator.IsNotNull(obj);

            // Assert
            Assert.False(result.IsSuccess);
        }
        [Fact]
        public void Validator_IsNotNull_SuccessWhenPassingANotNullObject()
        {
            // Arrange
            object obj = new { };

            // Act
            var result = Validator.IsNotNull(obj);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Validator_Is_FailWhenPassingAObjectDiferentFromTestType()
        {
            // Arrange
            var obj = 12;

            // Act
            var result = Validator.Is<string, object>(obj);

            // Assert
            Assert.False(result.IsSuccess);
        }
        [Fact]
        public void Validator_Is_SuccesWhenPassingAObjectEqualTheTestType()
        {
            // Arrange
            var obj = "12";

            // Act
            var result = Validator.Is<string, object>(obj);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Validator_IsAll_FailWhenPassingOneObjectDiferentFromTestType()
        {
            // Arrange
            var list = new List<object> { 12, "34" };

            // Act
            var result = Validator.IsAll<string, object>(list);

            // Assert
            Assert.False(result.IsSuccess);
        }
        [Fact]
        public void Validator_IsAll_SuccesWhenPassingAllObjectsEqualTheTestType()
        {
            // Arrange
            var list = new List<object> { "12", "34" };

            // Act
            var result = Validator.IsAll<string, object>(list);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Validator_HasValue_FailWhenPassingAEmptyString()
        {
            // Arrange
            var str = "";

            // Act
            var result = Validator.HasValue(str);

            // Assert
            Assert.False(result.IsSuccess);
        }
        [Fact]
        public void Validator_HasValue_SuccesWhenPassingAllObjectsEqualTheTestType()
        {
            // Arrange
            var str = "filled";

            // Act
            var result = Validator.HasValue(str);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Validator_MinLength_FailWhenPassingAStringWithLessCharacters()
        {
            // Arrange
            var str = "asd";
            var minLength = 4;

            // Act
            var result = Validator.MinLength(minLength)(str);

            // Assert
            Assert.False(result.IsSuccess);
        }
        [Fact]
        public void Validator_MinLength_SuccesWhenPassingTheExactlyLengthOfString()
        {
            // Arrange
            var str = "asdf";
            var minLength = 4;

            // Act
            var result = Validator.MinLength(minLength)(str);

            // Assert
            Assert.True(result.IsSuccess);
        }
        [Fact]
        public void Validator_MinLength_SuccesWhenPassingGreaterLengthOfString()
        {
            // Arrange
            var str = "asdfg";
            var minLength = 4;

            // Act
            var result = Validator.MinLength(minLength)(str);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Validator_IsTrue_FailWhenPassingFalse()
        {
            // Arrange
            var value = false;

            // Act
            var result = Validator.IsTrue(value);

            // Assert
            Assert.False(result.IsSuccess);
        }
        [Fact]
        public void Validator_IsTrue_SuccesWhenPassingTrue()
        {
            // Arrange
            var value = true;

            // Act
            var result = Validator.IsTrue(value);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
