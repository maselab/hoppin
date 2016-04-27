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
                if (IsMovable(PlayerMove.UP)) return PlayerMove.UP;
                if (IsMovable(PlayerMove.DOWN)) return PlayerMove.DOWN;
                if (IsMovable(PlayerMove.LEFT)) return PlayerMove.LEFT;
                return PlayerMove.RIGHT;
            }

            if (myData.PositionX < nearestBoxPosition.x)
            {
                if (IsMovable(PlayerMove.RIGHT)) return PlayerMove.RIGHT;
            }
            else if (myData.PositionX > nearestBoxPosition.x)
            {
                if (IsMovable(PlayerMove.LEFT)) return PlayerMove.LEFT;
            }
            else if (myData.PositionY < nearestBoxPosition.y)
            {
                if (IsMovable(PlayerMove.DOWN)) return PlayerMove.DOWN;
            }
            return PlayerMove.UP;

        }
        
        private Boolean IsMovable(PlayerMove playerMove)
        {
            if (playerMove == PlayerMove.UP)
            {
                if (myData.PositionY == 0)
                {
                    return false;
                }

                if (fieldState[myData.PositionY - 1, myData.PositionX] == FieldObject.BLANK ||
                    fieldState[myData.PositionY - 1, myData.PositionX] == FieldObject.SHOES ||
                    fieldState[myData.PositionY - 1, myData.PositionX] == FieldObject.BONUS ||
                    fieldState[myData.PositionY - 1, myData.PositionX] == FieldObject.BOX)
                {
                    return true;
                }
            }
            else if (playerMove == PlayerMove.DOWN)
            {
                if (myData.PositionY == 7)
                {
                    return false;
                }

                if (fieldState[myData.PositionY + 1, myData.PositionX] == FieldObject.BLANK ||
                    fieldState[myData.PositionY + 1, myData.PositionX] == FieldObject.SHOES ||
                    fieldState[myData.PositionY + 1, myData.PositionX] == FieldObject.BONUS ||
                    fieldState[myData.PositionY + 1, myData.PositionX] == FieldObject.BOX)
                {
                    return true;
                }
            }
            else if( playerMove == PlayerMove.RIGHT)
            {
                if(myData.PositionX == 7)
                {
                    return false;
                }

                if (fieldState[myData.PositionY, myData.PositionX + 1] == FieldObject.BLANK ||
                   fieldState[myData.PositionY, myData.PositionX + 1] == FieldObject.SHOES ||
                   fieldState[myData.PositionY, myData.PositionX + 1] == FieldObject.BONUS ||
                   fieldState[myData.PositionY, myData.PositionX + 1] == FieldObject.BOX)
                {
                    return true;
                }
            }
            else if (playerMove == PlayerMove.LEFT)
            {
                if (myData.PositionX == 0)
                {
                    return false;
                }

                if (fieldState[myData.PositionY, myData.PositionX - 1] == FieldObject.BLANK ||
                   fieldState[myData.PositionY, myData.PositionX - 1] == FieldObject.SHOES ||
                   fieldState[myData.PositionY, myData.PositionX - 1] == FieldObject.BONUS ||
                   fieldState[myData.PositionY, myData.PositionX - 1] == FieldObject.BOX)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
