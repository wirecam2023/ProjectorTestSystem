using DevComponents.DotNetBar;
using ProjectorInformation_Bll;
using ProjectorInformation_Common;
using ProjectorInformation_Model;
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
    public partial class DataImport : Office2007Form
    {
        ProjectorInformation_MainTableBll pmtb = new ProjectorInformation_MainTableBll();

        SynchronizationContext my_context = null;

        string ZhiDan = "";
        public DataImport(string zhidan)
        {
            InitializeComponent();
            this.ZhiDan = zhidan;
        }

        private void btn_cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_selectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel文件(*.xls;*.xlsx)|*.xls;*.xlsx|所有文件|*.*";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string fileName = ofd.FileName;
                try
                {
                    Thread th = new Thread(delegate()
                    {
                        ExcelInput(fileName,checkBoxX1.Checked);
                    });
                    th.IsBackground = true;
                    th.Start();
                   // G.Setlb_QueryState("导入完成");
                    this.labelX2.Text = "开始写入";
                }
                catch (Exception ex)
                {
                    //G.Setlb_QueryState("可能导入的Excle文件无法识别");
                    MessageBox.Show(ex.Message);
                }
            }
        }

        void ExcelInput(string fileName,bool bl)
        {

            try
            {
                DataTable dt = ExcelHelperForCs.ImportFromExcel(fileName, "Sheet1", 0);
                List<ProjectorInformation_LD> list = new List<ProjectorInformation_LD>();
                if (bl)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        ProjectorInformation_LD ld = new ProjectorInformation_LD()
                        {
                            FuselageCode = item[4].ToString(),
                            IlluminationValue = item[5].ToString(),
                            LuminanceTestTime = Convert.ToDateTime(item[1].ToString()),
                            WiredMAC=item[2].ToString(),
                            wirelessMAC=item[3].ToString(),
                            ZhiDan=ZhiDan
                        };
                        list.Add(ld);
                    }
                    int i = 0;
                    foreach (ProjectorInformation_LD item in list)
                    {
                        i++;
                        if (pmtb.updateProjectorInformation_MainTable_LDBll(item, "updateProjectorInformation_MainTable_LD"))
                        {
                            my_context.Post(SetTextBox, "照度值写入成功！***" + " 机身码:" + item.FuselageCode + " ***时间：" + DateTime.Now.ToString() + "\r\n" + "\r\n");
                        }
                        else
                        {
                            my_context.Post(SetTextBox2, "照度值写入失败！***" + " 机身码:" + item.FuselageCode + " ***时间：" + DateTime.Now.ToString() + "\r\n" + "\r\n");
                        }

                        my_context.Post(SetLable, i.ToString() + "/" + list.Count.ToString());
                    }
                }
                else
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        ProjectorInformation_LD ld = new ProjectorInformation_LD()
                        {
                            WiredMAC=item[1].ToString(),
                            wirelessMAC=item[2].ToString(),
                            FuselageCode = item[3].ToString(),
                            IlluminationValue = item[4].ToString(),
                            ZhiDan=ZhiDan
                        };
                        list.Add(ld);
                    }
                    int i = 0;
                    foreach (ProjectorInformation_LD item in list)
                    {
                        i++;
                        if (pmtb.updateProjectorInformation_MainTable_LDZ2Bll(item, "updateProjectorInformation_MainTableLDZ2"))
                        {
                            my_context.Post(SetTextBox, "照度值写入成功！***" + " 机身码:" + item.FuselageCode + " ***时间：" + DateTime.Now.ToString() + "\r\n" + "\r\n");
                        }
                        else
                        {
                            my_context.Post(SetTextBox2, "照度值写入失败！***" + " 机身码:" + item.FuselageCode + " ***时间：" + DateTime.Now.ToString() + "\r\n" + "\r\n");
                        }

                        my_context.Post(SetLable, i.ToString() + "/" + list.Count.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void SetLable(object o)
        {
            string str = o as string;
            this.labelX2.Text = str;
        }

        void SetTextBox(object o)
        {
            string str = o as string;

            txt_message.AppendText(str);
        }

        void SetTextBox2(object o)
        {
            string str = o as string;

            txt_message2.AppendText(str);
        }

        private void DataImport_Load(object sender, EventArgs e)
        {
            my_context = SynchronizationContext.Current;
        }

        private void checkBoxX1_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxX2.Checked = !checkBoxX1.Checked;
        }

        private void checkBoxX2_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxX1.Checked = !checkBoxX2.Checked;
        }
    }
}
