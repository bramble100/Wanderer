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
    class MovingObjectTests
    {
        Random Random = new Random();

        public static void Main()
        {

        }

        [Test]
        public void MovingObjectIsAlive()
        {
            Dice Dice = new Dice(Random);
            Hero Hero = new Hero(1, Dice);
            Hero.InitalizePoints();
            Assert.True(Hero.IsAlive);
        }
    }
}
