using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 数独;

namespace 扫雷
{
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();
        }
        Map map;
        Label lCount;
        private void Game_Load(object sender, EventArgs e)
        {
            lCount = new Label();
            lCount.Left = 5;
            lCount.Top = this.Height - 50;
            SharedData.lCount = lCount;
            this.Controls.Add(lCount);
            map =new Map(SharedData.Width,SharedData.Height,SharedData.NumMine,this);

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Game
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Game";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "扫雷";
            this.Load += new System.EventHandler(this.Game_Load);
            this.ResumeLayout(false);

        }
    }
}
