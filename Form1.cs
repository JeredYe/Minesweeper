using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace 扫雷
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
        void NRefresh()
        {
            lMine.Text = "雷数:" + (tDifficulty.Value * (tWidth.Value * tHeight.Value / 20)).ToString();
            label2.Text = "地图宽度" + tWidth.Value.ToString();
            label3.Text = "地图高度" + tHeight.Value.ToString();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            NRefresh();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tWidth_Scroll(object sender, EventArgs e)
        {
            NRefresh();
        }

        private void tHeight_Scroll(object sender, EventArgs e)
        {
            NRefresh();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SharedData.Init();
            SharedData.Width=tWidth.Value;
            SharedData.Height=tHeight.Value;
            SharedData.NumMine = tDifficulty.Value * (SharedData.Width * SharedData.Height / 20);
            Game game= new Game();
            SharedData.FormPtr = game;
            game.Width = SharedData.Width * 35;
            game.Height= SharedData.Height * 35;
            game.ShowDialog();
        }

        private void tDifficulty_Scroll(object sender, EventArgs e)
        {
            lMine.Text = "雷数:" + (tDifficulty.Value * (tWidth.Value * tHeight.Value / 20)).ToString();
        }

        private void lMine_Click(object sender, EventArgs e)
        {

        }

    }
}