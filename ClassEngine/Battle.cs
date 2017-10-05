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
            Console.WriteLine($"Perform: {Attacker.IsAlive} {Defendant.IsAlive}");
            while (!IsWonByAnyParty)
            {
                Console.WriteLine("PerformOneRound");
                PerformOneRound();
            }
            if (Attacker.IsAlive)
            {
                Attacker.LevelUp();
            }
            else if (Defendant.IsAlive)
            {
                Defendant.LevelUp();
            }
            Console.WriteLine($"EndOfPerform: {Attacker.IsAlive} {Defendant.IsAlive}");
        }

        private void PerformOneRound()
        {
            Console.WriteLine("PerformOneRound");
            Attacker.Strike(Defendant);
            if (!Defendant.IsAlive)
            {
                return;
            }
            Defendant.Strike(Attacker);
        }
    }
}
