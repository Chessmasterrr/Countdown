using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Countdown
{
    public partial class main_window : Form
    {
        private int counter; // overall counter
        private int invisible; // time to get the counter invisible
        private List<KeyValuePair<string, int>> keys = new List<KeyValuePair<string, int>>(); // list of pressed keys


        public main_window()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Format an int to a string with dot.
        /// </summary>
        /// <param name="counter"></param>
        /// <returns></returns>
        private string formatTime(int counter)
        {
            return (counter / 10).ToString() + "." + (counter % 10).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button_start.Text == "Start")
            {
                // start countdown
                Random rand = new Random();
                button_start.Text = "Stopp";
                counter = rand.Next(200) + 200; // min 20 seconds + random 20 seconds
                invisible = rand.Next(150) + 50; // min 5 seconds + random 15 seconds
                label_timer.Text = formatTime(counter);
                label_timer.Visible = true;
                label_result.Visible = false;
                keys.Clear();
                timer1.Start();
            } else
            {
                // stop countdown and show keys
                label_timer.Visible = false;
                label_result.Text = "";
                // show keys
                foreach (KeyValuePair<string, int> pair in keys)
                {
                    if (pair.Value < 0)
                    {
                        label_result.Text += "Taste " + pair.Key + ": -" + (pair.Value *(-1) / 10).ToString() + "." + (pair.Value * (-1) % 10).ToString() + Environment.NewLine;
                    }
                    else {
                        label_result.Text += "Taste " + pair.Key + ": " + (pair.Value / 10).ToString() + "." + (pair.Value % 10).ToString() + Environment.NewLine;
                    }
                }
                label_result.Visible = true;
                timer1.Stop();
                button_start.Text = "Start";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            counter = counter - 1;
            if (counter > invisible)
            {
                label_timer.Text = formatTime(counter);
            } else
            {
                label_timer.Visible = false;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            keys.Add(new KeyValuePair<string, int>(e.KeyData.ToString(), counter));
        }
    }
}
