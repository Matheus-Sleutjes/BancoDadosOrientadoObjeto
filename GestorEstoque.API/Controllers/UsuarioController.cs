using GestorEstoque.Application.Contract;
using GestorEstoque.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GestorEstoque.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController(IUsuarioService usuarioService) : ControllerBase
    {
        private readonly IUsuarioService _usuarioService = usuarioService;
        
        //Post, Listagem, Update Usuario, alteração senha, delete usuario

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
            return Ok(retorno);
        }
    }
}
