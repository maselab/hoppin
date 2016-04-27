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
            int[] playerScores = gameState.GetPlayerScore().ToArray();
            List<string> playerNames = gameState.playerName;

            double sum = 0;
            foreach(int i in playerScores)
            {
                sum += i;
            }
            double mean = sum / playerScores.Length;
            sum = 0;
            foreach(int i in playerScores)
            {
                sum += Math.Pow(i - mean, 2);
            }
            double variance = sum / playerScores.Length;
            double stddev = Math.Sqrt(variance);
            double[] stdscore = new double[]
            {
                (10*(playerScores[0] - mean) / stddev) + 50,
                (10*(playerScores[1] - mean) / stddev) + 50,
                (10*(playerScores[2] - mean) / stddev) + 50,
                (10*(playerScores[3] - mean) / stddev) + 50
            };

            e.Graphics.TranslateTransform(420, 43);
            ScorePanel scorePanelA = new ScorePanel("A", style.playerAColor, playerNames[0], playerScores[0], stdscore[0], (gameState.CurrentPlayer == FieldObject.PlayerA) ? true : false);
            scorePanelA.draw(e);
            e.Graphics.TranslateTransform(0, 84);
            ScorePanel scorePanelB = new ScorePanel("B", style.playerBColor, playerNames[1], playerScores[1], stdscore[1], (gameState.CurrentPlayer == FieldObject.PlayerB) ? true : false);
            scorePanelB.draw(e);
            e.Graphics.TranslateTransform(0, 84);
            ScorePanel scorePanelC = new ScorePanel("C", style.playerCColor, playerNames[2], playerScores[2], stdscore[2], (gameState.CurrentPlayer == FieldObject.PlayerC) ? true : false);
            scorePanelC.draw(e);
            e.Graphics.TranslateTransform(0, 84);
            ScorePanel scorePanelD = new ScorePanel("D", style.playerDColor, playerNames[3], playerScores[3], stdscore[3], (gameState.CurrentPlayer == FieldObject.PlayerD) ? true : false);
            scorePanelD.draw(e);
            e.Graphics.ResetTransform();
        }
    }
}
