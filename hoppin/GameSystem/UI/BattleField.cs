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
        int[,] testFieldNum = new int[,]
        {
            {0, 0, 0, 0, 3, 3, 3, 3 },
            {0, 6, 7, 7, 7, 7, 7, 3 },
            {0, 6, 0, 4, 4, 3, 5, 3 },
            {0, 6, 4, 9, 9, 4, 5, 3 },
            {1, 6, 4, 9, 9, 4, 5, 2 },
            {1, 6, 1, 4, 4, 2, 5, 2 },
            {1, 8, 8, 8, 8, 8, 5, 2 },
            {1, 1, 1, 1, 2, 2, 2, 2 },
        };

        Style style = new Style();
        FieldBlock blankBlock = new FieldBlock();
        public void drawBlankField(PaintEventArgs e) {
            e.Graphics.TranslateTransform(41, 41);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    blankBlock.draw(e, j, i, (BlockType)testFieldNum[i, j]);
                }
            }
            e.Graphics.ResetTransform();
        }

    }
}
