using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using hoppin.GameSystem;

namespace hoppin
{
    class SamplePlayer : AbstractPlayer
    {
        public SamplePlayer(string name)
        {
            this.name = name;
        }

        override public PlayerMove move()
        {
            return PlayerMove.DOWN;
        }
    }
}
