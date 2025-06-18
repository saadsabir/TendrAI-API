using Microsoft.AspNetCore.Mvc;
using TendrAI.Application.Ports.In;

namespace TendrAI.WebApi.Controllers
{
    [ApiController]
    [Route("api/appelOffre")]
    public class AppelOffreController : ControllerBase
    {
        private readonly IAnalyserAppelOffre _useCase;

        public AppelOffreController(IAnalyserAppelOffre useCase)
        {
            _useCase = useCase;
        }

        [HttpPost("analyse")]
        public async Task<IActionResult> AnalyserAppelOffre(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var result = await _useCase.ExecuteAsync(stream);
            return Ok(result);
        }
    }
}
