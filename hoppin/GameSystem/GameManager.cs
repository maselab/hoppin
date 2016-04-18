using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hoppin.GameSystem
{
    class GameManager
    {
        private FieldObject playerTurn;
        private GameState gameState = new PrivateGameState();

        private class PrivateGameState : GameState
        {
            public PrivateGameState()
            {

            }

            public PrivateGameState(AbstractPlayer player1, AbstractPlayer player2, AbstractPlayer player3,AbstractPlayer player4)
            {
                // fieldStateにplayer配置
                // playerList,scoreListにデータ挿入
                // あとは?

                playerList.Add((int)FieldObject.PLAYER1, player1);
                playerList.Add((int)FieldObject.PLAYER2, player2);
                playerList.Add((int)FieldObject.PLAYER3, player3);
                playerList.Add((int)FieldObject.PLAYER4, player4);

                playerScoreList.Add((int)FieldObject.PLAYER1, 0);
                playerScoreList.Add((int)FieldObject.PLAYER2, 0);
                playerScoreList.Add((int)FieldObject.PLAYER3, 0);
                playerScoreList.Add((int)FieldObject.PLAYER4, 0);
            }
        }
    }
}
