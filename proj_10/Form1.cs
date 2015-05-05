using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proj_10
{
    public partial class Form1 : Form
    {

        Button[,] butt_array;

        //constructor
        public Form1()
        {
            InitializeComponent();

            butt_array = new Button[4, 4] { 
                              {butt0_0, butt0_1, butt0_2, butt0_3},
                              {butt1_0, butt1_1, butt1_2, butt1_3},
                              {butt2_0, butt2_1, butt2_2, butt2_3},
                              {butt3_0, butt3_1, butt3_2, butt3_3}
                              };
            randomButton();
            winnerBox.Visible = false;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    butt_array[i, j].Click += new EventHandler(shiftClick);
                }
            }
            autoWin.Click += new EventHandler(winitAll);
        }

        private void winitAll(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            int num = 1;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    butt_array[i, j].Text = num++.ToString();
                    butt_array[i, j].Visible = true;
                }
            }
            butt_array[3, 2].Text = "16";
            butt_array[3, 2].Visible = false;

            butt_array[3, 3].Text = "15";
            butt_array[3, 3].Visible = true;
        }

        private void randomButton()
        {
            Random r = new Random();
            int num = 0;
            bool foundNum = false;

            List<int> list = new List<int>();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    foundNum = false;
                    do
                    {
                        num = r.Next(16);
                        if (list.Contains(num + 1) != true)
                        {
                            butt_array[i, j].Text = (num + 1).ToString();
                            list.Add(num + 1);
                            foundNum = true;
                            if (num == 15) // since the random num is one less
                                butt_array[i, j].Visible = false;

                        }
                    }
                    while (!foundNum);
                }
            }
        }

        private void shiftClick(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            int butt_row = 0;
            int butt_col = 0;
            int blank_row = 0;
            int blank_col = 0;
            

            //find the col and row of the clicked button
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    //find array position for the blank button
                    if (butt_array[i,j].Text == "16")
                    {
                        blank_row = i;
                        blank_col = j;
                    }

                    //find array position for the one that was pressed
                    if (butt_array[i,j].Text == b.Text)
                    {
                        butt_row = i;
                        butt_col = j;
                    }
                }
            }

            if (butt_col == blank_col && butt_row != blank_row && Math.Abs(butt_row - blank_row) == 1)
            {
                butt_array[blank_row, blank_col].Text = butt_array[butt_row, butt_col].Text; //changing the button text
                butt_array[butt_row, butt_col].Text = "16"; // making the old button the new blank button

                //flipping the visible button
                butt_array[butt_row, butt_col].Visible = false;
                butt_array[blank_row, blank_col].Visible = true;
            }
            else if (butt_col != blank_col && butt_row == blank_row && Math.Abs(butt_col - blank_col) == 1)
            {
                butt_array[blank_row, blank_col].Text = butt_array[butt_row, butt_col].Text; //changing the button text
                butt_array[butt_row, butt_col].Text = "16"; // making the old button the new blank button

                //flipping the visible button
                butt_array[butt_row, butt_col].Visible = false;
                butt_array[blank_row, blank_col].Visible = true;
            }  

            if (isWinner() == true)
            {
                winnerBox.Visible = true;

                //disable all buttons
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        butt_array[i, j].Enabled = false;
                    }
                }
            }
        }

        private bool isWinner()
        {
            int num = 1;
            bool isWinner = true;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (butt_array[i, j].Text == num++.ToString())
                        isWinner = true;
                    else
                    {
                        isWinner = false;
                        return isWinner;
                    }
                }
            }
            return isWinner;
        }
 
        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
