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
            // Create rooms with names and optional items
            Room dungeon = new Room("Dungeon", "A dark, eerie dungeon.", new List<string> { "Torch", "Sword", "Potion" });
            Room corridor = new Room("Corridor", "A narrow, damp corridor. It feels cold.");
            Room treasureRoom = new Room("Treasure Room", "A bright chamber filled with treasure! But there's a trap.");

            // Set up exits between rooms
            dungeon.AddExit("north", corridor);
            corridor.AddExit("south", dungeon);
            corridor.AddExit("north", treasureRoom);
            treasureRoom.AddExit("south", corridor);

            // Add a trap to treasure room
            treasureRoom.SetTrap(10);

            // Start the player in the dungeon
            currentRoom = dungeon;

            // Store rooms in a dictionary for future access
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

            // Keep looping while the player is alive and hasn't exited
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

            // Split input into command and optional argument
            string[] parts = input.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
            string command = parts[0];
            string argument = parts.Length > 1 ? parts[1] : "";

            switch (command)
            {
                case "look":
                    // Show current room description
                    Console.WriteLine(currentRoom.GetDescription());
                    break;

                case "take":
                    // Try to take item from the room
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
                    // Show player's current inventory
                    Console.WriteLine(player.InventoryContents());
                    break;

                case "use":
                    // Use an item if specified
                    if (!string.IsNullOrWhiteSpace(argument))
                        player.UseItem(argument);
                    else
                        Console.WriteLine("Use what? Please specify an item.");
                    break;

                case "go":
                    // Move to another room in a valid direction
                    if (!string.IsNullOrWhiteSpace(argument))
                    {
                        if (currentRoom.HasExit(argument))
                        {
                            currentRoom = currentRoom.GetExit(argument);
                            Console.WriteLine($"You moved {argument}.");

                            // Trigger trap if there's one
                            currentRoom.TriggerTrap(player);
                            if (!player.IsAlive())
                            {
                                playing = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("You can't go that way.");
                            // Suggest available directions to help player
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
                    // End the game
                    Console.WriteLine("Thanks for playing Dungeon Explorer!");
                    playing = false;
                    break;

                default:
                    // Handle unknown commands
                    Console.WriteLine("Invalid command.");
                    Console.WriteLine("Available commands: look, take <item>, go <direction>, inventory, use <item>, exit");
                    break;
            }
        }

        // Helper method to list available directions from current room
        private List<string> GetAvailableDirections()
        {
            List<string> directions = new List<string>();
            if (currentRoom != null)
            {
                // Try to extract all available exits
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
