using DevComponents.DotNetBar;
using System.Threading;
using ProjectorInformation_Bll;
using ProjectorInformation_Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectorInformation_Common;
using System.Management;
using System.Globalization;

namespace ProjectorInformation_Main
{
    public partial class MainForm : Office2007Form
    {
        ProjectorInformation_MainTableBll pmtb = new ProjectorInformation_MainTableBll();
        ProjectorInformation_ModelBll pimb = new ProjectorInformation_ModelBll();
        ProjectorInformation_MACBll pimac = new ProjectorInformation_MACBll();
        ProjectorInformation_SelectPrefixBll projeSelectBll = new ProjectorInformation_SelectPrefixBll();

        List<ProjectorInformation_DG> DataList_DG = new List<ProjectorInformation_DG>();
        List<ProjectorInformation_MainBoard> DataList_ZB = new List<ProjectorInformation_MainBoard>();
        List<ProjectorInformation_LHQ_STR> DataList_LHQ = new List<ProjectorInformation_LHQ_STR>();
        List<ProjectorInformation_LHS> DataList_LHS = new List<ProjectorInformation_LHS>();
        List<ProjectorInformation_LHX> DataList_LHX = new List<ProjectorInformation_LHX>();
        List<ProjectorInformation_LHH_Str> DataList_LHH = new List<ProjectorInformation_LHH_Str>();
        List<ProjectorInformation_LD> DataList_LD = new List<ProjectorInformation_LD>();
        List<ProjectorInformation_WX> DataList_WX = new List<ProjectorInformation_WX>();
        List<ProjectorInformation_BZ> DataList_BZ = new List<ProjectorInformation_BZ>();

        private ProjectorInformation_SelectPrefix projeSelect;

        Thread th;
        SynchronizationContext My_context = null;

        List<ProjectorInformation_MainTable_STR> DataList = new List<ProjectorInformation_MainTable_STR>();
        public MainForm()
        {
            InitializeComponent();
        }

        private void superTabItem2_Click(object sender, EventArgs e)
        {
            AdminiHelp.LHSX = false;
        }

        private void btni_save_DG_Click(object sender, EventArgs e)
        {
            this.btni_save_DG_save();
        }

        bool MssageDG()
        {
            if (txt_FuselageCode_DG.Text.Trim() == "")
            {
                MessageBox.Show("机身号不能为空!");
                return true;
            }
            else if (txt_OpticalCode_DG.Text.Trim() == ""&& ck_OpticalCode_DG.Checked)
            {
                MessageBox.Show("光机号不能为空!");
                return true;
            }
            else if (txt_FuselageCode_DGZB.Text.Trim() == ""&&ck_FuselageCode_DGZB.Checked)
            {
                MessageBox.Show("主板编码不能为空!");
                return true;
            }
            else if (txt_ZhiDan_DG.Text.Trim() == "")
            {
                MessageBox.Show("订单号不能为空！");
                return true;
            }
            else
            {
                return false;
            }
        }

        ProjectorInformation_DG GetProjectorInformation_DG()
        {
            ProjectorInformation_DG dg = new ProjectorInformation_DG()
            {
                FuselageCode=txt_FuselageCode_DG.Text.Trim(),
                OpticalCode= ck_OpticalCode_DG.Checked?txt_OpticalCode_DG.Text.Trim():"",
                PolishingMachineTime=DateTime.Now,
                MainBoardCode= ck_FuselageCode_DGZB.Checked? txt_FuselageCode_DGZB.Text.Trim():"",
                MainBoardTime=DateTime.Now,
                ZhiDan=txt_ZhiDan_DG.Text.Trim()
            };
            return dg;
        }

