using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using hoppin.GameSystem;

namespace hoppin
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
                int boxDistance = Math.Abs(position.x - myData.PositionX) + Math.Abs(position.y - myData.PositionY);
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

            if (myData.PositionX < nearestBoxPosition.x)
            {
                if (IsMovable(PlayerMove.Right)) return PlayerMove.Right;
            }
            else if (myData.PositionX > nearestBoxPosition.x)
            {
                if (IsMovable(PlayerMove.Left)) return PlayerMove.Left;
            }
            else if (myData.PositionY < nearestBoxPosition.y)
            {
                if (IsMovable(PlayerMove.Down)) return PlayerMove.Down;
            }
            return PlayerMove.Up;

        }
        
        private Boolean IsMovable(PlayerMove playerMove)
        {
            if (playerMove == PlayerMove.Up)
            {
                if (myData.PositionY == 0)
                {
                    return false;
                }

                if (fieldState[myData.PositionY - 1, myData.PositionX] == FieldObject.Blank ||
                    fieldState[myData.PositionY - 1, myData.PositionX] == FieldObject.Shoes ||
                    fieldState[myData.PositionY - 1, myData.PositionX] == FieldObject.Bonus ||
                    fieldState[myData.PositionY - 1, myData.PositionX] == FieldObject.Box)
                {
                    return true;
                }
            }
            else if (playerMove == PlayerMove.Down)
            {
                if (myData.PositionY == 7)
                {
                    return false;
                }

                if (fieldState[myData.PositionY + 1, myData.PositionX] == FieldObject.Blank ||
                    fieldState[myData.PositionY + 1, myData.PositionX] == FieldObject.Shoes ||
                    fieldState[myData.PositionY + 1, myData.PositionX] == FieldObject.Bonus ||
                    fieldState[myData.PositionY + 1, myData.PositionX] == FieldObject.Box)
                {
                    return true;
                }
            }
            else if( playerMove == PlayerMove.Right)
            {
                if(myData.PositionX == 7)
                {
                    return false;
                }

                if (fieldState[myData.PositionY, myData.PositionX + 1] == FieldObject.Blank ||
                   fieldState[myData.PositionY, myData.PositionX + 1] == FieldObject.Shoes ||
                   fieldState[myData.PositionY, myData.PositionX + 1] == FieldObject.Bonus ||
                   fieldState[myData.PositionY, myData.PositionX + 1] == FieldObject.Box)
                {
                    return true;
                }
            }
            else if (playerMove == PlayerMove.Left)
            {
                if (myData.PositionX == 0)
                {
                    return false;
                }

                if (fieldState[myData.PositionY, myData.PositionX - 1] == FieldObject.Blank ||
                   fieldState[myData.PositionY, myData.PositionX - 1] == FieldObject.Shoes ||
                   fieldState[myData.PositionY, myData.PositionX - 1] == FieldObject.Bonus ||
                   fieldState[myData.PositionY, myData.PositionX - 1] == FieldObject.Box)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
