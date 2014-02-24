namespace JiChuDict
{
    partial class ProjectUnitManag
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectUnitManag));
            YtWinContrl.com.datagrid.TvList tvList1 = new YtWinContrl.com.datagrid.TvList();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Add_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ModifyButton = new System.Windows.Forms.ToolStripButton();
            this.DeleButton = new System.Windows.Forms.ToolStripButton();
            this.rfresh_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.scan_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Search_button = new System.Windows.Forms.Button();
            this.Search_yTextBox = new YtWinContrl.com.contrl.YTextBox();
            this.Search_ytComboBox = new YtWinContrl.com.contrl.YtComboBox();
            this.dataGView1 = new YtWinContrl.com.datagrid.DataGView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Add_toolStripButton,
            this.ModifyButton,
            this.DeleButton,
            this.rfresh_toolStripButton,
            this.scan_toolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(634, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Add_toolStripButton
            // 
            this.Add_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("Add_toolStripButton.Image")));
            this.Add_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Add_toolStripButton.Name = "Add_toolStripButton";
            this.Add_toolStripButton.Size = new System.Drawing.Size(67, 22);
            this.Add_toolStripButton.Text = "新增(&A)";
            this.Add_toolStripButton.Click += new System.EventHandler(this.Add_toolStripButton_Click);
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
            this.groupBox1.Controls.Add(this.Search_button);
            this.groupBox1.Controls.Add(this.Search_yTextBox);
            this.groupBox1.Controls.Add(this.Search_ytComboBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(634, 62);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // Search_button
            // 
            this.Search_button.Image = ((System.Drawing.Image)(resources.GetObject("Search_button.Image")));
            this.Search_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Search_button.Location = new System.Drawing.Point(417, 26);
            this.Search_button.Name = "Search_button";
            this.Search_button.Size = new System.Drawing.Size(75, 23);
            this.Search_button.TabIndex = 2;
            this.Search_button.Text = "查询(&Q)";
            this.Search_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Search_button.UseVisualStyleBackColor = true;
            this.Search_button.Click += new System.EventHandler(this.Search_button_Click);
            // 
            // Search_yTextBox
            // 
            // 
            // 
            // 
            this.Search_yTextBox.Border.Class = "TextBoxBorder";
            this.Search_yTextBox.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Search_yTextBox.Location = new System.Drawing.Point(201, 22);
            this.Search_yTextBox.Name = "yTextBox1";
            this.Search_yTextBox.Size = new System.Drawing.Size(120, 21);
            this.Search_yTextBox.TabIndex = 1;
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
            this.Search_ytComboBox.ItemStr = "";
            this.Search_ytComboBox.Location = new System.Drawing.Point(19, 21);
            this.Search_ytComboBox.Name = "Search_ytComboBox";
            this.Search_ytComboBox.Param = null;
            this.Search_ytComboBox.Size = new System.Drawing.Size(121, 22);
            this.Search_ytComboBox.Sql = null;
            this.Search_ytComboBox.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Search_ytComboBox.TabIndex = 0;
            this.Search_ytComboBox.Tag = tvList1;
            this.Search_ytComboBox.Value = null;
            // 
            // dataGView1
            // 
            this.dataGView1.AllowUserToAddRows = false;
            this.dataGView1.AllowUserToDeleteRows = false;
            this.dataGView1.AllowUserToResizeRows = false;
            this.dataGView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGView1.ChangeDataColumName = null;
            this.dataGView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.dataGView1.DbConn = null;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGView1.DefaultCellStyle = dataGridViewCellStyle1;
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
            this.dataGView1.Size = new System.Drawing.Size(634, 332);
            this.dataGView1.TabIndex = 4;
            this.dataGView1.TjFmtStr = null;
            this.dataGView1.TjFormat = null;
            this.dataGView1.Url = null;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "DICGRPID";
            this.Column1.HeaderText = "字典组类别";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "DICID";
            this.Column2.HeaderText = "DICID";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "DICDESC";
            this.Column3.HeaderText = "名称";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "PYCODE";
            this.Column4.HeaderText = "拼音码";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "FIXED";
            this.Column5.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.Column5.HeaderText = "是否使用";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "DEFVALUE";
            this.Column6.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.Column6.HeaderText = "是否默认值";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ProjectUnitManag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 419);
            this.Controls.Add(this.dataGView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ProjectUnitManag";
            this.Text = "物资项目单位管理";
            this.Load += new System.EventHandler(this.ProjectUnitManag_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton Add_toolStripButton;
        private System.Windows.Forms.ToolStripButton ModifyButton;
        private System.Windows.Forms.ToolStripButton DeleButton;
        private System.Windows.Forms.ToolStripButton rfresh_toolStripButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Search_button;
        private YtWinContrl.com.contrl.YTextBox Search_yTextBox;
        private YtWinContrl.com.datagrid.DataGView dataGView1;
        private YtWinContrl.com.contrl.YtComboBox Search_ytComboBox;
        private System.Windows.Forms.ToolStripButton scan_toolStripButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column5;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column6;
    }
}