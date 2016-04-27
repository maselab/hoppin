using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using hoppin.GameSystem;

namespace hoppin.GameSystem.UI
{
    class FieldBlock
    {
        Style style = new Style();
        public void draw(PaintEventArgs e, int x, int y, FieldObject fieldObj)
        {
            e.Graphics.ResetTransform();
            e.Graphics.TranslateTransform(42 * (x + 1), 42 * (y + 1));
            if (fieldObj != FieldObject.BLANK)
            {
                SolidBrush blockColor;
                switch (fieldObj)
                {
                    case FieldObject.PLAYER1:
                        blockColor = new SolidBrush(style.playerAColor);
                        break;
                    case FieldObject.PLAYER2:
                        blockColor = new SolidBrush(style.playerBColor);
                        break;
                    case FieldObject.PLAYER3:
                        blockColor = new SolidBrush(style.playerCColor);
                        break;
                    case FieldObject.PLAYER4:
                        blockColor = new SolidBrush(style.playerDColor);
                        break;
                    default:
                        blockColor = new SolidBrush(style.separationColor);
                        break;
                }
                switch (fieldObj)
                {
                    case FieldObject.BOX:
                    case FieldObject.BONUS:
                    case FieldObject.SHOES:
                        e.Graphics.FillRectangle(blockColor, 4, 4, 3, 36);
                        e.Graphics.FillRectangle(blockColor, 4, 4, 36, 3);
                        e.Graphics.FillRectangle(blockColor, 4, 37, 36, 3);
                        e.Graphics.FillRectangle(blockColor, 37, 4, 3, 36);
                        break;
                    default: break;
                }
                switch (fieldObj)
                {
                    //case fieldObj.ArrowB:
                    //case fieldObj.ArrowL:
                    //case fieldObj.ArrowR:
                    //case fieldObj.ArrowT:
                    //    drawArrow(e, fieldObj);
                    //    break;
                    case FieldObject.SHOES:
                        drawShoes(e);
                        break;
                    case FieldObject.BONUS:
                        drawCross(e);
                        break;
                    case FieldObject.PLAYER1:
                    case FieldObject.PLAYER2:
                    case FieldObject.PLAYER3:
                    case FieldObject.PLAYER4:
                    case FieldObject.BOX:
                        e.Graphics.FillRectangle(blockColor, 9, 9, 26, 26);
                        break;
                    default: break;
                }
            }
            e.Graphics.ResetTransform();
        }
        void drawShoes(PaintEventArgs e)
        {
            e.Graphics.FillEllipse(new SolidBrush(style.separationColor), 13, 15, 8, 14);
            e.Graphics.FillEllipse(new SolidBrush(style.separationColor), 14, 30, 6, 4);
            e.Graphics.FillEllipse(new SolidBrush(style.separationColor), 23, 10, 8, 14);
            e.Graphics.FillEllipse(new SolidBrush(style.separationColor), 24, 25, 6, 4);
        }
        void drawCross(PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(22, 22);
            Point[] points = new Point[7] {
                new Point(-1, 0),
                new Point(-1, 7),
                new Point(-6, 7),
                new Point(0, 13),
                new Point(6, 7),
                new Point(1, 7),
                new Point(1, 0),
            };
            for (int i = 0; i < 4; i++)
            {
                e.Graphics.FillPolygon(new SolidBrush(style.separationColor), points);
                e.Graphics.RotateTransform(90F);
            }
            e.Graphics.ResetTransform();
        }
    }
}
