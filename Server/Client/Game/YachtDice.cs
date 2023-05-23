using Client.Manager;
using Client.Room;
using Packetdll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    public partial class YachtDice : Form
    {
        Lobby m_fLobby;
        Thread m_tHandler = null;

        private int m_iRollCount = 3;
        private int m_iRound = 1;
        private int m_iRollRandomCount = 0;

        private int m_iCurrPlayerID = 0;

        int[] m_arrDices = new int[5];
        Image[] m_images = {
             Properties.Resources._1, Properties.Resources._2, Properties.Resources._3, Properties.Resources._4, Properties.Resources._5, Properties.Resources._6
        };

        TextBox[] m_P1Scores;
        TextBox[] m_P2Scores;
        TextBox[] m_P3Scores;
        TextBox[] m_P4Scores;

        public Lobby Lobby { 
            get { return m_fLobby; } 
            set { m_fLobby = value; }
        }
   
        public YachtDice()
        {
            InitializeComponent();

            m_tHandler = new Thread(GetPacket);
            m_tHandler.Start();

            // 점수판 관리를 편하게 하기 위해 각 플레이어 점수판마다 하나씩 작성
            m_P1Scores = new TextBox[16] { txtPlayer1,
                                           txtAcesScore1, 
                                           txtDeucesScore1,
                                           txtThreesScore1, 
                                           txtFoursScore1, 
                                           txtFivesScore1,
                                           txtSixesScore1, 
                                           txtSubtotalScore1, 
                                           txtBonusAble1, 
                                           txtChoiceScore1, 
                                           txt4KindScore1,
                                           txtFHScore1, 
                                           txtSSScore1, 
                                           txtLSScore1, 
                                           txtYachtScore1, 
                                           txtTotalScore1};

            m_P2Scores = new TextBox[16] { txtPlayer2,
                                           txtAcesScore2,
                                           txtDeucesScore2,
                                           txtThreesScore2,
                                           txtFoursScore2,
                                           txtFivesScore2,
                                           txtSixesScore2,
                                           txtSubtotalScore2,
                                           txtBonusAble2,
                                           txtChoiceScore2,
                                           txt4KindScore2,
                                           txtFHScore2,
                                           txtSSScore2,
                                           txtLSScore2,
                                           txtYachtScore2,
                                           txtTotalScore2};

            m_P3Scores = new TextBox[16] { txtPlayer3,
                                           txtAcesScore3,
                                           txtDeucesScore3,
                                           txtThreesScore3,
                                           txtFoursScore3,
                                           txtFivesScore3,
                                           txtSixesScore3,
                                           txtSubtotalScore3,
                                           txtBonusAble3,
                                           txtChoiceScore3,
                                           txt4KindScore3,
                                           txtFHScore3,
                                           txtSSScore3,
                                           txtLSScore3,
                                           txtYachtScore3,
                                           txtTotalScore3};

            m_P4Scores = new TextBox[16] { txtPlayer4,
                                           txtAcesScore4,
                                           txtDeucesScore4,
                                           txtThreesScore4,
                                           txtFoursScore4,
                                           txtFivesScore4,
                                           txtSixesScore4,
                                           txtSubtotalScore4,
                                           txtBonusAble4,
                                           txtChoiceScore4,
                                           txt4KindScore4,
                                           txtFHScore4,
                                           txtSSScore4,
                                           txtLSScore4,
                                           txtYachtScore4,
                                           txtTotalScore4};

            // 현재 게임에 접속한 인원수대로 점수표 활성화
            int iPlayerCount = SocketManager.GetInst().NickNameList.Count;
            txtPlayer1.Text = SocketManager.GetInst().NickNameList[0];
            if(2 <= iPlayerCount)
            {
                txtPlayer2.Text = SocketManager.GetInst().NickNameList[1];
                foreach (TextBox control in m_P2Scores)
                {
                    control.Visible = true;
                }

                if(3 <= iPlayerCount)
                {
                    txtPlayer3.Text = SocketManager.GetInst().NickNameList[2];
                    foreach (TextBox control in m_P3Scores)
                    {
                        control.Visible = true;
                    }

                    if (4 == iPlayerCount)
                    {
                        txtPlayer4.Text = SocketManager.GetInst().NickNameList[3];
                        foreach (TextBox control in m_P4Scores)
                        {
                            control.Visible = true;
                        }
                    }
                }
            }

            // 자신의 점수판만 활성화
            switch (SocketManager.GetInst().UID)
            {
                case 0:
                    {
                        foreach (TextBox control in m_P1Scores)
                        {
                            control.Enabled = true;
                        }
                    }
                    break;
                case 1:
                    {
                        foreach (TextBox control in m_P2Scores)
                        {
                            control.Enabled = true;
                        }
                    }
                    break;
                case 2:
                    {
                        foreach (TextBox control in m_P3Scores)
                        {
                            control.Enabled = true;
                        }
                    }
                    break;
                case 3:
                    {
                        foreach (TextBox control in m_P4Scores)
                        {
                            control.Enabled = true;
                        }
                    }
                    break;
            }

            Reset();

            if(m_iCurrPlayerID == SocketManager.GetInst().UID)
                btnRoll.Enabled = true;
        }

        private void GetPacket()
        {
            while (true)
            {
                byte[] buffer = new byte[1024 * 4];
                SocketManager.GetInst().Stream.Read(buffer, 0, buffer.Length);

                Packet packet = (Packet)Packet.Deserialize(buffer);

                switch (packet.packet_Type)
                {
                    case PacketType.RollStart_Result:
                        {
                            RollStartResult pkRSR = (RollStartResult)packet;
                            if (m_iCurrPlayerID != SocketManager.GetInst().UID)
                            {
                                RollDiceImage(false);
                            }

                            RollDisplay.Invoke(new MethodInvoker(() =>
                            {
                                RollDisplay.Text = pkRSR.remainRollCount.ToString();
                            }));
                        }
                        break;
                    case PacketType.RollEnd_Result:
                        {
                            tmrRoll.Stop();
                            RollEndResult pkRER = (RollEndResult)packet;
                            if (m_iCurrPlayerID != SocketManager.GetInst().UID)
                            {
                                pbDice1.Invoke(new MethodInvoker(() =>
                                {
                                    pbDice1.Image = m_images[pkRER.dice1 - 1];
                                }));

                                pbDice2.Invoke(new MethodInvoker(() =>
                                {
                                    pbDice2.Image = m_images[pkRER.dice2 - 1];
                                }));

                                pbDice3.Invoke(new MethodInvoker(() =>
                                {
                                    pbDice3.Image = m_images[pkRER.dice3 - 1];
                                }));

                                pbDice4.Invoke(new MethodInvoker(() =>
                                {
                                    pbDice4.Image = m_images[pkRER.dice4 - 1];
                                }));

                                pbDice5.Invoke(new MethodInvoker(() =>
                                {
                                    pbDice5.Image = m_images[pkRER.dice5 - 1];
                                }));
                            }

                            TextBox[] arrTB = null;

                            if (m_iCurrPlayerID == 0)
                                arrTB = m_P1Scores;
                            else if (m_iCurrPlayerID == 1)
                                arrTB = m_P2Scores;
                            else if (m_iCurrPlayerID == 2)
                                arrTB = m_P3Scores;
                            else if (m_iCurrPlayerID == 3)
                                arrTB = m_P4Scores;

                            UpdateScore(arrTB);
                        }
                        break;
                    case PacketType.Lock_Result:
                        {
                            LockResult pkLR = (LockResult)packet;
                            if (m_iCurrPlayerID != SocketManager.GetInst().UID)
                            {
                                CheckBox cbTmp = null;
                                switch (pkLR.lockNumber)
                                {
                                    case 1:
                                        cbTmp = cbDice1;
                                        break;
                                    case 2:
                                        cbTmp = cbDice2;
                                        break;
                                    case 3:
                                        cbTmp = cbDice3;
                                        break;
                                    case 4:
                                        cbTmp = cbDice4;
                                        break;
                                    case 5:
                                        cbTmp = cbDice5;
                                        break;
                                }

                                if (cbTmp.InvokeRequired)
                                    cbTmp.Invoke(new MethodInvoker(() => cbTmp.Checked = pkLR.isLock));
                            }
                        }
                        break;
                    case PacketType.Select_Result:
                        {
                            SelectResult pkSR = (SelectResult)packet;
                            if (m_iCurrPlayerID != SocketManager.GetInst().UID)
                            {
                                TextBox[] arrTB = null;
                                switch (m_iCurrPlayerID)
                                {
                                    case 0:
                                        arrTB = m_P1Scores;
                                        break;
                                    case 1:
                                        arrTB = m_P2Scores;
                                        break;
                                    case 2:
                                        arrTB = m_P3Scores;
                                        break;
                                    case 3:
                                        arrTB = m_P4Scores;
                                        break;
                                }

                                switch (pkSR.eScoreType)
                                {
                                    case ScoreType.ACES:
                                        arrTB[1].Invoke(new Action(() => arrTB[1].Text = Convert.ToString(pkSR.iScore)));
                                        break;
                                    case ScoreType.DEUCES:
                                        arrTB[2].Invoke(new Action(() => arrTB[2].Text = Convert.ToString(pkSR.iScore)));
                                        break;
                                    case ScoreType.THREES:
                                        arrTB[3].Invoke(new Action(() => arrTB[3].Text = Convert.ToString(pkSR.iScore)));
                                        break;
                                    case ScoreType.FOURS:
                                        arrTB[4].Invoke(new Action(() => arrTB[4].Text = Convert.ToString(pkSR.iScore)));
                                        break;
                                    case ScoreType.FIVES:
                                        arrTB[5].Invoke(new Action(() => arrTB[5].Text = Convert.ToString(pkSR.iScore)));
                                        break;
                                    case ScoreType.SIXES:
                                        arrTB[6].Invoke(new Action(() => arrTB[6].Text = Convert.ToString(pkSR.iScore)));
                                        break;
                                    case ScoreType.CHOICE:
                                        arrTB[9].Invoke(new Action(() => arrTB[9].Text = Convert.ToString(pkSR.iScore)));
                                        break;
                                    case ScoreType.FOUR_OF_KIND:
                                        arrTB[10].Invoke(new Action(() => arrTB[10].Text = Convert.ToString(pkSR.iScore)));
                                        break;
                                    case ScoreType.FULLHOUSE:
                                        arrTB[11].Invoke(new Action(() => arrTB[11].Text = Convert.ToString(pkSR.iScore)));
                                        break;
                                    case ScoreType.SMALL_STRAIGHT:
                                        arrTB[12].Invoke(new Action(() => arrTB[12].Text = Convert.ToString(pkSR.iScore)));
                                        break;
                                    case ScoreType.LARGE_STRAIGHT:
                                        arrTB[13].Invoke(new Action(() => arrTB[13].Text = Convert.ToString(pkSR.iScore)));
                                        break;
                                    case ScoreType.YACHT:
                                        arrTB[14].Invoke(new Action(() => arrTB[14].Text = Convert.ToString(pkSR.iScore)));
                                        break;
                                }
                            }

                            UpdateScore();
                            if (SocketManager.GetInst().NickNameList.Count <= ++m_iCurrPlayerID)
                            {
                                m_iCurrPlayerID = 0;
                                this.Invoke(new Action(() => UpdateRound()));
                            }
                            Reset();
                        }
                        break;
                    case PacketType.GameOver_Result:
                        {
                            GameOverResult pkGOR = (GameOverResult)packet;

                            DialogResult dr = MessageBox.Show(pkGOR.result, "게임 결과", MessageBoxButtons.OK);
                            if (dr == DialogResult.OK)
                            {
                                this.Invoke(new Action(() => BackToLobby()));
                            }
                        }
                        break;
                    case PacketType.Disconnect:
                        break;
                }
            }
        }

        private void BackToLobby()
        {
            this.Invoke(new MethodInvoker(delegate
            {
                if (m_fLobby != null)
                    m_fLobby.Show();

                m_tHandler.Abort();
                this.Close();
            }));
        }

        private void UpdateScore()
        {
            int iPlayerCount = SocketManager.GetInst().NickNameList.Count;
            UpdateTotalScore(m_P1Scores);
            if (2 <= iPlayerCount)
            {
                UpdateTotalScore(m_P2Scores);
                if (3 <= iPlayerCount)
                {
                    UpdateTotalScore(m_P3Scores);
                    if (4 == iPlayerCount)
                    {
                        UpdateTotalScore(m_P4Scores);
                    }
                }
            }
        }

        // 플레이 턴이 들어왔을 때 초기상태로 되돌림
        private void Reset()
        {
            m_iRollCount = 3;

            // 자신이 현재 플레이어라면 주사위를 굴릴수 있게 세팅
            if(m_iCurrPlayerID == SocketManager.GetInst().UID)
            {
                if (btnRoll.InvokeRequired)
                    btnRoll.Invoke(new MethodInvoker(() => { btnRoll.Enabled = true; }));
                
                if (cbDice1.InvokeRequired)
                    cbDice1.Invoke(new MethodInvoker(() => { cbDice1.Enabled = false; }));

                if (cbDice2.InvokeRequired)
                    cbDice2.Invoke(new MethodInvoker(() => { cbDice2.Enabled = false; }));

                if (cbDice3.InvokeRequired)
                    cbDice3.Invoke(new MethodInvoker(() => { cbDice3.Enabled = false; }));

                if (cbDice4.InvokeRequired)
                    cbDice4.Invoke(new MethodInvoker(() => { cbDice4.Enabled = false; }));

                if (cbDice5.InvokeRequired)
                    cbDice5.Invoke(new MethodInvoker(() => { cbDice5.Enabled = false; }));
            }

            // 다이스 락 상태 해제
            if (cbDice1.InvokeRequired)
                cbDice1.Invoke(new MethodInvoker(() => { cbDice1.Checked = false; }));

            if (cbDice2.InvokeRequired)
                cbDice2.Invoke(new MethodInvoker(() => { cbDice2.Checked = false; }));

            if (cbDice3.InvokeRequired)
                cbDice3.Invoke(new MethodInvoker(() => { cbDice3.Checked = false; }));

            if (cbDice4.InvokeRequired)
                cbDice4.Invoke(new MethodInvoker(() => { cbDice4.Checked = false; }));

            if (cbDice5.InvokeRequired)
                cbDice5.Invoke(new MethodInvoker(() => { cbDice5.Checked = false; }));

            // 다이스 이미지 초기화
            if (pbDice1.InvokeRequired)
                pbDice1.Invoke(new MethodInvoker(() => { pbDice1.Image = null; }));

            if (pbDice2.InvokeRequired)
                pbDice2.Invoke(new MethodInvoker(() => { pbDice2.Image = null; }));

            if (pbDice3.InvokeRequired)
                pbDice3.Invoke(new MethodInvoker(() => { pbDice3.Image = null; }));

            if (pbDice4.InvokeRequired)
                pbDice4.Invoke(new MethodInvoker(() => { pbDice4.Image = null; }));

            if (pbDice5.InvokeRequired)
                pbDice5.Invoke(new MethodInvoker(() => { pbDice5.Image = null; }));
        }

        private void TurnEnd()
        {
            btnRoll.Enabled = false;
            m_iRollCount = 3;
            RollDisplay.Text = m_iRollCount.ToString();
        }

        private void UpdateTotalScore(TextBox[] _arrTB)
        {
            // Score Text 클리어
            foreach (TextBox item in _arrTB)
            {
                if (item == _arrTB[0])
                    continue;

                ClearText(item);
            }

            // SubScore 갱신
            int iSubScore = TextToScore(_arrTB[1]) + TextToScore(_arrTB[2]) + TextToScore(_arrTB[3])
                            + TextToScore(_arrTB[4]) + TextToScore(_arrTB[5]) + TextToScore(_arrTB[6]);

            if (_arrTB[7].InvokeRequired)
            {
                _arrTB[7].Invoke(new MethodInvoker(() =>
                {
                    _arrTB[7].Text = iSubScore.ToString() + " / 63";
                }));
            }

            if (63 <= iSubScore)
            {
                if (_arrTB[8].InvokeRequired)
                {
                    _arrTB[8].Invoke(new MethodInvoker(() =>
                    {
                        _arrTB[8].Text = "v";
                    }));
                }
            }

            // TotalScore 갱신
            int iTotalScore = iSubScore + TextToScore(_arrTB[9]) + TextToScore(_arrTB[10]) + TextToScore(_arrTB[11])
                                + TextToScore(_arrTB[12]) + TextToScore(_arrTB[13]) + TextToScore(_arrTB[14]);
            
            if (_arrTB[15].InvokeRequired)
                _arrTB[15].Invoke(new MethodInvoker(() => { _arrTB[15].Text = iTotalScore.ToString(); }));
        }

        public void UpdateRound()
        {
            // 12 라운드 종료시 첫번째 플레이어가 게임 종료 패킷 전송
            if (12 < ++m_iRound && 0 == SocketManager.GetInst().UID)
            {
                Dictionary<string, int> dicScore = new Dictionary<string, int>();

                int iPlayerCount = SocketManager.GetInst().NickNameList.Count;
                
                dicScore.Add(txtPlayer1.Text, Convert.ToInt32(TextToScore(txtTotalScore1)));
                if (2 <= iPlayerCount)
                {
                    dicScore.Add(txtPlayer2.Text, Convert.ToInt32(TextToScore(txtTotalScore2)));
                    if (3 <= iPlayerCount)
                    {
                        dicScore.Add(txtPlayer3.Text, Convert.ToInt32(TextToScore(txtTotalScore3)));
                        if (4 == iPlayerCount)
                        {
                            dicScore.Add(txtPlayer4.Text, Convert.ToInt32(TextToScore(txtTotalScore4)));
                        }
                    }
                }

                dicScore.OrderByDescending(item => item.Value);

                string strResult = null;

                int iRank = 0;
                foreach (var item in dicScore)
                {
                    strResult += (++iRank).ToString();
                    strResult += "등 : " + item.Key + " / " + item.Value.ToString() + "점\n";
                }

                // 패킷 전송
                byte[] buff = new byte[1024 * 4];
                GameOver pkGameOver = new GameOver();
                pkGameOver.result = strResult;
                Packet.Serialize(pkGameOver).CopyTo(buff, 0);
                SocketManager.GetInst().Stream.Write(buff, 0, buff.Length);
                SocketManager.GetInst().Stream.Flush();

                return;
            }

            lbRoundDisplay.Invoke(new MethodInvoker(() =>
            {
                lbRoundDisplay.Text = m_iRound.ToString();
            }));
        }

        private void ClearText(TextBox _TextBox)
        {
            if (true == _TextBox.Enabled)
            {
                if(_TextBox.InvokeRequired)
                    _TextBox.Invoke(new MethodInvoker(() => { _TextBox.Text = null; }));
            }
        }

        private int TextToScore(TextBox _TextBox)
        {
            if (_TextBox.Text == String.Empty)
                return 0;

            return Int32.Parse(_TextBox.Text);
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
            cbDice1.Invoke(new MethodInvoker(() =>
            {
                cbDice1.Enabled = true;
            }));

            cbDice2.Invoke(new MethodInvoker(() =>
            {
                cbDice2.Enabled = true;
            }));

            cbDice3.Invoke(new MethodInvoker(() =>
            {
                cbDice3.Enabled = true;
            }));

            cbDice4.Invoke(new MethodInvoker(() =>
            {
                cbDice4.Enabled = true;
            }));

            cbDice5.Invoke(new MethodInvoker(() =>
            {
                cbDice5.Enabled = true;
            }));

            if(m_iCurrPlayerID == SocketManager.GetInst().UID && 0 < m_iRollCount)
            {
                btnRoll.Invoke(new MethodInvoker(() =>
                {
                    btnRoll.Enabled = true;
                }));
            }
        }

        // 현재 턴인 플레이어만 누를수 있음
        private void btnRoll_Click(object sender, EventArgs e)
        {
            LockRolling();

            // 패킷 전송
            byte[] buff = new byte[1024 * 4];
            RollStart pkRollStart = new RollStart();
            pkRollStart.remainRollCount = --m_iRollCount;
            Packet.Serialize(pkRollStart).CopyTo(buff, 0);
            SocketManager.GetInst().Stream.Write(buff, 0, buff.Length);
            SocketManager.GetInst().Stream.Flush();

            RollDiceImage(false);
        }

        private void cbLock_CheckChanged(object sender, EventArgs e)
        {
            if (m_iCurrPlayerID != SocketManager.GetInst().UID)
                return;

            CheckBox cbChanged = (CheckBox)sender;

            bool bChecked = cbChanged.Checked;

            int iNum = 0;
            switch (cbChanged.Text)
            {
                case "cbDice1":
                    iNum = 1;
                    break;
                case "cbDice2":
                    iNum = 2;
                    break;
                case "cbDice3":
                    iNum = 3;
                    break;
                case "cbDice4":
                    iNum = 4;
                    break;
                case "cbDice5":
                    iNum = 5;
                    break;
            }

            byte[] buff = new byte[1024 * 4];
            Lock pkLock = new Lock();
            pkLock.isLock = bChecked;
            pkLock.lockNumber = iNum;
            Packet.Serialize(pkLock).CopyTo(buff, 0);
            SocketManager.GetInst().Stream.Write(buff, 0, buff.Length);
            SocketManager.GetInst().Stream.Flush();
        }


        private void RollDiceImage(bool _bWait)
        {
            // 이미지 연속 출력 연출 위한 타이머
            m_iRollRandomCount = 0;

            void Timer_Tick(object ss, EventArgs ee)
            {
                Random random = new Random(DateTime.Now.Millisecond);

                if (false == cbDice1.Checked)
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

                m_iRollRandomCount++;

                if(false == _bWait)
                {
                    if(6 <= m_iRollRandomCount)
                    {
                        tmrRoll.Stop();
                        RollEnd();
                    }
                }
            }

            tmrRoll.Tick += Timer_Tick;
            tmrRoll.Start();
        }

        private void RollEnd()
        {
            byte[] buff = new byte[1024 * 4];
            RollEnd pkRollEnd = new RollEnd();
            pkRollEnd.dice1 = m_arrDices[0];
            pkRollEnd.dice2 = m_arrDices[1];
            pkRollEnd.dice3 = m_arrDices[2];
            pkRollEnd.dice4 = m_arrDices[3];
            pkRollEnd.dice5 = m_arrDices[4];
            Packet.Serialize(pkRollEnd).CopyTo(buff, 0);
            SocketManager.GetInst().Stream.Write(buff, 0, buff.Length);
            SocketManager.GetInst().Stream.Flush();
        }

        private void UpdateScoreText(TextBox _TextBox, string _strText)
        {
            if (false == _TextBox.Enabled)
                return;

            _TextBox.Invoke(new MethodInvoker(() =>
            {
                _TextBox.Text = _strText;
            }));
        }

        private void UpdateScore(TextBox[] _arrTexBox)
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

            //txtSubtotalScore1,: 7
            //txtBonusAble1,    : 8
            //txtTotalScore1};  : 15

            // Aces 갱신
            UpdateScoreText(_arrTexBox[1], (arrNumCount[0] * 1).ToString());
            // Dueces 갱신
            UpdateScoreText(_arrTexBox[2], (arrNumCount[1] * 2).ToString());
            // Threes 갱신
            UpdateScoreText(_arrTexBox[3], (arrNumCount[2] * 3).ToString());
            // Fours 갱신
            UpdateScoreText(_arrTexBox[4], (arrNumCount[3] * 4).ToString());
            // Fives 갱신
            UpdateScoreText(_arrTexBox[5], (arrNumCount[4] * 5).ToString());
            // Sixes 갱신
            UpdateScoreText(_arrTexBox[6], (arrNumCount[5] * 6).ToString());

            // 족보 점수 갱신

            // Choice
            UpdateScoreText(_arrTexBox[9], iTotal.ToString());

            // 4 of a Kind
            UpdateScoreText(_arrTexBox[10], Convert.ToString(0));
            for (int i = 0; i < iDice; i++)
            {
                if (4 <= arrNumCount[i])
                    UpdateScoreText(_arrTexBox[10], iTotal.ToString());
            }

            // Full House
            int iThreeDice = -1, iTwoDice = -1;

            for (int i = 0; i < iDice; i++)
            {
                if (3 == arrNumCount[i])
                    iThreeDice = i;
            }

            if (0 <= iThreeDice)
            {
                for (int i = 0; i < iDice; i++)
                {
                    if (2 == arrNumCount[i])
                        iTwoDice = i;
                }

                if (0 <= iTwoDice)
                    UpdateScoreText(_arrTexBox[11], iTotal.ToString());
                else
                    UpdateScoreText(_arrTexBox[11], Convert.ToString(0));
            }
            else
            {
                UpdateScoreText(_arrTexBox[11], Convert.ToString(0));
            }

            // Small Straight
            UpdateScoreText(_arrTexBox[12], Convert.ToString(0));
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
            if (0 <= iSmallOne)
            {
                for (int i = iSmallOne + 1; i < iDice; i++)
                {
                    if (1 == arrNumCount[i])
                        iStraightCount++;
                }
            }

            if (4 <= iStraightCount)
                UpdateScoreText(_arrTexBox[12], Convert.ToString(15));

            // Large Straight
            UpdateScoreText(_arrTexBox[13], Convert.ToString(0));

            if (5 == iStraightCount)
                UpdateScoreText(_arrTexBox[13], Convert.ToString(30));

            // Yacht
            UpdateScoreText(_arrTexBox[14], Convert.ToString(0));
            foreach (int num in arrNumCount)
            {
                if (5 == num)
                {
                    UpdateScoreText(_arrTexBox[14], Convert.ToString(50));
                    break;
                }
            }
        }

        private void Score_DoubleClick(object sender, EventArgs e)
        {
            if (m_iCurrPlayerID != SocketManager.GetInst().UID)
                return;

            TextBox tbScore = (TextBox)sender;

            if (false == tbScore.Enabled || string.Empty == tbScore.Text)
                return;

            if (false == btnRoll.Enabled)
            {
                if (0 < m_iRollCount)
                    return;
            }

            // 패킷 전송
            byte[] buff = new byte[1024 * 4];
            Select pkSelect = new Select();

            if(tbScore.Text.Contains("txtAcesScore"))
            {
                pkSelect.eScoreType = ScoreType.ACES;
            }
            else if(tbScore.Text.Contains("txtDeucesScore"))
            {
                pkSelect.eScoreType = ScoreType.DEUCES;
            }
            else if (tbScore.Text.Contains("txtThreesScore"))
            {
                pkSelect.eScoreType = ScoreType.THREES;
            }
            else if (tbScore.Text.Contains("txtFoursScore"))
            {
                pkSelect.eScoreType = ScoreType.FOURS;
            }
            else if (tbScore.Text.Contains("txtFivesScore"))
            {
                pkSelect.eScoreType = ScoreType.FIVES;
            }
            else if (tbScore.Text.Contains("txtSixesScore"))
            {
                pkSelect.eScoreType = ScoreType.SIXES;
            }
            else if (tbScore.Text.Contains("txtChoiceScore"))
            {
                pkSelect.eScoreType = ScoreType.CHOICE;
            }
            else if (tbScore.Text.Contains("txt4KindScore"))
            {
                pkSelect.eScoreType = ScoreType.FOUR_OF_KIND;
            }
            else if (tbScore.Text.Contains("txtAcesScore"))
            {
                pkSelect.eScoreType = ScoreType.ACES;
            }
            else if (tbScore.Text.Contains("txtFHScore"))
            {
                pkSelect.eScoreType = ScoreType.FULLHOUSE;
            }
            else if (tbScore.Text.Contains("txtSSScore"))
            {
                pkSelect.eScoreType = ScoreType.SMALL_STRAIGHT;
            }
            else if (tbScore.Text.Contains("txtSSScore"))
            {
                pkSelect.eScoreType = ScoreType.LARGE_STRAIGHT;
            }
            else if (tbScore.Text.Contains("txtYachtScore"))
            {
                pkSelect.eScoreType = ScoreType.SMALL_STRAIGHT;
            }

            pkSelect.iScore = Convert.ToInt32(tbScore.Text);
            Packet.Serialize(pkSelect).CopyTo(buff, 0);
            SocketManager.GetInst().Stream.Write(buff, 0, buff.Length);
            SocketManager.GetInst().Stream.Flush();

            tbScore.Enabled = false;
            TurnEnd();
        }
    }
}
