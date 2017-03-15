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
    public class WordSchedulerServiceTests
    {
        private static IEnumerable<Word> _defaultWords = new List<Word>
        {
            new Word("nerd"),
            new Word("f1"),
            new Word("apple"),
            new Word("development"),
            new Word("movie"),
        };

        [TestMethod]
        public void WordSchedulerService_ExecuteAsync_MustCallGetWordsOnce()
        {
            // Arrange
            var words = new List<Word>();
            var wordServiceMock = new Mock<IWordService>();
            var addQueueServiceMock = new Mock<IAddQueueService>();
            var queueServiceMock = new Mock<IQueueService>();

            wordServiceMock.Setup(x => x.GetWordsAsync())
                .ReturnsAsync(words);
            queueServiceMock.Setup(x => x.Enqueue(It.IsAny<List<Word>>()));
            var service = new WordSchedulerService(wordServiceMock.Object,
                addQueueServiceMock.Object, queueServiceMock.Object, _defaultWords);

            // Act      
            service.Execute();

            // Assert
            wordServiceMock.Verify(x => x.GetWordsAsync(), Times.Once);
        }
        [TestMethod]
        public void WordSchedulerService_ExecuteAsync_IfReturnsNoWordsExecuteEnqueueWordsWithDefaultWordsOnce()
        {
            // Arrange            
            var wordServiceMock = new Mock<IWordService>();
            var queueServiceMock = new Mock<IQueueService>();
            var addQueueServiceMock = new Mock<IAddQueueService>();

            wordServiceMock.Setup(x => x.GetWordsAsync())
                .ReturnsAsync(new List<Word>());
            addQueueServiceMock.Setup(x => x.Enqueue(_defaultWords))
                .Verifiable();

            var service = new WordSchedulerService(wordServiceMock.Object,
                addQueueServiceMock.Object, queueServiceMock.Object, _defaultWords);

            // Act
            service.Execute();

            // Assert
            addQueueServiceMock.Verify(x => x.Enqueue(_defaultWords), Times.Once());
        }
        [TestMethod]
        public void WordSchedulerService_ExecuteAsync_IfReturnsManyWordsExecuteEnqueueNeverCallWords()
        {
            // Arrange            
            var words = new List<Word> { new Word("f1"), new Word("nerd"), new Word("apple") }.AsQueryable();
            var wordServiceMock = new Mock<IWordService>();
            var addQueueServiceMock = new Mock<IAddQueueService>();
            var queueServiceMock = new Mock<IQueueService>();

            wordServiceMock.Setup(x => x.GetWordsAsync())
                .ReturnsAsync(words);
            addQueueServiceMock.Setup(x => x.Enqueue(_defaultWords))
                .Verifiable();

            var service = new WordSchedulerService(wordServiceMock.Object,
                addQueueServiceMock.Object, queueServiceMock.Object, _defaultWords);

            // Act
            service.Execute();

            // Assert
            addQueueServiceMock.Verify(x => x.Enqueue(_defaultWords), Times.Never());
        }
        [TestMethod]
        public void WordSchedulerService_ExecuteAsync_IfReturnsNoWordsCannotExecuteQueueService()
        {
            // Arrange            
            var words = new List<Word>().AsQueryable();
            var wordServiceMock = new Mock<IWordService>();
            var queueServiceMock = new Mock<IQueueService>();
            var addQueueServiceMock = new Mock<IAddQueueService>();

            wordServiceMock.Setup(x => x.GetWordsAsync())
                .ReturnsAsync(words);
            queueServiceMock.Setup(x => x.Enqueue(words))
                .Verifiable();

            var service = new WordSchedulerService(wordServiceMock.Object,
                addQueueServiceMock.Object, queueServiceMock.Object, _defaultWords);

            // Act
            service.Execute();

            // Assert
            queueServiceMock.Verify(x => x.Enqueue(words), Times.Never());
        }
        [TestMethod]
        public void WordSchedulerService_ExecuteAsync_IfReturnsManyWordsExecuteCallsQueue()
        {
            // Arrange            
            var words = new List<Word> { new Word("f1"), new Word("nerd"), new Word("apple") }.AsQueryable();
            var wordServiceMock = new Mock<IWordService>();
            var addQueueServiceMock = new Mock<IAddQueueService>();
            var queueServiceMock = new Mock<IQueueService>();

            wordServiceMock.Setup(x => x.GetWordsAsync())
                .ReturnsAsync(words);
            queueServiceMock.Setup(x => x.Enqueue(words))
                .Verifiable();

            var service = new WordSchedulerService(wordServiceMock.Object,
                addQueueServiceMock.Object, queueServiceMock.Object, _defaultWords);

            // Act
            service.Execute();

            // Assert
            queueServiceMock.Verify(x => x.Enqueue(words), Times.Once());
        }
    }
}
