using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace hoppin.GameSystem.UI
{
    class BattleField
    {
        private GameState gameState;
        public BattleField(GameState gameState)
        {
            this.gameState = gameState;
        }
        int[,] testFieldNum = new int[,]
        {
            {1, 1, 1, 1, 2, 2, 2, 2 },
            {1, 0, 0, 1, 0, 0, 0, 2 },
            {1, 0, 0, 1, 0, 0, 0, 2 },
            {1, 0, 0, 1, 2, 2, 2, 2 },
            {4, 4, 4, 4, 3, 0, 0, 3 },
            {4, 0, 0, 0, 3, 0, 0, 3 },
            {4, 0, 0, 0, 3, 0, 0, 3 },
            {4, 4, 4, 4, 3, 3, 3, 3 }
        };
        int[,] testBlockNum = new int[,]
        {
            {0, 0, 0, 0, 7, 0, 0, 0 },
            {0, 0, 0, 0, 6, 0, 0, 0 },
            {0, 0, 0, 0, 5, 0, 0, 0 },
            {7, 6, 5, 1, 2, 0, 0, 0 },
            {0, 0, 0, 4, 3, 5, 6, 7 },
            {0, 0, 0, 5, 0, 0, 0, 0 },
            {0, 0, 0, 6, 0, 0, 0, 0 },
            {0, 0, 0, 7, 0, 0, 0, 0 },
        };


        Style style = new Style();
        FieldPanel fieldPanel = new FieldPanel();
        FieldBlock fieldBlock = new FieldBlock();

        FieldObject[,] fieldPanelMatrix = new FieldObject[8, 8];
        FieldObject[,] fieldObjectMatrix = new FieldObject[8, 8];

        public void drawBlankField(PaintEventArgs e) {
            e.Graphics.TranslateTransform(41, 41);
            fieldPanelMatrix = gameState.FieldFloorColor;
            fieldObjectMatrix = gameState.FieldState;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    fieldBlock.draw(e, j, i, fieldObjectMatrix[i, j]);
                }
            }
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    fieldPanel.draw(e, j, i, fieldPanelMatrix[i, j]);
                }
            }
            e.Graphics.ResetTransform();
        }

    }
}
