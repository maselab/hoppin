using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Player作成にあたり必要な情報を含む
using hoppin.GameInformation;

namespace hoppin.Player
{

    class SamplePlayer : AbstractPlayer
    {
        FieldObject[,] fieldState;

        PlayerData myData;
        List<Position> boxList;

        public SamplePlayer() : base()
        {
        }

        public SamplePlayer(string name) : base(name)
        {
        }

        //※This player don't think floorColor!
        //BOXに向かって一直線に走ります
        override public PlayerMove GetMove()
        {
            fieldState = GetFieldState();

            myData = GetMyData();
            boxList = GetBoxPositionList();

            int nearestBoxDistance = 100;//ありえない遠さを初期値とする
            Position nearestBoxPosition = null;

            //Search nearest box position
            foreach(Position position in boxList)
            {
                int boxDistance = Math.Abs(position.X - myData.position.X) + Math.Abs(position.Y - myData.position.Y);
                if( boxDistance < nearestBoxDistance)
                {
                    nearestBoxPosition = position;
                    nearestBoxDistance = boxDistance;
                }
            }

            //箱がないと適当に動く
            if (nearestBoxPosition == null)
            {
                if (IsMovable(PlayerMove.Up)) return PlayerMove.Up;
                if (IsMovable(PlayerMove.Down)) return PlayerMove.Down;
                if (IsMovable(PlayerMove.Left)) return PlayerMove.Left;
                return PlayerMove.Right;
            }

            if (myData.position.X < nearestBoxPosition.X)
            {
                if (IsMovable(PlayerMove.Right)) return PlayerMove.Right;
            }
            else if (myData.position.X > nearestBoxPosition.X)
            {
                if (IsMovable(PlayerMove.Left)) return PlayerMove.Left;
            }
            else if (myData.position.Y < nearestBoxPosition.Y)
            {
                if (IsMovable(PlayerMove.Down)) return PlayerMove.Down;
            }
            return PlayerMove.Up;

        }
        
        private Boolean IsMovable(PlayerMove playerMove)
        {
            if (playerMove == PlayerMove.Up)
            {
                if (myData.position.Y == 0)
                {
                    return false;
                }

                if (fieldState[myData.position.Y - 1, myData.position.X] == FieldObject.Blank ||
                    fieldState[myData.position.Y - 1, myData.position.X] == FieldObject.Shoes ||
                    fieldState[myData.position.Y - 1, myData.position.X] == FieldObject.Bonus ||
                    fieldState[myData.position.Y - 1, myData.position.X] == FieldObject.Box)
                {
                    return true;
                }
            }
            else if (playerMove == PlayerMove.Down)
            {
                if (myData.position.Y == 7)
                {
                    return false;
                }

                if (fieldState[myData.position.Y + 1, myData.position.X] == FieldObject.Blank ||
                    fieldState[myData.position.Y + 1, myData.position.X] == FieldObject.Shoes ||
                    fieldState[myData.position.Y + 1, myData.position.X] == FieldObject.Bonus ||
                    fieldState[myData.position.Y + 1, myData.position.X] == FieldObject.Box)
                {
                    return true;
                }
            }
            else if( playerMove == PlayerMove.Right)
            {
                if(myData.position.X == 7)
                {
                    return false;
                }

                if (fieldState[myData.position.Y, myData.position.X + 1] == FieldObject.Blank ||
                   fieldState[myData.position.Y, myData.position.X + 1] == FieldObject.Shoes ||
                   fieldState[myData.position.Y, myData.position.X + 1] == FieldObject.Bonus ||
                   fieldState[myData.position.Y, myData.position.X + 1] == FieldObject.Box)
                {
                    return true;
                }
            }
            else if (playerMove == PlayerMove.Left)
            {
                if (myData.position.X == 0)
                {
                    return false;
                }

                if (fieldState[myData.position.Y, myData.position.X - 1] == FieldObject.Blank ||
                   fieldState[myData.position.Y, myData.position.X - 1] == FieldObject.Shoes ||
                   fieldState[myData.position.Y, myData.position.X - 1] == FieldObject.Bonus ||
                   fieldState[myData.position.Y, myData.position.X - 1] == FieldObject.Box)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
