using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Mastermind
{   
    public partial class Form1 : Form
    {
        Random rnd = new Random();

        HashSet<string> POOL;

        string keptNumber;
        string lastGuessed;

        public Form1()
        {

            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupGame();
        }

        private void SetupGame()
        {
            ClearFields();
            FillPool();
            GuessNumber();
            PlayerTurn();
        }

        private void ClearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
        }
        private void FillPool()
        {
            POOL = new HashSet<string>();
            for(int i=100; i<10000; i++)
            {
                if (i < 1000 && !i.ToString().Contains("0"))
                {
                    if (isUniqueDigits(i))
                    {
                        POOL.Add("0" + i.ToString());
                    }

                } else if(i > 1000)
                {
                    if (isUniqueDigits(i))
                    {
                        POOL.Add(i.ToString());
                    }
                }
            }
        }

        private bool isUniqueDigits(int number)
        {
            var digits = new HashSet<string>(number.ToString().Select(digit => digit.ToString()).ToList());
            return digits.Count().Equals(number.ToString().Length);
        }


        private AccuracyRate GetAccuracyRate(string relationNumber, string number)
        {
            int arti = 0;
            int eksi = 0;

            for(int i=0; i<number.Length; i++)
            {
                if (number[i].Equals(relationNumber[i]))
                {
                    arti++;
                }else if (relationNumber.Contains(number[i]))
                {
                    eksi++;
                }
            }

            return (AccuracyRate)int.Parse(arti + "" + eksi);
        }

        private void GuessNumber()
        {
            keptNumber = POOL.ToList()[rnd.Next(0, POOL.Count())];
        }

        private void TahminYazdir()
        {
            if (POOL.Count().Equals(0))
            {
                MessageBox.Show("I have no guesses, there are errors in your tips. I will restart the game, you should be more careful. The number in my mind is " + keptNumber);
                SetupGame();
            }
            else
            {
                lastGuessed = POOL.ToList()[rnd.Next(0, POOL.Count())];
                textBox1.Text += lastGuessed + "\r\n";
            }
        }

        private void PlayerTurn()
        {
            groupBox1.Enabled = false;
            groupBox2.Enabled = true;
        }
        private void PcTurn()
        {
            groupBox1.Enabled = true;
            groupBox2.Enabled = false;
            TahminYazdir();
        }

        private void buttonIpucuVer(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox8.Text) || string.IsNullOrEmpty(textBox9.Text))
            {
                MessageBox.Show("Please enter hint value");
                return;
            }

            int hint = int.Parse(textBox8.Text) + int.Parse(textBox9.Text);
            var accuracyRateOfPc = textBox8.Text + textBox9.Text;

            if ((hint) > 4)
            {
                MessageBox.Show("The sum of the plus and minus values of the hint you give, cannot exceed 4.");
            }
            else if (((AccuracyRate) int.Parse(accuracyRateOfPc)).Equals(AccuracyRate.ARTI4EKSI0))
            {
                FinishGame(false);
            }
            else
            {
                var newPool = new HashSet<string>();

                foreach(var number in POOL)
                {
                    if (!number.Equals(lastGuessed))
                    {
                        var rate = ((int)GetAccuracyRate(number, lastGuessed)).ToString();
                        rate = rate.Length > 1 ? rate : "0" + rate;
                        if (accuracyRateOfPc.Equals(rate))
                        {
                            newPool.Add(number);
                        }
                    }
                }
                POOL = new HashSet<string>(newPool);
                textBox7.Text += textBox8.Text + "\r\n";
                textBox6.Text += textBox9.Text + "\r\n";
                textBox8.Clear();
                textBox9.Clear();
                PlayerTurn();
            }

        }

        private void buttonTahminEt(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("Please enter your guess");
                return;
            }

            int guess = int.Parse(textBox5.Text);
            if (guess < 1000 && guess.ToString().Contains("0"))
            {
                MessageBox.Show("The digits of the prediction you entered must be different from each other");
                return;
            }
            else
            {
                if (!isUniqueDigits(guess))
                {
                    MessageBox.Show("The digits of the prediction you entered must be different from each other");
                    return;
                }
            }

            if (textBox5.Text.Equals(keptNumber))
            {
                FinishGame(true);
            }
            else
            {
                var accuracyRateOfPlayer = ((int)GetAccuracyRate(textBox5.Text, keptNumber)).ToString();
                accuracyRateOfPlayer = accuracyRateOfPlayer.Length > 1 ? accuracyRateOfPlayer : "0" + accuracyRateOfPlayer;
                textBox3.Text += accuracyRateOfPlayer[0] + "\r\n";
                textBox4.Text += accuracyRateOfPlayer[1] + "\r\n";
                textBox2.Text += textBox5.Text + "\r\n";
                textBox5.Clear();
                PcTurn();
            }
        }

        private void FinishGame(bool playerWon)
        {
            if (playerWon)
            {
                MessageBox.Show(("Congratulations, you won. Let's start the new game").ToUpper());
            }
            else
            {
                MessageBox.Show(("I won :) The number in my mind is " + keptNumber + "\r\n Let's start the new game").ToUpper());
            }
            SetupGame();
        }


        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;

            if(chr != 8 && textBox8.Text.Length > 0)
            {
                e.Handled = true;
            }
            else if (chr != 8 && chr != 48 && chr != 49 && chr != 50 && chr != 51 && chr != 52)
            {
                e.Handled = true;
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;

            if (chr != 8 && textBox9.Text.Length > 0)
            {
                e.Handled = true;
            }
            else if (chr != 8 && chr != 48 && chr != 49 && chr != 50 && chr != 51 && chr != 52)
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;

            if (chr != 8 && textBox5.Text.Length > 3)
            {
                e.Handled = true;
            }
            else if (chr != 8 && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

    }

    enum AccuracyRate
    {
        ARTI0EKSI0 = 0,

        ARTI0EKSI1 = 1,
        ARTI0EKSI2 = 2,
        ARTI0EKSI3 = 3,
        ARTI0EKSI4 = 4,

        ARTI1EKSI0 = 10,
        ARTI1EKSI1 = 11,
        ARTI1EKSI2 = 12,
        ARTI1EKSI3 = 13,

        ARTI2EKSI0 = 20,
        ARTI2EKSI1 = 21,
        ARTI2EKSI2 = 22,

        ARTI3EKSI0 = 30,

        ARTI4EKSI0 = 40
    }
}
