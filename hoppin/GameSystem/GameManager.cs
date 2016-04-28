using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using hoppin.GameInformation;

namespace hoppin.GameSystem
{
    public class GameManager
    {
        public NewGameState gameState;
        private Dictionary<FieldObject, AbstractPlayer> playerList = new Dictionary<FieldObject, AbstractPlayer>();
        public int PlayCount = 1000;
        private int processFPS;
        private int seed = Environment.TickCount;//乱数用
        private int seedCount = 0;

        private delegate PlayerMove MoveDelegate();

        public GameManager(AbstractPlayer player1, AbstractPlayer player2, AbstractPlayer player3, AbstractPlayer player4,int playCount, int fps)
        {
            playerList.Add(FieldObject.PlayerA, player1);
            playerList.Add(FieldObject.PlayerB, player2);
            playerList.Add(FieldObject.PlayerC, player3);
            playerList.Add(FieldObject.PlayerD, player4);
            gameState = new NewGameState(GetPlayerName(),playCount);
            this.processFPS = fps;
        }


        public List<String> GetPlayerName()
        {
            List<string> retList = new List<string>();
            foreach(KeyValuePair<FieldObject,AbstractPlayer> pair in playerList)
            {
                retList.Add(pair.Value.Name);
            }
            return retList;
        }

        public int GetPlayCount()
        {
            return PlayCount;
        } 

        #region ゲーム進行
        public void ProcessGame()
        {
            ///N回まわしたら終了
            ///player毎にmoveもらってJudgeになげる．
            ///最終結果を最後に表示して終わり．
            ///
            System.Threading.Thread.Sleep(3000);

            for(int i = 0; i < gameState.MaxTurn; i++)
            {
                gameState.TurnNum++;
                gameState.CurrentPlayer = FieldObject.PlayerA;
                GenerateItems();
                ProcessTurn();
                if (gameState.CurrentPlayerData.Score >= gameState.MaxScore) break;

                gameState.CurrentPlayer = FieldObject.PlayerB;
                GenerateItems();
                ProcessTurn();
                if (gameState.CurrentPlayerData.Score >= gameState.MaxScore) break;

                gameState.CurrentPlayer = FieldObject.PlayerC;
                GenerateItems();
                ProcessTurn();
                if (gameState.CurrentPlayerData.Score >= gameState.MaxScore) break;

                gameState.CurrentPlayer = FieldObject.PlayerD;
                GenerateItems();
                ProcessTurn();
                if (gameState.CurrentPlayerData.Score >= gameState.MaxScore) break;
            }
            gameState.WriteScore();
        }

