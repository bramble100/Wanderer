using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WandererEngine
{
    public class MonsterBoss : Monster
    {
        internal MonsterBoss(int areaLevel, Dice dice) : base(areaLevel, dice)
        {
            InitalizeLevel(areaLevel);
            System.Console.WriteLine($"{GetType().Name} level: {Level}");
            InitalizePoints();
        }

        /// <summary>
        /// Initializes the HP, SP and DP of the monsterboss.
        /// </summary>
        public override void InitalizePoints()
        {
            // Monster Lvl x(if boss)
            // HP: 2 * x * d6(+d6)
            MaximalHealthPoints = CurrentHealthPoints = 2 * Level * dice.Roll() + dice.Roll();
            // DP: x / 2 * d6(+d6 / 2)
            DefendPoints = Level / 2 * dice.Roll() + dice.Roll() + dice.Roll() / 2;
            // SP: x* d6(+x)
            StrikePoints = Level * dice.Roll() + Level;
        }

    }
}
