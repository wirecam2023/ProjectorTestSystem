using DevComponents.DotNetBar;
using ProjectorInformation_Bll;
using ProjectorInformation_Model;
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
    public partial class ConfiguringCodePrefixes : Office2007Form
    {

        MainForm MF;

        public string ZhiDan { get; set; }

        ProjectorInformation_EncodingRulesBll pieBll = new ProjectorInformation_EncodingRulesBll();
        ProjectorInformation_SelectPrefixBll projeSelectBll = new ProjectorInformation_SelectPrefixBll();

        List<ProjectorInformation_EncodingRules> TreeList = new List<ProjectorInformation_EncodingRules>();


        string state = "browse";

        public ConfiguringCodePrefixes()
        {
            InitializeComponent();
        }

        public ConfiguringCodePrefixes(MainForm mf)
        {
            InitializeComponent();
            this.MF = mf;
        }

        private void ConfiguringCodePrefixes_Load(object sender, EventArgs e)
        {
            SetTreeView(-1);
            UpdateBtni();
        }

        void SetTreeView(int number)
        {
            treeView1.Nodes.Clear();
            TreeList.Clear();

            TreeList = pieBll.selectProjectorInformation_EncodingRulesBll("selectProjectorInformation_EncodingRules");

            TreeNode tn = new TreeNode();
            tn.Text = "前缀类型名";
            treeView1.Nodes.Add(tn);
            for (int i = 0; i < TreeList.Count; i++)
            {
                TreeNode tnChild = new TreeNode();
                tnChild.Text = TreeList[i].TypeName;
                tnChild.Tag = i;
                tn.Nodes.Add(tnChild);
            }
            if (number >= 0)
            {
                if (treeView1.Nodes[0].LastNode != null)
                {
                    treeView1.Nodes[0].Expand();
                    treeView1.SelectedNode = treeView1.Nodes[0].Nodes[number];
                }

            }
            else
            {
                if (treeView1.Nodes[0].LastNode != null)
                {
                    treeView1.Nodes[0].Expand();
                    treeView1.SelectedNode = treeView1.Nodes[0].LastNode;
                }
            }
        }

        private void btni_New_Click(object sender, EventArgs e)
        {
            state = "insert";
            SetTxtBox(new ProjectorInformation_EncodingRules(), false);
            UpdateBtni();
        }

        List<ProjectorInformation_EncodingRules> GetProjectorInformation_EncodingRules()
        {
            List<ProjectorInformation_EncodingRules> list = new List<ProjectorInformation_EncodingRules>();
            list.Add(new ProjectorInformation_EncodingRules
            {
                TypeName=txt_TypeName.Text.Trim(),
                Prefix_BodyCode=txt_Prefix_BodyCode.Text.Trim(),
                Prefix_OpticalCode=txt_Prefix_OpticalCode.Text.Trim(),
                Prefix_MotherboardEncoding=txt_Prefix_MotherboardEncoding.Text.Trim()
            });

            return list;
        }

        private void btni_sava_Click(object sender, EventArgs e)
        {
            if (state == "insert")
            {
                if (pieBll.insertProjectorInformation_EncodingRulesBll(GetProjectorInformation_EncodingRules(), "insertProjectorInformation_EncodingRules"))
                {
                    try
                    {
                        SetTreeView(-1);
                        MessageBox.Show("完成");
                        state = "browse";
                        UpdateBtni();
                    }
                    catch
                    {
                        MessageBox.Show("保存错误，或该前缀类型名已存在！");
                    }
                }
            }
            else if (state == "modefy")
            {
                if (pieBll.insertProjectorInformation_EncodingRulesBll(GetProjectorInformation_EncodingRules(), "updateProjectorInformation_EncodingRules"))
                {
                    try
                    {
                        SetTreeView(Convert.ToInt32(treeView1.SelectedNode.Tag));
                        MessageBox.Show("完成");
                        state = "browse";
                        UpdateBtni();
                    }
                    catch
                    {
                        MessageBox.Show("保存错误，或该前缀类型名已存在！");
                    }
                }
            }
        }

        private void btni_cancle_Click(object sender, EventArgs e)
        {
            state = "browse";
            UpdateBtni();
        }

        private void btni_modefy_Click(object sender, EventArgs e)
        {
            state = "modefy";
            UpdateBtni();
        }

        private void btni_delete_Click(object sender, EventArgs e)
        {
            if (pieBll.insertProjectorInformation_EncodingRulesBll(GetProjectorInformation_EncodingRules(), "deleteProjectorInformation_EncodingRules"))
            {
                try
                {
                    SetTreeView(-1);
                    MessageBox.Show("完成");
                    state = "browse";
                    UpdateBtni();
                }
                catch
                {
                    MessageBox.Show("保存错误，或该前缀类型名已存在！");
                }
            }
        }

        void UpdateBtni()
        {
            if (state == "browse")
            {
                btni_New.Enabled = true;
                btni_sava.Enabled = false;
                btni_cancle.Enabled = false;
                btni_delete.Enabled = true;
                btni_modefy.Enabled = true;
                btni_select.Enabled = true;

                treeView1.Enabled = true;
                panelEx1.Enabled = false;
            }
            else if (state == "modefy")
            {
                btni_New.Enabled = false;
                btni_sava.Enabled = true;
                btni_cancle.Enabled = true;
                btni_delete.Enabled = false;
                btni_modefy.Enabled = false;
                btni_select.Enabled = false;

                treeView1.Enabled = false;
                panelEx1.Enabled = true;

                txt_TypeName.Enabled = false;
            }
            else
            {
                btni_New.Enabled = false;
                btni_sava.Enabled = true;
                btni_cancle.Enabled = true;
                btni_delete.Enabled = false;
                btni_modefy.Enabled = false;
                btni_select.Enabled = false;

                treeView1.Enabled = false;
                panelEx1.Enabled = true;

                txt_TypeName.Enabled = true;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == treeView1.Nodes[0])
            {
                SetTxtBox(new ProjectorInformation_EncodingRules(), false);
                return;   
            }
            SetTxtBox(TreeList[Convert.ToInt32(e.Node.Tag)],true);
        }

        void SetTxtBox(ProjectorInformation_EncodingRules proje,bool bl)
        {
            if (bl)
            {
                txt_TypeName.Text = proje.TypeName;
                txt_Prefix_BodyCode.Text = proje.Prefix_BodyCode == null ? "" : proje.Prefix_BodyCode;
                txt_Prefix_OpticalCode.Text = proje.Prefix_OpticalCode == null ? "" : proje.Prefix_OpticalCode;
                txt_Prefix_MotherboardEncoding.Text = proje.Prefix_MotherboardEncoding == null ? "" : proje.Prefix_MotherboardEncoding;
            }
            else
            {
                txt_TypeName.Text = "";
                txt_Prefix_BodyCode.Text = "";
                txt_Prefix_OpticalCode.Text = "";
                txt_Prefix_MotherboardEncoding.Text = "";
            }
        }

        private void btni_select_Click(object sender, EventArgs e)
        {
            GetZhiDan gzd = new GetZhiDan(this);
            gzd.ShowDialog();
            try
            {
                if (projeSelectBll.updateProjectorInformation_SelectPrefixBll(GetProjectorInformation_SelectPrefix(TreeList[Convert.ToInt32(this.treeView1.SelectedNode.Tag)]), "updateProjectorInformation_SelectPrefix"))
                {
                    this.Close();
                    MF.SetProjectorInformation_SelectPrefix();
                }
                else MessageBox.Show("保存失败");

            }
            catch
            {
                MessageBox.Show("错误,请检查当前网络或配置!");
            }
        }

        ProjectorInformation_SelectPrefix GetProjectorInformation_SelectPrefix(ProjectorInformation_EncodingRules projeEncoding)
        {
            ProjectorInformation_SelectPrefix projeSelect = new ProjectorInformation_SelectPrefix();
            projeSelect.TypeName = projeEncoding.TypeName;
            projeSelect.Prefix_BodyCode = projeEncoding.Prefix_BodyCode;
            projeSelect.Prefix_OpticalCode = projeEncoding.Prefix_OpticalCode;
            projeSelect.Prefix_MotherboardEncoding = projeEncoding.Prefix_MotherboardEncoding;
            projeSelect.ZhiDan = this.ZhiDan;

            return projeSelect;
        }

    }
}
