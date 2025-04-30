using System;

namespace DungeonExplorer
{
    // A key is an item that the player can use to unlock doors.
    public class Key : Item
    {
        // By default, the key is named "Key".
        // You can give it a different name if needed.
        public Key(string name = "Key") : base(name) { }

        // Keys are consumed after they’re used.
        public override bool IsConsumable => true;

        public override void Use(Player player)
        {
            Console.WriteLine($"{player.Name} uses the {Name}. It might open a locked door.");
        }
    }
}
