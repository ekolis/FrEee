namespace FrEee.Gui
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
			this.pnlSystemMap = new FrEee.Gui.Controls.GamePanel();
			this.starSystemView = new FrEee.Gui.Controls.StarSystemView();
			this.pnlGalaxyMap = new FrEee.Gui.Controls.GamePanel();
			this.galaxyView = new FrEee.Gui.Controls.GalaxyView();
			this.pnlDetailReport = new FrEee.Gui.Controls.GamePanel();
			this.gameShipReport1 = new FrEee.Gui.Controls.ShipReport();
			this.pnlSystemTabs = new FrEee.Gui.Controls.GamePanel();
			this.pnlSearch = new FrEee.Gui.Controls.GamePanel();
			this.pnlSubCommands = new FrEee.Gui.Controls.GamePanel();
			this.btnPlanets = new FrEee.Gui.Controls.GameButton();
			this.pnlMainCommands = new FrEee.Gui.Controls.GamePanel();
			this.pnlHeader = new FrEee.Gui.Controls.GamePanel();
			this.gameProgressBar1 = new FrEee.Gui.Controls.GameProgressBar();
			this.pagResources = new FrEee.Gui.Controls.Pager();
			this.txtGameDate = new System.Windows.Forms.Label();
			this.lblGameDate = new System.Windows.Forms.Label();
			this.picEmpireFlag = new System.Windows.Forms.PictureBox();
			this.btnMenu = new FrEee.Gui.Controls.GameButton();
			this.btnDesigns = new FrEee.Gui.Controls.GameButton();
			this.btnEmpires = new FrEee.Gui.Controls.GameButton();
			this.btnShips = new FrEee.Gui.Controls.GameButton();
			this.btnQueues = new FrEee.Gui.Controls.GameButton();
			this.btnLog = new FrEee.Gui.Controls.GameButton();
			this.pnlSystemMap.SuspendLayout();
			this.pnlGalaxyMap.SuspendLayout();
			this.pnlDetailReport.SuspendLayout();
			this.pnlMainCommands.SuspendLayout();
			this.pnlHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picEmpireFlag)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlSystemMap
			// 
			this.pnlSystemMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlSystemMap.BackColor = System.Drawing.Color.Black;
			this.pnlSystemMap.BorderColor = System.Drawing.Color.RoyalBlue;
			this.pnlSystemMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlSystemMap.Controls.Add(this.starSystemView);
			this.pnlSystemMap.ForeColor = System.Drawing.Color.White;
			this.pnlSystemMap.Location = new System.Drawing.Point(15, 159);
			this.pnlSystemMap.Margin = new System.Windows.Forms.Padding(4);
			this.pnlSystemMap.Name = "pnlSystemMap";
			this.pnlSystemMap.Size = new System.Drawing.Size(612, 557);
			this.pnlSystemMap.TabIndex = 4;
			// 
			// starSystemView
			// 
			this.starSystemView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.starSystemView.BackColor = System.Drawing.Color.Black;
			this.starSystemView.Location = new System.Drawing.Point(4, 3);
			this.starSystemView.Name = "starSystemView";
			this.starSystemView.SelectedSector = null;
			this.starSystemView.Size = new System.Drawing.Size(603, 549);
			this.starSystemView.StarSystem = null;
			this.starSystemView.TabIndex = 0;
			this.starSystemView.SectorClicked += new FrEee.Gui.Controls.StarSystemView.SectorSelectionDelegate(this.starSystemView_SectorClicked);
			this.starSystemView.SectorSelected += new FrEee.Gui.Controls.StarSystemView.SectorSelectionDelegate(this.starSystemView_SectorSelected);
			// 
			// pnlGalaxyMap
			// 
			this.pnlGalaxyMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlGalaxyMap.BackColor = System.Drawing.Color.Black;
			this.pnlGalaxyMap.BorderColor = System.Drawing.Color.RoyalBlue;
			this.pnlGalaxyMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlGalaxyMap.Controls.Add(this.galaxyView);
			this.pnlGalaxyMap.ForeColor = System.Drawing.Color.White;
			this.pnlGalaxyMap.Location = new System.Drawing.Point(635, 522);
			this.pnlGalaxyMap.Margin = new System.Windows.Forms.Padding(4);
			this.pnlGalaxyMap.Name = "pnlGalaxyMap";
			this.pnlGalaxyMap.Size = new System.Drawing.Size(360, 194);
			this.pnlGalaxyMap.TabIndex = 5;
			// 
			// galaxyView
			// 
			this.galaxyView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.galaxyView.BackColor = System.Drawing.Color.Black;
			this.galaxyView.Galaxy = null;
			this.galaxyView.Location = new System.Drawing.Point(4, 3);
			this.galaxyView.Name = "galaxyView";
			this.galaxyView.SelectedStarSystem = null;
			this.galaxyView.Size = new System.Drawing.Size(351, 186);
			this.galaxyView.TabIndex = 0;
			this.galaxyView.Text = "galaxyView1";
			this.galaxyView.StarSystemClicked += new FrEee.Gui.Controls.GalaxyView.StarSystemSelectionDelegate(this.galaxyView_StarSystemClicked);
			this.galaxyView.StarSystemSelected += new FrEee.Gui.Controls.GalaxyView.StarSystemSelectionDelegate(this.galaxyView_StarSystemSelected);
			// 
			// pnlDetailReport
			// 
			this.pnlDetailReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlDetailReport.BackColor = System.Drawing.Color.Black;
			this.pnlDetailReport.BorderColor = System.Drawing.Color.RoyalBlue;
			this.pnlDetailReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlDetailReport.Controls.Add(this.gameShipReport1);
			this.pnlDetailReport.ForeColor = System.Drawing.Color.White;
			this.pnlDetailReport.Location = new System.Drawing.Point(635, 54);
			this.pnlDetailReport.Margin = new System.Windows.Forms.Padding(4);
			this.pnlDetailReport.Name = "pnlDetailReport";
			this.pnlDetailReport.Size = new System.Drawing.Size(360, 460);
			this.pnlDetailReport.TabIndex = 3;
			// 
			// gameShipReport1
			// 
			this.gameShipReport1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gameShipReport1.BackColor = System.Drawing.Color.Black;
			this.gameShipReport1.ForeColor = System.Drawing.Color.White;
			this.gameShipReport1.Location = new System.Drawing.Point(4, 4);
			this.gameShipReport1.Name = "gameShipReport1";
			this.gameShipReport1.Size = new System.Drawing.Size(351, 451);
			this.gameShipReport1.TabIndex = 0;
			// 
			// pnlSystemTabs
			// 
			this.pnlSystemTabs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlSystemTabs.BackColor = System.Drawing.Color.Black;
			this.pnlSystemTabs.BorderColor = System.Drawing.Color.RoyalBlue;
			this.pnlSystemTabs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlSystemTabs.ForeColor = System.Drawing.Color.White;
			this.pnlSystemTabs.Location = new System.Drawing.Point(320, 108);
			this.pnlSystemTabs.Margin = new System.Windows.Forms.Padding(4);
			this.pnlSystemTabs.Name = "pnlSystemTabs";
			this.pnlSystemTabs.Size = new System.Drawing.Size(307, 46);
			this.pnlSystemTabs.TabIndex = 3;
			// 
			// pnlSearch
			// 
			this.pnlSearch.BackColor = System.Drawing.Color.Black;
			this.pnlSearch.BorderColor = System.Drawing.Color.RoyalBlue;
			this.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlSearch.ForeColor = System.Drawing.Color.White;
			this.pnlSearch.Location = new System.Drawing.Point(15, 108);
			this.pnlSearch.Margin = new System.Windows.Forms.Padding(4);
			this.pnlSearch.Name = "pnlSearch";
			this.pnlSearch.Size = new System.Drawing.Size(297, 46);
			this.pnlSearch.TabIndex = 2;
			// 
			// pnlSubCommands
			// 
			this.pnlSubCommands.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlSubCommands.BackColor = System.Drawing.Color.Black;
			this.pnlSubCommands.BorderColor = System.Drawing.Color.RoyalBlue;
			this.pnlSubCommands.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlSubCommands.ForeColor = System.Drawing.Color.White;
			this.pnlSubCommands.Location = new System.Drawing.Point(320, 54);
			this.pnlSubCommands.Margin = new System.Windows.Forms.Padding(4);
			this.pnlSubCommands.Name = "pnlSubCommands";
			this.pnlSubCommands.Size = new System.Drawing.Size(307, 46);
			this.pnlSubCommands.TabIndex = 2;
			// 
			// btnPlanets
			// 
			this.btnPlanets.BackColor = System.Drawing.Color.Black;
			this.btnPlanets.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnPlanets.Location = new System.Drawing.Point(87, 4);
			this.btnPlanets.Name = "btnPlanets";
			this.btnPlanets.Size = new System.Drawing.Size(36, 36);
			this.btnPlanets.TabIndex = 0;
			this.btnPlanets.Text = "Planets";
			this.btnPlanets.UseVisualStyleBackColor = false;
			this.btnPlanets.Click += new System.EventHandler(this.btnPlanets_Click);
			// 
			// pnlMainCommands
			// 
			this.pnlMainCommands.BackColor = System.Drawing.Color.Black;
			this.pnlMainCommands.BorderColor = System.Drawing.Color.RoyalBlue;
			this.pnlMainCommands.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlMainCommands.Controls.Add(this.btnLog);
			this.pnlMainCommands.Controls.Add(this.btnQueues);
			this.pnlMainCommands.Controls.Add(this.btnShips);
			this.pnlMainCommands.Controls.Add(this.btnEmpires);
			this.pnlMainCommands.Controls.Add(this.btnPlanets);
			this.pnlMainCommands.Controls.Add(this.btnDesigns);
			this.pnlMainCommands.Controls.Add(this.btnMenu);
			this.pnlMainCommands.ForeColor = System.Drawing.Color.White;
			this.pnlMainCommands.Location = new System.Drawing.Point(15, 54);
			this.pnlMainCommands.Margin = new System.Windows.Forms.Padding(4);
			this.pnlMainCommands.Name = "pnlMainCommands";
			this.pnlMainCommands.Padding = new System.Windows.Forms.Padding(4);
			this.pnlMainCommands.Size = new System.Drawing.Size(297, 46);
			this.pnlMainCommands.TabIndex = 1;
			// 
			// pnlHeader
			// 
			this.pnlHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlHeader.BackColor = System.Drawing.Color.Black;
			this.pnlHeader.BorderColor = System.Drawing.Color.RoyalBlue;
			this.pnlHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlHeader.Controls.Add(this.gameProgressBar1);
			this.pnlHeader.Controls.Add(this.pagResources);
			this.pnlHeader.Controls.Add(this.txtGameDate);
			this.pnlHeader.Controls.Add(this.lblGameDate);
			this.pnlHeader.Controls.Add(this.picEmpireFlag);
			this.pnlHeader.ForeColor = System.Drawing.Color.White;
			this.pnlHeader.Location = new System.Drawing.Point(15, 15);
			this.pnlHeader.Margin = new System.Windows.Forms.Padding(4);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(980, 32);
			this.pnlHeader.TabIndex = 0;
			// 
			// gameProgressBar1
			// 
			this.gameProgressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gameProgressBar1.BackColor = System.Drawing.Color.Black;
			this.gameProgressBar1.BarColor = System.Drawing.Color.Magenta;
			this.gameProgressBar1.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.gameProgressBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.gameProgressBar1.ForeColor = System.Drawing.Color.White;
			this.gameProgressBar1.LeftText = "Ice Planet Colonization";
			this.gameProgressBar1.Location = new System.Drawing.Point(603, 4);
			this.gameProgressBar1.Margin = new System.Windows.Forms.Padding(0);
			this.gameProgressBar1.Maximum = 500000;
			this.gameProgressBar1.Name = "gameProgressBar1";
			this.gameProgressBar1.RightText = "0.2 years";
			this.gameProgressBar1.Size = new System.Drawing.Size(372, 22);
			this.gameProgressBar1.TabIndex = 9;
			this.gameProgressBar1.Value = 350000;
			// 
			// pagResources
			// 
			this.pagResources.BackColor = System.Drawing.Color.Black;
			this.pagResources.Content = null;
			this.pagResources.CurrentPage = 0;
			this.pagResources.ForeColor = System.Drawing.Color.White;
			this.pagResources.Location = new System.Drawing.Point(186, 5);
			this.pagResources.Margin = new System.Windows.Forms.Padding(0);
			this.pagResources.Name = "pagResources";
			this.pagResources.ShowPager = false;
			this.pagResources.Size = new System.Drawing.Size(417, 21);
			this.pagResources.TabIndex = 7;
			// 
			// txtGameDate
			// 
			this.txtGameDate.Location = new System.Drawing.Point(131, 5);
			this.txtGameDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.txtGameDate.Name = "txtGameDate";
			this.txtGameDate.Size = new System.Drawing.Size(51, 21);
			this.txtGameDate.TabIndex = 4;
			this.txtGameDate.Text = "2400.6";
			this.txtGameDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblGameDate
			// 
			this.lblGameDate.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblGameDate.Location = new System.Drawing.Point(50, 5);
			this.lblGameDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblGameDate.Name = "lblGameDate";
			this.lblGameDate.Size = new System.Drawing.Size(73, 21);
			this.lblGameDate.TabIndex = 3;
			this.lblGameDate.Text = "Game Date";
			this.lblGameDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			// btnMenu
			// 
			this.btnMenu.BackColor = System.Drawing.Color.Black;
			this.btnMenu.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnMenu.Location = new System.Drawing.Point(4, 4);
			this.btnMenu.Name = "btnMenu";
			this.btnMenu.Size = new System.Drawing.Size(36, 36);
			this.btnMenu.TabIndex = 1;
			this.btnMenu.Text = "Menu";
			this.btnMenu.UseVisualStyleBackColor = false;
			// 
			// btnDesigns
			// 
			this.btnDesigns.BackColor = System.Drawing.Color.Black;
			this.btnDesigns.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnDesigns.Location = new System.Drawing.Point(46, 4);
			this.btnDesigns.Name = "btnDesigns";
			this.btnDesigns.Size = new System.Drawing.Size(36, 36);
			this.btnDesigns.TabIndex = 2;
			this.btnDesigns.Text = "Designs";
			this.btnDesigns.UseVisualStyleBackColor = false;
			// 
			// btnEmpires
			// 
			this.btnEmpires.BackColor = System.Drawing.Color.Black;
			this.btnEmpires.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnEmpires.Location = new System.Drawing.Point(129, 4);
			this.btnEmpires.Name = "btnEmpires";
			this.btnEmpires.Size = new System.Drawing.Size(36, 36);
			this.btnEmpires.TabIndex = 3;
			this.btnEmpires.Text = "Empires";
			this.btnEmpires.UseVisualStyleBackColor = false;
			// 
			// btnShips
			// 
			this.btnShips.BackColor = System.Drawing.Color.Black;
			this.btnShips.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnShips.Location = new System.Drawing.Point(171, 4);
			this.btnShips.Name = "btnShips";
			this.btnShips.Size = new System.Drawing.Size(36, 36);
			this.btnShips.TabIndex = 4;
			this.btnShips.Text = "Ships";
			this.btnShips.UseVisualStyleBackColor = false;
			// 
			// btnQueues
			// 
			this.btnQueues.BackColor = System.Drawing.Color.Black;
			this.btnQueues.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnQueues.Location = new System.Drawing.Point(213, 4);
			this.btnQueues.Name = "btnQueues";
			this.btnQueues.Size = new System.Drawing.Size(36, 36);
			this.btnQueues.TabIndex = 5;
			this.btnQueues.Text = "Queues";
			this.btnQueues.UseVisualStyleBackColor = false;
			// 
			// btnLog
			// 
			this.btnLog.BackColor = System.Drawing.Color.Black;
			this.btnLog.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnLog.Location = new System.Drawing.Point(255, 4);
			this.btnLog.Name = "btnLog";
			this.btnLog.Size = new System.Drawing.Size(36, 36);
			this.btnLog.TabIndex = 6;
			this.btnLog.Text = "Log";
			this.btnLog.UseVisualStyleBackColor = false;
			// 
			// GameForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(1008, 729);
			this.Controls.Add(this.pnlSystemMap);
			this.Controls.Add(this.pnlGalaxyMap);
			this.Controls.Add(this.pnlDetailReport);
			this.Controls.Add(this.pnlSystemTabs);
			this.Controls.Add(this.pnlSearch);
			this.Controls.Add(this.pnlSubCommands);
			this.Controls.Add(this.pnlMainCommands);
			this.Controls.Add(this.pnlHeader);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.White;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "GameForm";
			this.Text = "FrEee";
			this.pnlSystemMap.ResumeLayout(false);
			this.pnlGalaxyMap.ResumeLayout(false);
			this.pnlDetailReport.ResumeLayout(false);
			this.pnlMainCommands.ResumeLayout(false);
			this.pnlHeader.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picEmpireFlag)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Gui.Controls.GamePanel pnlHeader;
		private Gui.Controls.GamePanel pnlMainCommands;
		private Gui.Controls.GamePanel pnlSubCommands;
		private Gui.Controls.GamePanel pnlSearch;
		private Gui.Controls.GamePanel pnlSystemTabs;
		private Gui.Controls.GamePanel pnlSystemMap;
		private Gui.Controls.GamePanel pnlDetailReport;
		private Gui.Controls.GamePanel pnlGalaxyMap;
		private System.Windows.Forms.PictureBox picEmpireFlag;
		private System.Windows.Forms.Label lblGameDate;
		private System.Windows.Forms.Label txtGameDate;
		private Gui.Controls.Pager pagResources;
		private Gui.Controls.GameProgressBar gameProgressBar1;
		private Gui.Controls.ShipReport gameShipReport1;
		private Gui.Controls.StarSystemView starSystemView;
		private Gui.Controls.GalaxyView galaxyView;
		private Gui.Controls.GameButton btnPlanets;
		private Controls.GameButton btnEmpires;
		private Controls.GameButton btnDesigns;
		private Controls.GameButton btnMenu;
		private Controls.GameButton btnShips;
		private Controls.GameButton btnLog;
		private Controls.GameButton btnQueues;
	}
}

