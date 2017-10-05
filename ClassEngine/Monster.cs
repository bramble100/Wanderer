using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WandererEngine
{
    public class Monster : MovingObject
    {
        public Monster(int areaLevel, Dice dice) : base(dice)
        {
            InitalizeLevel(areaLevel);
            InitalizePoints();
        }

        /// <summary>
        /// Initializes the level of the monster.
        /// </summary>
        /// <param name="areaLevel"></param>
        public override void InitalizeLevel(int areaLevel) => Level = areaLevel + LevelModifierCalculator();

        /// <summary>
        /// Calculates an additional level increase based on dice roll.
        /// </summary>
        /// <returns></returns>
        private int LevelModifierCalculator()
        {
            // the monsters levels come from the number of the area
            // if its the Xth area, the mosters have lvl X(with 50 % chance) or lvl X + 1(40 %) or lvl X + 2(10 %)
            int random10 = dice.random.Next(10);
            int modifier = 0; // lower 50 %
            modifier += random10 > 4 ? 1 : 0; // upper 50%
            modifier += random10 > 8 ? 1 : 0; // upper 10%
            return modifier;
        }

        /// <summary>
        /// Initializes the HP, SP and DP of the monster.
        /// </summary>
        public override void InitalizePoints()
        {
            // Monster Lvl x
            // HP: 2 * x * d6
            MaximalHealthPoints = CurrentHealthPoints = 2 * Level * dice.Roll();
            // DP: x / 2 * d6
            DefendPoints = Level / 2 * dice.Roll() + dice.Roll();
            // SP: x* d6
            StrikePoints = Level * dice.Roll();
        }
    }
}