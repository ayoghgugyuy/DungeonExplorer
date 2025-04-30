using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    internal class Game
    {
        private Player player;
        private Room currentRoom;
        private Dictionary<string, Room> rooms;
        private bool playing;

        // Game constructor - prompts user for name and initializes rooms
        public Game()
        {
            Console.Write("Enter your character's name: ");
            string playerName = Console.ReadLine();
            player = new Player(playerName, 100); // Default health set to 100
            InitializeRooms();
            playing = true;
        }

        // Sets up all rooms, their connections, items, and traps
        private void InitializeRooms()
        {
            // Create item objects
            Item torch = new Key(); // Placeholder if you want to define a Torch class later
            Item sword = new Sword();
            Item potion = new HealingPotion();

            // Create rooms with item objects
            Room dungeon = new Room("Dungeon", "A dark, eerie dungeon.", new List<Item> { torch, sword, potion });
            Room corridor = new Room("Corridor", "A narrow, damp corridor. It feels cold.");
            Room treasureRoom = new Room("Treasure Room", "A bright chamber filled with treasure! But there's a trap.");

            // Set a trap
            treasureRoom.SetTrap(10);

            // Add exits
            dungeon.AddExit("north", corridor);
            corridor.AddExit("south", dungeon);
            corridor.AddExit("north", treasureRoom);
            treasureRoom.AddExit("south", corridor);

            // Set the starting room
            currentRoom = dungeon;

            // Store rooms in a dictionary
            rooms = new Dictionary<string, Room>
            {
                { "dungeon", dungeon },
                { "corridor", corridor },
                { "treasureRoom", treasureRoom }
            };
        }

        // Starts the main game loop
        public void Start()
        {
            Console.WriteLine($"\nWelcome, {player.Name}! You find yourself in a dungeon.");

            // Main game loop
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

        // Handles player input commands
        private void HandleInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Please enter a command.");
                return;
            }

            string[] parts = input.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
            string command = parts[0];
            string argument = parts.Length > 1 ? parts[1] : "";

            switch (command)
            {
                case "look":
                    Console.WriteLine(currentRoom.GetDescription());
                    break;

                case "take":
                    if (!string.IsNullOrWhiteSpace(argument))
                    {
                        if (currentRoom.TakeItem(argument, player))
                            Console.WriteLine($"You picked up the {argument}.");
                        else
                            Console.WriteLine($"You can't take {argument}.");
                    }
                    else
                    {
                        Console.WriteLine("Take what? Please specify an item.");
                    }
                    break;

                case "inventory":
                    Console.WriteLine(player.InventoryContents());
                    break;

                case "use":
                    if (!string.IsNullOrWhiteSpace(argument))
                        player.UseItem(argument);
                    else
                        Console.WriteLine("Use what? Please specify an item.");
                    break;

                case "go":
                    if (!string.IsNullOrWhiteSpace(argument))
                    {
                        if (currentRoom.HasExit(argument))
                        {
                            currentRoom = currentRoom.GetExit(argument);
                            Console.WriteLine($"You moved {argument}.");
                            currentRoom.TriggerTrap(player);

                            if (!player.IsAlive())
                                playing = false;
                        }
                        else
                        {
                            Console.WriteLine("You can't go that way.");
                            Console.WriteLine($"Available directions: {string.Join(", ", GetAvailableDirections())}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Go where? Please specify a direction.");
                        Console.WriteLine($"Available directions: {string.Join(", ", GetAvailableDirections())}");
                    }
                    break;

                case "exit":
                    Console.WriteLine("Thanks for playing Dungeon Explorer!");
                    playing = false;
                    break;

                default:
                    Console.WriteLine("Invalid command.");
                    Console.WriteLine("Available commands: look, take <item>, go <direction>, inventory, use <item>, exit");
                    break;
            }
        }

        // Extracts directions available in the current room
        private List<string> GetAvailableDirections()
        {
            List<string> directions = new List<string>();
            if (currentRoom != null)
            {
                string description = currentRoom.GetDescription();
                string[] lines = description.Split('\n');
                foreach (string line in lines)
                {
                    if (line.StartsWith("You are in:")) continue;
                    if (line.Contains("Exits:"))
                    {
                        string exitLine = line.Substring(line.IndexOf("Exits:") + 7);
                        directions.AddRange(exitLine.Split(new[] { ',', '.' }, StringSplitOptions.RemoveEmptyEntries));
                    }
                }
            }
            return directions;
        }
    }
}
