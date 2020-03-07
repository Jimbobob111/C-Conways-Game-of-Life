using System;
using System.Drawing;
using System.Windows.Forms;

namespace ConwaysGameOfLife
{
    public partial class Form1 : Form
    {
        int iterations = 0;
        Random r = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int[,] oldImgArr = CreateOldImgArray();
            int[,] newImgArr = new int[oldImgArr.GetLength(0), oldImgArr.GetLength(1)];
            int numAdjacent;
            for (int x = 0; x < oldImgArr.GetLength(0); x++)
            {
                for (int y = 0; y < oldImgArr.GetLength(1); y++)
                {
                    numAdjacent = getNumberAjacent(oldImgArr, x, y);
                    if(numAdjacent==3&&oldImgArr[x,y]==0)
                    {
                        newImgArr[x, y] = 1;
                    }
                    if(numAdjacent<=1&&oldImgArr[x,y]==1)
                    {
                        newImgArr[x, y] = 0;
                    }
                    if(numAdjacent>=4&&oldImgArr[x,y]==1)
                    {
                        newImgArr[x, y] = 0;
                    }
                    if((numAdjacent== 2|| numAdjacent == 3) && oldImgArr[x, y] == 1)
                    {
                        newImgArr[x, y] = 1;
                    }
                }
            }
            pictureBox1.Image = CreateNewImg(newImgArr);
            iterations++;
            label2.Text = "Iterations: " + iterations;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Text = "Iterations: " + iterations;
            timerSetIntBox.Value = timer1.Interval;
            Bitmap img = new Bitmap(200, 200);
            pictureBox1.Image = img;
            for (int x=0;x<img.Width;x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    int num = r.Next(0, 100);
                    if(num<=25)
                    {
                        img.SetPixel(x, y, Color.Black);
                    }
                    else
                    {
                        img.SetPixel(x, y, Color.White);
                    }
                    
                }
            }
            checkBox1.Checked = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = checkBox1.Checked;
        }

        private Bitmap CreateNewImg(int[,] array)
        {
            Bitmap img = new Bitmap(array.GetLength(0), array.GetLength(1));
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    if (array[x, y]==1)
                    {
                        img.SetPixel(x, y, Color.Black);
                    }
                    else
                    {
                        img.SetPixel(x, y, Color.White);
                    }

                }
            }
            return img;
        }

        private int[,] CreateOldImgArray()
        {
            Bitmap img = new Bitmap(pictureBox1.Image);
            int[,] array = new int[img.Width, img.Height];
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    Color pixel = img.GetPixel(x, y);
                    if (pixel.ToArgb().Equals(Color.Black.ToArgb()))
                    {
                        array[x, y] = 1;
                    }
                    else
                    {
                        array[x, y] = 0;
                    }
                    
                }
            }
            return array;
        }

        private int getNumberAjacent(int[,] array,int x, int y)
        {
            int ajacentAlive=0;
            if(x!=0)
            {
                if(array[x-1,y]==1)
                {
                    ajacentAlive++;
                }
                if (y + 1 < array.GetLength(1))
                {
                    if (array[x - 1, y + 1] == 1)
                    {
                        ajacentAlive++;
                    }
                }
            }
            if (y != 0)
            {
                if (array[x, y-1] == 1)
                {
                    ajacentAlive++;
                }
                if (x + 1 < array.GetLength(0))
                {
                    if (array[x + 1, y - 1] == 1)
                    {
                        ajacentAlive++;
                    }
                }
            }
            if(x!=0&&y!=0)
            {
                if (array[x - 1, y - 1] == 1)
                {
                    ajacentAlive++;
                }
            }
            if(y+1<array.GetLength(1))
            {
                if (array[x, y + 1] == 1)
                {
                    ajacentAlive++;
                }
                if (x + 1 < array.GetLength(0))
                {
                    if (array[x + 1, y+1] == 1)
                    {
                        ajacentAlive++;
                    }
                }
            }
            if(x+1<array.GetLength(0))
            {
                if (array[x + 1, y] == 1)
                {
                    ajacentAlive++;
                }
            }
            return ajacentAlive;
        }

        private void timerSetIntBox_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = Convert.ToInt32(timerSetIntBox.Value);
        }
    }
}
