using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WandererEngine
{
    public class Battle
    {
        MovingObject Attacker;
        MovingObject Defendant;

        public Battle(MovingObject attacker, MovingObject defendant)
        {
            Attacker = attacker;
            Defendant = defendant;
        }

        public bool IsWonByAnyParty
        {
            get
            {
                return !(Attacker.IsAlive && Defendant.IsAlive);
            }
        }

        public void Perform()
        {
            Console.WriteLine("Perform");
            Console.WriteLine(Attacker.IsAlive);
            Console.WriteLine(Defendant.IsAlive);
            while (!IsWonByAnyParty)
            {
                Console.WriteLine("PerformOneRound");
                PerformOneRound();
            }
            Console.WriteLine("EndOfPerform");
            Console.WriteLine(Attacker.IsAlive);
            Console.WriteLine(Defendant.IsAlive);

        }

        private void PerformOneRound()
        {
            Console.WriteLine("PerformOneRound");
            Attacker.Strike(Defendant);
            if (!Defendant.IsAlive)
            {
                Attacker.LevelUp();
                return;
            }
            Defendant.Strike(Attacker);
            if (Attacker.IsAlive)
            {
                Defendant.LevelUp();
            }
        }
    }
}
