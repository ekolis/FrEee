namespace FrEee.UI.WinForms.Forms;

partial class GameOverForm
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
		this.btnOk = new FrEee.UI.WinForms.Controls.GameButton();
		this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
		this.pic = new FrEee.UI.WinForms.Controls.Blazor.GamePictureBox();
		((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
		this.SuspendLayout();
		// 
		// btnOk
		// 
		this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.btnOk.BackColor = System.Drawing.Color.Black;
		this.btnOk.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnOk.Location = new System.Drawing.Point(385, 534);
		this.btnOk.Name = "btnOk";
		this.btnOk.Size = new System.Drawing.Size(140, 29);
		this.btnOk.TabIndex = 26;
		this.btnOk.Text = "OK";
		this.btnOk.UseVisualStyleBackColor = false;
		this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
		// 
		// pic
		// 
		this.pic.Location = new System.Drawing.Point(12, 12);
		this.pic.Name = "pic";
		this.pic.Size = new System.Drawing.Size(512, 512);
		this.pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.pic.TabIndex = 27;
		this.pic.TabStop = false;
		// 
		// GameOverForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.ClientSize = new System.Drawing.Size(537, 575);
		this.Controls.Add(this.pic);
		this.Controls.Add(this.btnOk);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "GameOverForm";
		this.ShowInTaskbar = false;
		this.Text = "Game Over";
		((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
		this.ResumeLayout(false);

	}

	#endregion
	private Controls.GameButton btnOk;
	private System.Windows.Forms.ColumnHeader columnHeader1;
	private FrEee.UI.WinForms.Controls.Blazor.GamePictureBox pic;
}