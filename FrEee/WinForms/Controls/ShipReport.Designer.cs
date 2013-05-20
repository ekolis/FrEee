namespace FrEee.WinForms.Controls
{
	partial class ShipReport
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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("3x \"Buster\" class Weapon Platform");
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("10x \"Guard\" class Troop");
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("100x Eee Population");
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("6x Ion Engine");
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("2x Phased Shield Generator");
			System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("3x Anti-Proton Beam");
			System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("6x Ion Engine");
			System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("2x Phased Shield Generator");
			System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("3x Anti-Proton Beam");
			System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("3x \"Buster\" class Weapon Platform");
			System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("10x \"Guard\" class Troop");
			System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("100x Eee Population");
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Racial Trait: +5");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("War Shrine: +10");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Attack Modifier: +15", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Hull Size: -10");
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Defense Modifier: -10", new System.Windows.Forms.TreeNode[] {
            treeNode4});
			this.pageDetail = new System.Windows.Forms.TabPage();
			this.txtComponentsFunctional = new System.Windows.Forms.Label();
			this.gameResourceDisplay3 = new ResourceDisplay();
			this.lblComponents = new System.Windows.Forms.Label();
			this.gameResourceDisplay2 = new ResourceDisplay();
			this.gameResourceDisplay1 = new ResourceDisplay();
			this.lblMaint = new System.Windows.Forms.Label();
			this.txtFleet = new System.Windows.Forms.Label();
			this.lblFleet = new System.Windows.Forms.Label();
			this.txtExperience = new System.Windows.Forms.Label();
			this.lblExperience = new System.Windows.Forms.Label();
			this.txtOrder = new System.Windows.Forms.Label();
			this.lblOrder = new System.Windows.Forms.Label();
			this.txtCargoSpaceFree = new System.Windows.Forms.Label();
			this.gamePanel2 = new GamePanel();
			this.lstCargoSummary = new System.Windows.Forms.ListView();
			this.lblCargo = new System.Windows.Forms.Label();
			this.gamePanel1 = new GamePanel();
			this.lstComponentsSummary = new System.Windows.Forms.ListView();
			this.pnlStats = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.lblSupplies = new System.Windows.Forms.Label();
			this.progSupplies = new GameProgressBar();
			this.lblAmmunition = new System.Windows.Forms.Label();
			this.progAmmunition = new GameProgressBar();
			this.label3 = new System.Windows.Forms.Label();
			this.progFuel = new GameProgressBar();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.progHull = new GameProgressBar();
			this.progArmor = new GameProgressBar();
			this.lblShields = new System.Windows.Forms.Label();
			this.progShields = new GameProgressBar();
			this.lblArmor = new System.Windows.Forms.Label();
			this.lblHull = new System.Windows.Forms.Label();
			this.txtMovement = new System.Windows.Forms.Label();
			this.progMovement = new GameProgressBar();
			this.lblMovement = new System.Windows.Forms.Label();
			this.txtSize = new System.Windows.Forms.Label();
			this.lblSize = new System.Windows.Forms.Label();
			this.txtClass = new System.Windows.Forms.Label();
			this.lblClass = new System.Windows.Forms.Label();
			this.txtRole = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.Label();
			this.picPortrait = new System.Windows.Forms.PictureBox();
			this.picOwnerFlag = new System.Windows.Forms.PictureBox();
			this.pageOrders = new System.Windows.Forms.TabPage();
			this.btnOrdersClear = new GameButton();
			this.btnOrderDelete = new GameButton();
			this.btnOrderDown = new GameButton();
			this.btnOrderUp = new GameButton();
			this.lstOrdersDetail = new System.Windows.Forms.ListBox();
			this.pageComps = new System.Windows.Forms.TabPage();
			this.txtComponentsFunctionalDetail = new System.Windows.Forms.Label();
			this.lstComponentsDetail = new System.Windows.Forms.ListView();
			this.pageCargo = new System.Windows.Forms.TabPage();
			this.txtCargoSpaceFreeDetail = new System.Windows.Forms.Label();
			this.lstCargoDetail = new System.Windows.Forms.ListView();
			this.pageAbility = new System.Windows.Forms.TabPage();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.gameTabControl1 = new GameTabControl();
			this.pageDetail.SuspendLayout();
			this.gamePanel2.SuspendLayout();
			this.gamePanel1.SuspendLayout();
			this.pnlStats.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picPortrait)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picOwnerFlag)).BeginInit();
			this.pageOrders.SuspendLayout();
			this.pageComps.SuspendLayout();
			this.pageCargo.SuspendLayout();
			this.pageAbility.SuspendLayout();
			this.SuspendLayout();
			// 
			// pageDetail
			// 
			this.pageDetail.AutoScroll = true;
			this.pageDetail.BackColor = System.Drawing.Color.Black;
			this.pageDetail.Controls.Add(this.txtComponentsFunctional);
			this.pageDetail.Controls.Add(this.gameResourceDisplay3);
			this.pageDetail.Controls.Add(this.lblComponents);
			this.pageDetail.Controls.Add(this.gameResourceDisplay2);
			this.pageDetail.Controls.Add(this.gameResourceDisplay1);
			this.pageDetail.Controls.Add(this.lblMaint);
			this.pageDetail.Controls.Add(this.txtFleet);
			this.pageDetail.Controls.Add(this.lblFleet);
			this.pageDetail.Controls.Add(this.txtExperience);
			this.pageDetail.Controls.Add(this.lblExperience);
			this.pageDetail.Controls.Add(this.txtOrder);
			this.pageDetail.Controls.Add(this.lblOrder);
			this.pageDetail.Controls.Add(this.txtCargoSpaceFree);
			this.pageDetail.Controls.Add(this.gamePanel2);
			this.pageDetail.Controls.Add(this.lblCargo);
			this.pageDetail.Controls.Add(this.gamePanel1);
			this.pageDetail.Controls.Add(this.pnlStats);
			this.pageDetail.Controls.Add(this.txtMovement);
			this.pageDetail.Controls.Add(this.progMovement);
			this.pageDetail.Controls.Add(this.lblMovement);
			this.pageDetail.Controls.Add(this.txtSize);
			this.pageDetail.Controls.Add(this.lblSize);
			this.pageDetail.Controls.Add(this.txtClass);
			this.pageDetail.Controls.Add(this.lblClass);
			this.pageDetail.Controls.Add(this.txtRole);
			this.pageDetail.Controls.Add(this.txtName);
			this.pageDetail.Controls.Add(this.picPortrait);
			this.pageDetail.Controls.Add(this.picOwnerFlag);
			this.pageDetail.Location = new System.Drawing.Point(4, 29);
			this.pageDetail.Name = "pageDetail";
			this.pageDetail.Padding = new System.Windows.Forms.Padding(3);
			this.pageDetail.Size = new System.Drawing.Size(292, 614);
			this.pageDetail.TabIndex = 0;
			this.pageDetail.Text = "Detail";
			this.gameTabControl1.TabPages.Add(this.pageDetail);
			// 
			// txtComponentsFunctional
			// 
			this.txtComponentsFunctional.AutoSize = true;
			this.txtComponentsFunctional.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtComponentsFunctional.Location = new System.Drawing.Point(189, 366);
			this.txtComponentsFunctional.Name = "txtComponentsFunctional";
			this.txtComponentsFunctional.Size = new System.Drawing.Size(94, 15);
			this.txtComponentsFunctional.TabIndex = 35;
			this.txtComponentsFunctional.Text = "11/11 functional";
			// 
			// gameResourceDisplay3
			// 
			this.gameResourceDisplay3.Amount = 500;
			this.gameResourceDisplay3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.gameResourceDisplay3.BackColor = System.Drawing.Color.Black;
			this.gameResourceDisplay3.Change = null;
			this.gameResourceDisplay3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gameResourceDisplay3.ForeColor = System.Drawing.Color.Blue;
			this.gameResourceDisplay3.Location = new System.Drawing.Point(64, 346);
			this.gameResourceDisplay3.Margin = new System.Windows.Forms.Padding(0);
			this.gameResourceDisplay3.Name = "gameResourceDisplay3";
			this.gameResourceDisplay3.ResourceColor = System.Drawing.Color.Empty;
			this.gameResourceDisplay3.Size = new System.Drawing.Size(68, 20);
			this.gameResourceDisplay3.TabIndex = 46;
			// 
			// lblComponents
			// 
			this.lblComponents.AutoSize = true;
			this.lblComponents.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblComponents.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblComponents.Location = new System.Drawing.Point(11, 366);
			this.lblComponents.Name = "lblComponents";
			this.lblComponents.Size = new System.Drawing.Size(77, 15);
			this.lblComponents.TabIndex = 21;
			this.lblComponents.Text = "Components";
			// 
			// gameResourceDisplay2
			// 
			this.gameResourceDisplay2.Amount = 100;
			this.gameResourceDisplay2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.gameResourceDisplay2.BackColor = System.Drawing.Color.Black;
			this.gameResourceDisplay2.Change = null;
			this.gameResourceDisplay2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gameResourceDisplay2.ForeColor = System.Drawing.Color.Lime;
			this.gameResourceDisplay2.Location = new System.Drawing.Point(139, 346);
			this.gameResourceDisplay2.Margin = new System.Windows.Forms.Padding(0);
			this.gameResourceDisplay2.Name = "gameResourceDisplay2";
			this.gameResourceDisplay2.ResourceColor = System.Drawing.Color.Empty;
			this.gameResourceDisplay2.Size = new System.Drawing.Size(68, 20);
			this.gameResourceDisplay2.TabIndex = 45;
			// 
			// gameResourceDisplay1
			// 
			this.gameResourceDisplay1.Amount = 200;
			this.gameResourceDisplay1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.gameResourceDisplay1.BackColor = System.Drawing.Color.Black;
			this.gameResourceDisplay1.Change = null;
			this.gameResourceDisplay1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gameResourceDisplay1.ForeColor = System.Drawing.Color.Red;
			this.gameResourceDisplay1.Location = new System.Drawing.Point(214, 346);
			this.gameResourceDisplay1.Margin = new System.Windows.Forms.Padding(0);
			this.gameResourceDisplay1.Name = "gameResourceDisplay1";
			this.gameResourceDisplay1.ResourceColor = System.Drawing.Color.Empty;
			this.gameResourceDisplay1.Size = new System.Drawing.Size(68, 20);
			this.gameResourceDisplay1.TabIndex = 44;
			// 
			// lblMaint
			// 
			this.lblMaint.AutoSize = true;
			this.lblMaint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMaint.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblMaint.Location = new System.Drawing.Point(10, 346);
			this.lblMaint.Name = "lblMaint";
			this.lblMaint.Size = new System.Drawing.Size(41, 15);
			this.lblMaint.TabIndex = 43;
			this.lblMaint.Text = "Maint.";
			// 
			// txtFleet
			// 
			this.txtFleet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtFleet.AutoSize = true;
			this.txtFleet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtFleet.Location = new System.Drawing.Point(210, 331);
			this.txtFleet.Name = "txtFleet";
			this.txtFleet.Size = new System.Drawing.Size(68, 15);
			this.txtFleet.TabIndex = 42;
			this.txtFleet.Text = "Alpha Fleet";
			this.txtFleet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblFleet
			// 
			this.lblFleet.AutoSize = true;
			this.lblFleet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFleet.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblFleet.Location = new System.Drawing.Point(10, 331);
			this.lblFleet.Name = "lblFleet";
			this.lblFleet.Size = new System.Drawing.Size(34, 15);
			this.lblFleet.TabIndex = 41;
			this.lblFleet.Text = "Fleet";
			// 
			// txtExperience
			// 
			this.txtExperience.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtExperience.AutoSize = true;
			this.txtExperience.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtExperience.Location = new System.Drawing.Point(209, 316);
			this.txtExperience.Name = "txtExperience";
			this.txtExperience.Size = new System.Drawing.Size(73, 15);
			this.txtExperience.TabIndex = 40;
			this.txtExperience.Text = "Novice (0%)";
			this.txtExperience.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblExperience
			// 
			this.lblExperience.AutoSize = true;
			this.lblExperience.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblExperience.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblExperience.Location = new System.Drawing.Point(10, 316);
			this.lblExperience.Name = "lblExperience";
			this.lblExperience.Size = new System.Drawing.Size(100, 15);
			this.lblExperience.TabIndex = 39;
			this.lblExperience.Text = "Crew Experience";
			// 
			// txtOrder
			// 
			this.txtOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtOrder.AutoSize = true;
			this.txtOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtOrder.Location = new System.Drawing.Point(152, 301);
			this.txtOrder.Name = "txtOrder";
			this.txtOrder.Size = new System.Drawing.Size(130, 15);
			this.txtOrder.TabIndex = 38;
			this.txtOrder.Text = "Move to Kartogia (0, 5)";
			this.txtOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblOrder
			// 
			this.lblOrder.AutoSize = true;
			this.lblOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblOrder.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblOrder.Location = new System.Drawing.Point(10, 301);
			this.lblOrder.Name = "lblOrder";
			this.lblOrder.Size = new System.Drawing.Size(81, 15);
			this.lblOrder.TabIndex = 37;
			this.lblOrder.Text = "Current Order";
			// 
			// txtCargoSpaceFree
			// 
			this.txtCargoSpaceFree.AutoSize = true;
			this.txtCargoSpaceFree.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtCargoSpaceFree.Location = new System.Drawing.Point(190, 487);
			this.txtCargoSpaceFree.Name = "txtCargoSpaceFree";
			this.txtCargoSpaceFree.Size = new System.Drawing.Size(92, 15);
			this.txtCargoSpaceFree.TabIndex = 36;
			this.txtCargoSpaceFree.Text = "100/300 kT free";
			// 
			// gamePanel2
			// 
			this.gamePanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gamePanel2.BackColor = System.Drawing.Color.Black;
			this.gamePanel2.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.gamePanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.gamePanel2.Controls.Add(this.lstCargoSummary);
			this.gamePanel2.ForeColor = System.Drawing.Color.White;
			this.gamePanel2.Location = new System.Drawing.Point(11, 505);
			this.gamePanel2.Name = "gamePanel2";
			this.gamePanel2.Size = new System.Drawing.Size(272, 100);
			this.gamePanel2.TabIndex = 24;
			// 
			// lstCargoSummary
			// 
			this.lstCargoSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstCargoSummary.BackColor = System.Drawing.Color.Black;
			this.lstCargoSummary.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstCargoSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstCargoSummary.ForeColor = System.Drawing.Color.White;
			this.lstCargoSummary.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
			this.lstCargoSummary.Location = new System.Drawing.Point(2, 3);
			this.lstCargoSummary.Name = "lstCargoSummary";
			this.lstCargoSummary.Size = new System.Drawing.Size(265, 92);
			this.lstCargoSummary.TabIndex = 23;
			this.lstCargoSummary.UseCompatibleStateImageBehavior = false;
			// 
			// lblCargo
			// 
			this.lblCargo.AutoSize = true;
			this.lblCargo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCargo.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblCargo.Location = new System.Drawing.Point(13, 487);
			this.lblCargo.Name = "lblCargo";
			this.lblCargo.Size = new System.Drawing.Size(40, 15);
			this.lblCargo.TabIndex = 24;
			this.lblCargo.Text = "Cargo";
			// 
			// gamePanel1
			// 
			this.gamePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gamePanel1.BackColor = System.Drawing.Color.Black;
			this.gamePanel1.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.gamePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.gamePanel1.Controls.Add(this.lstComponentsSummary);
			this.gamePanel1.ForeColor = System.Drawing.Color.White;
			this.gamePanel1.Location = new System.Drawing.Point(13, 384);
			this.gamePanel1.Name = "gamePanel1";
			this.gamePanel1.Size = new System.Drawing.Size(272, 100);
			this.gamePanel1.TabIndex = 23;
			// 
			// lstComponentsSummary
			// 
			this.lstComponentsSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstComponentsSummary.BackColor = System.Drawing.Color.Black;
			this.lstComponentsSummary.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstComponentsSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstComponentsSummary.ForeColor = System.Drawing.Color.White;
			this.lstComponentsSummary.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem4,
            listViewItem5,
            listViewItem6});
			this.lstComponentsSummary.Location = new System.Drawing.Point(4, 3);
			this.lstComponentsSummary.Name = "lstComponentsSummary";
			this.lstComponentsSummary.Size = new System.Drawing.Size(265, 92);
			this.lstComponentsSummary.TabIndex = 23;
			this.lstComponentsSummary.UseCompatibleStateImageBehavior = false;
			// 
			// pnlStats
			// 
			this.pnlStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlStats.ColumnCount = 2;
			this.pnlStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.pnlStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.pnlStats.Controls.Add(this.tableLayoutPanel1, 0, 0);
			this.pnlStats.Controls.Add(this.tableLayoutPanel2, 1, 0);
			this.pnlStats.Location = new System.Drawing.Point(8, 167);
			this.pnlStats.Name = "pnlStats";
			this.pnlStats.RowCount = 1;
			this.pnlStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.pnlStats.Size = new System.Drawing.Size(278, 123);
			this.pnlStats.TabIndex = 20;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.lblSupplies, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.progSupplies, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.lblAmmunition, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.progAmmunition, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.progFuel, 0, 5);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 6;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(133, 117);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// lblSupplies
			// 
			this.lblSupplies.AutoSize = true;
			this.lblSupplies.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSupplies.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblSupplies.Location = new System.Drawing.Point(3, 0);
			this.lblSupplies.Name = "lblSupplies";
			this.lblSupplies.Size = new System.Drawing.Size(55, 15);
			this.lblSupplies.TabIndex = 19;
			this.lblSupplies.Text = "Supplies";
			// 
			// progSupplies
			// 
			this.progSupplies.BackColor = System.Drawing.Color.Black;
			this.progSupplies.BarColor = System.Drawing.Color.Blue;
			this.progSupplies.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.progSupplies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.progSupplies.Dock = System.Windows.Forms.DockStyle.Fill;
			this.progSupplies.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.progSupplies.ForeColor = System.Drawing.Color.White;
			this.progSupplies.LeftText = "";
			this.progSupplies.Location = new System.Drawing.Point(0, 20);
			this.progSupplies.Margin = new System.Windows.Forms.Padding(0);
			this.progSupplies.Maximum = 3000;
			this.progSupplies.Name = "progSupplies";
			this.progSupplies.RightText = "";
			this.progSupplies.Size = new System.Drawing.Size(133, 19);
			this.progSupplies.TabIndex = 18;
			this.progSupplies.Value = 2500;
			// 
			// lblAmmunition
			// 
			this.lblAmmunition.AutoSize = true;
			this.lblAmmunition.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblAmmunition.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblAmmunition.Location = new System.Drawing.Point(3, 39);
			this.lblAmmunition.Name = "lblAmmunition";
			this.lblAmmunition.Size = new System.Drawing.Size(73, 15);
			this.lblAmmunition.TabIndex = 21;
			this.lblAmmunition.Text = "Ammunition";
			// 
			// progAmmunition
			// 
			this.progAmmunition.BackColor = System.Drawing.Color.Black;
			this.progAmmunition.BarColor = System.Drawing.Color.Blue;
			this.progAmmunition.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.progAmmunition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.progAmmunition.Dock = System.Windows.Forms.DockStyle.Fill;
			this.progAmmunition.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.progAmmunition.ForeColor = System.Drawing.Color.White;
			this.progAmmunition.LeftText = "";
			this.progAmmunition.Location = new System.Drawing.Point(0, 59);
			this.progAmmunition.Margin = new System.Windows.Forms.Padding(0);
			this.progAmmunition.Maximum = 500;
			this.progAmmunition.Name = "progAmmunition";
			this.progAmmunition.RightText = "";
			this.progAmmunition.Size = new System.Drawing.Size(133, 19);
			this.progAmmunition.TabIndex = 20;
			this.progAmmunition.Value = 100;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label3.Location = new System.Drawing.Point(3, 78);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(31, 15);
			this.label3.TabIndex = 23;
			this.label3.Text = "Fuel";
			// 
			// progFuel
			// 
			this.progFuel.BackColor = System.Drawing.Color.Black;
			this.progFuel.BarColor = System.Drawing.Color.Blue;
			this.progFuel.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.progFuel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.progFuel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.progFuel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.progFuel.ForeColor = System.Drawing.Color.White;
			this.progFuel.LeftText = "";
			this.progFuel.Location = new System.Drawing.Point(0, 98);
			this.progFuel.Margin = new System.Windows.Forms.Padding(0);
			this.progFuel.Maximum = 2000;
			this.progFuel.Name = "progFuel";
			this.progFuel.RightText = "";
			this.progFuel.Size = new System.Drawing.Size(133, 19);
			this.progFuel.TabIndex = 22;
			this.progFuel.Value = 1000;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.progHull, 0, 5);
			this.tableLayoutPanel2.Controls.Add(this.progArmor, 0, 3);
			this.tableLayoutPanel2.Controls.Add(this.lblShields, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.progShields, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.lblArmor, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.lblHull, 0, 3);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(142, 3);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 6;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(133, 117);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// progHull
			// 
			this.progHull.BackColor = System.Drawing.Color.Black;
			this.progHull.BarColor = System.Drawing.Color.Blue;
			this.progHull.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.progHull.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.progHull.Dock = System.Windows.Forms.DockStyle.Fill;
			this.progHull.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.progHull.ForeColor = System.Drawing.Color.White;
			this.progHull.LeftText = "";
			this.progHull.Location = new System.Drawing.Point(0, 98);
			this.progHull.Margin = new System.Windows.Forms.Padding(0);
			this.progHull.Maximum = 200;
			this.progHull.Name = "progHull";
			this.progHull.RightText = "";
			this.progHull.Size = new System.Drawing.Size(133, 19);
			this.progHull.TabIndex = 30;
			this.progHull.Value = 150;
			// 
			// progArmor
			// 
			this.progArmor.BackColor = System.Drawing.Color.Black;
			this.progArmor.BarColor = System.Drawing.Color.Blue;
			this.progArmor.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.progArmor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.progArmor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.progArmor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.progArmor.ForeColor = System.Drawing.Color.White;
			this.progArmor.LeftText = "";
			this.progArmor.Location = new System.Drawing.Point(0, 59);
			this.progArmor.Margin = new System.Windows.Forms.Padding(0);
			this.progArmor.Maximum = 300;
			this.progArmor.Name = "progArmor";
			this.progArmor.RightText = "";
			this.progArmor.Size = new System.Drawing.Size(133, 19);
			this.progArmor.TabIndex = 29;
			this.progArmor.Value = 100;
			// 
			// lblShields
			// 
			this.lblShields.AutoSize = true;
			this.lblShields.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblShields.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblShields.Location = new System.Drawing.Point(3, 0);
			this.lblShields.Name = "lblShields";
			this.lblShields.Size = new System.Drawing.Size(48, 15);
			this.lblShields.TabIndex = 25;
			this.lblShields.Text = "Shields";
			// 
			// progShields
			// 
			this.progShields.BackColor = System.Drawing.Color.Black;
			this.progShields.BarColor = System.Drawing.Color.Blue;
			this.progShields.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.progShields.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.progShields.Dock = System.Windows.Forms.DockStyle.Fill;
			this.progShields.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.progShields.ForeColor = System.Drawing.Color.White;
			this.progShields.LeftText = "";
			this.progShields.Location = new System.Drawing.Point(0, 20);
			this.progShields.Margin = new System.Windows.Forms.Padding(0);
			this.progShields.Maximum = 200;
			this.progShields.Name = "progShields";
			this.progShields.RightText = "";
			this.progShields.Size = new System.Drawing.Size(133, 19);
			this.progShields.TabIndex = 24;
			this.progShields.Value = 200;
			// 
			// lblArmor
			// 
			this.lblArmor.AutoSize = true;
			this.lblArmor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblArmor.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblArmor.Location = new System.Drawing.Point(3, 39);
			this.lblArmor.Name = "lblArmor";
			this.lblArmor.Size = new System.Drawing.Size(40, 15);
			this.lblArmor.TabIndex = 27;
			this.lblArmor.Text = "Armor";
			// 
			// lblHull
			// 
			this.lblHull.AutoSize = true;
			this.lblHull.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHull.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblHull.Location = new System.Drawing.Point(3, 78);
			this.lblHull.Name = "lblHull";
			this.lblHull.Size = new System.Drawing.Size(29, 15);
			this.lblHull.TabIndex = 28;
			this.lblHull.Text = "Hull";
			// 
			// txtMovement
			// 
			this.txtMovement.AutoSize = true;
			this.txtMovement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtMovement.Location = new System.Drawing.Point(159, 132);
			this.txtMovement.Name = "txtMovement";
			this.txtMovement.Size = new System.Drawing.Size(30, 15);
			this.txtMovement.TabIndex = 19;
			this.txtMovement.Text = "4 / 6";
			// 
			// progMovement
			// 
			this.progMovement.BackColor = System.Drawing.Color.Black;
			this.progMovement.BarColor = System.Drawing.Color.Blue;
			this.progMovement.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.progMovement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.progMovement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.progMovement.ForeColor = System.Drawing.Color.White;
			this.progMovement.LeftText = "";
			this.progMovement.Location = new System.Drawing.Point(212, 132);
			this.progMovement.Margin = new System.Windows.Forms.Padding(0);
			this.progMovement.Maximum = 6;
			this.progMovement.Name = "progMovement";
			this.progMovement.RightText = "";
			this.progMovement.Size = new System.Drawing.Size(47, 15);
			this.progMovement.TabIndex = 18;
			this.progMovement.Value = 4;
			// 
			// lblMovement
			// 
			this.lblMovement.AutoSize = true;
			this.lblMovement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMovement.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblMovement.Location = new System.Drawing.Point(144, 118);
			this.lblMovement.Name = "lblMovement";
			this.lblMovement.Size = new System.Drawing.Size(65, 15);
			this.lblMovement.TabIndex = 16;
			this.lblMovement.Text = "Movement";
			// 
			// txtSize
			// 
			this.txtSize.AutoSize = true;
			this.txtSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSize.Location = new System.Drawing.Point(159, 103);
			this.txtSize.Name = "txtSize";
			this.txtSize.Size = new System.Drawing.Size(127, 15);
			this.txtSize.TabIndex = 15;
			this.txtSize.Text = "Heavy Cruiser (600kT)";
			// 
			// lblSize
			// 
			this.lblSize.AutoSize = true;
			this.lblSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSize.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblSize.Location = new System.Drawing.Point(144, 88);
			this.lblSize.Name = "lblSize";
			this.lblSize.Size = new System.Drawing.Size(31, 15);
			this.lblSize.TabIndex = 14;
			this.lblSize.Text = "Size";
			// 
			// txtClass
			// 
			this.txtClass.AutoSize = true;
			this.txtClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtClass.Location = new System.Drawing.Point(159, 73);
			this.txtClass.Name = "txtClass";
			this.txtClass.Size = new System.Drawing.Size(71, 15);
			this.txtClass.TabIndex = 13;
			this.txtClass.Text = "Constitution";
			// 
			// lblClass
			// 
			this.lblClass.AutoSize = true;
			this.lblClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblClass.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblClass.Location = new System.Drawing.Point(144, 58);
			this.lblClass.Name = "lblClass";
			this.lblClass.Size = new System.Drawing.Size(37, 15);
			this.lblClass.TabIndex = 12;
			this.lblClass.Text = "Class";
			// 
			// txtRole
			// 
			this.txtRole.AutoSize = true;
			this.txtRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtRole.Location = new System.Drawing.Point(159, 26);
			this.txtRole.Name = "txtRole";
			this.txtRole.Size = new System.Drawing.Size(67, 15);
			this.txtRole.TabIndex = 11;
			this.txtRole.Text = "Attack Ship";
			// 
			// txtName
			// 
			this.txtName.AutoSize = true;
			this.txtName.Location = new System.Drawing.Point(143, 6);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(89, 13);
			this.txtName.TabIndex = 10;
			this.txtName.Text = "Constitution 0001";
			// 
			// picPortrait
			// 
			this.picPortrait.Location = new System.Drawing.Point(8, 32);
			this.picPortrait.Name = "picPortrait";
			this.picPortrait.Size = new System.Drawing.Size(128, 128);
			this.picPortrait.TabIndex = 9;
			this.picPortrait.TabStop = false;
			// 
			// picOwnerFlag
			// 
			this.picOwnerFlag.Location = new System.Drawing.Point(6, 6);
			this.picOwnerFlag.Name = "picOwnerFlag";
			this.picOwnerFlag.Size = new System.Drawing.Size(34, 20);
			this.picOwnerFlag.TabIndex = 8;
			this.picOwnerFlag.TabStop = false;
			// 
			// pageOrders
			// 
			this.pageOrders.BackColor = System.Drawing.Color.Black;
			this.pageOrders.Controls.Add(this.btnOrdersClear);
			this.pageOrders.Controls.Add(this.btnOrderDelete);
			this.pageOrders.Controls.Add(this.btnOrderDown);
			this.pageOrders.Controls.Add(this.btnOrderUp);
			this.pageOrders.Controls.Add(this.lstOrdersDetail);
			this.pageOrders.Location = new System.Drawing.Point(4, 29);
			this.pageOrders.Name = "pageOrders";
			this.pageOrders.Padding = new System.Windows.Forms.Padding(3);
			this.pageOrders.Size = new System.Drawing.Size(292, 614);
			this.pageOrders.TabIndex = 1;
			this.pageOrders.Text = "Orders";
			this.gameTabControl1.TabPages.Add(this.pageOrders);
			// 
			// btnOrdersClear
			// 
			this.btnOrdersClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOrdersClear.BackColor = System.Drawing.Color.Black;
			this.btnOrdersClear.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnOrdersClear.Location = new System.Drawing.Point(224, 631);
			this.btnOrdersClear.Name = "btnOrdersClear";
			this.btnOrdersClear.Size = new System.Drawing.Size(57, 33);
			this.btnOrdersClear.TabIndex = 4;
			this.btnOrdersClear.Text = "Clear";
			this.btnOrdersClear.UseVisualStyleBackColor = false;
			// 
			// btnOrderDelete
			// 
			this.btnOrderDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOrderDelete.BackColor = System.Drawing.Color.Black;
			this.btnOrderDelete.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnOrderDelete.Location = new System.Drawing.Point(152, 631);
			this.btnOrderDelete.Name = "btnOrderDelete";
			this.btnOrderDelete.Size = new System.Drawing.Size(57, 33);
			this.btnOrderDelete.TabIndex = 3;
			this.btnOrderDelete.Text = "Del";
			this.btnOrderDelete.UseVisualStyleBackColor = false;
			// 
			// btnOrderDown
			// 
			this.btnOrderDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOrderDown.BackColor = System.Drawing.Color.Black;
			this.btnOrderDown.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnOrderDown.Location = new System.Drawing.Point(80, 631);
			this.btnOrderDown.Name = "btnOrderDown";
			this.btnOrderDown.Size = new System.Drawing.Size(57, 33);
			this.btnOrderDown.TabIndex = 2;
			this.btnOrderDown.Text = "Dn";
			this.btnOrderDown.UseVisualStyleBackColor = false;
			// 
			// btnOrderUp
			// 
			this.btnOrderUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOrderUp.BackColor = System.Drawing.Color.Black;
			this.btnOrderUp.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnOrderUp.Location = new System.Drawing.Point(8, 631);
			this.btnOrderUp.Name = "btnOrderUp";
			this.btnOrderUp.Size = new System.Drawing.Size(57, 33);
			this.btnOrderUp.TabIndex = 1;
			this.btnOrderUp.Text = "Up";
			this.btnOrderUp.UseVisualStyleBackColor = false;
			// 
			// lstOrdersDetail
			// 
			this.lstOrdersDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstOrdersDetail.BackColor = System.Drawing.Color.Black;
			this.lstOrdersDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstOrdersDetail.ForeColor = System.Drawing.Color.White;
			this.lstOrdersDetail.FormattingEnabled = true;
			this.lstOrdersDetail.Items.AddRange(new object[] {
            "Move to Kartogia (0, -5)",
            "Drop All Eee Population",
            "Load All Troops",
            "Move to Fizbon (3, 6)"});
			this.lstOrdersDetail.Location = new System.Drawing.Point(7, 4);
			this.lstOrdersDetail.Name = "lstOrdersDetail";
			this.lstOrdersDetail.Size = new System.Drawing.Size(279, 611);
			this.lstOrdersDetail.TabIndex = 0;
			// 
			// pageComps
			// 
			this.pageComps.BackColor = System.Drawing.Color.Black;
			this.pageComps.Controls.Add(this.txtComponentsFunctionalDetail);
			this.pageComps.Controls.Add(this.lstComponentsDetail);
			this.pageComps.Location = new System.Drawing.Point(4, 29);
			this.pageComps.Name = "pageComps";
			this.pageComps.Padding = new System.Windows.Forms.Padding(3);
			this.pageComps.Size = new System.Drawing.Size(292, 614);
			this.pageComps.TabIndex = 2;
			this.pageComps.Text = "Comps";
			this.gameTabControl1.TabPages.Add(this.pageComps);
			// 
			// txtComponentsFunctionalDetail
			// 
			this.txtComponentsFunctionalDetail.AutoSize = true;
			this.txtComponentsFunctionalDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtComponentsFunctionalDetail.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.txtComponentsFunctionalDetail.Location = new System.Drawing.Point(3, 3);
			this.txtComponentsFunctionalDetail.Name = "txtComponentsFunctionalDetail";
			this.txtComponentsFunctionalDetail.Size = new System.Drawing.Size(94, 15);
			this.txtComponentsFunctionalDetail.TabIndex = 36;
			this.txtComponentsFunctionalDetail.Text = "11/11 functional";
			// 
			// lstComponentsDetail
			// 
			this.lstComponentsDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstComponentsDetail.BackColor = System.Drawing.Color.Black;
			this.lstComponentsDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstComponentsDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstComponentsDetail.ForeColor = System.Drawing.Color.White;
			this.lstComponentsDetail.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem7,
            listViewItem8,
            listViewItem9});
			this.lstComponentsDetail.Location = new System.Drawing.Point(0, 22);
			this.lstComponentsDetail.Name = "lstComponentsDetail";
			this.lstComponentsDetail.Size = new System.Drawing.Size(292, 646);
			this.lstComponentsDetail.TabIndex = 24;
			this.lstComponentsDetail.UseCompatibleStateImageBehavior = false;
			this.lstComponentsDetail.View = System.Windows.Forms.View.Tile;
			// 
			// pageCargo
			// 
			this.pageCargo.BackColor = System.Drawing.Color.Black;
			this.pageCargo.Controls.Add(this.txtCargoSpaceFreeDetail);
			this.pageCargo.Controls.Add(this.lstCargoDetail);
			this.pageCargo.Location = new System.Drawing.Point(4, 29);
			this.pageCargo.Name = "pageCargo";
			this.pageCargo.Padding = new System.Windows.Forms.Padding(3);
			this.pageCargo.Size = new System.Drawing.Size(292, 614);
			this.pageCargo.TabIndex = 3;
			this.pageCargo.Text = "Cargo";
			this.gameTabControl1.TabPages.Add(this.pageCargo);
			// 
			// txtCargoSpaceFreeDetail
			// 
			this.txtCargoSpaceFreeDetail.AutoSize = true;
			this.txtCargoSpaceFreeDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtCargoSpaceFreeDetail.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.txtCargoSpaceFreeDetail.Location = new System.Drawing.Point(3, 3);
			this.txtCargoSpaceFreeDetail.Name = "txtCargoSpaceFreeDetail";
			this.txtCargoSpaceFreeDetail.Size = new System.Drawing.Size(92, 15);
			this.txtCargoSpaceFreeDetail.TabIndex = 37;
			this.txtCargoSpaceFreeDetail.Text = "100/300 kT free";
			// 
			// lstCargoDetail
			// 
			this.lstCargoDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstCargoDetail.BackColor = System.Drawing.Color.Black;
			this.lstCargoDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstCargoDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstCargoDetail.ForeColor = System.Drawing.Color.White;
			this.lstCargoDetail.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem10,
            listViewItem11,
            listViewItem12});
			this.lstCargoDetail.Location = new System.Drawing.Point(-1, 21);
			this.lstCargoDetail.Name = "lstCargoDetail";
			this.lstCargoDetail.Size = new System.Drawing.Size(293, 640);
			this.lstCargoDetail.TabIndex = 24;
			this.lstCargoDetail.UseCompatibleStateImageBehavior = false;
			this.lstCargoDetail.View = System.Windows.Forms.View.Tile;
			// 
			// pageAbility
			// 
			this.pageAbility.BackColor = System.Drawing.Color.Black;
			this.pageAbility.Controls.Add(this.treeView1);
			this.pageAbility.Location = new System.Drawing.Point(4, 29);
			this.pageAbility.Name = "pageAbility";
			this.pageAbility.Padding = new System.Windows.Forms.Padding(3);
			this.pageAbility.Size = new System.Drawing.Size(292, 614);
			this.pageAbility.TabIndex = 4;
			this.pageAbility.Text = "Ability";
			this.gameTabControl1.TabPages.Add(this.pageAbility);
			// 
			// treeView1
			// 
			this.treeView1.BackColor = System.Drawing.Color.Black;
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.ForeColor = System.Drawing.Color.White;
			this.treeView1.LineColor = System.Drawing.Color.Empty;
			this.treeView1.Location = new System.Drawing.Point(3, 3);
			this.treeView1.Name = "treeView1";
			treeNode1.Name = "Node1";
			treeNode1.Text = "Racial Trait: +5";
			treeNode2.Name = "Node3";
			treeNode2.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic);
			treeNode2.Text = "War Shrine: +10";
			treeNode3.Name = "Node0";
			treeNode3.Text = "Attack Modifier: +15";
			treeNode4.Name = "Node5";
			treeNode4.Text = "Hull Size: -10";
			treeNode5.Name = "Node4";
			treeNode5.Text = "Defense Modifier: -10";
			this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode5});
			this.treeView1.Size = new System.Drawing.Size(286, 608);
			this.treeView1.TabIndex = 0;
			// 
			// gameTabControl1
			// 
			this.gameTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gameTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.gameTabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gameTabControl1.Location = new System.Drawing.Point(0, 0);
			this.gameTabControl1.Name = "gameTabControl1";
			this.gameTabControl1.SelectedIndex = 0;
			this.gameTabControl1.SelectedTabBackColor = System.Drawing.Color.SkyBlue;
			this.gameTabControl1.SelectedTabForeColor = System.Drawing.Color.Black;
			this.gameTabControl1.Size = new System.Drawing.Size(300, 647);
			this.gameTabControl1.TabBackColor = System.Drawing.Color.Black;
			this.gameTabControl1.TabForeColor = System.Drawing.Color.CornflowerBlue;
			this.gameTabControl1.TabIndex = 1;
			// 
			// ShipReport
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.Controls.Add(this.gameTabControl1);
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "ShipReport";
			this.Size = new System.Drawing.Size(300, 647);
			this.pageDetail.ResumeLayout(false);
			this.pageDetail.PerformLayout();
			this.gamePanel2.ResumeLayout(false);
			this.gamePanel1.ResumeLayout(false);
			this.pnlStats.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picPortrait)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picOwnerFlag)).EndInit();
			this.pageOrders.ResumeLayout(false);
			this.pageComps.ResumeLayout(false);
			this.pageComps.PerformLayout();
			this.pageCargo.ResumeLayout(false);
			this.pageCargo.PerformLayout();
			this.pageAbility.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabPage pageDetail;
		private System.Windows.Forms.TabPage pageOrders;
		private System.Windows.Forms.TabPage pageComps;
		private System.Windows.Forms.TabPage pageCargo;
		private System.Windows.Forms.Label txtSize;
		private System.Windows.Forms.Label lblSize;
		private System.Windows.Forms.Label txtClass;
		private System.Windows.Forms.Label lblClass;
		private System.Windows.Forms.Label txtRole;
		private System.Windows.Forms.Label txtName;
		private System.Windows.Forms.PictureBox picPortrait;
		private System.Windows.Forms.PictureBox picOwnerFlag;
		private System.Windows.Forms.TabPage pageAbility;
		private System.Windows.Forms.Label lblMovement;
		private GameProgressBar progMovement;
		private System.Windows.Forms.Label txtMovement;
		private System.Windows.Forms.TableLayoutPanel pnlStats;
		private System.Windows.Forms.Label lblComponents;
		private GamePanel gamePanel1;
		private System.Windows.Forms.ListView lstComponentsSummary;
		private GamePanel gamePanel2;
		private System.Windows.Forms.ListView lstCargoSummary;
		private System.Windows.Forms.Label lblCargo;
		private GameButton btnOrdersClear;
		private GameButton btnOrderDelete;
		private GameButton btnOrderDown;
		private GameButton btnOrderUp;
		private System.Windows.Forms.ListBox lstOrdersDetail;
		private System.Windows.Forms.ListView lstComponentsDetail;
		private System.Windows.Forms.ListView lstCargoDetail;
		private System.Windows.Forms.Label txtCargoSpaceFree;
		private System.Windows.Forms.Label txtComponentsFunctional;
		private System.Windows.Forms.Label txtComponentsFunctionalDetail;
		private System.Windows.Forms.Label txtCargoSpaceFreeDetail;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label lblSupplies;
		private GameProgressBar progSupplies;
		private System.Windows.Forms.Label lblAmmunition;
		private GameProgressBar progAmmunition;
		private System.Windows.Forms.Label label3;
		private GameProgressBar progFuel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private GameProgressBar progHull;
		private GameProgressBar progArmor;
		private System.Windows.Forms.Label lblShields;
		private GameProgressBar progShields;
		private System.Windows.Forms.Label lblArmor;
		private System.Windows.Forms.Label lblHull;
		private ResourceDisplay gameResourceDisplay3;
		private ResourceDisplay gameResourceDisplay2;
		private ResourceDisplay gameResourceDisplay1;
		private System.Windows.Forms.Label lblMaint;
		private System.Windows.Forms.Label txtFleet;
		private System.Windows.Forms.Label lblFleet;
		private System.Windows.Forms.Label txtExperience;
		private System.Windows.Forms.Label lblExperience;
		private System.Windows.Forms.Label txtOrder;
		private System.Windows.Forms.Label lblOrder;
		private GameTabControl gameTabControl1;
	}
}
