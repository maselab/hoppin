using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hoppin.GameSystem
{
    abstract class Judgement
    {
        private void MovePlayer()
        {
            //メイン処理
            //・currentPlayerMoveとFieldObjectを参照し移動先を確認
            //(if)移動先にプレイヤーがいる→無効な移動と判断して動かない
            //(if)移動先が壁→無効な移動と判断して動かない
            //(if)移動先にアイテムがある→ Repaint , GetItemsを呼び出してアイテム処理
            //(if)移動先が普通のマス → Repaint
            //(if)else 例外

            //GenerateItemsでアイテム生成
            //例外チェック(FieldObjectに各プレーヤーが1人ずついるか)

        }
        private void GetItems
        {
            //アイテムの取得
            //if(靴)靴取得フラグをたてる
            //if(矢印)矢印方向の色を塗りつぶす
            //if(!箱) AddScoreを呼び出す
            //else 例外

            //取得したアイテムをFieldObjectsから削除
        }
        private void GenerateItems()
        {
            //アイテムの生成
            //生成条件を設ける
            //・一定確率で生成(x%)
            //・盤面にn個アイテムがあると生成しない
            //・生成されたアイテムには最低m個！箱がある
        }
        private void Repaint()
        {
            //移動先マスの塗り替えを行う
            //囲み領域の探索 SearchClosedSpace を呼ぶ
        }
        private Boolean SearchClosedSpace()
        {
            //囲み空間を探索する関数
            //囲み空間を見つけたら塗りつぶす
        }
        private void AddScore()
        {
            //スコア加算
            //スコア変更と盤面の塗り替えも行う
        }
    }
}