        /// <summary>
        /// 打光机保存
        /// </summary>
        void btni_save_DG_save()
        {
            if (MssageDG())
            {
                return;
            }
            this.lab_Mssage1.Text = "正在保存";
            try
            {
                if (pmtb.selectCountProjectorInformation_MainTableBll(txt_FuselageCode_DG.Text.Trim(), txt_ZhiDan_DG.Text.Trim(), "selectCountProjectorInformation_MainTable"))
                {
                    if (pmtb.updateProjectorInformation_MainTableBll(this.GetProjectorInformation_DG(), "updateProjectorInformation_MainTable"))
                    {
                        DGV_DG.DataSource = new List<ProjectorInformation_DG>();
                        DataList_DG.Insert(0, this.GetProjectorInformation_DG());
                        if (DataList_DG.Count > 100)
                        {
                            DataList_DG.RemoveAt(DataList_DG.Count - 1);
                        }
                        DGV_DG.DataSource = DataList_DG;
                        this.lab_Mssage1.Text = "完成";
                        txt_FuselageCode_DG.Clear();
                        txt_OpticalCode_DG.Clear();
                        txt_FuselageCode_DGZB.Clear();
                        txt_FuselageCode_DG.Focus();
                        txt_OpticalCode_DG.Enabled = false;
                        txt_FuselageCode_DGZB.Enabled = false;
                    }
                    else { this.lab_Mssage1.Text = "保存异常"; }
                }
                else
                {
                    if (pmtb.insertProjectorInformation_MainTableBll(this.GetProjectorInformation_DG(), "insertProjectorInformation_MainTable"))
                    {
                        DGV_DG.DataSource = new List<ProjectorInformation_DG>();
                        DataList_DG.Insert(0, this.GetProjectorInformation_DG());
                        if (DataList_DG.Count > 100)
                        {
                            DataList_DG.RemoveAt(DataList_DG.Count - 1);
                        }
                        DGV_DG.DataSource = DataList_DG;

                        this.lab_Mssage1.Text = "完成";
                        txt_FuselageCode_DG.Clear();
                        txt_OpticalCode_DG.Clear();
                        txt_FuselageCode_DGZB.Clear();
                        txt_FuselageCode_DG.Focus();
                        txt_OpticalCode_DG.Enabled = false;
                        txt_FuselageCode_DGZB.Enabled = false;
                    }
                    else { this.lab_Mssage1.Text = "保存异常"; }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void superTabItem3_Click(object sender, EventArgs e)
        {
            AdminiHelp.LHSX = false;
        }

        private void superTabItem4_Click(object sender, EventArgs e)
        {
            AdminiHelp.LHSX = false;
        }

        private void superTabItem5_Click(object sender, EventArgs e)
        {
            AdminiHelp.LHSX = true;
        }

        private void superTabItem6_Click(object sender, EventArgs e)
        {
            AdminiHelp.LHSX = true;
        }

        private void superTabItem7_Click(object sender, EventArgs e)
        {
            AdminiHelp.LHSX = false;
        }

        private void superTabItem8_Click(object sender, EventArgs e)
        {
            AdminiHelp.LHSX = false;
        }

        private void superTabItem9_Click(object sender, EventArgs e)
        {
            AdminiHelp.LHSX = false;
        }

        private void superTabItem10_Click(object sender, EventArgs e)
        {
            AdminiHelp.LHSX = false;
        }

        private void superTabItem1_Click(object sender, EventArgs e)
        {
            AdminiHelp.LHSX = false;
        }

        ProjectorInformation_MainBoard GetProjectorInformation_MainBoard()
        {
            ProjectorInformation_MainBoard pmb = new ProjectorInformation_MainBoard()
            {
                MainBoardCode = txt_FuselageCode_ZB.Text.Trim(),
                MainBoardTime=DateTime.Now
            };
            return pmb;
        }

        bool MessageZB()
        {
            if (txt_FuselageCode_ZB.Text.Trim() == "")
            {
                MessageBox.Show("主板编码不能为空！");
                return true;
            }
            else
                return false;
        }
        private void btni_save_ZB_Click(object sender, EventArgs e)
        {
            this.saveZB();
        }

        void saveZB()
        {
            if (this.MessageZB())
            {
                return;
            }
            this.lab_Mssage1.Text = "正在保存";
            if (pimb.selectCountProjectorInformation_MainBoardBll(txt_FuselageCode_ZB.Text.Trim(), "selectCountProjectorInformation_MainBoard"))
            {
                if (pimb.insertProjectorInformation_MainBoardBll(this.GetProjectorInformation_MainBoard(), "updateProjectorInformation_MainTable"))
                {
                    DGV_ZB.DataSource = new List<ProjectorInformation_MainBoard>();
                    DataList_ZB.Insert(0, this.GetProjectorInformation_MainBoard());
                    if (DataList_ZB.Count > 100)
                    {
                        DataList_ZB.RemoveAt(DataList_ZB.Count - 1);
                    }                    
                    DGV_ZB.DataSource = DataList_ZB;

                    this.lab_Mssage1.Text = "完成";
                    txt_FuselageCode_ZB.Clear();
                    txt_FuselageCode_ZB.Focus();
                }
                else { this.lab_Mssage1.Text = "保存异常"; }
            }
            else
            {
                if (pimb.insertProjectorInformation_MainBoardBll(this.GetProjectorInformation_MainBoard(), "insertProjectorInformation_MainBoard"))
                {
                    DGV_ZB.DataSource = new List<ProjectorInformation_MainBoard>();
                    DataList_ZB.Insert(0, this.GetProjectorInformation_MainBoard());
                    if (DataList_ZB.Count > 100)
                    {
                        DataList_ZB.RemoveAt(DataList_ZB.Count - 1);
                    }   
                    DGV_ZB.DataSource = DataList_ZB;

                    this.lab_Mssage1.Text = "完成";
                    txt_FuselageCode_ZB.Clear();
                    txt_FuselageCode_ZB.Focus();
                }
                else { this.lab_Mssage1.Text = "保存异常"; }
            }
        }
        /// <summary>
        /// 打主板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_FuselageCode_ZB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (txt_FuselageCode_ZB.Text.Trim()=="")
                {
                    return;
                }

                this.saveZB();
            }
        }
        /// <summary>
        /// 老化前测试位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btni_save_LHQ_Click(object sender, EventArgs e)
        {
            this.save_LHQ();
        }

        ProjectorInformation_LHQ GetProjectorInformation_LHQ()
        {
            ProjectorInformation_LHQ plhq = new ProjectorInformation_LHQ()
            {
                FuselageCode=txt_FuselageCode_LHQ.Text.Trim(),
                PreAgingTestTime=DateTime.Now
            };
            return plhq;
        }

        ProjectorInformation_LHQ_STR GetProjectorInformation_LHQ_STR(ProjectorInformation_LHQ plhq)
        {
            ProjectorInformation_LHQ_STR plhqstr = new ProjectorInformation_LHQ_STR()
            {
                FuselageCode=plhq.FuselageCode,
                PreAgingTestTime=plhq.PreAgingTestTime.ToString(),
                PreAgingTestTime2 = plhq.PreAgingTestTime2 == null ? "" : plhq.PreAgingTestTime2
            };
            return plhqstr;
        }

        bool MessageLHQ()
        {
            if (txt_FuselageCode_LHQ.Text.Trim() == "")
            {
                MessageBox.Show("机身码不能为空！");
                return true;
            }
            else return false;
        }
        /// <summary>
        /// 老化前测试位
        /// </summary>
        void save_LHQ()
        {
            if (MessageLHQ()) return;

            this.lab_Mssage1.Text = "正在保存";
            try
            {
                if (pmtb.selectCountProjectorInformation_MainTableBll(txt_FuselageCode_LHQ.Text.Trim(), txt_ZhiDan_DG.Text.Trim(), "selectCountProjectorInformation_MainTable"))
                {
                    ProjectorInformation_LHQ projeLHQ = pmtb.selectProjectorInformation_MainTable_LHQBll(txt_FuselageCode_LHQ.Text.Trim(), "selectProjectorInformation_MainTable_LHQ");
                    if (projeLHQ.PreAgingTestTime.Year == 1993)
                    {
                        //第一次测试
                        ProjectorInformation_LHQ projeLHQ1 = new ProjectorInformation_LHQ()
                        {
                            FuselageCode = txt_FuselageCode_LHQ.Text.Trim(),
                            PreAgingTestTime = DateTime.Now,
                            PreAgingTestTime2 = null
                        };
                        if (pmtb.updateProjectorInformation_MainTable_LHQBll(projeLHQ1.FuselageCode, projeLHQ1.PreAgingTestTime, "updateProjectorInformation_MainTable_LHQ"))
                        {
                            DGV_LHQ.DataSource = new List<ProjectorInformation_LHQ_STR>();
                            DataList_LHQ.Insert(0, this.GetProjectorInformation_LHQ_STR(projeLHQ1));
                            if (DataList_LHQ.Count > 100)
                            {
                                DataList_LHQ.RemoveAt(DataList_LHQ.Count - 1);
                            }
                            DGV_LHQ.DataSource = DataList_LHQ;
                            this.lab_Mssage1.Text = "完成";
                            txt_FuselageCode_LHQ.Clear();
                            txt_FuselageCode_LHQ.Focus();
                        }
                        else { this.lab_Mssage1.Text = "保存异常"; }


                    }
                    else
                    {
                        //第二次测试
                        ProjectorInformation_LHQ projeLHQ1 = new ProjectorInformation_LHQ()
                        {
                            FuselageCode = txt_FuselageCode_LHQ.Text.Trim(),
                            PreAgingTestTime = projeLHQ.PreAgingTestTime,
                            PreAgingTestTime2 = DateTime.Now.ToString()
                        };
                        if (pmtb.updateProjectorInformation_MainTable_LHQBll(projeLHQ1.FuselageCode, Convert.ToDateTime(projeLHQ1.PreAgingTestTime2), "updateProjectorInformation_MainTable_LHQ1"))
                        {
                            DGV_LHQ.DataSource = new List<ProjectorInformation_LHQ_STR>();
                            DataList_LHQ.Insert(0, this.GetProjectorInformation_LHQ_STR(projeLHQ1));
                            if (DataList_LHQ.Count > 100)
                            {
                                DataList_LHQ.RemoveAt(DataList_LHQ.Count - 1);
                            }
                            DGV_LHQ.DataSource = DataList_LHQ;
                            this.lab_Mssage1.Text = "完成";
                            txt_FuselageCode_LHQ.Clear();
                            txt_FuselageCode_LHQ.Focus();
                        }
                        else { this.lab_Mssage1.Text = "保存异常"; }
                    }
                }
                else
                {
                    //if (pmtb.insertProjectorInformation_MainTable_LHQBll(this.GetProjectorInformation_LHQ(), "insertProjectorInformation_MainTable_LHQ"))
                    //{
                    //    DGV_LHQ.DataSource = new List<ProjectorInformation_LHQ_STR>();
                    //    DataList_LHQ.Insert(0, this.GetProjectorInformation_LHQ_STR(this.GetProjectorInformation_LHQ()));
                    //    if (DataList_LHQ.Count > 100)
                    //    {
                    //        DataList_LHQ.RemoveAt(DataList_LHQ.Count - 1);
                    //    }
                    //    DGV_LHQ.DataSource = DataList_LHQ;
                    //    this.lab_Mssage1.Text = "完成";
                    //    txt_FuselageCode_LHQ.Clear();
                    //    txt_FuselageCode_LHQ.Focus();
                    //}
                    //else { this.lab_Mssage1.Text = "保存异常"; }
                    txt_FuselageCode_LHQ.Text = "";
                    MessageBox.Show("该机身码不存在！");
                    txt_FuselageCode_LHQ.SelectAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        /// <summary>
        /// 老化（上架时间）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btni_save_LHS_Click(object sender, EventArgs e)
        {
            this.saveLHS();
        }

        bool MessageLHS()
        {
            if (txt_FuselageCode_LHS.Text.Trim() == "")
            {
                MessageBox.Show("机身码不能为空！");
                return true;
            }
            else return false;
        }

        ProjectorInformation_LHS GetProjectorInformation_LHS()
        {
            ProjectorInformation_LHS lhs = new ProjectorInformation_LHS()
            {
                FuselageCode=txt_FuselageCode_LHS.Text.Trim(),
                AgeingBeginTime=DateTime.Now
            };
            return lhs;
        }

        void saveLHS()
        {
            if (this.MessageLHS()) return;

            this.lab_Mssage1.Text = "正在保存";
            try
            {
                if (pmtb.selectCountProjectorInformation_MainTableBll(txt_FuselageCode_LHS.Text.Trim(), txt_ZhiDan_DG.Text.Trim(), "selectCountProjectorInformation_MainTable"))
                {
                    List<ProjectorInformation_MainTable_STR> LHSlist = pmtb.selectProjectorInformation_MainTableBll(txt_FuselageCode_LHS.Text.Trim(), "", "", "", "", "", "selectProjectorInformation_MainTable");
                    if (LHSlist[0].PreAgingTestTime == "")
                    {
                        MessageBox.Show("错误，该产品没有老化前测试！");
                        return;
                    }
                    if (pmtb.updateProjectorInformation_MainTable_LHSBll(this.GetProjectorInformation_LHS(), "updateProjectorInformation_MainTable_LHS"))
                    {
                        DGV_LHS.DataSource = new List<ProjectorInformation_LHS>();
                        DataList_LHS.Insert(0, this.GetProjectorInformation_LHS());
                        if (DataList_LHS.Count > 100)
                        {
                            DataList_LHS.RemoveAt(DataList_LHS.Count - 1);
                        }
                        DGV_LHS.DataSource = DataList_LHS;

                        this.lab_Mssage1.Text = "完成";
                        txt_FuselageCode_LHS.Clear();
                        txt_FuselageCode_LHS.Focus();
                    }
                    else { this.lab_Mssage1.Text = "保存异常"; }
                }
                else
                {
                    //if (pmtb.insertProjectorInformation_MainTable_LHSBll(this.GetProjectorInformation_LHS(), "insertProjectorInformation_MainTable_LHS"))
                    //{
                    //    DGV_LHS.DataSource = new List<ProjectorInformation_LHS>();
                    //    DataList_LHS.Insert(0, this.GetProjectorInformation_LHS());
                    //    if (DataList_LHS.Count > 100)
                    //    {
                    //        DataList_LHS.RemoveAt(DataList_LHS.Count - 1);
                    //    }
                    //    DGV_LHS.DataSource = DataList_LHS;

                    //    this.lab_Mssage1.Text = "完成";
                    //    txt_FuselageCode_LHS.Clear();
                    //    txt_FuselageCode_LHS.Focus();
                    //}
                    //else { this.lab_Mssage1.Text = "保存异常"; }
                    txt_FuselageCode_LHS.Text = "";
                    MessageBox.Show("该机身码不存在！");
                    txt_FuselageCode_LHS.SelectAll();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 老化下架时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btni_save_LHX_Click(object sender, EventArgs e)
        {
            this.saveLHX();
        }

        bool MessageLHX()
        {
            if (txt_FuselageCode_LHX.Text.Trim() == "")
            {
                MessageBox.Show("机身码不能为空！");
                return true;
            }
            else return false;
        }

        ProjectorInformation_LHX GetProjectorInformation_LHX()
        {
            ProjectorInformation_LHX lhs = new ProjectorInformation_LHX()
            {
                FuselageCode = txt_FuselageCode_LHX.Text.Trim(),
                AgeingEndTime = DateTime.Now
            };
            return lhs;
        }

        void saveLHX()
        {
            if (this.MessageLHX()) return;

            this.lab_Mssage1.Text = "正在保存";
            try
            {
                if (pmtb.selectCountProjectorInformation_MainTableBll(txt_FuselageCode_LHX.Text.Trim(), txt_ZhiDan_DG.Text.Trim(), "selectCountProjectorInformation_MainTable"))
                {
                    List<ProjectorInformation_MainTable_STR> LHXlist = pmtb.selectProjectorInformation_MainTableBll(txt_FuselageCode_LHX.Text.Trim(), "", "", "", "", "", "selectProjectorInformation_MainTable");
                    if (LHXlist[0].AgeingBeginTime == "")
                    {
                        MessageBox.Show("错误，该产品没有老化上架！");
                        return;
                    }

                    if (pmtb.updateProjectorInformation_MainTable_LHXBll(this.GetProjectorInformation_LHX(), "updateProjectorInformation_MainTable_LHX"))
                    {
                        DGV_LHX.DataSource = new List<ProjectorInformation_LHX>();
                        DataList_LHX.Insert(0, this.GetProjectorInformation_LHX());
                        if (DataList_LHX.Count > 100)
                        {
                            DataList_LHX.RemoveAt(DataList_LHX.Count - 1);
                        }
                        DGV_LHX.DataSource = DataList_LHX;

                        this.lab_Mssage1.Text = "完成";
                        txt_FuselageCode_LHX.Clear();
                        txt_FuselageCode_LHX.Focus();
                    }
                    else { this.lab_Mssage1.Text = "保存异常"; }
                }
                else
                {
                    //if (pmtb.insertProjectorInformation_MainTable_LHXBll(this.GetProjectorInformation_LHX(), "insertProjectorInformation_MainTable_LHX"))
                    //{
                    //    DGV_LHX.DataSource = new List<ProjectorInformation_LHX>();
                    //    DataList_LHX.Insert(0, this.GetProjectorInformation_LHX());
                    //    if (DataList_LHX.Count > 100)
                    //    {
                    //        DataList_LHX.RemoveAt(DataList_LHX.Count - 1);
                    //    }
                    //    DGV_LHX.DataSource = DataList_LHX;

                    //    this.lab_Mssage1.Text = "完成";
                    //    txt_FuselageCode_LHX.Clear();
                    //    txt_FuselageCode_LHX.Focus();
                    //}
                    //else { this.lab_Mssage1.Text = "保存异常"; }
                    txt_FuselageCode_LHX.Text = "";
                    MessageBox.Show("该机身码不存在!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 老化后测试位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btni_save_LHH_Click(object sender, EventArgs e)
        {
            this.saveLHH();
        }

        ProjectorInformation_LHH GetProjectorInformation_LHH()
        {
            ProjectorInformation_LHH plhH = new ProjectorInformation_LHH()
            {
                FuselageCode = txt_FuselageCode_LHQ.Text.Trim(),
                PostAgingTestTime = DateTime.Now
            };
            return plhH;
        }

        ProjectorInformation_LHH_Str GetProjectorInformation_LHH_Str(ProjectorInformation_LHH plhh)
        {
            ProjectorInformation_LHH_Str plhqstr = new ProjectorInformation_LHH_Str()
            {
                FuselageCode = plhh.FuselageCode,
                PostAgingTestTime = plhh.PostAgingTestTime.ToString(),
                PostAgingTestTime2 = plhh.PostAgingTestTime2 == null ? "" : plhh.PostAgingTestTime2
            };
            return plhqstr;
        }
        bool MessageLHH()
        {
            if (txt_FuselageCode_LHH.Text.Trim() == "")
            {
                MessageBox.Show("机身码不能为空！");
                return true;
            }
            else return false;
        }

        void saveLHH()
        {
            if (this.MessageLHH()) return;

            this.lab_Mssage1.Text = "正在保存";
            try
            {
                if (pmtb.selectCountProjectorInformation_MainTableBll(txt_FuselageCode_LHH.Text.Trim(), txt_ZhiDan_DG.Text.Trim(), "selectCountProjectorInformation_MainTable"))
                {
                    List<ProjectorInformation_MainTable_STR> LHHlist = pmtb.selectProjectorInformation_MainTableBll(txt_FuselageCode_LHH.Text.Trim(), "", "", "", "", "", "selectProjectorInformation_MainTable");
                    if (LHHlist[0].AgeingEndTime == "")
                    {
                        MessageBox.Show("错误，该产品没有老化下架！");
                        return;
                    }

                    ProjectorInformation_LHH projeLHH = pmtb.selectProjectorInformation_MainTable_LHHBll(txt_FuselageCode_LHH.Text.Trim(), "selectProjectorInformation_MainTable_LHH");
                    if (projeLHH.PostAgingTestTime.Year == 1993)
                    {
                        //第一次测试
                        ProjectorInformation_LHH projeLHQ1 = new ProjectorInformation_LHH()
                        {
                            FuselageCode = txt_FuselageCode_LHH.Text.Trim(),
                            PostAgingTestTime = DateTime.Now,
                            PostAgingTestTime2 = null
                        };
                        if (pmtb.updateProjectorInformation_MainTable_LHHBll(projeLHQ1.FuselageCode, projeLHQ1.PostAgingTestTime, "updateProjectorInformation_MainTable_LHH"))
                        {
                            DGV_LHH.DataSource = new List<ProjectorInformation_LHH_Str>();
                            DataList_LHH.Insert(0, this.GetProjectorInformation_LHH_Str(projeLHQ1));
                            if (DataList_LHH.Count > 100)
                            {
                                DataList_LHH.RemoveAt(DataList_LHH.Count - 1);
                            }
                            DGV_LHH.DataSource = DataList_LHH;
                            this.lab_Mssage1.Text = "完成";
                            txt_FuselageCode_LHH.Clear();
                            txt_FuselageCode_LHH.Focus();
                        }
                        else { this.lab_Mssage1.Text = "保存异常"; }


                    }
                    else
                    {
                        //第二次测试
                        ProjectorInformation_LHH projeLHQ1 = new ProjectorInformation_LHH()
                        {
                            FuselageCode = txt_FuselageCode_LHH.Text.Trim(),
                            PostAgingTestTime = projeLHH.PostAgingTestTime,
                            PostAgingTestTime2 = DateTime.Now.ToString()
                        };
                        if (pmtb.updateProjectorInformation_MainTable_LHHBll(projeLHQ1.FuselageCode, Convert.ToDateTime(projeLHQ1.PostAgingTestTime2), "updateProjectorInformation_MainTable_LHH1"))
                        {
                            DGV_LHH.DataSource = new List<ProjectorInformation_LHH_Str>();
                            DataList_LHH.Insert(0, this.GetProjectorInformation_LHH_Str(projeLHQ1));
                            if (DataList_LHH.Count > 100)
                            {
                                DataList_LHH.RemoveAt(DataList_LHH.Count - 1);
                            }
                            DGV_LHH.DataSource = DataList_LHH;
                            this.lab_Mssage1.Text = "完成";
                            txt_FuselageCode_LHH.Clear();
                            txt_FuselageCode_LHH.Focus();
                        }
                        else { this.lab_Mssage1.Text = "保存异常"; }
                    }
                }
                else
                {
                    //if (pmtb.insertProjectorInformation_MainTable_LHHBll(this.GetProjectorInformation_LHH(), "insertProjectorInformation_MainTable_LHH"))
                    //{
                    //    DGV_LHH.DataSource = new List<ProjectorInformation_LHH_Str>();
                    //    DataList_LHH.Insert(0, this.GetProjectorInformation_LHH_Str(this.GetProjectorInformation_LHH()));
                    //    if (DataList_LHH.Count > 100)
                    //    {
                    //        DataList_LHH.RemoveAt(DataList_LHH.Count - 1);
                    //    }
                    //    DGV_LHH.DataSource = DataList_LHH;
                    //    this.lab_Mssage1.Text = "完成";
                    //    txt_FuselageCode_LHH.Clear();
                    //    txt_FuselageCode_LHH.Focus();
                    //}
                    //else { this.lab_Mssage1.Text = "保存异常"; }
                    txt_FuselageCode_LHH.Text = "";
                    MessageBox.Show("该机身码不存在！");
                    txt_FuselageCode_LHH.SelectAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 亮度测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btni_save_LD_Click(object sender, EventArgs e)
        {
            this.saveLD();
        }

        bool MessageLD()
        {
            if (txt_FuselageCode_LD.Text.Trim() == "")
            {
                MessageBox.Show("机身码不能为空！");
                return true;
            }
            else if (txt_IlluminationValue_LD.Text.Trim() == ""&&ck_IlluminationValue_LD.Checked)
            {
                MessageBox.Show("照度值不能为空！");
                return true;
            }
            else if (txt_WiredMAC_LD.Text.Trim() == ""&&ck_WiredMAC_LD.Checked)
            {
                MessageBox.Show("有线MAC不能为空！");
                return true;
            }
            else if(txt_wirelessMAC_LD.Text.Trim() == ""&&ck_wirelessMAC_LD.Checked)
            {
                MessageBox.Show("无线线MAC不能为空！");
                return true;
            }
            else return false;
        }

        ProjectorInformation_LD GetProjectorInformation_LD()
        {
            ProjectorInformation_LD ld = new ProjectorInformation_LD()
            {
                FuselageCode=txt_FuselageCode_LD.Text.Trim(),
                IlluminationValue= ck_IlluminationValue_LD.Checked? txt_IlluminationValue_LD.Text.Trim():"",
                WiredMAC= ck_WiredMAC_LD.Checked? txt_WiredMAC_LD.Text.Trim()==""?null:txt_WiredMAC_LD.Text.Trim():"",
                wirelessMAC= ck_wirelessMAC_LD.Checked? txt_wirelessMAC_LD.Text.Trim()==""?null:txt_wirelessMAC_LD.Text.Trim():"",
                LuminanceTestTime=DateTime.Now,
                ZhiDan=txt_ZhiDan_DG.Text

            };
            return ld;
        }

        void saveLD()
        { 
            if (this.MessageLD()) return;

            this.lab_Mssage1.Text = "正在保存";
            try
            {
                if (pmtb.selectCountProjectorInformation_MainTableBll(txt_FuselageCode_LD.Text.Trim(), txt_ZhiDan_DG.Text.Trim(), "selectCountProjectorInformation_MainTable"))
                {
                    List<ProjectorInformation_MainTable_STR> LDlist = pmtb.selectProjectorInformation_MainTableBll(txt_FuselageCode_LD.Text.Trim(), "", "", "", "", "", "selectProjectorInformation_MainTable");
                    if (LDlist[0].PostAgingTestTime == "")
                    {
                        MessageBox.Show("错误，该产品没有老化后测试！");
                        return;
                    }
                    if (pmtb.updateProjectorInformation_MainTable_LDBll(this.GetProjectorInformation_LD(), "updateProjectorInformation_MainTableLDQ"))
                    {
                        DGV_LD.DataSource = new List<ProjectorInformation_LD>();
                        DataList_LD.Insert(0, this.GetProjectorInformation_LD());
                        if (DataList_LD.Count > 100)
                        {
                            DataList_LD.RemoveAt(DataList_LD.Count - 1);
                        }
                        DGV_LD.DataSource = DataList_LD;

                        this.lab_Mssage1.Text = "完成";
                        txt_FuselageCode_LD.Clear();
                        txt_IlluminationValue_LD.Clear();
                        txt_WiredMAC_LD.Clear();
                        txt_wirelessMAC_LD.Clear();
                        txt_FuselageCode_LD.Focus();
                        txt_IlluminationValue_LD.Enabled = false;
                        txt_WiredMAC_LD.Enabled = false;
                        txt_wirelessMAC_LD.Enabled = false;
                    }
                    else { this.lab_Mssage1.Text = "保存异常"; }
                }
                else
                {
                    //if (pmtb.insertProjectorInformation_MainTable_LDBll(this.GetProjectorInformation_LD(), "insertProjectorInformation_MainTable_LD"))
                    //{
                    //    DGV_LD.DataSource = new List<ProjectorInformation_LD>();
                    //    DataList_LD.Insert(0, this.GetProjectorInformation_LD());
                    //    if (DataList_LD.Count > 100)
                    //    {
                    //        DataList_LD.RemoveAt(DataList_LD.Count - 1);
                    //    }
                    //    DGV_LD.DataSource = DataList_LD;

                    //    this.lab_Mssage1.Text = "完成";
                    //    txt_FuselageCode_LD.Clear();
                    //    txt_IlluminationValue_LD.Clear();
                    //    txt_WiredMAC_LD.Clear();
                    //    txt_wirelessMAC_LD.Clear();
                    //    txt_FuselageCode_LD.Focus();
                    //    txt_IlluminationValue_LD.Enabled = false;
                    //    txt_WiredMAC_LD.Enabled = false;
                    //    txt_wirelessMAC_LD.Enabled = false;
                    //}
                    //else { this.lab_Mssage1.Text = "保存异常"; }
                    MessageBox.Show("该机身码不存在！");
                    txt_FuselageCode_LD.Focus();
                    txt_FuselageCode_LD.SelectAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 维修
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btni_save_WX_Click(object sender, EventArgs e)
        {
            this.saveWX();
        }

        bool MessageWX()
        {
            if (txt_FuselageCode_WX.Text.Trim() == "")
            {
                MessageBox.Show("机身码不能为空！");
                return true;
            }
            else if (txt_describe_WX.Text.Trim() == "")
            {
                MessageBox.Show("维修描述不能为空！");
                return true;
            }
            else return false;
        }

        ProjectorInformation_WX GetProjectorInformation_WX()
        {
            ProjectorInformation_WX wx = new ProjectorInformation_WX()
            {
                FuselageCode=txt_FuselageCode_WX.Text.Trim(),
                RepairText=txt_describe_WX.Text.Trim(),
                AfterMaintenanceOpticalCode=txt_OpticalCode_WX.Text.Trim(),
                AfterMaintenanceMainBoardCode=txt_FuselageCode_WXZB.Text.Trim(),
                RepairTime=DateTime.Now
            };
            return wx;
        }

        void saveWX()
        { 
            if (this.MessageWX()) return;

            this.lab_Mssage1.Text = "正在保存";
            try
            {
                if (pmtb.selectCountProjectorInformation_MainTableBll(txt_FuselageCode_WX.Text.Trim(), txt_ZhiDan_DG.Text.Trim(), "selectCountProjectorInformation_MainTable"))
                {
                    if (pmtb.updateProjectorInformation_MainTable_WXBll(this.GetProjectorInformation_WX(), "updateProjectorInformation_MainTable_WX"))
                    {
                        DGV_WX.DataSource = new List<ProjectorInformation_WX>();
                        DataList_WX.Insert(0, this.GetProjectorInformation_WX());
                        if (DataList_WX.Count > 100)
                        {
                            DataList_WX.RemoveAt(DataList_WX.Count - 1);
                        }
                        DGV_WX.DataSource = DataList_WX;

                        this.lab_Mssage1.Text = "完成";
                        txt_FuselageCode_WX.Clear();
                        txt_describe_WX.Clear();
                        txt_FuselageCode_WX.Focus();
                    }
                    else { this.lab_Mssage1.Text = "保存异常"; }
                }
                else
                {
                    //if (pmtb.insertProjectorInformation_MainTable_WXBll(this.GetProjectorInformation_WX(), "insertProjectorInformation_MainTable_WX"))
                    //{
                    //    DGV_WX.DataSource = new List<ProjectorInformation_WX>();
                    //    DataList_WX.Insert(0, this.GetProjectorInformation_WX());
                    //    if (DataList_WX.Count > 100)
                    //    {
                    //        DataList_WX.RemoveAt(DataList_WX.Count - 1);
                    //    }
                    //    DGV_WX.DataSource = DataList_WX;

                    //    this.lab_Mssage1.Text = "完成";
                    //    txt_FuselageCode_WX.Clear();
                    //    txt_describe_WX.Clear();
                    //    txt_FuselageCode_WX.Focus();
                    //}
                    //else { this.lab_Mssage1.Text = "保存异常"; }
                    MessageBox.Show("该机身码不存在！");
                    txt_FuselageCode_WX.Focus();
                    txt_FuselageCode_WX.SelectAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 包装
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btni_save_BZ_Click(object sender, EventArgs e)
        {
            this.saveBZ();
        }

        bool MessageBZ()
        {
            if (txt_FuselageCode_BZ.Text.Trim() == "")
            {
                MessageBox.Show("机身码不能为空！");
                return true;
            }
            else return false;
        }

        ProjectorInformation_BZ GetProjectorInformation_BZ()
        {
            ProjectorInformation_BZ bz = new ProjectorInformation_BZ()
            {
                FuselageCode=txt_FuselageCode_BZ.Text.Trim(),
                PackingTime=DateTime.Now
            };
            return bz;
        }

        void saveBZ()
        { 
            if (this.MessageBZ()) return;

            this.lab_Mssage1.Text = "正在保存";
            try
            {
                if (pmtb.selectCountProjectorInformation_MainTableBll(txt_FuselageCode_BZ.Text.Trim(), txt_ZhiDan_DG.Text.Trim(), "selectCountProjectorInformation_MainTable"))
                {
                    List<ProjectorInformation_MainTable_STR> BZlist = pmtb.selectProjectorInformation_MainTableBll(txt_FuselageCode_BZ.Text.Trim(), "", "", "", "", "", "selectProjectorInformation_MainTable");
                    if (BZlist[0].LuminanceTestTime == "")
                    {
                        MessageBox.Show("错误，该产品没有亮度测试！");
                        return;
                    }

                    if (pmtb.updateProjectorInformation_MainTable_BZDal(this.GetProjectorInformation_BZ(), "updateProjectorInformation_MainTable_BZ"))
                    {
                        DGV_BZ.DataSource = new List<ProjectorInformation_BZ>();
                        DataList_BZ.Insert(0, this.GetProjectorInformation_BZ());
                        if (DataList_BZ.Count > 100)
                        {
                            DataList_BZ.RemoveAt(DataList_BZ.Count - 1);
                        }
                        DGV_BZ.DataSource = DataList_BZ;

                        this.lab_Mssage1.Text = "完成";
                        txt_FuselageCode_BZ.Clear();
                        txt_FuselageCode_BZ.Focus();
                    }
                    else { this.lab_Mssage1.Text = "保存异常"; }
                }
                else
                {
                    //if (pmtb.insertProjectorInformation_MainTable_BZDal(this.GetProjectorInformation_BZ(), "insertProjectorInformation_MainTable_BZ"))
                    //{
                    //    DGV_BZ.DataSource = new List<ProjectorInformation_BZ>();
                    //    DataList_BZ.Insert(0, this.GetProjectorInformation_BZ());
                    //    if (DataList_BZ.Count > 100)
                    //    {
                    //        DataList_BZ.RemoveAt(DataList_BZ.Count - 1);
                    //    }
                    //    DGV_BZ.DataSource = DataList_BZ;

                    //    this.lab_Mssage1.Text = "完成";
                    //    txt_FuselageCode_BZ.Clear();
                    //    txt_FuselageCode_BZ.Focus();
                    //}
                    //else { this.lab_Mssage1.Text = "保存异常"; }
                    MessageBox.Show("该机身码不存在！");
                    txt_FuselageCode_BZ.Focus();
                    txt_FuselageCode_BZ.SelectAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 主表按机身码查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btni_query_Click(object sender, EventArgs e)
        {
            this.queryZJM();
        }

        bool MessageZJM()
        {
            if (txt_FuselageCode_ZJM.Text.Trim() == "")
            {
                MessageBox.Show("机身码不能为空！");
                return true;
            }
            else return false;
        }

        void queryZJM()
        {
            ProjectorInformation_Common.DeleteCondition.FuselageCode = this.txt_FuselageCode_ZJM.Text.Trim();
            ProjectorInformation_Common.DeleteCondition.ZhiDan = this.txt_ZhiDan_ZJM.Text.Trim();
            ProjectorInformation_Common.DeleteCondition.OpticalCode = txt_OpticalCode_ZJM.Text.Trim();
            ProjectorInformation_Common.DeleteCondition.MainBoardCode = txt_MainBoardCode_ZJM.Text.Trim();
            ProjectorInformation_Common.DeleteCondition.WiredMAC = txt_WiredMAC_ZJM.Text.Trim();
            ProjectorInformation_Common.DeleteCondition.wirelessMAC = txt_wirelessMAC_ZJM.Text.Trim();

            this.lab_Mssage1.Text = "正在查询";
            th = new Thread(delegate()
            {
                queryZJMth(this.txt_FuselageCode_ZJM.Text.Trim(),this.txt_ZhiDan_ZJM.Text.Trim(),txt_OpticalCode_ZJM.Text.Trim(),txt_MainBoardCode_ZJM.Text.Trim(),txt_WiredMAC_ZJM.Text.Trim(),txt_wirelessMAC_ZJM.Text.Trim());
            });
            th.IsBackground = true;
            th.Start();

        }

        void queryZJMth(string txt_FuselageCode_ZJM,string zhidan,string OpticalCode, string MainBoardCode, string WiredMAC, string wirelessMAC)
        {
            My_context.Post(SetDGV_ZJM, pmtb.selectProjectorInformation_MainTableBll(txt_FuselageCode_ZJM, zhidan, OpticalCode, MainBoardCode, WiredMAC, wirelessMAC, "selectProjectorInformation_MainTable"));
        }

         public void SetDGV_ZJM(object o)
        {
            List<ProjectorInformation_MainTable_STR> List = o as List<ProjectorInformation_MainTable_STR>;
            DGV_ZJM.DataSource = new List<ProjectorInformation_MainTable_STR>();
            DGV_ZJM.DataSource = List;
            DataList = List;
            this.lab_Mssage1.Text = "完成";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            DisplayInterface di = new DisplayInterface();
            di.ShowDialog();

            My_context = SynchronizationContext.Current;
            this.WindowState = FormWindowState.Maximized;
            SetProjectorInformation_SelectPrefix();
            try
            {
                AdminiHelp.MAC = this.GetMacAddress();
                if (AdminiHelp.MAC == "unknow")
                {
                    MessageBox.Show("MAC地址读取失败", "系统提示", MessageBoxButtons.OK);
                }
                else
                {
                    List<ProjectorInformation_MAC> ListMac = pimac.selectProjectorInformation_MAC(AdminiHelp.MAC, "selectProjectorInformation_MAC");
                    if (ListMac.Count > 0)
                    {
                        AdminiHelp.LHSX = ListMac[0].LHSX;

                        if (ListMac[0].selectedTab != null && ListMac[0].selectedTab != "")
                        {
                            AdminiHelp.Login = true;

                            foreach (SuperTabItem tabitem in this.superTabControl1.Tabs)
                            {
                                if (tabitem.Name == ListMac[0].selectedTab)
                                {
                                    superTabControl1.SelectedTab = tabitem;
                                    break;
                                }
                            }

                            AdminiHelp.Login = false;
                        }
                    }
                }
            }
            catch { }
        }

        private void 管理员登陆ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Admini_Login al = new Admini_Login(this);
            al.ShowDialog();
        }

        public void SetAdminiHelp()
        {
            AdminiHelp.Login = true;
            this.lab_Users.Text = "管理员已登录";
            this.lab_Users.ForeColor = Color.Lime;
            txt_ZhiDan_DG.Enabled = true;
        }

        private void 注销登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdminiHelp.Login = false;
            this.lab_Users.Text = "未登录";
            this.lab_Users.ForeColor = Color.DarkRed;
            txt_ZhiDan_DG.Enabled = false;
        }

        private void 系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AdminiHelp.Login)
            {
                注销登录ToolStripMenuItem.Enabled = true;
                配置编码前缀ToolStripMenuItem.Enabled = true;
            }
            else
            {
                注销登录ToolStripMenuItem.Enabled = false;
                配置编码前缀ToolStripMenuItem.Enabled = false;
            }
        }

        private void superTabControl1_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
        }

        private void 退出系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void superTabControl1_SelectedTabChanging(object sender, SuperTabStripSelectedTabChangingEventArgs e)
        {
            if (e.NewValue.Name == "superTabItem5" || e.NewValue.Name == "superTabItem6")
            {
                if ((!AdminiHelp.Login)&&(!AdminiHelp.LHSX))
                {
                    MessageBox.Show("改变工位需要登录管理员");
                    e.Cancel = true;
                }
            }
            else
            {
                if (!AdminiHelp.Login)
                {
                    MessageBox.Show("改变工位需要登录管理员");
                    e.Cancel = true;
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("是否退出系统？","系统提示",MessageBoxButtons.OKCancel)==DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void txt_FuselageCode_ZJM_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.queryZJM();
            }
        }
        /// <summary>
        /// 获取MAC地址
        /// </summary>
        /// <returns></returns>
        public string GetMacAddress()
        {
            try
            {
                //获取网卡硬件地址 
                string mac = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return mac;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }

        }

        private void superTabControl1_SelectedTabChanged_1(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
                ProjectorInformation_MAC mac = new ProjectorInformation_MAC()
                {
                    MAC = AdminiHelp.MAC,
                    selectedTab = e.NewValue.Name,
                    LHSX = e.NewValue.Name == "superTabItem5" || e.NewValue.Name == "superTabItem6"?true:false
                };
                try
                {
                    pimac.insertProjectorInformation_MACBll(mac, "insertProjectorInformation_MAC");
                }
                catch
                {
                    pimac.updateProjectorInformation_MACBll(mac, "updateProjectorInformation_MAC");
                }
                finally
                {
                    //this.lab_Mssage1.Text = "网络连接异常";
                }

        }

        private void btni_DataOut_Click(object sender, EventArgs e)
        {
            if (!AdminiHelp.Login)
            {
                MessageBox.Show("你没有数据导出的权限！");
                return;
            }

            if (this.DGV_ZJM.CurrentRow == null)
            {
                MessageBox.Show("没有可导出的数据!");
                return;
            }

            if (ExcelHelperForCs.ExportToExcel(DGV_ZJM) != null)
            {
                MessageBox.Show("导出完成");
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (superTabControl1.SelectedTab.Name == "superTabItem1")
            {
                if (e.Control && e.KeyCode == Keys.F)
                {
                    MainQury mq = new MainQury(this);
                    mq.ShowDialog();
                }
            }

        }

        private void txt_ZhiDan_DG_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (txt_ZhiDan_DG.Text.Trim() == "")
                {
                    txt_FuselageCode_DG.Text = "";
                    txt_FuselageCode_DG.Enabled = false;                    
                    return;
                }

                txt_FuselageCode_DG.Enabled = true;
                txt_FuselageCode_DG.Focus();
            }
        }

        private void txt_FuselageCode_DG_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (lb_FuselageCode_DG.Text.Trim() == "未选择")
                {
                    MessageBox.Show("未选择前缀！");
                    return;
                }

                if (!txt_FuselageCode_DG.Text.Contains(lb_FuselageCode_DG.Text.Trim()))
                {
                    MessageBox.Show("机身码错误！");
                    return;
                }

                if (txt_FuselageCode_DG.Text == "")
                {
                    txt_OpticalCode_DG.Text = "";
                    txt_OpticalCode_DG.Enabled = false;
                    txt_FuselageCode_DGZB.Text = "";
                    txt_FuselageCode_DGZB.Enabled = false;
                    return;
                }
                if (ck_OpticalCode_DG.Checked)
                {
                    txt_OpticalCode_DG.Enabled = true;
                    txt_OpticalCode_DG.Focus();
                }
                else
                {
                    if (ck_FuselageCode_DGZB.Checked)
                    {
                        txt_FuselageCode_DGZB.Enabled = true;
                        txt_FuselageCode_DGZB.Focus();
                    }
                    else
                    {
                        this.btni_save_DG_save(); 
                    }
                }

            }
        }

        private void txt_OpticalCode_DG_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (lb_OpticalCode_DG.Text.Trim() == "未选择")
                {
                    MessageBox.Show("未选择前缀！");
                    return;
                }

                if (!txt_OpticalCode_DG.Text.Contains(lb_OpticalCode_DG.Text.Trim()))
                {
                    MessageBox.Show("光机码错误！");
                    return;
                }

                if (txt_OpticalCode_DG.Text == "")
                {
                    txt_FuselageCode_DGZB.Text = "";
                    txt_FuselageCode_DGZB.Enabled = false;
                    return;
                }

                if (ck_FuselageCode_DGZB.Checked)
                {
                    txt_FuselageCode_DGZB.Enabled = true;
                    txt_FuselageCode_DGZB.Focus();
                }
                else
                {
                    this.btni_save_DG_save();
                }

            }
        }

        private void txt_FuselageCode_DGZB_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (lb_FuselageCode_DGZB.Text.Trim() == "未选择")
                {
                    MessageBox.Show("未选择前缀！");
                    return;
                }

                if (!txt_FuselageCode_DGZB.Text.Contains(lb_FuselageCode_DGZB.Text.Trim()))
                {
                    MessageBox.Show("主板编码错误！");
                    return;
                }

                if (txt_FuselageCode_DGZB.Text.Trim() == "")
                {
                    txt_FuselageCode_DGZB.Focus();
                    return;
                }
                else
                {
                    this.btni_save_DG_save();
                }

            }
        }

        private void txt_FuselageCode_LD_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (lb_FuselageCode_LD.Text.Trim() == "未选择")
                {
                    MessageBox.Show("未选择前缀！");
                    return;
                }

                if (!txt_FuselageCode_LD.Text.Contains(lb_FuselageCode_LD.Text.Trim()))
                {
                    MessageBox.Show("机身码错误！");
                    return;
                }

                if (txt_FuselageCode_LD.Text.Trim() == "")
                {
                    txt_IlluminationValue_LD.Text = "";
                    txt_IlluminationValue_LD.Enabled = false;
                    txt_wirelessMAC_LD.Text = "";
                    txt_wirelessMAC_LD.Enabled = false;
                    return;
                }
                if (ck_IlluminationValue_LD.Checked)
                {
                    txt_IlluminationValue_LD.Enabled = true;
                    txt_IlluminationValue_LD.Focus();
                }
                else if (ck_wirelessMAC_LD.Checked)
                {
                    txt_wirelessMAC_LD.Enabled = true;
                    txt_wirelessMAC_LD.Focus();
                }
                else if (ck_WiredMAC_LD.Checked)
                {
                    txt_WiredMAC_LD.Enabled = true;
                    txt_WiredMAC_LD.Focus();
                }
                else
                {
                    this.saveLD();  
                }

            }
        }

        private void txt_IlluminationValue_LD_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (txt_IlluminationValue_LD.Text.Trim() == "")
                {
                    txt_wirelessMAC_LD.Text = "";
                    txt_wirelessMAC_LD.Enabled = false;
                    txt_WiredMAC_LD.Text = "";
                    txt_WiredMAC_LD.Enabled = false;
                    return;
                }

                if (ck_wirelessMAC_LD.Checked)
                {
                    txt_wirelessMAC_LD.Enabled = true;
                    txt_wirelessMAC_LD.Focus();
                }
                else if (ck_WiredMAC_LD.Checked)
                {
                    txt_WiredMAC_LD.Enabled = true;
                    txt_WiredMAC_LD.Focus();
                }
                else
                {
                    this.saveLD();
                }
            }
        }

        private void txt_wirelessMAC_LD_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (txt_wirelessMAC_LD.Text.Trim() == "")
                {
                    txt_WiredMAC_LD.Text = "";
                    txt_WiredMAC_LD.Enabled = false;
                    return;
                }

                if (ck_WiredMAC_LD.Checked)
                {
                    txt_WiredMAC_LD.Enabled = true;
                    txt_WiredMAC_LD.Focus();
                }
                else
                {
                    this.saveLD();
                }
                
            }
        }

        private void txt_WiredMAC_LD_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (txt_WiredMAC_LD.Text.Trim() == "")
                {
                    txt_WiredMAC_LD.Focus();
                    return;
                }
                else
                {
                    this.saveLD();  
                }

            }
        }

        private void txt_describe_WX_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 13)
            {

                if (txt_FuselageCode_WX.Text == "")
                {
                    txt_OpticalCode_WX.Text = "";
                    txt_OpticalCode_WX.Enabled = false;
                    txt_FuselageCode_WXZB.Text = "";
                    txt_FuselageCode_WXZB.Enabled = false;
                    txt_FuselageCode_WX.Focus();
                    return;
                }
                if (ck_OpticalCode_WX.Checked)
                {
                    txt_OpticalCode_WX.Enabled = true;
                    txt_OpticalCode_WX.Focus();
                }
                else
                {
                    if (ck_FuselageCode_WXZB.Checked)
                    {
                        txt_FuselageCode_WXZB.Enabled = true;
                        txt_FuselageCode_WXZB.Focus();
                    }
                    else
                    {
                        this.saveWX();
                    }
                }

                
            }
        }

        private void txt_FuselageCode_BZ_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (lb_FuselageCode_BZ.Text.Trim() == "未选择")
                {
                    MessageBox.Show("未选择前缀！");
                    return;
                }
                
                if (!txt_FuselageCode_BZ.Text.Contains(lb_FuselageCode_BZ.Text.Trim()))
                {
                    MessageBox.Show("机身码错误！");
                    return;
                }

                if (txt_FuselageCode_BZ.Text.Trim() == "")
                {
                    return;
                }

                this.saveBZ();
            }
        }

