using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hoppin.GameSystem
{
    enum PlayerMove : int { up,down,left,right };

    abstract class GameState
    {
        private const int PLAYERNUM = 4;
        private int[,] fieldState = new int[8,8];
        private int[] playerScore = new int[PLAYERNUM]; 
        private AbstractPlayer[] playerArray = new AbstractPlayer[PLAYERNUM];

        public int[,] FieldState
        {
            get { return this.fieldState; }
            set { this.fieldState = value; }
        }

    }
}
