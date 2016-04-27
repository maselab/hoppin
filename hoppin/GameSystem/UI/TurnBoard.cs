using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace hoppin.GameSystem.UI
{
    class TurnBoard
    {
        int currentTurn;
        int turns;
        Style style = new Style();
        private GameState gameState;
        public TurnBoard(GameState gameState)
        {
            this.gameState = gameState;
        }
        public void draw(PaintEventArgs e)
        {
            currentTurn = gameState.TurnNum;
            turns = gameState.MaxTurn;
            e.Graphics.TranslateTransform(0, style.windowHeight - 42);
            e.Graphics.FillRectangle(new SolidBrush(style.turnBoardColor), 0, 0, style.windowWidth, 42);
            int restBarWidth = (int)(style.windowWidth * (double)((double)currentTurn / (double)turns));
            e.Graphics.FillRectangle(new SolidBrush(style.turnBoardRestColor), 0, 0, restBarWidth, 42);
            string turnMsg = currentTurn.ToString() + "/" + turns.ToString();
            e.Graphics.DrawString(turnMsg, new Font("Impact", 20), new SolidBrush(style.turnColor), new PointF(270, 3));
            e.Graphics.ResetTransform();

        }
    }
}
