namespace ProjectorInformation_Main
{
    partial class ConfiguringCodePrefixes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btni_New = new DevComponents.DotNetBar.ButtonItem();
            this.btni_sava = new DevComponents.DotNetBar.ButtonItem();
            this.btni_cancle = new DevComponents.DotNetBar.ButtonItem();
            this.btni_modefy = new DevComponents.DotNetBar.ButtonItem();
            this.btni_delete = new DevComponents.DotNetBar.ButtonItem();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.txt_Prefix_MotherboardEncoding = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txt_Prefix_OpticalCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txt_Prefix_BodyCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txt_TypeName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btni_select = new DevComponents.DotNetBar.ButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(181, 447);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.bar1.IsMaximized = false;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btni_New,
            this.btni_sava,
            this.btni_cancle,
            this.btni_modefy,
            this.btni_delete,
            this.btni_select});
            this.bar1.Location = new System.Drawing.Point(181, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(377, 27);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 1;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // btni_New
            // 
            this.btni_New.Name = "btni_New";
            this.btni_New.Text = "新建";
            this.btni_New.Click += new System.EventHandler(this.btni_New_Click);
            // 
            // btni_sava
            // 
            this.btni_sava.Name = "btni_sava";
            this.btni_sava.Text = "保存";
            this.btni_sava.Click += new System.EventHandler(this.btni_sava_Click);
            // 
            // btni_cancle
            // 
            this.btni_cancle.Name = "btni_cancle";
            this.btni_cancle.Text = "取消";
            this.btni_cancle.Click += new System.EventHandler(this.btni_cancle_Click);
            // 
            // btni_modefy
            // 
            this.btni_modefy.Name = "btni_modefy";
            this.btni_modefy.Text = "修改";
            this.btni_modefy.Click += new System.EventHandler(this.btni_modefy_Click);
            // 
            // btni_delete
            // 
            this.btni_delete.Name = "btni_delete";
            this.btni_delete.Text = "删除";
            this.btni_delete.Click += new System.EventHandler(this.btni_delete_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.txt_Prefix_MotherboardEncoding);
            this.panelEx1.Controls.Add(this.labelX4);
            this.panelEx1.Controls.Add(this.txt_Prefix_OpticalCode);
            this.panelEx1.Controls.Add(this.labelX3);
            this.panelEx1.Controls.Add(this.txt_Prefix_BodyCode);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Controls.Add(this.txt_TypeName);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(181, 27);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(377, 420);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 2;
            // 
            // txt_Prefix_MotherboardEncoding
            // 
            // 
            // 
            // 
            this.txt_Prefix_MotherboardEncoding.Border.Class = "TextBoxBorder";
            this.txt_Prefix_MotherboardEncoding.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_Prefix_MotherboardEncoding.Location = new System.Drawing.Point(159, 299);
            this.txt_Prefix_MotherboardEncoding.Name = "txt_Prefix_MotherboardEncoding";
            this.txt_Prefix_MotherboardEncoding.PreventEnterBeep = true;
            this.txt_Prefix_MotherboardEncoding.Size = new System.Drawing.Size(146, 21);
            this.txt_Prefix_MotherboardEncoding.TabIndex = 17;
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(72, 302);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(81, 18);
            this.labelX4.TabIndex = 16;
            this.labelX4.Text = "主板编码前缀";
            // 
            // txt_Prefix_OpticalCode
            // 
            // 
            // 
            // 
            this.txt_Prefix_OpticalCode.Border.Class = "TextBoxBorder";
            this.txt_Prefix_OpticalCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_Prefix_OpticalCode.Location = new System.Drawing.Point(159, 236);
            this.txt_Prefix_OpticalCode.Name = "txt_Prefix_OpticalCode";
            this.txt_Prefix_OpticalCode.PreventEnterBeep = true;
            this.txt_Prefix_OpticalCode.Size = new System.Drawing.Size(146, 21);
            this.txt_Prefix_OpticalCode.TabIndex = 15;
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(85, 239);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(68, 18);
            this.labelX3.TabIndex = 14;
            this.labelX3.Text = "光机码前缀";
            // 
            // txt_Prefix_BodyCode
            // 
            // 
            // 
            // 
            this.txt_Prefix_BodyCode.Border.Class = "TextBoxBorder";
            this.txt_Prefix_BodyCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_Prefix_BodyCode.Location = new System.Drawing.Point(159, 171);
            this.txt_Prefix_BodyCode.Name = "txt_Prefix_BodyCode";
            this.txt_Prefix_BodyCode.PreventEnterBeep = true;
            this.txt_Prefix_BodyCode.Size = new System.Drawing.Size(146, 21);
            this.txt_Prefix_BodyCode.TabIndex = 13;
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(85, 174);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(68, 18);
            this.labelX2.TabIndex = 12;
            this.labelX2.Text = "机身码前缀";
            // 
            // txt_TypeName
            // 
            // 
            // 
            // 
            this.txt_TypeName.Border.Class = "TextBoxBorder";
            this.txt_TypeName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_TypeName.Location = new System.Drawing.Point(159, 100);
            this.txt_TypeName.Name = "txt_TypeName";
            this.txt_TypeName.PreventEnterBeep = true;
            this.txt_TypeName.Size = new System.Drawing.Size(146, 21);
            this.txt_TypeName.TabIndex = 11;
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(85, 103);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(68, 18);
            this.labelX1.TabIndex = 10;
            this.labelX1.Text = "前缀类型名";
            // 
            // btni_select
            // 
            this.btni_select.Name = "btni_select";
            this.btni_select.Text = "选定前缀类型";
            this.btni_select.Click += new System.EventHandler(this.btni_select_Click);
            // 
            // ConfiguringCodePrefixes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 447);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.bar1);
            this.Controls.Add(this.treeView1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "ConfiguringCodePrefixes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配置编码前缀";
            this.Load += new System.EventHandler(this.ConfiguringCodePrefixes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btni_sava;
        private DevComponents.DotNetBar.ButtonItem btni_delete;
        private DevComponents.DotNetBar.ButtonItem btni_modefy;
        private DevComponents.DotNetBar.ButtonItem btni_cancle;
        private DevComponents.DotNetBar.ButtonItem btni_New;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_Prefix_MotherboardEncoding;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_Prefix_OpticalCode;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_Prefix_BodyCode;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_TypeName;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonItem btni_select;
    }
}