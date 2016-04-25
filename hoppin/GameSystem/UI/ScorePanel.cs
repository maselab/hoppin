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
        string playerID = "00";
        Color panelColor = new Color();
        String playerName = "Null Player";
        int score = 0;
        bool active = false;

        public ScorePanel() { }
        public ScorePanel(String playerID_, Color panelColor_, String playerName_, int score_, bool active_) {
            playerID = playerID_;
            panelColor = panelColor_;
            playerName = playerName_;
            score = score_;
            active = active_;

        }
        public void draw(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(panelColor), 0, 0, 210, 82);
            //e.Graphics.FillRectangle(new SolidBrush(Color.Aqua), 0, 0, 82, 82);
            SolidBrush idColor = new SolidBrush((active) ?style.scoreColor:style.scoreInactiveColor);
            e.Graphics.DrawString(playerID, new Font("Impact", 60), idColor, new PointF(-3, -8));
            e.Graphics.DrawString(playerName, new Font("Impact", 12), new SolidBrush(style.scoreColor), new PointF(60, 7));
            e.Graphics.DrawString(String.Format("{0:00000}", score), new Font("Impact", 39), new SolidBrush(style.scoreColor), new PointF(52, 20));
        }
    }
}