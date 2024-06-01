namespace FrEee.UI.WinForms.Controls;

partial class TraitPicker
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
		this.pnlTraits = new System.Windows.Forms.FlowLayoutPanel();
		this.toolTip = new System.Windows.Forms.ToolTip(this.components);
		this.SuspendLayout();
		// 
		// pnlTraits
		// 
		this.pnlTraits.AutoScroll = true;
		this.pnlTraits.Dock = System.Windows.Forms.DockStyle.Fill;
		this.pnlTraits.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
		this.pnlTraits.Location = new System.Drawing.Point(0, 0);
		this.pnlTraits.Name = "pnlTraits";
		this.pnlTraits.Size = new System.Drawing.Size(361, 473);
		this.pnlTraits.TabIndex = 0;
		this.pnlTraits.WrapContents = false;
		this.pnlTraits.SizeChanged += new System.EventHandler(this.pnlTraits_SizeChanged);
		// 
		// TraitPicker
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.Controls.Add(this.pnlTraits);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "TraitPicker";
		this.Size = new System.Drawing.Size(361, 473);
		this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.FlowLayoutPanel pnlTraits;
	private System.Windows.Forms.ToolTip toolTip;
}
