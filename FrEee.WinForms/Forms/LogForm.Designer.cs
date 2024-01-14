namespace FrEee.WinForms.Forms;

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
		this.colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
		this.colMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
		this.SuspendLayout();
		// 
		// lstLog
		// 
		this.lstLog.BackColor = System.Drawing.Color.Black;
		this.lstLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDate,
            this.colMessage});
		this.lstLog.Dock = System.Windows.Forms.DockStyle.Fill;
		this.lstLog.ForeColor = System.Drawing.Color.White;
		this.lstLog.FullRowSelect = true;
		this.lstLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
		this.lstLog.Location = new System.Drawing.Point(0, 0);
		this.lstLog.Name = "lstLog";
		this.lstLog.ShowItemToolTips = true;
		this.lstLog.Size = new System.Drawing.Size(774, 492);
		this.lstLog.TabIndex = 0;
		this.lstLog.UseCompatibleStateImageBehavior = false;
		this.lstLog.View = System.Windows.Forms.View.Details;
		this.lstLog.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstLog_MouseDoubleClick);
		// 
		// colDate
		// 
		this.colDate.Text = "Date";
		this.colDate.Width = 100;
		// 
		// colMessage
		// 
		this.colMessage.Text = "Message";
		this.colMessage.Width = 670;
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
		this.SizeChanged += new System.EventHandler(this.LogForm_SizeChanged);
		this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.ListView lstLog;
	private System.Windows.Forms.ColumnHeader colMessage;
	private System.Windows.Forms.ColumnHeader colDate;

}