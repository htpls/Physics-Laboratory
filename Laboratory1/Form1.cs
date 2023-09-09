using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratory1
{
    public partial class Form1 : Form
    {
        double elapsedSecondsBoost = 0;
        Stopwatch stopwatchBoost = new Stopwatch();

        double elapsedSecondsBraking = 0;
        Stopwatch stopwatchBraking = new Stopwatch();

        double power = 108118.5;
        double mass = 1470;
        double coefficientFriction = 0.8;
        double g = 9.8;

        double speed = 0;
        double speedConst = 0;
        double acceleration = 0;
        double speedOutput = 0;

        public Form1()
        {
            InitializeComponent();
            
            this.KeyDown += new KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new KeyEventHandler(this.Form1_KeyUp);
        }

        private void outputInformation()
        {
            speedOutput = speed * 3.6;
            label4.Text = speedOutput.ToString("F2") + " км/ч";
            label5.Text = stopwatchBoost.Elapsed.ToString();
            label6.Text = stopwatchBraking.Elapsed.ToString();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.W)
            {
                stopwatchBoost.Start();

                //elapsedSecondsBoost = stopwatchBoost.ElapsedMilliseconds;
                elapsedSecondsBoost = stopwatchBoost.Elapsed.Seconds;

                acceleration = Math.Sqrt((2 * power) / (mass * elapsedSecondsBoost));
                speed = (acceleration * elapsedSecondsBoost);   // м/с

                outputInformation();
            }

            if (e.KeyCode == Keys.S)
            {
                stopwatchBraking.Start();

                elapsedSecondsBraking = stopwatchBraking.Elapsed.Seconds;

                speed = speedConst - (coefficientFriction * g * elapsedSecondsBraking);

                outputInformation();
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                stopwatchBoost.Stop();

                speedConst = speed;
            }

            if (e.KeyCode == Keys.S)
            {
                stopwatchBraking.Stop();

                speedConst = speed;
            }
        }
    }
}
