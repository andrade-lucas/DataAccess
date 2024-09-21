using Dapper.Contrib.Extensions;
using DataAccess.Models;
using Microsoft.Data.SqlClient;

namespace DataAccess.Repositories;

public class UserRepository
{
    private readonly SqlConnection _connection;

    public UserRepository(SqlConnection connection)
        => _connection = connection;

    public IEnumerable<User> Get() => _connection.GetAll<User>();

    public User GetById(int userId) => _connection.Get<User>(userId);

    public void Create(User user) => _connection.Insert<User>(user);

    public void Update(User user) => _connection.Update<User>(user);

    public void Delete(int userId)
    {
        var user = _connection.Get<User>(4);
        _connection.Delete<User>(user);
    }
}
