using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using hoppin.GameSystem;

namespace hoppin.GameInformation
{
    /// <summary>
    /// 今回作成していただくプレイヤーの抽象クラスです．
    /// ゲーム側はGetMove()を使用してプレイヤーの動作を決定しています．
    /// 使用できる関数は参照値を返すため，取得したデータをそのまま変更すると，関数から取得できる値も変更されてしまいます．
    /// 
    /// </summary>
    [Serializable()]
    public abstract class AbstractPlayer
    {
        protected string name;
        private GameState gameState;

        abstract public PlayerMove GetMove(); 

        /// <summary>
        /// コンストラクタ
        /// 名前表示がNoNameになります
        /// </summary>
        public AbstractPlayer()
        {
            this.name = "NoName";
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">
        /// 表示したい名前
        /// </param>
        public AbstractPlayer(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// プレイヤーの名前を取得する
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }
        
        public void SetGameState(GameState gameState)
        {
            this.gameState = CopyUtility.DeepCopy<GameState>(gameState);
        }

        /// <summary>
        /// FieldObjectをKeyとするPlayerDataのリストを取得する
        /// </summary>
        /// <returns>
        /// FieldObjectをKeyとするPlayerDataのDectionary
        /// </returns>
        public Dictionary<FieldObject,PlayerData> GetPlayerDataList()
        {
            return gameState.playerDataList;
        }
        /// <summary>
        /// 自身のスコア，位置，Shoesの取得状況を取得する
        /// </summary>
        /// <returns>
        /// Type:Dictionary<FieldObject,PlayerData>
        /// 自身のスコア，位置，Shoesの取得状況
        /// </returns>
        public PlayerData GetMyData()
        {
            return gameState.playerDataList[gameState.CurrentPlayer];
        }
        /// <summary>
        /// フィールド上のプレイヤー，その他アイテムの配置を取得する
        /// </summary>
        /// <returns>
        /// Type:FieldObject[,]
        /// 8×8の配列
        /// </returns>
        public FieldObject[,] GetFieldState()
        {
            return gameState.FieldState;
        }

        /// <summary>
        /// フィールドのマスの色を取得する
        /// FieldObjectのBlank,PlayerA,B,C,Dのみが使用される
        /// </summary>
        /// <returns>
        /// Type:FieldObject[,]
        /// 8×8の配列
        /// </returns>
        public FieldObject[,] GetFieldFloorColor()
        {
            return gameState.FieldFloorColor;
        }
        /// <summary>
        /// アイテム：箱の位置のリストを取得する
        /// </summary>
        /// <returns>
        /// Type:List<Position>
        /// 箱の位置のリスト
        /// </returns>
        public List<Position> GetBoxPositionList()
        {
            return gameState.boxPositionList;
        }

        /// <summary>
        /// アイテム：靴の位置のリストを取得する
        /// </summary>
        /// <returns>
        /// 靴の位置のリスト
        /// </returns>
        public List<Position> GetShoesPosition()
        {
            return gameState.shoesPositionList;
        }
        /// <summary>
        /// アイテム：ボーナスの位置のリストを取得する
        /// </summary>
        /// <returns>
        /// ボーナスの位置のリスト
        /// </returns>
        public List<Position> GetBonusPosition()
        {
            return gameState.bonusPositionList;
        }
        /// <summary>
        /// 自身のFieldObjectにおける値を取得する
        /// </summary>
        /// <returns>
        /// Type:FieldObject
        /// PlayerA or PlayerB or PlayerC or PlayerD
        /// </returns>
        public FieldObject GetMyPlayerId()
        {
            return gameState.CurrentPlayer;
        }

        /// <summary>
        /// 現在のターン数を取得する
        /// </summary>
        /// <returns>
        /// ターン数
        /// </returns>
        public int GetTurn()
        {
            return gameState.TurnNum;
        }
    }
}
