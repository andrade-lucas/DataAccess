using Dapper;
using DataAccess.Models;
using Microsoft.Data.SqlClient;

internal class Program
{
    private static void Main(string[] args)
    {
        const string connectionString = "Server=localhost,1433; Database=balta;User Id=sa; Password=1q2w3e4r@#$;TrustServerCertificate=True";

        using (var connection = new SqlConnection(connectionString))
        {
            // CreateCategory(connection);
            UpdateCategory(connection);
            ListCategories(connection);
        }
    }

    private static void ListCategories(SqlConnection connection)
    {
        var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");
        foreach (var item in categories)
        {
            Console.WriteLine($"{item.Id} - {item.Title}");
        }
    }

    private static void CreateCategory(SqlConnection connection)
    {
        var category = new Category();
        category.Id = Guid.NewGuid();
        category.Title = "PlayStation 5";
        category.Url = "amazon";
        category.Description = "Categoria destinada à serviços do AWS";
        category.Order = 8;
        category.Summary = "AWS Cloud";
        category.Featured = false;

        var insertSql = @"INSERT INTO [Category] VALUES(@Id, @Title, @Url, @Summary, @Order, @Description, @Featured)";

        var rows = connection.Execute(insertSql, category);
        Console.WriteLine("{0} linha(s) inserida(s)", rows);
    }

    private static void UpdateCategory(SqlConnection connection)
    {
        var updateQuery = @"UPDATE [Category] SET [Title]=@title WHERE [Id]=@id";

        var rows = connection.Execute(updateQuery, new
        {
            id = new Guid("c4e5bdd3-c9a0-4af6-8014-7230b89d6edd"),
            title = "X-Box Series S|X"
        });

        Console.WriteLine("{0} linha(s) afetada(s)", rows);
    }
}