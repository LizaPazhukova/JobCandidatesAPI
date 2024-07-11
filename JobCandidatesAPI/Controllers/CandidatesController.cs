using Logic.Dtos;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobCandidatesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(CandidateDto candidate)
        {

            await _candidateService.AddOrUpdateCandidate(candidate);

            return Ok();
        }

    }
}
