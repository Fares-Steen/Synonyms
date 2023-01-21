using Models;

namespace Application.Services.SynonymServices
{
    public interface ICreateSynonymService
    {
        void AddSynonym(AddSynonymDto addSynonymDto);
    }
}
