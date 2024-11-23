using GestorEstoque.Application.Contract;
using GestorEstoque.Application.Service;
using GestorEstoque.Data.Contract;
using GestorEstoque.Data.Repository;

namespace GestorEstoque.API.Configuration
{
    public static class DependecyConfig
    {
        public static void RegisterConfig(this IServiceCollection service) 
        {
            service.AddScoped<IUsuarioRepository, UsuarioRepository>();
            service.AddScoped<IUsuarioService, UsuarioService>();
            service.AddScoped<IClienteService, ClienteService>();
            service.AddScoped<IClienteRepository, ClienteRepository>();
        }
    }
}
