namespace DungeonExplorer
{
    // Base class for all creatures (like players or monsters) that can take damage.
    public abstract class Creature : IDamageable
    {
        // The creature's name and health points.
        public string Name { get; protected set; }
        public int Health { get; protected set; }

        // Constructor to set the creature's name and health when it's created.
        public Creature(string name, int health)
        {
            Name = name;
            Health = health;
        }

        // When the creature takes damage, this method reduces its health.
        // If health drops below 0, it’s set to 0 (creature can't have negative health).
        public virtual void TakeDamage(int amount)
        {
            Health -= amount;
            if (Health < 0) Health = 0;
        }

        // Checks if the creature is still alive based on its health.
        public bool IsAlive()
        {
            return Health > 0;
        }
    }
}
