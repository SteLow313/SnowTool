using Guna.UI2.Native;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snow
{

    public partial class Snow : Form
    {
        private static readonly Initializer Initializer = new Initializer();
        private static readonly Scanner Scanner = new Scanner();
        public Snow()
        {
            InitializeComponent();
            Region = Region.FromHrgn(WinApi.CreateRoundRectRgn(0, 0, Width, Height, 8, 8));
        }
        private void Snow_Load(object sender, EventArgs e)
        {
            guna2ProgressBar1.Hide();
            label2.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/YUNqmMJCyb");
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            label3.Hide();
            guna2CustomCheckBox1.Hide();
            if (guna2CustomCheckBox1.Checked)
            {
                Program.ShowConsole();
            }
            else
            {
                Program.HideConsole();
            }
            stringsHelper.Print("Initializing...");
            if (randomsHelper.isbanned)
            {
                stringsHelper.sendTGMessage($@"User {stringsHelper.username} with the hwid #{stringsHelper.finalHwid} tried to start the tool but is banned for the reason: " + stringsHelper.reason);
                label2.Text = "USER BANNED FOR THE REASON: " + stringsHelper.reason;
                label2.Show();
                guna2Button1.Hide();
            }
            else
            {
                guna2Button1.Hide();
                guna2ProgressBar1.Show();
                timer1.Start();
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {

            if (guna2ProgressBar1.Value < 2)
            {
                guna2ProgressBar1.Increment(1);
            }
            else
            {
                timer1.Stop();
                Task.Run(() => Initializer.startInitializer().Wait());
                Task.Run(() => Scanner.startScanners());
            }
        }
    }
}