using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WandererEngine
{
    public class Game
    {
        public int Level;
        public Area Area;
        public Dice Dice;
        public MovingObjects MovingObjects;

        public Game()
        {
            Level = 1;
            Area = new Area(Level, new Dice(new Random()));
            Area.movingObjects.Hero.XPosition = 0;
            Area.movingObjects.Hero.YPosition = 0;
        }
    }
}
