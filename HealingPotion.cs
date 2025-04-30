using System;

namespace DungeonExplorer
{
    // Type of potion that heals the player.
    public class HealingPotion : Potion
    {
        // Default values 
        public HealingPotion(string name = "Healing Potion", int healAmount = 20) : base(name, healAmount) { }

        // Healing potions are consumable, so they’re used up after being consumed.
        public override bool IsConsumable => true;

        // When the player uses a healing potion, they get healed and feel rejuvenated.
        public override void Use(Player player)
        {
            base.Use(player);
            Console.WriteLine($"{player.Name} feels rejuvenated after using the {Name}.");
        }
    }
}
