using System;
using System.Collections.Generic;
using CastZone.Tools.Pipes;
using CastZone.Importer.WordsScheduler.Persistences;
using CastZone.Tools.Aspect;
using System.Threading.Tasks;

namespace CastZone.Importer.WordsScheduler.Services
{
    public class WordSchedulerService : IWordSchedulerService
    {
        private readonly IWordService _wordService;
        private readonly IQueueService _queueService;
        private readonly IAddQueueService _addQueueService;

        private readonly IReadOnlyList<Word> _defaultWords;

        public WordSchedulerService(IWordService wordService, IAddQueueService addQueueService, 
            IQueueService queueService, IEnumerable<Word> defaultWords)
        {
            _wordService = wordService;
            _queueService = queueService;
            _addQueueService = addQueueService;

            _defaultWords = new List<Word>((defaultWords ?? new List<Word>())).AsReadOnly();
        }

        [Logging]
        public async Task ExecuteAsync()
        {
            (await _wordService.GetWordsAsync())
                .Map(Validator.IsNotEmpty)
                .Failure(() => _addQueueService.Enqueue(_defaultWords))            
                .Success(x => _queueService.Enqueue(x));
        }
    }
}
