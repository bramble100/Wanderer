using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WandererEngine;
using NUnit.Framework;

namespace WandererEngineTests
{
    [TestFixture]
    class MonsterTest
    {
        Random Random = new Random();

        [Test]
        public void MonsterIsAlive()
        {
            Dice Dice = new Dice(Random);
            Monster Monster = new Monster(1, Dice);
            Monster.InitalizeLevel(1);
            Monster.InitalizePoints();
            Assert.True(Monster.IsAlive);
        }

        [Test]
        public void MonsterLevelIsGreaterThanZero()
        {
            Dice Dice = new Dice(Random);
            Monster Monster = new Monster(1, Dice);
            Monster.InitalizeLevel(1);
            Monster.InitalizePoints();
            Assert.Greater(Monster.Level, 0);
        }

        [Test]
        public void MonsterLevelIsLessThanThree()
        {
            Dice Dice = new Dice(Random);
            Monster Monster = new Monster(1, Dice);
            Monster.InitalizeLevel(1);
            Monster.InitalizePoints();
            Assert.Less(Monster.Level, 3);
        }
    }
}
