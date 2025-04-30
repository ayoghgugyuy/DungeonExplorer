using System.Collections.Generic;
using System.Linq;

namespace DungeonExplorer
{
    public class Inventory
    {
        private List<Item> items;

        public Inventory()
        {
            items = new List<Item>();
        }

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }

        public List<Item> GetAllItems() => items;

        public List<Weapon> GetWeapons() =>
            items.OfType<Weapon>().ToList();

        public Item FindItemByName(string name) =>
            items.FirstOrDefault(i => i.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
    }
}
