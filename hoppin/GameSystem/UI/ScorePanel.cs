using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace hoppin.GameSystem.UI
{
    class ScorePanel
    {
        Style style = new Style();
        public void draw(PaintEventArgs e, Color panelColor)
        {
            e.Graphics.FillRectangle(new SolidBrush(panelColor), 0, 0, 210, 82);
            //e.Graphics.FillRectangle(new SolidBrush(Color.AliceBlue), 0, 0, 82, 82);
            e.Graphics.DrawString("1P", new Font("Impact", 60), new SolidBrush(style.scoreInactiveColor), new PointF(-8, -8));
            e.Graphics.DrawString("Sample Player", new Font("Impact", 12), new SolidBrush(style.scoreColor), new PointF(82, 7));
            e.Graphics.DrawString("0001", new Font("Impact", 39), new SolidBrush(style.scoreColor), new PointF(74, 20));
        }
    }
}
