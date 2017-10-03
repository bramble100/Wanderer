using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WandererEngine
{
    public class MovingObjects
    {
        public Hero Hero;
        public List<Monster> Monsters = new List<Monster>();
        public int TotalNumberOfMonsters;
        public int Level;

        private Dice Dice;

        public MovingObjects(int totalNumberOfMonsters, int areaLevel, Dice dice)
        {
            TotalNumberOfMonsters = totalNumberOfMonsters;
            Level = areaLevel;
            Dice = dice;

            Hero = new Hero(areaLevel, dice);
            Monsters.Add(new MonsterBoss(Level, dice));
            Monsters.Add(new KeyHolderMonster(Level, dice));
            for (int i = 2; i < totalNumberOfMonsters; i++)
            {
                Monsters.Add(new Monster(Level, dice));
            }
        }
    }
}
