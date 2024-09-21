namespace DataAccess.Screens.TagScreens;

public static class MenuTagScreen
{
    public static void Load()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("Gestão de Tags");
            Console.WriteLine("-----------------");
            Console.WriteLine("O que deseja fazer?");
            Console.WriteLine("");
            Console.WriteLine("1- Listar tags");
            Console.WriteLine("2- Cadastrar tags");
            Console.WriteLine("3- Atualizar tags");
            Console.WriteLine("4- Deletar tags");
            Console.WriteLine("5- Menu inicial");
            Console.WriteLine();
            Console.WriteLine();
            var options = short.Parse(Console.ReadLine());

            switch (options)
            {
                case 1:
                    ListTagsScreen.Load();
                    break;
                case 2:
                    CreateTagScreen.Load();
                    break;
                case 3:
                    UpdateTagScreen.Load();
                    break;
                case 4:
                    DeleteTagScreen.Load();
                    break;
                case 5:
                    Program.Load();
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
