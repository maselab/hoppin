using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace hoppin.GameSystem.UI
{
    class Background
    {
        Style style = new Style();
        public void draw(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(style.backgroundColor), 0, 0, style.windowWidth, style.windowHeight);
        }
    }
}
