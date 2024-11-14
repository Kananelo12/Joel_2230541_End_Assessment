/*
-- =============================================
--Author:		Kananelo Joel
-- Student ID:  2230541
-- Batch NO:	Batch 2
-- =============================================
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Joel_2230541_End_Assessment
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
            // Call the method to start the progress bar animation
            startProgressBar();
        }

        // method to control progress bar
        private void startProgressBar()
        {
            Timer timer = new Timer { Interval = 20};
            int progress = 0;

            timer.Tick += (sender, e) =>
            {
                progress++;
                // Set the progress bar value
                progressBar1.Value = progress;
                // Update the label with percentage
                lblProgress.Text = progress + "%";

                if (progress >= 100)
                {
                    timer.Stop();  // Stop the timer when 100% is reached
                    progressBar1.ForeColor = Color.Blue;

                    // Redirect to login page
                    //new LoginForm().Show(); // due to showing it in Program.cs, it doubles the form.
                    // Close the splash screen
                    this.Close();
                }
            };

            timer.Start();
        }
    }
}
