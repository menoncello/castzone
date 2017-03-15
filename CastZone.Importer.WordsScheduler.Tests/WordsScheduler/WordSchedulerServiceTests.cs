using System.Collections.Generic;
using System.Linq;
using CastZone.Importer.WordsScheduler.Models;
using CastZone.Importer.WordsScheduler.Services;
using Moq;
using Xunit;

namespace CastZone.Importer.WordsScheduler.Tests.WordsScheduler
{
    public class WordSchedulerServiceTests
    {
        private static readonly IEnumerable<Word> DefaultWords = new List<Word>
        {
            new Word("nerd"),
            new Word("f1"),
            new Word("apple"),
            new Word("development"),
            new Word("movie"),
        };

        [Fact]
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
                addQueueServiceMock.Object, queueServiceMock.Object, DefaultWords);

            // Act      
            service.Execute();

            // Assert
            wordServiceMock.Verify(x => x.GetWordsAsync(), Times.Once);
        }
        [Fact]
        public void WordSchedulerService_ExecuteAsync_IfReturnsNoWordsExecuteEnqueueWordsWithDefaultWordsOnce()
        {
            // Arrange            
            var wordServiceMock = new Mock<IWordService>();
            var queueServiceMock = new Mock<IQueueService>();
            var addQueueServiceMock = new Mock<IAddQueueService>();

            wordServiceMock.Setup(x => x.GetWordsAsync())
                .ReturnsAsync(new List<Word>());
            addQueueServiceMock.Setup(x => x.Enqueue(DefaultWords))
                .Verifiable();

            var service = new WordSchedulerService(wordServiceMock.Object,
                addQueueServiceMock.Object, queueServiceMock.Object, DefaultWords);

            // Act
            service.Execute();

            // Assert
            addQueueServiceMock.Verify(x => x.Enqueue(DefaultWords), Times.Once());
        }
        [Fact]
        public void WordSchedulerService_ExecuteAsync_IfReturnsManyWordsExecuteEnqueueNeverCallWords()
        {
            // Arrange            
            var words = new List<Word> { new Word("f1"), new Word("nerd"), new Word("apple") }.AsQueryable();
            var wordServiceMock = new Mock<IWordService>();
            var addQueueServiceMock = new Mock<IAddQueueService>();
            var queueServiceMock = new Mock<IQueueService>();

            wordServiceMock.Setup(x => x.GetWordsAsync())
                .ReturnsAsync(words);
            addQueueServiceMock.Setup(x => x.Enqueue(DefaultWords))
                .Verifiable();

            var service = new WordSchedulerService(wordServiceMock.Object,
                addQueueServiceMock.Object, queueServiceMock.Object, DefaultWords);

            // Act
            service.Execute();

            // Assert
            addQueueServiceMock.Verify(x => x.Enqueue(DefaultWords), Times.Never());
        }
        [Fact]
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
                addQueueServiceMock.Object, queueServiceMock.Object, DefaultWords);

            // Act
            service.Execute();

            // Assert
            queueServiceMock.Verify(x => x.Enqueue(words), Times.Never());
        }
        [Fact]
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
                addQueueServiceMock.Object, queueServiceMock.Object, DefaultWords);

            // Act
            service.Execute();

            // Assert
            queueServiceMock.Verify(x => x.Enqueue(words), Times.Once());
        }
    }
}
