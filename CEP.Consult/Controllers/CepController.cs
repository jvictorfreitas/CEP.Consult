using CEP.Consult.Controllers.Response;
using CEP.Consult.Services;
using Microsoft.AspNetCore.Mvc;

namespace CEP.Consult.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CepController : ControllerBase
    {
        [HttpGet("v1/{CEP}")]
        [ProducesResponseType(typeof(ApiResponse<ResponseData>), 200)]
        public async Task<IActionResult> Get(string CEP) => Ok(await CepConsult.Get(CEP));
    }
}
