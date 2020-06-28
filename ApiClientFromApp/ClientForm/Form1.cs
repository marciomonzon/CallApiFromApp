using DemoLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientForm
{
    public partial class Form1 : Form
    {
        private int maxNumber = 0;
        private int currentNumber = 0;

        public Form1()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();
            btnNext.Enabled = false;
            btnPrev.Enabled = false;
        }

        private async Task LoadImage(int imageNumber = 0)
        {
            var comic = await ComicProcessor.LoadComic(imageNumber);

            if (imageNumber == 0)
            {
                maxNumber = comic.Num;
            }

            currentNumber = comic.Num;

            if (currentNumber > 0)
            {
                btnPrev.Enabled = true;
            }

            var uriSource = new Uri(comic.Img, UriKind.Absolute);
            pictureBox1.Load(uriSource.AbsoluteUri);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await LoadImage();
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (currentNumber < maxNumber)
            {
                currentNumber += 1;
                btnPrev.Enabled = true;
                await LoadImage(currentNumber);

                if (currentNumber == maxNumber)
                {
                    btnNext.Enabled = false;
                }
            }
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentNumber > 1)
            {
                currentNumber -= 1;
                btnNext.Enabled = true;
                await LoadImage(currentNumber);

                if (currentNumber == 1)
                {
                    btnPrev.Enabled = false;
                }
            }
        }
    }
}
