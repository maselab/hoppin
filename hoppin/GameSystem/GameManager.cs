using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace hoppin.GameSystem
{
    class GameManager
    {
        private PrivateGameState gameState;
        private Dictionary<FieldObject, AbstractPlayer> playerList = new Dictionary<FieldObject, AbstractPlayer>();
        private const int PlayCount = 200;

        private delegate PlayerMove MoveDelegate();

        public GameManager()
        {
        }

        public GameManager(AbstractPlayer player1, AbstractPlayer player2, AbstractPlayer player3, AbstractPlayer player4)
        {
            playerList.Add(FieldObject.PLAYER1, player1);
            playerList.Add(FieldObject.PLAYER2, player2);
            playerList.Add(FieldObject.PLAYER3, player3);
            playerList.Add(FieldObject.PLAYER4, player4);
            gameState = new PrivateGameState();
        }

        public void ProcessGame()
        {
            ///N回まわしたら終了
            ///player毎にmoveもらってJudgeになげる．
            ///最終結果を最後に表示して終わり．
            ///
            
            for(int i = 0; i < PlayCount; i++)
            {
                gameState.CurrentPlayer = FieldObject.PLAYER1;
                Debug.WriteLine(gameState.CurrentPlayer);
                ProcessTurn();
                for(int y = 0; y < gameState.FieldHeight; y++)
                {
                    for(int x = 0; x < gameState.FieldWidth; x++)
                    {
                        Debug.Write(gameState.FieldState[y, x] + " ");
                    }
                    Debug.WriteLine("");
                }

                gameState.CurrentPlayer = FieldObject.PLAYER2;
                Debug.WriteLine(gameState.CurrentPlayer);
                ProcessTurn();
                for (int y = 0; y < gameState.FieldHeight; y++)
                {
                    for (int x = 0; x < gameState.FieldWidth; x++)
                    {
                        Debug.Write(gameState.FieldState[y, x] + " ");
                    }
                    Debug.WriteLine("");
                }
                gameState.CurrentPlayer = FieldObject.PLAYER3;
                Debug.WriteLine(gameState.CurrentPlayer);
                ProcessTurn();
                for (int y = 0; y < gameState.FieldHeight; y++)
                {
                    for (int x = 0; x < gameState.FieldWidth; x++)
                    {
                        Debug.Write(gameState.FieldState[y, x] + " ");
                    }
                    Debug.WriteLine("");
                }
                gameState.CurrentPlayer = FieldObject.PLAYER4;
                Debug.WriteLine(gameState.CurrentPlayer);
                ProcessTurn();
                for (int y = 0; y < gameState.FieldHeight; y++)
                {
                    for (int x = 0; x < gameState.FieldWidth; x++)
                    {
                        Debug.Write(gameState.FieldState[y, x] + " ");
                    }
                    Debug.WriteLine("");
                }
            }

            gameState.WriteScore();
        }

        private void ProcessTurn()
        {
            playerList[gameState.CurrentPlayer].SetGameState(gameState);
            MovePlayer();
            if(gameState.playerDataList[gameState.CurrentPlayer].Shoes > 0)
            {
                gameState.playerDataList[gameState.CurrentPlayer].Shoes--;
                MovePlayer();
            }
        }

        private void GetPlayerMove()
        {
            gameState.CurrentPlayerMove = playerList[gameState.CurrentPlayer].GetMove();
        }

        private void MovePlayer()
        {
            
            Thread thread = null;
            Boolean isThreadRunning = false;
            Boolean isThreadTimeOut;

            TimeSpan timeSpan;
            DateTime endTime;
            DateTime startTime = DateTime.Now;

            while (true)
            {
                if (!isThreadRunning)
                {
                    thread = new Thread(new ThreadStart(GetPlayerMove));
                    thread.Start();
                    isThreadRunning = true;
                }
                else
                {
                    endTime = DateTime.Now;
                    timeSpan = endTime - startTime;
                    if (timeSpan.TotalMilliseconds > 1000)
                    {
                        thread.Abort();
                        isThreadTimeOut = true;

                        break;
                    }
                    if (!thread.IsAlive)
                    {
                        isThreadTimeOut = false;
                        break;
                    }
                }
            }

            if(!isThreadTimeOut)
            {
                Debug.WriteLine(gameState.CurrentPlayerMove);
                JudgeMove();
            }

            else
            {
                Debug.WriteLine("Time OVER");
            }
            /*
            MoveDelegate moveDelegate = new MoveDelegate(playerList[gameState.CurrentPlayer].GetMove);

            IAsyncResult ar = moveDelegate.BeginInvoke(null, null);

            gameState.CurrentPlayerMove = moveDelegate.EndInvoke(ar);
            Debug.WriteLine(gameState.CurrentPlayerMove);

            ar.AsyncWaitHandle.WaitOne(1000);
            
            if(ar.IsCompleted)
            {
                gameState.CurrentPlayerMove = moveDelegate.EndInvoke(ar);
                JudgeMove();
            }

            else
            {
                Debug.WriteLine("TimeOver!");
                //ar.AsyncWaitHandle.WaitOne();
            }*/
        }


        #region 移動判定用メソッド群

        private void JudgeMove()
        {
            #region memo
            //・ currentPlayerMoveとFieldObjectを参照し移動先を確認
            //(if)移動先にプレイヤーがいる→無効な移動と判断して動かない
            //(if)移 動先が壁→無効な移動と判断して動かない
            //(if)移動先にアイテムがある→ Repaint , GetItemsを呼び出してアイテム処理
            //(if)移動先が普通のマス → Repaint
            //(if)else 例外

            //GenerateItemsでアイテム生成
            //例外チェック(FieldObjectに各プレーヤーが1人ずついるか)
            #endregion
            if (!IsBump())
            {
                Debug.WriteLine("進めません");
            }
            else
            {
                Position destination = GetPlayerDestination();
                if (IsGetItems() == FieldObject.BONUS)//アイテム1
                {
                    Repaint();
                    GetBonus();
                    gameState.bonusPositionList.RemoveAll(s => s.IsSamePosition(destination));
                    
                }
                else if (IsGetItems() == FieldObject.BOX)//アイテム2
                {
                    Repaint();
                    //BOX();
                    AddScore();
                    gameState.boxPositionList.RemoveAll(s => s.IsSamePosition(destination));
                }
                else if (IsGetItems() == FieldObject.SHOES)//アイテム3
                {
                    Repaint();
                    gameState.CurrentPlayerData.Shoes = gameState.ShoesTurn;
                    gameState.shoesPositionList.RemoveAll(s =>s.IsSamePosition(destination));
                }
                else if (IsGetItems() == FieldObject.BLANK)
                {
                    Repaint();
                }
                else
                {
                    Debug.WriteLine("例外1");
                }
            }
            GenerateItems();//アイテム生成判定

        }
        private bool IsBump()
        {
            int playerHeight = gameState.playerDataList[gameState.CurrentPlayer].PositionY;
            int playerWidth = gameState.playerDataList[gameState.CurrentPlayer].PositionX;

            if (gameState.CurrentPlayerMove == PlayerMove.UP && playerHeight == 0 ||
                gameState.CurrentPlayerMove == PlayerMove.DOWN && playerHeight == 7 ||
                gameState.CurrentPlayerMove == PlayerMove.RIGHT && playerWidth == 7 ||
                gameState.CurrentPlayerMove == PlayerMove.LEFT && playerWidth == 0
              )
            {
               //Debug.Write("壁");
                return false;
            }
            int height = 0; int width = 0;
            if (gameState.CurrentPlayerMove == PlayerMove.UP)
                height = -1;
            else if (gameState.CurrentPlayerMove == PlayerMove.DOWN)
                height = 1;
            else if (gameState.CurrentPlayerMove == PlayerMove.RIGHT)
                width = +1;
            else if (gameState.CurrentPlayerMove == PlayerMove.LEFT)
                width = -1;

            if ( gameState.FieldState[playerHeight + height, playerWidth + width] == FieldObject.BLANK ||
                 gameState.FieldState[playerHeight + height, playerWidth + width] == FieldObject.BONUS ||
                 gameState.FieldState[playerHeight + height, playerWidth + width] == FieldObject.BOX   ||
                 gameState.FieldState[playerHeight + height, playerWidth + width] == FieldObject.SHOES 
                )//移動ok
            {
                //Debug.Write("人");
                return true;
            }
            return false;
        }
        private void GetBonus()
        {
            Debug.WriteLine("{0} ボーナス獲得", gameState.CurrentPlayer);
            //とりあえず今はこうしてる
        }
        private Position GetPlayerDestination()
        {
            int height = 0; int width = 0;
            if (gameState.CurrentPlayerMove == PlayerMove.UP)
                height = -1;
            else if (gameState.CurrentPlayerMove == PlayerMove.DOWN)
                height = 1;
            else if (gameState.CurrentPlayerMove == PlayerMove.RIGHT)
                width = +1;
            else if (gameState.CurrentPlayerMove == PlayerMove.LEFT)
                width = -1;

            return new Position(gameState.CurrentPlayerData.PositionX + width, gameState.CurrentPlayerData.PositionY + height);
        }
        private FieldObject IsGetItems()
        {
            int playerHeight = gameState.playerDataList[gameState.CurrentPlayer].PositionY;
            int playerWidth = gameState.playerDataList[gameState.CurrentPlayer].PositionX;

            int height = 0; int width = 0;
            if (gameState.CurrentPlayerMove == PlayerMove.UP)
                height = -1;
            else if (gameState.CurrentPlayerMove == PlayerMove.DOWN)
                height = 1;
            else if (gameState.CurrentPlayerMove == PlayerMove.RIGHT)
                width = +1;
            else if (gameState.CurrentPlayerMove == PlayerMove.LEFT)
                width = -1;

            return gameState.FieldState[playerHeight + height, playerWidth + width];
        }
        private void GenerateItems()
        {
            #region 概要
            //アイテムの生成
            //生成条件を設ける
            //・一定確率で生成(x%)
            //・盤面にn個アイテムがあると生成しない
            //・生成されたアイテムには最低m個！箱がある
            #endregion

            int itemNum = gameState.bonusPositionList.Count + gameState.shoesPositionList.Count;//盤面のアイテム数
            int boxNum = gameState.boxPositionList.Count;//盤面の箱数

            Debug.WriteLine("item box " + itemNum + " " + boxNum);

            const int MINBOXNUM = 1;//スコア箱の下限
            const int MAXIETMNUM = 4;//アイテム全体の上限
            const int BOXGENERATIONPROBABILITY = 30;
            const int ITEMGENERATIONPROBABILITY = 20; //アイテムの発生確率(0~100)

            int seed = Environment.TickCount;//乱数用
            Random rnd = new Random(seed++);
            int randomX;
            int randomY;


            if(boxNum == 0 && rnd.Next(101) < BOXGENERATIONPROBABILITY)
            {
                randomX = rnd.Next(8);
                randomY = rnd.Next(8);
                if (gameState.FieldState[randomY, randomX] == FieldObject.BLANK)
                {
                    gameState.FieldState[randomY, randomX] = FieldObject.BOX;
                    gameState.boxPositionList.Add(new Position(randomX, randomY));
                }
            }
            else if (boxNum == MINBOXNUM && rnd.Next(101) < BOXGENERATIONPROBABILITY - 10)
            {
                randomX = rnd.Next(8);
                randomY = rnd.Next(8);
                if (gameState.FieldState[randomY, randomX] == FieldObject.BLANK)
                {
                    gameState.FieldState[randomY, randomX] = FieldObject.BOX;
                    gameState.boxPositionList.Add(new Position(randomX, randomY));
                }
            }

            if (itemNum < MAXIETMNUM && rnd.Next(101) < ITEMGENERATIONPROBABILITY)
            {
                randomX = rnd.Next(8);
                randomY = rnd.Next(8);
                if (gameState.FieldState[randomY, randomX] == FieldObject.BLANK)
                {
                    gameState.FieldState[randomY, randomX] = FieldObject.BONUS;
                    gameState.bonusPositionList.Add(new Position(randomX, randomY));
                }   
                    //とりあえずボーナスだけおく
                
            }


        }
        private void Repaint()
        {
            Step();//移動先マスの塗り替えを行う
            SearchClosedSpace();//囲み領域の探索 SearchClosedSpace を呼ぶ
        }
        private void Step()
        {
            //1歩前進
            //アイテムの塗り替えも同時に行われているはず
            int playerHeight = gameState.CurrentPlayerData.PositionY;
            int playerWidth = gameState.CurrentPlayerData.PositionX;

            int height = 0; int width = 0;
            if (gameState.CurrentPlayerMove == PlayerMove.UP)
                height = -1;
            else if (gameState.CurrentPlayerMove == PlayerMove.DOWN)
                height = 1;
            else if (gameState.CurrentPlayerMove == PlayerMove.RIGHT)
                width = +1;
            else if (gameState.CurrentPlayerMove == PlayerMove.LEFT)
                width = -1;

            gameState.FieldFloorColor[playerHeight + height, playerWidth + width] = (FieldColor)(gameState.CurrentPlayer);//移動先の色塗り替え
            //↑ばぐりそう
            gameState.FieldState[playerHeight, playerWidth] = FieldObject.BLANK;//自分のいた位置をBLANKに
            gameState.FieldState[playerHeight + height, playerWidth + width] = gameState.CurrentPlayer;//移動後を自分のマスに
            gameState.CurrentPlayerData.PositionY = playerHeight + height;
            gameState.CurrentPlayerData.PositionX = playerWidth + width;
        }
        private void SearchClosedSpace()
        {

            //囲み空間を探索する関数
            //囲み空間を見つけたら塗りつぶす

            //自色以外のマスを空マスとみなす
            //他色壁に隣接する空マスを見つける。
            //その空マスを使った囲み領域は存在しないので、再帰的にその領域全体を割り出して、除外する。
            //これを全他色壁に対して行い最後までのこる空マスがあれば、囲みあり。
            int[,] workField = new int[gameState.FieldHeight, gameState.FieldWidth];
            // int[,] getArea = new int[gameState.FieldHeight, gameState.FieldWidth];
            for (int i = 0; i < gameState.FieldHeight; i++)//変数を用意
                for (int j = 0; j < gameState.FieldWidth; j++)
                {
                    if (gameState.FieldFloorColor[i, j] == (FieldColor)(gameState.CurrentPlayer))
                    {
                        workField[i, j] = 1;
                    }
                    else
                    {
                        workField[i, j] = -1;
                    }
                    // getArea[i, j] = 0;
                }
            for (int i = 0; i < gameState.FieldHeight; i++)//塗りつぶし作業
                for (int j = 0; j < gameState.FieldWidth; j++)
                {
                    if (i == 0 || i == gameState.FieldHeight - 1 || j == 0 || j == gameState.FieldWidth - 1)
                    {
                        workField = Fill(workField, i, j);

                    }
                }
            for (int i = 0; i < gameState.FieldHeight; i++)//囲み領域の発見
                for (int j = 0; j < gameState.FieldWidth; j++)
                {
                    if (workField[i, j] == -1)
                    {//-1の箇所が囲まれている領域
                        // getArea[i, j] = 1;
                        gameState.FieldFloorColor[i, j] = (FieldColor)(gameState.CurrentPlayer);
                    }
                }

        }
        private int[,] Fill(int[,] field, int x, int y)
        { //塗りつぶしをする再帰関数

            if (y > 0 && field[x, y - 1] == -1)
            {
                field[x, y - 1] = 0;
                Fill(field, x, y - 1);
            }
            if (x < gameState.FieldHeight - 1 && field[x + 1, y] == -1)
            { /* 右 */
                field[x + 1, y] = 0;
                Fill(field, x + 1, y);
            }
            if (y < gameState.FieldWidth - 1 && field[x, y + 1] == -1)
            {/* 下 */
                field[x, y + 1] = 0;
                Fill(field, x, y + 1);
            }
            if (x > 0 && field[x - 1, y] == -1)
            { /* 左 */
                field[x - 1, y] = 0;
                Fill(field, x - 1, y);
            }

            return field;
        }
        private void AddScore()
        {
            //現在塗られている自色をリセットして得点を加算
            int score = 0;

            for (int i = 0; i < gameState.FieldHeight; i++)
                for (int j = 0; j < gameState.FieldWidth; j++)
                {
                    if (gameState.FieldFloorColor[i, j] == (FieldColor)gameState.CurrentPlayer)
                    {
                        score++;
                        gameState.FieldFloorColor[i, j] = FieldColor.BLANK;
                    }
                }

            gameState.playerDataList[gameState.CurrentPlayer].Score += score;
        }
        #endregion

        [Serializable()]
        private class PrivateGameState : GameState
        {
            public PrivateGameState()
            {
                // fieldObjectにplayer配置 :ok
                // fieldStoorColorに初期色 :ok

                for(int x = 0; x < this.FieldWidth; x++)
                {
                    for( int y = 0; y < this.FieldHeight; y++)
                    {
                        if(y == 0 && x == 0)
                        {
                            this.FieldState[y, x] = FieldObject.PLAYER1;
                            this.FieldFloorColor[y, x] = FieldColor.PLAYER1;
                            playerDataList.Add(FieldObject.PLAYER1, new PlayerData(x, y));
                        }
                        else if(y == FieldHeight - 1 && x == 0)
                        {
                            this.FieldState[y, x] = FieldObject.PLAYER2;
                            this.FieldFloorColor[y, x] = FieldColor.PLAYER2;
                            playerDataList.Add(FieldObject.PLAYER2, new PlayerData(x, y));
                        }
                        else if(y == FieldHeight - 1 && x == FieldWidth - 1)
                        {
                            this.FieldState[y,x] = FieldObject.PLAYER3;
                            this.FieldFloorColor[y,x] = FieldColor.PLAYER3;
                            playerDataList.Add(FieldObject.PLAYER3, new PlayerData(x, y));
                        }
                        else if(y == 0 && x == FieldWidth - 1)
                        {
                            this.FieldState[y, x] = FieldObject.PLAYER4;
                            this.FieldFloorColor[y, x] = FieldColor.PLAYER4;
                            playerDataList.Add(FieldObject.PLAYER4, new PlayerData(x, y));
                        }
                        else
                        {
                            this.FieldState[y, x] = FieldObject.BLANK;
                            this.FieldFloorColor[y, x] = FieldColor.BLANK;
                        }
                    }
                }
                
                
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
                set { this.currentPlayerMove = value; }
            }

            public void WriteScore()
            {
                foreach(KeyValuePair<FieldObject,PlayerData> pair in playerDataList)
                {
                    Debug.WriteLine("player{0} : score is {1}",pair.Key, pair.Value.Score);
                }
            }
        }

    }
}
