﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


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

            string[] name = new string[2] { "deemo", "cytus" };

            List<AbstractPlayer> playerList = new List<AbstractPlayer>(); /// プレイヤーリスト
            playerList.Add(new SamplePlayer());
            playerList.Add(new SamplePlayer());

            Application.Run(new HoppinUI());

            // test message (kodama)

        }
        /// <summary>
        /// This is testFunc()
        /// </summary>
        static void testFunc()
        {

        }
    }
}