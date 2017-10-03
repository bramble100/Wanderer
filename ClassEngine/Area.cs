using System;
using System.Collections.Generic;
using System.Linq;

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
        public int MIN_NUMBER_OF_MONSTERS = 23;
        public int MAX_NUMBER_OF_MONSTERS = 26;

        Dice Dice;

        public int NumberOfFreeTiles 
            => this.Count(tile => tile.IsWalkable) - movingObjects.Monsters.Count - 1;

        public Area(int level, Dice dice)
        {
            Dice = dice;
            Level = level;
            totalNumberOfMonsters = Dice.random.Next(MIN_NUMBER_OF_MONSTERS , MAX_NUMBER_OF_MONSTERS + 1);
            movingObjects = new MovingObjects(totalNumberOfMonsters, Level, Dice);
            AddRange(LayoutGenerator());
            //RandomLayoutGenerator();
            PlaceMonsters();
        }

        private void PlaceMonsters()
        {
            // place boss
            int monsterPlaceIndexOnTotalArea = GetNextFreeRandomPlace();
            movingObjects.Monsters[0].XPosition = XPosition(monsterPlaceIndexOnTotalArea);
            movingObjects.Monsters[0].YPosition = YPosition(monsterPlaceIndexOnTotalArea);

            // place keyholder
            monsterPlaceIndexOnTotalArea = GetNextFreeRandomPlace();
            movingObjects.Monsters[1].XPosition = XPosition(monsterPlaceIndexOnTotalArea);
            movingObjects.Monsters[1].YPosition = YPosition(monsterPlaceIndexOnTotalArea);

            // place the others
            for (int i = 2; i < movingObjects.Monsters.Count; i++)
            {
                monsterPlaceIndexOnTotalArea = GetNextFreeRandomPlace();
                movingObjects.Monsters[i].XPosition = XPosition(monsterPlaceIndexOnTotalArea);
                movingObjects.Monsters[i].YPosition = YPosition(monsterPlaceIndexOnTotalArea);
            }
        }

        private int GetNextFreeRandomPlace()
        {
            return GetIndexOnTotalArea(Dice.random.Next(NumberOfFreeTiles));
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
                if (TileIsFreeAndWalkable(i))
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

        private bool TileIsFreeAndWalkable(int i)
        {
            if (!this[i].IsWalkable)
            {
                return false;
            }
            if (movingObjects.Hero.XPosition == XPosition(i) && movingObjects.Hero.YPosition == YPosition(i))
            {
                return false;
            }
            return !movingObjects.Monsters.Exists(monster 
                => monster.XPosition == XPosition(i) && monster.YPosition == YPosition(i));
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
            if (TargetTileIsWalkable(movingObjects.Hero.XPosition, movingObjects.Hero.YPosition, direction))
            {
                PerformMove(movingObjects.Hero.XPosition, movingObjects.Hero.YPosition, direction);
            }
        }

        private void PerformMove(int xPosition, int yPosition, Direction direction)
        {
            movingObjects.Hero.LookingDirection = direction;

            if (direction == Direction.Up)
            {
                movingObjects.Hero.YPosition--;
            }
            else if (direction == Direction.Down)
            {
                movingObjects.Hero.YPosition++;
            }
            else if (direction == Direction.Right)
            {
                movingObjects.Hero.XPosition++;
            }
            else if (direction == Direction.Left)
            {
                movingObjects.Hero.XPosition--;
            }

            PerformBattleIfAny();
        }

        private void PerformBattleIfAny()
        {
            foreach (Monster monster in movingObjects.Monsters)
            {
                if (movingObjects.Hero.XPosition == monster.XPosition && 
                    movingObjects.Hero.YPosition == monster.YPosition)
                {
                    Battle battle = new Battle(movingObjects.Hero, monster);
                    battle.Perform();
                }
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