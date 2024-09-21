using DataAccess.Models;
using DataAccess.Repositories;

namespace DataAccess.Screens.TagScreens;

public static class UpdateTagScreen
{
    public static void Load()
    {
        Console.Clear();
        Console.WriteLine("ATUALIZANDO TAG");
        Console.WriteLine("----------");
        Console.Write("Id: ");
        var id = int.Parse(Console.ReadLine());
        Console.Write("Nome: ");
        var name = Console.ReadLine();
        Console.Write("Slug: ");
        var slug = Console.ReadLine();
        Update(new Tag
        {
            Id = id,
            Name = name,
            Slug = slug
        });
        Console.ReadKey();
        MenuTagScreen.Load();
    }

    public static void Update(Tag tag)
    {
        try
        {
            var repository = new Repository<Tag>(Database.connection);
            repository.Update(tag);
            Console.WriteLine("Tag atualizada com sucesso");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Não foi possível atualizar a tag");
            Console.WriteLine(ex.Message);
        }
    }
}
