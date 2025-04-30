using System;

namespace DungeonExplorer
{
    // Base class for items that can be collected and used by the player.
    public abstract class Item : ICollectible
    {
        public string Name { get; set; }

        // Tells us whether the item is consumed after use (e.g., potions, keys).
        public abstract bool IsConsumable { get; }

        public Item(string name)
        {
            Name = name;
        }

        // Every item has a "Use" method, but how it's used depends on the item type.
        public abstract void Use(Player player);
    }

    // A weapon is an item that deals damage when used.
    public class Weapon : Item
    {
        public int Damage { get; private set; }

        // A weapon has a name and a damage value.
        public Weapon(string name, int damage) : base(name)
        {
            Damage = damage;
        }

        // Weapons are not consumed after use (you can keep using them).
        public override bool IsConsumable => false;

        // Using a weapon deals damage to an enemy or target.
        public override void Use(Player player)
        {
            Console.WriteLine($"{player.Name} uses {Name}, dealing {Damage} damage!");
            // Future updates could include adding attack logic here.
        }
    }

    // A potion is an item that heals the player when used.
    public class Potion : Item
    {
        public int HealAmount { get; private set; }

        // Potions have a name and a healing value.
        public Potion(string name, int healAmount) : base(name)
        {
            HealAmount = healAmount;
        }

        // Potions are consumed once used.
        public override bool IsConsumable => true;

        // Using a potion heals the player.
        public override void Use(Player player)
        {
            Console.WriteLine($"{player.Name} uses {Name}, healing {HealAmount} HP.");
            player.Heal(HealAmount);
        }
    }
}
