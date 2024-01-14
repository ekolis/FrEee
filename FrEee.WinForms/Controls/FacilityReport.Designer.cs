namespace FrEee.WinForms.Controls;

partial class FacilityReport
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
		this.resRad = new FrEee.WinForms.Controls.ResourceDisplay();
		this.resOrg = new FrEee.WinForms.Controls.ResourceDisplay();
		this.resMin = new FrEee.WinForms.Controls.ResourceDisplay();
		this.label2 = new System.Windows.Forms.Label();
		this.txtDescription = new System.Windows.Forms.Label();
		this.txtName = new System.Windows.Forms.Label();
		this.picPortrait = new FrEee.WinForms.Controls.GamePictureBox();
		this.abilityTree = new FrEee.WinForms.Controls.AbilityTreeView();
		((System.ComponentModel.ISupportInitialize)(this.picPortrait)).BeginInit();
		this.SuspendLayout();
		// 
		// resRad
		// 
		this.resRad.Amount = 0;
		this.resRad.BackColor = System.Drawing.Color.Black;
		this.resRad.Change = null;
		this.resRad.ForeColor = System.Drawing.Color.White;
		this.resRad.Location = new System.Drawing.Point(287, 134);
		this.resRad.Margin = new System.Windows.Forms.Padding(0);
		this.resRad.Name = "resRad";
		this.resRad.ResourceName = "Radioactives";
		this.resRad.Size = new System.Drawing.Size(73, 20);
		this.resRad.TabIndex = 20;
		// 
		// resOrg
		// 
		this.resOrg.Amount = 0;
		this.resOrg.BackColor = System.Drawing.Color.Black;
		this.resOrg.Change = null;
		this.resOrg.ForeColor = System.Drawing.Color.White;
		this.resOrg.Location = new System.Drawing.Point(211, 134);
		this.resOrg.Margin = new System.Windows.Forms.Padding(0);
		this.resOrg.Name = "resOrg";
		this.resOrg.ResourceName = "Organics";
		this.resOrg.Size = new System.Drawing.Size(73, 20);
		this.resOrg.TabIndex = 19;
		// 
		// resMin
		// 
		this.resMin.Amount = 0;
		this.resMin.BackColor = System.Drawing.Color.Black;
		this.resMin.Change = null;
		this.resMin.ForeColor = System.Drawing.Color.White;
		this.resMin.Location = new System.Drawing.Point(138, 134);
		this.resMin.Margin = new System.Windows.Forms.Padding(0);
		this.resMin.Name = "resMin";
		this.resMin.ResourceName = "Minerals";
		this.resMin.Size = new System.Drawing.Size(73, 20);
		this.resMin.TabIndex = 18;
		// 
		// label2
		// 
		this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label2.Location = new System.Drawing.Point(3, 134);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(128, 18);
		this.label2.TabIndex = 16;
		this.label2.Text = "Cost";
		// 
		// txtDescription
		// 
		this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtDescription.Location = new System.Drawing.Point(137, 49);
		this.txtDescription.Name = "txtDescription";
		this.txtDescription.Size = new System.Drawing.Size(228, 82);
		this.txtDescription.TabIndex = 15;
		this.txtDescription.Text = "Description";
		// 
		// txtName
		// 
		this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtName.Location = new System.Drawing.Point(138, 4);
		this.txtName.Name = "txtName";
		this.txtName.Size = new System.Drawing.Size(227, 45);
		this.txtName.TabIndex = 14;
		this.txtName.Text = "Name";
		// 
		// picPortrait
		// 
		this.picPortrait.Location = new System.Drawing.Point(3, 3);
		this.picPortrait.Name = "picPortrait";
		this.picPortrait.Size = new System.Drawing.Size(128, 128);
		this.picPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.picPortrait.TabIndex = 13;
		this.picPortrait.TabStop = false;
		// 
		// abilityTree
		// 
		this.abilityTree.Abilities = null;
		this.abilityTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.abilityTree.BackColor = System.Drawing.Color.Black;
		this.abilityTree.ForeColor = System.Drawing.Color.White;
		this.abilityTree.IntrinsicAbilities = null;
		this.abilityTree.Location = new System.Drawing.Point(3, 157);
		this.abilityTree.Name = "abilityTree";
		this.abilityTree.Size = new System.Drawing.Size(357, 292);
		this.abilityTree.TabIndex = 17;
		// 
		// FacilityReport
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.Controls.Add(this.resRad);
		this.Controls.Add(this.resOrg);
		this.Controls.Add(this.resMin);
		this.Controls.Add(this.label2);
		this.Controls.Add(this.txtDescription);
		this.Controls.Add(this.txtName);
		this.Controls.Add(this.picPortrait);
		this.Controls.Add(this.abilityTree);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "FacilityReport";
		this.Size = new System.Drawing.Size(365, 452);
		((System.ComponentModel.ISupportInitialize)(this.picPortrait)).EndInit();
		this.ResumeLayout(false);

	}

	#endregion

	private ResourceDisplay resRad;
	private ResourceDisplay resOrg;
	private ResourceDisplay resMin;
	private System.Windows.Forms.Label label2;
	private System.Windows.Forms.Label txtDescription;
	private System.Windows.Forms.Label txtName;
	private GamePictureBox picPortrait;
	private AbilityTreeView abilityTree;
}
