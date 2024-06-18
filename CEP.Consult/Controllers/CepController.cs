using CEP.Consult.Controllers.Response;
using CEP.Consult.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace CEP.Consult.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CepController : ControllerBase
    {
        [HttpGet("v1/{CEP}")]
        [ProducesResponseType(typeof(ApiResponse<ResponseData>), 200)]
        public async Task<IActionResult> Get(string CEP)
        {
            var result = await CepConsult.Get(CEP);

            if (result.Status is Status.Success)
                return Ok(result);

            else if (result.Status is Status.NotFound)
                return NotFound(result);

            return BadRequest(result);
        } 
    }
}
