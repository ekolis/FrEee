namespace FrEee.UI.WinForms.Controls;

partial class ResourceDisplay
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
		this.SuspendLayout();
		// 
		// ResourceDisplay
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.ForeColor = System.Drawing.Color.White;
		this.Margin = new System.Windows.Forms.Padding(0);
		this.Name = "ResourceDisplay";
		this.Size = new System.Drawing.Size(128, 20);
		this.Paint += new System.Windows.Forms.PaintEventHandler(this.ResourceDisplay_Paint);
		this.ResumeLayout(false);

	}

	#endregion


}
