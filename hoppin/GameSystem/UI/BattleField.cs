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
        Style style = new Style();
        FieldBlock blankBlock = new FieldBlock();
        public void drawBlankField(PaintEventArgs e) {
            e.Graphics.TranslateTransform(41, 41);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    blankBlock.draw(e, j, i);
                }
            }
            e.Graphics.ResetTransform();
        }

    }
}
