using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using hoppin.GameSystem.UI;
using hoppin.GameSystem;


namespace hoppin
{
    public partial class HoppinUI : Form
    {
        private GameState gameState;

        public HoppinUI()
        {
            InitializeComponent();
        }

        private HoppinUI(GameState gameState)
        {
            InitializeComponent();
            this.gameState = gameState;
        }

        private void HoppinUI_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 描画インタフェース
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Background background = new Background();
            background.draw(e);

            BattleField battleField = new BattleField();
            battleField.drawBlankField(e);

            TurnBoard turnBoard = new TurnBoard();
            turnBoard.draw(e);

            ScoreBoard scoreBoard = new ScoreBoard();
            scoreBoard.draw(e);
        }
    }
}
