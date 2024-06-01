namespace FrEee.UI.WinForms.Controls;

partial class AptitudePicker
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

	#region Component Designer generated code

	/// <summary> 
	/// Required method for Designer support - do not modify 
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		this.pnl = new System.Windows.Forms.TableLayoutPanel();
		this.toolTip = new System.Windows.Forms.ToolTip(this.components);
		this.SuspendLayout();
		// 
		// pnl
		// 
		this.pnl.ColumnCount = 2;
		this.pnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
		this.pnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
		this.pnl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.pnl.Location = new System.Drawing.Point(0, 0);
		this.pnl.Name = "pnl";
		this.pnl.RowCount = 2;
		this.pnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
		this.pnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
		this.pnl.Size = new System.Drawing.Size(368, 524);
		this.pnl.TabIndex = 0;
		// 
		// AptitudePicker
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.Controls.Add(this.pnl);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "AptitudePicker";
		this.Size = new System.Drawing.Size(368, 524);
		this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.TableLayoutPanel pnl;
	private System.Windows.Forms.ToolTip toolTip;
}
