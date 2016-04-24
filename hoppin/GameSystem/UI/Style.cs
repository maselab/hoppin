using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace hoppin.GameSystem.UI
{
    class Style
    {
        public int windowWidth = 630;
        public int windowHeight = 462;

        public Color backgroundColor = Color.White;
        public Color separationColor = Color.FromArgb(255, 179, 179, 179);

        public Color turnBoardColor = Color.FromArgb(255, 26, 26, 26);
        public Color turnBoardRestColor = Color.FromArgb(255, 51, 51, 51);
        public Color turnColor = Color.FromArgb(255, 204, 204, 204);

        public Color scoreInactiveColor = Color.FromArgb(127, 255, 255, 255);
        public Color scoreColor = Color.White;

        public Color playerAColor = Color.FromArgb(255, 0, 146, 80);
        public Color playerBColor = Color.FromArgb(255, 237, 173, 11);
        public Color playerCColor = Color.FromArgb(255, 191, 30, 86);
        public Color playerDColor = Color.FromArgb(255, 0, 134, 171);

    
    }
}
