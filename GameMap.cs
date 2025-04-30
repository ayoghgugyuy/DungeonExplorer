using System.Collections.Generic;

namespace DungeonExplorer
{
    // This class is used to keep track of all the rooms in the game.
    public class GameMap
    {
        // A list of rooms in the game.
        public List<Room> Rooms { get; private set; }

        // Initializes the GameMap with an empty list of rooms.
        public GameMap()
        {
            Rooms = new List<Room>();
        }

        // Adds a new room to the map if it's not already there.
        public void AddRoom(Room room)
        {
            if (!Rooms.Contains(room))
                Rooms.Add(room);
        }
    }
}
