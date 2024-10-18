using Microsoft.AspNetCore.Mvc;

namespace GestorEstoque.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet("{idade}")]
        public IActionResult Get(int idade)
        {
            if(idade >= 18)
            {
                return Ok("hello world!");

            }
            else
            {
                return BadRequest();
            }
        }
    }
}
