using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectorInformation_Main
{
    public partial class DisplayInterface : Form
    {
        Timer timer;
        public DisplayInterface()
        {
            InitializeComponent();
        }

        private void DisplayInterface_Load(object sender, EventArgs e)
        {
            timer = new Timer();
            timer.Interval = 2000;
            timer.Tick += timer_Tick;
            timer.Enabled = true;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
