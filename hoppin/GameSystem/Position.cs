using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hoppin.GameSystem
{

    /// <summary>
    /// 盤面の位置を表現するための汎用クラス
    /// </summary>
    [Serializable()]
    public class Position
    {
        /// <summary>
        /// 盤面のX座標
        /// </summary>
        private int x;
        /// <summary>
        /// 盤面のy座標
        /// </summary>
        private int y;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="x">盤面のx座標</param>
        /// <param name="y">盤面のy座標</param>
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        /// <summary>
        /// x座標
        /// </summary>
        public int X
        {
            get { return this.x; }
            set { this.x = value; }
        }
        /// <summary>
        /// y座標
        /// </summary>
        public int Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        /// <summary>
        /// x,y座標を共に変更する
        /// </summary>
        /// <param name="x">盤面のx座標</param>
        /// <param name="y">盤面のy座標</param>
        public void SetPoint(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// 引数で受け取った座標が同じ座標か判定
        /// </summary>
        /// <param name="x">比較したい盤面のx座標</param>
        /// <param name="y">比較したい盤面のy座標</param>
        /// <returns>
        /// true:同じ座標
        /// false:異なる座標
        /// </returns>
        public Boolean IsSamePosition(int x, int y)
        {
            return this.x == x && this.y == y;
        }
        /// <summary>
        /// 引数で受け取った座標が同じ座標か判定
        /// </summary>
        /// <param name="compare">比較したい座標</param>
        /// <returns>
        /// true:同じ座標
        /// false:異なる座標
        /// </returns>
        public Boolean IsSamePosition(Position compare)
        {
            return this.x == compare.x && this.y == compare.y;
        }

    }
}
