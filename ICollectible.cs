namespace DungeonExplorer
{
    // This interface is for anything that can be collected and used by the player.
    // It requires items to have a name and a Use method to interact with the player.
    public interface ICollectible
    {
        string Name { get; }

        // Defines how the item is used by the player.
        void Use(Player player);
    }
}
