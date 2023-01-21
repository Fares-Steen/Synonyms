using Application.IRepositories;

namespace Application.Services.SynonymServices
{
    public class DeleteSynonymService : IDeleteSynonymService
    {
        private readonly ISynonymsRepository _synonymsRepository;

        public DeleteSynonymService(ISynonymsRepository synonymsRepository)
        {
            _synonymsRepository = synonymsRepository;
        }

        public void RemoveAll()
        {
            _synonymsRepository.RemoveAll();
        }
    }
}
