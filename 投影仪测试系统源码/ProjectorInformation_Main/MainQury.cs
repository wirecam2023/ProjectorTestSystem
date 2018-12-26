using DevComponents.DotNetBar;
using ProjectorInformation_Bll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ProjectorInformation_Main
{
    public partial class MainQury : Office2007Form
    {
        ProjectorInformation_MainTableBll pmtb = new ProjectorInformation_MainTableBll();

        MainForm MF;

        Thread th;

        SynchronizationContext My_context = null;

        public MainQury()
        {
            InitializeComponent();
        }

        public MainQury(MainForm mf)
        {
            InitializeComponent();
            this.MF = mf;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            ProjectorInformation_Common.DeleteCondition.FuselageCode = this.txt_FuselageCode_ZJM.Text.Trim();
            ProjectorInformation_Common.DeleteCondition.ZhiDan = this.txt_ZhiDan_ZJM.Text.Trim();
            ProjectorInformation_Common.DeleteCondition.OpticalCode = txt_OpticalCode_ZJM.Text.Trim();
            ProjectorInformation_Common.DeleteCondition.MainBoardCode = txt_MainBoardCode_ZJM.Text.Trim();
            ProjectorInformation_Common.DeleteCondition.WiredMAC = txt_WiredMAC_ZJM.Text.Trim();
            ProjectorInformation_Common.DeleteCondition.wirelessMAC = txt_wirelessMAC_ZJM.Text.Trim();

            th = new Thread(delegate()
            {
                queryZJMth(this.txt_FuselageCode_ZJM.Text.Trim(), this.txt_ZhiDan_ZJM.Text.Trim(), txt_OpticalCode_ZJM.Text.Trim(), txt_MainBoardCode_ZJM.Text.Trim(), txt_WiredMAC_ZJM.Text.Trim(), txt_wirelessMAC_ZJM.Text.Trim());
            });
            th.IsBackground = true;
            th.Start();
        }

        void queryZJMth(string txt_FuselageCode_ZJM, string zhidan, string OpticalCode, string MainBoardCode, string WiredMAC, string wirelessMAC)
        {
            My_context.Post(SetDGV_ZJM, pmtb.selectProjectorInformation_MainTableBll(txt_FuselageCode_ZJM, zhidan, OpticalCode, MainBoardCode, WiredMAC, wirelessMAC, "selectProjectorInformation_MainTable"));
        }

        private void MainQury_Load(object sender, EventArgs e)
        {
            My_context = SynchronizationContext.Current;
        }

        void SetDGV_ZJM(object o)
        {
            MF.SetDGV_ZJM(o);
            this.Close();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
