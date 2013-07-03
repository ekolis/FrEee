using FrEee.WinForms.Controls;

namespace FrEee.WinForms.Forms
{
	partial class GameForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			FrEee.Utility.Resource resource1 = new FrEee.Utility.Resource();
			FrEee.Utility.Resource resource2 = new FrEee.Utility.Resource();
			FrEee.Utility.Resource resource3 = new FrEee.Utility.Resource();
			FrEee.Utility.Resource resource4 = new FrEee.Utility.Resource();
			FrEee.Utility.Resource resource5 = new FrEee.Utility.Resource();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.pnlHeader = new FrEee.WinForms.Controls.GamePanel();
			this.resInt = new FrEee.WinForms.Controls.ResourceDisplay();
			this.resRes = new FrEee.WinForms.Controls.ResourceDisplay();
			this.resRad = new FrEee.WinForms.Controls.ResourceDisplay();
			this.resOrg = new FrEee.WinForms.Controls.ResourceDisplay();
			this.resMin = new FrEee.WinForms.Controls.ResourceDisplay();
			this.progResearch = new FrEee.WinForms.Controls.GameProgressBar();
			this.picEmpireFlag = new System.Windows.Forms.PictureBox();
			this.pnlMainCommands = new FrEee.WinForms.Controls.GamePanel();
			this.btnEndTurn = new FrEee.WinForms.Controls.GameButton();
			this.btnLog = new FrEee.WinForms.Controls.GameButton();
			this.btnQueues = new FrEee.WinForms.Controls.GameButton();
			this.btnShips = new FrEee.WinForms.Controls.GameButton();
			this.btnEmpires = new FrEee.WinForms.Controls.GameButton();
			this.btnPlanets = new FrEee.WinForms.Controls.GameButton();
			this.btnDesigns = new FrEee.WinForms.Controls.GameButton();
			this.btnMenu = new FrEee.WinForms.Controls.GameButton();
			this.pnlSubCommands = new FrEee.WinForms.Controls.GamePanel();
			this.btnClearOrders = new FrEee.WinForms.Controls.GameButton();
			this.btnFleetTransfer = new FrEee.WinForms.Controls.GameButton();
			this.btnTransferCargo = new FrEee.WinForms.Controls.GameButton();
			this.btnConstructionQueue = new FrEee.WinForms.Controls.GameButton();
			this.btnColonize = new FrEee.WinForms.Controls.GameButton();
			this.btnEvade = new FrEee.WinForms.Controls.GameButton();
			this.btnWarp = new FrEee.WinForms.Controls.GameButton();
			this.btnPursue = new FrEee.WinForms.Controls.GameButton();
			this.btnMove = new FrEee.WinForms.Controls.GameButton();
			this.pnlSearch = new FrEee.WinForms.Controls.GamePanel();
			this.searchBox = new FrEee.WinForms.Controls.SearchBox();
			this.pnlSystemTabs = new FrEee.WinForms.Controls.GamePanel();
			this.pnlTabs = new System.Windows.Forms.FlowLayoutPanel();
			this.btnNewTab = new FrEee.WinForms.Controls.GameButton();
			this.pnlDetailReport = new FrEee.WinForms.Controls.GamePanel();
			this.pnlSystemMap = new FrEee.WinForms.Controls.GamePanel();
			this.starSystemView = new FrEee.WinForms.Controls.StarSystemView();
			this.pnlGalaxyMap = new FrEee.WinForms.Controls.GamePanel();
			this.galaxyView = new FrEee.WinForms.Controls.GalaxyView();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.tableLayoutPanel1.SuspendLayout();
			this.pnlHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picEmpireFlag)).BeginInit();
			this.pnlMainCommands.SuspendLayout();
			this.pnlSubCommands.SuspendLayout();
			this.pnlSearch.SuspendLayout();
			this.pnlSystemTabs.SuspendLayout();
			this.pnlTabs.SuspendLayout();
			this.pnlSystemMap.SuspendLayout();
			this.pnlGalaxyMap.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 5;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 194F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 359F));
			this.tableLayoutPanel1.Controls.Add(this.pnlHeader, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.pnlMainCommands, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.pnlSubCommands, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.pnlSearch, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.pnlSystemTabs, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.pnlDetailReport, 3, 1);
			this.tableLayoutPanel1.Controls.Add(this.pnlSystemMap, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.pnlGalaxyMap, 3, 4);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 6;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 308F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1229, 779);
			this.tableLayoutPanel1.TabIndex = 7;
			// 
			// pnlHeader
			// 
			this.pnlHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlHeader.BackColor = System.Drawing.Color.Black;
			this.pnlHeader.BorderColor = System.Drawing.Color.RoyalBlue;
			this.pnlHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tableLayoutPanel1.SetColumnSpan(this.pnlHeader, 5);
			this.pnlHeader.Controls.Add(this.resInt);
			this.pnlHeader.Controls.Add(this.resRes);
			this.pnlHeader.Controls.Add(this.resRad);
			this.pnlHeader.Controls.Add(this.resOrg);
			this.pnlHeader.Controls.Add(this.resMin);
			this.pnlHeader.Controls.Add(this.progResearch);
			this.pnlHeader.Controls.Add(this.picEmpireFlag);
			this.pnlHeader.ForeColor = System.Drawing.Color.White;
			this.pnlHeader.Location = new System.Drawing.Point(2, 2);
			this.pnlHeader.Margin = new System.Windows.Forms.Padding(2);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Padding = new System.Windows.Forms.Padding(3);
			this.pnlHeader.Size = new System.Drawing.Size(1225, 35);
			this.pnlHeader.TabIndex = 0;
			// 
			// resInt
			// 
			this.resInt.Amount = 0;
			this.resInt.BackColor = System.Drawing.Color.Black;
			this.resInt.Change = null;
			this.resInt.ForeColor = System.Drawing.Color.White;
			this.resInt.Location = new System.Drawing.Point(777, 6);
			this.resInt.Margin = new System.Windows.Forms.Padding(0);
			this.resInt.Name = "resInt";
			resource1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			resource1.IsGlobal = false;
			resource1.IsLocal = false;
			resource1.Name = "Intelligence";
			resource1.PictureName = "Resource5";
			this.resInt.Resource = resource1;
			this.resInt.ResourceName = "Intelligence";
			this.resInt.Size = new System.Drawing.Size(88, 20);
			this.resInt.TabIndex = 14;
			// 
			// resRes
			// 
			this.resRes.Amount = 0;
			this.resRes.BackColor = System.Drawing.Color.Black;
			this.resRes.Change = null;
			this.resRes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.resRes.Location = new System.Drawing.Point(689, 6);
			this.resRes.Margin = new System.Windows.Forms.Padding(0);
			this.resRes.Name = "resRes";
			resource2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			resource2.IsGlobal = false;
			resource2.IsLocal = false;
			resource2.Name = "Research";
			resource2.PictureName = "Resource4";
			this.resRes.Resource = resource2;
			this.resRes.ResourceName = "Research";
			this.resRes.Size = new System.Drawing.Size(88, 20);
			this.resRes.TabIndex = 13;
			// 
			// resRad
			// 
			this.resRad.Amount = 0;
			this.resRad.BackColor = System.Drawing.Color.Black;
			this.resRad.Change = null;
			this.resRad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.resRad.Location = new System.Drawing.Point(415, 6);
			this.resRad.Margin = new System.Windows.Forms.Padding(0);
			this.resRad.Name = "resRad";
			resource3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			resource3.IsGlobal = true;
			resource3.IsLocal = false;
			resource3.Name = "Radioactives";
			resource3.PictureName = "Resource3";
			this.resRad.Resource = resource3;
			this.resRad.ResourceName = "Radioactives";
			this.resRad.Size = new System.Drawing.Size(184, 20);
			this.resRad.TabIndex = 12;
			// 
			// resOrg
			// 
			this.resOrg.Amount = 0;
			this.resOrg.BackColor = System.Drawing.Color.Black;
			this.resOrg.Change = null;
			this.resOrg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.resOrg.Location = new System.Drawing.Point(231, 6);
			this.resOrg.Margin = new System.Windows.Forms.Padding(0);
			this.resOrg.Name = "resOrg";
			resource4.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			resource4.IsGlobal = true;
			resource4.IsLocal = false;
			resource4.Name = "Organics";
			resource4.PictureName = "Resource2";
			this.resOrg.Resource = resource4;
			this.resOrg.ResourceName = "Organics";
			this.resOrg.Size = new System.Drawing.Size(184, 20);
			this.resOrg.TabIndex = 11;
			// 
			// resMin
			// 
			this.resMin.Amount = 0;
			this.resMin.BackColor = System.Drawing.Color.Black;
			this.resMin.Change = null;
			this.resMin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.resMin.Location = new System.Drawing.Point(47, 6);
			this.resMin.Margin = new System.Windows.Forms.Padding(0);
			this.resMin.Name = "resMin";
			resource5.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			resource5.IsGlobal = true;
			resource5.IsLocal = false;
			resource5.Name = "Minerals";
			resource5.PictureName = "Resource1";
			this.resMin.Resource = resource5;
			this.resMin.ResourceName = "Minerals";
			this.resMin.Size = new System.Drawing.Size(184, 20);
			this.resMin.TabIndex = 10;
			// 
			// progResearch
			// 
			this.progResearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.progResearch.BackColor = System.Drawing.Color.Black;
			this.progResearch.BarColor = System.Drawing.Color.Magenta;
			this.progResearch.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.progResearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.progResearch.ForeColor = System.Drawing.Color.White;
			this.progResearch.IncrementalProgress = 0;
			this.progResearch.LeftText = "Ice Planet Colonization";
			this.progResearch.Location = new System.Drawing.Point(869, 3);
			this.progResearch.Margin = new System.Windows.Forms.Padding(0);
			this.progResearch.Maximum = 500000;
			this.progResearch.Name = "progResearch";
			this.progResearch.Padding = new System.Windows.Forms.Padding(5);
			this.progResearch.RightText = "0.2 years";
			this.progResearch.Size = new System.Drawing.Size(351, 27);
			this.progResearch.TabIndex = 9;
			this.progResearch.Value = 350000;
			this.progResearch.Click += new System.EventHandler(this.progResearch_Click);
			// 
			// picEmpireFlag
			// 
			this.picEmpireFlag.Location = new System.Drawing.Point(4, 5);
			this.picEmpireFlag.Margin = new System.Windows.Forms.Padding(4);
			this.picEmpireFlag.Name = "picEmpireFlag";
			this.picEmpireFlag.Size = new System.Drawing.Size(38, 21);
			this.picEmpireFlag.TabIndex = 0;
			this.picEmpireFlag.TabStop = false;
			// 
			// pnlMainCommands
			// 
			this.pnlMainCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlMainCommands.BackColor = System.Drawing.Color.Black;
			this.pnlMainCommands.BorderColor = System.Drawing.Color.RoyalBlue;
			this.pnlMainCommands.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tableLayoutPanel1.SetColumnSpan(this.pnlMainCommands, 2);
			this.pnlMainCommands.Controls.Add(this.btnEndTurn);
			this.pnlMainCommands.Controls.Add(this.btnLog);
			this.pnlMainCommands.Controls.Add(this.btnQueues);
			this.pnlMainCommands.Controls.Add(this.btnShips);
			this.pnlMainCommands.Controls.Add(this.btnEmpires);
			this.pnlMainCommands.Controls.Add(this.btnPlanets);
			this.pnlMainCommands.Controls.Add(this.btnDesigns);
			this.pnlMainCommands.Controls.Add(this.btnMenu);
			this.pnlMainCommands.ForeColor = System.Drawing.Color.White;
			this.pnlMainCommands.Location = new System.Drawing.Point(2, 41);
			this.pnlMainCommands.Margin = new System.Windows.Forms.Padding(2);
			this.pnlMainCommands.Name = "pnlMainCommands";
			this.pnlMainCommands.Padding = new System.Windows.Forms.Padding(6);
			this.pnlMainCommands.Size = new System.Drawing.Size(340, 50);
			this.pnlMainCommands.TabIndex = 1;
			// 
			// btnEndTurn
			// 
			this.btnEndTurn.BackColor = System.Drawing.Color.Black;
			this.btnEndTurn.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnEndTurn.Location = new System.Drawing.Point(296, 6);
			this.btnEndTurn.Name = "btnEndTurn";
			this.btnEndTurn.Size = new System.Drawing.Size(36, 36);
			this.btnEndTurn.TabIndex = 7;
			this.toolTip.SetToolTip(this.btnEndTurn, "End Turn");
			this.btnEndTurn.UseVisualStyleBackColor = false;
			this.btnEndTurn.Click += new System.EventHandler(this.btnEndTurn_Click);
			// 
			// btnLog
			// 
			this.btnLog.BackColor = System.Drawing.Color.Black;
			this.btnLog.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnLog.Location = new System.Drawing.Point(256, 6);
			this.btnLog.Name = "btnLog";
			this.btnLog.Size = new System.Drawing.Size(36, 36);
			this.btnLog.TabIndex = 6;
			this.toolTip.SetToolTip(this.btnLog, "Log");
			this.btnLog.UseVisualStyleBackColor = false;
			this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
			// 
			// btnQueues
			// 
			this.btnQueues.BackColor = System.Drawing.Color.Black;
			this.btnQueues.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnQueues.Location = new System.Drawing.Point(214, 6);
			this.btnQueues.Name = "btnQueues";
			this.btnQueues.Size = new System.Drawing.Size(36, 36);
			this.btnQueues.TabIndex = 5;
			this.toolTip.SetToolTip(this.btnQueues, "Construction Queues");
			this.btnQueues.UseVisualStyleBackColor = false;
			this.btnQueues.Click += new System.EventHandler(this.btnQueues_Click);
			// 
			// btnShips
			// 
			this.btnShips.BackColor = System.Drawing.Color.Black;
			this.btnShips.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnShips.Location = new System.Drawing.Point(172, 6);
			this.btnShips.Name = "btnShips";
			this.btnShips.Size = new System.Drawing.Size(36, 36);
			this.btnShips.TabIndex = 4;
			this.toolTip.SetToolTip(this.btnShips, "Ships");
			this.btnShips.UseVisualStyleBackColor = false;
			this.btnShips.Click += new System.EventHandler(this.btnShips_Click);
			// 
			// btnEmpires
			// 
			this.btnEmpires.BackColor = System.Drawing.Color.Black;
			this.btnEmpires.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnEmpires.Location = new System.Drawing.Point(130, 6);
			this.btnEmpires.Name = "btnEmpires";
			this.btnEmpires.Size = new System.Drawing.Size(36, 36);
			this.btnEmpires.TabIndex = 3;
			this.toolTip.SetToolTip(this.btnEmpires, "Empires");
			this.btnEmpires.UseVisualStyleBackColor = false;
			this.btnEmpires.Click += new System.EventHandler(this.btnEmpires_Click);
			// 
			// btnPlanets
			// 
			this.btnPlanets.BackColor = System.Drawing.Color.Black;
			this.btnPlanets.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnPlanets.Location = new System.Drawing.Point(88, 6);
			this.btnPlanets.Name = "btnPlanets";
			this.btnPlanets.Size = new System.Drawing.Size(36, 36);
			this.btnPlanets.TabIndex = 0;
			this.toolTip.SetToolTip(this.btnPlanets, "Planets");
			this.btnPlanets.UseVisualStyleBackColor = false;
			this.btnPlanets.Click += new System.EventHandler(this.btnPlanets_Click);
			// 
			// btnDesigns
			// 
			this.btnDesigns.BackColor = System.Drawing.Color.Black;
			this.btnDesigns.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnDesigns.Location = new System.Drawing.Point(47, 6);
			this.btnDesigns.Name = "btnDesigns";
			this.btnDesigns.Size = new System.Drawing.Size(36, 36);
			this.btnDesigns.TabIndex = 2;
			this.toolTip.SetToolTip(this.btnDesigns, "Designs");
			this.btnDesigns.UseVisualStyleBackColor = false;
			this.btnDesigns.Click += new System.EventHandler(this.btnDesigns_Click);
			// 
			// btnMenu
			// 
			this.btnMenu.BackColor = System.Drawing.Color.Black;
			this.btnMenu.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnMenu.Location = new System.Drawing.Point(5, 6);
			this.btnMenu.Name = "btnMenu";
			this.btnMenu.Size = new System.Drawing.Size(36, 36);
			this.btnMenu.TabIndex = 1;
			this.toolTip.SetToolTip(this.btnMenu, "Menu");
			this.btnMenu.UseVisualStyleBackColor = false;
			this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
			// 
			// pnlSubCommands
			// 
			this.pnlSubCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlSubCommands.BackColor = System.Drawing.Color.Black;
			this.pnlSubCommands.BorderColor = System.Drawing.Color.RoyalBlue;
			this.pnlSubCommands.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tableLayoutPanel1.SetColumnSpan(this.pnlSubCommands, 2);
			this.pnlSubCommands.Controls.Add(this.btnClearOrders);
			this.pnlSubCommands.Controls.Add(this.btnFleetTransfer);
			this.pnlSubCommands.Controls.Add(this.btnTransferCargo);
			this.pnlSubCommands.Controls.Add(this.btnConstructionQueue);
			this.pnlSubCommands.Controls.Add(this.btnColonize);
			this.pnlSubCommands.Controls.Add(this.btnEvade);
			this.pnlSubCommands.Controls.Add(this.btnWarp);
			this.pnlSubCommands.Controls.Add(this.btnPursue);
			this.pnlSubCommands.Controls.Add(this.btnMove);
			this.pnlSubCommands.ForeColor = System.Drawing.Color.White;
			this.pnlSubCommands.Location = new System.Drawing.Point(346, 41);
			this.pnlSubCommands.Margin = new System.Windows.Forms.Padding(2);
			this.pnlSubCommands.Name = "pnlSubCommands";
			this.pnlSubCommands.Padding = new System.Windows.Forms.Padding(4);
			this.pnlSubCommands.Size = new System.Drawing.Size(522, 50);
			this.pnlSubCommands.TabIndex = 2;
			// 
			// btnClearOrders
			// 
			this.btnClearOrders.BackColor = System.Drawing.Color.Black;
			this.btnClearOrders.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnClearOrders.Location = new System.Drawing.Point(476, 4);
			this.btnClearOrders.Name = "btnClearOrders";
			this.btnClearOrders.Size = new System.Drawing.Size(36, 36);
			this.btnClearOrders.TabIndex = 11;
			this.toolTip.SetToolTip(this.btnClearOrders, "(Bksp) Clear Orders");
			this.btnClearOrders.UseVisualStyleBackColor = false;
			this.btnClearOrders.Click += new System.EventHandler(this.btnClearOrders_Click);
			// 
			// btnFleetTransfer
			// 
			this.btnFleetTransfer.BackColor = System.Drawing.Color.Black;
			this.btnFleetTransfer.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnFleetTransfer.Location = new System.Drawing.Point(295, 4);
			this.btnFleetTransfer.Name = "btnFleetTransfer";
			this.btnFleetTransfer.Size = new System.Drawing.Size(36, 36);
			this.btnFleetTransfer.TabIndex = 10;
			this.btnFleetTransfer.Text = "Flt";
			this.toolTip.SetToolTip(this.btnFleetTransfer, "(F) Fleet Transfer");
			this.btnFleetTransfer.UseVisualStyleBackColor = false;
			this.btnFleetTransfer.Click += new System.EventHandler(this.btnFleetTransfer_Click);
			// 
			// btnTransferCargo
			// 
			this.btnTransferCargo.BackColor = System.Drawing.Color.Black;
			this.btnTransferCargo.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnTransferCargo.Location = new System.Drawing.Point(253, 4);
			this.btnTransferCargo.Name = "btnTransferCargo";
			this.btnTransferCargo.Size = new System.Drawing.Size(36, 36);
			this.btnTransferCargo.TabIndex = 9;
			this.btnTransferCargo.Text = "TC";
			this.toolTip.SetToolTip(this.btnTransferCargo, "(T) Transfer Cargo");
			this.btnTransferCargo.UseVisualStyleBackColor = false;
			this.btnTransferCargo.Click += new System.EventHandler(this.btnTransferCargo_Click);
			// 
			// btnConstructionQueue
			// 
			this.btnConstructionQueue.BackColor = System.Drawing.Color.Black;
			this.btnConstructionQueue.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnConstructionQueue.Location = new System.Drawing.Point(211, 4);
			this.btnConstructionQueue.Name = "btnConstructionQueue";
			this.btnConstructionQueue.Size = new System.Drawing.Size(36, 36);
			this.btnConstructionQueue.TabIndex = 8;
			this.btnConstructionQueue.Text = "CQ";
			this.toolTip.SetToolTip(this.btnConstructionQueue, "(Q) Construction Queue");
			this.btnConstructionQueue.UseVisualStyleBackColor = false;
			this.btnConstructionQueue.Click += new System.EventHandler(this.btnConstructionQueue_Click);
			// 
			// btnColonize
			// 
			this.btnColonize.BackColor = System.Drawing.Color.Black;
			this.btnColonize.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnColonize.Location = new System.Drawing.Point(169, 4);
			this.btnColonize.Name = "btnColonize";
			this.btnColonize.Size = new System.Drawing.Size(36, 36);
			this.btnColonize.TabIndex = 7;
			this.btnColonize.Text = "Col";
			this.toolTip.SetToolTip(this.btnColonize, "(C) Colonize");
			this.btnColonize.UseVisualStyleBackColor = false;
			this.btnColonize.Click += new System.EventHandler(this.btnColonize_Click);
			// 
			// btnEvade
			// 
			this.btnEvade.BackColor = System.Drawing.Color.Black;
			this.btnEvade.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnEvade.Location = new System.Drawing.Point(86, 4);
			this.btnEvade.Name = "btnEvade";
			this.btnEvade.Size = new System.Drawing.Size(36, 36);
			this.btnEvade.TabIndex = 6;
			this.btnEvade.Text = "Ev";
			this.toolTip.SetToolTip(this.btnEvade, "(V) Evade");
			this.btnEvade.UseVisualStyleBackColor = false;
			this.btnEvade.Click += new System.EventHandler(this.btnEvade_Click);
			// 
			// btnWarp
			// 
			this.btnWarp.BackColor = System.Drawing.Color.Black;
			this.btnWarp.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnWarp.Location = new System.Drawing.Point(127, 4);
			this.btnWarp.Name = "btnWarp";
			this.btnWarp.Size = new System.Drawing.Size(36, 36);
			this.btnWarp.TabIndex = 5;
			this.toolTip.SetToolTip(this.btnWarp, "(W) Warp");
			this.btnWarp.UseVisualStyleBackColor = false;
			this.btnWarp.Click += new System.EventHandler(this.btnWarp_Click);
			// 
			// btnPursue
			// 
			this.btnPursue.BackColor = System.Drawing.Color.Black;
			this.btnPursue.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnPursue.Location = new System.Drawing.Point(47, 4);
			this.btnPursue.Name = "btnPursue";
			this.btnPursue.Size = new System.Drawing.Size(36, 36);
			this.btnPursue.TabIndex = 4;
			this.btnPursue.Text = "Pur";
			this.toolTip.SetToolTip(this.btnPursue, "(P) Pursue");
			this.btnPursue.UseVisualStyleBackColor = false;
			this.btnPursue.Click += new System.EventHandler(this.btnPursue_Click);
			// 
			// btnMove
			// 
			this.btnMove.BackColor = System.Drawing.Color.Black;
			this.btnMove.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnMove.Location = new System.Drawing.Point(7, 4);
			this.btnMove.Name = "btnMove";
			this.btnMove.Size = new System.Drawing.Size(36, 36);
			this.btnMove.TabIndex = 2;
			this.toolTip.SetToolTip(this.btnMove, "(M) Move");
			this.btnMove.UseVisualStyleBackColor = false;
			this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
			// 
			// pnlSearch
			// 
			this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlSearch.BackColor = System.Drawing.Color.Black;
			this.pnlSearch.BorderColor = System.Drawing.Color.RoyalBlue;
			this.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tableLayoutPanel1.SetColumnSpan(this.pnlSearch, 4);
			this.pnlSearch.Controls.Add(this.searchBox);
			this.pnlSearch.ForeColor = System.Drawing.Color.White;
			this.pnlSearch.Location = new System.Drawing.Point(2, 95);
			this.pnlSearch.Margin = new System.Windows.Forms.Padding(2);
			this.pnlSearch.Name = "pnlSearch";
			this.pnlSearch.Padding = new System.Windows.Forms.Padding(3);
			this.pnlSearch.Size = new System.Drawing.Size(866, 46);
			this.pnlSearch.TabIndex = 2;
			// 
			// searchBox
			// 
			this.searchBox.BackColor = System.Drawing.Color.Black;
			this.searchBox.ForeColor = System.Drawing.Color.White;
			this.searchBox.Location = new System.Drawing.Point(9, 6);
			this.searchBox.Name = "searchBox";
			this.searchBox.ObjectsToSearch = null;
			this.searchBox.ResultsPopupHeight = 128;
			this.searchBox.Size = new System.Drawing.Size(332, 21);
			this.searchBox.StarSystem = null;
			this.searchBox.TabIndex = 0;
			this.searchBox.ObjectSelected += new FrEee.WinForms.Controls.SearchBox.ObjectSelectedDelegate(this.searchBox_ObjectSelected);
			// 
			// pnlSystemTabs
			// 
			this.pnlSystemTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlSystemTabs.BackColor = System.Drawing.Color.Black;
			this.pnlSystemTabs.BorderColor = System.Drawing.Color.RoyalBlue;
			this.pnlSystemTabs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlSystemTabs.Controls.Add(this.pnlTabs);
			this.pnlSystemTabs.ForeColor = System.Drawing.Color.White;
			this.pnlSystemTabs.Location = new System.Drawing.Point(2, 145);
			this.pnlSystemTabs.Margin = new System.Windows.Forms.Padding(2);
			this.pnlSystemTabs.Name = "pnlSystemTabs";
			this.pnlSystemTabs.Padding = new System.Windows.Forms.Padding(3);
			this.tableLayoutPanel1.SetRowSpan(this.pnlSystemTabs, 2);
			this.pnlSystemTabs.Size = new System.Drawing.Size(146, 611);
			this.pnlSystemTabs.TabIndex = 3;
			// 
			// pnlTabs
			// 
			this.pnlTabs.Controls.Add(this.btnNewTab);
			this.pnlTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlTabs.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.pnlTabs.Location = new System.Drawing.Point(3, 3);
			this.pnlTabs.Name = "pnlTabs";
			this.pnlTabs.Size = new System.Drawing.Size(138, 603);
			this.pnlTabs.TabIndex = 0;
			// 
			// btnNewTab
			// 
			this.btnNewTab.BackColor = System.Drawing.Color.Black;
			this.btnNewTab.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnNewTab.Location = new System.Drawing.Point(3, 3);
			this.btnNewTab.Name = "btnNewTab";
			this.btnNewTab.Size = new System.Drawing.Size(128, 23);
			this.btnNewTab.TabIndex = 0;
			this.btnNewTab.Text = "(New Tab)";
			this.btnNewTab.UseVisualStyleBackColor = false;
			this.btnNewTab.Click += new System.EventHandler(this.btnNewTab_Click);
			// 
			// pnlDetailReport
			// 
			this.pnlDetailReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlDetailReport.BackColor = System.Drawing.Color.Black;
			this.pnlDetailReport.BorderColor = System.Drawing.Color.RoyalBlue;
			this.pnlDetailReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlDetailReport.ForeColor = System.Drawing.Color.White;
			this.pnlDetailReport.Location = new System.Drawing.Point(872, 41);
			this.pnlDetailReport.Margin = new System.Windows.Forms.Padding(2);
			this.pnlDetailReport.Name = "pnlDetailReport";
			this.pnlDetailReport.Padding = new System.Windows.Forms.Padding(3);
			this.tableLayoutPanel1.SetRowSpan(this.pnlDetailReport, 3);
			this.pnlDetailReport.Size = new System.Drawing.Size(355, 407);
			this.pnlDetailReport.TabIndex = 3;
			// 
			// pnlSystemMap
			// 
			this.pnlSystemMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlSystemMap.BackColor = System.Drawing.Color.Black;
			this.pnlSystemMap.BorderColor = System.Drawing.Color.RoyalBlue;
			this.pnlSystemMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tableLayoutPanel1.SetColumnSpan(this.pnlSystemMap, 3);
			this.pnlSystemMap.Controls.Add(this.starSystemView);
			this.pnlSystemMap.ForeColor = System.Drawing.Color.White;
			this.pnlSystemMap.Location = new System.Drawing.Point(152, 145);
			this.pnlSystemMap.Margin = new System.Windows.Forms.Padding(2);
			this.pnlSystemMap.Name = "pnlSystemMap";
			this.pnlSystemMap.Padding = new System.Windows.Forms.Padding(3);
			this.tableLayoutPanel1.SetRowSpan(this.pnlSystemMap, 2);
			this.pnlSystemMap.Size = new System.Drawing.Size(716, 611);
			this.pnlSystemMap.TabIndex = 4;
			// 
			// starSystemView
			// 
			this.starSystemView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.starSystemView.BackColor = System.Drawing.Color.Black;
			this.starSystemView.Location = new System.Drawing.Point(6, 6);
			this.starSystemView.Name = "starSystemView";
			this.starSystemView.SelectedSector = null;
			this.starSystemView.SelectedSpaceObject = null;
			this.starSystemView.Size = new System.Drawing.Size(702, 597);
			this.starSystemView.StarSystem = null;
			this.starSystemView.TabIndex = 0;
			this.starSystemView.SectorClicked += new FrEee.WinForms.Controls.StarSystemView.SectorSelectionDelegate(this.starSystemView_SectorClicked);
			this.starSystemView.SectorSelected += new FrEee.WinForms.Controls.StarSystemView.SectorSelectionDelegate(this.starSystemView_SectorSelected);
			// 
			// pnlGalaxyMap
			// 
			this.pnlGalaxyMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlGalaxyMap.BackColor = System.Drawing.Color.Black;
			this.pnlGalaxyMap.BorderColor = System.Drawing.Color.RoyalBlue;
			this.pnlGalaxyMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlGalaxyMap.Controls.Add(this.galaxyView);
			this.pnlGalaxyMap.ForeColor = System.Drawing.Color.White;
			this.pnlGalaxyMap.Location = new System.Drawing.Point(872, 452);
			this.pnlGalaxyMap.Margin = new System.Windows.Forms.Padding(2);
			this.pnlGalaxyMap.Name = "pnlGalaxyMap";
			this.pnlGalaxyMap.Padding = new System.Windows.Forms.Padding(3);
			this.pnlGalaxyMap.Size = new System.Drawing.Size(355, 304);
			this.pnlGalaxyMap.TabIndex = 5;
			// 
			// galaxyView
			// 
			this.galaxyView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.galaxyView.BackColor = System.Drawing.Color.Black;
			this.galaxyView.Location = new System.Drawing.Point(4, 3);
			this.galaxyView.Name = "galaxyView";
			this.galaxyView.SelectedStarSystem = null;
			this.galaxyView.Size = new System.Drawing.Size(346, 292);
			this.galaxyView.TabIndex = 0;
			this.galaxyView.Text = "galaxyView1";
			this.galaxyView.StarSystemClicked += new FrEee.WinForms.Controls.GalaxyView.StarSystemSelectionDelegate(this.galaxyView_StarSystemClicked);
			this.galaxyView.StarSystemSelected += new FrEee.WinForms.Controls.GalaxyView.StarSystemSelectionDelegate(this.galaxyView_StarSystemSelected);
			// 
			// GameForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(1229, 779);
			this.Controls.Add(this.tableLayoutPanel1);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.White;
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MinimumSize = new System.Drawing.Size(900, 700);
			this.Name = "GameForm";
			this.Text = "FrEee";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameForm_FormClosing);
			this.Load += new System.EventHandler(this.GameForm_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyUp);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.pnlHeader.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picEmpireFlag)).EndInit();
			this.pnlMainCommands.ResumeLayout(false);
			this.pnlSubCommands.ResumeLayout(false);
			this.pnlSearch.ResumeLayout(false);
			this.pnlSystemTabs.ResumeLayout(false);
			this.pnlTabs.ResumeLayout(false);
			this.pnlSystemMap.ResumeLayout(false);
			this.pnlGalaxyMap.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private GamePanel pnlHeader;
		private GamePanel pnlSubCommands;
		private GamePanel pnlSearch;
		private GamePanel pnlSystemTabs;
		private GamePanel pnlSystemMap;
		private GamePanel pnlDetailReport;
		private GamePanel pnlGalaxyMap;
		private System.Windows.Forms.PictureBox picEmpireFlag;
		private GameProgressBar progResearch;
		private StarSystemView starSystemView;
		private GalaxyView galaxyView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private GamePanel pnlMainCommands;
		private GameButton btnLog;
		private GameButton btnQueues;
		private GameButton btnShips;
		private GameButton btnEmpires;
		private GameButton btnPlanets;
		private GameButton btnDesigns;
		private GameButton btnMenu;
		private GameButton btnEndTurn;
		private ResourceDisplay resInt;
		private ResourceDisplay resRes;
		private ResourceDisplay resRad;
		private ResourceDisplay resOrg;
		private ResourceDisplay resMin;
		private System.Windows.Forms.FlowLayoutPanel pnlTabs;
		private GameButton btnNewTab;
		private GameButton btnMove;
		private GameButton btnWarp;
		private GameButton btnPursue;
		private GameButton btnEvade;
		private GameButton btnClearOrders;
		private GameButton btnFleetTransfer;
		private GameButton btnTransferCargo;
		private GameButton btnConstructionQueue;
		private GameButton btnColonize;
		private System.Windows.Forms.ToolTip toolTip;
		private SearchBox searchBox;
	}
}

