using cep_api.Model;
using cep_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace cep_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CepController : ControllerBase
    {
        private readonly CepServices _cepServices;

        public CepController(CepServices cepServices)
        {
            _cepServices = cepServices;
        }

        [HttpGet("{cep}")]
        public async Task<ActionResult<CepModel>> GetCepInfo(string cep)
        {
            var cepInfo = await _cepServices.GetCepModelAsync(cep);

            if(cepInfo == null)
            {
                return NotFound(new {Message = "CEP não encontrado"});
            }

            return Ok(cepInfo);
        }

    }
}
