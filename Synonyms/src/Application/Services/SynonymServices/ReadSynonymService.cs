using Application.Extensions;
using Application.IRepositories;
using Models;

namespace Application.Services.SynonymServices
{
    public class ReadSynonymService : IReadSynonymService
    {
        private readonly ISynonymsRepository _synonymsRepository;

        public ReadSynonymService(ISynonymsRepository synonymsRepository)
        {
            _synonymsRepository = synonymsRepository;
        }

        public IEnumerable<SynonymsDto> GetSynonyms(string word)
        {
            IEnumerable<SynonymsDto> result = _synonymsRepository.GetSynonyms(word.ClearWord());
            return result;
        }

    }
}
