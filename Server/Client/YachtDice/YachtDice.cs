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
        private int m_iRollCount = 3;
        int[] m_arrDices = new int[5];
        Image[] m_images = {
             Properties.Resources._1, Properties.Resources._2, Properties.Resources._3, Properties.Resources._4, Properties.Resources._5, Properties.Resources._6
        };

        public YachtDice()
        {
            InitializeComponent();
        }

        private void btnRoll_Click(object sender, EventArgs e)
        {
            //3번 누르면 비활성화
            if (0 >= --m_iRollCount)
            {
                btnRoll.Enabled = false;
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
                m_arrDices[0] = random.Next(m_images.Length);
                m_arrDices[1] = random.Next(m_images.Length);
                m_arrDices[2] = random.Next(m_images.Length);
                m_arrDices[3] = random.Next(m_images.Length);
                m_arrDices[4] = random.Next(m_images.Length);
                pbDice1.Image = m_images[m_arrDices[0]];
                pbDice2.Image = m_images[m_arrDices[1]];
                pbDice3.Image = m_images[m_arrDices[2]];
                pbDice4.Image = m_images[m_arrDices[3]];
                pbDice5.Image = m_images[m_arrDices[4]];

                ncount++;

                if (ncount == 6)
                {
                    timer.Stop();
                    UpdateScore();
                }
            }
            timer.Tick += Timer_Tick;

            timer.Start();
        }

        private void UpdateScore()
        {
            const int iDice = 6;

            for (int i = 0; i < 5; i++)
            {
                m_arrDices[i]++;
            }

            // 단숫 숫자 점수 갱신
            int[] arrNumCount = new int[6];
            int iTotal = 0;

            foreach (int num in m_arrDices)
            {
                if (1 == num)
                    arrNumCount[0]++;
                else if (2 == num)
                    arrNumCount[1]++;
                else if (3 == num)
                    arrNumCount[2]++;
                else if (4 == num)
                    arrNumCount[3]++;
                else if (5 == num)
                    arrNumCount[4]++;
                else if (6 == num)
                    arrNumCount[5]++;

                iTotal += num;
            }

            txtAcesScore.Text = (arrNumCount[0] * 1).ToString();
            txtDeucesScore.Text = (arrNumCount[1] * 2).ToString();
            txtThreesScore.Text = (arrNumCount[2] * 3).ToString();
            txtFoursScore.Text = (arrNumCount[3] * 4).ToString();
            txtFivesScore.Text = (arrNumCount[4] * 5).ToString();
            txtSixesScore.Text = (arrNumCount[5] * 6).ToString();

            // 족보 점수 갱신

            // Choice
            txtChoiceScore.Text = iTotal.ToString();

            // 4 of a Kind
            for (int i = 0; i < iDice; i++)
            {
                if (4 <= arrNumCount[i])
                    txt4KindScore.Text = iTotal.ToString();
            }

            txt4KindScore.Text = Convert.ToString(0);

            // Full House
            int iThreeDice = -1, iTwoDice = -1;

            for (int i = 0; i < iDice; i++)
            {
                if(3 == arrNumCount[i])
                    iThreeDice = i;
            }

            if(0 <= iThreeDice)
            {
                for (int i = 0; i < iDice; i++)
                {
                    if(2 == arrNumCount[i])
                        iTwoDice = i;
                }

                if (0 <= iTwoDice)
                    txtFHScore.Text = iTotal.ToString();
                else
                    txtFHScore.Text = Convert.ToString(0);
            }
            else
            {
                txtFHScore.Text = Convert.ToString(0);
            }

            // Small Straight
            txtSSScore.Text = Convert.ToString(0);
            int iSmallOne = -1;

            for (int i = 0; i < iDice; i++)
            {
                if (1 == arrNumCount[i])
                {
                    iSmallOne = i;
                    break;
                }
            }

            int iStraightCount = 1;
            if(0 <= iSmallOne)
            {
                for (int i = iSmallOne + 1; i < iDice; i++)
                {
                    if (1 == arrNumCount[i])
                        iStraightCount++;
                }
            }

            if(4 <= iStraightCount)
                txtSSScore.Text = Convert.ToString(15);

            // Large Straight
            txtLSScore.Text = Convert.ToString(0);

            if (5 == iStraightCount)
                txtLSScore.Text = Convert.ToString(30);

            // Yacht
            txtYachtScore.Text = Convert.ToString(0);
            foreach (int num in arrNumCount)
            {
                if (5 == num)
                {
                    txtYachtScore.Text = Convert.ToString(50);
                    break;
                }
            }
        }
    }
}
