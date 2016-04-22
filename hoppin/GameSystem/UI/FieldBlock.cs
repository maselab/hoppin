using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace hoppin.GameSystem.UI
{
    class FieldBlock
    {
        Style style = new Style();
        public void draw(PaintEventArgs e, int x, int y)
        {
            drawBlankBlock(e, x, y);
        }
        // 空のフィールドを描画
        private void drawBlankBlock(PaintEventArgs e, int x, int y)
        {
            // left-top
            e.Graphics.FillRectangle(new SolidBrush(style.separationColor), 42 * x, 42 * y, 7, 2);
            e.Graphics.FillRectangle(new SolidBrush(style.separationColor), 42 * x, 42 * y, 2, 7);
            // right-top
            e.Graphics.FillRectangle(new SolidBrush(style.separationColor), 42 * x + 37, 42 * y, 7, 2);
            e.Graphics.FillRectangle(new SolidBrush(style.separationColor), 42 * x + 42, 42 * y, 2, 7);
            // left-bottom
            e.Graphics.FillRectangle(new SolidBrush(style.separationColor), 42 * x, 42 * y + 42, 7, 2);
            e.Graphics.FillRectangle(new SolidBrush(style.separationColor), 42 * x, 42 * y + 37, 2, 7);
            // right-bottom
            e.Graphics.FillRectangle(new SolidBrush(style.separationColor), 42 * x + 37, 42 * y + 42, 7, 2);
            e.Graphics.FillRectangle(new SolidBrush(style.separationColor), 42 * x + 42, 42 * y + 37, 2, 7);
        }
    }
}
