using Npgsql;
using System.Data;

namespace GestorEstoque.Data
{
    public static class Proxy
    {
        public async static Task<DataSet?> ReaderAsync(string path, List<NpgsqlParameter> listaParameter)
        {
            var dataSet = new DataSet();

            using (NpgsqlConnection connection = new NpgsqlConnection(Utils.StringConnection))
            {
                connection.Open();
                try
                {
                    string sqlScript = File.ReadAllText(path);
                    using (var command = new NpgsqlCommand(sqlScript, connection))
                    {
                        foreach (var parameter in listaParameter)
                        {
                            command.Parameters.Add(parameter);
                        }

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var dataTable = new DataTable();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                dataTable.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                            }

                            while (reader.Read())
                            {
                                var row = dataTable.NewRow();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row[i] = reader[i];
                                }
                                dataTable.Rows.Add(row);
                            }

                            dataSet.Tables.Add(dataTable);
                        }
                    }
                    return dataSet;
                }
                catch (Exception ex)
                {
                    // Log the error using a logging framework
                    Console.WriteLine($"Erro ao executar o comando SQL: {ex.Message}");
                    return null;
                }
            }
        }
    }
}
