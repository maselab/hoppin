using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hoppin.GameSystem
{
    /// <summary>
    /// プレイヤーのデータを表現する汎用クラス
    /// </summary>
    [Serializable()]
    public class PlayerData
    {
        private int shoes;
        private int score;
        /// <summary>
        /// プレイヤーの盤面での位置
        /// </summary>
        public Position position;

        /// <summary>
        /// コンストラクタ
        /// Shoes,Scoreは0に初期化
        /// </summary>
        /// <param name="pointX">プレイヤーのx座標</param>
        /// <param name="pointY">プレイヤーのy座標</param>
        public PlayerData(int pointX, int pointY)
        {
            this.shoes = 0;
            this.score = 0;
            this.position = new Position(pointX, pointY);
        }
        /// <summary>
        /// コンストラクタ
        /// Shoes,Scoreは0に初期化
        /// </summary>
        /// <param name="position">プレイヤーの座標</param>
        public PlayerData(Position position)
        {
            this.shoes = 0;
            this.score = 0;
            this.position = position;
        }
        /// <summary>
        /// アイテム：靴の取得状況
        /// 0:取得していない
        /// 1~5:靴の残りターン数
        /// </summary>
        public int Shoes
        {
            get { return this.shoes; }
            set { this.shoes = value; }
        }

        /// <summary>
        /// プレイヤーのスコア
        /// </summary>
        public int Score
        {
            get { return this.score; }
            set { this.score = value; }
        }

        
    }
}
