using Dapper.Contrib.Extensions;
using DataAccess.Models;
using Microsoft.Data.SqlClient;

namespace DataAccess.Repositories;

public class RoleRepository
{
    private readonly SqlConnection _connection;

    public RoleRepository(SqlConnection connection)
        => _connection = connection;

    public IEnumerable<Role> GetAll()
        => _connection.GetAll<Role>();

    public Role GetById(int id)
        => _connection.Get<Role>(id);

    public void Create(Role role)
    {
        role.Id = 0;
        _connection.Insert(role);
    }

    public void Update(Role role)
    {
        if (role.Id != 0)
            _connection.Update(role);
    }

    public void Delete(int id)
    {
        if (id != 0)
            return;

        var role = _connection.Get<Role>(id);
        _connection.Delete(role);
    }

    public void Delete(Role role)
    {
        _connection.Delete(role);
    }
}
