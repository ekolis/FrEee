namespace FrEee.WinForms.Controls
{
	partial class StormReport
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Racial Trait: +5");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("War Shrine: +10");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Attack Modifier: +15", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Happiness: +10");
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Population: +30");
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Minerals Income Modifier: +40", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5});
			this.gameTabControl1 = new FrEee.WinForms.Controls.GameTabControl();
			this.pageDetail = new System.Windows.Forms.TabPage();
			this.picPortrait = new FrEee.WinForms.Controls.GamePictureBox();
			this.txtDescription = new System.Windows.Forms.Label();
			this.txtSize = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.Label();
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
			this.pageDetail.Controls.Add(this.picPortrait);
			this.pageDetail.Controls.Add(this.txtDescription);
			this.pageDetail.Controls.Add(this.txtSize);
			this.pageDetail.Controls.Add(this.txtName);
			this.pageDetail.Controls.Add(this.picOwnerFlag);
			this.pageDetail.Location = new System.Drawing.Point(4, 29);
			this.pageDetail.Name = "pageDetail";
			this.pageDetail.Padding = new System.Windows.Forms.Padding(3);
			this.pageDetail.Size = new System.Drawing.Size(312, 426);
			this.pageDetail.TabIndex = 0;
			this.pageDetail.Text = "Detail";
			// 
			// picPortrait
			// 
			this.picPortrait.Location = new System.Drawing.Point(6, 32);
			this.picPortrait.Name = "picPortrait";
			this.picPortrait.Size = new System.Drawing.Size(128, 128);
			this.picPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picPortrait.TabIndex = 52;
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
			this.txtDescription.Size = new System.Drawing.Size(296, 36);
			this.txtDescription.TabIndex = 50;
			this.txtDescription.Text = "A storm composed of electrostatic gases.";
			// 
			// txtSize
			// 
			this.txtSize.AutoSize = true;
			this.txtSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSize.Location = new System.Drawing.Point(159, 26);
			this.txtSize.Name = "txtSize";
			this.txtSize.Size = new System.Drawing.Size(89, 15);
			this.txtSize.TabIndex = 11;
			this.txtSize.Text = "Medium Storm";
			// 
			// txtName
			// 
			this.txtName.AutoSize = true;
			this.txtName.Location = new System.Drawing.Point(143, 6);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(52, 20);
			this.txtName.TabIndex = 10;
			this.txtName.Text = "Storm";
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
			treeNode1.Name = "Node1";
			treeNode1.Text = "Racial Trait: +5";
			treeNode2.Name = "Node3";
			treeNode2.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic);
			treeNode2.Text = "War Shrine: +10";
			treeNode3.Name = "Node0";
			treeNode3.Text = "Attack Modifier: +15";
			treeNode4.Name = "Node1";
			treeNode4.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			treeNode4.Text = "Happiness: +10";
			treeNode5.Name = "Node2";
			treeNode5.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			treeNode5.Text = "Population: +30";
			treeNode6.Name = "Node0";
			treeNode6.Text = "Minerals Income Modifier: +40";
			this.treeAbilities.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode6});
			this.treeAbilities.Size = new System.Drawing.Size(306, 420);
			this.treeAbilities.TabIndex = 0;
			// 
			// StormReport
			// 
			this.AutoScroll = true;
			this.BackColor = System.Drawing.Color.Black;
			this.Controls.Add(this.gameTabControl1);
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "StormReport";
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
		private System.Windows.Forms.Label txtSize;
		private System.Windows.Forms.Label txtName;
		private System.Windows.Forms.PictureBox picOwnerFlag;
		private System.Windows.Forms.TabPage pageAbility;
		private System.Windows.Forms.TreeView treeAbilities;
		private System.Windows.Forms.Label txtDescription;
		private GamePictureBox picPortrait;
	}
}
