using System;

namespace DungeonExplorer
{
    // Represents a sword, a type of weapon.
    public class Sword : Weapon
    {
        // Sets default name and damage, unless specified.
        public Sword(string name = "Sword", int damage = 25) : base(name, damage) { }

        // A sword is not consumable.
        public override bool IsConsumable => false;

        // Executes when the sword is used in combat.
        public override void Use(Player player)
        {
            Console.WriteLine($"{player.Name} swings the {Name}, ready to fight!");
        }
    }
}
