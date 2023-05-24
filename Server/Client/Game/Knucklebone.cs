using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Client.Game
{
    public partial class Knucklebone : Form
    {
        private int m_DiceNumber = 0;

        System.Drawing.Image[] m_images = {
             Properties.Resources._1, Properties.Resources._2, Properties.Resources._3, Properties.Resources._4, Properties.Resources._5, Properties.Resources._6
        };


        public Knucklebone()
        {
            InitializeComponent();
        }

        //버튼으로 주사위 굴리기
        private void btnRoll_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            List<PictureBox> m_P1Boxes = pnlP1Boxes.Controls.OfType<PictureBox>().ToList();
            List<PictureBox> m_P2Boxes = pnlP2Boxes.Controls.OfType<PictureBox>().ToList();

            //box들 Enabled 초기화
            foreach (PictureBox pictureBox in m_P1Boxes)
            {
                if(pictureBox.Image == null) pictureBox.Enabled = true;
            }
            foreach (PictureBox pictureBox in m_P2Boxes)
            {
                if (pictureBox.Image == null) pictureBox.Enabled = true;
            }

            //주사위 굴리기 구현
            if (clickedButton != null)
            {
                Timer timer = new Timer();
                timer.Interval = 100;
                int ncount = 0;
                PictureBox targetPictureBox = null;

                if (clickedButton.Name == "btnP1Roll")
                {
                    btnP2Roll.Enabled = false;
                    foreach (PictureBox pictureBox in m_P2Boxes)
                    {
                        pictureBox.Enabled = false;
                    }
                    targetPictureBox = pbP1Dice;
                }
                else if (clickedButton.Name == "btnP2Roll")
                {
                    foreach (PictureBox pictureBox in m_P1Boxes)
                    {
                        pictureBox.Enabled = false;
                    }
                    btnP1Roll.Enabled = false;
                    targetPictureBox = pbP2Dice;
                }

                if (targetPictureBox != null)
                {
                    void Timer_Tick(object ss, EventArgs ee)
                    {
                        Random random = new Random(DateTime.Now.Millisecond);
                        m_DiceNumber = random.Next(1, 7);
                        targetPictureBox.Image = m_images[m_DiceNumber - 1];

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

        //박스 선택
        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;
            List<PictureBox> m_P1Boxes = pnlP1Boxes.Controls.OfType<PictureBox>().ToList();
            List<PictureBox> m_P2Boxes = pnlP2Boxes.Controls.OfType<PictureBox>().ToList();
            m_P1Boxes.Sort((pb1, pb2) => string.Compare(pb1.Name, pb2.Name));
            m_P2Boxes.Sort((pb1, pb2) => string.Compare(pb1.Name, pb2.Name));
            clickedPictureBox.Enabled = true;

            if (clickedPictureBox != null)
            {
                clickedPictureBox.Image = m_images[m_DiceNumber - 1];
                switch (clickedPictureBox.Name) //Row 점수 표시
                {
                    case "pbP1Box1":
                        DeleteSameDice(m_P1Boxes, m_P2Boxes, clickedPictureBox, lbP2Row1Score);
                        SameRowCalculate(m_P1Boxes, lbP1Row1Score);
                        break;
                    case "pbP1Box2":
                        DeleteSameDice(m_P1Boxes, m_P2Boxes, clickedPictureBox, lbP2Row1Score);
                        SameRowCalculate(m_P1Boxes, lbP1Row1Score);
                        break;
                    case "pbP1Box3":
                        DeleteSameDice(m_P1Boxes, m_P2Boxes, clickedPictureBox, lbP2Row1Score);
                        SameRowCalculate(m_P1Boxes, lbP1Row1Score);
                        break;
                    case "pbP1Box4":
                        DeleteSameDice(m_P1Boxes, m_P2Boxes, clickedPictureBox, lbP2Row2Score);
                        SameRowCalculate(m_P1Boxes, lbP1Row2Score);
                        break;
                    case "pbP1Box5":
                        DeleteSameDice(m_P1Boxes, m_P2Boxes, clickedPictureBox, lbP2Row2Score);
                        SameRowCalculate(m_P1Boxes, lbP1Row2Score);
                        break;
                    case "pbP1Box6":
                        DeleteSameDice(m_P1Boxes, m_P2Boxes, clickedPictureBox, lbP2Row2Score);
                        SameRowCalculate(m_P1Boxes, lbP1Row2Score);
                        break;
                    case "pbP1Box7":
                        DeleteSameDice(m_P1Boxes, m_P2Boxes, clickedPictureBox, lbP2Row3Score);
                        SameRowCalculate(m_P1Boxes, lbP1Row3Score);
                        break;
                    case "pbP1Box8":
                        DeleteSameDice(m_P1Boxes, m_P2Boxes, clickedPictureBox, lbP2Row3Score);
                        SameRowCalculate(m_P1Boxes, lbP1Row3Score);
                        break;
                    case "pbP1Box9":
                        DeleteSameDice(m_P1Boxes, m_P2Boxes, clickedPictureBox, lbP2Row3Score);
                        SameRowCalculate(m_P1Boxes, lbP1Row3Score);
                        break;
                    case "pbP2Box1": //p2box
                        DeleteSameDice(m_P2Boxes, m_P1Boxes, clickedPictureBox, lbP1Row1Score);
                       SameRowCalculate(m_P2Boxes, lbP2Row1Score);
                        break;
                    case "pbP2Box2":
                        DeleteSameDice(m_P2Boxes, m_P1Boxes, clickedPictureBox, lbP1Row1Score);
                        SameRowCalculate(m_P2Boxes, lbP2Row1Score);
                        break;
                    case "pbP2Box3":
                        DeleteSameDice(m_P2Boxes, m_P1Boxes, clickedPictureBox, lbP1Row1Score);
                        SameRowCalculate(m_P2Boxes, lbP2Row1Score);
                        break;
                    case "pbP2Box4":
                        DeleteSameDice(m_P2Boxes, m_P1Boxes, clickedPictureBox, lbP1Row2Score);
                        SameRowCalculate(m_P2Boxes, lbP2Row2Score);
                        break;
                    case "pbP2Box5":
                        DeleteSameDice(m_P2Boxes, m_P1Boxes, clickedPictureBox, lbP1Row2Score);
                        SameRowCalculate(m_P2Boxes, lbP2Row2Score);
                        break;
                    case "pbP2Box6":
                        DeleteSameDice(m_P2Boxes, m_P1Boxes, clickedPictureBox, lbP1Row2Score);
                        SameRowCalculate(m_P2Boxes, lbP2Row2Score);
                        break;
                    case "pbP2Box7":
                        DeleteSameDice(m_P2Boxes, m_P1Boxes, clickedPictureBox, lbP1Row3Score);
                        SameRowCalculate(m_P2Boxes, lbP2Row3Score);
                        break;
                    case "pbP2Box8":
                        DeleteSameDice(m_P2Boxes, m_P1Boxes, clickedPictureBox, lbP1Row3Score);
                        SameRowCalculate(m_P2Boxes, lbP2Row3Score);
                        break;
                    case "pbP2Box9":
                        DeleteSameDice(m_P2Boxes, m_P1Boxes, clickedPictureBox, lbP1Row3Score);
                        SameRowCalculate(m_P2Boxes, lbP2Row3Score);
                        break;
                }

            }

            // 다른 box들 이미지 변환 못하게 막기
            foreach (PictureBox pictureBox in m_P1Boxes)
            {
                if (pictureBox != clickedPictureBox)
                {
                    pictureBox.Enabled = false;
                }
            }
            foreach (PictureBox pictureBox in m_P2Boxes)
            {
                if (pictureBox != clickedPictureBox)
                {
                    pictureBox.Enabled = false;
                }
            }

            // 서로 턴 넘기면서 굴리기
            if (!btnP2Roll.Enabled)
            {
                btnP2Roll.Enabled = true;
                btnP1Roll.Enabled = false;
            }
            else if (!btnP1Roll.Enabled)
            {
                btnP1Roll.Enabled = true;
                btnP2Roll.Enabled = false;
            }

            lbP1ScoreSum.Text = SumLabelText(lbP1Row1Score, lbP1Row2Score, lbP1Row3Score).ToString();
            lbP2ScoreSum.Text = SumLabelText(lbP2Row1Score, lbP2Row2Score, lbP2Row3Score).ToString();
        }

        //같은 열 동일 주사위 삭제
        private void DeleteSameDice(List<PictureBox> myPictureBoxes, List<PictureBox> EnemyPictureBoxes, PictureBox pb, Label label)
        {
            int i_pbIndex = myPictureBoxes.IndexOf(pb);
            int i_targetIndex = Array.IndexOf(m_images, pb.Image) + 1;
            int[] imageIndexes = new int[9];

            for (int i = 0; i < 9; i++)
            {
                imageIndexes[i] = Array.IndexOf(m_images, EnemyPictureBoxes[i].Image) + 1;
            }

            if (i_pbIndex <= 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (imageIndexes[i] == i_targetIndex)
                    {
                        EnemyPictureBoxes[i].Image = null;
                    }
                }
            }
            else if (i_pbIndex <= 5)
            {
                for (int i = 3; i < 6; i++)
                {
                    if (imageIndexes[i] == i_targetIndex)
                    {
                        EnemyPictureBoxes[i].Image = null;
                    }
                }
            }
            else
            {
                for (int i = 6; i < 9; i++)
                {
                    if (imageIndexes[i] == i_targetIndex)
                    {
                        EnemyPictureBoxes[i].Image = null;
                    }
                }
            }

            SameRowCalculate(EnemyPictureBoxes, label);
        }

        //같은 열 점수 계산
        private void SameRowCalculate(List<PictureBox> pictureBoxes, Label label)
        {
            Dictionary<PictureBox, int> imageIndexes = new Dictionary<PictureBox, int>();

            // PictureBox와 해당 이미지 인덱스를 Dictionary에 저장
            for (int i = 0; i < pictureBoxes.Count; i++)
            {
                PictureBox pictureBox = pictureBoxes[i];
                int imageIndex = Array.IndexOf(m_images, pictureBox.Image) + 1;
                imageIndexes[pictureBox] = imageIndex;
            }

            int[] imageIndices = pictureBoxes.Select(p => imageIndexes[p]).ToArray();

            switch (label.Name) //Row 점수 표시
            {
                case "lbP1Row1Score":
                case "lbP2Row1Score":
                    CalculateRowScore(imageIndices[0], imageIndices[1], imageIndices[2], label);
                    break;
                case "lbP1Row2Score":
                case "lbP2Row2Score":
                    CalculateRowScore(imageIndices[3], imageIndices[4], imageIndices[5], label);
                    break;
                case "lbP1Row3Score":
                case "lbP2Row3Score":
                    CalculateRowScore(imageIndices[6], imageIndices[7], imageIndices[8], label);
                    break;
            }
        }

        //점수 계산
        private void CalculateRowScore(int imageIndex1, int imageIndex2, int imageIndex3, Label label)
        {
            if (imageIndex1 == imageIndex2 && imageIndex1 == imageIndex3)
            {
                int score = 3 * (imageIndex1 + imageIndex2 + imageIndex3);
                label.Text = score.ToString();
            }
            else if (imageIndex1 != imageIndex2 && imageIndex1 != imageIndex3 && imageIndex2 != imageIndex3)
            {
                int score = imageIndex1 + imageIndex2 + imageIndex3;
                label.Text = score.ToString();
            }
            else if (imageIndex1 == imageIndex2 && imageIndex1 != imageIndex3)
            {
                int score = imageIndex3 + 2 * (imageIndex1 + imageIndex2);
                label.Text = score.ToString();
            }
            else if (imageIndex1 == imageIndex3 && imageIndex1 != imageIndex2)
            {
                int score = imageIndex2 + 2 * (imageIndex1 + imageIndex3);
                label.Text = score.ToString();
            }
            else if (imageIndex2 == imageIndex3 && imageIndex1 != imageIndex2)
            {
                int score = imageIndex1 + 2 * (imageIndex2 + imageIndex3);
                label.Text = score.ToString();
            }
        }


        //Score 합계 계산
        private int SumLabelText(Label label1, Label label2, Label label3)
        {
            int n_sum = 0;

            if (label1 != null && !string.IsNullOrEmpty(label1.Text))
            {
                int value1;
                if (int.TryParse(label1.Text, out value1))
                {
                    n_sum += value1;
                }
            }

            if (label2 != null && !string.IsNullOrEmpty(label2.Text))
            {
                int value2;
                if (int.TryParse(label2.Text, out value2))
                {
                    n_sum += value2;
                }
            }

            if (label3 != null && !string.IsNullOrEmpty(label3.Text))
            {
                int value3;
                if (int.TryParse(label3.Text, out value3))
                {
                    n_sum += value3;
                }
            }

            return n_sum;
        }
    }
}