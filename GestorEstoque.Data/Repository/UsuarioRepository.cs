using GestorEstoque.Data.Contract;
using GestorEstoque.Domain.Entity;
using Npgsql;

namespace GestorEstoque.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public async Task<bool> Add(Usuario usuario)
        {
            string sqlScript = File.ReadAllText(Path.Combine( "..", "GestorEstoque.Data", "Script", "Usuario", "AddUsuario.sql"));

            using (NpgsqlConnection connection = new NpgsqlConnection(Utils.StringConnection))
            {
                connection.Open();
                var retorno = 0;
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand(sqlScript, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@NomeCompleto", usuario.NomeCompleto);
                            command.Parameters.AddWithValue("@Senha", usuario.Senha);
                            command.Parameters.AddWithValue("@SenhaSal", usuario.SenhaSal);
                            command.Parameters.AddWithValue("@Email", usuario.Email);
                            command.Parameters.AddWithValue("@Telefone", usuario.Telefone);
                            command.Parameters.AddWithValue("@Ativo", usuario.Ativo);

                            retorno = await command.ExecuteNonQueryAsync();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"Erro - Add - Usuario: {ex.Message}");
                    }
                    finally
                    {
                        connection.Close();
                    }
                    return retorno == 1;
                }
            }
        }

        public async Task<Usuario> Find(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Utils.StringConnection))
            {
                connection.Open();
                try
                {
                    string sqlScript = File.ReadAllText(Path.Combine("..", "GestorEstoque.Data", "Script", "Usuario", "FindUsuario.sql"));
                    using (NpgsqlCommand command = new NpgsqlCommand(sqlScript, connection))
                    {
                        command.Parameters.AddWithValue("@UsuarioId", id);

                        using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                int usuarioId = reader.GetInt32(0);
                                string nomeCompleto = reader.GetString(1);
                                string email = reader.GetString(2);
                                byte[] senha = reader.GetFieldValue<byte[]>(3);
                                byte[] senhaSal = reader.GetFieldValue<byte[]>(4);
                                string telefone = reader.GetFieldValue<string>(5);
                                bool ativo = reader.GetFieldValue<bool>(6);

                                return new Usuario()
                                {
                                    UsuarioId = usuarioId,
                                    NomeCompleto = nomeCompleto,
                                    Email = email,
                                    SenhaSal = senhaSal,
                                    Senha = senha,
                                    Telefone = telefone,
                                    Ativo = ativo
                                };
                            }
                        }
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao executar o comando SQL: {ex.Message}");
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public async Task<Usuario> FindByEmail(string email)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Utils.StringConnection))
            {
                connection.Open();
                try
                {
                    string sqlScript = File.ReadAllText(Path.Combine("..", "GestorEstoque.Data", "Script", "Usuario", "FindUsuarioByEmail.sql"));
                    using (NpgsqlCommand command = new NpgsqlCommand(sqlScript, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);

                        using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                int usuarioId = reader.GetInt32(0);
                                string nomeCompleto = reader.GetString(1);
                                string _email = reader.GetString(2);
                                byte[] senha = reader.GetFieldValue<byte[]>(3);
                                byte[] senhaSal = reader.GetFieldValue<byte[]>(4);
                                string telefone = reader.GetFieldValue<string>(5);
                                bool ativo = reader.GetFieldValue<bool>(6);

                                return new Usuario()
                                {
                                    UsuarioId = usuarioId,
                                    NomeCompleto = nomeCompleto,
                                    Email = _email,
                                    SenhaSal = senhaSal,
                                    Senha = senha,
                                    Telefone = telefone,
                                    Ativo = ativo
                                };
                            }
                        }
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao executar o comando SQL: {ex.Message}");
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
