using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using hoppin.GameSystem;

namespace hoppin
{
    [Serializable()]
    public abstract class AbstractPlayer
    {
        protected string name;
        private GameState gameState;

        abstract public PlayerMove GetMove(); 

        public string Name
        {
            get { return this.name; }
        }

        public void SetGameState(GameState gameState)
        {
            this.gameState = CopyUtility.DeepCopy<GameState>(gameState);
        }

        public Dictionary<FieldObject,PlayerData> GetPlayerDataList()
        {
            return gameState.playerDataList;
        }
        
        public PlayerData GetMyData()
        {
            return gameState.playerDataList[gameState.CurrentPlayer];
        }

        public FieldObject[,] GetFieldState()
        {
            return gameState.FieldState;
        }

        public FieldObject[,] GetFieldFloorColor()
        {
            return gameState.FieldFloorColor;
        }

        public List<Position> GetBoxPositionList()
        {
            return gameState.boxPositionList;
        }

        public List<Position> GetShoesPosition()
        {
            return gameState.shoesPositionList;
        }

        public List<Position> GetBonusPosition()
        {
            return gameState.bonusPositionList;
        }

        public FieldObject GetPlayerId()
        {
            return gameState.CurrentPlayer;
        }
    }
}
