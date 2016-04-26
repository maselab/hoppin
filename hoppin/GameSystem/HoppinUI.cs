﻿using System;
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
        private int count;

        public HoppinUI()
        {
            InitializeComponent();
        }

        public HoppinUI(GameState gameState)
        {
            InitializeComponent();
            this.gameState = gameState;

            Timer timer = new Timer();
            timer.Interval = 10;
            timer.Tick += new EventHandler(tick);
            timer.Start();

            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
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

            BattleField battleField = new BattleField(gameState);
            battleField.drawBlankField(e);

            TurnBoard turnBoard = new TurnBoard();
            turnBoard.draw(e);

            ScoreBoard scoreBoard = new ScoreBoard();
            scoreBoard.draw(e);
        }
    }
}
