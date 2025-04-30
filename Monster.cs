using System;

namespace DungeonExplorer
{
    // A monster is a creature that can attack the player.
    public class Monster : Creature
    {
        // How strong the monster's attack is.
        public int AttackPower { get; private set; }

        // When creating a monster, we set its name, health, and attack power.
        public Monster(string name, int health, int attackPower) : base(name, health)
        {
            AttackPower = attackPower;
        }

        // This is what happens when the monster attacks the player.
        // It deals damage and tells the player about it.
        public void Attack(Player player)
        {
            Console.WriteLine($"{Name} attacks {player.Name} for {AttackPower} damage!");
            player.TakeDamage(AttackPower);
        }
    }
}
