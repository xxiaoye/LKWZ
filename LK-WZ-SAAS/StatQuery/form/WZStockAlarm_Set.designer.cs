namespace StatQuery.form
{
    partial class WZStockAlarm_Set
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.yTextBox_WZName = new YtWinContrl.com.contrl.YTextBox();
            this.yTextBox_KCDown = new YtWinContrl.com.contrl.YTextBox();
            this.yTextBox_KCUp = new YtWinContrl.com.contrl.YTextBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.yTextBox_WZName);
            this.groupBox2.Controls.Add(this.yTextBox_KCDown);
            this.groupBox2.Controls.Add(this.yTextBox_KCUp);
            this.groupBox2.ForeColor = System.Drawing.Color.Blue;
            this.groupBox2.Location = new System.Drawing.Point(32, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(641, 103);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "信息";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.Blue;
            this.label18.Location = new System.Drawing.Point(355, 69);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 12);
            this.label18.TabIndex = 30;
            this.label18.Text = "库存下限";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.Blue;
            this.label19.Location = new System.Drawing.Point(41, 69);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 12);
            this.label19.TabIndex = 29;
            this.label19.Text = "库存上限";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(41, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "物资名称";
            // 
            // yTextBox_WZName
            // 
            // 
            // 
            // 
            this.yTextBox_WZName.Border.Class = "TextBoxBorder";
            this.yTextBox_WZName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.yTextBox_WZName.Location = new System.Drawing.Point(120, 24);
            this.yTextBox_WZName.Name = "yTextBox_User";
            this.yTextBox_WZName.Size = new System.Drawing.Size(200, 21);
            this.yTextBox_WZName.TabIndex = 0;
            // 
            // yTextBox_KCDown
            // 
            // 
            // 
            // 
            this.yTextBox_KCDown.Border.Class = "TextBoxBorder";
            this.yTextBox_KCDown.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.yTextBox_KCDown.Location = new System.Drawing.Point(428, 60);
            this.yTextBox_KCDown.Name = "yTextBox_MadeSPiNum";
            this.yTextBox_KCDown.Size = new System.Drawing.Size(200, 21);
            this.yTextBox_KCDown.TabIndex = 0;
            // 
            // yTextBox_KCUp
            // 
            // 
            // 
            // 
            this.yTextBox_KCUp.Border.Class = "TextBoxBorder";
            this.yTextBox_KCUp.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.yTextBox_KCUp.Location = new System.Drawing.Point(120, 59);
            this.yTextBox_KCUp.Name = "yTextBox_PdID";
            this.yTextBox_KCUp.Size = new System.Drawing.Size(200, 21);
            this.yTextBox_KCUp.TabIndex = 0;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(356, 134);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(74, 26);
            this.btn_Cancel.TabIndex = 13;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(236, 134);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(74, 26);
            this.btn_Save.TabIndex = 12;
            this.btn_Save.Text = "确定";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // WZStockAlarm_Set
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 169);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.groupBox2);
            this.Name = "WZStockAlarm_Set";
            this.Text = "库存上下限设置";
            this.Load += new System.EventHandler(this.WZStockAlarm_Set_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label4;
        private YtWinContrl.com.contrl.YTextBox yTextBox_WZName;
        private YtWinContrl.com.contrl.YTextBox yTextBox_KCDown;
        private YtWinContrl.com.contrl.YTextBox yTextBox_KCUp;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Save;

    }
}