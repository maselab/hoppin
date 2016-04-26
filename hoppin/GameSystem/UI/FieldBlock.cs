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
    public class FieldBlock
    {
        Style style = new Style();
        public void draw(PaintEventArgs e, int x, int y, FieldColor fieldColor)
        {
            e.Graphics.ResetTransform();
            e.Graphics.TranslateTransform(42 * (x+1), 42 * (y+1));
            drawBlockFrame(e);
            if (fieldColor != FieldColor.BLANK)
            {
                SolidBrush blockColor;
                switch (fieldColor)
                {
                    case FieldColor.PLAYER1:
                        blockColor = new SolidBrush(style.playerAColor);
                        break;
                    case FieldColor.PLAYER2:
                        blockColor = new SolidBrush(style.playerBColor);
                        break;
                    case FieldColor.PLAYER3:
                        blockColor = new SolidBrush(style.playerCColor);
                        break;
                    case FieldColor.PLAYER4:
                        blockColor = new SolidBrush(style.playerDColor);
                        break;
                    default:
                        blockColor = new SolidBrush(style.separationColor);
                        break;
                }
                e.Graphics.FillRectangle(blockColor, 4, 4, 3, 36);
                e.Graphics.FillRectangle(blockColor, 4, 4, 36, 3);
                e.Graphics.FillRectangle(blockColor, 4, 37, 36, 3);
                e.Graphics.FillRectangle(blockColor, 37, 4, 3, 36);
                //switch (fieldColor)
                //{
                //    case fieldColor.ArrowB:
                //    case fieldColor.ArrowL:
                //    case fieldColor.ArrowR:
                //    case fieldColor.ArrowT:
                //        drawArrow(e, fieldColor);
                //        break;
                //    case fieldColor.Dash:
                //        drawShoes(e);
                //        break;
                //    case fieldColor.PlayerA:
                //    case fieldColor.PlayerB:
                //    case fieldColor.PlayerC:
                //    case fieldColor.PlayerD:
                //    case fieldColor.Box:
                //        e.Graphics.FillRectangle(blockColor, 9, 9, 26, 26);
                //        break;
                //    default: break;
                //}
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
        //void drawArrow(PaintEventArgs e, fieldColor fieldColor)
        //{
        //    e.Graphics.TranslateTransform(22, 22);
        //    switch (fieldColor)
        //    {
        //        case fieldColor.ArrowL:
        //            e.Graphics.RotateTransform(90F);
        //            break;
        //        case fieldColor.ArrowT:
        //            e.Graphics.RotateTransform(180F);
        //            break;
        //        case fieldColor.ArrowR:
        //            e.Graphics.RotateTransform(270F);
        //            break;
        //        default: break;
        //    }
        //    Point[] points = new Point[9];
        //    points[0] = new Point(-2, -11);
        //    points[1] = new Point(-2, 2);
        //    points[2] = new Point(-9, -5);
        //    points[3] = new Point(-9, 2);
        //    points[4] = new Point(0, 11);
        //    points[5] = new Point(9, 2);
        //    points[6] = new Point(9, -5);
        //    points[7] = new Point(2, 2);
        //    points[8] = new Point(2, -11);
        //    e.Graphics.FillPolygon(new SolidBrush(style.separationColor), points);
        //}
        void drawBlockFrame(PaintEventArgs e)
        {
            // left-top
            e.Graphics.FillRectangle(new SolidBrush(style.separationColor), 0, 0, 7, 2);
            e.Graphics.FillRectangle(new SolidBrush(style.separationColor), 0, 0, 2, 7);
            // right-top
            e.Graphics.FillRectangle(new SolidBrush(style.separationColor), 37, 0, 7, 2);
            e.Graphics.FillRectangle(new SolidBrush(style.separationColor), 42, 0, 2, 7);
            // left-bottom
            e.Graphics.FillRectangle(new SolidBrush(style.separationColor), 0, 42, 7, 2);
            e.Graphics.FillRectangle(new SolidBrush(style.separationColor), 0, 37, 2, 7);
            // right-bottom
            e.Graphics.FillRectangle(new SolidBrush(style.separationColor), 37, 42, 7, 2);
            e.Graphics.FillRectangle(new SolidBrush(style.separationColor), 42, 37, 2, 7);
        }
    }
}
