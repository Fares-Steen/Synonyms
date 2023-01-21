using Entities.Entities;
using Models;

namespace Application.IRepositories
{
    public interface ISynonymsRepository
    {
        List<Word> _synonyms { get; set; }

        void AddSynonym(string word, string synonym);

        IEnumerable<SynonymsDto> GetSynonyms(string word);

        void RemoveAll();
    }
}
