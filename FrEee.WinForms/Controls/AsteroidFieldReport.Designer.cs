namespace FrEee.WinForms.Controls
{
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
			System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Racial Trait: +5");
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("War Shrine: +10");
			System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Attack Modifier: +15", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8});
			System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Happiness: +10");
			System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Population: +30");
			System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Minerals Income Modifier: +40", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode11});
			this.gameTabControl1 = new FrEee.WinForms.Controls.GameTabControl();
			this.pageDetail = new System.Windows.Forms.TabPage();
			this.txtDescription = new System.Windows.Forms.Label();
			this.txtValueRadioactives = new System.Windows.Forms.Label();
			this.txtValueOrganics = new System.Windows.Forms.Label();
			this.txtValueMinerals = new System.Windows.Forms.Label();
			this.lblValue = new System.Windows.Forms.Label();
			this.txtConditions = new System.Windows.Forms.Label();
			this.lblConditions = new System.Windows.Forms.Label();
			this.txtAtmosphere = new System.Windows.Forms.Label();
			this.lblAtmosphere = new System.Windows.Forms.Label();
			this.txtSizeSurface = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.Label();
			this.picPortrait = new System.Windows.Forms.PictureBox();
			this.picOwnerFlag = new System.Windows.Forms.PictureBox();
			this.pageAbility = new System.Windows.Forms.TabPage();
			this.treeAbilities = new System.Windows.Forms.TreeView();
			this.gameTabControl1.SuspendLayout();
			this.pageDetail.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picPortrait)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picOwnerFlag)).BeginInit();
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
			this.gameTabControl1.TabForeColor = System.Drawing.Color.CornflowerBlue;
			this.gameTabControl1.TabIndex = 1;
			// 
			// pageDetail
			// 
			this.pageDetail.AutoScroll = true;
			this.pageDetail.BackColor = System.Drawing.Color.Black;
			this.pageDetail.Controls.Add(this.txtDescription);
			this.pageDetail.Controls.Add(this.txtValueRadioactives);
			this.pageDetail.Controls.Add(this.txtValueOrganics);
			this.pageDetail.Controls.Add(this.txtValueMinerals);
			this.pageDetail.Controls.Add(this.lblValue);
			this.pageDetail.Controls.Add(this.txtConditions);
			this.pageDetail.Controls.Add(this.lblConditions);
			this.pageDetail.Controls.Add(this.txtAtmosphere);
			this.pageDetail.Controls.Add(this.lblAtmosphere);
			this.pageDetail.Controls.Add(this.txtSizeSurface);
			this.pageDetail.Controls.Add(this.txtName);
			this.pageDetail.Controls.Add(this.picPortrait);
			this.pageDetail.Controls.Add(this.picOwnerFlag);
			this.pageDetail.Location = new System.Drawing.Point(4, 29);
			this.pageDetail.Name = "pageDetail";
			this.pageDetail.Padding = new System.Windows.Forms.Padding(3);
			this.pageDetail.Size = new System.Drawing.Size(312, 426);
			this.pageDetail.TabIndex = 0;
			this.pageDetail.Text = "Detail";
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
			// txtValueRadioactives
			// 
			this.txtValueRadioactives.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtValueRadioactives.ForeColor = System.Drawing.Color.Red;
			this.txtValueRadioactives.Location = new System.Drawing.Point(227, 133);
			this.txtValueRadioactives.Name = "txtValueRadioactives";
			this.txtValueRadioactives.Size = new System.Drawing.Size(45, 23);
			this.txtValueRadioactives.TabIndex = 49;
			this.txtValueRadioactives.Text = "150%";
			// 
			// txtValueOrganics
			// 
			this.txtValueOrganics.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtValueOrganics.ForeColor = System.Drawing.Color.Lime;
			this.txtValueOrganics.Location = new System.Drawing.Point(190, 133);
			this.txtValueOrganics.Name = "txtValueOrganics";
			this.txtValueOrganics.Size = new System.Drawing.Size(45, 23);
			this.txtValueOrganics.TabIndex = 48;
			this.txtValueOrganics.Text = "150%";
			// 
			// txtValueMinerals
			// 
			this.txtValueMinerals.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtValueMinerals.ForeColor = System.Drawing.Color.Blue;
			this.txtValueMinerals.Location = new System.Drawing.Point(156, 133);
			this.txtValueMinerals.Name = "txtValueMinerals";
			this.txtValueMinerals.Size = new System.Drawing.Size(45, 23);
			this.txtValueMinerals.TabIndex = 47;
			this.txtValueMinerals.Text = "150%";
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
			// picPortrait
			// 
			this.picPortrait.Location = new System.Drawing.Point(8, 32);
			this.picPortrait.Name = "picPortrait";
			this.picPortrait.Size = new System.Drawing.Size(128, 128);
			this.picPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picPortrait.TabIndex = 9;
			this.picPortrait.TabStop = false;
			// 
			// picOwnerFlag
			// 
			this.picOwnerFlag.Location = new System.Drawing.Point(6, 6);
			this.picOwnerFlag.Name = "picOwnerFlag";
			this.picOwnerFlag.Size = new System.Drawing.Size(34, 20);
			this.picOwnerFlag.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picOwnerFlag.TabIndex = 8;
			this.picOwnerFlag.TabStop = false;
			// 
			// pageAbility
			// 
			this.pageAbility.BackColor = System.Drawing.Color.Black;
			this.pageAbility.Controls.Add(this.treeAbilities);
			this.pageAbility.Location = new System.Drawing.Point(4, 29);
			this.pageAbility.Name = "pageAbility";
			this.pageAbility.Padding = new System.Windows.Forms.Padding(3);
			this.pageAbility.Size = new System.Drawing.Size(312, 426);
			this.pageAbility.TabIndex = 4;
			this.pageAbility.Text = "Ability";
			// 
			// treeAbilities
			// 
			this.treeAbilities.BackColor = System.Drawing.Color.Black;
			this.treeAbilities.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeAbilities.ForeColor = System.Drawing.Color.White;
			this.treeAbilities.Location = new System.Drawing.Point(3, 3);
			this.treeAbilities.Name = "treeAbilities";
			treeNode7.Name = "Node1";
			treeNode7.Text = "Racial Trait: +5";
			treeNode8.Name = "Node3";
			treeNode8.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic);
			treeNode8.Text = "War Shrine: +10";
			treeNode9.Name = "Node0";
			treeNode9.Text = "Attack Modifier: +15";
			treeNode10.Name = "Node1";
			treeNode10.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			treeNode10.Text = "Happiness: +10";
			treeNode11.Name = "Node2";
			treeNode11.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			treeNode11.Text = "Population: +30";
			treeNode12.Name = "Node0";
			treeNode12.Text = "Minerals Income Modifier: +40";
			this.treeAbilities.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode12});
			this.treeAbilities.Size = new System.Drawing.Size(306, 420);
			this.treeAbilities.TabIndex = 0;
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
			((System.ComponentModel.ISupportInitialize)(this.picOwnerFlag)).EndInit();
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
		private System.Windows.Forms.PictureBox picPortrait;
		private System.Windows.Forms.PictureBox picOwnerFlag;
		private System.Windows.Forms.TabPage pageAbility;
		private System.Windows.Forms.TreeView treeAbilities;
		private System.Windows.Forms.Label txtValueRadioactives;
		private System.Windows.Forms.Label txtValueOrganics;
		private System.Windows.Forms.Label txtValueMinerals;
		private System.Windows.Forms.Label txtDescription;
	}
}
