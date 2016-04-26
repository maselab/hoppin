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
        private GameState gameState;
        public ScoreBoard(GameState gameState)
        {
            this.gameState = gameState;
        }
        public void draw(PaintEventArgs e)
        {
            List<int> playerScores = gameState.GetPlayerScore();
            List<string> playerNames = gameState.playerName;
            

            e.Graphics.TranslateTransform(420, 43);
            ScorePanel scorePanelA = new ScorePanel("A", style.playerAColor, playerNames[0], playerScores[0], (gameState.CurrentPlayer == FieldObject.PLAYER1) ? true : false);
            scorePanelA.draw(e);
            e.Graphics.TranslateTransform(0, 84);
            ScorePanel scorePanelB = new ScorePanel("B", style.playerBColor, playerNames[1], playerScores[1], (gameState.CurrentPlayer == FieldObject.PLAYER2) ? true : false);
            scorePanelB.draw(e);
            e.Graphics.TranslateTransform(0, 84);
            ScorePanel scorePanelC = new ScorePanel("C", style.playerCColor, playerNames[2], playerScores[2], (gameState.CurrentPlayer == FieldObject.PLAYER3) ? true : false);
            scorePanelC.draw(e);
            e.Graphics.TranslateTransform(0, 84);
            ScorePanel scorePanelD = new ScorePanel("D", style.playerDColor, playerNames[3], playerScores[3], (gameState.CurrentPlayer == FieldObject.PLAYER4) ? true : false);
            scorePanelD.draw(e);
            e.Graphics.ResetTransform();
        }
    }
}
