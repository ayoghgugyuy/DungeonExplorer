namespace DungeonExplorer
{
    // This interface is for anything that can take damage (like players or monsters).
    // It forces the implementing class to have a method to handle taking damage.
    public interface IDamageable
    {
        // Takes the specified amount of damage.
        void TakeDamage(int amount);
    }
}
