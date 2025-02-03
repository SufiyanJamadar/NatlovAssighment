using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NatlovAssighment.Services;

namespace NatlovAssighment.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyController : ControllerBase
    {
        private readonly ThirdPartyService _service;
        public MyController(ThirdPartyService service)
        {
            _service = service;
        }

        [HttpGet("thirdpartydata")]
        public async Task<IActionResult> GetData()
        {
            var data = await _service.GetDataAsync();
            return Ok(data);
        }
    }
}
