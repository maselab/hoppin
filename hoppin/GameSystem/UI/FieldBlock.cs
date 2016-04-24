using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace hoppin.GameSystem.UI
{
    public enum BlockType
    {
        Blank,
        PaintedA, PaintedB, PaintedC, PaintedD,  
        PlayerA, PlayerB, PlayerC, PlayerD,
        ArrowT, ArrowR, ArrowB, ArrowL,
        Dash
    }
    public class FieldBlock
    {
        Style style = new Style();
        public void draw(PaintEventArgs e, int x, int y, BlockType blockType)
        {
            e.Graphics.ResetTransform();
            e.Graphics.TranslateTransform(42 * (x+1), 42 * (y+1));
            drawBlockFrame(e);
            if (blockType != BlockType.Blank)
            {
                SolidBrush blockColor;
                switch (blockType)
                {
                    case BlockType.PlayerA:
                    case BlockType.PaintedA:
                        blockColor = new SolidBrush(style.playerAColor);
                        break;
                    case BlockType.PlayerB:
                    case BlockType.PaintedB:
                        blockColor = new SolidBrush(style.playerBColor);
                        break;
                    case BlockType.PlayerC:
                    case BlockType.PaintedC:
                        blockColor = new SolidBrush(style.playerCColor);
                        break;
                    case BlockType.PlayerD:
                    case BlockType.PaintedD:
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
                switch (blockType)
                {
                    case BlockType.ArrowB:
                    case BlockType.ArrowL:
                    case BlockType.ArrowR:
                    case BlockType.ArrowT:
                        drawArrow(e, blockType);
                        break;
                    case BlockType.Dash:
                        drawShoes(e);
                        break;
                    case BlockType.PlayerA:
                    case BlockType.PlayerB:
                    case BlockType.PlayerC:
                    case BlockType.PlayerD:
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
        void drawArrow(PaintEventArgs e, BlockType blockType)
        {
            e.Graphics.TranslateTransform(22, 22);
            switch (blockType)
            {
                case BlockType.ArrowL:
                    e.Graphics.RotateTransform(90F);
                    break;
                case BlockType.ArrowT:
                    e.Graphics.RotateTransform(180F);
                    break;
                case BlockType.ArrowR:
                    e.Graphics.RotateTransform(270F);
                    break;
                default: break;
            }
            Point[] points = new Point[9];
            points[0] = new Point(-2, -11);
            points[1] = new Point(-2, 2);
            points[2] = new Point(-9, -5);
            points[3] = new Point(-9, 2);
            points[4] = new Point(0, 11);
            points[5] = new Point(9, 2);
            points[6] = new Point(9, -5);
            points[7] = new Point(2, 2);
            points[8] = new Point(2, -11);
            e.Graphics.FillPolygon(new SolidBrush(style.separationColor), points);
        }
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
