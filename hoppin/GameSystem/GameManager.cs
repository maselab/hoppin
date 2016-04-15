using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hoppin.GameSystem
{
    class GameManager
    {
        private class PrivateGameState : GameState
        {
            public PrivateGameState(AbstractPlayer player1, AbstractPlayer player2, AbstractPlayer player3,AbstractPlayer player4)
            {
                playerList.Add((int)FieldId.PLAYER1, player1);
                playerList.Add((int)FieldId.PLAYER2, player2);
                playerList.Add((int)FieldId.PLAYER3, player3);
                playerList.Add((int)FieldId.PLAYER4, player4);

                playerScoreList.Add((int)FieldId.PLAYER1, 0);
                playerScoreList.Add((int)FieldId.PLAYER2, 0);
                playerScoreList.Add((int)FieldId.PLAYER3, 0);
                playerScoreList.Add((int)FieldId.PLAYER4, 0);

            }
        }
    }
}
