using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hoppin.GameSystem
{
    abstract class Judgement
    {
        //もらう
        //GameState
        //  currentPlayer and currentOlayerMove
        //playerScore

        //やること
        //1.移動後の盤面の反映
        //  変更の要因
        //    ・プレイヤーの移動
        //    ・アイテムの取得
        //      ・盤面の繁栄
        //      ・プレイヤースコア書き換え
        //    ・色囲みによる塗りつぶし
        //2.アイテムの生成
        abstract public void JudgePlayerMove();

        private void MovePlayer(){//プレイヤーの移動
        
        }
        private void IsGetItems(){//アイテム取得
        

        }
        private void IsBlockadeTerritory(){//囲み領域の探索

        }
        private void MakeItems(){//アイテムの生成


        }
    }
}
