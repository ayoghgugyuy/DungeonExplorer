using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    internal class Room
    {
        private string description;
        private List<string> items;
        private Dictionary<string, Room> exits;
        public bool HasTrap { get; private set; }
        public int TrapDamage { get; private set; }

        public Room(string description, List<string> items = null)
        {
            this.description = description;
            this.items = items ?? new List<string>();
            this.exits = new Dictionary<string, Room>();
            HasTrap = false;
            TrapDamage = 0;
        }

        public string GetDescription()
        {
            string itemText = items.Count > 0 ? $" You see: {string.Join(", ", items)}." : " There are no items here.";
            string exitText = exits.Count > 0 ? $" Exits: {string.Join(", ", exits.Keys)}." : " No exits available.";
            return $"{description}{itemText}{exitText}";
        }

        public void AddExit(string direction, Room room)
        {
            if (!exits.ContainsKey(direction))
            {
                exits[direction] = room;
            }
        }

        public bool HasExit(string direction)
        {
            return exits.ContainsKey(direction);
        }

        public Room GetExit(string direction)
        {
            return exits.TryGetValue(direction, out Room room) ? room : null;
        }

        public bool TakeItem(string item, Player player)
        {
            string foundItem = items.Find(i => i.Equals(item, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(foundItem))
            {
                items.Remove(foundItem);
                player.PickUpItem(foundItem);
                return true;
            }
            return false;
        }

        public void SetTrap(int damage)
        {
            HasTrap = true;
            TrapDamage = damage;
        }

        public void TriggerTrap(Player player)
        {
            if (HasTrap)
            {
                Console.WriteLine($"{player.Name} triggered a trap and took {TrapDamage} damage!");
                player.TakeDamage(TrapDamage);

                // Decide if traps should persist or be one-time use
                HasTrap = false; // If you want persistent traps, remove this line
            }
        }
    }
}
