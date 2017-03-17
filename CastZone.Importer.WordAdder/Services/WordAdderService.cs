using CastZone.Importer.WordAdder.Persistences;
using System;
using CastZone.Tools.Pipes;
using StructureMap;

namespace CastZone.Importer.WordAdder.Services
{
    public class WordAdderService : IWordAdderService
    {
        private readonly IWordService _wordService;
        private readonly IQueueService _queueService;
        
        [DefaultConstructor]
        public WordAdderService(IWordService wordService, IQueueService queueService)
        {
            _wordService = wordService;
            _queueService = queueService;
        }

        public void Execute(Word word)
        {
            _wordService.ExistsAsync(word.Id)
                .Then()                
                .Map(Validator.IsTrue)
                .Success(x => _wordService.InsertAsync(word))
                .Success(x => _queueService.Enqueue(word));
        }
    }
}
