using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    internal class Room
    {
        private string name; // Name of the room (new feature added for better clarity)
        private string description; // Description of what the room looks like or feels like
        private List<string> items; // Items available in this room
        private Dictionary<string, Room> exits; // Directions and connected rooms (e.g., "north" => Room)

        public bool HasTrap { get; private set; } // Flag to indicate if the room has a trap
        public int TrapDamage { get; private set; } // Amount of damage dealt by the trap

        // Constructor - initializes room with name, description, optional items
        public Room(string name, string description, List<string> items = null)
        {
            this.name = name;
            this.description = description;
            this.items = items ?? new List<string>();
            this.exits = new Dictionary<string, Room>();
            HasTrap = false;
            TrapDamage = 0;
        }

        // Getter for the room's name (if needed from outside)
        public string GetName()
        {
            return name;
        }

        // Returns a full description of the room, including name, items, and exits
        public string GetDescription()
        {
            string itemText = items.Count > 0 ? $" You see: {string.Join(", ", items)}." : " There are no items here.";
            string exitText = exits.Count > 0 ? $" Exits: {string.Join(", ", exits.Keys)}." : " No exits available.";
            return $"You are in: {name}\n{description}{itemText}{exitText}";
        }

        // Add an exit in a specific direction to another room
        public void AddExit(string direction, Room room)
        {
            if (!exits.ContainsKey(direction))
            {
                exits[direction] = room;
            }
        }

        // Check if an exit exists in the given direction
        public bool HasExit(string direction)
        {
            return exits.ContainsKey(direction);
        }

        // Get the room connected in the specified direction
        public Room GetExit(string direction)
        {
            return exits.TryGetValue(direction, out Room room) ? room : null;
        }

        // Allow player to take an item from the room (case-insensitive)
        public bool TakeItem(string item, Player player)
        {
            string foundItem = items.Find(i => i.Equals(item, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(foundItem))
            {
                items.Remove(foundItem); // Remove from room
                player.PickUpItem(foundItem); // Add to player inventory
                return true;
            }
            return false;
        }

        // Set a trap in the room with specified damage value
        public void SetTrap(int damage)
        {
            HasTrap = true;
            TrapDamage = damage;
        }

        // Trigger the trap if it's active – warn the player and apply damage
        public void TriggerTrap(Player player)
        {
            if (HasTrap)
            {
                Console.WriteLine($"{player.Name} hears a faint clicking sound... A TRAP!");
                Console.WriteLine($"{player.Name} triggered a trap and took {TrapDamage} damage!");
                player.TakeDamage(TrapDamage);
                HasTrap = false; // Deactivate trap after triggering (one-time use)
            }
        }
    }
}
