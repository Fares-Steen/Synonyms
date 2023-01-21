using Application.Services.SynonymServices;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace SynonymsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SynonymController : ControllerBase
    {
        private readonly ICreateSynonymService _createSynonymService;
        private readonly IReadSynonymService _readSynonymService;
        private readonly IDeleteSynonymService _deleteSynonymService;
        public SynonymController(ICreateSynonymService createSynonymService, IReadSynonymService readSynonymService, IDeleteSynonymService deleteSynonymService)
        {
            _createSynonymService = createSynonymService;
            _readSynonymService = readSynonymService;
            _deleteSynonymService = deleteSynonymService;
        }

        [HttpPost("AddSynonym")]
        public IActionResult AddSynonym(AddSynonymDto addSynonymDto)
        {
            _createSynonymService.AddSynonym(addSynonymDto);
            return NoContent();
        }

        [HttpGet("GetSynonyms")]
        public IActionResult GetSynonyms(string word)
        {
            IEnumerable<SynonymsDto> synonyms = _readSynonymService.GetSynonyms(word);
            return Ok(synonyms);
        }

        [HttpDelete("RemoveAll")]
        public IActionResult RemoveAll()
        {
            _deleteSynonymService.RemoveAll();
            return NoContent();
        }
    }
}
