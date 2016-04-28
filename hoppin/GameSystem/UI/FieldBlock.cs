using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using hoppin.GameSystem;
using hoppin.GameInformation;

namespace hoppin.GameSystem.UI
{
    class FieldBlock
    {
        Style style = new Style();
        public void draw(PaintEventArgs e, int x, int y, FieldObject fieldObj)
        {
            e.Graphics.ResetTransform();
            e.Graphics.TranslateTransform(42 * (x + 1), 42 * (y + 1));
            if (fieldObj != FieldObject.Blank)
            {
                SolidBrush blockColor;
                switch (fieldObj)
                {
                    case FieldObject.PlayerA:
                        blockColor = new SolidBrush(style.playerAColor);
                        break;
                    case FieldObject.PlayerB:
                        blockColor = new SolidBrush(style.playerBColor);
                        break;
                    case FieldObject.PlayerC:
                        blockColor = new SolidBrush(style.playerCColor);
                        break;
                    case FieldObject.PlayerD:
                        blockColor = new SolidBrush(style.playerDColor);
                        break;
                    default:
                        blockColor = new SolidBrush(style.separationColor);
                        break;
                }
                switch (fieldObj)
                {
                    case FieldObject.Box:
                    case FieldObject.Bonus:
                    case FieldObject.Shoes:
                        e.Graphics.FillRectangle(blockColor, 4, 4, 3, 36);
                        e.Graphics.FillRectangle(blockColor, 4, 4, 36, 3);
                        e.Graphics.FillRectangle(blockColor, 4, 37, 36, 3);
                        e.Graphics.FillRectangle(blockColor, 37, 4, 3, 36);
                        break;
                    default: break;
                }
                switch (fieldObj)
                {
                    case FieldObject.Shoes:
                        drawShoes(e);
                        break;
                    case FieldObject.Bonus:
                        drawCross(e);
                        break;
                    case FieldObject.Box:
                        e.Graphics.FillRectangle(blockColor, 9, 9, 26, 26);
                        drawExMark(e);
                        break;
                    case FieldObject.PlayerA:
                    case FieldObject.PlayerB:
                    case FieldObject.PlayerC:
                    case FieldObject.PlayerD:
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
        void drawExMark(PaintEventArgs e)
        {
            Point[] points = new Point[6]
            {
                new Point(20, 13),
                new Point(24, 13),
                new Point(24, 18),
                new Point(23, 26),
                new Point(21, 26),
                new Point(20, 19),
            };
            e.Graphics.FillPolygon(new SolidBrush(style.backgroundColor), points);
            e.Graphics.FillRectangle(new SolidBrush(style.backgroundColor), 20, 28, 4, 4);
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
