using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace hoppin.GameSystem.UI
{
    class ScoreBoard
    {
        Style style = new Style();
        public void draw(PaintEventArgs e)
        {
            ScorePanel scorePanel = new ScorePanel();
            e.Graphics.TranslateTransform(420, 43);
            scorePanel.draw(e, style.playerAColor);
            e.Graphics.TranslateTransform(0, 84);
            scorePanel.draw(e, style.playerBColor);
            e.Graphics.TranslateTransform(0, 84);
            scorePanel.draw(e, style.playerCColor);
            e.Graphics.TranslateTransform(0, 84);
            scorePanel.draw(e, style.playerDColor);
            e.Graphics.ResetTransform();
        }
    }
}
