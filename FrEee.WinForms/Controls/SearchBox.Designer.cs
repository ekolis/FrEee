namespace FrEee.WinForms.Controls;

partial class SearchBox
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
		this.textBox = new System.Windows.Forms.TextBox();
		this.SuspendLayout();
		// 
		// textBox
		// 
		this.textBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.textBox.Location = new System.Drawing.Point(0, 0);
		this.textBox.Name = "textBox";
		this.textBox.Size = new System.Drawing.Size(591, 20);
		this.textBox.TabIndex = 0;
		this.textBox.SizeChanged += new System.EventHandler(this.textBox_SizeChanged);
		this.textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
		this.textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
		this.textBox.Leave += new System.EventHandler(this.textBox_Leave);
		// 
		// SearchBox
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.Controls.Add(this.textBox);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "SearchBox";
		this.Size = new System.Drawing.Size(591, 22);
		this.Load += new System.EventHandler(this.SearchBox_Load);
		this.SizeChanged += new System.EventHandler(this.SearchBox_SizeChanged);
		this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchBox_KeyDown);
		this.Leave += new System.EventHandler(this.SearchBox_Leave);
		this.ResumeLayout(false);
		this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.TextBox textBox;
}
