using Dapper;
using DataAccess.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DapperQueries;

public static class DapperQuery
{
    const string connectionString = "Server=localhost,1433; Database=balta;User Id=sa; Password=1q2w3e4r@#$;TrustServerCertificate=True";

    public static void ListCategories(SqlConnection connection)
    {
        var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");
        foreach (var item in categories)
        {
            Console.WriteLine($"{item.Id} - {item.Title}");
        }
    }

    public static void CreateCategory(SqlConnection connection)
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

    public static void UpdateCategory(SqlConnection connection)
    {
        var updateQuery = @"UPDATE [Category] SET [Title]=@title WHERE [Id]=@id";

        var rows = connection.Execute(updateQuery, new
        {
            id = new Guid("c4e5bdd3-c9a0-4af6-8014-7230b89d6edd"),
            title = "X-Box Series S|X"
        });

        Console.WriteLine("{0} linha(s) afetada(s)", rows);
    }

    public static void CreateManyCategory(SqlConnection connection)
    {
        var category = new Category();
        category.Id = Guid.NewGuid();
        category.Title = "GCP";
        category.Url = "console.googlecloud.com";
        category.Description = "Categoria Nova One";
        category.Order = 8;
        category.Summary = "AWS Cloud";
        category.Featured = false;

        var category2 = new Category();
        category2.Id = Guid.NewGuid();
        category2.Title = "Azure DevOps";
        category2.Url = "dev.azure.com";
        category2.Description = "Categoria Nova";
        category2.Order = 8;
        category2.Summary = "AWS Cloud";
        category2.Featured = false;

        var insertSql = @"INSERT INTO [Category] VALUES(@Id, @Title, @Url, @Summary, @Order, @Description, @Featured)";

        var rows = connection.Execute(insertSql, new[]
        {
            category,
            category2
        });
        Console.WriteLine("{0} linha(s) inserida(s)", rows);
    }

    public static void ExecuteProcedure(SqlConnection connection)
    {
        var procedure = "spDeleteStudent";
        var pars = new { StudentId = "9ab5f0a2-103f-4e10-9c00-d8a5abd99d43" };

        var rows = connection.Execute(procedure, pars, commandType: CommandType.StoredProcedure);

        Console.WriteLine("{0} linhas afetadas", rows);
    }

    public static void ExecuteReadProcedure(SqlConnection connection)
    {
        var sql = "[spGetCoursesByCategory]";
        var pars = new { CategoryId = "09ce0b7b-cfca-497b-92c0-3290ad9d5142" };

        var courses = connection.Query(sql, pars, commandType: CommandType.StoredProcedure);

        Console.WriteLine("Listing {0} rows", courses.Count());
        foreach (var item in courses)
        {
            Console.WriteLine("{0} - {1}", item.Id, item.Title);
        }
    }

    public static void ExecuteScalar(SqlConnection connection)
    {
        var category = new Category();
        category.Title = "PlayStation 5";
        category.Url = "amazon";
        category.Description = "Categoria destinada à serviços do AWS";
        category.Order = 8;
        category.Summary = "AWS Cloud";
        category.Featured = false;

        var insertSql = @"INSERT INTO [Category] OUTPUT inserted.[Id] 
            VALUES(NEWID(), @Title, @Url, @Summary, @Order, @Description, @Featured)";

        var id = connection.ExecuteScalar<Guid>(insertSql, new
        {
            category.Title,
            category.Url,
            category.Description,
            category.Order,
            category.Summary,
            category.Featured,
        });
        Console.WriteLine("Id da categoria: {0}", id);
    }

    public static void ReadView(SqlConnection connection)
    {
        var sql = "SELECT * FROM [vwCourses]";
        var courses = connection.Query(sql);

        foreach (var course in courses)
        {
            Console.WriteLine("{0} -> {1}", course.Id, course.Title);
        }
    }

    public static void OneToOne(SqlConnection connection)
    {
        var sql = "SELECT * FROM [CareerItem] INNER JOIN [Course] ON [CareerItem].[CourseId] = [Course].[Id]";

        var items = connection.Query<CareerItem, Course, CareerItem>(sql, (careerItem, course) =>
        {
            careerItem.Course = course;
            return careerItem;
        }, splitOn: "Id");

        foreach (var item in items)
        {
            Console.WriteLine($"{item.Title} - Curso: {item.Course.Title}");
        }
    }

    public static void OneToMany(SqlConnection connection)
    {
        var sql = @"SELECT Career.Id, Career.Title, CareerItem.CareerId, CareerItem.Title FROM [CAREER]
            INNER JOIN [CAREERITEM] ON [CAREERITEM].[CAREERID] = [CAREER].[ID]
            ORDER BY [CAREER].[TITLE];";

        var careers = new List<Career>();
        var items = connection.Query<Career, CareerItem, Career>(sql, (career, item) =>
        {
            var car = careers.Where(x => x.Id == career.Id).FirstOrDefault();
            if (car == null)
            {
                car = career;
                car.Items.Add(item);
                careers.Add(car);
            }
            else
            {
                car.Items.Add(item);
            }

            return career;
        }, splitOn: "CareerId");

        foreach (var career in careers)
        {
            Console.WriteLine($"{career.Title} (with {career.Items.Count} items)");
            foreach (var item in career.Items)
            {
                Console.WriteLine("- " + item.Title);
            }
        }
    }

    public static void QueryMultiple(SqlConnection connection)
    {
        var sql = "SELECT * FROM [Category]; SELECT * FROM [Course]";

        using (var multi = connection.QueryMultiple(sql))
        {
            var categories = multi.Read<Category>();
            var courses = multi.Read<Course>();

            foreach (var item in categories)
            {
                Console.WriteLine(item.Title);
            }

            foreach (var item in courses)
            {
                Console.WriteLine(item.Title);
            }
        }
    }

    public static void SelectIn(SqlConnection connection)
    {
        var query = @"select * from Career where Id in @Id";

        var careers = connection.Query<Career>(query, new
        {
            Id = new[]
            {
                "E6730D1C-6870-4DF3-AE68-438624E04C72",
                "4327AC7E-963B-4893-9F31-9A3B28A4E72B"
            }
        });

        foreach (var career in careers)
        {
            Console.WriteLine(career.Title);
        }
    }

    public static void Like(SqlConnection connection)
    {
        var term = "api";
        var query = "SELECT * FROM Course WHERE Title like @exp";

        var courses = connection.Query<Course>(query, new
        {
            exp = $"%{term}%"
        });

        foreach (var course in courses)
        {
            Console.WriteLine(course.Title);
        }
    }

    public static void Transaction(SqlConnection connection)
    {
        var category = new Category();
        category.Id = Guid.NewGuid();
        category.Title = "C# .NET";
        category.Url = "amazon";
        category.Description = "Categoria destinada à serviços do AWS";
        category.Order = 8;
        category.Summary = "AWS Cloud";
        category.Featured = false;

        var insertSql = @"INSERT INTO [Category] VALUES(@Id, @Title, @Url, @Summary, @Order, @Description, @Featured)";

        connection.Open();
        using (var transaction = connection.BeginTransaction())
        {
            var rows = connection.Execute(insertSql, category, transaction);

            transaction.Commit();
            //transaction.Rollback();
            Console.WriteLine("{0} linha(s) inserida(s)", rows);
        }
    }
}
