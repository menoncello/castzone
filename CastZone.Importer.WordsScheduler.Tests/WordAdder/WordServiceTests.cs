using System.Threading.Tasks;
using CastZone.Importer.WordAdder.Persistences;
using CastZone.Importer.WordAdder.Services;
using Moq;
using Xunit;

namespace CastZone.Importer.WordAdder.Tests.WordsScheduler
{
    public class WordServiceTests
    {
        [Fact]
        public async Task WordScheduler_Exists_ReturnsTrueIfExists()
        {
            // Arrange
            var mock = new Moq.Mock<IWordPersistence>();
            var service = new WordService(mock.Object);
            const string word = "nerd";
            mock.Setup(x => x.ExistsAsync(word)).ReturnsAsync(true);

            // Act
            var result = await service.ExistsAsync(word);

            // Assert
            Assert.True(result);
        }
        [Fact]
        public async Task WordScheduler_Exists_ReturnsFalseIfNotExists()
        {
            // Arrange
            var mock = new Moq.Mock<IWordPersistence>();
            var service = new WordService(mock.Object);
            const string word = "nerd";
            mock.Setup(x => x.ExistsAsync(word)).ReturnsAsync(false);

            // Act
            var result = await service.ExistsAsync(word);

            // Assert
            Assert.False(result);
        }

        //[Fact]
        //public async Task WordScheduler_Insert_MustCallInsertFromPersistenceOnce()
        //{
        //    // Arrange
        //    var word = new Word("nerd");
        //    var mock = new Mock<IWordPersistence>();
        //    mock.Setup(x => x.InsertAsync(word)); //.Verifiable();

        //    var service = new WordService(mock.Object);

        //    // Act
        //    await service.InsertAsync(word);

        //    // Assert
        //    mock.Verify(x => x.InsertAsync(word), Times.Once());
        //}
    }
}
