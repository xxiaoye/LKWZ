namespace BusinessManag.form2
{
    partial class PlanForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlanForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.add_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.DeleButton = new System.Windows.Forms.ToolStripButton();
            this.save_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.cancel_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.gain_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.getNUmXx_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.memo_yTextBox = new YtWinContrl.com.contrl.YTextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.InWare_selTextInpt = new YtWinContrl.com.contrl.SelTextInpt();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.totalmoney_yTextBox = new YtWinContrl.com.contrl.YTextBox();
            this.lstotalmoney_yTextBox = new YtWinContrl.com.contrl.YTextBox();
            this.dataGView2 = new YtWinContrl.com.datagrid.DataGView();
            this.planid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kindname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wzid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.unitcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitcode0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lsunitcode = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.hsxs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bzxs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.money = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lsprice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lsmoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.memo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rowno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGView2)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.add_toolStripButton,
            this.DeleButton,
            this.save_toolStripButton,
            this.cancel_toolStripButton,
            this.gain_toolStripButton,
            this.getNUmXx_toolStripButton,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(818, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // add_toolStripButton
            // 
            this.add_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("add_toolStripButton.Image")));
            this.add_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.add_toolStripButton.Name = "add_toolStripButton";
            this.add_toolStripButton.Size = new System.Drawing.Size(116, 22);
            this.add_toolStripButton.Text = "添加采购物资(&A)";
            this.add_toolStripButton.Click += new System.EventHandler(this.add_toolStripButton_Click);
            // 
            // DeleButton
            // 
            this.DeleButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleButton.Image")));
            this.DeleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleButton.Name = "DeleButton";
            this.DeleButton.Size = new System.Drawing.Size(117, 22);
            this.DeleButton.Text = "删除采购物资(&D)";
            this.DeleButton.Click += new System.EventHandler(this.DeleButton_Click);
            // 
            // save_toolStripButton
            // 
            this.save_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("save_toolStripButton.Image")));
            this.save_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.save_toolStripButton.Name = "save_toolStripButton";
            this.save_toolStripButton.Size = new System.Drawing.Size(103, 22);
            this.save_toolStripButton.Text = "保存采购单(&S)";
            this.save_toolStripButton.Click += new System.EventHandler(this.save_toolStripButton_Click);
            // 
            // cancel_toolStripButton
            // 
            this.cancel_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("cancel_toolStripButton.Image")));
            this.cancel_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cancel_toolStripButton.Name = "cancel_toolStripButton";
            this.cancel_toolStripButton.Size = new System.Drawing.Size(68, 22);
            this.cancel_toolStripButton.Text = "取消(&C)";
            this.cancel_toolStripButton.Click += new System.EventHandler(this.cancel_toolStripButton_Click);
            // 
            // gain_toolStripButton
            // 
            this.gain_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("gain_toolStripButton.Image")));
            this.gain_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.gain_toolStripButton.Name = "gain_toolStripButton";
            this.gain_toolStripButton.Size = new System.Drawing.Size(112, 22);
            this.gain_toolStripButton.Text = "从历史明细导入";
            this.gain_toolStripButton.Click += new System.EventHandler(this.gain_toolStripButton_Click);
            // 
            // getNUmXx_toolStripButton
            // 
            this.getNUmXx_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("getNUmXx_toolStripButton.Image")));
            this.getNUmXx_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.getNUmXx_toolStripButton.Name = "getNUmXx_toolStripButton";
            this.getNUmXx_toolStripButton.Size = new System.Drawing.Size(124, 22);
            this.getNUmXx_toolStripButton.Text = "根据最低库存生成";
            this.getNUmXx_toolStripButton.Click += new System.EventHandler(this.getNUmXx_toolStripButton_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(67, 22);
            this.toolStripButton1.Text = "打印(&P)";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.memo_yTextBox);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.InWare_selTextInpt);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.totalmoney_yTextBox);
            this.groupBox1.Controls.Add(this.lstotalmoney_yTextBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(818, 136);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本信息";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(592, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 72;
            this.label3.Text = "共：0条";
            // 
            // memo_yTextBox
            // 
            // 
            // 
            // 
            this.memo_yTextBox.Border.Class = "TextBoxBorder";
            this.memo_yTextBox.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.memo_yTextBox.Location = new System.Drawing.Point(111, 64);
            this.memo_yTextBox.Name = "memo_yTextBox";
            this.memo_yTextBox.Size = new System.Drawing.Size(450, 21);
            this.memo_yTextBox.TabIndex = 2;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(423, 26);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(138, 21);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // InWare_selTextInpt
            // 
            this.InWare_selTextInpt.ColDefText = null;
            this.InWare_selTextInpt.ColStyle = null;
            this.InWare_selTextInpt.DataType = null;
            this.InWare_selTextInpt.DbConn = null;
            this.InWare_selTextInpt.Enabled = false;
            this.InWare_selTextInpt.Location = new System.Drawing.Point(118, 25);
            this.InWare_selTextInpt.Name = "InWare_selTextInpt";
            this.InWare_selTextInpt.NextFocusControl = null;
            this.InWare_selTextInpt.ReadOnly = false;
            this.InWare_selTextInpt.SelParam = null;
            this.InWare_selTextInpt.ShowColNum = 0;
            this.InWare_selTextInpt.ShowWidth = 0;
            this.InWare_selTextInpt.Size = new System.Drawing.Size(135, 22);
            this.InWare_selTextInpt.Sql = null;
            this.InWare_selTextInpt.SqlStr = null;
            this.InWare_selTextInpt.TabIndex = 0;
            this.InWare_selTextInpt.TvColName = null;
            this.InWare_selTextInpt.Value = null;
            this.InWare_selTextInpt.WatermarkText = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(334, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "制单日期";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(339, 113);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "零售总金额";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 112);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "总金额";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "备注";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(23, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "采购库房";
            // 
            // totalmoney_yTextBox
            // 
            // 
            // 
            // 
            this.totalmoney_yTextBox.Border.Class = "TextBoxBorder";
            this.totalmoney_yTextBox.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.totalmoney_yTextBox.Location = new System.Drawing.Point(108, 105);
            this.totalmoney_yTextBox.Name = "totalmoney_yTextBox";
            this.totalmoney_yTextBox.ReadOnly = true;
            this.totalmoney_yTextBox.Size = new System.Drawing.Size(130, 21);
            this.totalmoney_yTextBox.TabIndex = 3;
            // 
            // lstotalmoney_yTextBox
            // 
            // 
            // 
            // 
            this.lstotalmoney_yTextBox.Border.Class = "TextBoxBorder";
            this.lstotalmoney_yTextBox.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lstotalmoney_yTextBox.Location = new System.Drawing.Point(425, 108);
            this.lstotalmoney_yTextBox.Name = "lstotalmoney_yTextBox";
            this.lstotalmoney_yTextBox.ReadOnly = true;
            this.lstotalmoney_yTextBox.Size = new System.Drawing.Size(130, 21);
            this.lstotalmoney_yTextBox.TabIndex = 4;
            // 
            // dataGView2
            // 
            this.dataGView2.AllowUserToAddRows = false;
            this.dataGView2.AllowUserToDeleteRows = false;
            this.dataGView2.AllowUserToResizeRows = false;
            this.dataGView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGView2.BindCheckBox = null;
            this.dataGView2.ChangeDataColumName = null;
            this.dataGView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.planid,
            this.wz,
            this.kindname,
            this.wzid,
            this.unit,
            this.unitcode,
            this.unitcode0,
            this.lsunitcode,
            this.hsxs,
            this.bzxs,
            this.Column1,
            this.rate,
            this.num,
            this.price,
            this.price0,
            this.money,
            this.lsprice,
            this.lsmoney,
            this.memo,
            this.txm,
            this.rowno});
            this.dataGView2.DbConn = null;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGView2.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGView2.DwColIndex = 0;
            this.dataGView2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGView2.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGView2.IsEditOnEnter = true;
            this.dataGView2.IsFillForm = true;
            this.dataGView2.IsPage = false;
            this.dataGView2.Key = null;
            this.dataGView2.Location = new System.Drawing.Point(0, 161);
            this.dataGView2.Name = "dataGView2";
            this.dataGView2.RowHeadersWidth = 30;
            this.dataGView2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGView2.RowTemplate.Height = 23;
            this.dataGView2.Size = new System.Drawing.Size(818, 285);
            this.dataGView2.TabIndex = 7;
            this.dataGView2.TjFmtStr = null;
            this.dataGView2.TjFormat = null;
            this.dataGView2.Url = null;
            this.dataGView2.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGView2_RowPostPaint);
            // 
            // planid
            // 
            this.planid.DataPropertyName = "PLANID";
            this.planid.HeaderText = "采购计划ID";
            this.planid.Name = "planid";
            this.planid.ReadOnly = true;
            this.planid.Width = 90;
            // 
            // wz
            // 
            this.wz.DataPropertyName = "WZNAME";
            this.wz.HeaderText = "物资";
            this.wz.Name = "wz";
            this.wz.Width = 54;
            // 
            // kindname
            // 
            this.kindname.DataPropertyName = "KINDNAME";
            this.kindname.HeaderText = "物资类别";
            this.kindname.Name = "kindname";
            this.kindname.ReadOnly = true;
            this.kindname.Width = 78;
            // 
            // wzid
            // 
            this.wzid.DataPropertyName = "WZID";
            this.wzid.HeaderText = "物资ID";
            this.wzid.Name = "wzid";
            this.wzid.ReadOnly = true;
            this.wzid.Visible = false;
            this.wzid.Width = 66;
            // 
            // unit
            // 
            this.unit.DataPropertyName = "UNITCODE";
            this.unit.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.unit.HeaderText = "单位";
            this.unit.Name = "unit";
            this.unit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.unit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.unit.Width = 54;
            // 
            // unitcode
            // 
            this.unitcode.DataPropertyName = "UNITCODE";
            this.unitcode.HeaderText = "单位编码";
            this.unitcode.Name = "unitcode";
            this.unitcode.ReadOnly = true;
            this.unitcode.Visible = false;
            this.unitcode.Width = 78;
            // 
            // unitcode0
            // 
            this.unitcode0.DataPropertyName = "wzUNITCODE";
            this.unitcode0.HeaderText = "单位编码0";
            this.unitcode0.Name = "unitcode0";
            this.unitcode0.Visible = false;
            this.unitcode0.Width = 84;
            // 
            // lsunitcode
            // 
            this.lsunitcode.DataPropertyName = "LSUNITCODE";
            this.lsunitcode.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.lsunitcode.HeaderText = "最小单位编码";
            this.lsunitcode.Name = "lsunitcode";
            this.lsunitcode.ReadOnly = true;
            this.lsunitcode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.lsunitcode.Width = 102;
            // 
            // hsxs
            // 
            this.hsxs.DataPropertyName = "CHANGERATE";
            this.hsxs.HeaderText = "换算系数";
            this.hsxs.Name = "hsxs";
            this.hsxs.ReadOnly = true;
            this.hsxs.Visible = false;
            this.hsxs.Width = 78;
            // 
            // bzxs
            // 
            this.bzxs.HeaderText = "包装系数";
            this.bzxs.Name = "bzxs";
            this.bzxs.ReadOnly = true;
            this.bzxs.Width = 78;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "GG";
            this.Column1.HeaderText = "规格";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 54;
            // 
            // rate
            // 
            this.rate.DataPropertyName = "RATE";
            this.rate.HeaderText = "加价率";
            this.rate.Name = "rate";
            this.rate.Visible = false;
            this.rate.Width = 66;
            // 
            // num
            // 
            this.num.DataPropertyName = "NUM";
            this.num.HeaderText = "采购数量";
            this.num.Name = "num";
            this.num.Width = 78;
            // 
            // price
            // 
            this.price.DataPropertyName = "PRICE";
            this.price.HeaderText = "采购单价";
            this.price.Name = "price";
            this.price.Width = 78;
            // 
            // price0
            // 
            this.price0.DataPropertyName = "wzPRICE";
            this.price0.HeaderText = "采购单价0";
            this.price0.Name = "price0";
            this.price0.Visible = false;
            this.price0.Width = 84;
            // 
            // money
            // 
            this.money.DataPropertyName = "MONEY";
            this.money.HeaderText = "采购金额";
            this.money.Name = "money";
            this.money.ReadOnly = true;
            this.money.Width = 78;
            // 
            // lsprice
            // 
            this.lsprice.DataPropertyName = "LSPRICE";
            this.lsprice.HeaderText = "零售单价";
            this.lsprice.Name = "lsprice";
            this.lsprice.Width = 78;
            // 
            // lsmoney
            // 
            this.lsmoney.DataPropertyName = "LSMONEY";
            this.lsmoney.HeaderText = "零售金额";
            this.lsmoney.Name = "lsmoney";
            this.lsmoney.ReadOnly = true;
            this.lsmoney.Width = 78;
            // 
            // memo
            // 
            this.memo.DataPropertyName = "MEMO";
            this.memo.HeaderText = "备注";
            this.memo.Name = "memo";
            this.memo.Width = 54;
            // 
            // txm
            // 
            this.txm.DataPropertyName = "TXM";
            this.txm.HeaderText = "条形码";
            this.txm.Name = "txm";
            this.txm.ReadOnly = true;
            this.txm.Width = 66;
            // 
            // rowno
            // 
            this.rowno.DataPropertyName = "ROWNO";
            this.rowno.HeaderText = "行号";
            this.rowno.Name = "rowno";
            this.rowno.Visible = false;
            this.rowno.Width = 54;
            // 
            // PlanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 446);
            this.Controls.Add(this.dataGView2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "PlanForm";
            this.Text = "采购计划单";
            this.Load += new System.EventHandler(this.PlanForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton add_toolStripButton;
        private System.Windows.Forms.ToolStripButton DeleButton;
        private System.Windows.Forms.ToolStripButton save_toolStripButton;
        private System.Windows.Forms.ToolStripButton cancel_toolStripButton;
        private System.Windows.Forms.ToolStripButton gain_toolStripButton;
        private System.Windows.Forms.ToolStripButton getNUmXx_toolStripButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private YtWinContrl.com.contrl.YTextBox memo_yTextBox;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private YtWinContrl.com.contrl.SelTextInpt InWare_selTextInpt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private YtWinContrl.com.contrl.YTextBox totalmoney_yTextBox;
        private YtWinContrl.com.contrl.YTextBox lstotalmoney_yTextBox;
        private YtWinContrl.com.datagrid.DataGView dataGView2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn planid;
        private System.Windows.Forms.DataGridViewTextBoxColumn wz;
        private System.Windows.Forms.DataGridViewTextBoxColumn kindname;
        private System.Windows.Forms.DataGridViewTextBoxColumn wzid;
        private System.Windows.Forms.DataGridViewComboBoxColumn unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitcode0;
        private System.Windows.Forms.DataGridViewComboBoxColumn lsunitcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn hsxs;
        private System.Windows.Forms.DataGridViewTextBoxColumn bzxs;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn num;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewTextBoxColumn price0;
        private System.Windows.Forms.DataGridViewTextBoxColumn money;
        private System.Windows.Forms.DataGridViewTextBoxColumn lsprice;
        private System.Windows.Forms.DataGridViewTextBoxColumn lsmoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn memo;
        private System.Windows.Forms.DataGridViewTextBoxColumn txm;
        private System.Windows.Forms.DataGridViewTextBoxColumn rowno;
    }
}