namespace 数独
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using 扫雷;
    public delegate void DownEvent(object sender, MouseEventArgs e);
    public class Tile
    {
        public Button button = new Button();
        public int FilledNum;
        public int num;
        /// <summary>
        /// 0未点开
        /// 1已点开
        /// 2已标记
        /// </summary>
        public int status;
        public int x;
        public int y;
        public bool Tapped;
        public DownEvent downEvent;
        public Tile(int num, int x, int y, int Width, int Height, int mapWidth, int mapHeight, MouseEventHandler Down, MouseEventHandler Up)
        {
            this.num = num;
            Tapped= false;
            this.button.Tag = this;
            this.button.MouseDown += Down;
            this.button.MouseUp += Up;
            downEvent = new DownEvent(Down);
            this.x = x;
            this.y = y;
            this.Resize( mapWidth, mapHeight);
        }



        public void Initialize()
        {
            this.button.Text = "";
            this.button.Enabled = true;
            this.status = 0;
        }

        public void Resize(int mapWidth,int mapHeight)
        {
            int Width = mapWidth * 30, Height = mapHeight * 30;
            this.button.Width = Width * 5 / 7 / (2 + mapWidth) * 5 / 3;
            this.button.Height = Height * 5 / 7 / (2 + mapHeight) * 5 / 3;
            this.button.Left = (Width / 500) + ((this.x) * (button.Width + 1)) ;
            this.button.Top = (Height / 500) + ((this.y) * (button.Height + 1)) ;
            this.button.Font = new Font("微软雅黑", (float) (this.button.Width/3), FontStyle.Bold);
        }
    }
}

