using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.Data.SqlClient;

const string connectionString = "Server=localhost,1433; Database=Blog;User Id=sa; Password=1q2w3e4r@#$;TrustServerCertificate=True";

using var connection = new SqlConnection(connectionString);
var repository = new UserRepository(connection);
var users = repository.Get();

foreach (var user in users)
    Console.WriteLine(user.Name);


//var user = new User
//{
//    Name = "Batman",
//    Email = "batman@wayne.com",
//    PasswordHash = "HASH",
//    Bio = "Alguma biografia aqui",
//    Image = "https://",
//    Slug = "batman"
//};
