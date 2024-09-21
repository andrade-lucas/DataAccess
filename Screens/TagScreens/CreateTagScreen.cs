using DataAccess.Models;
using DataAccess.Repositories;

namespace DataAccess.Screens.TagScreens;

public static class CreateTagScreen
{
    public static void Load()
    {
        Console.Clear();
        Console.WriteLine("NOVA TAG");
        Console.WriteLine("----------");
        Console.Write("Nome: ");
        var name = Console.ReadLine();
        Console.Write("Slug: ");
        var slug = Console.ReadLine();
        Create(new Tag
        {
            Name = name,
            Slug = slug
        });
        Console.ReadKey();
        MenuTagScreen.Load();
    }

    public static void Create(Tag tag)
    {
        try
        {
            var repository = new Repository<Tag>(Database.connection);
            repository.Create(tag);
            Console.WriteLine("Tag cadastrada com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Não foi possível cadastrar a tag");
            Console.WriteLine(ex.Message);
        }
    }
}
