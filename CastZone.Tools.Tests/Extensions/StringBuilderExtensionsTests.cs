using System.Text;
using Xunit;

namespace CastZone.Tools.Tests.Extensions
{
    public class StringBuilderExtensionsTests
    {
        [Fact]
        public void StringBuilderExtensions_AppendFormattedLine_LineWasAddedCorrecly()
        {
            // Arrange
            var sb = new StringBuilder();

            // Act
            sb.AppendFormattedLine("Test {0}, {1}", 123, 321);

            // Assert
            Assert.Equal(@"Test 123, 321
", sb.ToString());
        }

        [Fact]
        public void StringBuilderExtensions_AppendSequence_LineWasAddedCorrecly()
        {
            // Arrange
            var stringBUilder = new StringBuilder();
            var sequence = new[] { 1, 2, 3, 4 };

            // Act
            stringBUilder.AppendSequence(sequence, (sb, x) => sb.AppendFormattedLine("Test {0}", x));

            // Assert
            Assert.Equal(@"Test 1
Test 2
Test 3
Test 4
", stringBUilder.ToString());
        }
    }
}
