using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 数独;
using static System.Windows.Forms.DataFormats;

namespace 扫雷
{
    internal class Map
    {
        int[,] map;
        int width,height;
        int numMine;
        const int flag = 9;
        int marked = 0;
        Tile[,] Tiles;
        void initializeMap(int bomb,int sX,int sY)
        {
            Random random= new Random(DateTime.Now.Millisecond);
            while (bomb > 0)
            {
                int x = random.Next(1, width);
                int y = random.Next(1, height);
                if (map[y, x] != flag && (x != sX ||y != sY))
                {
                    map[y, x] = flag;
                    bomb--;
                }

            }
            for(int y = 1; y <= height; y++)
            {
                for(int x = 1; x <= width; x++)
                {
                    if (map[y, x] == flag)
                    {
                        for (int Y = y - 1; Y <= y + 1; Y++)
                        {
                            for (int X = x - 1; X <= x + 1; X++)
                            {
                                if (map[Y, X] != flag) map[Y, X]++;
                            }
                        }
                    }

                }
            }
            for (int i = 0; i < width; i++)
            {
                for (int k = 0; k < height; k++)
                {
                    Tiles[k, i].num = map[k, i];
                }
            }
            //string src = "";
            //for (int i = 1; i <= height; i++)
            //{
            //    for (int j = 1; j <= width; j++)
            //    {
            //        src += Convert.ToString(Pos(i, j)) + "\t";
            //    }
            //    src += "\n";
            //}
            //MessageBox.Show(src);

        }
        void initializeTiles(Form formPtr)
        {
            Tiles=new Tile[height, width];
            for (int i = 0; i < width; i++)
            {
                for (int k = 0; k < height; k++)
                {
                    Tiles[k, i] = new Tile(map[k, i], i, k, formPtr.Width, formPtr.Height, width, height, new MouseEventHandler(ClickEvent), new MouseEventHandler(UpEvent));
                    formPtr.Controls.Add(this.Tiles[k, i].button);
                    this.Tiles[k, i].button.BringToFront();
                }
            }
            

        }
        public Map(int width, int height, int bomb, Form formPtr)
        {
            this.width = width;
            this.height = height;
            numMine = bomb;
            map = new int[height + 2, width + 2];
            Tiles = null;
            //initializeMap(bomb);
            initializeTiles(formPtr);
            SharedData.FormPtr.Height += 20;
            SharedData.lCount.Text = "剩余雷数:" + numMine.ToString();
        }
        public int Pos(int y,int x) { return map[y, x]; }
        void Search(int y,int x)
        {
            if (Tiles[y, x].Tapped)return;
            if (Tiles[y, x].num ==0)
            {
                Tiles[y, x].button.Text = "";
                Tiles[y, x].Tapped = true;
                Tiles[y, x].button.Enabled = false;
                if (y > 0) Search(y - 1, x);
                if (x > 0) Search(y, x - 1);
                if (x < width - 1) Search(y, x + 1);
                if (y < height - 1) Search(y + 1, x);
            }
            else if(Tiles[y, x].num != 9)
            {
                Tiles[y, x].button.Text = Tiles[y, x].num.ToString();
                Tiles[y, x].button.ForeColor = System.Drawing.Color.FromArgb(100 - 10 * Tiles[y, x].num, 100 - 10 * Tiles[y, x].num, 100 - 10 * Tiles[y, x].num);
                Tiles[y, x].Tapped = true;
            }
            Tiles[y, x].status = 1;
        }
        bool Won = false;
        bool JudgeWin()
        {
            
            if (Won) return false;
            if (marked != numMine) return false;
            else
            {
                for (int i = 0; i < width; i++)
                {
                    for (int k = 0; k < height; k++)
                    {
                        if (Tiles[k, i].status == 0) return false;
                        if(Tiles[k, i].status == 2)
                        {
                            if(Tiles[k, i].num!=9)return false;
                        }
                    }
                }
            }
            Won = true;
            return true;
        }
        bool JudgeMarked(Tile tile)
        {
            int target = tile.num, sum = 0;
            bool lX = tile.x > 0, lY = tile.y > 0, rX = tile.x < width - 1, rY = tile.y < height - 1;
            if (lX && Tiles[tile.y, tile.x - 1].status == 2) sum++;
            if (rX && Tiles[tile.y, tile.x + 1].status == 2) sum++;
            if (lY && Tiles[tile.y - 1, tile.x].status == 2) sum++;
            if (rY && Tiles[tile.y + 1, tile.x].status == 2) sum++;
            if (lX && lY && Tiles[tile.y - 1, tile.x - 1].status == 2) sum++;
            if (rX && rY && Tiles[tile.y + 1, tile.x + 1].status == 2) sum++;
            if (lX && rY && Tiles[tile.y + 1, tile.x - 1].status == 2) sum++;
            if (rX && lY && Tiles[tile.y - 1, tile.x + 1].status == 2) sum++;
            return sum == target;
        }
        private void UpEvent(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            Tile tile = (Tile)button.Tag;
            if (JudgeMarked(tile))
                if (Rt && e.Button == MouseButtons.Left)
                {
                    MouseEventArgs E = new MouseEventArgs(MouseButtons.Left, 1, e.X, e.Y, e.Delta);
                    bool lX = tile.x > 0, lY = tile.y > 0, rX = tile.x < width - 1, rY = tile.y < height - 1;
                    if (lX) Tiles[tile.y, tile.x - 1].downEvent(Tiles[tile.y, tile.x - 1].button, E);
                    if (rX) Tiles[tile.y, tile.x + 1].downEvent(Tiles[tile.y, tile.x + 1].button, E);
                    if (lY) Tiles[tile.y - 1, tile.x].downEvent(Tiles[tile.y - 1, tile.x].button, E);
                    if (rY) Tiles[tile.y + 1, tile.x].downEvent(Tiles[tile.y + 1, tile.x].button, E);
                    if (lX && lY) Tiles[tile.y - 1, tile.x - 1].downEvent(Tiles[tile.y - 1, tile.x - 1].button, E);
                    if (rX && rY) Tiles[tile.y + 1, tile.x + 1].downEvent(Tiles[tile.y + 1, tile.x + 1].button, E);
                    if (lX && rY) Tiles[tile.y + 1, tile.x - 1].downEvent(Tiles[tile.y + 1, tile.x - 1].button, E);
                    if (rX && lY) Tiles[tile.y - 1, tile.x + 1].downEvent(Tiles[tile.y - 1, tile.x + 1].button, E);
                }

        }
        public bool Lt = false, Rt = false;
        bool First = true;
        private void ClickEvent(object sender, MouseEventArgs e)
        {

            Button button = (Button)sender;
            Tile tile=(Tile)button.Tag;
            Lt = false;
            Rt = false;
            if (First)
            {
                First = false;
                initializeMap(numMine, tile.x, tile.y);
            }
            if (e.Button == MouseButtons.Left)Lt=true;
            if (e.Button == MouseButtons.Right) Rt = true;
            if (e.Button == MouseButtons.Left)
            {
                if(tile.status == 0)
                {
                    if (tile.num == 9)
                    {
                        button.Text = "💣";
                        MessageBox.Show("你输了！", "提示", MessageBoxButtons.OK);
                        for (int i = 0; i < width; i++)
                        {
                            for (int k = 0; k < height; k++)
                            {
                                Tiles[k, i].button.Text = Tiles[k, i].num.ToString();
                                if (Tiles[k, i].button.Text == "9") Tiles[k, i].button.Text = "💣";
                                if (Tiles[k, i].button.Text == "0") Tiles[k, i].button.Text = "";
                                Tiles[k, i].button.Enabled=false;
                            }
                        }
                    }
                    else
                    {
                        Search(tile.y, tile.x);
                    }
                }
            }
            else
            {
                if (tile.status == 0)
                {
                    tile.status = 2;
                    button.ForeColor= System.Drawing.Color.Red;
                    button.Text = "🚩";
                    marked++;
                }
                else if (tile.status == 2)
                {
                    tile.status = 0;
                    button.ForeColor = System.Drawing.Color.Black;
                    button.Text = "";
                    marked--;
                }
                SharedData.lCount.Text = "剩余雷数:" + (numMine - marked).ToString();
            }
            if (JudgeWin())
            {
                MessageBox.Show("恭喜！已通关","提示",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
