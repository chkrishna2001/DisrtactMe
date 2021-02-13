using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DistractMe
{
    public partial class Settings : Form
    {
        private SettingsManager settingsManager;
        private bool fromExit = false;
        public Settings()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            settingsManager.SaveSettings(new UserSettings()
            {
                DistractionInterval = (int)nudDistractInterval.Value,
                Messages = txtMessages.Lines,
                BlockInterval = (int)nudBlockInterval.Value,
                NoOfMessages = (int)nudNoofMessages.Value
            });
            mainTimer.Interval = (int)(nudDistractInterval.Value * 60 * 1000);
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            this.Hide();
            settingsManager = new SettingsManager();
            var userSettings = settingsManager.Get();
            nudDistractInterval.Value = userSettings.DistractionInterval;
            nudBlockInterval.Value = userSettings.BlockInterval;
            txtMessages.Lines = userSettings.Messages;
            nudNoofMessages.Value = userSettings.NoOfMessages > nudNoofMessages.Minimum ? userSettings.NoOfMessages : nudNoofMessages.Minimum;
            mainTimer.Interval = (int)(nudDistractInterval.Value * 60 * 1000);
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            DistractNow();
        }
        private void DistractNow()
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                Distractor screensaver = new Distractor(screen.Bounds);
                screensaver.SetDesktopBounds(screen.Bounds.X, screen.Bounds.Y, screen.Bounds.Width, screen.Bounds.Height);
                screensaver.Show();
            }
        }
        private void cmsNotify_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Tag.ToString())
            {
                case "configure":
                    this.Show();
                    break;
                case "exit":
                    fromExit = true;
                    Application.Exit();
                    break;
            }
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!fromExit)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            DistractNow();
        }
    }
}
