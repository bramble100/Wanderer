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
        public List<Monster> Monsters;
        public int TotalNumberOfMonsters;

        private Dice Dice;

        public MovingObjects(int totalNumberOfMonsters, int areaLevel, Dice dice)
        {
            TotalNumberOfMonsters = totalNumberOfMonsters;
            Dice = dice;
            Console.WriteLine($"Moving objects level: {areaLevel}");

            Hero = new Hero(areaLevel, dice);
            Monsters = new List<Monster>()
            {
                new MonsterBoss(areaLevel, dice),
                new KeyHolderMonster(areaLevel, dice)
            };
            for (int i = 2; i < totalNumberOfMonsters; i++)
            {
                Monsters.Add(new Monster(areaLevel, dice));
            }
        }
    }
}
