﻿using System;
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
            {1, 1, 1, 1, 2, 2, 2, 2 },
            {1, 0, 0, 1, 13, 0, 0, 2 },
            {1, 0, 0, 1, 9, 0, 0, 2 },
            {1, 13, 12, 5, 6, 2, 2, 2 },
            {4, 4, 4, 8, 7, 10, 13, 3 },
            {4, 0, 0, 11, 3, 0, 0, 3 },
            {4, 0, 0, 13, 3, 0, 0, 3 },
            {4, 4, 4, 4, 3, 3, 3, 3 },
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
