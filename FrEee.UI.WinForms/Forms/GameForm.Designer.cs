namespace FrEee.UI.WinForms.Forms;

partial class GameForm
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
	protected void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
		this.SuspendLayout();
		// 
		// GameForm
		// 
		this.BackColor = System.Drawing.Color.Black;
		this.ClientSize = new System.Drawing.Size(284, 261);
		this.ForeColor = System.Drawing.Color.White;
		this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
		this.Name = "GameForm";
		this.Text = "FrEee";
		this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameForm_FormClosed);
		this.Load += new System.EventHandler(this.GameForm_Load);
		this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
		this.ResumeLayout(false);

	}

	#endregion
}