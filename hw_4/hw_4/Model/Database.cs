using System.Data.SqlClient;

namespace hw_4.Model;

public class Database
{
    private string _connectionString;
    public Database(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public async Task Init()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            
            string data = @"INSERT INTO Users (Name, Age) VALUES ('John Doe', 30),('Jane Doe', 25),('Alice Smith', 35);";

            SqlCommand command = new SqlCommand(data, connection);
            await command.ExecuteNonQueryAsync();
        }
    }
}