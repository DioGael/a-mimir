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

namespace a_mimir
{
    public partial class Form1 : Form
    {
        bool flag = false;
        private Timer countdownTimer;
        private int totalTimeInSeconds;
        
        private void InitializeTimer()
        {
            // Initialize the Timer
            countdownTimer = new Timer();
            countdownTimer.Interval = 1000; // 1000 milliseconds = 1 second
            countdownTimer.Tick += CountdownTimer_Tick;
        }
        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (totalTimeInSeconds > 0)
            {
                // Decrease the time by 1 second
                totalTimeInSeconds--;

                // Update the label with the formatted time
                timer.Text = TimeSpan.FromSeconds(totalTimeInSeconds).ToString(@"hh\:mm\:ss");
                if (timer.Text == "00:01:00" && flag)
                {
                    //ExecutePowerShellCommand("shutdown /s /f /p");
                }
                if (timer.Text == "00:00:00" && flag)
                {
                    countdownTimer.Stop();
                    flag = false;
                    //MessageBox.Show("Time's up!");
                }
            }
            else
            {
                // Stop the timer when the countdown reaches zero
                countdownTimer.Stop();
                flag = false;
                //MessageBox.Show("Time's up!");

            }
        }
        public int ConvertToSeconds(string time)
        {
            // Split the time string into hours, minutes, and seconds
            string[] parts = time.Split(':');
            switch (parts.Length)
            {
                case 1:
                    try
                    {
                        return int.Parse(time) * 3600;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid format");
                        return -1;
                    }
                case 2:
                    try
                    {
                        return int.Parse(parts[0]) * 3600 + int.Parse(parts[1]) * 60;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid format");
                        return -1;
                    }
                case 3:
                    try
                    {
                        return int.Parse(parts[0]) * 3600 + int.Parse(parts[1]) * 60 + int.Parse(parts[2]);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid format");
                        return -1;
                    }
                case 4:
                    try
                    {
                        return int.Parse(parts[0]) * 86400 + int.Parse(parts[1]) * 3600 + int.Parse(parts[2]) * 60 + int.Parse(parts[3]);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid format");
                        return -1;
                    }
                default:
                    MessageBox.Show("Invalid format");
                    return -1;
            }
        }
        public void startTimer ()
        {
            if (totalTimeInSeconds > 86399)
                totalTimeInSeconds = 86399;
            // Update the label with the initial time
            timer.Text = TimeSpan.FromSeconds(totalTimeInSeconds).ToString(@"hh\:mm\:ss");

            // Start the timer
            countdownTimer.Start();
        }
        private void ExecutePowerShellCommand(string command)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-Command \"{command}\"", // Use -Command to execute the command
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true // Hide the PowerShell window
                };

                using (Process process = new Process())
                {
                    process.StartInfo = processStartInfo;
                    process.Start();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                    // Display output or error
                    if (error != null && error != "")
                        MessageBox.Show(error, "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        public Form1()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            string url = "https://www.youtube.com/watch?v=dg4dmNvxdu0";
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);

        }

        private void button1hour_Click(object sender, EventArgs e)
        {
            totalTimeInSeconds = 3600;
            ExecutePowerShellCommand("shutdown /s /t " + totalTimeInSeconds);
            startTimer();
            
        }

        private void button2hour_Click(object sender, EventArgs e)
        {
            totalTimeInSeconds = 7200;
            ExecutePowerShellCommand("shutdown /s /t " + totalTimeInSeconds);
            startTimer();
        }

        private void button3hour_Click(object sender, EventArgs e)
        {
            totalTimeInSeconds = 10800;
            ExecutePowerShellCommand("shutdown /s /t " + totalTimeInSeconds);
            startTimer();
        }

        private void buttonCustomTime_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            if (form2.ShowDialog() == DialogResult.OK)
            {
                totalTimeInSeconds = ConvertToSeconds(form2.InputText);
                ExecutePowerShellCommand("shutdown /s /t " + totalTimeInSeconds);
                startTimer();
            }
            
        }
        private void timer_Click(object sender, EventArgs e)
        {

        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            ExecutePowerShellCommand("shutdown /a");
            totalTimeInSeconds = 0;
            InitializeTimer();
            timer.Text = "00:00:00";
            MessageBox.Show("Apagado abortado");
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip1.SetToolTip(label1, "ᓚᘏᗢ");
        }
    }
}
