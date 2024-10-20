namespace FrEee.UI.WinForms.Controls;

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
		textBox = new System.Windows.Forms.TextBox();
		SuspendLayout();
		// 
		// textBox
		// 
		textBox.Dock = System.Windows.Forms.DockStyle.Fill;
		textBox.Location = new System.Drawing.Point(0, 0);
		textBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		textBox.Name = "textBox";
		textBox.Size = new System.Drawing.Size(690, 23);
		textBox.TabIndex = 0;
		textBox.SizeChanged += textBox_SizeChanged;
		textBox.TextChanged += textBox_TextChanged;
		textBox.KeyDown += textBox_KeyDown;
		textBox.Leave += textBox_Leave;
		// 
		// SearchBox
		// 
		AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
		AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		BackColor = System.Drawing.Color.Black;
		Controls.Add(textBox);
		ForeColor = System.Drawing.Color.White;
		Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		Name = "SearchBox";
		Size = new System.Drawing.Size(690, 25);
		Load += SearchBox_Load;
		SizeChanged += SearchBox_SizeChanged;
		KeyDown += SearchBox_KeyDown;
		Leave += SearchBox_Leave;
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private System.Windows.Forms.TextBox textBox;
}
