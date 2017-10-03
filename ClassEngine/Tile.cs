using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WandererEngine
{
    public class Tile
    {
        public bool IsWalkable;

        public Tile(bool isWalkable)
        {
            IsWalkable = isWalkable;
        }
    }
}