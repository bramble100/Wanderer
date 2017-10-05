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
        public Map Map;
        public Dice Dice;
        public MovingObjects MovingObjects;

        public Game()
        {
            Dice = new Dice(new Random());
            Level = 1;
            Map = new Map(Level, Dice);
        }

        public bool HeroIsAlive { get => Map.HeroIsAlive; }

        public void GetNewArea()
        {
            Map.Clear();
            Map = new Map(++Level, Dice);
        }
    }
}
