using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WandererEngine;

namespace WandererEngineTests
{
    [TestFixture]
    class BattleTests
    {
        Random Random = new Random();

        [Test]
        public void BasicStance()
        {
            Dice Dice = new Dice(Random);
            Hero Hero = new Hero(1, Dice);
            Hero.InitalizePoints();
            Hero.InitalizeLevel(1);
            Monster Monster = new Monster(1, Dice);
            Monster.InitalizePoints();
            Monster.InitalizeLevel(1);

            Assert.True(Hero.IsAlive);
        }
    }
}
