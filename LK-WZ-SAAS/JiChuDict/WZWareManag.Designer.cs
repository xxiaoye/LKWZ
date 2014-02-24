namespace JiChuDict
{
    partial class WZWareManag
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WZWareManag));
            YtWinContrl.com.datagrid.TvList tvList2 = new YtWinContrl.com.datagrid.TvList();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.ModifyButton = new System.Windows.Forms.ToolStripButton();
            this.DeleButton = new System.Windows.Forms.ToolStripButton();
            this.rfresh_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.DisableButton = new System.Windows.Forms.ToolStripButton();
            this.EnableButton = new System.Windows.Forms.ToolStripButton();
            this.scan_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.yTextBox1 = new YtWinContrl.com.contrl.YTextBox();
            this.Search_ytComboBox = new YtWinContrl.com.contrl.YtComboBox();
            this.dataGView1 = new YtWinContrl.com.datagrid.DataGView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.ModifyButton,
            this.DeleButton,
            this.rfresh_toolStripButton,
            this.DisableButton,
            this.EnableButton,
            this.scan_toolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(851, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(67, 22);
            this.toolStripButton1.Text = "新增(&A)";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // ModifyButton
            // 
            this.ModifyButton.Image = ((System.Drawing.Image)(resources.GetObject("ModifyButton.Image")));
            this.ModifyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ModifyButton.Name = "ModifyButton";
            this.ModifyButton.Size = new System.Drawing.Size(67, 22);
            this.ModifyButton.Text = "修改(&M)";
            this.ModifyButton.Click += new System.EventHandler(this.ModifyButton_Click);
            // 
            // DeleButton
            // 
            this.DeleButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleButton.Image")));
            this.DeleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleButton.Name = "DeleButton";
            this.DeleButton.Size = new System.Drawing.Size(67, 22);
            this.DeleButton.Text = "删除(&D)";
            this.DeleButton.Click += new System.EventHandler(this.DeleButton_Click);
            // 
            // rfresh_toolStripButton
            // 
            this.rfresh_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("rfresh_toolStripButton.Image")));
            this.rfresh_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rfresh_toolStripButton.Name = "rfresh_toolStripButton";
            this.rfresh_toolStripButton.Size = new System.Drawing.Size(67, 22);
            this.rfresh_toolStripButton.Text = " 刷新  ";
            this.rfresh_toolStripButton.Click += new System.EventHandler(this.rfresh_toolStripButton_Click);
            // 
            // DisableButton
            // 
            this.DisableButton.Image = ((System.Drawing.Image)(resources.GetObject("DisableButton.Image")));
            this.DisableButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DisableButton.Name = "DisableButton";
            this.DisableButton.Size = new System.Drawing.Size(49, 22);
            this.DisableButton.Text = "停用";
            this.DisableButton.Click += new System.EventHandler(this.DisableButton_Click);
            // 
            // EnableButton
            // 
            this.EnableButton.Image = ((System.Drawing.Image)(resources.GetObject("EnableButton.Image")));
            this.EnableButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EnableButton.Name = "EnableButton";
            this.EnableButton.Size = new System.Drawing.Size(49, 22);
            this.EnableButton.Text = "启用";
            this.EnableButton.Click += new System.EventHandler(this.EnableButton_Click);
            // 
            // scan_toolStripButton
            // 
            this.scan_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("scan_toolStripButton.Image")));
            this.scan_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.scan_toolStripButton.Name = "scan_toolStripButton";
            this.scan_toolStripButton.Size = new System.Drawing.Size(49, 22);
            this.scan_toolStripButton.Text = "浏览";
            this.scan_toolStripButton.Click += new System.EventHandler(this.scan_toolStripButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.yTextBox1);
            this.groupBox1.Controls.Add(this.Search_ytComboBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(851, 62);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(511, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "查询(&Q)";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // yTextBox1
            // 
            // 
            // 
            // 
            this.yTextBox1.Border.Class = "TextBoxBorder";
            this.yTextBox1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.yTextBox1.Location = new System.Drawing.Point(330, 24);
            this.yTextBox1.Name = "yTextBox1";
            this.yTextBox1.Size = new System.Drawing.Size(100, 21);
            this.yTextBox1.TabIndex = 1;
            // 
            // Search_ytComboBox
            // 
            this.Search_ytComboBox.CacheKey = null;
            this.Search_ytComboBox.DbConn = null;
            this.Search_ytComboBox.DefText = null;
            this.Search_ytComboBox.DefValue = null;
            this.Search_ytComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Search_ytComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Search_ytComboBox.EnableEmpty = true;
            this.Search_ytComboBox.FirstText = null;
            this.Search_ytComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Search_ytComboBox.Fomart = null;
            this.Search_ytComboBox.ItemStr = null;
            this.Search_ytComboBox.Location = new System.Drawing.Point(105, 23);
            this.Search_ytComboBox.Name = "Search_ytComboBox";
            this.Search_ytComboBox.Param = null;
            this.Search_ytComboBox.Size = new System.Drawing.Size(121, 22);
            this.Search_ytComboBox.Sql = null;
            this.Search_ytComboBox.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Search_ytComboBox.TabIndex = 0;
            this.Search_ytComboBox.Tag = tvList2;
            this.Search_ytComboBox.Value = null;
            this.Search_ytComboBox.SelectionChangeCommitted += new System.EventHandler(this.Search_ytComboBox_TextChanged);
            // 
            // dataGView1
            // 
            this.dataGView1.AllowUserToAddRows = false;
            this.dataGView1.AllowUserToDeleteRows = false;
            this.dataGView1.AllowUserToResizeRows = false;
            this.dataGView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGView1.BindCheckBox = null;
            this.dataGView1.ChangeDataColumName = null;
            this.dataGView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column15,
            this.Column16,
            this.Column17});
            this.dataGView1.DbConn = "LKWZ";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGView1.DwColIndex = 0;
            this.dataGView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGView1.IsEditOnEnter = true;
            this.dataGView1.IsFillForm = true;
            this.dataGView1.IsPage = false;
            this.dataGView1.Key = null;
            this.dataGView1.Location = new System.Drawing.Point(0, 87);
            this.dataGView1.Name = "dataGView1";
            this.dataGView1.ReadOnly = true;
            this.dataGView1.RowHeadersWidth = 30;
            this.dataGView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGView1.RowTemplate.Height = 23;
            this.dataGView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGView1.Size = new System.Drawing.Size(851, 422);
            this.dataGView1.TabIndex = 3;
            this.dataGView1.TjFmtStr = null;
            this.dataGView1.TjFormat = null;
            this.dataGView1.Url = "";
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "warecode";
            this.Column1.HeaderText = "库房编码";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "choscode";
            this.Column2.HeaderText = "医疗机构编码";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "warename";
            this.Column3.HeaderText = "库房名称";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "pycode";
            this.Column4.HeaderText = "拼音码";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "wbcode";
            this.Column5.HeaderText = "五笔码";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "ifstware";
            this.Column6.FalseValue = "0";
            this.Column6.HeaderText = "是否一级库房";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column6.TrueValue = "1";
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "ifndware";
            this.Column7.FalseValue = "0";
            this.Column7.HeaderText = "是否二级库房";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column7.TrueValue = "1";
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "ifrdware";
            this.Column8.FalseValue = "0";
            this.Column8.HeaderText = "是否三级库房";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column8.TrueValue = "1";
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "DEPTName";
            this.Column9.HeaderText = "科室";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "PRICENAME";
            this.Column10.HeaderText = "计价体系";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "outorder";
            this.Column11.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.Column11.HeaderText = "出库顺序";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "ifall";
            this.Column12.FalseValue = "0";
            this.Column12.HeaderText = "IFALL";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column12.TrueValue = "1";
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "ifuse";
            this.Column13.FalseValue = "0";
            this.Column13.HeaderText = "是否使用";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column13.TrueValue = "1";
            // 
            // Column14
            // 
            this.Column14.DataPropertyName = "memo";
            this.Column14.HeaderText = "备注";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            // 
            // Column15
            // 
            this.Column15.DataPropertyName = "userid";
            this.Column15.HeaderText = "操作员ID";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            // 
            // Column16
            // 
            this.Column16.DataPropertyName = "username";
            this.Column16.HeaderText = "操作员名称";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            // 
            // Column17
            // 
            this.Column17.DataPropertyName = "recdate";
            this.Column17.HeaderText = "修改时间";
            this.Column17.Name = "Column17";
            this.Column17.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(275, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "关键字";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "查询方式";
            // 
            // WZWareManag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 509);
            this.Controls.Add(this.dataGView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "WZWareManag";
            this.Text = "物资库房管理";
            this.Load += new System.EventHandler(this.WZWareManag_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton ModifyButton;
        private System.Windows.Forms.ToolStripButton DeleButton;
        private System.Windows.Forms.ToolStripButton DisableButton;
        private System.Windows.Forms.ToolStripButton EnableButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private YtWinContrl.com.datagrid.DataGView dataGView1;
        private YtWinContrl.com.contrl.YTextBox yTextBox1;
        private System.Windows.Forms.ToolStripButton rfresh_toolStripButton;
        private YtWinContrl.com.contrl.YtComboBox Search_ytComboBox;
        private System.Windows.Forms.ToolStripButton scan_toolStripButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column7;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column11;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column12;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}