using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WandererEngine
{
    /// <summary>
    /// Generates a random map.
    /// </summary>
    public static class RandomMap
    {

        public static List<bool> isWalkableList = new List<bool>()
            {
                true, true, true, false, true, false, true, true, true, true,
                true, true, true, false, true, false, true, false, false, true,
                true, false, false, false, true, false, true, false, false, true,
                true, true, true, true, true, false, true, true, true, true,
                false, false, false, false, true, false, false, false, false, true,
                true, false, true, false, true, true, true, true, false, true,
                true, false, true, false, true, false, false, true, false, true,
                true, true, true, true, true, false, false, true, false, true,
                true,false, false, false, true, true, true, true, false, true,
                true, true, true, false, true, false, false, true, false, true,
                true, false, true, false, true, false, true, true, true, true
            };

        public static List<Tile> Generator()
        {
            string[,] generatedMap = new string[10, 11];
            string[] possibleStates = { "floor", "wall", "possiblefloor", "possiblefork" };
            Position Position = new Position(0, 0);
            List<Position> nextPositions = new List<Position>();

            generatedMap[Position.X, Position.Y] = "floor";
            nextPositions = GetNextPossibleFloorTiles(generatedMap, Position);








            List<Tile> layout = new List<Tile>();
            isWalkableList.ForEach(isWalkable => layout.Add(new Tile(isWalkable)));
            return layout;
        }

        private static List<Position> GetNextPossibleFloorTiles(string[,] generatedMap, Position position)
        {
            // TODO: finish
            // N
            // S
            // E
            // W

            return new List<Position>();
        }
    }
}
