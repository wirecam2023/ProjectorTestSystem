using DevComponents.DotNetBar;
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
    public partial class GetZhiDan : Office2007Form
    {
        ConfiguringCodePrefixes CCP;
        public GetZhiDan(ConfiguringCodePrefixes ccp)
        {
            InitializeComponent();
            this.CCP = ccp;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (textBoxX1.Text.Trim() == "")
            {
                MessageBox.Show("订单号不能为空！");
            }

            CCP.ZhiDan = textBoxX1.Text.Trim();
            this.Close();
        }
    }
}
