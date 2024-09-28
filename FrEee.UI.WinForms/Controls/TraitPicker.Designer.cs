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
        components = new System.ComponentModel.Container();
        pnlTraits = new System.Windows.Forms.FlowLayoutPanel();
        toolTip = new System.Windows.Forms.ToolTip(components);
        SuspendLayout();
        // 
        // pnlTraits
        // 
        pnlTraits.AutoScroll = true;
        pnlTraits.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlTraits.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
        pnlTraits.Location = new System.Drawing.Point(0, 0);
        pnlTraits.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        pnlTraits.Name = "pnlTraits";
        pnlTraits.Size = new System.Drawing.Size(421, 546);
        pnlTraits.TabIndex = 0;
        pnlTraits.WrapContents = false;
        pnlTraits.SizeChanged += pnlTraits_SizeChanged;
        // 
        // TraitPicker
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.Black;
        Controls.Add(pnlTraits);
        ForeColor = System.Drawing.Color.White;
        Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        Name = "TraitPicker";
        Size = new System.Drawing.Size(421, 546);
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.FlowLayoutPanel pnlTraits;
	private System.Windows.Forms.ToolTip toolTip;
}
