
using DataAccess;
using DataAccess.Screens.TagScreens;
using Microsoft.Data.SqlClient;

internal class Program
{
    private static void Main(string[] args)
    {
        const string connectionString = "Server=localhost,1433; Database=Blog;User Id=sa; Password=1q2w3e4r@#$;TrustServerCertificate=True";

        Database.connection = new SqlConnection(connectionString);
        Database.connection.Open();

        Load();

        Database.connection.Close();

        Console.ReadKey();
    }

    public static void Load()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("Meu Blog");
            Console.WriteLine("-----------------");
            Console.WriteLine("O que deseja fazer?");
            Console.WriteLine("");
            Console.WriteLine("1- Gestão de usuário");
            Console.WriteLine("2- Gestão de perfil");
            Console.WriteLine("3- Gestão de categoria");
            Console.WriteLine("4- Gestão de tag");
            Console.WriteLine("5- Vincular perfil/usuário");
            Console.WriteLine("6- Vincular post/tag");
            Console.WriteLine("7- Relatório");
            Console.WriteLine("0- Sair");
            Console.WriteLine();
            Console.WriteLine();
            var options = short.Parse(Console.ReadLine()!);

            switch (options)
            {
                case 4:
                    MenuTagScreen.Load();
                    break;
                case 0:
                    Console.WriteLine("Finalizando aplicação...");
                    Environment.Exit(0);
                    break;
                default:
                    Load();
                    break;
            }
        }
        catch
        {
            Load();
        }
    }
}