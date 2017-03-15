using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CastZone.Importer.WordsScheduler.Services;
using System.Threading.Tasks;
using CastZone.Importer.WordsScheduler.Models;
using CastZone.Importer.WordsScheduler.Persistences;
using Moq;

namespace CastZone.Importer.Tests.WordsScheduler
{
    [TestClass]
    public class WordServiceTests
    {
        [TestMethod]
        public async Task WordScheduler_GetWords_MustNotBeNull()
        {
            // Arrange
            var mock = new Moq.Mock<IWordPersistence>();
            var service = new WordService(mock.Object);

            // Act
            var result = await service.GetWordsAsync();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
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
            Assert.AreEqual(3, result.Count());
        }
    }
}
