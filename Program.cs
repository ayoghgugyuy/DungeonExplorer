using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Game game = new Game();
                game.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\nThanks for playing Dungeon Explorer!");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
