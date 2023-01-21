using Application.Extensions;
using Application.IRepositories;
using Entities.Exceptions;
using Models;

namespace Application.Services.SynonymServices
{
    public class CreateSynonymService : ICreateSynonymService
    {
        private readonly ISynonymsRepository _synonymsRepository;

        public CreateSynonymService(ISynonymsRepository synonymsRepository)
        {
            _synonymsRepository = synonymsRepository;
        }

        public void AddSynonym(AddSynonymDto addSynonymDto)
        {
            ValidateAddSynonym(addSynonymDto);

            _synonymsRepository.AddSynonym(addSynonymDto.Word.ClearWord(), addSynonymDto.Synonym.ClearWord());
        }

        private void ValidateAddSynonym(AddSynonymDto addSynonymDto)
        {
            if (addSynonymDto.Synonym.ClearWord() == addSynonymDto.Word.ClearWord())
            {
                throw new DomainNotValidException("you can't add the same word as synonym");
            }
            ValidateWord(addSynonymDto.Word);
            ValidateWord(addSynonymDto.Synonym);
        }
        private void ValidateWord(string word)
        {
            if (word.ClearWord().Split(' ').Length > 1)
            {
                throw new DomainNotValidException($"{word} contains more than one word");
            }
        }

    }
}
