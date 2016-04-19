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
        //GameState内のfieldStateと，playerScoreを変更する．

        private GameState gameState;
        private int playerScore;
        private FieldObject currentTurnPlayer;
        private PlayerMove playerMove;

        public Judgement(GameState gameState, int playerScore, FieldObject currentTurnPlayer, PlayerMove playerMove)
        {
            this.gameState = gameState;
            this.playerScore = playerScore;
            this.currentTurnPlayer = currentTurnPlayer;
            this.playerMove = playerMove;
        }
        
        abstract public void JudgePlayerMove();

    }
}
