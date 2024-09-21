using DataAccess.Models;
using DataAccess.Repositories;

namespace DataAccess.Screens.TagScreens;

public static class DeleteTagScreen
{
    public static void Load()
    {
        Console.Clear();
        Console.WriteLine("EXCLUIR TAG");
        Console.WriteLine("----------");
        Console.Write("Id: ");
        var id = int.Parse(Console.ReadLine());
        Delete(id);
        Console.ReadKey();
        MenuTagScreen.Load();
    }

    public static void Delete(int tagId)
    {
        try
        {
            var repository = new Repository<Tag>(Database.connection);
            repository.Delete(tagId);
            Console.WriteLine("Tag excluida com sucesso");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Não foi possível excluir a tag");
            Console.WriteLine(ex.Message);
        }
    }
}
