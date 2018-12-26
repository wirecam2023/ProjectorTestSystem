using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectorInformation_Bll;

namespace ProjectorInformation_Main
{
    public partial class Admini_Login : Office2007Form
    {
        ProjectorInformation_AdminiBll pab = new ProjectorInformation_AdminiBll();

        MainForm MF;
        public Admini_Login()
        {
            InitializeComponent();
        }

        public Admini_Login(MainForm mf)
        {
            InitializeComponent();
            this.MF = mf;
        }
        private void checkBoxX1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxX1.Checked)
            {
                txt_Password.PasswordChar = new char();
            }
            else
            {
                txt_Password.PasswordChar = '*';
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (pab.selectCountProjectorInformation_AdminiBll(txt_Password.Text, "selectCountProjectorInformation_Admini"))
            {
                MF.SetAdminiHelp();
                this.Close();
            }
            else
            {
                MessageBox.Show("密码错误！", "系统提示", MessageBoxButtons.OK);
                txt_Password.SelectAll();
            }
        }
    }
}
