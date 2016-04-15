using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hoppin.GameSystem
{
    enum PlayerMove : int { UP,DOWN,LEFT,RIGHT };
    enum FieldId : int { BLANK,PLAYER1,PLAYER2,PLAYER3,PLAYER4,SHOES,ARROW };

    class GameState
    {
        private const int PLAYERNUM = 4;
        private const int FIELDHEIGHT = 8;
        private const int FIELDWIDTH = 8;
        private int[,] fieldState = new int[FIELDHEIGHT,FIELDWIDTH];
        protected Dictionary<int, int> playerScoreList = new Dictionary<int, int>();
        protected Dictionary<int, AbstractPlayer> playerList = new Dictionary<int, AbstractPlayer>();


        public int FieldWidth
        {
            get { return FIELDWIDTH; }
        }
        
        public int FieldHeight
        {
            get { return FIELDHEIGHT; }
        }

        public int[,] FieldState
        {
            get { return this.fieldState; }
            set { this.fieldState = value; }
        }

    }
}
