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

        public int NumberOfWalkableTiles => this.Count(tile => tile.IsWalkable);

        public Area(int level, Dice dice)
        {
            Dice = dice;
            Level = level;
            totalNumberOfMonsters = dice.random.Next(MIN_NUMBER_OF_MONSTERS - 2, MAX_NUMBER_OF_MONSTERS - 1);
            movingObjects = new MovingObjects(totalNumberOfMonsters, level, dice);
            AddRange(LayoutGenerator());
            PlaceMonsters(NumberOfWalkableTiles);
            //RandomLayoutGenerator();
        }

        private void PlaceMonsters(int numberOfWalkableTiles)
        {
            int field = numberOfWalkableTiles - 1 / totalNumberOfMonsters;

            //Queue<Monster> monsterQueue = new Queue<Monster>();

            //monsterQueue.Enqueue(new MonsterBoss(Level, Dice));

            // place boss
            int monsterPlaceIndexOnWalkableTiles = Dice.random.Next(numberOfWalkableTiles);

            // int monsterPlaceIndexOnWalkableTiles = field * (Dice.random.Next(totalNumberOfMonsters - 1) + 1);
            int monsterPlaceIndexOnTotalArea = GetIndexOnTotalArea(monsterPlaceIndexOnWalkableTiles);
            Console.WriteLine(monsterPlaceIndexOnWalkableTiles);
            movingObjects.monsters[0].XPosition = XPosition(monsterPlaceIndexOnTotalArea);
            movingObjects.monsters[0].YPosition = YPosition(monsterPlaceIndexOnTotalArea);

            // place keyholder
            // place the others
        }
        /// <summary>
        /// Returns index on total area (walls included) by index on walkable tiles (walls excluded).
        /// </summary>
        /// <param name="monsterPlaceIndexOnWalkableTiles"></param>
        /// <returns></returns>
        private int GetIndexOnTotalArea(int monsterPlaceIndexOnWalkableTiles)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].IsWalkable)
                {
                    monsterPlaceIndexOnWalkableTiles--;
                    if (monsterPlaceIndexOnWalkableTiles == 0)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns the X position based on the index in a one-dimensional list.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int XPosition(int index) => index % NUMBER_OF_TILES_X;

        /// <summary>
        /// Returns the Y position based on the index in a one-dimensional list.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int YPosition(int index) => index / NUMBER_OF_TILES_X;

        /// <summary>
        /// Creates the layout based on the predefined pattern.
        /// </summary>
        /// <returns></returns>
        internal List<Tile> LayoutGenerator()
        {
            List<bool> isWalkableList = new List<bool>
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

        public void TryToMoveHero(Direction direction)
        {
            if (TargetTileIsWalkable(movingObjects.hero.XPosition, movingObjects.hero.YPosition, direction))
            {
                PerformMove(movingObjects.hero.XPosition, movingObjects.hero.YPosition, direction);
            }
        }

        private void PerformMove(int xPosition, int yPosition, Direction direction)
        {
            movingObjects.hero.LookingDirection = direction;

            if (direction == Direction.Up)
            {
                movingObjects.hero.YPosition--;
            }
            else if (direction == Direction.Down)
            {
                movingObjects.hero.YPosition++;
            }
            else if (direction == Direction.Right)
            {
                movingObjects.hero.XPosition++;
            }
            else if (direction == Direction.Left)
            {
                movingObjects.hero.XPosition--;
            }

        }

        private bool TargetTileIsWalkable(int xPosition, int yPosition, Direction direction)
        {
            if ((xPosition == 0 && direction == Direction.Left) ||
                (xPosition >= NUMBER_OF_TILES_X - 1 && direction == Direction.Right) ||
                (yPosition == 0 && direction == Direction.Up) ||
                (yPosition >= NUMBER_OF_TILES_Y - 1 && direction == Direction.Down))
            {
                return false;
            }
            if ((direction == Direction.Up && this[GetindexFromCoordinates(xPosition, yPosition - 1)].IsWalkable) ||
                (direction == Direction.Down && this[GetindexFromCoordinates(xPosition, yPosition + 1)].IsWalkable) ||
                (direction == Direction.Left && this[GetindexFromCoordinates(xPosition - 1, yPosition)].IsWalkable) ||
                (direction == Direction.Right && this[GetindexFromCoordinates(xPosition + 1, yPosition)].IsWalkable))
            {
                return true;
            }
            return false;
        }

        private int GetindexFromCoordinates(int xPosition, int yPosition) => yPosition * NUMBER_OF_TILES_X + xPosition;

        public void RandomLayoutGenerator()
        {
            throw new NotImplementedException();
        }
    }
}