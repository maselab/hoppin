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
        private GameManager gameManager;
        private int count;
        private delegate void GameProcessDelegate();

        public HoppinUI()
        {
            InitializeComponent();
        }

        public HoppinUI(GameManager gameManager)
        {
            InitializeComponent();
            this.gameManager = gameManager;


            Timer timer = new Timer();
            timer.Interval = 10;
            timer.Tick += new EventHandler(tick);
            timer.Start();

            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            GameProcessDelegate gameDelegate = new GameProcessDelegate(gameManager.ProcessGame);
            IAsyncResult result = gameDelegate.BeginInvoke(null, null);
            
        }
        private void tick(object sender, EventArgs e)
        {
            count++;
            if (count == 10000) count = 0;
            Invalidate();
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
            base.OnPaint(e);
            Background background = new Background();
            background.draw(e);

            BattleField battleField = new BattleField(gameManager.gameState);
            battleField.drawBlankField(e);

            TurnBoard turnBoard = new TurnBoard(gameManager.gameState);
            turnBoard.draw(e);

            ScoreBoard scoreBoard = new ScoreBoard(gameManager.gameState);
            scoreBoard.draw(e);
        }
    }
}
