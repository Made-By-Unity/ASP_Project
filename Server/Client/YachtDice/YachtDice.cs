using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class YachtDice : Form
    {
        public YachtDice()
        {
            InitializeComponent();
        }

        Image[] images = {
             Properties.Resources._1, Properties.Resources._2, Properties.Resources._3, Properties.Resources._4, Properties.Resources._5, Properties.Resources._6
        };

        private int nbuttonClickCount = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            //버튼 클릭 횟수 증가
            nbuttonClickCount++;

            //3번 누르면 비활성화
            if (nbuttonClickCount == 3)
            {
                button1.Enabled = false;
            }

            // 이미지 연속 출력 연출 위한 타이머
            Timer timer = new Timer();
            timer.Interval = 100;
            int ncount = 0;

            // 앞으로 Roll할 수 있는 횟수 표시
            RollDisplay.Text = (int.Parse(RollDisplay.Text) - 1).ToString();

            // 이미지 출력
            void Timer_Tick(object ss, EventArgs ee)
            {
                Random random = new Random();
                int index1 = random.Next(images.Length);
                int index2 = random.Next(images.Length);
                int index3 = random.Next(images.Length);
                int index4 = random.Next(images.Length);
                int index5 = random.Next(images.Length);
                pictureBox1.Image = images[index1];
                pictureBox2.Image = images[index2];
                pictureBox3.Image = images[index3];
                pictureBox4.Image = images[index4];
                pictureBox5.Image = images[index5];

                ncount++;

                if (ncount == 6)
                {
                    timer.Stop();
                }
            }
            timer.Tick += Timer_Tick;

            timer.Start();
        }

    }
}
