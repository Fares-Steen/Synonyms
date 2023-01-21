using Models;

namespace Application.Services.SynonymServices
{
    public interface IReadSynonymService
    {
        IEnumerable<SynonymsDto> GetSynonyms(string word);
    }
}
