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
                return PlayerMove.Up;
            if (GetTurn() == 3 || GetTurn() == 4) return PlayerMove.Right;
            if (GetTurn() == 5 || GetTurn() == 6) return PlayerMove.Down;
            if (GetTurn() == 7 || GetTurn() == 8) return PlayerMove.Left;
            return PlayerMove.Up;
        }
    }
}
