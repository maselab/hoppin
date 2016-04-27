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
        double stdscore = 50;
        bool active = false;

        public ScorePanel() { }
        public ScorePanel(String playerID, Color panelColor, String playerName, int score, double stdscore, bool active) {
            this.playerID = playerID;
            this.panelColor = panelColor;
            this.playerName = playerName;
            this.score = score;
            this.stdscore = stdscore;
            this.active = active;

        }
        public void draw(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(panelColor), 0, 0, 210, 82);
            e.Graphics.FillRectangle(new SolidBrush(style.scoreInactiveColor), (stdscore > 0)?(int)((stdscore / 100.0) * 210):0, 0, (stdscore > 0)?220:0, 82);
            //e.Graphics.FillRectangle(new SolidBrush(Color.Aqua), 0, 0, 82, 82);
            SolidBrush idColor = new SolidBrush((active) ?style.scoreColor:style.scoreInactiveColor);
            e.Graphics.DrawString(playerID, new Font("Impact", 60), idColor, new PointF(-3, -8));
            e.Graphics.DrawString(playerName, new Font("Impact", 12), new SolidBrush(style.scoreColor), new PointF(60, 7));
            e.Graphics.DrawString(String.Format("{0:00000}", score), new Font("Impact", 39), new SolidBrush(style.scoreColor), new PointF(52, 20));
        }
    }
}