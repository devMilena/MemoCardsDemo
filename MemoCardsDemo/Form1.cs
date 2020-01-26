using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MemoCardsDemo
{
    public partial class Form1 : Form
    {
        public List<int> ImageIds = new List<int>();
        public PictureBox[] picBoxes = new PictureBox[12];
        string firstCard = null;
        string secondCard = null;
        int firstCardId;
        int secondCardId;
        bool gamereset = false;
        int clicks = 0;

        int score = 0;
        int matchCount = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 12; i++)
            {
                picBoxes[i] = (PictureBox)this.Controls["pictureBox" + i.ToString()];
                picBoxes[i].Image = Properties.Resources.back;
                picBoxes[i].Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ImageIds.Clear();
            Random rand = new Random();

            while (ImageIds.Count < 12)
            {
                int id = rand.Next(0, 6);
                int idsCount = ImageIds.FindAll(i => i == id).Count;

                if (idsCount < 2)
                {
                    ImageIds.Add(id);
                }
            }

            for (int i = 0; i < 12; i++)
            {
                picBoxes[i].Image = Properties.Resources.back;
                picBoxes[i].Visible = true;
            }

            firstCard = null;
            secondCard = null;
            gamereset = false;
            score = 0;
            label2.Text = score.ToString();

            matchCount = 0;
            clicks = 0;
            label3.Text = clicks.ToString();
        }

        private void CardClick(object sender, EventArgs e)
        {
            clicks = clicks + 1;
            label3.Text = clicks.ToString();

            string sid = ((PictureBox)sender).Name.Remove(0, 10);
            int id = Convert.ToInt32(sid);

            picBoxes[id].Image = GetCardPicture(ImageIds[id]);

            if(String.IsNullOrEmpty(firstCard))
            {
                if(gamereset)
                {
                    picBoxes[firstCardId].Image = GetCardPicture(6);
                    picBoxes[secondCardId].Image = GetCardPicture(6);
                    secondCard = null;

                    gamereset = false;
                }

                firstCard = ImageIds[id].ToString();
                firstCardId = id;

            }
            else if (String.IsNullOrEmpty(secondCard))
            {
                secondCard = ImageIds[id].ToString();
                secondCardId = id;

                if (firstCard == secondCard)
                {
                    picBoxes[firstCardId].Visible = false;
                    picBoxes[secondCardId].Visible = false;
                    score = score + 100;
                    label2.Text = score.ToString();

                    matchCount = matchCount + 1;

                    //game over
                    if(matchCount == 6)
                    {
                        MessageBox.Show("You won! Your score is " + score.ToString());
                    }
                }

                firstCard = null;
                gamereset = true;
            }
        }
        
        private Bitmap GetCardPicture(int picId)
        {
            switch(picId)
            {
                case 0:
                    return Properties.Resources.Img0;
                case 1:
                    return Properties.Resources.Img1;
                case 2:
                    return Properties.Resources.Img2;
                case 3:
                    return Properties.Resources.Img3;
                case 4:
                    return Properties.Resources.Img4;
                case 5:
                    return Properties.Resources.Img5;
                case 6:
                    return Properties.Resources.back;
                default:
                    return Properties.Resources.back;
            }
        }
    }
}
