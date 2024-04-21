namespace FrEee.UI.WinForms.Forms;

partial class DebugForm
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
		this.txtCommand = new System.Windows.Forms.TextBox();
		this.rtbOutput = new System.Windows.Forms.RichTextBox();
		this.btnSubmit = new FrEee.UI.WinForms.Controls.GameButton();
		this.SuspendLayout();
		// 
		// txtCommand
		// 
		this.txtCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtCommand.Location = new System.Drawing.Point(12, 434);
		this.txtCommand.Name = "txtCommand";
		this.txtCommand.Size = new System.Drawing.Size(754, 20);
		this.txtCommand.TabIndex = 0;
		// 
		// rtbOutput
		// 
		this.rtbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.rtbOutput.BackColor = System.Drawing.Color.Black;
		this.rtbOutput.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.rtbOutput.Location = new System.Drawing.Point(12, 13);
		this.rtbOutput.Name = "rtbOutput";
		this.rtbOutput.Size = new System.Drawing.Size(840, 415);
		this.rtbOutput.TabIndex = 1;
		this.rtbOutput.Text = "";
		// 
		// btnSubmit
		// 
		this.btnSubmit.BackColor = System.Drawing.Color.Black;
		this.btnSubmit.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnSubmit.Location = new System.Drawing.Point(772, 434);
		this.btnSubmit.Name = "btnSubmit";
		this.btnSubmit.Size = new System.Drawing.Size(80, 20);
		this.btnSubmit.TabIndex = 2;
		this.btnSubmit.Text = "Submit";
		this.btnSubmit.UseVisualStyleBackColor = false;
		this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
		// 
		// DebugForm
		// 
		this.AcceptButton = this.btnSubmit;
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.ClientSize = new System.Drawing.Size(864, 466);
		this.Controls.Add(this.btnSubmit);
		this.Controls.Add(this.rtbOutput);
		this.Controls.Add(this.txtCommand);
		this.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.Name = "DebugForm";
		this.Text = "Debug Console";
		this.ResumeLayout(false);
		this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.TextBox txtCommand;
	private System.Windows.Forms.RichTextBox rtbOutput;
	private Controls.GameButton btnSubmit;
}