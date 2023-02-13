using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 数独;
using System.Windows.Forms;
namespace 扫雷
{
    static class SharedData
    {
        public static int Width, Height;
        public static int NumMine;
        public static Tile[,] tilesPtr;
        public static Form FormPtr;
        public static Label lCount;
        public static void Init()
        {
            Width=Height=NumMine=0;
            tilesPtr = null;
            FormPtr = null;
            lCount = null;
        }
    }
}
