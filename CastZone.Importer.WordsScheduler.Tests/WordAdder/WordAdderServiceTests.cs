using System.Collections.Generic;
using System.Linq;
using CastZone.Importer.WordAdder.Services;
using Moq;
using Xunit;
using CastZone.Importer.WordAdder.Persistences;

namespace CastZone.Importer.WordsScheduler.Tests.WordsScheduler
{
    public class WordAdderServiceTests
    {
        [Fact]
        public void WordAdderService_Execute_MustCallWordExitsOnce()
        {
            // Arrange
            var word = new Word("nerd");
            var wordServiceMock = new Mock<IWordService>();
            var queueServiceMock = new Mock<IQueueService>();

            wordServiceMock.Setup(x => x.ExistsAsync(word.Id))
                .ReturnsAsync(true);
            queueServiceMock.Setup(x => x.Enqueue(It.IsAny<Word>()));
            var service = new WordAdderService(wordServiceMock.Object, queueServiceMock.Object);

            // Act
            service.Execute(word);

            // Assert
            wordServiceMock.Verify(x => x.ExistsAsync(word.Id), Times.Once);
        }

        [Fact]
        public void WordAdderService_Execute_MustCallWordInsertOnceIfExitsIsTrue()
        {
            // Arrange
            var word = new Word("nerd");
            var wordServiceMock = new Mock<IWordService>();
            var queueServiceMock = new Mock<IQueueService>();

            wordServiceMock.Setup(x => x.ExistsAsync(word.Id))
                .ReturnsAsync(true);
            queueServiceMock.Setup(x => x.Enqueue(It.IsAny<Word>()));
            var service = new WordAdderService(wordServiceMock.Object, queueServiceMock.Object);

            // Act
            service.Execute(word);

            // Assert
            wordServiceMock.Verify(x => x.InsertAsync(word), Times.Once);
        }
        [Fact]
        public void WordAdderService_Execute_MustCallWordEnqueueOnceIfExitsIsTrue()
        {
            // Arrange
            var word = new Word("nerd");
            var wordServiceMock = new Mock<IWordService>();
            var queueServiceMock = new Mock<IQueueService>();

            wordServiceMock.Setup(x => x.ExistsAsync(word.Id))
                .ReturnsAsync(true);
            queueServiceMock.Setup(x => x.Enqueue(word));

            var service = new WordAdderService(wordServiceMock.Object, queueServiceMock.Object);

            // Act
            service.Execute(word);

            // Assert
            queueServiceMock.Verify(x => x.Enqueue(word), Times.Once);
        }

        [Fact]
        public void WordAdderService_Execute_NeverCallWordInsertOnceIfExitsIsFalse()
        {
            // Arrange
            var word = new Word("nerd");
            var wordServiceMock = new Mock<IWordService>();
            var queueServiceMock = new Mock<IQueueService>();

            wordServiceMock.Setup(x => x.ExistsAsync(word.Id))
                .ReturnsAsync(false);
            queueServiceMock.Setup(x => x.Enqueue(It.IsAny<Word>()));
            var service = new WordAdderService(wordServiceMock.Object, queueServiceMock.Object);

            // Act
            service.Execute(word);

            // Assert
            wordServiceMock.Verify(x => x.InsertAsync(word), Times.Never);
        }
        [Fact]
        public void WordAdderService_Execute_NeverCallWordEnqueueOnceIfExitsIsFalse()
        {
            // Arrange
            var word = new Word("nerd");
            var wordServiceMock = new Mock<IWordService>();
            var queueServiceMock = new Mock<IQueueService>();

            wordServiceMock.Setup(x => x.ExistsAsync(word.Id))
                .ReturnsAsync(false);
            queueServiceMock.Setup(x => x.Enqueue(word));

            var service = new WordAdderService(wordServiceMock.Object, queueServiceMock.Object);

            // Act
            service.Execute(word);

            // Assert
            queueServiceMock.Verify(x => x.Enqueue(word), Times.Never);
        }
    }
}
