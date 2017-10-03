using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WandererEngine
{
    public class MovingObjects
    {
        public Hero hero;
        public List<Monster> monsters = new List<Monster>();
        public int totalNumberOfMonsters;
        public int level;

        private Dice dice;

        public MovingObjects(int totalNumberOfMonsters, int areaLevel, Dice dice)
        {
            this.totalNumberOfMonsters = totalNumberOfMonsters;
            this.level = areaLevel;
            this.dice = dice;

            hero = new Hero(areaLevel, dice);
            monsters.Add(new MonsterBoss(level, dice));
            monsters.Add(new KeyHolderMonster(level, dice));
            for (int i = 2; i < totalNumberOfMonsters; i++)
            {
                monsters.Add(new Monster(level, dice));
            }
        }
    }
}
