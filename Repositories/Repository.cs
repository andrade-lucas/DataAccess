using Dapper.Contrib.Extensions;
using DataAccess.Models;
using Microsoft.Data.SqlClient;

namespace DataAccess.Repositories;

public class Repository<T> where T : class
{
    private readonly SqlConnection _connection;

    public Repository(SqlConnection connection)
        => _connection = connection;

    public IEnumerable<T> GetAll()
        => _connection.GetAll<T>();

    public T GetById(int modelId) 
        => _connection.Get<T>(modelId);

    public void Create(T model)
        => _connection.Insert<T>(model);

    public void Update(T model)
        => _connection.Update<T>(model);

    public void Delete(int modelId)
    {
        var model = _connection.Get<T>(modelId);
        _connection.Delete<T>(model);
    }

    public void Delete(T model)
        => _connection.Delete<T>(model);
}
