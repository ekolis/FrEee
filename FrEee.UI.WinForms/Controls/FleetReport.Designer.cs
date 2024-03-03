namespace FrEee.UI.WinForms.Controls;

partial class FleetReport
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
		System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("6x Constitution Heavy Cruiser");
		System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("4x Phalanx Destroyer");
		System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("2x Trooper Medium Transport");
		this.pageDetail = new System.Windows.Forms.TabPage();
		this.txtAge = new System.Windows.Forms.Label();
		this.txtCargoSpaceFree = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.picPortrait = new FrEee.UI.WinForms.Controls.GamePictureBox();
		this.resMaintMin = new FrEee.UI.WinForms.Controls.ResourceDisplay();
		this.lblComponents = new System.Windows.Forms.Label();
		this.resMaintOrg = new FrEee.UI.WinForms.Controls.ResourceDisplay();
		this.resMaintRad = new FrEee.UI.WinForms.Controls.ResourceDisplay();
		this.lblIncome = new System.Windows.Forms.Label();
		this.txtExperience = new System.Windows.Forms.Label();
		this.lblExperience = new System.Windows.Forms.Label();
		this.txtOrder = new System.Windows.Forms.Label();
		this.lblOrder = new System.Windows.Forms.Label();
		this.gamePanel1 = new FrEee.UI.WinForms.Controls.GamePanel();
		this.lstVehicleSummary = new System.Windows.Forms.ListView();
		this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
		this.pnlStats = new System.Windows.Forms.TableLayoutPanel();
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.lblSupplies = new System.Windows.Forms.Label();
		this.progSupplies = new FrEee.UI.WinForms.Controls.GameProgressBar();
		this.lblAmmunition = new System.Windows.Forms.Label();
		this.progAmmunition = new FrEee.UI.WinForms.Controls.GameProgressBar();
		this.label3 = new System.Windows.Forms.Label();
		this.progFuel = new FrEee.UI.WinForms.Controls.GameProgressBar();
		this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
		this.progHull = new FrEee.UI.WinForms.Controls.GameProgressBar();
		this.progArmor = new FrEee.UI.WinForms.Controls.GameProgressBar();
		this.lblShields = new System.Windows.Forms.Label();
		this.progShields = new FrEee.UI.WinForms.Controls.GameProgressBar();
		this.lblArmor = new System.Windows.Forms.Label();
		this.lblHull = new System.Windows.Forms.Label();
		this.progMovement = new FrEee.UI.WinForms.Controls.GameProgressBar();
		this.lblMovement = new System.Windows.Forms.Label();
		this.txtHullSize = new System.Windows.Forms.Label();
		this.lblHullSize = new System.Windows.Forms.Label();
		this.txtClass = new System.Windows.Forms.Label();
		this.lblClass = new System.Windows.Forms.Label();
		this.txtCount = new System.Windows.Forms.Label();
		this.txtName = new System.Windows.Forms.Label();
		this.picOwnerFlag = new System.Windows.Forms.PictureBox();
		this.pageVehicles = new System.Windows.Forms.TabPage();
		this.treeVehicles = new System.Windows.Forms.TreeView();
		this.pageAbility = new System.Windows.Forms.TabPage();
		this.abilityTreeView = new FrEee.UI.WinForms.Controls.AbilityTreeView();
		this.gameTabControl1 = new FrEee.UI.WinForms.Controls.GameTabControl();
		this.pageOrders = new System.Windows.Forms.TabPage();
		this.chkOnHold = new System.Windows.Forms.CheckBox();
		this.chkRepeat = new System.Windows.Forms.CheckBox();
		this.btnDeleteOrder = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnOrderGoesDown = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnOrderToBottom = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnClearOrders = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnOrderGoesUp = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnOrderToTop = new FrEee.UI.WinForms.Controls.GameButton();
		this.lstOrdersDetail = new System.Windows.Forms.ListBox();
		this.pageDetail.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.picPortrait)).BeginInit();
		this.gamePanel1.SuspendLayout();
		this.pnlStats.SuspendLayout();
		this.tableLayoutPanel1.SuspendLayout();
		this.tableLayoutPanel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.picOwnerFlag)).BeginInit();
		this.pageVehicles.SuspendLayout();
		this.pageAbility.SuspendLayout();
		this.gameTabControl1.SuspendLayout();
		this.pageOrders.SuspendLayout();
		this.SuspendLayout();
		// 
		// pageDetail
		// 
		this.pageDetail.AutoScroll = true;
		this.pageDetail.BackColor = System.Drawing.Color.Black;
		this.pageDetail.Controls.Add(this.txtAge);
		this.pageDetail.Controls.Add(this.txtCargoSpaceFree);
		this.pageDetail.Controls.Add(this.label1);
		this.pageDetail.Controls.Add(this.picPortrait);
		this.pageDetail.Controls.Add(this.resMaintMin);
		this.pageDetail.Controls.Add(this.lblComponents);
		this.pageDetail.Controls.Add(this.resMaintOrg);
		this.pageDetail.Controls.Add(this.resMaintRad);
		this.pageDetail.Controls.Add(this.lblIncome);
		this.pageDetail.Controls.Add(this.txtExperience);
		this.pageDetail.Controls.Add(this.lblExperience);
		this.pageDetail.Controls.Add(this.txtOrder);
		this.pageDetail.Controls.Add(this.lblOrder);
		this.pageDetail.Controls.Add(this.gamePanel1);
		this.pageDetail.Controls.Add(this.pnlStats);
		this.pageDetail.Controls.Add(this.progMovement);
		this.pageDetail.Controls.Add(this.lblMovement);
		this.pageDetail.Controls.Add(this.txtHullSize);
		this.pageDetail.Controls.Add(this.lblHullSize);
		this.pageDetail.Controls.Add(this.txtClass);
		this.pageDetail.Controls.Add(this.lblClass);
		this.pageDetail.Controls.Add(this.txtCount);
		this.pageDetail.Controls.Add(this.txtName);
		this.pageDetail.Controls.Add(this.picOwnerFlag);
		this.pageDetail.Location = new System.Drawing.Point(4, 29);
		this.pageDetail.Name = "pageDetail";
		this.pageDetail.Padding = new System.Windows.Forms.Padding(3);
		this.pageDetail.Size = new System.Drawing.Size(292, 492);
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
		this.txtAge.TabIndex = 56;
		this.txtAge.Text = "Current";
		// 
		// txtCargoSpaceFree
		// 
		this.txtCargoSpaceFree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.txtCargoSpaceFree.AutoSize = true;
		this.txtCargoSpaceFree.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtCargoSpaceFree.Location = new System.Drawing.Point(177, 351);
		this.txtCargoSpaceFree.Name = "txtCargoSpaceFree";
		this.txtCargoSpaceFree.Size = new System.Drawing.Size(101, 15);
		this.txtCargoSpaceFree.TabIndex = 91;
		this.txtCargoSpaceFree.Text = "0kT / 1200kT free";
		this.txtCargoSpaceFree.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		// 
		// label1
		// 
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label1.Location = new System.Drawing.Point(6, 346);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(40, 15);
		this.label1.TabIndex = 90;
		this.label1.Text = "Cargo";
		// 
		// picPortrait
		// 
		this.picPortrait.Location = new System.Drawing.Point(6, 32);
		this.picPortrait.Name = "picPortrait";
		this.picPortrait.Size = new System.Drawing.Size(128, 128);
		this.picPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.picPortrait.TabIndex = 89;
		this.picPortrait.TabStop = false;
		this.picPortrait.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picPortrait_MouseClick);
		// 
		// resMaintMin
		// 
		this.resMaintMin.Amount = 500;
		this.resMaintMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.resMaintMin.BackColor = System.Drawing.Color.Black;
		this.resMaintMin.Change = null;
		this.resMaintMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.resMaintMin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
		this.resMaintMin.Location = new System.Drawing.Point(60, 331);
		this.resMaintMin.Margin = new System.Windows.Forms.Padding(0);
		this.resMaintMin.Name = "resMaintMin";
		this.resMaintMin.ResourceName = "Minerals";
		this.resMaintMin.Size = new System.Drawing.Size(68, 20);
		this.resMaintMin.TabIndex = 46;
		// 
		// lblComponents
		// 
		this.lblComponents.AutoSize = true;
		this.lblComponents.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblComponents.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblComponents.Location = new System.Drawing.Point(6, 361);
		this.lblComponents.Name = "lblComponents";
		this.lblComponents.Size = new System.Drawing.Size(107, 15);
		this.lblComponents.TabIndex = 21;
		this.lblComponents.Text = "Vehicles/Subfleets";
		// 
		// resMaintOrg
		// 
		this.resMaintOrg.Amount = 100;
		this.resMaintOrg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.resMaintOrg.BackColor = System.Drawing.Color.Black;
		this.resMaintOrg.Change = null;
		this.resMaintOrg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.resMaintOrg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
		this.resMaintOrg.Location = new System.Drawing.Point(135, 331);
		this.resMaintOrg.Margin = new System.Windows.Forms.Padding(0);
		this.resMaintOrg.Name = "resMaintOrg";
		this.resMaintOrg.ResourceName = "Organics";
		this.resMaintOrg.Size = new System.Drawing.Size(68, 20);
		this.resMaintOrg.TabIndex = 45;
		// 
		// resMaintRad
		// 
		this.resMaintRad.Amount = 200;
		this.resMaintRad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.resMaintRad.BackColor = System.Drawing.Color.Black;
		this.resMaintRad.Change = null;
		this.resMaintRad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.resMaintRad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
		this.resMaintRad.Location = new System.Drawing.Point(210, 331);
		this.resMaintRad.Margin = new System.Windows.Forms.Padding(0);
		this.resMaintRad.Name = "resMaintRad";
		this.resMaintRad.ResourceName = "Radioactives";
		this.resMaintRad.Size = new System.Drawing.Size(68, 20);
		this.resMaintRad.TabIndex = 44;
		// 
		// lblIncome
		// 
		this.lblIncome.AutoSize = true;
		this.lblIncome.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblIncome.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblIncome.Location = new System.Drawing.Point(6, 331);
		this.lblIncome.Name = "lblIncome";
		this.lblIncome.Size = new System.Drawing.Size(48, 15);
		this.lblIncome.TabIndex = 43;
		this.lblIncome.Text = "Income";
		// 
		// txtExperience
		// 
		this.txtExperience.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.txtExperience.AutoSize = true;
		this.txtExperience.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtExperience.Location = new System.Drawing.Point(205, 305);
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
		this.lblExperience.Location = new System.Drawing.Point(6, 305);
		this.lblExperience.Name = "lblExperience";
		this.lblExperience.Size = new System.Drawing.Size(114, 15);
		this.lblExperience.TabIndex = 39;
		this.lblExperience.Text = "Admiral Experience";
		// 
		// txtOrder
		// 
		this.txtOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.txtOrder.AutoSize = true;
		this.txtOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtOrder.Location = new System.Drawing.Point(148, 290);
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
		this.lblOrder.Location = new System.Drawing.Point(6, 290);
		this.lblOrder.Name = "lblOrder";
		this.lblOrder.Size = new System.Drawing.Size(81, 15);
		this.lblOrder.TabIndex = 37;
		this.lblOrder.Text = "Current Order";
		// 
		// gamePanel1
		// 
		this.gamePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.gamePanel1.BackColor = System.Drawing.Color.Black;
		this.gamePanel1.BorderColor = System.Drawing.Color.CornflowerBlue;
		this.gamePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.gamePanel1.Controls.Add(this.lstVehicleSummary);
		this.gamePanel1.ForeColor = System.Drawing.Color.White;
		this.gamePanel1.Location = new System.Drawing.Point(9, 379);
		this.gamePanel1.Name = "gamePanel1";
		this.gamePanel1.Padding = new System.Windows.Forms.Padding(3);
		this.gamePanel1.Size = new System.Drawing.Size(272, 100);
		this.gamePanel1.TabIndex = 23;
		// 
		// lstVehicleSummary
		// 
		this.lstVehicleSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.lstVehicleSummary.BackColor = System.Drawing.Color.Black;
		this.lstVehicleSummary.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.lstVehicleSummary.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
		this.lstVehicleSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lstVehicleSummary.ForeColor = System.Drawing.Color.White;
		this.lstVehicleSummary.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
		this.lstVehicleSummary.HideSelection = false;
		this.lstVehicleSummary.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
		this.lstVehicleSummary.Location = new System.Drawing.Point(4, 3);
		this.lstVehicleSummary.Name = "lstVehicleSummary";
		this.lstVehicleSummary.Size = new System.Drawing.Size(264, 92);
		this.lstVehicleSummary.TabIndex = 23;
		this.lstVehicleSummary.TileSize = new System.Drawing.Size(64, 64);
		this.lstVehicleSummary.UseCompatibleStateImageBehavior = false;
		this.lstVehicleSummary.View = System.Windows.Forms.View.Tile;
		this.lstVehicleSummary.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstVehicleSummary_MouseClick);
		// 
		// columnHeader2
		// 
		this.columnHeader2.Width = 164;
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
		this.pnlStats.Location = new System.Drawing.Point(8, 163);
		this.pnlStats.Margin = new System.Windows.Forms.Padding(0);
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
		this.progSupplies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.progSupplies.Dock = System.Windows.Forms.DockStyle.Fill;
		this.progSupplies.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.progSupplies.ForeColor = System.Drawing.Color.White;
		this.progSupplies.IncrementalProgress = ((long)(0));
		this.progSupplies.LeftText = "";
		this.progSupplies.Location = new System.Drawing.Point(0, 20);
		this.progSupplies.Margin = new System.Windows.Forms.Padding(0);
		this.progSupplies.Maximum = ((long)(3000));
		this.progSupplies.Name = "progSupplies";
		this.progSupplies.Padding = new System.Windows.Forms.Padding(5);
		this.progSupplies.ProgressDisplayType = FrEee.UI.Blazor.Views.ProgressDisplayType.Numeric;
		this.progSupplies.RightText = "";
		this.progSupplies.Size = new System.Drawing.Size(133, 19);
		this.progSupplies.TabIndex = 18;
		this.progSupplies.Value = ((long)(2500));
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
		this.progAmmunition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.progAmmunition.Dock = System.Windows.Forms.DockStyle.Fill;
		this.progAmmunition.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.progAmmunition.ForeColor = System.Drawing.Color.White;
		this.progAmmunition.IncrementalProgress = ((long)(0));
		this.progAmmunition.LeftText = "";
		this.progAmmunition.Location = new System.Drawing.Point(0, 59);
		this.progAmmunition.Margin = new System.Windows.Forms.Padding(0);
		this.progAmmunition.Maximum = ((long)(500));
		this.progAmmunition.Name = "progAmmunition";
		this.progAmmunition.Padding = new System.Windows.Forms.Padding(5);
		this.progAmmunition.ProgressDisplayType = FrEee.UI.Blazor.Views.ProgressDisplayType.Numeric;
		this.progAmmunition.RightText = "";
		this.progAmmunition.Size = new System.Drawing.Size(133, 19);
		this.progAmmunition.TabIndex = 20;
		this.progAmmunition.Value = ((long)(100));
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
		this.progFuel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.progFuel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.progFuel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.progFuel.ForeColor = System.Drawing.Color.White;
		this.progFuel.IncrementalProgress = ((long)(0));
		this.progFuel.LeftText = "";
		this.progFuel.Location = new System.Drawing.Point(0, 98);
		this.progFuel.Margin = new System.Windows.Forms.Padding(0);
		this.progFuel.Maximum = ((long)(2000));
		this.progFuel.Name = "progFuel";
		this.progFuel.Padding = new System.Windows.Forms.Padding(5);
		this.progFuel.ProgressDisplayType = FrEee.UI.Blazor.Views.ProgressDisplayType.Numeric;
		this.progFuel.RightText = "";
		this.progFuel.Size = new System.Drawing.Size(133, 19);
		this.progFuel.TabIndex = 22;
		this.progFuel.Value = ((long)(1000));
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
		this.progHull.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.progHull.Dock = System.Windows.Forms.DockStyle.Fill;
		this.progHull.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.progHull.ForeColor = System.Drawing.Color.White;
		this.progHull.IncrementalProgress = ((long)(0));
		this.progHull.LeftText = "";
		this.progHull.Location = new System.Drawing.Point(0, 98);
		this.progHull.Margin = new System.Windows.Forms.Padding(0);
		this.progHull.Maximum = ((long)(200));
		this.progHull.Name = "progHull";
		this.progHull.Padding = new System.Windows.Forms.Padding(5);
		this.progHull.ProgressDisplayType = FrEee.UI.Blazor.Views.ProgressDisplayType.Numeric;
		this.progHull.RightText = "";
		this.progHull.Size = new System.Drawing.Size(133, 19);
		this.progHull.TabIndex = 30;
		this.progHull.Value = ((long)(150));
		// 
		// progArmor
		// 
		this.progArmor.BackColor = System.Drawing.Color.Black;
		this.progArmor.BarColor = System.Drawing.Color.Blue;
		this.progArmor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.progArmor.Dock = System.Windows.Forms.DockStyle.Fill;
		this.progArmor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.progArmor.ForeColor = System.Drawing.Color.White;
		this.progArmor.IncrementalProgress = ((long)(0));
		this.progArmor.LeftText = "";
		this.progArmor.Location = new System.Drawing.Point(0, 59);
		this.progArmor.Margin = new System.Windows.Forms.Padding(0);
		this.progArmor.Maximum = ((long)(300));
		this.progArmor.Name = "progArmor";
		this.progArmor.Padding = new System.Windows.Forms.Padding(5);
		this.progArmor.ProgressDisplayType = FrEee.UI.Blazor.Views.ProgressDisplayType.Numeric;
		this.progArmor.RightText = "";
		this.progArmor.Size = new System.Drawing.Size(133, 19);
		this.progArmor.TabIndex = 29;
		this.progArmor.Value = ((long)(100));
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
		this.progShields.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.progShields.Dock = System.Windows.Forms.DockStyle.Fill;
		this.progShields.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.progShields.ForeColor = System.Drawing.Color.White;
		this.progShields.IncrementalProgress = ((long)(0));
		this.progShields.LeftText = "";
		this.progShields.Location = new System.Drawing.Point(0, 20);
		this.progShields.Margin = new System.Windows.Forms.Padding(0);
		this.progShields.Maximum = ((long)(200));
		this.progShields.Name = "progShields";
		this.progShields.Padding = new System.Windows.Forms.Padding(5);
		this.progShields.ProgressDisplayType = FrEee.UI.Blazor.Views.ProgressDisplayType.Numeric;
		this.progShields.RightText = "";
		this.progShields.Size = new System.Drawing.Size(133, 19);
		this.progShields.TabIndex = 24;
		this.progShields.Value = ((long)(200));
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
		// progMovement
		// 
		this.progMovement.BackColor = System.Drawing.Color.Black;
		this.progMovement.BarColor = System.Drawing.Color.Blue;
		this.progMovement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.progMovement.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
		this.progMovement.ForeColor = System.Drawing.Color.White;
		this.progMovement.IncrementalProgress = ((long)(0));
		this.progMovement.LeftText = "";
		this.progMovement.Location = new System.Drawing.Point(162, 133);
		this.progMovement.Margin = new System.Windows.Forms.Padding(0);
		this.progMovement.Maximum = ((long)(6));
		this.progMovement.Name = "progMovement";
		this.progMovement.Padding = new System.Windows.Forms.Padding(5);
		this.progMovement.ProgressDisplayType = FrEee.UI.Blazor.Views.ProgressDisplayType.Numeric;
		this.progMovement.RightText = "";
		this.progMovement.Size = new System.Drawing.Size(103, 18);
		this.progMovement.TabIndex = 18;
		this.progMovement.Value = ((long)(4));
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
		// txtHullSize
		// 
		this.txtHullSize.AutoSize = true;
		this.txtHullSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtHullSize.Location = new System.Drawing.Point(159, 103);
		this.txtHullSize.Name = "txtHullSize";
		this.txtHullSize.Size = new System.Drawing.Size(48, 15);
		this.txtHullSize.TabIndex = 15;
		this.txtHullSize.Text = "5000kT";
		// 
		// lblHullSize
		// 
		this.lblHullSize.AutoSize = true;
		this.lblHullSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblHullSize.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblHullSize.Location = new System.Drawing.Point(144, 88);
		this.lblHullSize.Name = "lblHullSize";
		this.lblHullSize.Size = new System.Drawing.Size(86, 15);
		this.lblHullSize.TabIndex = 14;
		this.lblHullSize.Text = "Total Tonnage";
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
		this.lblClass.Size = new System.Drawing.Size(82, 15);
		this.lblClass.TabIndex = 12;
		this.lblClass.Text = "Primary Class";
		// 
		// txtCount
		// 
		this.txtCount.AutoSize = true;
		this.txtCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtCount.Location = new System.Drawing.Point(159, 26);
		this.txtCount.Name = "txtCount";
		this.txtCount.Size = new System.Drawing.Size(68, 15);
		this.txtCount.TabIndex = 11;
		this.txtCount.Text = "12 vehicles";
		// 
		// txtName
		// 
		this.txtName.AutoSize = true;
		this.txtName.Location = new System.Drawing.Point(143, 6);
		this.txtName.Name = "txtName";
		this.txtName.Size = new System.Drawing.Size(102, 20);
		this.txtName.TabIndex = 10;
		this.txtName.Text = "Assault Fleet";
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
		// pageVehicles
		// 
		this.pageVehicles.BackColor = System.Drawing.Color.Black;
		this.pageVehicles.Controls.Add(this.treeVehicles);
		this.pageVehicles.Location = new System.Drawing.Point(4, 29);
		this.pageVehicles.Name = "pageVehicles";
		this.pageVehicles.Padding = new System.Windows.Forms.Padding(3);
		this.pageVehicles.Size = new System.Drawing.Size(292, 492);
		this.pageVehicles.TabIndex = 2;
		this.pageVehicles.Text = "Vehicles";
		// 
		// treeVehicles
		// 
		this.treeVehicles.BackColor = System.Drawing.Color.Black;
		this.treeVehicles.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.treeVehicles.Dock = System.Windows.Forms.DockStyle.Fill;
		this.treeVehicles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.treeVehicles.ForeColor = System.Drawing.Color.White;
		this.treeVehicles.Location = new System.Drawing.Point(3, 3);
		this.treeVehicles.Name = "treeVehicles";
		this.treeVehicles.Size = new System.Drawing.Size(286, 486);
		this.treeVehicles.TabIndex = 0;
		this.treeVehicles.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeVehicles_NodeMouseClick);
		// 
		// pageAbility
		// 
		this.pageAbility.BackColor = System.Drawing.Color.Black;
		this.pageAbility.Controls.Add(this.abilityTreeView);
		this.pageAbility.Location = new System.Drawing.Point(4, 29);
		this.pageAbility.Name = "pageAbility";
		this.pageAbility.Padding = new System.Windows.Forms.Padding(3);
		this.pageAbility.Size = new System.Drawing.Size(292, 492);
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
		this.abilityTreeView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.abilityTreeView.Name = "abilityTreeView";
		this.abilityTreeView.Size = new System.Drawing.Size(286, 486);
		this.abilityTreeView.TabIndex = 0;
		// 
		// gameTabControl1
		// 
		this.gameTabControl1.Controls.Add(this.pageDetail);
		this.gameTabControl1.Controls.Add(this.pageOrders);
		this.gameTabControl1.Controls.Add(this.pageVehicles);
		this.gameTabControl1.Controls.Add(this.pageAbility);
		this.gameTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gameTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
		this.gameTabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.gameTabControl1.Location = new System.Drawing.Point(0, 0);
		this.gameTabControl1.Name = "gameTabControl1";
		this.gameTabControl1.SelectedIndex = 0;
		this.gameTabControl1.SelectedTabBackColor = System.Drawing.Color.SkyBlue;
		this.gameTabControl1.SelectedTabForeColor = System.Drawing.Color.Black;
		this.gameTabControl1.Size = new System.Drawing.Size(300, 525);
		this.gameTabControl1.TabBackColor = System.Drawing.Color.Black;
		this.gameTabControl1.TabBorderColor = System.Drawing.Color.CornflowerBlue;
		this.gameTabControl1.TabForeColor = System.Drawing.Color.CornflowerBlue;
		this.gameTabControl1.TabIndex = 1;
		// 
		// pageOrders
		// 
		this.pageOrders.AutoScroll = true;
		this.pageOrders.BackColor = System.Drawing.Color.Black;
		this.pageOrders.Controls.Add(this.chkOnHold);
		this.pageOrders.Controls.Add(this.chkRepeat);
		this.pageOrders.Controls.Add(this.btnDeleteOrder);
		this.pageOrders.Controls.Add(this.btnOrderGoesDown);
		this.pageOrders.Controls.Add(this.btnOrderToBottom);
		this.pageOrders.Controls.Add(this.btnClearOrders);
		this.pageOrders.Controls.Add(this.btnOrderGoesUp);
		this.pageOrders.Controls.Add(this.btnOrderToTop);
		this.pageOrders.Controls.Add(this.lstOrdersDetail);
		this.pageOrders.Location = new System.Drawing.Point(4, 29);
		this.pageOrders.Name = "pageOrders";
		this.pageOrders.Padding = new System.Windows.Forms.Padding(3);
		this.pageOrders.Size = new System.Drawing.Size(292, 492);
		this.pageOrders.TabIndex = 1;
		this.pageOrders.Text = "Orders";
		// 
		// chkOnHold
		// 
		this.chkOnHold.AutoSize = true;
		this.chkOnHold.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
		this.chkOnHold.Location = new System.Drawing.Point(120, 74);
		this.chkOnHold.Name = "chkOnHold";
		this.chkOnHold.Size = new System.Drawing.Size(111, 19);
		this.chkOnHold.TabIndex = 20;
		this.chkOnHold.Text = "Orders On Hold";
		this.chkOnHold.UseVisualStyleBackColor = true;
		this.chkOnHold.CheckedChanged += new System.EventHandler(this.chkOnHold_CheckedChanged);
		// 
		// chkRepeat
		// 
		this.chkRepeat.AutoSize = true;
		this.chkRepeat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
		this.chkRepeat.Location = new System.Drawing.Point(8, 73);
		this.chkRepeat.Name = "chkRepeat";
		this.chkRepeat.Size = new System.Drawing.Size(106, 19);
		this.chkRepeat.TabIndex = 19;
		this.chkRepeat.Text = "Repeat Orders";
		this.chkRepeat.UseVisualStyleBackColor = true;
		this.chkRepeat.CheckedChanged += new System.EventHandler(this.chkRepeat_CheckedChanged);
		// 
		// btnDeleteOrder
		// 
		this.btnDeleteOrder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.btnDeleteOrder.BackColor = System.Drawing.Color.Black;
		this.btnDeleteOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.btnDeleteOrder.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnDeleteOrder.Location = new System.Drawing.Point(214, 37);
		this.btnDeleteOrder.Name = "btnDeleteOrder";
		this.btnDeleteOrder.Size = new System.Drawing.Size(72, 30);
		this.btnDeleteOrder.TabIndex = 17;
		this.btnDeleteOrder.Text = "Delete";
		this.btnDeleteOrder.UseVisualStyleBackColor = false;
		this.btnDeleteOrder.Click += new System.EventHandler(this.btnDeleteOrder_Click);
		// 
		// btnOrderGoesDown
		// 
		this.btnOrderGoesDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.btnOrderGoesDown.BackColor = System.Drawing.Color.Black;
		this.btnOrderGoesDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.btnOrderGoesDown.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnOrderGoesDown.Location = new System.Drawing.Point(113, 37);
		this.btnOrderGoesDown.Name = "btnOrderGoesDown";
		this.btnOrderGoesDown.Size = new System.Drawing.Size(72, 30);
		this.btnOrderGoesDown.TabIndex = 16;
		this.btnOrderGoesDown.Text = "Down";
		this.btnOrderGoesDown.UseVisualStyleBackColor = false;
		this.btnOrderGoesDown.Click += new System.EventHandler(this.btnOrderGoesDown_Click);
		// 
		// btnOrderToBottom
		// 
		this.btnOrderToBottom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.btnOrderToBottom.BackColor = System.Drawing.Color.Black;
		this.btnOrderToBottom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.btnOrderToBottom.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnOrderToBottom.Location = new System.Drawing.Point(8, 37);
		this.btnOrderToBottom.Name = "btnOrderToBottom";
		this.btnOrderToBottom.Size = new System.Drawing.Size(72, 30);
		this.btnOrderToBottom.TabIndex = 15;
		this.btnOrderToBottom.Text = "Bottom";
		this.btnOrderToBottom.UseVisualStyleBackColor = false;
		this.btnOrderToBottom.Click += new System.EventHandler(this.btnOrderToBottom_Click);
		// 
		// btnClearOrders
		// 
		this.btnClearOrders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.btnClearOrders.BackColor = System.Drawing.Color.Black;
		this.btnClearOrders.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.btnClearOrders.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnClearOrders.Location = new System.Drawing.Point(214, 6);
		this.btnClearOrders.Name = "btnClearOrders";
		this.btnClearOrders.Size = new System.Drawing.Size(72, 30);
		this.btnClearOrders.TabIndex = 14;
		this.btnClearOrders.Text = "Clear";
		this.btnClearOrders.UseVisualStyleBackColor = false;
		this.btnClearOrders.Click += new System.EventHandler(this.btnClearOrders_Click);
		// 
		// btnOrderGoesUp
		// 
		this.btnOrderGoesUp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.btnOrderGoesUp.BackColor = System.Drawing.Color.Black;
		this.btnOrderGoesUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.btnOrderGoesUp.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnOrderGoesUp.Location = new System.Drawing.Point(113, 6);
		this.btnOrderGoesUp.Name = "btnOrderGoesUp";
		this.btnOrderGoesUp.Size = new System.Drawing.Size(72, 30);
		this.btnOrderGoesUp.TabIndex = 13;
		this.btnOrderGoesUp.Text = "Up";
		this.btnOrderGoesUp.UseVisualStyleBackColor = false;
		this.btnOrderGoesUp.Click += new System.EventHandler(this.btnOrderGoesUp_Click);
		// 
		// btnOrderToTop
		// 
		this.btnOrderToTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.btnOrderToTop.BackColor = System.Drawing.Color.Black;
		this.btnOrderToTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.btnOrderToTop.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnOrderToTop.Location = new System.Drawing.Point(8, 6);
		this.btnOrderToTop.Name = "btnOrderToTop";
		this.btnOrderToTop.Size = new System.Drawing.Size(72, 30);
		this.btnOrderToTop.TabIndex = 12;
		this.btnOrderToTop.Text = "Top";
		this.btnOrderToTop.UseVisualStyleBackColor = false;
		this.btnOrderToTop.Click += new System.EventHandler(this.btnOrderToTop_Click);
		// 
		// lstOrdersDetail
		// 
		this.lstOrdersDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.lstOrdersDetail.BackColor = System.Drawing.Color.Black;
		this.lstOrdersDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.lstOrdersDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lstOrdersDetail.ForeColor = System.Drawing.Color.White;
		this.lstOrdersDetail.FormattingEnabled = true;
		this.lstOrdersDetail.ItemHeight = 15;
		this.lstOrdersDetail.Items.AddRange(new object[] {
            "Move to Kartogia (0, -5)",
            "Drop All Eee Population",
            "Load All Troops",
            "Move to Fizbon (3, 6)"});
		this.lstOrdersDetail.Location = new System.Drawing.Point(7, 99);
		this.lstOrdersDetail.Name = "lstOrdersDetail";
		this.lstOrdersDetail.Size = new System.Drawing.Size(279, 375);
		this.lstOrdersDetail.TabIndex = 0;
		// 
		// FleetReport
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.Controls.Add(this.gameTabControl1);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "FleetReport";
		this.Size = new System.Drawing.Size(300, 525);
		this.pageDetail.ResumeLayout(false);
		this.pageDetail.PerformLayout();
		((System.ComponentModel.ISupportInitialize)(this.picPortrait)).EndInit();
		this.gamePanel1.ResumeLayout(false);
		this.pnlStats.ResumeLayout(false);
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.PerformLayout();
		this.tableLayoutPanel2.ResumeLayout(false);
		this.tableLayoutPanel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)(this.picOwnerFlag)).EndInit();
		this.pageVehicles.ResumeLayout(false);
		this.pageAbility.ResumeLayout(false);
		this.gameTabControl1.ResumeLayout(false);
		this.pageOrders.ResumeLayout(false);
		this.pageOrders.PerformLayout();
		this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.TabPage pageDetail;
	private System.Windows.Forms.TabPage pageVehicles;
	private System.Windows.Forms.Label txtHullSize;
	private System.Windows.Forms.Label lblHullSize;
	private System.Windows.Forms.Label txtClass;
	private System.Windows.Forms.Label lblClass;
	private System.Windows.Forms.Label txtCount;
	private System.Windows.Forms.Label txtName;
	private System.Windows.Forms.PictureBox picOwnerFlag;
	private System.Windows.Forms.TabPage pageAbility;
	private System.Windows.Forms.Label lblMovement;
	private GameProgressBar progMovement;
	private System.Windows.Forms.TableLayoutPanel pnlStats;
	private System.Windows.Forms.Label lblComponents;
	private GamePanel gamePanel1;
	private System.Windows.Forms.ListView lstVehicleSummary;
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
	private ResourceDisplay resMaintMin;
	private ResourceDisplay resMaintOrg;
	private ResourceDisplay resMaintRad;
	private System.Windows.Forms.Label lblIncome;
	private System.Windows.Forms.Label txtExperience;
	private System.Windows.Forms.Label lblExperience;
	private System.Windows.Forms.Label txtOrder;
	private System.Windows.Forms.Label lblOrder;
	private GameTabControl gameTabControl1;
	private System.Windows.Forms.TabPage pageOrders;
	private GameButton btnDeleteOrder;
	private GameButton btnOrderGoesDown;
	private GameButton btnOrderToBottom;
	private GameButton btnClearOrders;
	private GameButton btnOrderGoesUp;
	private GameButton btnOrderToTop;
	private System.Windows.Forms.ListBox lstOrdersDetail;
	private AbilityTreeView abilityTreeView;
	private System.Windows.Forms.ColumnHeader columnHeader2;
	private GamePictureBox picPortrait;
	private System.Windows.Forms.TreeView treeVehicles;
	private System.Windows.Forms.Label txtCargoSpaceFree;
	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.Label txtAge;
	private System.Windows.Forms.CheckBox chkRepeat;
	private System.Windows.Forms.CheckBox chkOnHold;
}
