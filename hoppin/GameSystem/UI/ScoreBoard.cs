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
            e.Graphics.TranslateTransform(420, 43);
            ScorePanel scorePanelA = new ScorePanel("A", style.playerAColor, "Sample Player A", 0, true);
            scorePanelA.draw(e);
            e.Graphics.TranslateTransform(0, 84);
            ScorePanel scorePanelB = new ScorePanel("B", style.playerBColor, "Sample Player B", 1, false);
            scorePanelB.draw(e);
            e.Graphics.TranslateTransform(0, 84);
            ScorePanel scorePanelC = new ScorePanel("C", style.playerCColor, "Sample Player C", 10, false);
            scorePanelC.draw(e);
            e.Graphics.TranslateTransform(0, 84);
            ScorePanel scorePanelD = new ScorePanel("D", style.playerDColor, "Sample Player D", 100, false);
            scorePanelD.draw(e);
            e.Graphics.ResetTransform();
        }
    }
}
