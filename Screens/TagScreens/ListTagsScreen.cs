using DataAccess.Models;
using DataAccess.Repositories;

namespace DataAccess.Screens.TagScreens;

public static class ListTagsScreen
{
    public static void Load()
    {
        Console.Clear();
        Console.WriteLine("LISTA DE TAGS");
        Console.WriteLine("----------------");
        List();

        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();

        MenuTagScreen.Load();
    }

    private static void List()
    {
        var repository = new Repository<Tag>(Database.connection);
        var tags = repository.GetAll();

        foreach ( var tag in tags )
        {
            Console.WriteLine($"{tag.Id}- {tag.Name} ({tag.Slug})");
        }
    }
}
