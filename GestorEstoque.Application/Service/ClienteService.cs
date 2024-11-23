using GestorEstoque.Application.Contract;
using GestorEstoque.Data.Contract;
using GestorEstoque.Domain.Dto;
using GestorEstoque.Domain.Entity;
using System.Security.Cryptography;
using System.Text;

namespace GestorEstoque.Application.Service
{
    public class ClienteService(IClienteRepository clienteRepository) : IClienteService
    {
        private readonly IClienteRepository _clienteRepository = clienteRepository;

        
    }
}