        private void txt_FuselageCode_LHH_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (lb_FuselageCode_LHH.Text.Trim() == "未选择")
                {
                    MessageBox.Show("未选择前缀！");
                    return;
                }

                if (!txt_FuselageCode_LHH.Text.Contains(lb_FuselageCode_LHH.Text.Trim()))
                {
                    MessageBox.Show("机身码错误！");
                    return;
                }

                if (txt_FuselageCode_LHH.Text.Trim() == "")
                {
                    return;
                }

                this.saveLHH();
            }
        }

        private void txt_FuselageCode_LHX_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (lb_FuselageCode_LHX.Text.Trim() == "未选择")
                {
                    MessageBox.Show("未选择前缀！");
                    return;
                }

                if (!txt_FuselageCode_LHX.Text.Contains(lb_FuselageCode_LHX.Text.Trim()))
                {
                    MessageBox.Show("机身码错误！");
                    return;
                }

                if (txt_FuselageCode_LHX.Text.Trim() == "")
                {
                    return;
                }

                this.saveLHX();
            }
        }

        private void txt_FuselageCode_LHS_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (lb_FuselageCode_LHS.Text.Trim() == "未选择")
                {
                    MessageBox.Show("未选择前缀！");
                    return;
                }

                if (!txt_FuselageCode_LHS.Text.Contains(lb_FuselageCode_LHS.Text.Trim()))
                {
                    MessageBox.Show("机身码错误！");
                    return;
                }

                if (txt_FuselageCode_LHS.Text.Trim() == "")
                {
                    return;
                }

                this.saveLHS();
            }
        }

        private void txt_FuselageCode_LHQ_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (lb_FuselageCode_LHQ.Text.Trim() == "未选择")
                {
                    MessageBox.Show("未选择前缀！");
                    return;
                }

                if (!txt_FuselageCode_LHQ.Text.Contains(lb_FuselageCode_LHQ.Text.Trim()))
                {
                    MessageBox.Show("机身码错误！");
                    return;
                }

                if (txt_FuselageCode_LHQ.Text.Trim() == "")
                {
                    return;
                }

                this.save_LHQ();
            }
        }

        private void txt_FuselageCode_WX_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (lb_FuselageCode_WX.Text.Trim() == "未选择")
                {
                    MessageBox.Show("未选择前缀！");
                    return;
                }

                if (!txt_FuselageCode_WX.Text.Contains(lb_FuselageCode_WX.Text.Trim()))
                {
                    MessageBox.Show("机身码错误！");
                    return;
                }

                if (txt_FuselageCode_WX.Text.Trim() == "")
                {
                    txt_FuselageCode_WX.Focus();
                    return;
                }
                else if (txt_describe_WX.Text.Trim() == "")
                {
                    txt_describe_WX.Focus();
                    return;
                }

                this.saveWX();
            }
        }

        private void 配置编码前缀ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfiguringCodePrefixes config = new ConfiguringCodePrefixes(this);
            config.ShowDialog();
        }
        /// <summary>
        /// 查询前缀更新变获得配置前缀
        /// </summary>
        public void SetProjectorInformation_SelectPrefix()
        {
            this.projeSelect = projeSelectBll.selecctProjectorInformation_SelectPrefixBll("selecctProjectorInformation_SelectPrefix");
            this.SetLable();
        }

        void SetLable()
        {
            labitem_TypeName.Text = this.projeSelect.TypeName == "" ? "未选择" : this.projeSelect.TypeName;
            lb_FuselageCode_DG.Text = this.projeSelect.Prefix_BodyCode == "" ? "未选择" : this.projeSelect.Prefix_BodyCode;
            lb_FuselageCode_BZ.Text = this.projeSelect.Prefix_BodyCode == "" ? "未选择" : this.projeSelect.Prefix_BodyCode;
            lb_FuselageCode_LD.Text = this.projeSelect.Prefix_BodyCode == "" ? "未选择" : this.projeSelect.Prefix_BodyCode;
            lb_FuselageCode_LHH.Text = this.projeSelect.Prefix_BodyCode== "" ? "未选择" : this.projeSelect.Prefix_BodyCode;
            lb_FuselageCode_LHQ.Text = this.projeSelect.Prefix_BodyCode == "" ? "未选择" : this.projeSelect.Prefix_BodyCode;
            lb_FuselageCode_LHS.Text = this.projeSelect.Prefix_BodyCode == "" ? "未选择" : this.projeSelect.Prefix_BodyCode;
            lb_FuselageCode_LHX.Text = this.projeSelect.Prefix_BodyCode == "" ? "未选择" : this.projeSelect.Prefix_BodyCode;
            lb_FuselageCode_WX.Text = this.projeSelect.Prefix_BodyCode == "" ? "未选择" : this.projeSelect.Prefix_BodyCode;
            lb_OpticalCode_DG.Text = this.projeSelect.Prefix_OpticalCode == "" ? "未选择" : this.projeSelect.Prefix_OpticalCode;
            lb_FuselageCode_DGZB.Text = this.projeSelect.Prefix_MotherboardEncoding == "" ? "未选择" : this.projeSelect.Prefix_MotherboardEncoding;
            lb_OpticalCode_WX.Text = this.projeSelect.Prefix_OpticalCode == "" ? "未选择" : this.projeSelect.Prefix_OpticalCode;
            lb_FuselageCode_WXZB.Text = this.projeSelect.Prefix_MotherboardEncoding == "" ? "未选择" : this.projeSelect.Prefix_MotherboardEncoding;
            txt_ZhiDan_DG.Text = this.projeSelect.ZhiDan;
        }

        private void txt_OpticalCode_WX_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (lb_OpticalCode_WX.Text.Trim() == "未选择")
                {
                    MessageBox.Show("未选择前缀！");
                    return;
                }

                if (!txt_OpticalCode_WX.Text.Contains(lb_OpticalCode_WX.Text.Trim()))
                {
                    MessageBox.Show("光机码错误！");
                    return;
                }

                if (txt_OpticalCode_WX.Text == "")
                {
                    txt_FuselageCode_WXZB.Text = "";
                    txt_FuselageCode_WXZB.Enabled = false;
                    return;
                }

                if (ck_FuselageCode_WXZB.Checked)
                {
                    txt_FuselageCode_WXZB.Enabled = true;
                    txt_FuselageCode_WXZB.Focus();
                }
                else
                {
                    this.saveWX();
                }

            }
        }

        private void txt_FuselageCode_WXZB_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (lb_FuselageCode_WXZB.Text.Trim() == "未选择")
                {
                    MessageBox.Show("未选择前缀！");
                    return;
                }

                if (!txt_FuselageCode_WXZB.Text.Contains(lb_FuselageCode_WXZB.Text.Trim()))
                {
                    MessageBox.Show("主板编码错误！");
                    return;
                }

                if (txt_FuselageCode_WXZB.Text.Trim() == "")
                {
                    txt_FuselageCode_WXZB.Focus();
                    return;
                }
                else
                {
                    this.saveWX();
                }

            }
        }

        private void 刷新前缀ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SetProjectorInformation_SelectPrefix();
        }

        private void btni_deleteAll_Click(object sender, EventArgs e)
        {
            if (!AdminiHelp.Login)
            {
                MessageBox.Show("您没有权限，先登录");
                return;
            }

            if (DGV_ZJM.Rows.Count == 0)
            {
                MessageBox.Show("此界面没有数据！");
                return;
            }

            if (MessageBox.Show("确定删除此界面所有数据？", "系统提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
            this.lab_Mssage1.Text = "正在删除..";
            try
            {
                if (pmtb.deleteProjectorInformation_MainTableBll(ProjectorInformation_Common.DeleteCondition.FuselageCode, 
                    ProjectorInformation_Common.DeleteCondition.ZhiDan, ProjectorInformation_Common.DeleteCondition.OpticalCode, 
                    ProjectorInformation_Common.DeleteCondition.MainBoardCode, ProjectorInformation_Common.DeleteCondition.WiredMAC, 
                    ProjectorInformation_Common.DeleteCondition.wirelessMAC, "deleteProjectorInformation_MainTable"))
                {
                    DGV_ZJM.DataSource = new List<ProjectorInformation_MainTable_STR>();
                    this.lab_Mssage1.Text = "完成";
                }
                else { this.lab_Mssage1.Text = "异常"; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btni_delete_Click(object sender, EventArgs e)
        {
            if (!AdminiHelp.Login)
            {
                MessageBox.Show("您没有权限，先登录");
                return;
            }

            if (MessageBox.Show("确定删除选中行数据？", "系统提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
            this.lab_Mssage1.Text = "正在删除..";
            try
            {
                if (pmtb.deleteProjectorInformation_MainTableFuselageCodeBll(this.DGV_ZJM.Rows[this.DGV_ZJM.CurrentCell.RowIndex].Cells[1].Value.ToString(), "deleteProjectorInformation_MainTableFuselageCode"))
                {
                    int i = DGV_ZJM.CurrentCell.RowIndex;
                    DGV_ZJM.DataSource = new List<ProjectorInformation_MainTable_STR>();
                    DataList.RemoveAt(i);
                    DGV_ZJM.DataSource = DataList;
                    this.lab_Mssage1.Text = "完成";
                }
                else { this.lab_Mssage1.Text = "异常"; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DGV_ZJM_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(this.DGV_ZJM.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1, CultureInfo.CurrentCulture),
                e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        private void DGV_DG_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(this.DGV_DG.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1, CultureInfo.CurrentCulture),
                e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        private void DGV_LHQ_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(this.DGV_LHQ.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1, CultureInfo.CurrentCulture),
                e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        private void DGV_LHS_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(this.DGV_LHS.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1, CultureInfo.CurrentCulture),
                e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        private void DGV_LHX_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(this.DGV_LHX.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1, CultureInfo.CurrentCulture),
                e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        private void DGV_LHH_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(this.DGV_LHH.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1, CultureInfo.CurrentCulture),
                e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        private void DGV_LD_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(this.DGV_LD.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1, CultureInfo.CurrentCulture),
                e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        private void DGV_WX_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(this.DGV_WX.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1, CultureInfo.CurrentCulture),
                e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        private void DGV_BZ_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(this.DGV_BZ.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1, CultureInfo.CurrentCulture),
                e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        private void DGV_ZJM_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            if (!AdminiHelp.Login)
            {
                MessageBox.Show("您没有权限，先登录");
                return;
            }
            DataImport di = new DataImport(this.txt_ZhiDan_DG.Text.Trim());
            di.ShowDialog();
        }

    }
}
