using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using hoppin.GameSystem;

namespace hoppin
{
    class DebugPlayer :AbstractPlayer
    {
        public override PlayerMove GetMove()
        {
            if (GetTurn() == 1 || GetTurn() == 2)
                return PlayerMove.UP;
            if (GetTurn() == 3 || GetTurn() == 4) return PlayerMove.RIGHT;
            if (GetTurn() == 5 || GetTurn() == 6) return PlayerMove.DOWN;
            if (GetTurn() == 7 || GetTurn() == 8) return PlayerMove.LEFT;
            return PlayerMove.UP;
        }
    }
}
