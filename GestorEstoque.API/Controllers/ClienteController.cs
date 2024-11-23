using GestorEstoque.Application.Contract;
using GestorEstoque.Domain.Dto;
using GestorEstoque.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace GestorEstoque.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController(IClienteService clienteService) : ControllerBase
    {
        private readonly IClienteService _clienteService = clienteService;

        //post, find, put, delete, paginação

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ClienteDto dto)
        {
            return Ok();
        }

        [HttpGet("{clienteId}")]
        public async Task<IActionResult> Find(int clienteId)
        {
            return Ok();
        }

        [HttpDelete("{clienteId}")]
        public async Task<IActionResult> Remove(int clienteId)
        {
            return Ok();
        }

        [HttpPut("{clienteId}")]
        public async Task<IActionResult> Update(int clienteId, [FromBody] ClienteDto dto)
        {
            return Ok();
        }

        [HttpPost("Paginacao")]
        public async Task<IActionResult> Paginacao([FromBody] Paginacao paginacao)
        {
            return Ok();
        }
    }
}
