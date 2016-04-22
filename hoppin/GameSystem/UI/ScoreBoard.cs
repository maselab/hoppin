using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace hoppin.GameSystem.UI
{
    class ScoreBoard
    {
        int currentTurn = 180;
        int turns = 200;
        Style style = new Style();
        public void draw(PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(0, style.windowHeight - 42);
            e.Graphics.FillRectangle(new SolidBrush(style.scoreBoardColor), 0, 0, style.windowWidth, 42);
            int restBarWidth = (int)(style.windowWidth * (double)((double)currentTurn / (double)turns));
            e.Graphics.FillRectangle(new SolidBrush(style.scoreBoardRestColor), 0, 0, restBarWidth, 42);
            string scoreMsg = currentTurn.ToString() + "/" + turns.ToString();
            e.Graphics.DrawString(scoreMsg, new Font("Impact", 20), new SolidBrush(style.scoreColor), new PointF(270, 3));
            e.Graphics.ResetTransform();

        }
    }
}
