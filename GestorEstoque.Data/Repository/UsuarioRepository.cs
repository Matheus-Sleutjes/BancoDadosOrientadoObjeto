﻿using GestorEstoque.Data.Contract;
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
        public async Task<bool> Update(Usuario usuario)
        {
            string sqlScript = File.ReadAllText(Path.Combine("..", "GestorEstoque.Data", "Script", "Usuario", "UpdateUsuario.sql"));

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
                            command.Parameters.AddWithValue("@UsuarioId", usuario.UsuarioId);
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
                        Console.WriteLine($"Erro - Update - Usuario: {ex.Message}");
                    }
                    finally
                    {
                        connection.Close();
                    }
                    return retorno == 1;
                }
            }
        }
        public async Task<bool> Remove(int usuarioId)
        {
            string sqlScript = File.ReadAllText(Path.Combine("..", "GestorEstoque.Data", "Script", "Usuario", "RemoveUsuario.sql"));

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
                            command.Parameters.AddWithValue("@UsuarioId", usuarioId);

                            retorno = await command.ExecuteNonQueryAsync();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"Erro - Remove - Usuario: {ex.Message}");
                    }
                    finally
                    {
                        connection.Close();
                    }
                    return retorno == 1;
                }
            }
        }
        public async Task<List<UsuarioDto>> Paginacao(Paginacao paginacao)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Utils.StringConnection))
            {
                List<UsuarioDto> listaRetorno = new();
                connection.Open();
                try
                {
                    string sqlScript = File.ReadAllText(Path.Combine("..", "GestorEstoque.Data", "Script", "Usuario", "PaginacaoUsuario.sql"));
                    using (NpgsqlCommand command = new NpgsqlCommand(sqlScript, connection))
                    {
                        command.Parameters.AddWithValue("@Sort", paginacao.Sort);
                        command.Parameters.AddWithValue("@NomePesquisa", paginacao.Pesquisa ?? string.Empty);
                        command.Parameters.AddWithValue("@TamanhoPagina", paginacao.TamanhoPagina);
                        command.Parameters.AddWithValue("@Pagina", paginacao.Pagina); 

                        using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                listaRetorno.Add(new UsuarioDto
                                {
                                    UsuarioId = reader.GetInt32(0),
                                    NomeCompleto = reader.GetString(1),
                                    Email = reader.GetString(2),
                                    Telefone = reader.GetString(3),
                                    Ativo = reader.GetBoolean(4)
                                });
                            }
                        }
                    }
                    return listaRetorno;
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