        private void ProcessTurn()
        {
            if(gameState.playerDataList[gameState.CurrentPlayer].Shoes > 0)
            {
                playerList[gameState.CurrentPlayer].SetGameState(gameState);
                gameState.playerDataList[gameState.CurrentPlayer].Shoes--;
                MovePlayer();
            }
            playerList[gameState.CurrentPlayer].SetGameState(gameState);
            MovePlayer();
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
                    if (timeSpan.TotalMilliseconds > gameState.ThinkTime)
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
                JudgeMove();
                if (timeSpan.TotalMilliseconds + processFPS < gameState.ThinkTime)
                    Thread.Sleep(processFPS);
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
        #endregion

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
            if(IsBump())
            {
                Position destination = GetPlayerDestination();
                if (IsGetItems() == FieldObject.Bonus)//アイテム1
                {
                    Repaint();
                    GetBonus(destination);
                    gameState.bonusPositionList.RemoveAll(s => s.IsSamePosition(destination));
                    SearchClosedSpace();
                }
                else if (IsGetItems() == FieldObject.Box)//アイテム2
                {
                    Repaint();
                    //BOX();
                    AddScore();
                    gameState.boxPositionList.RemoveAll(s => s.IsSamePosition(destination));
                }
                else if (IsGetItems() == FieldObject.Shoes)//アイテム3
                {
                    Repaint();
                    gameState.CurrentPlayerData.Shoes = gameState.ShoesTurn;
                    gameState.shoesPositionList.RemoveAll(s =>s.IsSamePosition(destination));
                }
                else if (IsGetItems() == FieldObject.Blank)
                {
                    Repaint();
                }
                else
                {
                }
            }

        }
        private bool IsBump()
        {
            int playerHeight = gameState.CurrentPlayerData.position.Y;
            int playerWidth = gameState.CurrentPlayerData.position.X;

            if (gameState.CurrentPlayerMove == PlayerMove.Up && playerHeight == 0 ||
                gameState.CurrentPlayerMove == PlayerMove.Down && playerHeight == 7 ||
                gameState.CurrentPlayerMove == PlayerMove.Right && playerWidth == 7 ||
                gameState.CurrentPlayerMove == PlayerMove.Left && playerWidth == 0
              )
            {
               //Debug.Write("壁");
                return false;
            }
            int height = 0; int width = 0;
            if (gameState.CurrentPlayerMove == PlayerMove.Up)
                height = -1;
            else if (gameState.CurrentPlayerMove == PlayerMove.Down)
                height = 1;
            else if (gameState.CurrentPlayerMove == PlayerMove.Right)
                width = +1;
            else if (gameState.CurrentPlayerMove == PlayerMove.Left)
                width = -1;

            if ( gameState.FieldState[playerHeight + height, playerWidth + width] == FieldObject.Blank ||
                 gameState.FieldState[playerHeight + height, playerWidth + width] == FieldObject.Bonus ||
                 gameState.FieldState[playerHeight + height, playerWidth + width] == FieldObject.Box   ||
                 gameState.FieldState[playerHeight + height, playerWidth + width] == FieldObject.Shoes 
                )//移動ok
            {
                //Debug.Write("人");
                return true;
            }
            return false;
        }
        private void GetBonus(Position destination)
        {
            for(int i = 0; i < gameState.FieldHeight; i++)
            {
                gameState.FieldFloorColor[i, destination.X] = gameState.CurrentPlayer;
            }

            for(int i = 0; i < gameState.FieldWidth; i++)
            {
                gameState.FieldFloorColor[destination.Y, i] = gameState.CurrentPlayer;
            }
        }
        private Position GetPlayerDestination()
        {
            int height = 0; int width = 0;
            if (gameState.CurrentPlayerMove == PlayerMove.Up)
                height = -1;
            else if (gameState.CurrentPlayerMove == PlayerMove.Down)
                height = 1;
            else if (gameState.CurrentPlayerMove == PlayerMove.Right)
                width = +1;
            else if (gameState.CurrentPlayerMove == PlayerMove.Left)
                width = -1;

            return new Position(gameState.CurrentPlayerData.position.X + width, gameState.CurrentPlayerData.position.Y + height);
        }
        private FieldObject IsGetItems()
        {
            int playerHeight = gameState.CurrentPlayerData.position.Y;
            int playerWidth = gameState.CurrentPlayerData.position.X;

            int height = 0; int width = 0;
            if (gameState.CurrentPlayerMove == PlayerMove.Up)
                height = -1;
            else if (gameState.CurrentPlayerMove == PlayerMove.Down)
                height = 1;
            else if (gameState.CurrentPlayerMove == PlayerMove.Right)
                width = +1;
            else if (gameState.CurrentPlayerMove == PlayerMove.Left)
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

            const int MAXIETMNUM = 4;//アイテム全体の上限
            const int ONEBOXGENERATIONPROBABILITY = 20;
            const int TWOBOXGENERATIONPROBABILITY = 10;
            const int ITEMGENERATIONPROBABILITY = 5; //アイテムの発生確率(0~100)

            Random rnd = new Random(seed++);
            seedCount++;
            if(seedCount > 10000)
            {
                seedCount = 0;
                seed = Environment.TickCount;
            }
            int randomX;
            int randomY;


            if(boxNum == 0 && rnd.Next(100) < ONEBOXGENERATIONPROBABILITY)
            {
                randomX = rnd.Next(8);
                randomY = rnd.Next(8);
                if (gameState.FieldState[randomY, randomX] == FieldObject.Blank)
                {
                    gameState.FieldState[randomY, randomX] = FieldObject.Box;
                    gameState.boxPositionList.Add(new Position(randomX, randomY));
                }
            }
            else if (boxNum == 1 && rnd.Next(100) < TWOBOXGENERATIONPROBABILITY)
            {
                randomX = rnd.Next(8);
                randomY = rnd.Next(8);
                if (gameState.FieldState[randomY, randomX] == FieldObject.Blank)
                {
                    gameState.FieldState[randomY, randomX] = FieldObject.Box;
                    gameState.boxPositionList.Add(new Position(randomX, randomY));
                }
            }

            if (itemNum < MAXIETMNUM && rnd.Next(101) < ITEMGENERATIONPROBABILITY)
            {
                randomX = rnd.Next(8);
                randomY = rnd.Next(8);

                if(rnd.Next(2) == 0)
                {
                    if (gameState.FieldState[randomY, randomX] == FieldObject.Blank)
                    {
                        gameState.FieldState[randomY, randomX] = FieldObject.Bonus;
                        gameState.bonusPositionList.Add(new Position(randomX, randomY));
                    }
                }
                else
                {
                    if (gameState.FieldState[randomY, randomX] == FieldObject.Blank)
                    {
                        gameState.FieldState[randomY, randomX] = FieldObject.Shoes;
                        gameState.shoesPositionList.Add(new Position(randomX, randomY));
                    }
                }
                
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
            int playerHeight = gameState.CurrentPlayerData.position.Y;
            int playerWidth = gameState.CurrentPlayerData.position.X;

            int height = 0; int width = 0;
            if (gameState.CurrentPlayerMove == PlayerMove.Up)
                height = -1;
            else if (gameState.CurrentPlayerMove == PlayerMove.Down)
                height = 1;
            else if (gameState.CurrentPlayerMove == PlayerMove.Right)
                width = +1;
            else if (gameState.CurrentPlayerMove == PlayerMove.Left)
                width = -1;

            gameState.FieldFloorColor[playerHeight + height, playerWidth + width] = gameState.CurrentPlayer;//移動先の色塗り替え
            gameState.FieldState[playerHeight, playerWidth] = FieldObject.Blank;//自分のいた位置をBLANKに
            gameState.FieldState[playerHeight + height, playerWidth + width] = gameState.CurrentPlayer;//移動後を自分のマスに
            gameState.CurrentPlayerData.position.Y = playerHeight + height;
            gameState.CurrentPlayerData.position.X = playerWidth + width;
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
                    if (gameState.FieldFloorColor[i, j] == gameState.CurrentPlayer)
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
                    if ((i == 0 || i == gameState.FieldHeight - 1 || j == 0 || j == gameState.FieldWidth - 1)
                    && workField[i, j] == -1)
                    {
                        if (workField[i, j] == -1) workField = Fill(workField, i, j);
                    }
                }

            int enclosedFieldNum = 0;
            for (int i = 0; i < gameState.FieldHeight; i++)//囲み領域の発見
                for (int j = 0; j < gameState.FieldWidth; j++)
                {
                    if (workField[i, j] == -1)
                    {//-1の箇所が囲まれている領域
                        // getArea[i, j] = 1;
                        gameState.FieldFloorColor[i, j] = gameState.CurrentPlayer;
                        enclosedFieldNum++;
                    }
                }

            if(enclosedFieldNum != 0)
            gameState.CurrentPlayerData.Score += ((enclosedFieldNum * enclosedFieldNum) + (3 * enclosedFieldNum) + 2) / 2;
        }
        private int[,] Fill(int[,] field, int i, int j)
        { //塗りつぶしをする再帰関数

            if (j > 0 && field[i, j - 1] == -1)
            {
                field[i, j - 1] = 0;
                field = Fill(field, i, j - 1);
            }
            if (i < gameState.FieldHeight - 1 && field[i + 1, j] == -1)
            { /* 右 */
                field[i + 1, j] = 0;
                field = Fill(field, i + 1, j);
            }
            if (j < gameState.FieldWidth - 1 && field[i, j + 1] == -1)
            {/* 下 */
                field[i, j + 1] = 0;
                field = Fill(field, i, j + 1);
            }
            if (i > 0 && field[i - 1, j] == -1)
            { /* 左 */
                field[i - 1, j] = 0;
                field = Fill(field, i - 1, j);
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
                    if (gameState.FieldFloorColor[i, j] == gameState.CurrentPlayer)
                    {
                        score++;
                        gameState.FieldFloorColor[i, j] = FieldObject.Blank;
                    }
                }

            gameState.CurrentPlayerData.Score += score;
        }
        #endregion

