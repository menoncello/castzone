using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CastZone.Importer.WordsScheduler.Models;
using CastZone.Importer.WordsScheduler.Persistences;
using CastZone.Importer.WordsScheduler.Services;
using Moq;
using Xunit;

namespace CastZone.Importer.WordsScheduler.Tests.WordsScheduler
{
    public class WordSchedulerTests
    {
        [Fact]
        public async Task WordScheduler_GetWords_MustNotBeNull()
        {
            // Arrange
            var mock = new Moq.Mock<IWordPersistence>();
            var service = new WordService(mock.Object);

            // Act
            var result = await service.GetWordsAsync();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task WordScheduler_GetWords_MustReturnAllWords()
        {
            // Arrange
            var words = new List<Word> { new Word("f1"), new Word("nerd"), new Word("apple") }.AsQueryable();
            var mock = new Mock<IWordPersistence>();
            mock.Setup(x => x.GetWordsAsync()).ReturnsAsync(words);
            var service = new WordService(mock.Object);

            // Act
            var result = await service.GetWordsAsync();

            // Assert
            Assert.Equal(3, result.Count());
        }
    }
}
