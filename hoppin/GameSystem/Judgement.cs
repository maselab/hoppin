using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hoppin.GameSystem
{
    abstract class Judgement
    {
        //各プレイヤーの1マス移動毎に呼び出される関数
        //やること
        //・マスの塗り替え、囲みによる塗り替え判定
        //・アイテム取得判定
        //  ・アイテムの生成
        //  ・スコア換算
        //GameStateを返す
        abstract public void JudgePlayerMove();

    }
}
