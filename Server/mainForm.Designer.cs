namespace Hit_Brick_Server
{
    partial class mainForm
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
            this.lbl_port = new System.Windows.Forms.Label();
            this.txt_port = new System.Windows.Forms.TextBox();
            this.rtb_log = new System.Windows.Forms.RichTextBox();
            this.lbl_log = new System.Windows.Forms.Label();
            this.btn_clearLog = new System.Windows.Forms.Button();
            this.lbl_control = new System.Windows.Forms.Label();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_port
            // 
            this.lbl_port.AutoSize = true;
            this.lbl_port.Location = new System.Drawing.Point(13, 14);
            this.lbl_port.Name = "lbl_port";
            this.lbl_port.Size = new System.Drawing.Size(41, 12);
            this.lbl_port.TabIndex = 0;
            this.lbl_port.Text = "端口号";
            // 
            // txt_port
            // 
            this.txt_port.Location = new System.Drawing.Point(15, 37);
            this.txt_port.MaxLength = 5;
            this.txt_port.Name = "txt_port";
            this.txt_port.Size = new System.Drawing.Size(109, 21);
            this.txt_port.TabIndex = 1;
            this.txt_port.Text = "8800";
            this.txt_port.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_port_KeyPress);
            // 
            // rtb_log
            // 
            this.rtb_log.Location = new System.Drawing.Point(160, 37);
            this.rtb_log.Name = "rtb_log";
            this.rtb_log.ReadOnly = true;
            this.rtb_log.Size = new System.Drawing.Size(652, 382);
            this.rtb_log.TabIndex = 2;
            this.rtb_log.Text = "";
            // 
            // lbl_log
            // 
            this.lbl_log.AutoSize = true;
            this.lbl_log.Location = new System.Drawing.Point(158, 14);
            this.lbl_log.Name = "lbl_log";
            this.lbl_log.Size = new System.Drawing.Size(29, 12);
            this.lbl_log.TabIndex = 3;
            this.lbl_log.Text = "日志";
            // 
            // btn_clearLog
            // 
            this.btn_clearLog.Location = new System.Drawing.Point(737, 425);
            this.btn_clearLog.Name = "btn_clearLog";
            this.btn_clearLog.Size = new System.Drawing.Size(75, 23);
            this.btn_clearLog.TabIndex = 4;
            this.btn_clearLog.Text = "清空";
            this.btn_clearLog.UseVisualStyleBackColor = true;
            this.btn_clearLog.Click += new System.EventHandler(this.btn_clearLog_Click);
            // 
            // lbl_control
            // 
            this.lbl_control.AutoSize = true;
            this.lbl_control.Location = new System.Drawing.Point(13, 71);
            this.lbl_control.Name = "lbl_control";
            this.lbl_control.Size = new System.Drawing.Size(29, 12);
            this.lbl_control.TabIndex = 5;
            this.lbl_control.Text = "控制";
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(15, 95);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(109, 23);
            this.btn_start.TabIndex = 6;
            this.btn_start.Text = "启动";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Enabled = false;
            this.btn_stop.Location = new System.Drawing.Point(15, 125);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(109, 23);
            this.btn_stop.TabIndex = 7;
            this.btn_stop.Text = "停止";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 457);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.lbl_control);
            this.Controls.Add(this.btn_clearLog);
            this.Controls.Add(this.lbl_log);
            this.Controls.Add(this.rtb_log);
            this.Controls.Add(this.txt_port);
            this.Controls.Add(this.lbl_port);
            this.MaximizeBox = false;
            this.Name = "mainForm";
            this.Text = "Hit Brick Server";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_port;
        private System.Windows.Forms.TextBox txt_port;
        private System.Windows.Forms.RichTextBox rtb_log;
        private System.Windows.Forms.Label lbl_log;
        private System.Windows.Forms.Button btn_clearLog;
        private System.Windows.Forms.Label lbl_control;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_stop;
    }
}

