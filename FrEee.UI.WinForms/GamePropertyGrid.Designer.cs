namespace FrEee.WinForms;

partial class GamePropertyGrid
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
		this.table = new FrEee.WinForms.Controls.GameTableLayoutPanel();
		this.SuspendLayout();
		// 
		// table
		// 
		this.table.AutoScroll = true;
		this.table.ColumnCount = 2;
		this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
		this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
		this.table.Dock = System.Windows.Forms.DockStyle.Fill;
		this.table.Location = new System.Drawing.Point(0, 0);
		this.table.Name = "table";
		this.table.RowCount = 2;
		this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
		this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
		this.table.Size = new System.Drawing.Size(559, 684);
		this.table.TabIndex = 0;
		// 
		// GamePropertyGrid
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.Controls.Add(this.table);
		this.Name = "GamePropertyGrid";
		this.Size = new System.Drawing.Size(559, 684);
		this.ResumeLayout(false);

	}

	#endregion

	private Controls.GameTableLayoutPanel table;
}
