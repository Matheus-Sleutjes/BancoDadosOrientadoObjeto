using GestorEstoque.Data.Contract;
using GestorEstoque.Domain.Dto;
using GestorEstoque.Domain.Entity;
using Npgsql;
using System.Data;

namespace GestorEstoque.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public async Task<bool> Add(Usuario usuario)
        {
            var path = Path.Combine("..", "GestorEstoque.Data", "Script", "Usuario", "AddUsuario.sql");

            var param = new NpgsqlParameter("@NomeCompleto", usuario.NomeCompleto);
            var param1 = new NpgsqlParameter("@Senha", usuario.Senha);
            var param2 = new NpgsqlParameter("@SenhaSal", usuario.SenhaSal);
            var param3 = new NpgsqlParameter("@Email", usuario.Email);
            var param4 = new NpgsqlParameter("@Telefone", usuario.Telefone);
            var param5 = new NpgsqlParameter("@Ativo", usuario.Ativo);

            var listParams = new List<NpgsqlParameter>() { param , param1, param2, param3, param4, param5 };

            var response = await Proxy.AnyQueryAsync(path, listParams);

            return response;
        }
        public async Task<Usuario> Find(int id)
        {
            var path = Path.Combine("..", "GestorEstoque.Data", "Script", "Usuario", "FindUsuario.sql");
            var param = new NpgsqlParameter("@UsuarioId", id);
            var listParams = new List<NpgsqlParameter>() { param };

            var response = await Proxy.ReaderAsync(path, listParams);

            if (response.Tables.Count > 0 && response.Tables[0].Rows.Count > 0)
            {
                var row = response.Tables[0].Rows[0];

                return new Usuario()
                {
                    UsuarioId = row.Field<int>("UsuarioId"),
                    NomeCompleto = row.Field<string>("NomeCompleto"),
                    Email = row.Field<string>("Email"),
                    SenhaSal = row.Field<byte[]>("SenhaSal"),
                    Senha = row.Field<byte[]>("Senha"),
                    Telefone = row.Field<string>("Telefone"),
                    Ativo = row.Field<bool>("Ativo")
                };
            }

            return null;
        }
        public async Task<Usuario> FindByEmail(string email)
        {
            var path = Path.Combine("..", "GestorEstoque.Data", "Script", "Usuario", "FindUsuarioByEmail.sql");
            var param = new NpgsqlParameter("@Email", email);
            var listParams = new List<NpgsqlParameter>() { param };

            var response = await Proxy.ReaderAsync(path, listParams);

            if (response.Tables.Count > 0 && response.Tables[0].Rows.Count > 0)
            {
                var row = response.Tables[0].Rows[0];

                return new Usuario()
                {
                    UsuarioId = row.Field<int>("UsuarioId"),
                    NomeCompleto = row.Field<string>("NomeCompleto"),
                    Email = row.Field<string>("Email"),
                    SenhaSal = row.Field<byte[]>("SenhaSal"),
                    Senha = row.Field<byte[]>("Senha"),
                    Telefone = row.Field<string>("Telefone"),
                    Ativo = row.Field<bool>("Ativo")
                };
            }

            return null;
        }
        public async Task<bool> Update(Usuario usuario)
        {
            var path = Path.Combine("..", "GestorEstoque.Data", "Script", "Usuario", "UpdateUsuario.sql");

            var param = new NpgsqlParameter("@NomeCompleto", usuario.NomeCompleto);
            var param0 = new NpgsqlParameter("@UsuarioId", usuario.UsuarioId);
            var param1 = new NpgsqlParameter("@Senha", usuario.Senha);
            var param2 = new NpgsqlParameter("@SenhaSal", usuario.SenhaSal);
            var param3 = new NpgsqlParameter("@Email", usuario.Email);
            var param4 = new NpgsqlParameter("@Telefone", usuario.Telefone);
            var param5 = new NpgsqlParameter("@Ativo", usuario.Ativo);

            var listParams = new List<NpgsqlParameter>() { param, param0, param1, param2, param3, param4, param5 };

            var response = await Proxy.AnyQueryAsync(path, listParams);

            return response;
        }
        public async Task<bool> Remove(int usuarioId)
        {
            var path = Path.Combine("..", "GestorEstoque.Data", "Script", "Usuario", "RemoveUsuario.sql");

            var param = new NpgsqlParameter("@UsuarioId", usuarioId);
            
            var listParams = new List<NpgsqlParameter>() { param };

            var response = await Proxy.AnyQueryAsync(path, listParams);

            return response;
        }
        public async Task<List<UsuarioDto>> Paginacao(Paginacao paginacao)
        {
            var path = Path.Combine("..", "GestorEstoque.Data", "Script", "Usuario", "PaginacaoUsuario.sql");

            var param = new NpgsqlParameter("@Sort", paginacao.Sort);
            var param1 = new NpgsqlParameter("@NomePesquisa", paginacao.Pesquisa ?? string.Empty);
            var param2 = new NpgsqlParameter("@TamanhoPagina", paginacao.TamanhoPagina);
            var param3 = new NpgsqlParameter("@Pagina", paginacao.Pagina);

            var listParams = new List<NpgsqlParameter>() { param, param1, param2, param3 };

            var response = await Proxy.ReaderAsync(path, listParams);

            if (response.Tables.Count > 0 && response.Tables[0].Rows.Count > 0)
            {
                var listaResposta = new List<UsuarioDto>();
                foreach (DataRow? row in response.Tables[0].Rows)
                {
                    var item = new UsuarioDto()
                    {
                        UsuarioId = row.Field<int>("UsuarioId"),
                        NomeCompleto = row.Field<string>("NomeCompleto"),
                        Email = row.Field<string>("Email"),
                        Telefone = row.Field<string>("Telefone"),
                        Ativo = row.Field<bool>("Ativo")
                    };
                    listaResposta.Add(item);
                }

                return listaResposta;
            }

            return null;
        }
    }
}
