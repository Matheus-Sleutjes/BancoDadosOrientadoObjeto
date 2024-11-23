using GestorEstoque.Application.Contract;
using GestorEstoque.Domain.Dto;
using GestorEstoque.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace GestorEstoque.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController(IUsuarioService usuarioService) : ControllerBase
    {
        private readonly IUsuarioService _usuarioService = usuarioService;
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UsuarioDto dto)
        {
            var retorno = await _usuarioService.Add(dto);
            return Ok(retorno);
        }

        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> Find(int usuarioId)
        {
            var retorno = await _usuarioService.Find(usuarioId);
            if (retorno == null) return NotFound();

            return Ok(retorno);
        }

        [HttpPut("{usuarioId}")]
        public async Task<IActionResult> Update(int usuarioId, [FromBody]UsuarioDto dto)
        {
            var retorno = await _usuarioService.Update(usuarioId, dto);

            return Ok(retorno);
        }

        [HttpPut("Senha/{usuarioId}")]
        public async Task<IActionResult> UpdateSenha(int usuarioId, [FromBody] string novaSenha)
        {
            var retorno = await _usuarioService.UpdateSenha(usuarioId, novaSenha);

            return Ok(retorno);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UsuarioDto dto)
        {
            var retorno = await _usuarioService.Login(dto);

            return Ok(retorno);
        }

        [HttpDelete("{usuarioId}")]
        public async Task<IActionResult> Remove(int usuarioId)
        {
            var retorno = await _usuarioService.Remove(usuarioId);

            return Ok(retorno);
        }

        [HttpPost("Paginacao")]
        public async Task<IActionResult> Paginacao([FromBody] Paginacao paginacao)
        {
            var retorno = await _usuarioService.Paginacao(paginacao);

            return Ok(retorno);
        }
    }
}
