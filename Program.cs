using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.Data.SqlClient;

const string connectionString = "Server=localhost,1433; Database=Blog;User Id=sa; Password=1q2w3e4r@#$;TrustServerCertificate=True";

var connection = new SqlConnection(connectionString);
connection.Open();

ReadUser(connection);
//CreateUser(connection);
//ReadRoles(connection);
//ReadTags(connection);

connection.Close();

static void ReadUser(SqlConnection connection)
{
    var repository = new UserRepository(connection);
    var items = repository.GetWithRoles();

    foreach (var item in items)
    {
        Console.WriteLine($"{item.Name} ({item.Email})");
        foreach (var role in item.Roles)
        {
            Console.WriteLine($"- {role.Name}");
        }
    }
}

static void CreateUser(SqlConnection connection)
{
    var user = new User
    {
        Name = "Teste",
        Email = "teste@teste.com",
        Bio = "aaaaa aaaa aa aa a",
        Image = "https://",
        PasswordHash = "HASH",
        Slug = "teste-01"
    };
    //var role = new Role
    //{
    //    Name = "Teste",
    //    Slug = "teste-01"
    //};
    //user.Roles.Add(role);

    var repository = new Repository<User>(connection);
    repository.Create(user);
}

static void ReadRoles(SqlConnection connection)
{
    var repository = new Repository<Role>(connection);
    var items = repository.GetAll();

    foreach (var item in items)
        Console.WriteLine("Role: " + item.Name);
}

static void ReadTags(SqlConnection connection)
{
    var repository = new Repository<Tag>(connection);
    var items = repository.GetAll();

    foreach (var item in items)
        Console.WriteLine("Tag: " + item.Name);
}
