namespace FrEee.WinForms.Forms
{
	partial class LogForm
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
			this.lstLog = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.SuspendLayout();
			// 
			// lstLog
			// 
			this.lstLog.BackColor = System.Drawing.Color.Black;
			this.lstLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
			this.lstLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstLog.ForeColor = System.Drawing.Color.White;
			this.lstLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lstLog.Location = new System.Drawing.Point(0, 0);
			this.lstLog.Name = "lstLog";
			this.lstLog.ShowItemToolTips = true;
			this.lstLog.Size = new System.Drawing.Size(774, 492);
			this.lstLog.TabIndex = 0;
			this.lstLog.UseCompatibleStateImageBehavior = false;
			this.lstLog.View = System.Windows.Forms.View.List;
			this.lstLog.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstLog_MouseDoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Width = 0;
			// 
			// LogForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(774, 492);
			this.Controls.Add(this.lstLog);
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "LogForm";
			this.Text = "Log";
			this.Load += new System.EventHandler(this.LogForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lstLog;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;

	}
}