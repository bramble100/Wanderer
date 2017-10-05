using System;
using System.Collections.Generic;

namespace WandererEngine
{
    public class MovingObjects
    {
        public Hero Hero;
        public List<Monster> Monsters;

        private Dice Dice;

        public MovingObjects(int totalNumberOfMonsters, int areaLevel, Dice dice)
        {
            Dice = dice;

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
