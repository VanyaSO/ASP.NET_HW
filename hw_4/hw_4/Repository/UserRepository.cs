using System.Data.SqlClient;
using hw_4.Interfaces;
using hw_4.Model;

namespace hw_4.Repository;

public class UserRepository : IUser
{
    private string _connectionString;
    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        List<User> users = new List<User>();
        
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand("select Id, Name, Age from Users", connection);
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        users.Add(new User(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
                    }
                }
            }
        }

        return users;
    }

    public async Task DeleteUserById(int id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand($"delete from Users where Id = {id}", connection);

            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task UpdateUser(User user)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            
            SqlCommand command;
            if (user.Id == 0)
            {
                command = new SqlCommand($"insert into Users ([Name], [Age]) values ('{user.Name}', {user.Age})", connection);
            } 
            else
            {
                command = new SqlCommand($"UPDATE Users SET [Name] = '{user.Name}', [Age] = '{user.Age}' WHERE Id = '{user.Id}'", connection);
            }
            
            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task<IEnumerable<User>> SearchUsers(string option, string value)
    {
        List<User> users = new List<User>();
        
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand($"select Id, Name, Age from Users WHERE {option} = '{value}'", connection);
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        users.Add(new User(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
                    }
                }
            }
        }
        
        return users;
    }
}