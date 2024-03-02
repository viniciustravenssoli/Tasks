using Dapper;
using MySqlConnector;

namespace Tasks.Infra.Migrations;
public static class DataBase
{
    public static void CreateDataBase(string connectionString, string databaseName)
    {
        using var myconnection = new MySqlConnection(connectionString);

        var parameters = new DynamicParameters();
        parameters.Add("nome", databaseName);

        var registers = myconnection.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @nome", parameters);

        if (!registers.Any())
        {
            myconnection.Execute($"CREATE DATABASE {databaseName}");
        }
    }
}
