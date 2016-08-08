namespace Main
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tb_old = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_new = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_new = new System.Windows.Forms.Button();
            this.btn_old = new System.Windows.Forms.Button();
            this.btn_run = new System.Windows.Forms.Button();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.tb_startDay = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_result = new System.Windows.Forms.TextBox();
            this.nup_row = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_smsconfirm = new System.Windows.Forms.TextBox();
            this.tb_mobileconfirm = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_smsundo = new System.Windows.Forms.TextBox();
            this.tb_mobileundo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nup_row)).BeginInit();
            this.SuspendLayout();
            // 
            // tb_old
            // 
            this.tb_old.Location = new System.Drawing.Point(57, 12);
            this.tb_old.Name = "tb_old";
            this.tb_old.Size = new System.Drawing.Size(353, 21);
            this.tb_old.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Old:";
            // 
            // tb_new
            // 
            this.tb_new.Location = new System.Drawing.Point(57, 55);
            this.tb_new.Name = "tb_new";
            this.tb_new.Size = new System.Drawing.Size(353, 21);
            this.tb_new.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "New:";
            // 
            // btn_new
            // 
            this.btn_new.Location = new System.Drawing.Point(417, 55);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(75, 23);
            this.btn_new.TabIndex = 4;
            this.btn_new.Text = "Select";
            this.btn_new.UseVisualStyleBackColor = true;
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            // 
            // btn_old
            // 
            this.btn_old.Location = new System.Drawing.Point(416, 12);
            this.btn_old.Name = "btn_old";
            this.btn_old.Size = new System.Drawing.Size(75, 23);
            this.btn_old.TabIndex = 5;
            this.btn_old.Text = "Select";
            this.btn_old.UseVisualStyleBackColor = true;
            this.btn_old.Click += new System.EventHandler(this.btn_old_Click);
            // 
            // btn_run
            // 
            this.btn_run.Location = new System.Drawing.Point(371, 93);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(75, 41);
            this.btn_run.TabIndex = 6;
            this.btn_run.Text = "Run";
            this.btn_run.UseVisualStyleBackColor = true;
            this.btn_run.Click += new System.EventHandler(this.btn_run_Click);
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(122, 127);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 14;
            this.monthCalendar1.Visible = false;
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
            // 
            // tb_startDay
            // 
            this.tb_startDay.Location = new System.Drawing.Point(122, 104);
            this.tb_startDay.Name = "tb_startDay";
            this.tb_startDay.Size = new System.Drawing.Size(102, 21);
            this.tb_startDay.TabIndex = 11;
            this.tb_startDay.Click += new System.EventHandler(this.tb_startDay_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "Sprint Start Day";
            // 
            // tb_result
            // 
            this.tb_result.Location = new System.Drawing.Point(10, 171);
            this.tb_result.Multiline = true;
            this.tb_result.Name = "tb_result";
            this.tb_result.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_result.Size = new System.Drawing.Size(481, 113);
            this.tb_result.TabIndex = 10;
            // 
            // nup_row
            // 
            this.nup_row.Location = new System.Drawing.Point(453, 144);
            this.nup_row.Name = "nup_row";
            this.nup_row.Size = new System.Drawing.Size(38, 21);
            this.nup_row.TabIndex = 15;
            this.nup_row.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(390, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "Field Row";
            // 
            // tb_smsconfirm
            // 
            this.tb_smsconfirm.Location = new System.Drawing.Point(10, 344);
            this.tb_smsconfirm.Multiline = true;
            this.tb_smsconfirm.Name = "tb_smsconfirm";
            this.tb_smsconfirm.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_smsconfirm.Size = new System.Drawing.Size(480, 142);
            this.tb_smsconfirm.TabIndex = 17;
            // 
            // tb_mobileconfirm
            // 
            this.tb_mobileconfirm.Location = new System.Drawing.Point(10, 531);
            this.tb_mobileconfirm.Multiline = true;
            this.tb_mobileconfirm.Name = "tb_mobileconfirm";
            this.tb_mobileconfirm.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_mobileconfirm.Size = new System.Drawing.Size(480, 142);
            this.tb_mobileconfirm.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 329);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(191, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "previous sprint tickets (other)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 516);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(197, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "previous sprint tickets (mobile)";
            // 
            // tb_smsundo
            // 
            this.tb_smsundo.Location = new System.Drawing.Point(521, 344);
            this.tb_smsundo.Multiline = true;
            this.tb_smsundo.Name = "tb_smsundo";
            this.tb_smsundo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_smsundo.Size = new System.Drawing.Size(480, 142);
            this.tb_smsundo.TabIndex = 22;
            // 
            // tb_mobileundo
            // 
            this.tb_mobileundo.Location = new System.Drawing.Point(521, 531);
            this.tb_mobileundo.Multiline = true;
            this.tb_mobileundo.Name = "tb_mobileundo";
            this.tb_mobileundo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_mobileundo.Size = new System.Drawing.Size(480, 142);
            this.tb_mobileundo.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(519, 329);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "SMS Undo";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(519, 507);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 12);
            this.label8.TabIndex = 25;
            this.label8.Text = "Mobile Undo";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 698);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tb_mobileundo);
            this.Controls.Add(this.tb_smsundo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tb_mobileconfirm);
            this.Controls.Add(this.tb_smsconfirm);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nup_row);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_startDay);
            this.Controls.Add(this.btn_run);
            this.Controls.Add(this.btn_old);
            this.Controls.Add(this.btn_new);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_new);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_old);
            this.Controls.Add(this.tb_result);
            this.Name = "MainForm";
            this.RightToLeftLayout = true;
            this.Text = "Main";
            ((System.ComponentModel.ISupportInitialize)(this.nup_row)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_old;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_new;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_new;
        private System.Windows.Forms.Button btn_old;
        private System.Windows.Forms.Button btn_run;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.TextBox tb_startDay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_result;
        private System.Windows.Forms.NumericUpDown nup_row;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_smsconfirm;
        private System.Windows.Forms.TextBox tb_mobileconfirm;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_smsundo;
        private System.Windows.Forms.TextBox tb_mobileundo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}

