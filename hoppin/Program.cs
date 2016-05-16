﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using hoppin.GameSystem;
using hoppin.Player;

namespace hoppin
{

    /// <summary>
    /// ゲームシステムのクラス
    /// </summary>
    /// <returns>ファイルの内容</returns>
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //引数：(AbstractPlayer , AbstractPlayer, AbstractPlayer, AbstractPlayer, int ターン数, int ゲーム進行速度)
            //思考時間は500msです
            //ゲーム進行速度：1プレイヤーごとの時間を調整
            //例：100 → 進行を100ms遅らせる
            //0 ~ 500 までの値で調整
            //GameManager gameManager = new GameManager
            //    (new SamplePlayer("a"), new SamplePlayer(), new SamplePlayer("c"), new SamplePlayer("d"),500,10);
            
            //思考時間を調節できるようになりました．
            //引数：(AbstractPlayer , AbstractPlayer, AbstractPlayer, AbstractPlayer, int ターン数, int ゲーム進行速度, int 思考時間(ms))
            GameManager gameManager = new GameManager
                (new SamplePlayer("a"), new SamplePlayer(), new SamplePlayer("c"), new SamplePlayer("d"), 500, 10, 500);
            //上記：思考時間 1000ms

            Application.Run(new HoppinUI(gameManager));
            
        }
    }
}
