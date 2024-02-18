namespace FrEee.WinForms.Controls;

partial class AsteroidFieldReport
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
		this.gameTabControl1 = new FrEee.WinForms.Controls.GameTabControl();
		this.pageDetail = new System.Windows.Forms.TabPage();
		this.txtAge = new System.Windows.Forms.Label();
		this.txtValueRadioactives = new System.Windows.Forms.Label();
		this.txtValueOrganics = new System.Windows.Forms.Label();
		this.txtValueMinerals = new System.Windows.Forms.Label();
		this.picPortrait = new FrEee.WinForms.Controls.GamePictureBox();
		this.txtDescription = new System.Windows.Forms.Label();
		this.lblValue = new System.Windows.Forms.Label();
		this.txtConditions = new System.Windows.Forms.Label();
		this.lblConditions = new System.Windows.Forms.Label();
		this.txtAtmosphere = new System.Windows.Forms.Label();
		this.lblAtmosphere = new System.Windows.Forms.Label();
		this.txtSizeSurface = new System.Windows.Forms.Label();
		this.txtName = new System.Windows.Forms.Label();
		this.pageAbility = new System.Windows.Forms.TabPage();
		this.abilityTreeView = new FrEee.WinForms.Controls.AbilityTreeView();
		this.gameTabControl1.SuspendLayout();
		this.pageDetail.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.picPortrait)).BeginInit();
		this.pageAbility.SuspendLayout();
		this.SuspendLayout();
		// 
		// gameTabControl1
		// 
		this.gameTabControl1.Controls.Add(this.pageDetail);
		this.gameTabControl1.Controls.Add(this.pageAbility);
		this.gameTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gameTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
		this.gameTabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.gameTabControl1.Location = new System.Drawing.Point(0, 0);
		this.gameTabControl1.Name = "gameTabControl1";
		this.gameTabControl1.SelectedIndex = 0;
		this.gameTabControl1.SelectedTabBackColor = System.Drawing.Color.SkyBlue;
		this.gameTabControl1.SelectedTabForeColor = System.Drawing.Color.Black;
		this.gameTabControl1.Size = new System.Drawing.Size(320, 459);
		this.gameTabControl1.TabBackColor = System.Drawing.Color.Black;
		this.gameTabControl1.TabBorderColor = System.Drawing.Color.CornflowerBlue;
		this.gameTabControl1.TabForeColor = System.Drawing.Color.CornflowerBlue;
		this.gameTabControl1.TabIndex = 1;
		// 
		// pageDetail
		// 
		this.pageDetail.AutoScroll = true;
		this.pageDetail.BackColor = System.Drawing.Color.Black;
		this.pageDetail.Controls.Add(this.txtAge);
		this.pageDetail.Controls.Add(this.txtValueRadioactives);
		this.pageDetail.Controls.Add(this.txtValueOrganics);
		this.pageDetail.Controls.Add(this.txtValueMinerals);
		this.pageDetail.Controls.Add(this.picPortrait);
		this.pageDetail.Controls.Add(this.txtDescription);
		this.pageDetail.Controls.Add(this.lblValue);
		this.pageDetail.Controls.Add(this.txtConditions);
		this.pageDetail.Controls.Add(this.lblConditions);
		this.pageDetail.Controls.Add(this.txtAtmosphere);
		this.pageDetail.Controls.Add(this.lblAtmosphere);
		this.pageDetail.Controls.Add(this.txtSizeSurface);
		this.pageDetail.Controls.Add(this.txtName);
		this.pageDetail.Location = new System.Drawing.Point(4, 29);
		this.pageDetail.Name = "pageDetail";
		this.pageDetail.Padding = new System.Windows.Forms.Padding(3);
		this.pageDetail.Size = new System.Drawing.Size(312, 426);
		this.pageDetail.TabIndex = 0;
		this.pageDetail.Text = "Detail";
		// 
		// txtAge
		// 
		this.txtAge.AutoSize = true;
		this.txtAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtAge.Location = new System.Drawing.Point(162, 43);
		this.txtAge.Name = "txtAge";
		this.txtAge.Size = new System.Drawing.Size(47, 15);
		this.txtAge.TabIndex = 55;
		this.txtAge.Text = "Current";
		// 
		// txtValueRadioactives
		// 
		this.txtValueRadioactives.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtValueRadioactives.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
		this.txtValueRadioactives.Location = new System.Drawing.Point(230, 133);
		this.txtValueRadioactives.Name = "txtValueRadioactives";
		this.txtValueRadioactives.Size = new System.Drawing.Size(45, 23);
		this.txtValueRadioactives.TabIndex = 54;
		this.txtValueRadioactives.Text = "150%";
		// 
		// txtValueOrganics
		// 
		this.txtValueOrganics.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtValueOrganics.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
		this.txtValueOrganics.Location = new System.Drawing.Point(193, 133);
		this.txtValueOrganics.Name = "txtValueOrganics";
		this.txtValueOrganics.Size = new System.Drawing.Size(45, 23);
		this.txtValueOrganics.TabIndex = 53;
		this.txtValueOrganics.Text = "150%";
		// 
		// txtValueMinerals
		// 
		this.txtValueMinerals.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtValueMinerals.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
		this.txtValueMinerals.Location = new System.Drawing.Point(159, 133);
		this.txtValueMinerals.Name = "txtValueMinerals";
		this.txtValueMinerals.Size = new System.Drawing.Size(45, 23);
		this.txtValueMinerals.TabIndex = 52;
		this.txtValueMinerals.Text = "150%";
		// 
		// picPortrait
		// 
		this.picPortrait.Location = new System.Drawing.Point(6, 32);
		this.picPortrait.Name = "picPortrait";
		this.picPortrait.Size = new System.Drawing.Size(128, 128);
		this.picPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.picPortrait.TabIndex = 51;
		this.picPortrait.TabStop = false;
		this.picPortrait.Click += new System.EventHandler(this.picPortrait_Click);
		// 
		// txtDescription
		// 
		this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtDescription.Location = new System.Drawing.Point(10, 163);
		this.txtDescription.Name = "txtDescription";
		this.txtDescription.Size = new System.Drawing.Size(296, 18);
		this.txtDescription.TabIndex = 50;
		this.txtDescription.Text = "A field of asteroids orbiting the star.";
		// 
		// lblValue
		// 
		this.lblValue.AutoSize = true;
		this.lblValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblValue.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblValue.Location = new System.Drawing.Point(144, 118);
		this.lblValue.Name = "lblValue";
		this.lblValue.Size = new System.Drawing.Size(38, 15);
		this.lblValue.TabIndex = 16;
		this.lblValue.Text = "Value";
		// 
		// txtConditions
		// 
		this.txtConditions.AutoSize = true;
		this.txtConditions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtConditions.Location = new System.Drawing.Point(159, 103);
		this.txtConditions.Name = "txtConditions";
		this.txtConditions.Size = new System.Drawing.Size(50, 15);
		this.txtConditions.TabIndex = 15;
		this.txtConditions.Text = "Optimal";
		// 
		// lblConditions
		// 
		this.lblConditions.AutoSize = true;
		this.lblConditions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblConditions.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblConditions.Location = new System.Drawing.Point(144, 88);
		this.lblConditions.Name = "lblConditions";
		this.lblConditions.Size = new System.Drawing.Size(65, 15);
		this.lblConditions.TabIndex = 14;
		this.lblConditions.Text = "Conditions";
		// 
		// txtAtmosphere
		// 
		this.txtAtmosphere.AutoSize = true;
		this.txtAtmosphere.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtAtmosphere.Location = new System.Drawing.Point(159, 73);
		this.txtAtmosphere.Name = "txtAtmosphere";
		this.txtAtmosphere.Size = new System.Drawing.Size(37, 15);
		this.txtAtmosphere.TabIndex = 13;
		this.txtAtmosphere.Text = "None";
		// 
		// lblAtmosphere
		// 
		this.lblAtmosphere.AutoSize = true;
		this.lblAtmosphere.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblAtmosphere.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblAtmosphere.Location = new System.Drawing.Point(144, 58);
		this.lblAtmosphere.Name = "lblAtmosphere";
		this.lblAtmosphere.Size = new System.Drawing.Size(73, 15);
		this.lblAtmosphere.TabIndex = 12;
		this.lblAtmosphere.Text = "Atmosphere";
		// 
		// txtSizeSurface
		// 
		this.txtSizeSurface.AutoSize = true;
		this.txtSizeSurface.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtSizeSurface.Location = new System.Drawing.Point(159, 26);
		this.txtSizeSurface.Name = "txtSizeSurface";
		this.txtSizeSurface.Size = new System.Drawing.Size(147, 15);
		this.txtSizeSurface.TabIndex = 11;
		this.txtSizeSurface.Text = "Large Rock Asteroid Field";
		// 
		// txtName
		// 
		this.txtName.AutoSize = true;
		this.txtName.Location = new System.Drawing.Point(143, 6);
		this.txtName.Name = "txtName";
		this.txtName.Size = new System.Drawing.Size(169, 20);
		this.txtName.TabIndex = 10;
		this.txtName.Text = "Tudran Asteroid Field I";
		// 
		// pageAbility
		// 
		this.pageAbility.BackColor = System.Drawing.Color.Black;
		this.pageAbility.Controls.Add(this.abilityTreeView);
		this.pageAbility.Location = new System.Drawing.Point(4, 29);
		this.pageAbility.Name = "pageAbility";
		this.pageAbility.Padding = new System.Windows.Forms.Padding(3);
		this.pageAbility.Size = new System.Drawing.Size(312, 426);
		this.pageAbility.TabIndex = 4;
		this.pageAbility.Text = "Ability";
		// 
		// abilityTreeView
		// 
		this.abilityTreeView.Abilities = null;
		this.abilityTreeView.BackColor = System.Drawing.Color.Black;
		this.abilityTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
		this.abilityTreeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.abilityTreeView.ForeColor = System.Drawing.Color.White;
		this.abilityTreeView.IntrinsicAbilities = null;
		this.abilityTreeView.Location = new System.Drawing.Point(3, 3);
		this.abilityTreeView.Name = "abilityTreeView";
		this.abilityTreeView.Size = new System.Drawing.Size(306, 420);
		this.abilityTreeView.TabIndex = 1;
		// 
		// AsteroidFieldReport
		// 
		this.AutoScroll = true;
		this.BackColor = System.Drawing.Color.Black;
		this.Controls.Add(this.gameTabControl1);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "AsteroidFieldReport";
		this.Size = new System.Drawing.Size(320, 459);
		this.gameTabControl1.ResumeLayout(false);
		this.pageDetail.ResumeLayout(false);
		this.pageDetail.PerformLayout();
		((System.ComponentModel.ISupportInitialize)(this.picPortrait)).EndInit();
		this.pageAbility.ResumeLayout(false);
		this.ResumeLayout(false);

	}

	#endregion

	private GameTabControl gameTabControl1;
	private System.Windows.Forms.TabPage pageDetail;
	private System.Windows.Forms.Label lblValue;
	private System.Windows.Forms.Label txtConditions;
	private System.Windows.Forms.Label lblConditions;
	private System.Windows.Forms.Label txtAtmosphere;
	private System.Windows.Forms.Label lblAtmosphere;
	private System.Windows.Forms.Label txtSizeSurface;
	private System.Windows.Forms.Label txtName;
	private System.Windows.Forms.TabPage pageAbility;
	private System.Windows.Forms.Label txtDescription;
	private GamePictureBox picPortrait;
	private AbilityTreeView abilityTreeView;
	private System.Windows.Forms.Label txtValueRadioactives;
	private System.Windows.Forms.Label txtValueOrganics;
	private System.Windows.Forms.Label txtValueMinerals;
	private System.Windows.Forms.Label txtAge;
}
