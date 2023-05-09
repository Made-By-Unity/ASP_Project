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
    enum YACTH_MODE
    {
        SINGLE,
        MULTI
    }

    public partial class YachtDice : Form
    {
        private YACTH_MODE m_eMode = YACTH_MODE.SINGLE;

        private int m_iRollCount = 3;
        private bool m_bClientTurn = false;

        private int m_iRound = 1;

        int[] m_arrDices = new int[5];
        Image[] m_images = {
             Properties.Resources._1, Properties.Resources._2, Properties.Resources._3, Properties.Resources._4, Properties.Resources._5, Properties.Resources._6
        };

        public YachtDice()
        {
            InitializeComponent();
            Reset();
        }

        // 플레이 턴이 들어왔을 때 초기상태로 되돌림
        public void Reset()
        {
            m_iRollCount = 3;
            btnRoll.Enabled = true;

            cbDice1.Enabled = false;
            cbDice2.Enabled = false;
            cbDice3.Enabled = false;
            cbDice4.Enabled = false;
            cbDice5.Enabled = false;

            cbDice1.Checked = false;
            cbDice2.Checked = false;
            cbDice3.Checked = false;
            cbDice4.Checked = false;
            cbDice5.Checked = false;

            pbDice1.Image = null;
            pbDice2.Image = null;
            pbDice3.Image = null;
            pbDice4.Image = null;
            pbDice5.Image = null;
        }

        private void TurnEnd()
        {
            btnRoll.Enabled = false;
            m_iRollCount = 3;
            RollDisplay.Text = m_iRollCount.ToString();

            // Score Text 클리어
            ClearText(txtAcesScore);
            ClearText(txtDeucesScore);
            ClearText(txtThreesScore);
            ClearText(txtFoursScore);
            ClearText(txtFivesScore);
            ClearText(txtSixesScore);

            ClearText(txtChoiceScore);
            ClearText(txt4KindScore);
            ClearText(txtFHScore);
            ClearText(txtSSScore);
            ClearText(txtLSScore);
            ClearText(txtYachtScore);

            // SubScore 갱신
            int iSubScore = TextToScore(txtAcesScore) + TextToScore(txtDeucesScore) + TextToScore(txtThreesScore) 
                            + TextToScore(txtFoursScore) + TextToScore(txtFivesScore) + TextToScore(txtSixesScore);

            txtSubtotalScore.Text = iSubScore.ToString() + " / 63";

            if (63 <= iSubScore)
                txtBonusAble.Text = "v";

            // TotalScore 갱신
            int iTotalScore = iSubScore + TextToScore(txtChoiceScore) + TextToScore(txt4KindScore) + TextToScore(txtFHScore)
                                + TextToScore(txtSSScore) + TextToScore(txtLSScore) + TextToScore(txtYachtScore);

            txtTotalScore.Text = iTotalScore.ToString();

            if (YACTH_MODE.SINGLE == m_eMode)
            {
                Reset();
                UpdateRound();
            }
        }

        public void UpdateRound()
        {
            m_iRound++;
            lbRoundDisplay.Text = m_iRound.ToString();
        }

        private void ClearText(TextBox _TextBox)
        {
            if (true == _TextBox.Enabled)
                _TextBox.Text = null;
        }

        private int TextToScore(TextBox _TextBox)
        {
            if (false == _TextBox.Enabled)
                return Int32.Parse(_TextBox.Text);

            return 0;
        }

        // 주사위를 굴리는 도중 입력 방지
        private void LockRolling()
        {
            btnRoll.Enabled = false;

            cbDice1.Enabled = false;
            cbDice2.Enabled = false;
            cbDice3.Enabled = false;
            cbDice4.Enabled = false;
            cbDice5.Enabled = false;
        }

        // 주사위가 굴려진후 입력 방지 해제
        private void UnlockRolling()
        {
            //3번 누르지 않았으면 활성화
            if (0 < --m_iRollCount)
            {
                btnRoll.Enabled = true;
            }
            RollDisplay.Text = m_iRollCount.ToString();

            // 앞으로 Roll할 수 있는 횟수 표시

            cbDice1.Enabled = true;
            cbDice2.Enabled = true;
            cbDice3.Enabled = true;
            cbDice4.Enabled = true;
            cbDice5.Enabled = true;
        }

        private void btnRoll_Click(object sender, EventArgs e)
        {
            LockRolling();

            // 이미지 연속 출력 연출 위한 타이머
            Timer timer = new Timer();
            timer.Interval = 100;
            int ncount = 0;

            // 이미지 출력
            void Timer_Tick(object ss, EventArgs ee)
            {
                Random random = new Random(DateTime.Now.Millisecond);
                
                if(false == cbDice1.Checked)
                {
                    m_arrDices[0] = random.Next(1, 7);
                    pbDice1.Image = m_images[m_arrDices[0] - 1];
                }
                if (false == cbDice2.Checked)
                {
                    m_arrDices[1] = random.Next(1, 7);
                    pbDice2.Image = m_images[m_arrDices[1] - 1];
                }
                if (false == cbDice3.Checked)
                {
                    m_arrDices[2] = random.Next(1, 7);
                    pbDice3.Image = m_images[m_arrDices[2] - 1];
                }
                if (false == cbDice4.Checked)
                {
                    m_arrDices[3] = random.Next(1, 7);
                    pbDice4.Image = m_images[m_arrDices[3] - 1];
                }
                if (false == cbDice5.Checked)
                {
                    m_arrDices[4] = random.Next(1, 7);
                    pbDice5.Image = m_images[m_arrDices[4] - 1];
                }

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

        private void UpdateScoreText(TextBox _TextBox, string _strText)
        {
            if (false == _TextBox.Enabled)
                return;

            _TextBox.Text = _strText;
        }

        private void UpdateScore()
        {
            UnlockRolling();
            const int iDice = 6;

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

            UpdateScoreText(txtAcesScore, (arrNumCount[0] * 1).ToString());
            UpdateScoreText(txtDeucesScore, (arrNumCount[1] * 2).ToString());
            UpdateScoreText(txtThreesScore, (arrNumCount[2] * 3).ToString());
            UpdateScoreText(txtFoursScore, (arrNumCount[3] * 4).ToString());
            UpdateScoreText(txtFivesScore, (arrNumCount[4] * 5).ToString());
            UpdateScoreText(txtSixesScore, (arrNumCount[5] * 6).ToString());

            // 족보 점수 갱신

            // Choice
            UpdateScoreText(txtChoiceScore, iTotal.ToString());

            UpdateScoreText(txt4KindScore, Convert.ToString(0));

            // 4 of a Kind
            for (int i = 0; i < iDice; i++)
            {
                if (4 <= arrNumCount[i])
                    UpdateScoreText(txt4KindScore, iTotal.ToString());
            }

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
                    UpdateScoreText(txtFHScore, iTotal.ToString());
                else
                    UpdateScoreText(txtFHScore, Convert.ToString(0));
            }
            else
            {
                UpdateScoreText(txtFHScore, Convert.ToString(0));
            }

            // Small Straight
            UpdateScoreText(txtSSScore, Convert.ToString(0));
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
                UpdateScoreText(txtSSScore, Convert.ToString(15));

            // Large Straight
            UpdateScoreText(txtLSScore, Convert.ToString(0));

            if (5 == iStraightCount)
                UpdateScoreText(txtLSScore, Convert.ToString(30));

            // Yacht
            UpdateScoreText(txtYachtScore, Convert.ToString(0));
            foreach (int num in arrNumCount)
            {
                if (5 == num)
                {
                    UpdateScoreText(txtYachtScore, Convert.ToString(50));
                    break;
                }
            }
        }

        private void Score_DoubleClick(object sender, EventArgs e)
        {
            TextBox tbScore = (TextBox)sender;

            if (false == tbScore.Enabled || string.Empty == tbScore.Text)
                return;

            if(false == btnRoll.Enabled)
            {
                if (0 < m_iRollCount)
                    return;
            }

            tbScore.Enabled = false;
            TurnEnd();
        }
    }
}
