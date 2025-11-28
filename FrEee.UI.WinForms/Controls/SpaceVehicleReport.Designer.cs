using FrEee.UI.WinForms.Controls.Blazor;
using FrEee.UI.WinForms.Controls;

namespace FrEee.UI.WinForms.Controls;

partial class SpaceVehicleReport
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
		this.gameTabControl1 = new FrEee.UI.WinForms.Controls.GameTabControl();
		this.pageDetail = new System.Windows.Forms.TabPage();
		this.panel1 = new System.Windows.Forms.Panel();
		this.txtAge = new System.Windows.Forms.Label();
		this.picPortrait = new FrEee.UI.WinForms.Controls.GamePictureBox();
		this.txtConstructionTime = new System.Windows.Forms.Label();
		this.lblConstructionTime = new System.Windows.Forms.Label();
		this.txtConstructionItem = new System.Windows.Forms.Label();
		this.lblConstructionItem = new System.Windows.Forms.Label();
		this.txtComponentsFunctional = new System.Windows.Forms.Label();
		this.resIncomeMin = new FrEee.UI.WinForms.Controls.Blazor.ResourceDisplay();
		this.lblComponents = new System.Windows.Forms.Label();
		this.resIncomeOrg = new FrEee.UI.WinForms.Controls.Blazor.ResourceDisplay();
		this.resIncomeRad = new FrEee.UI.WinForms.Controls.Blazor.ResourceDisplay();
		this.lblIncome = new System.Windows.Forms.Label();
		this.txtFleet = new System.Windows.Forms.Label();
		this.lblFleet = new System.Windows.Forms.Label();
		this.txtExperience = new System.Windows.Forms.Label();
		this.lblExperience = new System.Windows.Forms.Label();
		this.txtOrder = new System.Windows.Forms.Label();
		this.lblOrder = new System.Windows.Forms.Label();
		this.txtCargoSpaceFree = new System.Windows.Forms.Label();
		this.gamePanel2 = new FrEee.UI.WinForms.Controls.GamePanel();
		this.lstCargoSummary = new System.Windows.Forms.ListView();
		this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
		this.lblCargo = new System.Windows.Forms.Label();
		this.gamePanel1 = new FrEee.UI.WinForms.Controls.GamePanel();
		this.lstComponentsSummary = new System.Windows.Forms.ListView();
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
		this.txtRole = new System.Windows.Forms.Label();
		this.txtName = new System.Windows.Forms.Label();
		this.picOwnerFlag = new System.Windows.Forms.PictureBox();
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
		this.pageComps = new System.Windows.Forms.TabPage();
		this.txtComponentsFunctionalDetail = new System.Windows.Forms.Label();
		this.lstComponentsDetail = new System.Windows.Forms.ListView();
		this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
		this.pageCargo = new System.Windows.Forms.TabPage();
		this.txtCargoSpaceFreeDetail = new System.Windows.Forms.Label();
		this.lstCargoDetail = new System.Windows.Forms.ListView();
		this.pageAbility = new System.Windows.Forms.TabPage();
		this.abilityTreeView = new FrEee.UI.WinForms.Controls.AbilityTreeView();
		this.gameTabControl1.SuspendLayout();
		this.pageDetail.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.picPortrait)).BeginInit();
		this.gamePanel2.SuspendLayout();
		this.gamePanel1.SuspendLayout();
		this.pnlStats.SuspendLayout();
		this.tableLayoutPanel1.SuspendLayout();
		this.tableLayoutPanel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.picOwnerFlag)).BeginInit();
		this.pageOrders.SuspendLayout();
		this.pageComps.SuspendLayout();
		this.pageCargo.SuspendLayout();
		this.pageAbility.SuspendLayout();
		this.SuspendLayout();
		// 
		// gameTabControl1
		// 
		this.gameTabControl1.Controls.Add(this.pageDetail);
		this.gameTabControl1.Controls.Add(this.pageOrders);
		this.gameTabControl1.Controls.Add(this.pageComps);
		this.gameTabControl1.Controls.Add(this.pageCargo);
		this.gameTabControl1.Controls.Add(this.pageAbility);
		this.gameTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gameTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
		this.gameTabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.gameTabControl1.Location = new System.Drawing.Point(0, 0);
		this.gameTabControl1.Name = "gameTabControl1";
		this.gameTabControl1.SelectedIndex = 0;
		this.gameTabControl1.SelectedTabBackColor = System.Drawing.Color.SkyBlue;
		this.gameTabControl1.SelectedTabForeColor = System.Drawing.Color.Black;
		this.gameTabControl1.Size = new System.Drawing.Size(300, 675);
		this.gameTabControl1.TabBackColor = System.Drawing.Color.Black;
		this.gameTabControl1.TabBorderColor = System.Drawing.Color.CornflowerBlue;
		this.gameTabControl1.TabForeColor = System.Drawing.Color.CornflowerBlue;
		this.gameTabControl1.TabIndex = 1;
		// 
		// pageDetail
		// 
		this.pageDetail.AutoScroll = true;
		this.pageDetail.BackColor = System.Drawing.Color.Black;
		this.pageDetail.Controls.Add(this.panel1);
		this.pageDetail.Location = new System.Drawing.Point(4, 29);
		this.pageDetail.Name = "pageDetail";
		this.pageDetail.Padding = new System.Windows.Forms.Padding(3);
		this.pageDetail.Size = new System.Drawing.Size(292, 642);
		this.pageDetail.TabIndex = 0;
		this.pageDetail.Text = "Detail";
		// 
		// panel1
		// 
		this.panel1.Controls.Add(this.txtAge);
		this.panel1.Controls.Add(this.picPortrait);
		this.panel1.Controls.Add(this.txtConstructionTime);
		this.panel1.Controls.Add(this.lblConstructionTime);
		this.panel1.Controls.Add(this.txtConstructionItem);
		this.panel1.Controls.Add(this.lblConstructionItem);
		this.panel1.Controls.Add(this.txtComponentsFunctional);
		this.panel1.Controls.Add(this.resIncomeMin);
		this.panel1.Controls.Add(this.lblComponents);
		this.panel1.Controls.Add(this.resIncomeOrg);
		this.panel1.Controls.Add(this.resIncomeRad);
		this.panel1.Controls.Add(this.lblIncome);
		this.panel1.Controls.Add(this.txtFleet);
		this.panel1.Controls.Add(this.lblFleet);
		this.panel1.Controls.Add(this.txtExperience);
		this.panel1.Controls.Add(this.lblExperience);
		this.panel1.Controls.Add(this.txtOrder);
		this.panel1.Controls.Add(this.lblOrder);
		this.panel1.Controls.Add(this.txtCargoSpaceFree);
		this.panel1.Controls.Add(this.gamePanel2);
		this.panel1.Controls.Add(this.lblCargo);
		this.panel1.Controls.Add(this.gamePanel1);
		this.panel1.Controls.Add(this.pnlStats);
		this.panel1.Controls.Add(this.progMovement);
		this.panel1.Controls.Add(this.lblMovement);
		this.panel1.Controls.Add(this.txtHullSize);
		this.panel1.Controls.Add(this.lblHullSize);
		this.panel1.Controls.Add(this.txtClass);
		this.panel1.Controls.Add(this.lblClass);
		this.panel1.Controls.Add(this.txtRole);
		this.panel1.Controls.Add(this.txtName);
		this.panel1.Controls.Add(this.picOwnerFlag);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(3, 3);
		this.panel1.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(286, 636);
		this.panel1.TabIndex = 0;
		// 
		// txtAge
		// 
		this.txtAge.AutoSize = true;
		this.txtAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtAge.Location = new System.Drawing.Point(159, 40);
		this.txtAge.Name = "txtAge";
		this.txtAge.Size = new System.Drawing.Size(47, 15);
		this.txtAge.TabIndex = 56;
		this.txtAge.Text = "Current";
		// 
		// picPortrait
		// 
		this.picPortrait.Location = new System.Drawing.Point(3, 29);
		this.picPortrait.Name = "picPortrait";
		this.picPortrait.Size = new System.Drawing.Size(128, 128);
		this.picPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.picPortrait.TabIndex = 121;
		this.picPortrait.TabStop = false;
		// 
		// txtConstructionTime
		// 
		this.txtConstructionTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.txtConstructionTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtConstructionTime.Location = new System.Drawing.Point(159, 346);
		this.txtConstructionTime.Name = "txtConstructionTime";
		this.txtConstructionTime.Size = new System.Drawing.Size(127, 15);
		this.txtConstructionTime.TabIndex = 120;
		this.txtConstructionTime.Text = "0.3 years";
		this.txtConstructionTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		// 
		// lblConstructionTime
		// 
		this.lblConstructionTime.AutoSize = true;
		this.lblConstructionTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblConstructionTime.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblConstructionTime.Location = new System.Drawing.Point(4, 346);
		this.lblConstructionTime.Name = "lblConstructionTime";
		this.lblConstructionTime.Size = new System.Drawing.Size(99, 15);
		this.lblConstructionTime.TabIndex = 119;
		this.lblConstructionTime.Text = "Time Remaining";
		// 
		// txtConstructionItem
		// 
		this.txtConstructionItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.txtConstructionItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtConstructionItem.Location = new System.Drawing.Point(159, 331);
		this.txtConstructionItem.Name = "txtConstructionItem";
		this.txtConstructionItem.Size = new System.Drawing.Size(127, 15);
		this.txtConstructionItem.TabIndex = 118;
		this.txtConstructionItem.Text = "Barracuda IV";
		this.txtConstructionItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		// 
		// lblConstructionItem
		// 
		this.lblConstructionItem.AutoSize = true;
		this.lblConstructionItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblConstructionItem.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblConstructionItem.Location = new System.Drawing.Point(3, 331);
		this.lblConstructionItem.Name = "lblConstructionItem";
		this.lblConstructionItem.Size = new System.Drawing.Size(112, 15);
		this.lblConstructionItem.TabIndex = 117;
		this.lblConstructionItem.Text = "Under Construction";
		// 
		// txtComponentsFunctional
		// 
		this.txtComponentsFunctional.AutoSize = true;
		this.txtComponentsFunctional.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtComponentsFunctional.Location = new System.Drawing.Point(173, 381);
		this.txtComponentsFunctional.Name = "txtComponentsFunctional";
		this.txtComponentsFunctional.Size = new System.Drawing.Size(94, 15);
		this.txtComponentsFunctional.TabIndex = 105;
		this.txtComponentsFunctional.Text = "11/11 functional";
		// 
		// resIncomeMin
		// 
		this.resIncomeMin.Amount = 500;
		this.resIncomeMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.resIncomeMin.BackColor = System.Drawing.Color.Black;
		this.resIncomeMin.Change = null;
		this.resIncomeMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.resIncomeMin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
		this.resIncomeMin.Location = new System.Drawing.Point(63, 361);
		this.resIncomeMin.Margin = new System.Windows.Forms.Padding(0);
		this.resIncomeMin.Name = "resIncomeMin";
		this.resIncomeMin.ResourceName = "Minerals";
		this.resIncomeMin.Size = new System.Drawing.Size(68, 20);
		this.resIncomeMin.TabIndex = 116;
		// 
		// lblComponents
		// 
		this.lblComponents.AutoSize = true;
		this.lblComponents.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblComponents.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblComponents.Location = new System.Drawing.Point(7, 381);
		this.lblComponents.Name = "lblComponents";
		this.lblComponents.Size = new System.Drawing.Size(77, 15);
		this.lblComponents.TabIndex = 101;
		this.lblComponents.Text = "Components";
		// 
		// resIncomeOrg
		// 
		this.resIncomeOrg.Amount = 100;
		this.resIncomeOrg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.resIncomeOrg.BackColor = System.Drawing.Color.Black;
		this.resIncomeOrg.Change = null;
		this.resIncomeOrg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.resIncomeOrg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
		this.resIncomeOrg.Location = new System.Drawing.Point(138, 361);
		this.resIncomeOrg.Margin = new System.Windows.Forms.Padding(0);
		this.resIncomeOrg.Name = "resIncomeOrg";
		this.resIncomeOrg.ResourceName = "Organics";
		this.resIncomeOrg.Size = new System.Drawing.Size(68, 20);
		this.resIncomeOrg.TabIndex = 115;
		// 
		// resIncomeRad
		// 
		this.resIncomeRad.Amount = 200;
		this.resIncomeRad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.resIncomeRad.BackColor = System.Drawing.Color.Black;
		this.resIncomeRad.Change = null;
		this.resIncomeRad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.resIncomeRad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
		this.resIncomeRad.Location = new System.Drawing.Point(215, 361);
		this.resIncomeRad.Margin = new System.Windows.Forms.Padding(0);
		this.resIncomeRad.Name = "resIncomeRad";
		this.resIncomeRad.ResourceName = "Radioactives";
		this.resIncomeRad.Size = new System.Drawing.Size(68, 20);
		this.resIncomeRad.TabIndex = 114;
		// 
		// lblIncome
		// 
		this.lblIncome.AutoSize = true;
		this.lblIncome.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblIncome.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblIncome.Location = new System.Drawing.Point(6, 361);
		this.lblIncome.Name = "lblIncome";
		this.lblIncome.Size = new System.Drawing.Size(48, 15);
		this.lblIncome.TabIndex = 113;
		this.lblIncome.Text = "Income";
		// 
		// txtFleet
		// 
		this.txtFleet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.txtFleet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtFleet.Location = new System.Drawing.Point(156, 317);
		this.txtFleet.Name = "txtFleet";
		this.txtFleet.Size = new System.Drawing.Size(130, 15);
		this.txtFleet.TabIndex = 112;
		this.txtFleet.Text = "Alpha Fleet";
		this.txtFleet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		// 
		// lblFleet
		// 
		this.lblFleet.AutoSize = true;
		this.lblFleet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblFleet.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblFleet.Location = new System.Drawing.Point(3, 317);
		this.lblFleet.Name = "lblFleet";
		this.lblFleet.Size = new System.Drawing.Size(34, 15);
		this.lblFleet.TabIndex = 111;
		this.lblFleet.Text = "Fleet";
		// 
		// txtExperience
		// 
		this.txtExperience.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.txtExperience.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtExperience.Location = new System.Drawing.Point(159, 302);
		this.txtExperience.Name = "txtExperience";
		this.txtExperience.Size = new System.Drawing.Size(127, 15);
		this.txtExperience.TabIndex = 110;
		this.txtExperience.Text = "Novice (0%)";
		this.txtExperience.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		// 
		// lblExperience
		// 
		this.lblExperience.AutoSize = true;
		this.lblExperience.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblExperience.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblExperience.Location = new System.Drawing.Point(3, 302);
		this.lblExperience.Name = "lblExperience";
		this.lblExperience.Size = new System.Drawing.Size(100, 15);
		this.lblExperience.TabIndex = 109;
		this.lblExperience.Text = "Crew Experience";
		// 
		// txtOrder
		// 
		this.txtOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.txtOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtOrder.Location = new System.Drawing.Point(151, 287);
		this.txtOrder.Name = "txtOrder";
		this.txtOrder.Size = new System.Drawing.Size(135, 15);
		this.txtOrder.TabIndex = 108;
		this.txtOrder.Text = "Move to Kartogia (0, 5)";
		this.txtOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		// 
		// lblOrder
		// 
		this.lblOrder.AutoSize = true;
		this.lblOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblOrder.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblOrder.Location = new System.Drawing.Point(3, 287);
		this.lblOrder.Name = "lblOrder";
		this.lblOrder.Size = new System.Drawing.Size(81, 15);
		this.lblOrder.TabIndex = 107;
		this.lblOrder.Text = "Current Order";
		// 
		// txtCargoSpaceFree
		// 
		this.txtCargoSpaceFree.AutoSize = true;
		this.txtCargoSpaceFree.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtCargoSpaceFree.Location = new System.Drawing.Point(173, 514);
		this.txtCargoSpaceFree.Name = "txtCargoSpaceFree";
		this.txtCargoSpaceFree.Size = new System.Drawing.Size(92, 15);
		this.txtCargoSpaceFree.TabIndex = 106;
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
		this.gamePanel2.Location = new System.Drawing.Point(3, 532);
		this.gamePanel2.Name = "gamePanel2";
		this.gamePanel2.Padding = new System.Windows.Forms.Padding(3);
		this.gamePanel2.Size = new System.Drawing.Size(280, 100);
		this.gamePanel2.TabIndex = 104;
		// 
		// lstCargoSummary
		// 
		this.lstCargoSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.lstCargoSummary.BackColor = System.Drawing.Color.Black;
		this.lstCargoSummary.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.lstCargoSummary.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
		this.lstCargoSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lstCargoSummary.ForeColor = System.Drawing.Color.White;
		this.lstCargoSummary.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
		this.lstCargoSummary.HideSelection = false;
		this.lstCargoSummary.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
		this.lstCargoSummary.Location = new System.Drawing.Point(2, 3);
		this.lstCargoSummary.Name = "lstCargoSummary";
		this.lstCargoSummary.Size = new System.Drawing.Size(425, 92);
		this.lstCargoSummary.TabIndex = 23;
		this.lstCargoSummary.TileSize = new System.Drawing.Size(64, 64);
		this.lstCargoSummary.UseCompatibleStateImageBehavior = false;
		this.lstCargoSummary.View = System.Windows.Forms.View.Tile;
		// 
		// columnHeader3
		// 
		this.columnHeader3.Width = 198;
		// 
		// lblCargo
		// 
		this.lblCargo.AutoSize = true;
		this.lblCargo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblCargo.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblCargo.Location = new System.Drawing.Point(5, 514);
		this.lblCargo.Name = "lblCargo";
		this.lblCargo.Size = new System.Drawing.Size(40, 15);
		this.lblCargo.TabIndex = 103;
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
		this.gamePanel1.Location = new System.Drawing.Point(5, 411);
		this.gamePanel1.Name = "gamePanel1";
		this.gamePanel1.Padding = new System.Windows.Forms.Padding(3);
		this.gamePanel1.Size = new System.Drawing.Size(278, 100);
		this.gamePanel1.TabIndex = 102;
		// 
		// lstComponentsSummary
		// 
		this.lstComponentsSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.lstComponentsSummary.BackColor = System.Drawing.Color.Black;
		this.lstComponentsSummary.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.lstComponentsSummary.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
		this.lstComponentsSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lstComponentsSummary.ForeColor = System.Drawing.Color.White;
		this.lstComponentsSummary.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
		this.lstComponentsSummary.HideSelection = false;
		this.lstComponentsSummary.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem4,
            listViewItem5,
            listViewItem6});
		this.lstComponentsSummary.Location = new System.Drawing.Point(4, 3);
		this.lstComponentsSummary.Name = "lstComponentsSummary";
		this.lstComponentsSummary.Size = new System.Drawing.Size(423, 92);
		this.lstComponentsSummary.TabIndex = 23;
		this.lstComponentsSummary.TileSize = new System.Drawing.Size(64, 64);
		this.lstComponentsSummary.UseCompatibleStateImageBehavior = false;
		this.lstComponentsSummary.View = System.Windows.Forms.View.Tile;
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
		this.pnlStats.Location = new System.Drawing.Point(5, 160);
		this.pnlStats.Margin = new System.Windows.Forms.Padding(0);
		this.pnlStats.Name = "pnlStats";
		this.pnlStats.RowCount = 1;
		this.pnlStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
		this.pnlStats.Size = new System.Drawing.Size(281, 123);
		this.pnlStats.TabIndex = 100;
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
		this.tableLayoutPanel1.Size = new System.Drawing.Size(134, 117);
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
		this.progSupplies.ProgressDisplayType = FrEee.UI.WinForms.Controls.ProgressDisplayType.Numeric;
		this.progSupplies.RightText = "";
		this.progSupplies.Size = new System.Drawing.Size(134, 19);
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
		this.progAmmunition.ProgressDisplayType = FrEee.UI.WinForms.Controls.ProgressDisplayType.Numeric;
		this.progAmmunition.RightText = "";
		this.progAmmunition.Size = new System.Drawing.Size(134, 19);
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
		this.progFuel.ProgressDisplayType = FrEee.UI.WinForms.Controls.ProgressDisplayType.Numeric;
		this.progFuel.RightText = "";
		this.progFuel.Size = new System.Drawing.Size(134, 19);
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
		this.tableLayoutPanel2.Location = new System.Drawing.Point(143, 3);
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
		this.tableLayoutPanel2.Size = new System.Drawing.Size(135, 117);
		this.tableLayoutPanel2.TabIndex = 0;
		// 
		// progHull
		// 
		this.progHull.BackColor = System.Drawing.Color.Black;
		this.progHull.BarColor = System.Drawing.Color.Blue;
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
		this.progHull.ProgressDisplayType = FrEee.UI.WinForms.Controls.ProgressDisplayType.Numeric;
		this.progHull.RightText = "";
		this.progHull.Size = new System.Drawing.Size(135, 19);
		this.progHull.TabIndex = 30;
		this.progHull.Value = ((long)(150));
		// 
		// progArmor
		// 
		this.progArmor.BackColor = System.Drawing.Color.Black;
		this.progArmor.BarColor = System.Drawing.Color.Blue;
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
		this.progArmor.ProgressDisplayType = FrEee.UI.WinForms.Controls.ProgressDisplayType.Numeric;
		this.progArmor.RightText = "";
		this.progArmor.Size = new System.Drawing.Size(135, 19);
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
		this.progShields.ProgressDisplayType = FrEee.UI.WinForms.Controls.ProgressDisplayType.Numeric;
		this.progShields.RightText = "";
		this.progShields.Size = new System.Drawing.Size(135, 19);
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
		this.progMovement.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
		this.progMovement.ForeColor = System.Drawing.Color.White;
		this.progMovement.IncrementalProgress = ((long)(0));
		this.progMovement.LeftText = "";
		this.progMovement.Location = new System.Drawing.Point(159, 130);
		this.progMovement.Margin = new System.Windows.Forms.Padding(0);
		this.progMovement.Maximum = ((long)(6));
		this.progMovement.Name = "progMovement";
		this.progMovement.Padding = new System.Windows.Forms.Padding(5);
		this.progMovement.ProgressDisplayType = FrEee.UI.WinForms.Controls.ProgressDisplayType.Numeric;
		this.progMovement.RightText = "";
		this.progMovement.Size = new System.Drawing.Size(103, 19);
		this.progMovement.TabIndex = 98;
		this.progMovement.Value = ((long)(4));
		// 
		// lblMovement
		// 
		this.lblMovement.AutoSize = true;
		this.lblMovement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblMovement.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblMovement.Location = new System.Drawing.Point(141, 115);
		this.lblMovement.Name = "lblMovement";
		this.lblMovement.Size = new System.Drawing.Size(65, 15);
		this.lblMovement.TabIndex = 97;
		this.lblMovement.Text = "Movement";
		// 
		// txtHullSize
		// 
		this.txtHullSize.AutoSize = true;
		this.txtHullSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtHullSize.Location = new System.Drawing.Point(156, 100);
		this.txtHullSize.Name = "txtHullSize";
		this.txtHullSize.Size = new System.Drawing.Size(127, 15);
		this.txtHullSize.TabIndex = 96;
		this.txtHullSize.Text = "Heavy Cruiser (600kT)";
		// 
		// lblHullSize
		// 
		this.lblHullSize.AutoSize = true;
		this.lblHullSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblHullSize.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblHullSize.Location = new System.Drawing.Point(141, 85);
		this.lblHullSize.Name = "lblHullSize";
		this.lblHullSize.Size = new System.Drawing.Size(29, 15);
		this.lblHullSize.TabIndex = 95;
		this.lblHullSize.Text = "Hull";
		// 
		// txtClass
		// 
		this.txtClass.AutoSize = true;
		this.txtClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtClass.Location = new System.Drawing.Point(156, 70);
		this.txtClass.Name = "txtClass";
		this.txtClass.Size = new System.Drawing.Size(71, 15);
		this.txtClass.TabIndex = 94;
		this.txtClass.Text = "Constitution";
		// 
		// lblClass
		// 
		this.lblClass.AutoSize = true;
		this.lblClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblClass.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblClass.Location = new System.Drawing.Point(141, 55);
		this.lblClass.Name = "lblClass";
		this.lblClass.Size = new System.Drawing.Size(37, 15);
		this.lblClass.TabIndex = 93;
		this.lblClass.Text = "Class";
		// 
		// txtRole
		// 
		this.txtRole.AutoSize = true;
		this.txtRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtRole.Location = new System.Drawing.Point(156, 23);
		this.txtRole.Name = "txtRole";
		this.txtRole.Size = new System.Drawing.Size(67, 15);
		this.txtRole.TabIndex = 92;
		this.txtRole.Text = "Attack Ship";
		// 
		// txtName
		// 
		this.txtName.AutoSize = true;
		this.txtName.Location = new System.Drawing.Point(140, 3);
		this.txtName.Name = "txtName";
		this.txtName.Size = new System.Drawing.Size(134, 20);
		this.txtName.TabIndex = 91;
		this.txtName.Text = "Constitution 0001";
		// 
		// picOwnerFlag
		// 
		this.picOwnerFlag.Location = new System.Drawing.Point(3, 3);
		this.picOwnerFlag.Name = "picOwnerFlag";
		this.picOwnerFlag.Size = new System.Drawing.Size(34, 20);
		this.picOwnerFlag.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.picOwnerFlag.TabIndex = 90;
		this.picOwnerFlag.TabStop = false;
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
		this.pageOrders.Size = new System.Drawing.Size(292, 642);
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
		this.chkOnHold.TabIndex = 21;
		this.chkOnHold.Text = "Orders On Hold";
		this.chkOnHold.UseVisualStyleBackColor = true;
		this.chkOnHold.CheckedChanged += new System.EventHandler(this.chkOnHold_CheckedChanged);
		// 
		// chkRepeat
		// 
		this.chkRepeat.AutoSize = true;
		this.chkRepeat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
		this.chkRepeat.Location = new System.Drawing.Point(8, 74);
		this.chkRepeat.Name = "chkRepeat";
		this.chkRepeat.Size = new System.Drawing.Size(106, 19);
		this.chkRepeat.TabIndex = 18;
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
		this.lstOrdersDetail.Size = new System.Drawing.Size(279, 540);
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
		this.pageComps.Size = new System.Drawing.Size(292, 642);
		this.pageComps.TabIndex = 2;
		this.pageComps.Text = "Comps";
		// 
		// txtComponentsFunctionalDetail
		// 
		this.txtComponentsFunctionalDetail.AutoSize = true;
		this.txtComponentsFunctionalDetail.Dock = System.Windows.Forms.DockStyle.Top;
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
		this.lstComponentsDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
		this.lstComponentsDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lstComponentsDetail.ForeColor = System.Drawing.Color.White;
		this.lstComponentsDetail.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
		this.lstComponentsDetail.HideSelection = false;
		this.lstComponentsDetail.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem7,
            listViewItem8,
            listViewItem9});
		this.lstComponentsDetail.Location = new System.Drawing.Point(3, 21);
		this.lstComponentsDetail.Name = "lstComponentsDetail";
		this.lstComponentsDetail.Size = new System.Drawing.Size(286, 590);
		this.lstComponentsDetail.TabIndex = 24;
		this.lstComponentsDetail.UseCompatibleStateImageBehavior = false;
		this.lstComponentsDetail.View = System.Windows.Forms.View.Details;
		this.lstComponentsDetail.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstComponentsDetail_MouseDown);
		// 
		// columnHeader1
		// 
		this.columnHeader1.Width = 288;
		// 
		// pageCargo
		// 
		this.pageCargo.BackColor = System.Drawing.Color.Black;
		this.pageCargo.Controls.Add(this.txtCargoSpaceFreeDetail);
		this.pageCargo.Controls.Add(this.lstCargoDetail);
		this.pageCargo.Location = new System.Drawing.Point(4, 29);
		this.pageCargo.Name = "pageCargo";
		this.pageCargo.Padding = new System.Windows.Forms.Padding(3);
		this.pageCargo.Size = new System.Drawing.Size(292, 642);
		this.pageCargo.TabIndex = 3;
		this.pageCargo.Text = "Cargo";
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
		this.lstCargoDetail.HideSelection = false;
		this.lstCargoDetail.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem10,
            listViewItem11,
            listViewItem12});
		this.lstCargoDetail.Location = new System.Drawing.Point(-1, 21);
		this.lstCargoDetail.Name = "lstCargoDetail";
		this.lstCargoDetail.Size = new System.Drawing.Size(293, 640);
		this.lstCargoDetail.TabIndex = 24;
		this.lstCargoDetail.UseCompatibleStateImageBehavior = false;
		this.lstCargoDetail.View = System.Windows.Forms.View.List;
		// 
		// pageAbility
		// 
		this.pageAbility.BackColor = System.Drawing.Color.Black;
		this.pageAbility.Controls.Add(this.abilityTreeView);
		this.pageAbility.Location = new System.Drawing.Point(4, 29);
		this.pageAbility.Name = "pageAbility";
		this.pageAbility.Padding = new System.Windows.Forms.Padding(3);
		this.pageAbility.Size = new System.Drawing.Size(292, 642);
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
		this.abilityTreeView.Size = new System.Drawing.Size(286, 636);
		this.abilityTreeView.TabIndex = 0;
		// 
		// SpaceVehicleReport
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.Controls.Add(this.gameTabControl1);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "SpaceVehicleReport";
		this.Size = new System.Drawing.Size(300, 675);
		this.gameTabControl1.ResumeLayout(false);
		this.pageDetail.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)(this.picPortrait)).EndInit();
		this.gamePanel2.ResumeLayout(false);
		this.gamePanel1.ResumeLayout(false);
		this.pnlStats.ResumeLayout(false);
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.PerformLayout();
		this.tableLayoutPanel2.ResumeLayout(false);
		this.tableLayoutPanel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)(this.picOwnerFlag)).EndInit();
		this.pageOrders.ResumeLayout(false);
		this.pageOrders.PerformLayout();
		this.pageComps.ResumeLayout(false);
		this.pageComps.PerformLayout();
		this.pageCargo.ResumeLayout(false);
		this.pageCargo.PerformLayout();
		this.pageAbility.ResumeLayout(false);
		this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.TabPage pageDetail;
	private System.Windows.Forms.TabPage pageComps;
	private System.Windows.Forms.TabPage pageCargo;
	private System.Windows.Forms.TabPage pageAbility;
	private System.Windows.Forms.ListView lstComponentsDetail;
	private System.Windows.Forms.ListView lstCargoDetail;
	private System.Windows.Forms.Label txtComponentsFunctionalDetail;
	private System.Windows.Forms.Label txtCargoSpaceFreeDetail;
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
	private System.Windows.Forms.ColumnHeader columnHeader1;
	private System.Windows.Forms.Panel panel1;
	private GamePictureBox picPortrait;
	private System.Windows.Forms.Label txtConstructionTime;
	private System.Windows.Forms.Label lblConstructionTime;
	private System.Windows.Forms.Label txtConstructionItem;
	private System.Windows.Forms.Label lblConstructionItem;
	private System.Windows.Forms.Label txtComponentsFunctional;
	private ResourceDisplay resIncomeMin;
	private System.Windows.Forms.Label lblComponents;
	private ResourceDisplay resIncomeOrg;
	private ResourceDisplay resIncomeRad;
	private System.Windows.Forms.Label lblIncome;
	private System.Windows.Forms.Label txtFleet;
	private System.Windows.Forms.Label lblFleet;
	private System.Windows.Forms.Label txtExperience;
	private System.Windows.Forms.Label lblExperience;
	private System.Windows.Forms.Label txtOrder;
	private System.Windows.Forms.Label lblOrder;
	private System.Windows.Forms.Label txtCargoSpaceFree;
	private GamePanel gamePanel2;
	private System.Windows.Forms.ListView lstCargoSummary;
	private System.Windows.Forms.ColumnHeader columnHeader3;
	private System.Windows.Forms.Label lblCargo;
	private GamePanel gamePanel1;
	private System.Windows.Forms.ListView lstComponentsSummary;
	private System.Windows.Forms.ColumnHeader columnHeader2;
	private System.Windows.Forms.TableLayoutPanel pnlStats;
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
	private GameProgressBar progMovement;
	private System.Windows.Forms.Label lblMovement;
	private System.Windows.Forms.Label txtHullSize;
	private System.Windows.Forms.Label lblHullSize;
	private System.Windows.Forms.Label txtClass;
	private System.Windows.Forms.Label lblClass;
	private System.Windows.Forms.Label txtRole;
	private System.Windows.Forms.Label txtName;
	private System.Windows.Forms.PictureBox picOwnerFlag;
	private System.Windows.Forms.Label txtAge;
	private System.Windows.Forms.CheckBox chkRepeat;
	private System.Windows.Forms.CheckBox chkOnHold;
}
