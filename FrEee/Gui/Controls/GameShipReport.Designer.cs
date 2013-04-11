namespace FrEee.Gui.Controls
{
	partial class GameShipReport
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
			this.gameTabControl1 = new FrEee.Gui.Controls.GameTabControl();
			this.pageDetail = new System.Windows.Forms.TabPage();
			this.gameResourceDisplay1 = new FrEee.Gui.Controls.GameResourceDisplay();
			this.lblMaint = new System.Windows.Forms.Label();
			this.txtFleet = new System.Windows.Forms.Label();
			this.lblFleet = new System.Windows.Forms.Label();
			this.txtExperience = new System.Windows.Forms.Label();
			this.lblExperience = new System.Windows.Forms.Label();
			this.txtOrder = new System.Windows.Forms.Label();
			this.lblOrder = new System.Windows.Forms.Label();
			this.gamePanel2 = new FrEee.Gui.Controls.GamePanel();
			this.lstCargo = new System.Windows.Forms.ListView();
			this.lblCargo = new System.Windows.Forms.Label();
			this.gamePanel1 = new FrEee.Gui.Controls.GamePanel();
			this.lstComponents = new System.Windows.Forms.ListView();
			this.lblComponents = new System.Windows.Forms.Label();
			this.pnlStats = new System.Windows.Forms.TableLayoutPanel();
			this.pnlDamage = new System.Windows.Forms.FlowLayoutPanel();
			this.lblShields = new System.Windows.Forms.Label();
			this.progShields = new FrEee.Gui.Controls.GameProgressBar();
			this.lblArmor = new System.Windows.Forms.Label();
			this.progArmor = new FrEee.Gui.Controls.GameProgressBar();
			this.lblHull = new System.Windows.Forms.Label();
			this.progHull = new FrEee.Gui.Controls.GameProgressBar();
			this.pnlResources = new System.Windows.Forms.FlowLayoutPanel();
			this.lblSupplies = new System.Windows.Forms.Label();
			this.progSupplies = new FrEee.Gui.Controls.GameProgressBar();
			this.lblAmmunition = new System.Windows.Forms.Label();
			this.gameProgressBar2 = new FrEee.Gui.Controls.GameProgressBar();
			this.label3 = new System.Windows.Forms.Label();
			this.progFuel = new FrEee.Gui.Controls.GameProgressBar();
			this.txtMovement = new System.Windows.Forms.Label();
			this.gameProgressBar1 = new FrEee.Gui.Controls.GameProgressBar();
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
			this.pageComps = new System.Windows.Forms.TabPage();
			this.pageCargo = new System.Windows.Forms.TabPage();
			this.pageAbility = new System.Windows.Forms.TabPage();
			this.gameResourceDisplay2 = new FrEee.Gui.Controls.GameResourceDisplay();
			this.gameResourceDisplay3 = new FrEee.Gui.Controls.GameResourceDisplay();
			this.gameTabControl1.SuspendLayout();
			this.pageDetail.SuspendLayout();
			this.gamePanel2.SuspendLayout();
			this.gamePanel1.SuspendLayout();
			this.pnlStats.SuspendLayout();
			this.pnlDamage.SuspendLayout();
			this.pnlResources.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picPortrait)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picOwnerFlag)).BeginInit();
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
			this.gameTabControl1.Size = new System.Drawing.Size(300, 700);
			this.gameTabControl1.TabBackColor = System.Drawing.Color.Black;
			this.gameTabControl1.TabForeColor = System.Drawing.Color.CornflowerBlue;
			this.gameTabControl1.TabIndex = 0;
			// 
			// pageDetail
			// 
			this.pageDetail.BackColor = System.Drawing.Color.Black;
			this.pageDetail.Controls.Add(this.gameResourceDisplay3);
			this.pageDetail.Controls.Add(this.gameResourceDisplay2);
			this.pageDetail.Controls.Add(this.gameResourceDisplay1);
			this.pageDetail.Controls.Add(this.lblMaint);
			this.pageDetail.Controls.Add(this.txtFleet);
			this.pageDetail.Controls.Add(this.lblFleet);
			this.pageDetail.Controls.Add(this.txtExperience);
			this.pageDetail.Controls.Add(this.lblExperience);
			this.pageDetail.Controls.Add(this.txtOrder);
			this.pageDetail.Controls.Add(this.lblOrder);
			this.pageDetail.Controls.Add(this.gamePanel2);
			this.pageDetail.Controls.Add(this.lblCargo);
			this.pageDetail.Controls.Add(this.gamePanel1);
			this.pageDetail.Controls.Add(this.lblComponents);
			this.pageDetail.Controls.Add(this.pnlStats);
			this.pageDetail.Controls.Add(this.txtMovement);
			this.pageDetail.Controls.Add(this.gameProgressBar1);
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
			this.pageDetail.Size = new System.Drawing.Size(292, 667);
			this.pageDetail.TabIndex = 0;
			this.pageDetail.Text = "Detail";
			// 
			// gameResourceDisplay1
			// 
			this.gameResourceDisplay1.Amount = 200;
			this.gameResourceDisplay1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.gameResourceDisplay1.BackColor = System.Drawing.Color.Black;
			this.gameResourceDisplay1.Change = null;
			this.gameResourceDisplay1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gameResourceDisplay1.ForeColor = System.Drawing.Color.Red;
			this.gameResourceDisplay1.Location = new System.Drawing.Point(215, 634);
			this.gameResourceDisplay1.Margin = new System.Windows.Forms.Padding(0);
			this.gameResourceDisplay1.Name = "gameResourceDisplay1";
			this.gameResourceDisplay1.ResourceColor = System.Drawing.Color.Empty;
			this.gameResourceDisplay1.Size = new System.Drawing.Size(68, 20);
			this.gameResourceDisplay1.TabIndex = 32;
			// 
			// lblMaint
			// 
			this.lblMaint.AutoSize = true;
			this.lblMaint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMaint.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblMaint.Location = new System.Drawing.Point(11, 634);
			this.lblMaint.Name = "lblMaint";
			this.lblMaint.Size = new System.Drawing.Size(41, 15);
			this.lblMaint.TabIndex = 31;
			this.lblMaint.Text = "Maint.";
			// 
			// txtFleet
			// 
			this.txtFleet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtFleet.AutoSize = true;
			this.txtFleet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtFleet.Location = new System.Drawing.Point(211, 619);
			this.txtFleet.Name = "txtFleet";
			this.txtFleet.Size = new System.Drawing.Size(68, 15);
			this.txtFleet.TabIndex = 30;
			this.txtFleet.Text = "Alpha Fleet";
			this.txtFleet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblFleet
			// 
			this.lblFleet.AutoSize = true;
			this.lblFleet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFleet.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblFleet.Location = new System.Drawing.Point(11, 619);
			this.lblFleet.Name = "lblFleet";
			this.lblFleet.Size = new System.Drawing.Size(34, 15);
			this.lblFleet.TabIndex = 29;
			this.lblFleet.Text = "Fleet";
			// 
			// txtExperience
			// 
			this.txtExperience.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtExperience.AutoSize = true;
			this.txtExperience.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtExperience.Location = new System.Drawing.Point(210, 604);
			this.txtExperience.Name = "txtExperience";
			this.txtExperience.Size = new System.Drawing.Size(73, 15);
			this.txtExperience.TabIndex = 28;
			this.txtExperience.Text = "Novice (0%)";
			this.txtExperience.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblExperience
			// 
			this.lblExperience.AutoSize = true;
			this.lblExperience.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblExperience.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblExperience.Location = new System.Drawing.Point(11, 604);
			this.lblExperience.Name = "lblExperience";
			this.lblExperience.Size = new System.Drawing.Size(100, 15);
			this.lblExperience.TabIndex = 27;
			this.lblExperience.Text = "Crew Experience";
			// 
			// txtOrder
			// 
			this.txtOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtOrder.AutoSize = true;
			this.txtOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtOrder.Location = new System.Drawing.Point(153, 589);
			this.txtOrder.Name = "txtOrder";
			this.txtOrder.Size = new System.Drawing.Size(130, 15);
			this.txtOrder.TabIndex = 26;
			this.txtOrder.Text = "Move to Kartogia (0, 5)";
			this.txtOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblOrder
			// 
			this.lblOrder.AutoSize = true;
			this.lblOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblOrder.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblOrder.Location = new System.Drawing.Point(11, 589);
			this.lblOrder.Name = "lblOrder";
			this.lblOrder.Size = new System.Drawing.Size(81, 15);
			this.lblOrder.TabIndex = 25;
			this.lblOrder.Text = "Current Order";
			// 
			// gamePanel2
			// 
			this.gamePanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gamePanel2.BackColor = System.Drawing.Color.Black;
			this.gamePanel2.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.gamePanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.gamePanel2.Controls.Add(this.lstCargo);
			this.gamePanel2.ForeColor = System.Drawing.Color.White;
			this.gamePanel2.Location = new System.Drawing.Point(11, 472);
			this.gamePanel2.Name = "gamePanel2";
			this.gamePanel2.Size = new System.Drawing.Size(272, 100);
			this.gamePanel2.TabIndex = 24;
			// 
			// lstCargo
			// 
			this.lstCargo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstCargo.BackColor = System.Drawing.Color.Black;
			this.lstCargo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstCargo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstCargo.ForeColor = System.Drawing.Color.White;
			this.lstCargo.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
			this.lstCargo.Location = new System.Drawing.Point(2, 3);
			this.lstCargo.Name = "lstCargo";
			this.lstCargo.Size = new System.Drawing.Size(265, 92);
			this.lstCargo.TabIndex = 23;
			this.lstCargo.UseCompatibleStateImageBehavior = false;
			// 
			// lblCargo
			// 
			this.lblCargo.AutoSize = true;
			this.lblCargo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCargo.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblCargo.Location = new System.Drawing.Point(8, 454);
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
			this.gamePanel1.Controls.Add(this.lstComponents);
			this.gamePanel1.ForeColor = System.Drawing.Color.White;
			this.gamePanel1.Location = new System.Drawing.Point(11, 340);
			this.gamePanel1.Name = "gamePanel1";
			this.gamePanel1.Size = new System.Drawing.Size(272, 100);
			this.gamePanel1.TabIndex = 23;
			// 
			// lstComponents
			// 
			this.lstComponents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstComponents.BackColor = System.Drawing.Color.Black;
			this.lstComponents.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstComponents.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstComponents.ForeColor = System.Drawing.Color.White;
			this.lstComponents.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem4,
            listViewItem5,
            listViewItem6});
			this.lstComponents.Location = new System.Drawing.Point(2, 3);
			this.lstComponents.Name = "lstComponents";
			this.lstComponents.Size = new System.Drawing.Size(265, 92);
			this.lstComponents.TabIndex = 23;
			this.lstComponents.UseCompatibleStateImageBehavior = false;
			// 
			// lblComponents
			// 
			this.lblComponents.AutoSize = true;
			this.lblComponents.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblComponents.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblComponents.Location = new System.Drawing.Point(8, 322);
			this.lblComponents.Name = "lblComponents";
			this.lblComponents.Size = new System.Drawing.Size(77, 15);
			this.lblComponents.TabIndex = 21;
			this.lblComponents.Text = "Components";
			// 
			// pnlStats
			// 
			this.pnlStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlStats.ColumnCount = 2;
			this.pnlStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.pnlStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.pnlStats.Controls.Add(this.pnlDamage, 1, 0);
			this.pnlStats.Controls.Add(this.pnlResources, 0, 0);
			this.pnlStats.Location = new System.Drawing.Point(8, 167);
			this.pnlStats.Name = "pnlStats";
			this.pnlStats.RowCount = 1;
			this.pnlStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.pnlStats.Size = new System.Drawing.Size(278, 143);
			this.pnlStats.TabIndex = 20;
			// 
			// pnlDamage
			// 
			this.pnlDamage.AutoScroll = true;
			this.pnlDamage.Controls.Add(this.lblShields);
			this.pnlDamage.Controls.Add(this.progShields);
			this.pnlDamage.Controls.Add(this.lblArmor);
			this.pnlDamage.Controls.Add(this.progArmor);
			this.pnlDamage.Controls.Add(this.lblHull);
			this.pnlDamage.Controls.Add(this.progHull);
			this.pnlDamage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlDamage.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.pnlDamage.Location = new System.Drawing.Point(142, 3);
			this.pnlDamage.Name = "pnlDamage";
			this.pnlDamage.Size = new System.Drawing.Size(133, 137);
			this.pnlDamage.TabIndex = 1;
			// 
			// lblShields
			// 
			this.lblShields.AutoSize = true;
			this.lblShields.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblShields.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblShields.Location = new System.Drawing.Point(3, 0);
			this.lblShields.Name = "lblShields";
			this.lblShields.Size = new System.Drawing.Size(48, 15);
			this.lblShields.TabIndex = 19;
			this.lblShields.Text = "Shields";
			// 
			// progShields
			// 
			this.progShields.BackColor = System.Drawing.Color.Black;
			this.progShields.BarColor = System.Drawing.Color.Blue;
			this.progShields.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.progShields.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.progShields.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.progShields.ForeColor = System.Drawing.Color.White;
			this.progShields.LeftText = "";
			this.progShields.Location = new System.Drawing.Point(0, 15);
			this.progShields.Margin = new System.Windows.Forms.Padding(0);
			this.progShields.Maximum = 200;
			this.progShields.Name = "progShields";
			this.progShields.RightText = "";
			this.progShields.Size = new System.Drawing.Size(127, 28);
			this.progShields.TabIndex = 18;
			this.progShields.Value = 200;
			// 
			// lblArmor
			// 
			this.lblArmor.AutoSize = true;
			this.lblArmor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblArmor.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblArmor.Location = new System.Drawing.Point(3, 43);
			this.lblArmor.Name = "lblArmor";
			this.lblArmor.Size = new System.Drawing.Size(40, 15);
			this.lblArmor.TabIndex = 21;
			this.lblArmor.Text = "Armor";
			// 
			// progArmor
			// 
			this.progArmor.BackColor = System.Drawing.Color.Black;
			this.progArmor.BarColor = System.Drawing.Color.Blue;
			this.progArmor.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.progArmor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.progArmor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.progArmor.ForeColor = System.Drawing.Color.White;
			this.progArmor.LeftText = "";
			this.progArmor.Location = new System.Drawing.Point(0, 58);
			this.progArmor.Margin = new System.Windows.Forms.Padding(0);
			this.progArmor.Maximum = 150;
			this.progArmor.Name = "progArmor";
			this.progArmor.RightText = "";
			this.progArmor.Size = new System.Drawing.Size(127, 28);
			this.progArmor.TabIndex = 20;
			this.progArmor.Value = 75;
			// 
			// lblHull
			// 
			this.lblHull.AutoSize = true;
			this.lblHull.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHull.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblHull.Location = new System.Drawing.Point(3, 86);
			this.lblHull.Name = "lblHull";
			this.lblHull.Size = new System.Drawing.Size(29, 15);
			this.lblHull.TabIndex = 23;
			this.lblHull.Text = "Hull";
			// 
			// progHull
			// 
			this.progHull.BackColor = System.Drawing.Color.Black;
			this.progHull.BarColor = System.Drawing.Color.Blue;
			this.progHull.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.progHull.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.progHull.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.progHull.ForeColor = System.Drawing.Color.White;
			this.progHull.LeftText = "";
			this.progHull.Location = new System.Drawing.Point(0, 101);
			this.progHull.Margin = new System.Windows.Forms.Padding(0);
			this.progHull.Maximum = 300;
			this.progHull.Name = "progHull";
			this.progHull.RightText = "";
			this.progHull.Size = new System.Drawing.Size(127, 28);
			this.progHull.TabIndex = 22;
			this.progHull.Value = 200;
			// 
			// pnlResources
			// 
			this.pnlResources.AutoScroll = true;
			this.pnlResources.Controls.Add(this.lblSupplies);
			this.pnlResources.Controls.Add(this.progSupplies);
			this.pnlResources.Controls.Add(this.lblAmmunition);
			this.pnlResources.Controls.Add(this.gameProgressBar2);
			this.pnlResources.Controls.Add(this.label3);
			this.pnlResources.Controls.Add(this.progFuel);
			this.pnlResources.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlResources.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.pnlResources.Location = new System.Drawing.Point(3, 3);
			this.pnlResources.Name = "pnlResources";
			this.pnlResources.Size = new System.Drawing.Size(133, 137);
			this.pnlResources.TabIndex = 0;
			// 
			// lblSupplies
			// 
			this.lblSupplies.AutoSize = true;
			this.lblSupplies.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSupplies.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblSupplies.Location = new System.Drawing.Point(3, 0);
			this.lblSupplies.Name = "lblSupplies";
			this.lblSupplies.Size = new System.Drawing.Size(55, 15);
			this.lblSupplies.TabIndex = 13;
			this.lblSupplies.Text = "Supplies";
			// 
			// progSupplies
			// 
			this.progSupplies.BackColor = System.Drawing.Color.Black;
			this.progSupplies.BarColor = System.Drawing.Color.Blue;
			this.progSupplies.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.progSupplies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.progSupplies.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.progSupplies.ForeColor = System.Drawing.Color.White;
			this.progSupplies.LeftText = "";
			this.progSupplies.Location = new System.Drawing.Point(0, 15);
			this.progSupplies.Margin = new System.Windows.Forms.Padding(0);
			this.progSupplies.Maximum = 3000;
			this.progSupplies.Name = "progSupplies";
			this.progSupplies.RightText = "";
			this.progSupplies.Size = new System.Drawing.Size(127, 28);
			this.progSupplies.TabIndex = 0;
			this.progSupplies.Value = 2500;
			// 
			// lblAmmunition
			// 
			this.lblAmmunition.AutoSize = true;
			this.lblAmmunition.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblAmmunition.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblAmmunition.Location = new System.Drawing.Point(3, 43);
			this.lblAmmunition.Name = "lblAmmunition";
			this.lblAmmunition.Size = new System.Drawing.Size(73, 15);
			this.lblAmmunition.TabIndex = 15;
			this.lblAmmunition.Text = "Ammunition";
			// 
			// gameProgressBar2
			// 
			this.gameProgressBar2.BackColor = System.Drawing.Color.Black;
			this.gameProgressBar2.BarColor = System.Drawing.Color.Blue;
			this.gameProgressBar2.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.gameProgressBar2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.gameProgressBar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gameProgressBar2.ForeColor = System.Drawing.Color.White;
			this.gameProgressBar2.LeftText = "";
			this.gameProgressBar2.Location = new System.Drawing.Point(0, 58);
			this.gameProgressBar2.Margin = new System.Windows.Forms.Padding(0);
			this.gameProgressBar2.Maximum = 500;
			this.gameProgressBar2.Name = "gameProgressBar2";
			this.gameProgressBar2.RightText = "";
			this.gameProgressBar2.Size = new System.Drawing.Size(127, 28);
			this.gameProgressBar2.TabIndex = 14;
			this.gameProgressBar2.Value = 100;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label3.Location = new System.Drawing.Point(3, 86);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(31, 15);
			this.label3.TabIndex = 17;
			this.label3.Text = "Fuel";
			// 
			// progFuel
			// 
			this.progFuel.BackColor = System.Drawing.Color.Black;
			this.progFuel.BarColor = System.Drawing.Color.Blue;
			this.progFuel.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.progFuel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.progFuel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.progFuel.ForeColor = System.Drawing.Color.White;
			this.progFuel.LeftText = "";
			this.progFuel.Location = new System.Drawing.Point(0, 101);
			this.progFuel.Margin = new System.Windows.Forms.Padding(0);
			this.progFuel.Maximum = 2000;
			this.progFuel.Name = "progFuel";
			this.progFuel.RightText = "";
			this.progFuel.Size = new System.Drawing.Size(127, 28);
			this.progFuel.TabIndex = 16;
			this.progFuel.Value = 1000;
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
			// gameProgressBar1
			// 
			this.gameProgressBar1.BackColor = System.Drawing.Color.Black;
			this.gameProgressBar1.BarColor = System.Drawing.Color.Blue;
			this.gameProgressBar1.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.gameProgressBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.gameProgressBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gameProgressBar1.ForeColor = System.Drawing.Color.White;
			this.gameProgressBar1.LeftText = "";
			this.gameProgressBar1.Location = new System.Drawing.Point(212, 132);
			this.gameProgressBar1.Margin = new System.Windows.Forms.Padding(0);
			this.gameProgressBar1.Maximum = 6;
			this.gameProgressBar1.Name = "gameProgressBar1";
			this.gameProgressBar1.RightText = "";
			this.gameProgressBar1.Size = new System.Drawing.Size(47, 15);
			this.gameProgressBar1.TabIndex = 18;
			this.gameProgressBar1.Value = 4;
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
			this.txtName.Size = new System.Drawing.Size(134, 20);
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
			this.pageOrders.Location = new System.Drawing.Point(4, 29);
			this.pageOrders.Name = "pageOrders";
			this.pageOrders.Padding = new System.Windows.Forms.Padding(3);
			this.pageOrders.Size = new System.Drawing.Size(292, 667);
			this.pageOrders.TabIndex = 1;
			this.pageOrders.Text = "Orders";
			// 
			// pageComps
			// 
			this.pageComps.BackColor = System.Drawing.Color.Black;
			this.pageComps.Location = new System.Drawing.Point(4, 29);
			this.pageComps.Name = "pageComps";
			this.pageComps.Padding = new System.Windows.Forms.Padding(3);
			this.pageComps.Size = new System.Drawing.Size(292, 667);
			this.pageComps.TabIndex = 2;
			this.pageComps.Text = "Comps";
			// 
			// pageCargo
			// 
			this.pageCargo.BackColor = System.Drawing.Color.Black;
			this.pageCargo.Location = new System.Drawing.Point(4, 29);
			this.pageCargo.Name = "pageCargo";
			this.pageCargo.Padding = new System.Windows.Forms.Padding(3);
			this.pageCargo.Size = new System.Drawing.Size(292, 667);
			this.pageCargo.TabIndex = 3;
			this.pageCargo.Text = "Cargo";
			// 
			// pageAbility
			// 
			this.pageAbility.BackColor = System.Drawing.Color.Black;
			this.pageAbility.Location = new System.Drawing.Point(4, 29);
			this.pageAbility.Name = "pageAbility";
			this.pageAbility.Padding = new System.Windows.Forms.Padding(3);
			this.pageAbility.Size = new System.Drawing.Size(292, 667);
			this.pageAbility.TabIndex = 4;
			this.pageAbility.Text = "Ability";
			// 
			// gameResourceDisplay2
			// 
			this.gameResourceDisplay2.Amount = 100;
			this.gameResourceDisplay2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.gameResourceDisplay2.BackColor = System.Drawing.Color.Black;
			this.gameResourceDisplay2.Change = null;
			this.gameResourceDisplay2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gameResourceDisplay2.ForeColor = System.Drawing.Color.Lime;
			this.gameResourceDisplay2.Location = new System.Drawing.Point(140, 634);
			this.gameResourceDisplay2.Margin = new System.Windows.Forms.Padding(0);
			this.gameResourceDisplay2.Name = "gameResourceDisplay2";
			this.gameResourceDisplay2.ResourceColor = System.Drawing.Color.Empty;
			this.gameResourceDisplay2.Size = new System.Drawing.Size(68, 20);
			this.gameResourceDisplay2.TabIndex = 33;
			// 
			// gameResourceDisplay3
			// 
			this.gameResourceDisplay3.Amount = 500;
			this.gameResourceDisplay3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.gameResourceDisplay3.BackColor = System.Drawing.Color.Black;
			this.gameResourceDisplay3.Change = null;
			this.gameResourceDisplay3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gameResourceDisplay3.ForeColor = System.Drawing.Color.Blue;
			this.gameResourceDisplay3.Location = new System.Drawing.Point(65, 634);
			this.gameResourceDisplay3.Margin = new System.Windows.Forms.Padding(0);
			this.gameResourceDisplay3.Name = "gameResourceDisplay3";
			this.gameResourceDisplay3.ResourceColor = System.Drawing.Color.Empty;
			this.gameResourceDisplay3.Size = new System.Drawing.Size(68, 20);
			this.gameResourceDisplay3.TabIndex = 34;
			// 
			// GameShipReport
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.Controls.Add(this.gameTabControl1);
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "GameShipReport";
			this.Size = new System.Drawing.Size(300, 700);
			this.gameTabControl1.ResumeLayout(false);
			this.pageDetail.ResumeLayout(false);
			this.pageDetail.PerformLayout();
			this.gamePanel2.ResumeLayout(false);
			this.gamePanel1.ResumeLayout(false);
			this.pnlStats.ResumeLayout(false);
			this.pnlDamage.ResumeLayout(false);
			this.pnlDamage.PerformLayout();
			this.pnlResources.ResumeLayout(false);
			this.pnlResources.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picPortrait)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picOwnerFlag)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private GameTabControl gameTabControl1;
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
		private GameProgressBar gameProgressBar1;
		private System.Windows.Forms.Label txtMovement;
		private System.Windows.Forms.TableLayoutPanel pnlStats;
		private System.Windows.Forms.FlowLayoutPanel pnlDamage;
		private System.Windows.Forms.FlowLayoutPanel pnlResources;
		private GameProgressBar progSupplies;
		private System.Windows.Forms.Label lblSupplies;
		private System.Windows.Forms.Label lblAmmunition;
		private GameProgressBar gameProgressBar2;
		private System.Windows.Forms.Label label3;
		private GameProgressBar progFuel;
		private System.Windows.Forms.Label lblShields;
		private GameProgressBar progShields;
		private System.Windows.Forms.Label lblArmor;
		private GameProgressBar progArmor;
		private System.Windows.Forms.Label lblHull;
		private GameProgressBar progHull;
		private System.Windows.Forms.Label lblComponents;
		private GamePanel gamePanel1;
		private System.Windows.Forms.ListView lstComponents;
		private System.Windows.Forms.Label txtOrder;
		private System.Windows.Forms.Label lblOrder;
		private GamePanel gamePanel2;
		private System.Windows.Forms.ListView lstCargo;
		private System.Windows.Forms.Label lblCargo;
		private System.Windows.Forms.Label txtExperience;
		private System.Windows.Forms.Label lblExperience;
		private System.Windows.Forms.Label txtFleet;
		private System.Windows.Forms.Label lblFleet;
		private System.Windows.Forms.Label lblMaint;
		private GameResourceDisplay gameResourceDisplay1;
		private GameResourceDisplay gameResourceDisplay3;
		private GameResourceDisplay gameResourceDisplay2;
	}
}
