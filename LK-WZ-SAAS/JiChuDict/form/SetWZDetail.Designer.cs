namespace JiChuDict.form
{
    partial class SetWZDetail
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.kindcode_selTextInpt = new YtWinContrl.com.contrl.SelTextInpt();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.warecode_yTextBox = new YtWinContrl.com.contrl.YTextBox();
            this.choscode_yTextBox = new YtWinContrl.com.contrl.YTextBox();
            this.ok_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.dataGView1 = new YtWinContrl.com.datagrid.DataGView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.kindcode_selTextInpt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.warecode_yTextBox);
            this.groupBox1.Controls.Add(this.choscode_yTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 202);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "物资库房细表信息";
            // 
            // kindcode_selTextInpt
            // 
            this.kindcode_selTextInpt.ColDefText = null;
            this.kindcode_selTextInpt.ColStyle = null;
            this.kindcode_selTextInpt.DataType = null;
            this.kindcode_selTextInpt.DbConn = null;
            this.kindcode_selTextInpt.Location = new System.Drawing.Point(119, 141);
            this.kindcode_selTextInpt.Name = "kindcode_selTextInpt";
            this.kindcode_selTextInpt.NextFocusControl = null;
            this.kindcode_selTextInpt.ReadOnly = false;
            this.kindcode_selTextInpt.SelParam = null;
            this.kindcode_selTextInpt.ShowColNum = 0;
            this.kindcode_selTextInpt.ShowWidth = 0;
            this.kindcode_selTextInpt.Size = new System.Drawing.Size(126, 22);
            this.kindcode_selTextInpt.Sql = null;
            this.kindcode_selTextInpt.SqlStr = null;
            this.kindcode_selTextInpt.TabIndex = 14;
            this.kindcode_selTextInpt.TvColName = null;
            this.kindcode_selTextInpt.Value = null;
            this.kindcode_selTextInpt.WatermarkText = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "类别";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "医疗机构编码";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "库房编码";
            // 
            // warecode_yTextBox
            // 
            // 
            // 
            // 
            this.warecode_yTextBox.Border.Class = "TextBoxBorder";
            this.warecode_yTextBox.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.warecode_yTextBox.Location = new System.Drawing.Point(120, 57);
            this.warecode_yTextBox.Name = "warecode_yTextBox";
            this.warecode_yTextBox.ReadOnly = true;
            this.warecode_yTextBox.Size = new System.Drawing.Size(120, 21);
            this.warecode_yTextBox.TabIndex = 12;
            // 
            // choscode_yTextBox
            // 
            // 
            // 
            // 
            this.choscode_yTextBox.Border.Class = "TextBoxBorder";
            this.choscode_yTextBox.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.choscode_yTextBox.Location = new System.Drawing.Point(120, 104);
            this.choscode_yTextBox.Name = "choscode_yTextBox";
            this.choscode_yTextBox.ReadOnly = true;
            this.choscode_yTextBox.Size = new System.Drawing.Size(120, 21);
            this.choscode_yTextBox.TabIndex = 13;
            // 
            // ok_button
            // 
            this.ok_button.Location = new System.Drawing.Point(142, 227);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(75, 23);
            this.ok_button.TabIndex = 15;
            this.ok_button.Text = "添加";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.Location = new System.Drawing.Point(607, 227);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 17;
            this.cancel_button.Text = "关闭";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
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
            this.Column3,
            this.Column1,
            this.Column2,
            this.Column4,
            this.Column5});
            this.dataGView1.DbConn = null;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGView1.DwColIndex = 0;
            this.dataGView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGView1.IsEditOnEnter = true;
            this.dataGView1.IsFillForm = true;
            this.dataGView1.IsPage = false;
            this.dataGView1.Key = null;
            this.dataGView1.Location = new System.Drawing.Point(295, 12);
            this.dataGView1.Name = "dataGView1";
            this.dataGView1.ReadOnly = true;
            this.dataGView1.RowHeadersWidth = 30;
            this.dataGView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGView1.RowTemplate.Height = 23;
            this.dataGView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGView1.Size = new System.Drawing.Size(435, 202);
            this.dataGView1.TabIndex = 2;
            this.dataGView1.TjFmtStr = null;
            this.dataGView1.TjFormat = null;
            this.dataGView1.Url = null;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "KINDNAME";
            this.Column3.HeaderText = "类别名称";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "KINDCODE";
            this.Column1.HeaderText = "类别编码";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "SUPERCODE";
            this.Column2.HeaderText = "上级编码";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "MEMO";
            this.Column4.HeaderText = "备注";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "CHOSCODE";
            this.Column5.HeaderText = "医疗机构编码";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Delete
            // 
            this.Delete.Location = new System.Drawing.Point(427, 229);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(75, 23);
            this.Delete.TabIndex = 16;
            this.Delete.Text = "删除";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // SetWZDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 262);
            this.Controls.Add(this.Delete);
            this.Controls.Add(this.dataGView1);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.ok_button);
            this.Controls.Add(this.groupBox1);
            this.Name = "SetWZDetail";
            this.Text = "物资库房细表设置";
            this.Load += new System.EventHandler(this.SetWZDetail_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private YtWinContrl.com.contrl.YTextBox warecode_yTextBox;
        private System.Windows.Forms.Label label2;
        private YtWinContrl.com.contrl.YTextBox choscode_yTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ok_button;
        private System.Windows.Forms.Button cancel_button;
        private YtWinContrl.com.contrl.SelTextInpt kindcode_selTextInpt;
        private YtWinContrl.com.datagrid.DataGView dataGView1;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}