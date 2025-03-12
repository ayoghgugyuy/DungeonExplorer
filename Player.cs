using System.Collections.Generic;
using System;


namespace DungeonExplorer
{
    public class Player
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        private List<string> inventory = new List<string>();

        public Player(string name, int maxHealth)
        {
            Name = name;
            MaxHealth = maxHealth;
            Health = maxHealth; // Start at full health
        }

        // Checks if player is still alive
        public bool IsAlive()
        {
            return Health > 0;
        }

        // Adjusts player's health
        public void TakeDamage(int damage)
        {
            if (damage < 0) return; // Prevent negative damage
            Health = Math.Max(Health - damage, 0);
            Console.WriteLine($"{Name} took {damage} damage. Remaining health: {Health}");

            if (Health == 0)
            {
                Console.WriteLine($"{Name} has died.");
            }
        }

        // Method to retrieve inventory contents
        public string GetInventory()
        {
            return inventory.Count > 0 ? $"Your inventory: {string.Join(", ", inventory)}" : "Inventory is empty.";
        }

        // Heals the player, ensuring health doesn't exceed max health
        public void Heal(int amount)
        {
            if (amount < 0) return; // Prevent negative healing
            Health = Math.Min(Health + amount, MaxHealth);
            Console.WriteLine($"{Name} healed {amount} points. Current health: {Health}");
        }

        // Adds an item to inventory
        public void PickUpItem(string item)
        {
            if (!string.IsNullOrEmpty(item))
            {
                inventory.Add(item);
            }
        }

        // Removes an item from inventory
        public bool DropItem(string item)
        {
            if (inventory.Remove(item))
            {
                Console.WriteLine($"{Name} dropped {item}.");
                return true;
            }
            Console.WriteLine($"{Name} does not have {item} in inventory.");
            return false;
        }

        // Returns inventory as a formatted string
        public string InventoryContents()
        {
            return inventory.Count > 0 ? $"Inventory: {string.Join(", ", inventory)}" : "Inventory is empty.";
        }

        // Uses an item if applicable
        public void UseItem(string item)
        {
            if (!inventory.Contains(item))
            {
                Console.WriteLine($"{Name} does not have {item} in inventory.");
                return;
            }

            switch (item.ToLower())
            {
                case "potion":
                    Heal(20);
                    inventory.Remove(item);
                    Console.WriteLine($"{Name} used a potion and restored 20 health.");
                    break;
                case "key":
                    Console.WriteLine($"{Name} used a key. It might open a locked door!");
                    break;
                default:
                    Console.WriteLine($"{Name} can't use {item}.");
                    break;
            }
        }

        // Displays player status
        public void DisplayStatus()
        {
            Console.WriteLine($"Player: {Name} | Health: {Health}/{MaxHealth}");
            Console.WriteLine(InventoryContents());
        }
    }
}
