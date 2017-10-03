using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WandererEngine
{
    public class Area : List<Tile>
    {
        public int NUMBER_OF_TILES_X = 10;
        public int NUMBER_OF_TILES_Y = 11;
        public int TILE_SIZE = 50;
        public MovingObjects movingObjects;
        public int totalNumberOfMonsters;
        public int Level;
        public int MIN_NUMBER_OF_MONSTERS = 3;
        public int MAX_NUMBER_OF_MONSTERS = 6;

        Dice Dice;

        public Area(int level, Dice dice)
        {
            Dice = dice;
            Level = level;
            totalNumberOfMonsters = dice.random.Next(MIN_NUMBER_OF_MONSTERS - 2, MAX_NUMBER_OF_MONSTERS - 1);
            movingObjects = new MovingObjects(totalNumberOfMonsters, level, dice);
            AddRange(LayoutGenerator());
            //RandomLayoutGenerator();
        }

        internal List<Tile> LayoutGenerator()
        {
            List<bool> isWalkableList = new List<bool>()
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
            List<Tile> layout = new List<Tile>();
            isWalkableList.ForEach(isWalkable => layout.Add(new Tile(isWalkable)));
            return layout;
        }

        public void RandomLayoutGenerator()
        {
            throw new NotImplementedException();
        }
    }
}
