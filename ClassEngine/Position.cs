using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WandererEngine
{
    public class Position
    {
        public readonly int NUMBER_OF_TILES_X = 10;
        public readonly int NUMBER_OF_TILES_Y = 11;

        private int index;

        public Position(int index)
        {
            this.index = index;
        }

        public Position(int xPosition, int yPosition)
        {
            index = yPosition * NUMBER_OF_TILES_X + xPosition;
        }

        public int Index { get; set; }

        public int XPosition { get => index % NUMBER_OF_TILES_X; }
        public int YPosition { get => index / NUMBER_OF_TILES_X; }
    }
}
