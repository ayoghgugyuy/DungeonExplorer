using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonExplorer
{
    public class Room
    {
        private string name;
        private string description;
        private List<Item> items; // Updated to use Item objects
        private List<Monster> monsters; // New: Monsters in the room
        private Dictionary<string, Room> exits;

        public bool HasTrap { get; private set; }
        public int TrapDamage { get; private set; }

        public Room(string name, string description, List<Item> items = null)
        {
            this.name = name;
            this.description = description;
            this.items = items ?? new List<Item>();
            this.monsters = new List<Monster>();
            this.exits = new Dictionary<string, Room>();
            HasTrap = false;
            TrapDamage = 0;
        }

        public string GetName() => name;

        public string GetDescription()
        {
            string itemText = items.Count > 0 ? $" You see: {string.Join(", ", items.Select(i => i.Name))}." : " There are no items here.";
            string monsterText = monsters.Count > 0 ? $" Monsters present: {string.Join(", ", monsters.Select(m => m.Name))}." : "";
            string exitText = exits.Count > 0 ? $" Exits: {string.Join(", ", exits.Keys)}." : " No exits available.";
            return $"You are in: {name}\n{description}{itemText}{monsterText}{exitText}";
        }

        public void AddExit(string direction, Room room)
        {
            if (!exits.ContainsKey(direction))
                exits[direction] = room;
        }

        public bool HasExit(string direction) => exits.ContainsKey(direction);

        public Room GetExit(string direction) => exits.TryGetValue(direction, out Room room) ? room : null;

        public bool TakeItem(string itemName, Player player)
        {
            var item = items.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                items.Remove(item);
                player.PickUpItem(item);
                return true;
            }
            return false;
        }

        public void AddItem(Item item)
        {
            if (item != null)
                items.Add(item);
        }

        public void AddMonster(Monster monster)
        {
            if (monster != null)
                monsters.Add(monster);
        }

        public void RemoveMonster(Monster monster)
        {
            if (monster != null)
                monsters.Remove(monster);
        }

        public List<Monster> GetMonsters() => new List<Monster>(monsters);

        public Monster GetStrongestMonster()
        {
            return monsters.OrderByDescending(m => m.Health).FirstOrDefault();
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
                Console.WriteLine($"{player.Name} hears a faint clicking sound... A TRAP!");
                Console.WriteLine($"{player.Name} triggered a trap and took {TrapDamage} damage!");
                player.TakeDamage(TrapDamage);
                HasTrap = false;
            }
        }
    }
}
