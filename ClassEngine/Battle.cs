using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WandererEngine
{
    class Battle
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
            while (IsWonByAnyParty)
            {
                PerformOneRound();
            }
        }

        private void PerformOneRound()
        {
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
