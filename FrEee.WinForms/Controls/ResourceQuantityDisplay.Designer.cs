namespace FrEee.WinForms.Controls;

partial class ResourceQuantityDisplay
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
		this.min = new FrEee.WinForms.Controls.ResourceDisplay();
		this.org = new FrEee.WinForms.Controls.ResourceDisplay();
		this.rad = new FrEee.WinForms.Controls.ResourceDisplay();
		this.SuspendLayout();
		// 
		// min
		// 
		this.min.Amount = 0;
		this.min.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
		this.min.BackColor = System.Drawing.Color.Black;
		this.min.Change = null;
		this.min.ForeColor = System.Drawing.Color.White;
		this.min.Location = new System.Drawing.Point(0, 0);
		this.min.Margin = new System.Windows.Forms.Padding(0);
		this.min.Name = "min";
		this.min.ResourceName = "Minerals";
		this.min.Size = new System.Drawing.Size(128, 26);
		this.min.TabIndex = 0;
		// 
		// org
		// 
		this.org.Amount = 0;
		this.org.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
		this.org.BackColor = System.Drawing.Color.Black;
		this.org.Change = null;
		this.org.ForeColor = System.Drawing.Color.White;
		this.org.Location = new System.Drawing.Point(128, 0);
		this.org.Margin = new System.Windows.Forms.Padding(0);
		this.org.Name = "org";
		this.org.ResourceName = "Organics";
		this.org.Size = new System.Drawing.Size(128, 26);
		this.org.TabIndex = 1;
		// 
		// rad
		// 
		this.rad.Amount = 0;
		this.rad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
		this.rad.BackColor = System.Drawing.Color.Black;
		this.rad.Change = null;
		this.rad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
		this.rad.ForeColor = System.Drawing.Color.White;
		this.rad.Location = new System.Drawing.Point(256, 0);
		this.rad.Margin = new System.Windows.Forms.Padding(0);
		this.rad.Name = "rad";
		this.rad.ResourceName = "Radioactives";
		this.rad.Size = new System.Drawing.Size(131, 26);
		this.rad.TabIndex = 2;
		// 
		// ResourceQuantityDisplay
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.Controls.Add(this.rad);
		this.Controls.Add(this.org);
		this.Controls.Add(this.min);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "ResourceQuantityDisplay";
		this.Size = new System.Drawing.Size(387, 26);
		this.ResumeLayout(false);

	}

	#endregion

	private ResourceDisplay min;
	private ResourceDisplay org;
	private ResourceDisplay rad;
}
