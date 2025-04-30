using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player : Creature
    {
        public int MaxHealth { get; private set; }
        public Inventory Inventory { get; private set; }

        public Player(string name, int maxHealth) : base(name, maxHealth)
        {
            MaxHealth = maxHealth;
            Inventory = new Inventory();
        }

        // Heals the player, ensuring health doesn't exceed max health
        public void Heal(int amount)
        {
            if (amount < 0) return;
            Health = Math.Min(Health + amount, MaxHealth);
            Console.WriteLine($"{Name} healed {amount} points. Current health: {Health}");
        }

        // Adds an item to inventory
        public void PickUpItem(Item item)
        {
            if (item != null)
            {
                Inventory.AddItem(item);
                Console.WriteLine($"{Name} picked up {item.Name}.");
            }
        }

        // Drops an item by name
        public bool DropItem(string itemName)
        {
            var item = Inventory.FindItemByName(itemName);
            if (item != null)
            {
                Inventory.RemoveItem(item);
                Console.WriteLine($"{Name} dropped {item.Name}.");
                return true;
            }

            Console.WriteLine($"{Name} does not have {itemName} in inventory.");
            return false;
        }

        // Uses an item by name
        public void UseItem(string itemName)
        {
            var item = Inventory.FindItemByName(itemName);
            if (item == null)
            {
                Console.WriteLine($"{Name} does not have {itemName} in inventory.");
                return;
            }

            item.Use(this);
            if (item.IsConsumable)
            {
                Inventory.RemoveItem(item);
                Console.WriteLine($"{Name} used {item.Name}.");
            }
        }

        // Returns a string representation of inventory contents
        public string InventoryContents()
        {
            var items = Inventory.GetAllItems();
            return items.Count > 0 ? $"Inventory: {string.Join(", ", items.ConvertAll(i => i.Name))}" : "Inventory is empty.";
        }

        // Displays player status
        public void DisplayStatus()
        {
            Console.WriteLine($"Player: {Name} | Health: {Health}/{MaxHealth}");
            Console.WriteLine(InventoryContents());
        }
    }
}
