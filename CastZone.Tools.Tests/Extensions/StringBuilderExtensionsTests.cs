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
            var checkSB = new StringBuilder();
            checkSB.AppendLine("Test 123, 321");

            // Act
            sb.AppendFormattedLine("Test {0}, {1}", 123, 321);

            // Assert
            Assert.Equal(checkSB.ToString(), sb.ToString());
        }

        [Fact]
        public void StringBuilderExtensions_AppendSequence_LineWasAddedCorrecly()
        {
            // Arrange
            var stringBuilder = new StringBuilder();
            var sequence = new[] { 1, 2, 3, 4 };
            var checkSB = new StringBuilder();
            checkSB.AppendLine("Test 1");
            checkSB.AppendLine("Test 2");
            checkSB.AppendLine("Test 3");
            checkSB.AppendLine("Test 4");

            // Act
            stringBuilder.AppendSequence(sequence, (sb, x) => sb.AppendFormattedLine("Test {0}", x));

            // Assert
            Assert.Equal(checkSB.ToString(), stringBuilder.ToString());
        }
    }
}