        [Serializable()]
        public class NewGameState : GameState
        {
            public NewGameState(List<string> playerName, int playCount)
            {
                // fieldObjectにplayer配置 :ok
                // fieldStoorColorに初期色 :ok
                this.playerName = playerName;
                this.maxTurn = playCount;

                for(int x = 0; x < this.FieldWidth; x++)
                {
                    for( int y = 0; y < this.FieldHeight; y++)
                    {
                        if(y == 0 && x == 0)
                        {
                            this.FieldState[y, x] = FieldObject.PlayerA;
                            this.FieldFloorColor[y, x] = FieldObject.PlayerA;
                            playerDataList.Add(FieldObject.PlayerA, new PlayerData(x, y));
                        }
                        else if(y == FieldHeight - 1 && x == 0)
                        {
                            this.FieldState[y, x] = FieldObject.PlayerB;
                            this.FieldFloorColor[y, x] = FieldObject.PlayerB;
                            playerDataList.Add(FieldObject.PlayerB, new PlayerData(x, y));
                        }
                        else if(y == FieldHeight - 1 && x == FieldWidth - 1)
                        {
                            this.FieldState[y,x] = FieldObject.PlayerC;
                            this.FieldFloorColor[y,x] = FieldObject.PlayerC;
                            playerDataList.Add(FieldObject.PlayerC, new PlayerData(x, y));
                        }
                        else if(y == 0 && x == FieldWidth - 1)
                        {
                            this.FieldState[y, x] = FieldObject.PlayerD;
                            this.FieldFloorColor[y, x] = FieldObject.PlayerD;
                            playerDataList.Add(FieldObject.PlayerD, new PlayerData(x, y));
                        }
                        else
                        {
                            this.FieldState[y, x] = FieldObject.Blank;
                            this.FieldFloorColor[y, x] = FieldObject.Blank;
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
