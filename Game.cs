using System;
using System.Media;
using System.Collections.Generic;

namespace DungeonExplorer
{
    internal class Game
    {
        private Player player;
        private Room currentRoom;
        private Dictionary<string, Room> rooms;
        private bool playing;

        public Game()
        {
            Console.Write("Enter your character's name: ");
            string playerName = Console.ReadLine();
            player = new Player(playerName, 100); // Default health set to 100
            InitializeRooms();
            playing = true;
        }

        private void InitializeRooms()
        {
            // Create rooms
            Room dungeon = new Room("A dark, eerie dungeon.", new List<string> { "Torch", "Sword", "Potion" });
            Room corridor = new Room("A narrow, damp corridor. It feels cold.");
            Room treasureRoom = new Room("A bright chamber filled with treasure! But there's a trap.");

            // Set up room connections
            dungeon.AddExit("north", corridor);
            corridor.AddExit("south", dungeon);
            corridor.AddExit("north", treasureRoom);
            treasureRoom.AddExit("south", corridor);

            // Set a trap in treasureRoom
            treasureRoom.SetTrap(10);

            // Set the starting room
            currentRoom = dungeon;

            // Store in dictionary for reference
            rooms = new Dictionary<string, Room>
            {
                { "dungeon", dungeon },
                { "corridor", corridor },
                { "treasureRoom", treasureRoom }
            };
        }

        public void Start()
        {
            Console.WriteLine($"\nWelcome, {player.Name}! You find yourself in a dungeon.");

            while (playing && player.IsAlive())
            {
                Console.WriteLine($"\n{currentRoom.GetDescription()}");
                Console.Write("What would you like to do? (look / take <item> / go <direction> / inventory / use <item> / exit): ");
                string input = Console.ReadLine()?.ToLower();
                HandleInput(input);
            }

            if (!player.IsAlive())
            {
                Console.WriteLine("Game Over. You died.");
            }
        }

        private void HandleInput(string input)
        {
            string[] parts = input.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
            string command = parts[0];
            string argument = parts.Length > 1 ? parts[1] : "";

            switch (command)
            {
                case "look":
                    Console.WriteLine(currentRoom.GetDescription());
                    break;

                case "take":
                    if (currentRoom.TakeItem(argument, player))
                        Console.WriteLine($"You picked up the {argument}.");
                    else
                        Console.WriteLine($"You can't take {argument}.");
                    break;

                case "inventory":
                    Console.WriteLine(player.InventoryContents());
                    break;

                case "use":
                    if (!string.IsNullOrEmpty(argument))
                        player.UseItem(argument);
                    else
                        Console.WriteLine("Use what?");
                    break;

                case "go":
                    if (currentRoom.HasExit(argument))
                    {
                        currentRoom = currentRoom.GetExit(argument);
                        Console.WriteLine($"You moved {argument}.");

                        // Trigger trap if present
                        currentRoom.TriggerTrap(player);
                        if (!player.IsAlive())
                        {
                            playing = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("You can't go that way.");
                    }
                    break;

                case "exit":
                    Console.WriteLine("Thanks for playing Dungeon Explorer!");
                    playing = false;
                    break;

                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }
        }
    }
}
