using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using hoppin.GameInformation;

namespace hoppin.GameSystem
{



    ///<summary>
    ///全ゲーム情報を保持する
    ///GameManagerが統括する
    ///</summary>
    [Serializable()]
    public class GameState
    {
        private const int FIELDHEIGHT = 8;
        private const int FIELDWIDTH = 8;
        private const int SHOESTURN = 5;
        private int turnNum = 0;
        protected int THINKTIME = 500;
        private const int maxScore = 300;
        protected int maxTurn;
        protected FieldObject[,] fieldState = new FieldObject[FIELDHEIGHT,FIELDWIDTH];
        protected FieldObject[,] fieldFloorColor = new FieldObject[FIELDHEIGHT, FIELDWIDTH];
        public Dictionary<FieldObject,PlayerData> playerDataList = new Dictionary<FieldObject, PlayerData>();
        public List<string> playerName = new List<string>();
        public int PlayCount = new int();

        public List<Position> boxPositionList = new List<Position>();
        public List<Position> shoesPositionList = new List<Position>();
        public List<Position> bonusPositionList = new List<Position>();

        protected PlayerMove currentPlayerMove;
        protected FieldObject currentPlayer;

        public PlayerMove CurrentPlayerMove
        {
            get { return this.currentPlayerMove; }
        }

        public int MaxScore
        {
            get { return maxScore; }
        }

        public FieldObject CurrentPlayer
        {
            get { return this.currentPlayer; }
        }

        public PlayerData CurrentPlayerData
        {
            get { return playerDataList[currentPlayer]; }
        }

        public int TurnNum
        {
            get { return this.turnNum; }
            set { this.turnNum = value; }
        }

        public int FieldWidth
        {
            get { return FIELDWIDTH; }
        }
        
        public int FieldHeight
        {
            get { return FIELDHEIGHT; }
        }

        public int ShoesTurn
        {
            get { return SHOESTURN; }
        }

        public FieldObject[,] FieldState
        {
            get { return this.fieldState; }
            set { this.fieldState = value; }
        }

        public FieldObject[,] FieldFloorColor
        {
            get { return this.fieldFloorColor; }
            set { this.fieldFloorColor = value; }
        }

        public List<int> GetPlayerScore()
        {
            List<int> retList = new List<int>();
            retList.Add(playerDataList[FieldObject.PlayerA].Score);
            retList.Add(playerDataList[FieldObject.PlayerB].Score);
            retList.Add(playerDataList[FieldObject.PlayerC].Score);
            retList.Add(playerDataList[FieldObject.PlayerD].Score);
            return retList;
        }

        public int ThinkTime
        {
            get { return THINKTIME; }
        }

        public int MaxTurn
        {
            get { return maxTurn; }
        }

    }
}
