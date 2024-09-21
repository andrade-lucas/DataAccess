using Dapper;
using DataAccess.Models;
using Microsoft.Data.SqlClient;

namespace DataAccess.Repositories;

public class UserRepository : Repository<User>
{
    private readonly SqlConnection _connection;
    public UserRepository(SqlConnection connection) : base(connection)
    {
        _connection = connection;
    }

    public List<User> GetWithRoles()
    {
        var query = @"select [User].*, [Role].* from [User]
            left join [UserRole] on [UserRole].[UserId] = [User].[Id]
            left join [Role] on [UserRole].[RoleId] = [Role].[Id]";

        var users = new List<User>();

        var items = _connection.Query<User, Role, User>(query, (user, role) =>
        {
            var usr = users.FirstOrDefault(x => x.Id == user.Id);
            if (usr == null)
            {
                usr = user;
                usr.AddRole(role);
                users.Add(usr);
            }
            else
            {
                usr.AddRole(role);
            }

            return user;
        }, splitOn: "Id");

        return users;
    }
}
