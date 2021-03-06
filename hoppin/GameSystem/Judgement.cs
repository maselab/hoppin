﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hoppin.GameInformation;

namespace hoppin.GameSystem
{
    abstract class Judgement
    {
        #region 参照用
        //enum PlayerMove : int { UP,DOWN,LEFT,RIGHT };
        //enum FieldObject : int { BLANK, PLAYER1, PLAYER2, PLAYER3, PLAYER4, SHOES, ARROW };
        //enum FieldColor : int { BLANK, PLAYER1, PLAYER2, PLAYER3, PLAYER4 };
        //private const int PLAYERNUM = 4;
        //private const int FIELDHEIGHT = 8;
        //private const int FIELDWIDTH = 8;
        //private FieldObject[,] fieldState = new FieldObject[FIELDHEIGHT, FIELDWIDTH];
        //private FieldColor[,] fieldFloorColor = new FieldColor[FIELDHEIGHT, FIELDWIDTH];
        //protected Dictionary<int, int> playerScoreList = new Dictionary<int, int>();
        //protected Dictionary<int, AbstractPlayer> playerList = new Dictionary<int, AbstractPlayer>();
        //protected PlayerMove currentPlayerMove;
        //protected FieldObject currentPlayer;
        #endregion

        private GameState gameState;
        private int playerScore;

        public Judgement(GameState gameState, int playerScore)
        {
            this.gameState = gameState;
            this.playerScore = playerScore;
        }

        abstract public void JudgePlayerMove();

