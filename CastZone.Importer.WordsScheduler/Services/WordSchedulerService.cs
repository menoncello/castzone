using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CastZone.Importer.WordsScheduler.Models;
using CastZone.Tools.Pipes;

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

        public void Execute()
        {
            _wordService.GetWordsAsync()
                .Then()
                .Map(Validator.IsNotEmpty)
                .Failure(() => _addQueueService.Enqueue(_defaultWords))            
                .Success(x => _queueService.Enqueue(x));
        }
    }
}
