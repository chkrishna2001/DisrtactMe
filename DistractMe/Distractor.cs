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
    public partial class Distractor : Form
    {
        private UserSettings settings;
        private SettingsManager sm;
        public Distractor()
        {
            InitializeComponent();
        }
        public Distractor(Rectangle bounds)
        {
            this.Bounds = bounds;
            Cursor.Hide();
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Black;
            InitializeComponent();
        }

        private void Distractor_Load(object sender, EventArgs e)
         {
            sm = new SettingsManager();
            settings = sm.Get();
            closeTimer.Interval = settings.BlockInterval * 1000;
            closeTimer.Enabled = true;
            var labels = getLabels(settings.Messages.Take(settings.NoOfMessages).ToList());
            labels.ForEach(m =>
            {
                this.Controls.Add(m);
            });
            this.Refresh();
        }

        private void closeTimer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textRotationTimer_Tick(object sender, EventArgs e)
        {
            foreach (Control item in this.Controls)
            {
                item.Location = new Point(item.Location.X + 10, item.Location.Y + 10);
            }


        }
        private List<Label> getLabels(List<string> messages)
        {
            try
            {
                var labelY = this.Location.Y / messages.Count;
                return messages.Select((m, i) =>
                {
                    var location = new Point(10, ((i + 1) * 100));
                    return new Label()
                    {
                        ForeColor = Color.White,
                        Font = new Font("Verdana", 20),
                        AutoSize = true,
                        Location = location,
                        Text = m,
                    };
                }).ToList();
            }
            catch (Exception ex)
            {
                this.Bounds = new Rectangle(11, 11, 11, 11);
            }
            return new List<Label>() {  };
        }

        private void Distractor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cursor.Show();
        }
    }
}