        private void MovePlayer()
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
                Console.Write("進めません");
            }
            else
            {
                if (IsGetItems() == FieldObject.Bonus)//アイテム1
                {
                    Repaint();
                    GetBonus();
                }
                else if (IsGetItems() == FieldObject.Box)//アイテム2
                {
                    Repaint();
                    //BOX();
                    AddScore();
                }
                else if (IsGetItems() == FieldObject.Shoes)//アイテム3
                {
                    Repaint();
                    //ここどうしたらいいの
                }
                else if (IsGetItems() == FieldObject.Blank)
                {
                    Repaint();
                }
                else
                {
                    Console.Write("例外1");
                }
            }
            GenerateItems();//アイテム生成判定

        }
        private bool IsBump()
        {
            int playerHeight = new int(); //そのプレーヤーの位置座標
            int playerWidth = new int(); //そのプレーヤーの位置座標
            FieldObject[,] playersPosition = new FieldObject[gameState.FieldHeight, gameState.FieldWidth];

            for (int i = 0; i < gameState.FieldHeight; i++)
            {
                for (int j = 0; j < gameState.FieldWidth; j++)
                {
                    if (gameState.FieldState[i, j] == FieldObject.PlayerA)
                        playersPosition[i, j] = FieldObject.PlayerA;
                    else if (gameState.FieldState[i, j] == FieldObject.PlayerB)
                        playersPosition[i, j] = FieldObject.PlayerB;
                    else if (gameState.FieldState[i, j] == FieldObject.PlayerC)
                        playersPosition[i, j] = FieldObject.PlayerC;
                    else if (gameState.FieldState[i, j] == FieldObject.PlayerD)
                        playersPosition[i, j] = FieldObject.PlayerD;
                    else
                        playersPosition[i, j] = FieldObject.Blank;
                    if (gameState.FieldState[i, j] == gameState.CurrentPlayer)
                    {
                        playerHeight = i;
                        playerWidth = j;
                    }
                }
            }

            if (gameState.CurrentPlayerMove == PlayerMove.Up && playerHeight == 0 ||
                gameState.CurrentPlayerMove == PlayerMove.Down && playerHeight == 7 ||
                gameState.CurrentPlayerMove == PlayerMove.Right && playerWidth == 7 ||
                gameState.CurrentPlayerMove == PlayerMove.Left && playerWidth == 0
              )
            {
                Console.Write("壁");
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

            else if (!(playersPosition[playerHeight + height, playerWidth + width] == FieldObject.Blank))//移動先に人がいる
            {
                Console.Write("人");
                return false;
            }
            return true;
        }
        private void GetBonus()
        {
            Console.Write("{0} ボーナス獲得", gameState.CurrentPlayer);
            //とりあえず今はこうしてる
        }
        private FieldObject IsGetItems()
        {
            int playerHeight = new int(); //そのプレーヤーの位置座標
            int playerWidth = new int(); //そのプレーヤーの位置座標
            FieldObject[,] itemPosition = new FieldObject[gameState.FieldHeight, gameState.FieldWidth];

            for (int i = 0; i < gameState.FieldHeight; i++)
            {
                for (int j = 0; j < gameState.FieldWidth; j++)
                {
                    if (gameState.FieldState[i, j] == FieldObject.Bonus)
                        itemPosition[i, j] = FieldObject.Bonus;
                    else if (gameState.FieldState[i, j] == FieldObject.Box)
                        itemPosition[i, j] = FieldObject.Box;
                    else if (gameState.FieldState[i, j] == FieldObject.Shoes)
                        itemPosition[i, j] = FieldObject.Shoes;
                    else
                        itemPosition[i, j] = FieldObject.Blank;
                    if (gameState.FieldState[i, j] == gameState.CurrentPlayer)
                    {
                        playerHeight = i;
                        playerWidth = j;
                    }
                }
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

            return itemPosition[playerHeight + height, playerWidth + width];
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

            int itemNum = 0;//盤面のアイテム数
            int boxNum = 0;//盤面の箱数

            const int MINBOXNUM = 1;//スコア箱の下限
            const int MAXIETMNUM = 3;//アイテム全体の上限
            const int GENERATIONPROBABILITY = 10; //アイテムの発生確率(0~100)

            int seed = Environment.TickCount;//乱数用
            Random rnd = new Random(seed++);
            int seed2 = Environment.TickCount + 10;
            Random rnd2 = new Random(seed2++);
            int seed3 = Environment.TickCount;
            Random rnd3 = new Random(seed3--);

            for (int i = 0; i < gameState.FieldHeight; i++)
            {
                for (int j = 0; j < gameState.FieldWidth; j++)
                {
                    if (gameState.FieldState[i, j] == FieldObject.Bonus)
                        itemNum++;
                    else if (gameState.FieldState[i, j] == FieldObject.Box)
                        boxNum++;
                    else if (gameState.FieldState[i, j] == FieldObject.Shoes)
                        itemNum++;
                }
            }

            if(itemNum < MAXIETMNUM && rnd.Next(101) < GENERATIONPROBABILITY){//アイテム生成フラグ
                if(boxNum < MINBOXNUM){
                    if (gameState.FieldState[rnd2.Next(8), rnd3.Next(8)] == FieldObject.Blank)
                    {
                        gameState.FieldState[rnd2.Next(8), rnd3.Next(8)] = FieldObject.Box;
                    }
                }//箱が足りなければ優先して生成
                else{
                    if (gameState.FieldState[rnd2.Next(8), rnd3.Next(8)] == FieldObject.Blank)
                        gameState.FieldState[rnd2.Next(8), rnd3.Next(8)] = FieldObject.Bonus;
                    //とりあえずボーナスだけおく
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
            int playerHeight = new int(); //そのプレーヤーの位置座標
            int playerWidth = new int(); //そのプレーヤーの位置座標

            int height = 0; int width = 0;
            if (gameState.CurrentPlayerMove == PlayerMove.Up)
                height = -1;
            else if (gameState.CurrentPlayerMove == PlayerMove.Down)
                height = 1;
            else if (gameState.CurrentPlayerMove == PlayerMove.Right)
                width = +1;
            else if (gameState.CurrentPlayerMove == PlayerMove.Left)
                width = -1;

            for (int i = 0; i < gameState.FieldHeight; i++)
            {
                for (int j = 0; j < gameState.FieldWidth; j++)
                {
                    if (gameState.FieldState[i, j] == gameState.CurrentPlayer)
                    {
                        playerHeight = i;
                        playerWidth = j;
                        break;
                    }
                }
            }

            gameState.FieldFloorColor[playerHeight, playerWidth] = gameState.CurrentPlayer;//移動先の色塗り替え
            //↑ばぐりそう
            gameState.FieldState[playerHeight, playerWidth] = FieldObject.Blank;//自分のいた位置をBLANKに
            gameState.FieldState[playerHeight + height, playerWidth + width] = gameState.CurrentPlayer;//移動後を自分のマスに
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
            // 1 = 自分の色
            // -1 = 空白マス
            // 0 = 壁に隣接する空白マス

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
                    if( (i == 0 || i == gameState.FieldHeight - 1 || j == 0 || j == gameState.FieldWidth - 1)
                    && workField[i,j] == -1)
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
                        gameState.FieldFloorColor[i, j] = gameState.CurrentPlayer;
                    }
                }

        }
        private int[,] Fill(int[,] field, int x, int y)
        { //塗りつぶしをする再帰関数
            //x = height y = width
            if (y > 0 && field[x, y - 1] == -1)
            {
                Fill(field, x, y - 1);
                field[x, y - 1] = 0;

            }
            if (x < gameState.FieldHeight && field[x + 1, y] == -1)
            { /* 右 */
                Fill(field, x + 1, y);
                field[x + 1, y] = 0;

            }
            if (y < gameState.FieldWidth && field[x, y + 1] == -1)
            {/* 下 */
                Fill(field, x, y + 1);
                field[x, y + 1] = 0;

            }
            if (x > 0 && field[x - 1, y] == -1)
            { /* 左 */
                Fill(field, x - 1, y);
                field[x - 1, y] = 0;
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

            //プレーヤーのスコア　＝　今までのスコア + 変数score
        }

    }
}
