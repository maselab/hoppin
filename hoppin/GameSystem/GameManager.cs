using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hoppin.GameSystem
{
    class GameManager
    {
        private PrivateGameState gameState = new PrivateGameState();
        private const int PlayCount = 100;

        delegate PlayerMove MoveDelegate();
        MoveDelegate moveDelegate;

        public void ProcessGame()
        {
            ///N回まわしたら終了
            ///player毎にmoveもらってJudgeになげる．
            ///最終結果を最後に表示して終わり．
            ///
            
            for(int i = 0; i < PlayCount; i++)
            {
                ProcessTurn(FieldObject.PLAYER1);
                ProcessTurn(FieldObject.PLAYER2);
                ProcessTurn(FieldObject.PLAYER3);
                ProcessTurn(FieldObject.PLAYER4);
            }
        }

        private void ProcessTurn(FieldObject turnPlayer)
        {
            moveDelegate = new MoveDelegate(gameState.PlayerList[(int)turnPlayer].move);

            IAsyncResult ar = moveDelegate.BeginInvoke(null, null);

            System.Threading.Thread.Sleep(1000);

            if(ar.IsCompleted)
            {
                gameState.CurrentPlayer = turnPlayer;
                gameState.CurrentPlayerMove = moveDelegate.EndInvoke(ar);
                //Judgement judgement = new Judgement(gameState,gameState.PlayerScoreList[(int)turnPlayer]);
                //judgement.JudgePlayerMove();
            }

            else
            {
                System.Diagnostics.Debug.WriteLine("TimeOver!");
            }
        }

        private class PrivateGameState : GameState
        {
            public PrivateGameState()
            {

            }

            public PrivateGameState(AbstractPlayer player1, AbstractPlayer player2, AbstractPlayer player3,AbstractPlayer player4)
            {
                // fieldObjectにplayer配置 :ok
                // fieldStoorColorに初期色 :
                // playerList,scoreListにデータ挿入 :ok

                for(int column = 0; column < this.FieldWidth; column++)
                {
                    for( int row = 0; row < this.FieldHeight; row++)
                    {
                        if(row == 0 && column == 0)
                        {
                            this.FieldState[row, column] = FieldObject.PLAYER1;
                            this.FieldFloorColor[row, column] = FieldColor.PLAYER1;
                        }
                        else if(row == FieldHeight - 1 && column == 0)
                        {
                            this.FieldState[row, column] = FieldObject.PLAYER2;
                            this.FieldFloorColor[row, column] = FieldColor.PLAYER2;
                        }
                        else if(row == FieldHeight - 1 && column == FieldWidth - 1)
                        {
                            this.FieldState[row,column] = FieldObject.PLAYER3;
                            this.FieldFloorColor[row,column] = FieldColor.PLAYER3;
                        }
                        else if(row == 0 && column == FieldWidth - 1)
                        {
                            this.FieldState[row, column] = FieldObject.PLAYER4;
                            this.FieldFloorColor[row, column] = FieldColor.PLAYER4;
                        }
                        else
                        {
                            this.FieldState[row, column] = FieldObject.BLANK;
                            this.FieldFloorColor[row, column] = FieldColor.BLANK;
                        }
                    }
                }
               
                playerList.Add((int)FieldObject.PLAYER1, player1);
                playerList.Add((int)FieldObject.PLAYER2, player2);
                playerList.Add((int)FieldObject.PLAYER3, player3);
                playerList.Add((int)FieldObject.PLAYER4, player4);

                playerScoreList.Add((int)FieldObject.PLAYER1, 0);
                playerScoreList.Add((int)FieldObject.PLAYER2, 0);
                playerScoreList.Add((int)FieldObject.PLAYER3, 0);
                playerScoreList.Add((int)FieldObject.PLAYER4, 0);
            }

            //以下GameManagerでのみアクセス可能に
            public new FieldObject CurrentPlayer
            {
                get { return this.currentPlayer; }
                set { this.currentPlayer = value; }
            }

            public new PlayerMove CurrentPlayerMove
            {
                get { return this.currentPlayerMove; }
                set { this.currentPlayerMove = CurrentPlayerMove; }
            }

            public Dictionary<int,AbstractPlayer> PlayerList
            {
                get { return this.playerList; }
            }

            public Dictionary<int,int> PlayerScoreList
            {
                get { return this.playerScoreList; }
            }

            public void ChangePlayerScore(FieldObject player, int score)
            {
                this.playerScoreList[(int)player] = score;
            }

        }

    }
}
