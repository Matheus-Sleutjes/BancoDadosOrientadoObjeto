using GestorEstoque.Data.Contract;
using GestorEstoque.Domain.Entity;
using Npgsql;

namespace GestorEstoque.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public void Add(Usuario usuario)
        {
            string sqlScript = File.ReadAllText(Path.Combine("..", "Script", "AddUsuario.sql"));

            using (NpgsqlConnection connection = new NpgsqlConnection(Utils.StringConnection))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand(sqlScript, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@NomeCompleto", usuario.NomeCompleto);
                            command.Parameters.AddWithValue("@DataNascimento", usuario.DataNascimento);
                            command.Parameters.AddWithValue("@Senha", usuario.Senha);
                            command.Parameters.AddWithValue("@SenhaSal", usuario.SenhaSal);
                            command.Parameters.AddWithValue("@Email", usuario.Email);
                            command.Parameters.AddWithValue("@Telefone", usuario.Telefone);
                            command.Parameters.AddWithValue("@Ativo", usuario.Ativo);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"Erro -Repository - Add - Usuario: {ex.Message}");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public Usuario Find(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Utils.StringConnection))
            {
                connection.Open();
                try
                {
                    string sqlScript = File.ReadAllText(Path.Combine("..", "Script", "FindUsuario.sql"));
                    using (NpgsqlCommand command = new NpgsqlCommand(sqlScript, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int usuarioId = reader.GetInt32(0);
                                string nomeCompleto = reader.GetString(1);
                                DateTime dataNascimento = reader.GetDateTime(2);
                                string email = reader.GetString(3);
                                byte[] senha = reader.GetFieldValue<byte[]>(4);
                                byte[] senhaSal = reader.GetFieldValue<byte[]>(5);

                                return new Usuario()
                                {
                                    Id = usuarioId,
                                    NomeCompleto = nomeCompleto,
                                    DataNascimento = dataNascimento,
                                    Email = email,
                                    SenhaSal = senhaSal,
                                    Senha = senha
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
        public Usuario GetByEmail(string email)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Utils.StringConnection))
            {
                connection.Open();
                try
                {
                    string sqlScript = File.ReadAllText(Path.Combine("..", "Script", "GetUsuarioByEmail.sql"));
                    using (NpgsqlCommand command = new NpgsqlCommand(sqlScript, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int usuarioId = reader.GetInt32(0);
                                string nomeCompleto = reader.GetString(1);
                                DateTime dataNascimento = reader.GetDateTime(2);
                                string _email = reader.GetString(3);
                                byte[] senha = reader.GetFieldValue<byte[]>(4);
                                byte[] senhaSal = reader.GetFieldValue<byte[]>(5);

                                return new Usuario()
                                {
                                    Id = usuarioId,
                                    NomeCompleto = nomeCompleto,
                                    DataNascimento = dataNascimento,
                                    Email = _email,
                                    SenhaSal = senhaSal,
                                    Senha = senha
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
