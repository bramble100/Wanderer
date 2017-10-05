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
            Dice = new Dice(new Random());
            Level = 1;
            Area = new Area(Level, Dice);
        }

        public bool HeroIsAlive { get => Area.HeroIsAlive; }

        public void GetNewArea()
        {
            Area.Clear();
            Area = new Area(++Level, Dice);
        }
    }
}
