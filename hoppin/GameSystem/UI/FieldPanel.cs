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
    public class FieldPanel
    {
        Style style = new Style();
        public void draw(PaintEventArgs e, int x, int y, FieldObject fieldObj)
        {
            e.Graphics.ResetTransform();
            e.Graphics.TranslateTransform(42 * (x+1), 42 * (y+1));
            drawBlockFrame(e);
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
                e.Graphics.FillRectangle(blockColor, 4, 4, 3, 36);
                e.Graphics.FillRectangle(blockColor, 4, 4, 36, 3);
                e.Graphics.FillRectangle(blockColor, 4, 37, 36, 3);
                e.Graphics.FillRectangle(blockColor, 37, 4, 3, 36);
            }
            e.Graphics.ResetTransform();
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
