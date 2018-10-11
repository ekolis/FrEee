namespace FrEee.WinForms.Forms
{
	partial class GameSetupForm
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
			FrEee.WinForms.Objects.GalaxyViewModes.PresenceMode presenceMode1 = new FrEee.WinForms.Objects.GalaxyViewModes.PresenceMode();
			this.tabs = new FrEee.WinForms.Controls.GameTabControl();
			this.tabGalaxy = new System.Windows.Forms.TabPage();
			this.label65 = new System.Windows.Forms.Label();
			this.spnSeed = new System.Windows.Forms.NumericUpDown();
			this.btnPreviewMap = new FrEee.WinForms.Controls.GameButton();
			this.galaxyView = new FrEee.WinForms.Controls.GalaxyView();
			this.label62 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.spnSystemGroups = new System.Windows.Forms.NumericUpDown();
			this.txtWarpPointLocation = new System.Windows.Forms.Label();
			this.ddlMaximumEventSeverity = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.spnHeight = new System.Windows.Forms.NumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.spnWidth = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.ddlEventFrequency = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.chkOmniscient = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.chkAllSystemsExplored = new System.Windows.Forms.CheckBox();
			this.ddlWarpPointLocation = new System.Windows.Forms.ComboBox();
			this.warpPointPlacementStrategyBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.spnStarSystems = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.txtGalaxyTypeDescription = new System.Windows.Forms.Label();
			this.ddlGalaxyType = new System.Windows.Forms.ComboBox();
			this.galaxyTemplateBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.lblGalaxyType = new System.Windows.Forms.Label();
			this.tabResources = new System.Windows.Forms.TabPage();
			this.chkLimitRemote = new System.Windows.Forms.CheckBox();
			this.chkLimitStandard = new System.Windows.Forms.CheckBox();
			this.label29 = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.chkRemote = new System.Windows.Forms.CheckBox();
			this.btnRefreshGraphs = new FrEee.WinForms.Controls.GameButton();
			this.picMiningGraph = new System.Windows.Forms.PictureBox();
			this.spnMiningRate = new System.Windows.Forms.NumericUpDown();
			this.spnStartValue = new System.Windows.Forms.NumericUpDown();
			this.label27 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.picValueGraph = new System.Windows.Forms.PictureBox();
			this.spnHomeworldValue = new System.Windows.Forms.NumericUpDown();
			this.label25 = new System.Windows.Forms.Label();
			this.chkBonusDepletionRemote = new System.Windows.Forms.CheckBox();
			this.chkBonusDepletionStandard = new System.Windows.Forms.CheckBox();
			this.label17 = new System.Windows.Forms.Label();
			this.spnDepletionTurnRemote = new System.Windows.Forms.NumericUpDown();
			this.spnDepletionTurnStandard = new System.Windows.Forms.NumericUpDown();
			this.label24 = new System.Windows.Forms.Label();
			this.spnMaxValuePlanet = new System.Windows.Forms.NumericUpDown();
			this.spnMaxSpawnValueAsteroid = new System.Windows.Forms.NumericUpDown();
			this.spnMinSpawnValueAsteroid = new System.Windows.Forms.NumericUpDown();
			this.spnMinValueAsteroid = new System.Windows.Forms.NumericUpDown();
			this.spnMaxSpawnValuePlanet = new System.Windows.Forms.NumericUpDown();
			this.spnMinSpawnValuePlanet = new System.Windows.Forms.NumericUpDown();
			this.spnMinValuePlanet = new System.Windows.Forms.NumericUpDown();
			this.label23 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.spnDepletionResourceRemote = new System.Windows.Forms.NumericUpDown();
			this.spnBonusRemote = new System.Windows.Forms.NumericUpDown();
			this.spnRateRemote = new System.Windows.Forms.NumericUpDown();
			this.spnDepletionResourceStandard = new System.Windows.Forms.NumericUpDown();
			this.spnBonusStandard = new System.Windows.Forms.NumericUpDown();
			this.spnRateStandard = new System.Windows.Forms.NumericUpDown();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.btnLoadResourcePreset = new FrEee.WinForms.Controls.GameButton();
			this.ddlPresets = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.tabTechnology = new System.Windows.Forms.TabPage();
			this.ddlTechUniqueness = new System.Windows.Forms.ComboBox();
			this.label63 = new System.Windows.Forms.Label();
			this.ddlTechCost = new System.Windows.Forms.ComboBox();
			this.label61 = new System.Windows.Forms.Label();
			this.label31 = new System.Windows.Forms.Label();
			this.lstTechs = new System.Windows.Forms.CheckedListBox();
			this.ddlStartTech = new System.Windows.Forms.ComboBox();
			this.label30 = new System.Windows.Forms.Label();
			this.tabEmpires = new System.Windows.Forms.TabPage();
			this.lblMaxBonusResearchFromEmpirePoints = new System.Windows.Forms.Label();
			this.spnResearchPerUnspentEmpirePoint = new System.Windows.Forms.NumericUpDown();
			this.label64 = new System.Windows.Forms.Label();
			this.btnEmpireBottom = new FrEee.WinForms.Controls.GameButton();
			this.btnEmpireTop = new FrEee.WinForms.Controls.GameButton();
			this.btnEmpireDown = new FrEee.WinForms.Controls.GameButton();
			this.btnEmpireUp = new FrEee.WinForms.Controls.GameButton();
			this.btnToggleAI = new FrEee.WinForms.Controls.GameButton();
			this.label44 = new System.Windows.Forms.Label();
			this.spnMaxDispersion = new System.Windows.Forms.NumericUpDown();
			this.label43 = new System.Windows.Forms.Label();
			this.ddlHomeworldSize = new System.Windows.Forms.ComboBox();
			this.label42 = new System.Windows.Forms.Label();
			this.btnSaveEmpire = new FrEee.WinForms.Controls.GameButton();
			this.btnRemoveEmpire = new FrEee.WinForms.Controls.GameButton();
			this.btnEditEmpire = new FrEee.WinForms.Controls.GameButton();
			this.btnLoadEmpire = new FrEee.WinForms.Controls.GameButton();
			this.btnCreateEmpire = new FrEee.WinForms.Controls.GameButton();
			this.gamePanel1 = new FrEee.WinForms.Controls.GamePanel();
			this.lstEmpires = new System.Windows.Forms.ListView();
			this.label41 = new System.Windows.Forms.Label();
			this.spnResourceStorage = new System.Windows.Forms.NumericUpDown();
			this.label40 = new System.Windows.Forms.Label();
			this.spnMinorEmpires = new System.Windows.Forms.NumericUpDown();
			this.spnRandomAIs = new System.Windows.Forms.NumericUpDown();
			this.spnEmpirePoints = new System.Windows.Forms.NumericUpDown();
			this.ddlScoreDisplay = new System.Windows.Forms.ComboBox();
			this.ddlEmpirePlacement = new System.Windows.Forms.ComboBox();
			this.spnHomeworlds = new System.Windows.Forms.NumericUpDown();
			this.spnStartResearch = new System.Windows.Forms.NumericUpDown();
			this.spnStartResources = new System.Windows.Forms.NumericUpDown();
			this.label39 = new System.Windows.Forms.Label();
			this.label38 = new System.Windows.Forms.Label();
			this.label37 = new System.Windows.Forms.Label();
			this.label36 = new System.Windows.Forms.Label();
			this.label35 = new System.Windows.Forms.Label();
			this.label34 = new System.Windows.Forms.Label();
			this.label33 = new System.Windows.Forms.Label();
			this.label32 = new System.Windows.Forms.Label();
			this.tabVictory = new System.Windows.Forms.TabPage();
			this.label51 = new System.Windows.Forms.Label();
			this.spnVictoryDelay = new System.Windows.Forms.NumericUpDown();
			this.label50 = new System.Windows.Forms.Label();
			this.label49 = new System.Windows.Forms.Label();
			this.spnVictoryPeace = new System.Windows.Forms.NumericUpDown();
			this.chkVictoryPeace = new System.Windows.Forms.CheckBox();
			this.label48 = new System.Windows.Forms.Label();
			this.spnVictoryTech = new System.Windows.Forms.NumericUpDown();
			this.chkVictoryTech = new System.Windows.Forms.CheckBox();
			this.label47 = new System.Windows.Forms.Label();
			this.spnVictoryScorePercent = new System.Windows.Forms.NumericUpDown();
			this.chkVictoryScorePercent = new System.Windows.Forms.CheckBox();
			this.label46 = new System.Windows.Forms.Label();
			this.spnVictoryTurns = new System.Windows.Forms.NumericUpDown();
			this.chkVictoryTurns = new System.Windows.Forms.CheckBox();
			this.spnVictoryScore = new System.Windows.Forms.NumericUpDown();
			this.chkVictoryScore = new System.Windows.Forms.CheckBox();
			this.chkVictoryEliminateMajorEmpires = new System.Windows.Forms.CheckBox();
			this.label45 = new System.Windows.Forms.Label();
			this.tabSettings = new System.Windows.Forms.TabPage();
			this.chkAllowAnalysis = new System.Windows.Forms.CheckBox();
			this.label60 = new System.Windows.Forms.Label();
			this.chkColonizeOnlyHWSurface = new System.Windows.Forms.CheckBox();
			this.label59 = new System.Windows.Forms.Label();
			this.chkColonizeOnlyBreathable = new System.Windows.Forms.CheckBox();
			this.label58 = new System.Windows.Forms.Label();
			this.chkUniqueRuins = new System.Windows.Forms.CheckBox();
			this.label57 = new System.Windows.Forms.Label();
			this.chkRandomRuins = new System.Windows.Forms.CheckBox();
			this.label56 = new System.Windows.Forms.Label();
			this.chkAllowIntel = new System.Windows.Forms.CheckBox();
			this.label55 = new System.Windows.Forms.Label();
			this.chkAllowSurrender = new System.Windows.Forms.CheckBox();
			this.label54 = new System.Windows.Forms.Label();
			this.label53 = new System.Windows.Forms.Label();
			this.label52 = new System.Windows.Forms.Label();
			this.ddlAllowedTrades = new System.Windows.Forms.ComboBox();
			this.chkHumansVsAI = new System.Windows.Forms.CheckBox();
			this.txtGalaxyName = new System.Windows.Forms.TextBox();
			this.labelName = new System.Windows.Forms.Label();
			this.btnStart = new FrEee.WinForms.Controls.GameButton();
			this.btnCancel = new FrEee.WinForms.Controls.GameButton();
			this.btnLoadSetup = new FrEee.WinForms.Controls.GameButton();
			this.btnSaveSetup = new FrEee.WinForms.Controls.GameButton();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.tabs.SuspendLayout();
			this.tabGalaxy.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spnSeed)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnSystemGroups)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnHeight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.warpPointPlacementStrategyBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnStarSystems)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.galaxyTemplateBindingSource)).BeginInit();
			this.tabResources.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picMiningGraph)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnMiningRate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnStartValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picValueGraph)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnHomeworldValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnDepletionTurnRemote)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnDepletionTurnStandard)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnMaxValuePlanet)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnMaxSpawnValueAsteroid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnMinSpawnValueAsteroid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnMinValueAsteroid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnMaxSpawnValuePlanet)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnMinSpawnValuePlanet)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnMinValuePlanet)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnDepletionResourceRemote)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnBonusRemote)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnRateRemote)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnDepletionResourceStandard)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnBonusStandard)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnRateStandard)).BeginInit();
			this.tabTechnology.SuspendLayout();
			this.tabEmpires.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spnResearchPerUnspentEmpirePoint)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnMaxDispersion)).BeginInit();
			this.gamePanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spnResourceStorage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnMinorEmpires)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnRandomAIs)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnEmpirePoints)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnHomeworlds)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnStartResearch)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnStartResources)).BeginInit();
			this.tabVictory.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spnVictoryDelay)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnVictoryPeace)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnVictoryTech)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnVictoryScorePercent)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnVictoryTurns)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnVictoryScore)).BeginInit();
			this.tabSettings.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabs
			// 
			this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabs.Controls.Add(this.tabGalaxy);
			this.tabs.Controls.Add(this.tabResources);
			this.tabs.Controls.Add(this.tabTechnology);
			this.tabs.Controls.Add(this.tabEmpires);
			this.tabs.Controls.Add(this.tabVictory);
			this.tabs.Controls.Add(this.tabSettings);
			this.tabs.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.tabs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabs.Location = new System.Drawing.Point(0, 0);
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.SelectedTabBackColor = System.Drawing.Color.CornflowerBlue;
			this.tabs.SelectedTabForeColor = System.Drawing.Color.Black;
			this.tabs.Size = new System.Drawing.Size(553, 573);
			this.tabs.TabBackColor = System.Drawing.Color.Black;
			this.tabs.TabBorderColor = System.Drawing.Color.CornflowerBlue;
			this.tabs.TabForeColor = System.Drawing.Color.CornflowerBlue;
			this.tabs.TabIndex = 0;
			// 
			// tabGalaxy
			// 
			this.tabGalaxy.BackColor = System.Drawing.Color.Black;
			this.tabGalaxy.Controls.Add(this.label65);
			this.tabGalaxy.Controls.Add(this.spnSeed);
			this.tabGalaxy.Controls.Add(this.btnPreviewMap);
			this.tabGalaxy.Controls.Add(this.galaxyView);
			this.tabGalaxy.Controls.Add(this.label62);
			this.tabGalaxy.Controls.Add(this.label11);
			this.tabGalaxy.Controls.Add(this.spnSystemGroups);
			this.tabGalaxy.Controls.Add(this.txtWarpPointLocation);
			this.tabGalaxy.Controls.Add(this.ddlMaximumEventSeverity);
			this.tabGalaxy.Controls.Add(this.label10);
			this.tabGalaxy.Controls.Add(this.label9);
			this.tabGalaxy.Controls.Add(this.spnHeight);
			this.tabGalaxy.Controls.Add(this.label8);
			this.tabGalaxy.Controls.Add(this.spnWidth);
			this.tabGalaxy.Controls.Add(this.label7);
			this.tabGalaxy.Controls.Add(this.ddlEventFrequency);
			this.tabGalaxy.Controls.Add(this.label6);
			this.tabGalaxy.Controls.Add(this.chkOmniscient);
			this.tabGalaxy.Controls.Add(this.label4);
			this.tabGalaxy.Controls.Add(this.chkAllSystemsExplored);
			this.tabGalaxy.Controls.Add(this.ddlWarpPointLocation);
			this.tabGalaxy.Controls.Add(this.label3);
			this.tabGalaxy.Controls.Add(this.label2);
			this.tabGalaxy.Controls.Add(this.spnStarSystems);
			this.tabGalaxy.Controls.Add(this.label1);
			this.tabGalaxy.Controls.Add(this.txtGalaxyTypeDescription);
			this.tabGalaxy.Controls.Add(this.ddlGalaxyType);
			this.tabGalaxy.Controls.Add(this.lblGalaxyType);
			this.tabGalaxy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabGalaxy.Location = new System.Drawing.Point(4, 29);
			this.tabGalaxy.Name = "tabGalaxy";
			this.tabGalaxy.Padding = new System.Windows.Forms.Padding(3);
			this.tabGalaxy.Size = new System.Drawing.Size(545, 540);
			this.tabGalaxy.TabIndex = 0;
			this.tabGalaxy.Text = "Galaxy";
			// 
			// label65
			// 
			this.label65.AutoSize = true;
			this.label65.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label65.Location = new System.Drawing.Point(272, 9);
			this.label65.Margin = new System.Windows.Forms.Padding(3);
			this.label65.Name = "label65";
			this.label65.Size = new System.Drawing.Size(36, 15);
			this.label65.TabIndex = 31;
			this.label65.Text = "Seed";
			// 
			// spnSeed
			// 
			this.spnSeed.Location = new System.Drawing.Point(314, 7);
			this.spnSeed.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
			this.spnSeed.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
			this.spnSeed.Name = "spnSeed";
			this.spnSeed.Size = new System.Drawing.Size(116, 21);
			this.spnSeed.TabIndex = 30;
			// 
			// btnPreviewMap
			// 
			this.btnPreviewMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPreviewMap.BackColor = System.Drawing.Color.Black;
			this.btnPreviewMap.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnPreviewMap.Location = new System.Drawing.Point(442, 137);
			this.btnPreviewMap.Name = "btnPreviewMap";
			this.btnPreviewMap.Size = new System.Drawing.Size(97, 23);
			this.btnPreviewMap.TabIndex = 10;
			this.btnPreviewMap.Text = "Preview Map";
			this.btnPreviewMap.UseVisualStyleBackColor = false;
			this.btnPreviewMap.Click += new System.EventHandler(this.btnPreviewMap_Click);
			// 
			// galaxyView
			// 
			this.galaxyView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.galaxyView.BackColor = System.Drawing.Color.Black;
			this.galaxyView.Location = new System.Drawing.Point(14, 330);
			this.galaxyView.Mode = presenceMode1;
			this.galaxyView.Name = "galaxyView";
			this.galaxyView.SelectedStarSystem = null;
			this.galaxyView.Size = new System.Drawing.Size(525, 204);
			this.galaxyView.TabIndex = 29;
			this.galaxyView.Text = "galaxyView2";
			// 
			// label62
			// 
			this.label62.AutoSize = true;
			this.label62.Location = new System.Drawing.Point(272, 279);
			this.label62.Name = "label62";
			this.label62.Size = new System.Drawing.Size(158, 15);
			this.label62.TabIndex = 28;
			this.label62.Text = "Per mille per player per turn";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(191, 111);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(287, 15);
			this.label11.TabIndex = 27;
			this.label11.Text = "Fewer groups means more warp point connections.";
			// 
			// spnSystemGroups
			// 
			this.spnSystemGroups.Location = new System.Drawing.Point(114, 109);
			this.spnSystemGroups.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.spnSystemGroups.Name = "spnSystemGroups";
			this.spnSystemGroups.Size = new System.Drawing.Size(71, 21);
			this.spnSystemGroups.TabIndex = 4;
			this.spnSystemGroups.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.spnSystemGroups.ValueChanged += new System.EventHandler(this.spnSystemGroups_ValueChanged);
			// 
			// txtWarpPointLocation
			// 
			this.txtWarpPointLocation.Location = new System.Drawing.Point(19, 163);
			this.txtWarpPointLocation.Margin = new System.Windows.Forms.Padding(3);
			this.txtWarpPointLocation.MaximumSize = new System.Drawing.Size(467, 36);
			this.txtWarpPointLocation.Name = "txtWarpPointLocation";
			this.txtWarpPointLocation.Size = new System.Drawing.Size(467, 36);
			this.txtWarpPointLocation.TabIndex = 25;
			this.txtWarpPointLocation.Text = "Choose a warp point placement option.";
			// 
			// ddlMaximumEventSeverity
			// 
			this.ddlMaximumEventSeverity.DisplayMember = "Value";
			this.ddlMaximumEventSeverity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlMaximumEventSeverity.Enabled = false;
			this.ddlMaximumEventSeverity.FormattingEnabled = true;
			this.ddlMaximumEventSeverity.Location = new System.Drawing.Point(114, 305);
			this.ddlMaximumEventSeverity.Name = "ddlMaximumEventSeverity";
			this.ddlMaximumEventSeverity.Size = new System.Drawing.Size(152, 23);
			this.ddlMaximumEventSeverity.TabIndex = 9;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label10.Location = new System.Drawing.Point(11, 308);
			this.label10.Margin = new System.Windows.Forms.Padding(3);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(82, 15);
			this.label10.TabIndex = 21;
			this.label10.Text = "Event Severity";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.ForeColor = System.Drawing.Color.White;
			this.label9.Location = new System.Drawing.Point(241, 56);
			this.label9.Margin = new System.Windows.Forms.Padding(3);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(63, 15);
			this.label9.TabIndex = 20;
			this.label9.Text = "light-years";
			// 
			// spnHeight
			// 
			this.spnHeight.Location = new System.Drawing.Point(187, 54);
			this.spnHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.spnHeight.Name = "spnHeight";
			this.spnHeight.Size = new System.Drawing.Size(48, 21);
			this.spnHeight.TabIndex = 2;
			this.spnHeight.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
			this.spnHeight.ValueChanged += new System.EventHandler(this.spnHeight_ValueChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.ForeColor = System.Drawing.Color.White;
			this.label8.Location = new System.Drawing.Point(168, 56);
			this.label8.Margin = new System.Windows.Forms.Padding(3);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(13, 15);
			this.label8.TabIndex = 18;
			this.label8.Text = "x";
			// 
			// spnWidth
			// 
			this.spnWidth.Location = new System.Drawing.Point(114, 54);
			this.spnWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.spnWidth.Name = "spnWidth";
			this.spnWidth.Size = new System.Drawing.Size(48, 21);
			this.spnWidth.TabIndex = 1;
			this.spnWidth.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
			this.spnWidth.ValueChanged += new System.EventHandler(this.spnWidth_ValueChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label7.Location = new System.Drawing.Point(11, 56);
			this.label7.Margin = new System.Windows.Forms.Padding(3);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(73, 15);
			this.label7.TabIndex = 16;
			this.label7.Text = "Dimensions";
			// 
			// ddlEventFrequency
			// 
			this.ddlEventFrequency.DisplayMember = "Name";
			this.ddlEventFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlEventFrequency.Enabled = false;
			this.ddlEventFrequency.FormattingEnabled = true;
			this.ddlEventFrequency.Location = new System.Drawing.Point(114, 276);
			this.ddlEventFrequency.Name = "ddlEventFrequency";
			this.ddlEventFrequency.Size = new System.Drawing.Size(152, 23);
			this.ddlEventFrequency.TabIndex = 8;
			this.ddlEventFrequency.ValueMember = "Value";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label6.Location = new System.Drawing.Point(11, 279);
			this.label6.Margin = new System.Windows.Forms.Padding(3);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(97, 15);
			this.label6.TabIndex = 14;
			this.label6.Text = "Event Frequency";
			// 
			// chkOmniscient
			// 
			this.chkOmniscient.AutoSize = true;
			this.chkOmniscient.Location = new System.Drawing.Point(22, 251);
			this.chkOmniscient.Name = "chkOmniscient";
			this.chkOmniscient.Size = new System.Drawing.Size(231, 19);
			this.chkOmniscient.TabIndex = 7;
			this.chkOmniscient.Text = "Omniscient View of Explored Systems";
			this.chkOmniscient.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label4.Location = new System.Drawing.Point(11, 205);
			this.label4.Margin = new System.Windows.Forms.Padding(3);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(66, 15);
			this.label4.TabIndex = 10;
			this.label4.Text = "Fog of War";
			// 
			// chkAllSystemsExplored
			// 
			this.chkAllSystemsExplored.AutoSize = true;
			this.chkAllSystemsExplored.Location = new System.Drawing.Point(22, 226);
			this.chkAllSystemsExplored.Name = "chkAllSystemsExplored";
			this.chkAllSystemsExplored.Size = new System.Drawing.Size(140, 19);
			this.chkAllSystemsExplored.TabIndex = 6;
			this.chkAllSystemsExplored.Text = "All Systems Explored";
			this.chkAllSystemsExplored.UseVisualStyleBackColor = true;
			// 
			// ddlWarpPointLocation
			// 
			this.ddlWarpPointLocation.DataSource = this.warpPointPlacementStrategyBindingSource;
			this.ddlWarpPointLocation.DisplayMember = "Name";
			this.ddlWarpPointLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlWarpPointLocation.FormattingEnabled = true;
			this.ddlWarpPointLocation.Location = new System.Drawing.Point(114, 137);
			this.ddlWarpPointLocation.Name = "ddlWarpPointLocation";
			this.ddlWarpPointLocation.Size = new System.Drawing.Size(152, 23);
			this.ddlWarpPointLocation.TabIndex = 5;
			this.ddlWarpPointLocation.SelectedIndexChanged += new System.EventHandler(this.ddlWarpPointLocation_SelectedIndexChanged);
			// 
			// warpPointPlacementStrategyBindingSource
			// 
			this.warpPointPlacementStrategyBindingSource.DataSource = typeof(FrEee.Game.Setup.WarpPointPlacementStrategies.WarpPointPlacementStrategy);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label3.Location = new System.Drawing.Point(9, 140);
			this.label3.Margin = new System.Windows.Forms.Padding(3);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 15);
			this.label3.TabIndex = 7;
			this.label3.Text = "WP Placement";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label2.Location = new System.Drawing.Point(9, 111);
			this.label2.Margin = new System.Windows.Forms.Padding(3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(90, 15);
			this.label2.TabIndex = 5;
			this.label2.Text = "System Groups";
			// 
			// spnStarSystems
			// 
			this.spnStarSystems.Location = new System.Drawing.Point(114, 81);
			this.spnStarSystems.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.spnStarSystems.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.spnStarSystems.Name = "spnStarSystems";
			this.spnStarSystems.Size = new System.Drawing.Size(71, 21);
			this.spnStarSystems.TabIndex = 3;
			this.spnStarSystems.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
			this.spnStarSystems.ValueChanged += new System.EventHandler(this.spnStarSystems_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label1.Location = new System.Drawing.Point(9, 83);
			this.label1.Margin = new System.Windows.Forms.Padding(3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(78, 15);
			this.label1.TabIndex = 3;
			this.label1.Text = "Star Systems";
			// 
			// txtGalaxyTypeDescription
			// 
			this.txtGalaxyTypeDescription.AutoSize = true;
			this.txtGalaxyTypeDescription.Location = new System.Drawing.Point(19, 32);
			this.txtGalaxyTypeDescription.Name = "txtGalaxyTypeDescription";
			this.txtGalaxyTypeDescription.Size = new System.Drawing.Size(125, 15);
			this.txtGalaxyTypeDescription.TabIndex = 2;
			this.txtGalaxyTypeDescription.Text = "Choose a galaxy type.";
			// 
			// ddlGalaxyType
			// 
			this.ddlGalaxyType.DataSource = this.galaxyTemplateBindingSource;
			this.ddlGalaxyType.DisplayMember = "Name";
			this.ddlGalaxyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlGalaxyType.FormattingEnabled = true;
			this.ddlGalaxyType.Location = new System.Drawing.Point(114, 6);
			this.ddlGalaxyType.Name = "ddlGalaxyType";
			this.ddlGalaxyType.Size = new System.Drawing.Size(152, 23);
			this.ddlGalaxyType.TabIndex = 0;
			this.ddlGalaxyType.SelectedIndexChanged += new System.EventHandler(this.ddlGalaxyType_SelectedIndexChanged);
			// 
			// galaxyTemplateBindingSource
			// 
			this.galaxyTemplateBindingSource.AllowNew = false;
			this.galaxyTemplateBindingSource.DataSource = typeof(FrEee.Modding.Templates.GalaxyTemplate);
			// 
			// lblGalaxyType
			// 
			this.lblGalaxyType.AutoSize = true;
			this.lblGalaxyType.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblGalaxyType.Location = new System.Drawing.Point(9, 10);
			this.lblGalaxyType.Margin = new System.Windows.Forms.Padding(3);
			this.lblGalaxyType.Name = "lblGalaxyType";
			this.lblGalaxyType.Size = new System.Drawing.Size(33, 15);
			this.lblGalaxyType.TabIndex = 0;
			this.lblGalaxyType.Text = "Type";
			// 
			// tabResources
			// 
			this.tabResources.BackColor = System.Drawing.Color.Black;
			this.tabResources.Controls.Add(this.chkLimitRemote);
			this.tabResources.Controls.Add(this.chkLimitStandard);
			this.tabResources.Controls.Add(this.label29);
			this.tabResources.Controls.Add(this.label28);
			this.tabResources.Controls.Add(this.chkRemote);
			this.tabResources.Controls.Add(this.btnRefreshGraphs);
			this.tabResources.Controls.Add(this.picMiningGraph);
			this.tabResources.Controls.Add(this.spnMiningRate);
			this.tabResources.Controls.Add(this.spnStartValue);
			this.tabResources.Controls.Add(this.label27);
			this.tabResources.Controls.Add(this.label26);
			this.tabResources.Controls.Add(this.picValueGraph);
			this.tabResources.Controls.Add(this.spnHomeworldValue);
			this.tabResources.Controls.Add(this.label25);
			this.tabResources.Controls.Add(this.chkBonusDepletionRemote);
			this.tabResources.Controls.Add(this.chkBonusDepletionStandard);
			this.tabResources.Controls.Add(this.label17);
			this.tabResources.Controls.Add(this.spnDepletionTurnRemote);
			this.tabResources.Controls.Add(this.spnDepletionTurnStandard);
			this.tabResources.Controls.Add(this.label24);
			this.tabResources.Controls.Add(this.spnMaxValuePlanet);
			this.tabResources.Controls.Add(this.spnMaxSpawnValueAsteroid);
			this.tabResources.Controls.Add(this.spnMinSpawnValueAsteroid);
			this.tabResources.Controls.Add(this.spnMinValueAsteroid);
			this.tabResources.Controls.Add(this.spnMaxSpawnValuePlanet);
			this.tabResources.Controls.Add(this.spnMinSpawnValuePlanet);
			this.tabResources.Controls.Add(this.spnMinValuePlanet);
			this.tabResources.Controls.Add(this.label23);
			this.tabResources.Controls.Add(this.label22);
			this.tabResources.Controls.Add(this.label21);
			this.tabResources.Controls.Add(this.label20);
			this.tabResources.Controls.Add(this.label19);
			this.tabResources.Controls.Add(this.label18);
			this.tabResources.Controls.Add(this.spnDepletionResourceRemote);
			this.tabResources.Controls.Add(this.spnBonusRemote);
			this.tabResources.Controls.Add(this.spnRateRemote);
			this.tabResources.Controls.Add(this.spnDepletionResourceStandard);
			this.tabResources.Controls.Add(this.spnBonusStandard);
			this.tabResources.Controls.Add(this.spnRateStandard);
			this.tabResources.Controls.Add(this.label16);
			this.tabResources.Controls.Add(this.label15);
			this.tabResources.Controls.Add(this.label14);
			this.tabResources.Controls.Add(this.label13);
			this.tabResources.Controls.Add(this.label12);
			this.tabResources.Controls.Add(this.btnLoadResourcePreset);
			this.tabResources.Controls.Add(this.ddlPresets);
			this.tabResources.Controls.Add(this.label5);
			this.tabResources.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabResources.Location = new System.Drawing.Point(4, 29);
			this.tabResources.Name = "tabResources";
			this.tabResources.Size = new System.Drawing.Size(545, 540);
			this.tabResources.TabIndex = 7;
			this.tabResources.Text = "Resources";
			// 
			// chkLimitRemote
			// 
			this.chkLimitRemote.AutoSize = true;
			this.chkLimitRemote.Location = new System.Drawing.Point(257, 83);
			this.chkLimitRemote.Name = "chkLimitRemote";
			this.chkLimitRemote.Size = new System.Drawing.Size(15, 14);
			this.chkLimitRemote.TabIndex = 62;
			this.chkLimitRemote.UseVisualStyleBackColor = true;
			// 
			// chkLimitStandard
			// 
			this.chkLimitStandard.AutoSize = true;
			this.chkLimitStandard.Location = new System.Drawing.Point(153, 83);
			this.chkLimitStandard.Name = "chkLimitStandard";
			this.chkLimitStandard.Size = new System.Drawing.Size(15, 14);
			this.chkLimitStandard.TabIndex = 61;
			this.chkLimitStandard.UseVisualStyleBackColor = true;
			// 
			// label29
			// 
			this.label29.AutoSize = true;
			this.label29.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label29.Location = new System.Drawing.Point(8, 82);
			this.label29.Margin = new System.Windows.Forms.Padding(3);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(114, 15);
			this.label29.TabIndex = 60;
			this.label29.Text = "Limit Rate To Value";
			// 
			// label28
			// 
			this.label28.AutoSize = true;
			this.label28.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label28.Location = new System.Drawing.Point(365, 335);
			this.label28.Margin = new System.Windows.Forms.Padding(3);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(142, 15);
			this.label28.TabIndex = 59;
			this.label28.Text = "Graphs display 100 turns";
			// 
			// chkRemote
			// 
			this.chkRemote.AutoSize = true;
			this.chkRemote.Location = new System.Drawing.Point(372, 85);
			this.chkRemote.Name = "chkRemote";
			this.chkRemote.Size = new System.Drawing.Size(70, 19);
			this.chkRemote.TabIndex = 58;
			this.chkRemote.Text = "Remote";
			this.chkRemote.UseVisualStyleBackColor = true;
			this.chkRemote.CheckedChanged += new System.EventHandler(this.chkRemote_CheckedChanged);
			// 
			// btnRefreshGraphs
			// 
			this.btnRefreshGraphs.BackColor = System.Drawing.Color.Black;
			this.btnRefreshGraphs.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnRefreshGraphs.Location = new System.Drawing.Point(458, 82);
			this.btnRefreshGraphs.Name = "btnRefreshGraphs";
			this.btnRefreshGraphs.Size = new System.Drawing.Size(84, 23);
			this.btnRefreshGraphs.TabIndex = 57;
			this.btnRefreshGraphs.Text = "Refresh";
			this.btnRefreshGraphs.UseVisualStyleBackColor = false;
			this.btnRefreshGraphs.Click += new System.EventHandler(this.btnRefreshGraphs_Click);
			// 
			// picMiningGraph
			// 
			this.picMiningGraph.Location = new System.Drawing.Point(368, 223);
			this.picMiningGraph.Name = "picMiningGraph";
			this.picMiningGraph.Size = new System.Drawing.Size(174, 106);
			this.picMiningGraph.TabIndex = 56;
			this.picMiningGraph.TabStop = false;
			this.picMiningGraph.Paint += new System.Windows.Forms.PaintEventHandler(this.picMiningGraph_Paint);
			// 
			// spnMiningRate
			// 
			this.spnMiningRate.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.spnMiningRate.Location = new System.Drawing.Point(458, 59);
			this.spnMiningRate.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.spnMiningRate.Name = "spnMiningRate";
			this.spnMiningRate.Size = new System.Drawing.Size(84, 21);
			this.spnMiningRate.TabIndex = 55;
			this.spnMiningRate.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.spnMiningRate.ValueChanged += new System.EventHandler(this.spnMiningRate_ValueChanged);
			// 
			// spnStartValue
			// 
			this.spnStartValue.Location = new System.Drawing.Point(458, 32);
			this.spnStartValue.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
			this.spnStartValue.Name = "spnStartValue";
			this.spnStartValue.Size = new System.Drawing.Size(84, 21);
			this.spnStartValue.TabIndex = 54;
			this.spnStartValue.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
			this.spnStartValue.ValueChanged += new System.EventHandler(this.spnStartValue_ValueChanged);
			// 
			// label27
			// 
			this.label27.AutoSize = true;
			this.label27.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label27.Location = new System.Drawing.Point(365, 61);
			this.label27.Margin = new System.Windows.Forms.Padding(3);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(74, 15);
			this.label27.TabIndex = 53;
			this.label27.Text = "Mining Rate";
			// 
			// label26
			// 
			this.label26.AutoSize = true;
			this.label26.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label26.Location = new System.Drawing.Point(365, 32);
			this.label26.Margin = new System.Windows.Forms.Padding(3);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(66, 15);
			this.label26.TabIndex = 52;
			this.label26.Text = "Start Value";
			// 
			// picValueGraph
			// 
			this.picValueGraph.Location = new System.Drawing.Point(368, 111);
			this.picValueGraph.Name = "picValueGraph";
			this.picValueGraph.Size = new System.Drawing.Size(174, 106);
			this.picValueGraph.TabIndex = 51;
			this.picValueGraph.TabStop = false;
			this.picValueGraph.Paint += new System.Windows.Forms.PaintEventHandler(this.picValueGraph_Paint);
			// 
			// spnHomeworldValue
			// 
			this.spnHomeworldValue.Location = new System.Drawing.Point(153, 300);
			this.spnHomeworldValue.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
			this.spnHomeworldValue.Name = "spnHomeworldValue";
			this.spnHomeworldValue.Size = new System.Drawing.Size(95, 21);
			this.spnHomeworldValue.TabIndex = 50;
			this.spnHomeworldValue.ThousandsSeparator = true;
			this.spnHomeworldValue.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
			// 
			// label25
			// 
			this.label25.AutoSize = true;
			this.label25.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label25.Location = new System.Drawing.Point(8, 302);
			this.label25.Margin = new System.Windows.Forms.Padding(3);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(105, 15);
			this.label25.TabIndex = 49;
			this.label25.Text = "Homeworld Value";
			// 
			// chkBonusDepletionRemote
			// 
			this.chkBonusDepletionRemote.AutoSize = true;
			this.chkBonusDepletionRemote.Location = new System.Drawing.Point(257, 161);
			this.chkBonusDepletionRemote.Name = "chkBonusDepletionRemote";
			this.chkBonusDepletionRemote.Size = new System.Drawing.Size(15, 14);
			this.chkBonusDepletionRemote.TabIndex = 48;
			this.chkBonusDepletionRemote.UseVisualStyleBackColor = true;
			// 
			// chkBonusDepletionStandard
			// 
			this.chkBonusDepletionStandard.AutoSize = true;
			this.chkBonusDepletionStandard.Location = new System.Drawing.Point(153, 161);
			this.chkBonusDepletionStandard.Name = "chkBonusDepletionStandard";
			this.chkBonusDepletionStandard.Size = new System.Drawing.Size(15, 14);
			this.chkBonusDepletionStandard.TabIndex = 47;
			this.chkBonusDepletionStandard.UseVisualStyleBackColor = true;
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label17.Location = new System.Drawing.Point(8, 160);
			this.label17.Margin = new System.Windows.Forms.Padding(3);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(136, 15);
			this.label17.TabIndex = 46;
			this.label17.Text = "Bonus Affects Depletion";
			// 
			// spnDepletionTurnRemote
			// 
			this.spnDepletionTurnRemote.Location = new System.Drawing.Point(257, 181);
			this.spnDepletionTurnRemote.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.spnDepletionTurnRemote.Name = "spnDepletionTurnRemote";
			this.spnDepletionTurnRemote.Size = new System.Drawing.Size(95, 21);
			this.spnDepletionTurnRemote.TabIndex = 45;
			this.spnDepletionTurnRemote.ThousandsSeparator = true;
			this.spnDepletionTurnRemote.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// spnDepletionTurnStandard
			// 
			this.spnDepletionTurnStandard.Location = new System.Drawing.Point(153, 181);
			this.spnDepletionTurnStandard.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.spnDepletionTurnStandard.Name = "spnDepletionTurnStandard";
			this.spnDepletionTurnStandard.Size = new System.Drawing.Size(95, 21);
			this.spnDepletionTurnStandard.TabIndex = 44;
			this.spnDepletionTurnStandard.ThousandsSeparator = true;
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label24.Location = new System.Drawing.Point(8, 183);
			this.label24.Margin = new System.Windows.Forms.Padding(3);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(132, 15);
			this.label24.TabIndex = 43;
			this.label24.Text = "Depletion / Turn Mined";
			// 
			// spnMaxValuePlanet
			// 
			this.spnMaxValuePlanet.Location = new System.Drawing.Point(153, 354);
			this.spnMaxValuePlanet.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
			this.spnMaxValuePlanet.Name = "spnMaxValuePlanet";
			this.spnMaxValuePlanet.Size = new System.Drawing.Size(95, 21);
			this.spnMaxValuePlanet.TabIndex = 41;
			this.spnMaxValuePlanet.ThousandsSeparator = true;
			this.spnMaxValuePlanet.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
			// 
			// spnMaxSpawnValueAsteroid
			// 
			this.spnMaxSpawnValueAsteroid.Location = new System.Drawing.Point(257, 327);
			this.spnMaxSpawnValueAsteroid.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
			this.spnMaxSpawnValueAsteroid.Name = "spnMaxSpawnValueAsteroid";
			this.spnMaxSpawnValueAsteroid.Size = new System.Drawing.Size(95, 21);
			this.spnMaxSpawnValueAsteroid.TabIndex = 40;
			this.spnMaxSpawnValueAsteroid.ThousandsSeparator = true;
			this.spnMaxSpawnValueAsteroid.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
			// 
			// spnMinSpawnValueAsteroid
			// 
			this.spnMinSpawnValueAsteroid.Location = new System.Drawing.Point(257, 273);
			this.spnMinSpawnValueAsteroid.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
			this.spnMinSpawnValueAsteroid.Name = "spnMinSpawnValueAsteroid";
			this.spnMinSpawnValueAsteroid.Size = new System.Drawing.Size(95, 21);
			this.spnMinSpawnValueAsteroid.TabIndex = 39;
			this.spnMinSpawnValueAsteroid.ThousandsSeparator = true;
			this.spnMinSpawnValueAsteroid.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
			// 
			// spnMinValueAsteroid
			// 
			this.spnMinValueAsteroid.Location = new System.Drawing.Point(257, 246);
			this.spnMinValueAsteroid.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
			this.spnMinValueAsteroid.Name = "spnMinValueAsteroid";
			this.spnMinValueAsteroid.Size = new System.Drawing.Size(95, 21);
			this.spnMinValueAsteroid.TabIndex = 38;
			this.spnMinValueAsteroid.ThousandsSeparator = true;
			// 
			// spnMaxSpawnValuePlanet
			// 
			this.spnMaxSpawnValuePlanet.Location = new System.Drawing.Point(153, 327);
			this.spnMaxSpawnValuePlanet.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
			this.spnMaxSpawnValuePlanet.Name = "spnMaxSpawnValuePlanet";
			this.spnMaxSpawnValuePlanet.Size = new System.Drawing.Size(95, 21);
			this.spnMaxSpawnValuePlanet.TabIndex = 37;
			this.spnMaxSpawnValuePlanet.ThousandsSeparator = true;
			this.spnMaxSpawnValuePlanet.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
			// 
			// spnMinSpawnValuePlanet
			// 
			this.spnMinSpawnValuePlanet.Location = new System.Drawing.Point(153, 273);
			this.spnMinSpawnValuePlanet.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
			this.spnMinSpawnValuePlanet.Name = "spnMinSpawnValuePlanet";
			this.spnMinSpawnValuePlanet.Size = new System.Drawing.Size(95, 21);
			this.spnMinSpawnValuePlanet.TabIndex = 36;
			this.spnMinSpawnValuePlanet.ThousandsSeparator = true;
			// 
			// spnMinValuePlanet
			// 
			this.spnMinValuePlanet.Location = new System.Drawing.Point(153, 246);
			this.spnMinValuePlanet.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
			this.spnMinValuePlanet.Name = "spnMinValuePlanet";
			this.spnMinValuePlanet.Size = new System.Drawing.Size(95, 21);
			this.spnMinValuePlanet.TabIndex = 35;
			this.spnMinValuePlanet.ThousandsSeparator = true;
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label23.Location = new System.Drawing.Point(254, 223);
			this.label23.Margin = new System.Windows.Forms.Padding(3);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(57, 15);
			this.label23.TabIndex = 34;
			this.label23.Text = "Asteroids";
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label22.Location = new System.Drawing.Point(150, 223);
			this.label22.Margin = new System.Windows.Forms.Padding(3);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(48, 15);
			this.label22.TabIndex = 33;
			this.label22.Text = "Planets";
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label21.Location = new System.Drawing.Point(8, 356);
			this.label21.Margin = new System.Windows.Forms.Padding(3);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(65, 15);
			this.label21.TabIndex = 32;
			this.label21.Text = "Max Value";
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label20.Location = new System.Drawing.Point(8, 246);
			this.label20.Margin = new System.Windows.Forms.Padding(3);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(62, 15);
			this.label20.TabIndex = 31;
			this.label20.Text = "Min Value";
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label19.Location = new System.Drawing.Point(8, 329);
			this.label19.Margin = new System.Windows.Forms.Padding(3);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(106, 15);
			this.label19.TabIndex = 30;
			this.label19.Text = "Max Spawn Value";
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label18.Location = new System.Drawing.Point(8, 275);
			this.label18.Margin = new System.Windows.Forms.Padding(3);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(103, 15);
			this.label18.TabIndex = 29;
			this.label18.Text = "Min Spawn Value";
			// 
			// spnDepletionResourceRemote
			// 
			this.spnDepletionResourceRemote.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this.spnDepletionResourceRemote.Location = new System.Drawing.Point(257, 130);
			this.spnDepletionResourceRemote.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.spnDepletionResourceRemote.Name = "spnDepletionResourceRemote";
			this.spnDepletionResourceRemote.Size = new System.Drawing.Size(95, 21);
			this.spnDepletionResourceRemote.TabIndex = 27;
			// 
			// spnBonusRemote
			// 
			this.spnBonusRemote.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this.spnBonusRemote.Location = new System.Drawing.Point(257, 103);
			this.spnBonusRemote.Name = "spnBonusRemote";
			this.spnBonusRemote.Size = new System.Drawing.Size(95, 21);
			this.spnBonusRemote.TabIndex = 26;
			this.spnBonusRemote.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// spnRateRemote
			// 
			this.spnRateRemote.Location = new System.Drawing.Point(257, 51);
			this.spnRateRemote.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.spnRateRemote.Name = "spnRateRemote";
			this.spnRateRemote.Size = new System.Drawing.Size(95, 21);
			this.spnRateRemote.TabIndex = 25;
			// 
			// spnDepletionResourceStandard
			// 
			this.spnDepletionResourceStandard.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this.spnDepletionResourceStandard.Location = new System.Drawing.Point(153, 130);
			this.spnDepletionResourceStandard.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.spnDepletionResourceStandard.Name = "spnDepletionResourceStandard";
			this.spnDepletionResourceStandard.Size = new System.Drawing.Size(95, 21);
			this.spnDepletionResourceStandard.TabIndex = 23;
			// 
			// spnBonusStandard
			// 
			this.spnBonusStandard.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this.spnBonusStandard.Location = new System.Drawing.Point(153, 103);
			this.spnBonusStandard.Name = "spnBonusStandard";
			this.spnBonusStandard.Size = new System.Drawing.Size(95, 21);
			this.spnBonusStandard.TabIndex = 22;
			this.spnBonusStandard.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// spnRateStandard
			// 
			this.spnRateStandard.Location = new System.Drawing.Point(153, 51);
			this.spnRateStandard.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.spnRateStandard.Name = "spnRateStandard";
			this.spnRateStandard.Size = new System.Drawing.Size(95, 21);
			this.spnRateStandard.TabIndex = 21;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label16.Location = new System.Drawing.Point(8, 132);
			this.label16.Margin = new System.Windows.Forms.Padding(3);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(122, 15);
			this.label16.TabIndex = 19;
			this.label16.Text = "Depletion / Resource";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label15.Location = new System.Drawing.Point(8, 105);
			this.label15.Margin = new System.Windows.Forms.Padding(3);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(90, 15);
			this.label15.TabIndex = 18;
			this.label15.Text = "Value % Bonus";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label14.Location = new System.Drawing.Point(8, 53);
			this.label14.Margin = new System.Windows.Forms.Padding(3);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(47, 15);
			this.label14.TabIndex = 17;
			this.label14.Text = "Rate %";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label13.Location = new System.Drawing.Point(254, 32);
			this.label13.Margin = new System.Windows.Forms.Padding(3);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(92, 15);
			this.label13.TabIndex = 16;
			this.label13.Text = "Remote Mining";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label12.Location = new System.Drawing.Point(150, 32);
			this.label12.Margin = new System.Windows.Forms.Padding(3);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(98, 15);
			this.label12.TabIndex = 15;
			this.label12.Text = "Standard Mining";
			// 
			// btnLoadResourcePreset
			// 
			this.btnLoadResourcePreset.BackColor = System.Drawing.Color.Black;
			this.btnLoadResourcePreset.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnLoadResourcePreset.Location = new System.Drawing.Point(312, 2);
			this.btnLoadResourcePreset.Name = "btnLoadResourcePreset";
			this.btnLoadResourcePreset.Size = new System.Drawing.Size(75, 23);
			this.btnLoadResourcePreset.TabIndex = 13;
			this.btnLoadResourcePreset.Text = "Load";
			this.btnLoadResourcePreset.UseVisualStyleBackColor = false;
			this.btnLoadResourcePreset.Click += new System.EventHandler(this.btnLoadResourcePreset_Click);
			// 
			// ddlPresets
			// 
			this.ddlPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlPresets.FormattingEnabled = true;
			this.ddlPresets.Items.AddRange(new object[] {
            "Standard, Remote Mining Depletes",
            "Standard, Remote Mining Doesn\'t Deplete",
            "Finite"});
			this.ddlPresets.Location = new System.Drawing.Point(57, 3);
			this.ddlPresets.Name = "ddlPresets";
			this.ddlPresets.Size = new System.Drawing.Size(249, 23);
			this.ddlPresets.TabIndex = 12;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label5.Location = new System.Drawing.Point(3, 6);
			this.label5.Margin = new System.Windows.Forms.Padding(3);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 15);
			this.label5.TabIndex = 11;
			this.label5.Text = "Presets";
			// 
			// tabTechnology
			// 
			this.tabTechnology.BackColor = System.Drawing.Color.Black;
			this.tabTechnology.Controls.Add(this.ddlTechUniqueness);
			this.tabTechnology.Controls.Add(this.label63);
			this.tabTechnology.Controls.Add(this.ddlTechCost);
			this.tabTechnology.Controls.Add(this.label61);
			this.tabTechnology.Controls.Add(this.label31);
			this.tabTechnology.Controls.Add(this.lstTechs);
			this.tabTechnology.Controls.Add(this.ddlStartTech);
			this.tabTechnology.Controls.Add(this.label30);
			this.tabTechnology.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabTechnology.Location = new System.Drawing.Point(4, 29);
			this.tabTechnology.Name = "tabTechnology";
			this.tabTechnology.Padding = new System.Windows.Forms.Padding(3);
			this.tabTechnology.Size = new System.Drawing.Size(545, 540);
			this.tabTechnology.TabIndex = 2;
			this.tabTechnology.Text = "Technology";
			// 
			// ddlTechUniqueness
			// 
			this.ddlTechUniqueness.DisplayMember = "Name";
			this.ddlTechUniqueness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlTechUniqueness.FormattingEnabled = true;
			this.ddlTechUniqueness.Location = new System.Drawing.Point(424, 6);
			this.ddlTechUniqueness.Name = "ddlTechUniqueness";
			this.ddlTechUniqueness.Size = new System.Drawing.Size(121, 23);
			this.ddlTechUniqueness.TabIndex = 19;
			this.ddlTechUniqueness.ValueMember = "Value";
			// 
			// label63
			// 
			this.label63.AutoSize = true;
			this.label63.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label63.Location = new System.Drawing.Point(247, 9);
			this.label63.Margin = new System.Windows.Forms.Padding(3);
			this.label63.Name = "label63";
			this.label63.Size = new System.Drawing.Size(179, 15);
			this.label63.TabIndex = 18;
			this.label63.Text = "Tech Known By Other Players is";
			// 
			// ddlTechCost
			// 
			this.ddlTechCost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlTechCost.FormattingEnabled = true;
			this.ddlTechCost.Items.AddRange(new object[] {
            "Low",
            "Medium",
            "High"});
			this.ddlTechCost.Location = new System.Drawing.Point(120, 35);
			this.ddlTechCost.Name = "ddlTechCost";
			this.ddlTechCost.Size = new System.Drawing.Size(121, 23);
			this.ddlTechCost.TabIndex = 17;
			// 
			// label61
			// 
			this.label61.AutoSize = true;
			this.label61.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label61.Location = new System.Drawing.Point(3, 38);
			this.label61.Margin = new System.Windows.Forms.Padding(3);
			this.label61.Name = "label61";
			this.label61.Size = new System.Drawing.Size(97, 15);
			this.label61.TabIndex = 16;
			this.label61.Text = "Technology Cost";
			// 
			// label31
			// 
			this.label31.AutoSize = true;
			this.label31.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label31.Location = new System.Drawing.Point(3, 84);
			this.label31.Margin = new System.Windows.Forms.Padding(3);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(133, 15);
			this.label31.TabIndex = 15;
			this.label31.Text = "Available Technologies";
			// 
			// lstTechs
			// 
			this.lstTechs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstTechs.BackColor = System.Drawing.Color.Black;
			this.lstTechs.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstTechs.CheckOnClick = true;
			this.lstTechs.ColumnWidth = 200;
			this.lstTechs.ForeColor = System.Drawing.Color.White;
			this.lstTechs.FormattingEnabled = true;
			this.lstTechs.Location = new System.Drawing.Point(9, 105);
			this.lstTechs.MultiColumn = true;
			this.lstTechs.Name = "lstTechs";
			this.lstTechs.Size = new System.Drawing.Size(530, 416);
			this.lstTechs.TabIndex = 14;
			// 
			// ddlStartTech
			// 
			this.ddlStartTech.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlStartTech.FormattingEnabled = true;
			this.ddlStartTech.Items.AddRange(new object[] {
            "Low",
            "Medium",
            "High"});
			this.ddlStartTech.Location = new System.Drawing.Point(120, 6);
			this.ddlStartTech.Name = "ddlStartTech";
			this.ddlStartTech.Size = new System.Drawing.Size(121, 23);
			this.ddlStartTech.TabIndex = 13;
			// 
			// label30
			// 
			this.label30.AutoSize = true;
			this.label30.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label30.Location = new System.Drawing.Point(3, 9);
			this.label30.Margin = new System.Windows.Forms.Padding(3);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(111, 15);
			this.label30.TabIndex = 12;
			this.label30.Text = "Starting Tech Level";
			// 
			// tabEmpires
			// 
			this.tabEmpires.BackColor = System.Drawing.Color.Black;
			this.tabEmpires.Controls.Add(this.lblMaxBonusResearchFromEmpirePoints);
			this.tabEmpires.Controls.Add(this.spnResearchPerUnspentEmpirePoint);
			this.tabEmpires.Controls.Add(this.label64);
			this.tabEmpires.Controls.Add(this.btnEmpireBottom);
			this.tabEmpires.Controls.Add(this.btnEmpireTop);
			this.tabEmpires.Controls.Add(this.btnEmpireDown);
			this.tabEmpires.Controls.Add(this.btnEmpireUp);
			this.tabEmpires.Controls.Add(this.btnToggleAI);
			this.tabEmpires.Controls.Add(this.label44);
			this.tabEmpires.Controls.Add(this.spnMaxDispersion);
			this.tabEmpires.Controls.Add(this.label43);
			this.tabEmpires.Controls.Add(this.ddlHomeworldSize);
			this.tabEmpires.Controls.Add(this.label42);
			this.tabEmpires.Controls.Add(this.btnSaveEmpire);
			this.tabEmpires.Controls.Add(this.btnRemoveEmpire);
			this.tabEmpires.Controls.Add(this.btnEditEmpire);
			this.tabEmpires.Controls.Add(this.btnLoadEmpire);
			this.tabEmpires.Controls.Add(this.btnCreateEmpire);
			this.tabEmpires.Controls.Add(this.gamePanel1);
			this.tabEmpires.Controls.Add(this.label41);
			this.tabEmpires.Controls.Add(this.spnResourceStorage);
			this.tabEmpires.Controls.Add(this.label40);
			this.tabEmpires.Controls.Add(this.spnMinorEmpires);
			this.tabEmpires.Controls.Add(this.spnRandomAIs);
			this.tabEmpires.Controls.Add(this.spnEmpirePoints);
			this.tabEmpires.Controls.Add(this.ddlScoreDisplay);
			this.tabEmpires.Controls.Add(this.ddlEmpirePlacement);
			this.tabEmpires.Controls.Add(this.spnHomeworlds);
			this.tabEmpires.Controls.Add(this.spnStartResearch);
			this.tabEmpires.Controls.Add(this.spnStartResources);
			this.tabEmpires.Controls.Add(this.label39);
			this.tabEmpires.Controls.Add(this.label38);
			this.tabEmpires.Controls.Add(this.label37);
			this.tabEmpires.Controls.Add(this.label36);
			this.tabEmpires.Controls.Add(this.label35);
			this.tabEmpires.Controls.Add(this.label34);
			this.tabEmpires.Controls.Add(this.label33);
			this.tabEmpires.Controls.Add(this.label32);
			this.tabEmpires.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabEmpires.Location = new System.Drawing.Point(4, 29);
			this.tabEmpires.Name = "tabEmpires";
			this.tabEmpires.Padding = new System.Windows.Forms.Padding(3);
			this.tabEmpires.Size = new System.Drawing.Size(545, 540);
			this.tabEmpires.TabIndex = 3;
			this.tabEmpires.Text = "Empires";
			// 
			// lblMaxBonusResearchFromEmpirePoints
			// 
			this.lblMaxBonusResearchFromEmpirePoints.AutoSize = true;
			this.lblMaxBonusResearchFromEmpirePoints.ForeColor = System.Drawing.Color.White;
			this.lblMaxBonusResearchFromEmpirePoints.Location = new System.Drawing.Point(253, 86);
			this.lblMaxBonusResearchFromEmpirePoints.Margin = new System.Windows.Forms.Padding(3);
			this.lblMaxBonusResearchFromEmpirePoints.Name = "lblMaxBonusResearchFromEmpirePoints";
			this.lblMaxBonusResearchFromEmpirePoints.Size = new System.Drawing.Size(235, 15);
			this.lblMaxBonusResearchFromEmpirePoints.TabIndex = 50;
			this.lblMaxBonusResearchFromEmpirePoints.Text = "Empires can earn up to 0 bonus research.";
			// 
			// spnResearchPerUnspentEmpirePoint
			// 
			this.spnResearchPerUnspentEmpirePoint.Location = new System.Drawing.Point(449, 57);
			this.spnResearchPerUnspentEmpirePoint.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.spnResearchPerUnspentEmpirePoint.Name = "spnResearchPerUnspentEmpirePoint";
			this.spnResearchPerUnspentEmpirePoint.Size = new System.Drawing.Size(90, 21);
			this.spnResearchPerUnspentEmpirePoint.TabIndex = 3;
			this.spnResearchPerUnspentEmpirePoint.ThousandsSeparator = true;
			this.spnResearchPerUnspentEmpirePoint.ValueChanged += new System.EventHandler(this.spnResearchPerUnspentEmpirePoint_ValueChanged);
			// 
			// label64
			// 
			this.label64.AutoSize = true;
			this.label64.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label64.Location = new System.Drawing.Point(252, 59);
			this.label64.Margin = new System.Windows.Forms.Padding(3);
			this.label64.Name = "label64";
			this.label64.Size = new System.Drawing.Size(187, 15);
			this.label64.TabIndex = 48;
			this.label64.Text = "Bonus Per Unspent Empire Point\r\n";
			// 
			// btnEmpireBottom
			// 
			this.btnEmpireBottom.BackColor = System.Drawing.Color.Black;
			this.btnEmpireBottom.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnEmpireBottom.Location = new System.Drawing.Point(414, 485);
			this.btnEmpireBottom.Name = "btnEmpireBottom";
			this.btnEmpireBottom.Size = new System.Drawing.Size(101, 31);
			this.btnEmpireBottom.TabIndex = 47;
			this.btnEmpireBottom.Text = "To Bottom";
			this.btnEmpireBottom.UseVisualStyleBackColor = false;
			this.btnEmpireBottom.Click += new System.EventHandler(this.btnEmpireBottom_Click);
			// 
			// btnEmpireTop
			// 
			this.btnEmpireTop.BackColor = System.Drawing.Color.Black;
			this.btnEmpireTop.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnEmpireTop.Location = new System.Drawing.Point(414, 448);
			this.btnEmpireTop.Name = "btnEmpireTop";
			this.btnEmpireTop.Size = new System.Drawing.Size(101, 31);
			this.btnEmpireTop.TabIndex = 46;
			this.btnEmpireTop.Text = "To Top";
			this.btnEmpireTop.UseVisualStyleBackColor = false;
			this.btnEmpireTop.Click += new System.EventHandler(this.btnEmpireTop_Click);
			// 
			// btnEmpireDown
			// 
			this.btnEmpireDown.BackColor = System.Drawing.Color.Black;
			this.btnEmpireDown.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnEmpireDown.Location = new System.Drawing.Point(307, 485);
			this.btnEmpireDown.Name = "btnEmpireDown";
			this.btnEmpireDown.Size = new System.Drawing.Size(101, 31);
			this.btnEmpireDown.TabIndex = 45;
			this.btnEmpireDown.Text = "Move Down";
			this.btnEmpireDown.UseVisualStyleBackColor = false;
			this.btnEmpireDown.Click += new System.EventHandler(this.btnEmpireDown_Click);
			// 
			// btnEmpireUp
			// 
			this.btnEmpireUp.BackColor = System.Drawing.Color.Black;
			this.btnEmpireUp.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnEmpireUp.Location = new System.Drawing.Point(307, 448);
			this.btnEmpireUp.Name = "btnEmpireUp";
			this.btnEmpireUp.Size = new System.Drawing.Size(101, 31);
			this.btnEmpireUp.TabIndex = 44;
			this.btnEmpireUp.Text = "Move Up";
			this.btnEmpireUp.UseVisualStyleBackColor = false;
			this.btnEmpireUp.Click += new System.EventHandler(this.btnEmpireUp_Click);
			// 
			// btnToggleAI
			// 
			this.btnToggleAI.BackColor = System.Drawing.Color.Black;
			this.btnToggleAI.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnToggleAI.Location = new System.Drawing.Point(414, 411);
			this.btnToggleAI.Name = "btnToggleAI";
			this.btnToggleAI.Size = new System.Drawing.Size(101, 31);
			this.btnToggleAI.TabIndex = 43;
			this.btnToggleAI.Text = "Toggle AI";
			this.btnToggleAI.UseVisualStyleBackColor = false;
			this.btnToggleAI.Click += new System.EventHandler(this.btnToggleAI_Click);
			// 
			// label44
			// 
			this.label44.AutoSize = true;
			this.label44.Location = new System.Drawing.Point(253, 170);
			this.label44.Name = "label44";
			this.label44.Size = new System.Drawing.Size(173, 15);
			this.label44.TabIndex = 42;
			this.label44.Text = "warps from central homeworld";
			// 
			// spnMaxDispersion
			// 
			this.spnMaxDispersion.Location = new System.Drawing.Point(126, 169);
			this.spnMaxDispersion.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.spnMaxDispersion.Name = "spnMaxDispersion";
			this.spnMaxDispersion.Size = new System.Drawing.Size(120, 21);
			this.spnMaxDispersion.TabIndex = 7;
			this.spnMaxDispersion.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label43
			// 
			this.label43.AutoSize = true;
			this.label43.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label43.Location = new System.Drawing.Point(9, 171);
			this.label43.Margin = new System.Windows.Forms.Padding(3);
			this.label43.Name = "label43";
			this.label43.Size = new System.Drawing.Size(93, 15);
			this.label43.TabIndex = 40;
			this.label43.Text = "Max Dispersion";
			// 
			// ddlHomeworldSize
			// 
			this.ddlHomeworldSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlHomeworldSize.FormattingEnabled = true;
			this.ddlHomeworldSize.Location = new System.Drawing.Point(125, 111);
			this.ddlHomeworldSize.Name = "ddlHomeworldSize";
			this.ddlHomeworldSize.Size = new System.Drawing.Size(165, 23);
			this.ddlHomeworldSize.TabIndex = 5;
			// 
			// label42
			// 
			this.label42.AutoSize = true;
			this.label42.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label42.Location = new System.Drawing.Point(8, 114);
			this.label42.Margin = new System.Windows.Forms.Padding(3);
			this.label42.Name = "label42";
			this.label42.Size = new System.Drawing.Size(98, 15);
			this.label42.TabIndex = 38;
			this.label42.Text = "Homeworld Size";
			// 
			// btnSaveEmpire
			// 
			this.btnSaveEmpire.BackColor = System.Drawing.Color.Black;
			this.btnSaveEmpire.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnSaveEmpire.Location = new System.Drawing.Point(414, 374);
			this.btnSaveEmpire.Name = "btnSaveEmpire";
			this.btnSaveEmpire.Size = new System.Drawing.Size(101, 31);
			this.btnSaveEmpire.TabIndex = 37;
			this.btnSaveEmpire.Text = "Save";
			this.btnSaveEmpire.UseVisualStyleBackColor = false;
			this.btnSaveEmpire.Click += new System.EventHandler(this.btnSaveEmpire_Click);
			// 
			// btnRemoveEmpire
			// 
			this.btnRemoveEmpire.BackColor = System.Drawing.Color.Black;
			this.btnRemoveEmpire.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnRemoveEmpire.Location = new System.Drawing.Point(307, 411);
			this.btnRemoveEmpire.Name = "btnRemoveEmpire";
			this.btnRemoveEmpire.Size = new System.Drawing.Size(101, 31);
			this.btnRemoveEmpire.TabIndex = 36;
			this.btnRemoveEmpire.Text = "Remove";
			this.btnRemoveEmpire.UseVisualStyleBackColor = false;
			this.btnRemoveEmpire.Click += new System.EventHandler(this.btnRemoveEmpire_Click);
			// 
			// btnEditEmpire
			// 
			this.btnEditEmpire.BackColor = System.Drawing.Color.Black;
			this.btnEditEmpire.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnEditEmpire.Location = new System.Drawing.Point(414, 337);
			this.btnEditEmpire.Name = "btnEditEmpire";
			this.btnEditEmpire.Size = new System.Drawing.Size(101, 31);
			this.btnEditEmpire.TabIndex = 35;
			this.btnEditEmpire.Text = "Edit";
			this.btnEditEmpire.UseVisualStyleBackColor = false;
			this.btnEditEmpire.Click += new System.EventHandler(this.btnEditEmpire_Click);
			// 
			// btnLoadEmpire
			// 
			this.btnLoadEmpire.BackColor = System.Drawing.Color.Black;
			this.btnLoadEmpire.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnLoadEmpire.Location = new System.Drawing.Point(307, 374);
			this.btnLoadEmpire.Name = "btnLoadEmpire";
			this.btnLoadEmpire.Size = new System.Drawing.Size(101, 31);
			this.btnLoadEmpire.TabIndex = 34;
			this.btnLoadEmpire.Text = "Load";
			this.btnLoadEmpire.UseVisualStyleBackColor = false;
			this.btnLoadEmpire.Click += new System.EventHandler(this.btnLoadEmpire_Click);
			// 
			// btnCreateEmpire
			// 
			this.btnCreateEmpire.BackColor = System.Drawing.Color.Black;
			this.btnCreateEmpire.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnCreateEmpire.Location = new System.Drawing.Point(307, 337);
			this.btnCreateEmpire.Name = "btnCreateEmpire";
			this.btnCreateEmpire.Size = new System.Drawing.Size(101, 31);
			this.btnCreateEmpire.TabIndex = 12;
			this.btnCreateEmpire.Text = "Create";
			this.btnCreateEmpire.UseVisualStyleBackColor = false;
			this.btnCreateEmpire.Click += new System.EventHandler(this.btnCreateEmpire_Click);
			// 
			// gamePanel1
			// 
			this.gamePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gamePanel1.BackColor = System.Drawing.Color.Black;
			this.gamePanel1.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.gamePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.gamePanel1.Controls.Add(this.lstEmpires);
			this.gamePanel1.ForeColor = System.Drawing.Color.White;
			this.gamePanel1.Location = new System.Drawing.Point(11, 337);
			this.gamePanel1.Name = "gamePanel1";
			this.gamePanel1.Padding = new System.Windows.Forms.Padding(3);
			this.gamePanel1.Size = new System.Drawing.Size(290, 197);
			this.gamePanel1.TabIndex = 32;
			// 
			// lstEmpires
			// 
			this.lstEmpires.BackColor = System.Drawing.Color.Black;
			this.lstEmpires.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstEmpires.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstEmpires.ForeColor = System.Drawing.Color.White;
			this.lstEmpires.Location = new System.Drawing.Point(3, 3);
			this.lstEmpires.Name = "lstEmpires";
			this.lstEmpires.Size = new System.Drawing.Size(282, 189);
			this.lstEmpires.TabIndex = 0;
			this.lstEmpires.UseCompatibleStateImageBehavior = false;
			this.lstEmpires.View = System.Windows.Forms.View.List;
			// 
			// label41
			// 
			this.label41.AutoSize = true;
			this.label41.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label41.Location = new System.Drawing.Point(12, 316);
			this.label41.Margin = new System.Windows.Forms.Padding(3);
			this.label41.Name = "label41";
			this.label41.Size = new System.Drawing.Size(90, 15);
			this.label41.TabIndex = 31;
			this.label41.Text = "Player Empires";
			// 
			// spnResourceStorage
			// 
			this.spnResourceStorage.Location = new System.Drawing.Point(126, 32);
			this.spnResourceStorage.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.spnResourceStorage.Name = "spnResourceStorage";
			this.spnResourceStorage.Size = new System.Drawing.Size(120, 21);
			this.spnResourceStorage.TabIndex = 1;
			this.spnResourceStorage.Value = new decimal(new int[] {
            50000,
            0,
            0,
            0});
			this.spnResourceStorage.ValueChanged += new System.EventHandler(this.spnResourceStorage_ValueChanged);
			// 
			// label40
			// 
			this.label40.AutoSize = true;
			this.label40.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label40.Location = new System.Drawing.Point(6, 34);
			this.label40.Margin = new System.Windows.Forms.Padding(3);
			this.label40.Name = "label40";
			this.label40.Size = new System.Drawing.Size(106, 15);
			this.label40.TabIndex = 29;
			this.label40.Text = "Resource Storage";
			// 
			// spnMinorEmpires
			// 
			this.spnMinorEmpires.Location = new System.Drawing.Point(125, 279);
			this.spnMinorEmpires.Name = "spnMinorEmpires";
			this.spnMinorEmpires.Size = new System.Drawing.Size(120, 21);
			this.spnMinorEmpires.TabIndex = 11;
			// 
			// spnRandomAIs
			// 
			this.spnRandomAIs.Location = new System.Drawing.Point(125, 252);
			this.spnRandomAIs.Name = "spnRandomAIs";
			this.spnRandomAIs.Size = new System.Drawing.Size(120, 21);
			this.spnRandomAIs.TabIndex = 10;
			// 
			// spnEmpirePoints
			// 
			this.spnEmpirePoints.Location = new System.Drawing.Point(125, 225);
			this.spnEmpirePoints.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.spnEmpirePoints.Name = "spnEmpirePoints";
			this.spnEmpirePoints.Size = new System.Drawing.Size(120, 21);
			this.spnEmpirePoints.TabIndex = 9;
			this.spnEmpirePoints.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
			this.spnEmpirePoints.ValueChanged += new System.EventHandler(this.spnEmpirePoints_ValueChanged);
			// 
			// ddlScoreDisplay
			// 
			this.ddlScoreDisplay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlScoreDisplay.FormattingEnabled = true;
			this.ddlScoreDisplay.Items.AddRange(new object[] {
            "Own Only (No Rankings)",
            "Own Only (Ranked)",
            "Allies Only (No Rankings)",
            "Allies Only (Ranked)",
            "All"});
			this.ddlScoreDisplay.Location = new System.Drawing.Point(125, 196);
			this.ddlScoreDisplay.Name = "ddlScoreDisplay";
			this.ddlScoreDisplay.Size = new System.Drawing.Size(166, 23);
			this.ddlScoreDisplay.TabIndex = 8;
			// 
			// ddlEmpirePlacement
			// 
			this.ddlEmpirePlacement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlEmpirePlacement.FormattingEnabled = true;
			this.ddlEmpirePlacement.Items.AddRange(new object[] {
            "Can Start In Same System",
            "Different Systems",
            "Equidistant"});
			this.ddlEmpirePlacement.Location = new System.Drawing.Point(126, 140);
			this.ddlEmpirePlacement.Name = "ddlEmpirePlacement";
			this.ddlEmpirePlacement.Size = new System.Drawing.Size(165, 23);
			this.ddlEmpirePlacement.TabIndex = 6;
			// 
			// spnHomeworlds
			// 
			this.spnHomeworlds.Location = new System.Drawing.Point(125, 84);
			this.spnHomeworlds.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.spnHomeworlds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.spnHomeworlds.Name = "spnHomeworlds";
			this.spnHomeworlds.Size = new System.Drawing.Size(120, 21);
			this.spnHomeworlds.TabIndex = 4;
			this.spnHomeworlds.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// spnStartResearch
			// 
			this.spnStartResearch.Location = new System.Drawing.Point(126, 57);
			this.spnStartResearch.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.spnStartResearch.Name = "spnStartResearch";
			this.spnStartResearch.Size = new System.Drawing.Size(120, 21);
			this.spnStartResearch.TabIndex = 2;
			this.spnStartResearch.Value = new decimal(new int[] {
            20000,
            0,
            0,
            0});
			// 
			// spnStartResources
			// 
			this.spnStartResources.Location = new System.Drawing.Point(126, 7);
			this.spnStartResources.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.spnStartResources.Name = "spnStartResources";
			this.spnStartResources.Size = new System.Drawing.Size(120, 21);
			this.spnStartResources.TabIndex = 0;
			this.spnStartResources.Value = new decimal(new int[] {
            20000,
            0,
            0,
            0});
			// 
			// label39
			// 
			this.label39.AutoSize = true;
			this.label39.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label39.Location = new System.Drawing.Point(9, 281);
			this.label39.Margin = new System.Windows.Forms.Padding(3);
			this.label39.Name = "label39";
			this.label39.Size = new System.Drawing.Size(88, 15);
			this.label39.TabIndex = 20;
			this.label39.Text = "Minor Empires";
			// 
			// label38
			// 
			this.label38.AutoSize = true;
			this.label38.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label38.Location = new System.Drawing.Point(9, 254);
			this.label38.Margin = new System.Windows.Forms.Padding(3);
			this.label38.Name = "label38";
			this.label38.Size = new System.Drawing.Size(74, 15);
			this.label38.TabIndex = 19;
			this.label38.Text = "Random AIs";
			// 
			// label37
			// 
			this.label37.AutoSize = true;
			this.label37.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label37.Location = new System.Drawing.Point(9, 225);
			this.label37.Margin = new System.Windows.Forms.Padding(3);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(84, 15);
			this.label37.TabIndex = 18;
			this.label37.Text = "Empire Points";
			// 
			// label36
			// 
			this.label36.AutoSize = true;
			this.label36.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label36.Location = new System.Drawing.Point(9, 199);
			this.label36.Margin = new System.Windows.Forms.Padding(3);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(82, 15);
			this.label36.TabIndex = 17;
			this.label36.Text = "Score Display";
			// 
			// label35
			// 
			this.label35.AutoSize = true;
			this.label35.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label35.Location = new System.Drawing.Point(9, 143);
			this.label35.Margin = new System.Windows.Forms.Padding(3);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(66, 15);
			this.label35.TabIndex = 16;
			this.label35.Text = "Placement";
			// 
			// label34
			// 
			this.label34.AutoSize = true;
			this.label34.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label34.Location = new System.Drawing.Point(8, 86);
			this.label34.Margin = new System.Windows.Forms.Padding(3);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(77, 15);
			this.label34.TabIndex = 15;
			this.label34.Text = "Homeworlds";
			// 
			// label33
			// 
			this.label33.AutoSize = true;
			this.label33.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label33.Location = new System.Drawing.Point(6, 59);
			this.label33.Margin = new System.Windows.Forms.Padding(3);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(105, 15);
			this.label33.TabIndex = 14;
			this.label33.Text = "Starting Research";
			// 
			// label32
			// 
			this.label32.AutoSize = true;
			this.label32.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label32.Location = new System.Drawing.Point(6, 9);
			this.label32.Margin = new System.Windows.Forms.Padding(3);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(111, 15);
			this.label32.TabIndex = 13;
			this.label32.Text = "Starting Resources";
			// 
			// tabVictory
			// 
			this.tabVictory.BackColor = System.Drawing.Color.Black;
			this.tabVictory.Controls.Add(this.label51);
			this.tabVictory.Controls.Add(this.spnVictoryDelay);
			this.tabVictory.Controls.Add(this.label50);
			this.tabVictory.Controls.Add(this.label49);
			this.tabVictory.Controls.Add(this.spnVictoryPeace);
			this.tabVictory.Controls.Add(this.chkVictoryPeace);
			this.tabVictory.Controls.Add(this.label48);
			this.tabVictory.Controls.Add(this.spnVictoryTech);
			this.tabVictory.Controls.Add(this.chkVictoryTech);
			this.tabVictory.Controls.Add(this.label47);
			this.tabVictory.Controls.Add(this.spnVictoryScorePercent);
			this.tabVictory.Controls.Add(this.chkVictoryScorePercent);
			this.tabVictory.Controls.Add(this.label46);
			this.tabVictory.Controls.Add(this.spnVictoryTurns);
			this.tabVictory.Controls.Add(this.chkVictoryTurns);
			this.tabVictory.Controls.Add(this.spnVictoryScore);
			this.tabVictory.Controls.Add(this.chkVictoryScore);
			this.tabVictory.Controls.Add(this.chkVictoryEliminateMajorEmpires);
			this.tabVictory.Controls.Add(this.label45);
			this.tabVictory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.tabVictory.Location = new System.Drawing.Point(4, 29);
			this.tabVictory.Name = "tabVictory";
			this.tabVictory.Padding = new System.Windows.Forms.Padding(3);
			this.tabVictory.Size = new System.Drawing.Size(545, 540);
			this.tabVictory.TabIndex = 4;
			this.tabVictory.Text = "Victory";
			// 
			// label51
			// 
			this.label51.AutoSize = true;
			this.label51.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label51.Location = new System.Drawing.Point(369, 225);
			this.label51.Name = "label51";
			this.label51.Size = new System.Drawing.Size(34, 15);
			this.label51.TabIndex = 18;
			this.label51.Text = "turns";
			// 
			// spnVictoryDelay
			// 
			this.spnVictoryDelay.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.spnVictoryDelay.Location = new System.Drawing.Point(243, 223);
			this.spnVictoryDelay.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.spnVictoryDelay.Name = "spnVictoryDelay";
			this.spnVictoryDelay.Size = new System.Drawing.Size(120, 21);
			this.spnVictoryDelay.TabIndex = 17;
			this.spnVictoryDelay.ThousandsSeparator = true;
			// 
			// label50
			// 
			this.label50.AutoSize = true;
			this.label50.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label50.Location = new System.Drawing.Point(8, 225);
			this.label50.Name = "label50";
			this.label50.Size = new System.Drawing.Size(229, 15);
			this.label50.TabIndex = 16;
			this.label50.Text = "Custom victory conditions take effect after";
			// 
			// label49
			// 
			this.label49.AutoSize = true;
			this.label49.Location = new System.Drawing.Point(315, 178);
			this.label49.Name = "label49";
			this.label49.Size = new System.Drawing.Size(181, 15);
			this.label49.TabIndex = 15;
			this.label49.Text = "turns (Non-Aggression or better)";
			// 
			// spnVictoryPeace
			// 
			this.spnVictoryPeace.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.spnVictoryPeace.Location = new System.Drawing.Point(189, 176);
			this.spnVictoryPeace.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.spnVictoryPeace.Name = "spnVictoryPeace";
			this.spnVictoryPeace.Size = new System.Drawing.Size(120, 21);
			this.spnVictoryPeace.TabIndex = 14;
			this.spnVictoryPeace.ThousandsSeparator = true;
			this.spnVictoryPeace.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// chkVictoryPeace
			// 
			this.chkVictoryPeace.AutoSize = true;
			this.chkVictoryPeace.Location = new System.Drawing.Point(10, 177);
			this.chkVictoryPeace.Name = "chkVictoryPeace";
			this.chkVictoryPeace.Size = new System.Drawing.Size(173, 19);
			this.chkVictoryPeace.TabIndex = 13;
			this.chkVictoryPeace.Text = "Maintain galactic peace for";
			this.chkVictoryPeace.UseVisualStyleBackColor = true;
			// 
			// label48
			// 
			this.label48.AutoSize = true;
			this.label48.Location = new System.Drawing.Point(221, 153);
			this.label48.Name = "label48";
			this.label48.Size = new System.Drawing.Size(270, 15);
			this.label48.TabIndex = 12;
			this.label48.Text = "% of all non-racial, non-unique technology levels";
			// 
			// spnVictoryTech
			// 
			this.spnVictoryTech.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.spnVictoryTech.Location = new System.Drawing.Point(95, 151);
			this.spnVictoryTech.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.spnVictoryTech.Name = "spnVictoryTech";
			this.spnVictoryTech.Size = new System.Drawing.Size(120, 21);
			this.spnVictoryTech.TabIndex = 11;
			this.spnVictoryTech.ThousandsSeparator = true;
			this.spnVictoryTech.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// chkVictoryTech
			// 
			this.chkVictoryTech.AutoSize = true;
			this.chkVictoryTech.Location = new System.Drawing.Point(10, 152);
			this.chkVictoryTech.Name = "chkVictoryTech";
			this.chkVictoryTech.Size = new System.Drawing.Size(79, 19);
			this.chkVictoryTech.TabIndex = 10;
			this.chkVictoryTech.Text = "Research";
			this.chkVictoryTech.UseVisualStyleBackColor = true;
			// 
			// label47
			// 
			this.label47.AutoSize = true;
			this.label47.Location = new System.Drawing.Point(247, 103);
			this.label47.Name = "label47";
			this.label47.Size = new System.Drawing.Size(205, 15);
			this.label47.TabIndex = 9;
			this.label47.Text = "% of the second place player\'s score";
			// 
			// spnVictoryScorePercent
			// 
			this.spnVictoryScorePercent.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.spnVictoryScorePercent.Location = new System.Drawing.Point(121, 101);
			this.spnVictoryScorePercent.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.spnVictoryScorePercent.Minimum = new decimal(new int[] {
            150,
            0,
            0,
            0});
			this.spnVictoryScorePercent.Name = "spnVictoryScorePercent";
			this.spnVictoryScorePercent.Size = new System.Drawing.Size(120, 21);
			this.spnVictoryScorePercent.TabIndex = 8;
			this.spnVictoryScorePercent.ThousandsSeparator = true;
			this.spnVictoryScorePercent.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
			// 
			// chkVictoryScorePercent
			// 
			this.chkVictoryScorePercent.AutoSize = true;
			this.chkVictoryScorePercent.Location = new System.Drawing.Point(10, 102);
			this.chkVictoryScorePercent.Name = "chkVictoryScorePercent";
			this.chkVictoryScorePercent.Size = new System.Drawing.Size(105, 19);
			this.chkVictoryScorePercent.TabIndex = 7;
			this.chkVictoryScorePercent.Text = "Reach a score";
			this.chkVictoryScorePercent.UseVisualStyleBackColor = true;
			// 
			// label46
			// 
			this.label46.AutoSize = true;
			this.label46.Location = new System.Drawing.Point(224, 128);
			this.label46.Name = "label46";
			this.label46.Size = new System.Drawing.Size(34, 15);
			this.label46.TabIndex = 6;
			this.label46.Text = "turns";
			// 
			// spnVictoryTurns
			// 
			this.spnVictoryTurns.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.spnVictoryTurns.Location = new System.Drawing.Point(98, 126);
			this.spnVictoryTurns.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.spnVictoryTurns.Name = "spnVictoryTurns";
			this.spnVictoryTurns.Size = new System.Drawing.Size(120, 21);
			this.spnVictoryTurns.TabIndex = 5;
			this.spnVictoryTurns.ThousandsSeparator = true;
			this.spnVictoryTurns.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// chkVictoryTurns
			// 
			this.chkVictoryTurns.AutoSize = true;
			this.chkVictoryTurns.Location = new System.Drawing.Point(10, 127);
			this.chkVictoryTurns.Name = "chkVictoryTurns";
			this.chkVictoryTurns.Size = new System.Drawing.Size(82, 19);
			this.chkVictoryTurns.TabIndex = 4;
			this.chkVictoryTurns.Text = "Survive for";
			this.chkVictoryTurns.UseVisualStyleBackColor = true;
			// 
			// spnVictoryScore
			// 
			this.spnVictoryScore.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.spnVictoryScore.Location = new System.Drawing.Point(134, 76);
			this.spnVictoryScore.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
			this.spnVictoryScore.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.spnVictoryScore.Name = "spnVictoryScore";
			this.spnVictoryScore.Size = new System.Drawing.Size(120, 21);
			this.spnVictoryScore.TabIndex = 3;
			this.spnVictoryScore.ThousandsSeparator = true;
			this.spnVictoryScore.Value = new decimal(new int[] {
            5000000,
            0,
            0,
            0});
			// 
			// chkVictoryScore
			// 
			this.chkVictoryScore.AutoSize = true;
			this.chkVictoryScore.Location = new System.Drawing.Point(10, 77);
			this.chkVictoryScore.Name = "chkVictoryScore";
			this.chkVictoryScore.Size = new System.Drawing.Size(118, 19);
			this.chkVictoryScore.TabIndex = 2;
			this.chkVictoryScore.Text = "Reach a score of";
			this.chkVictoryScore.UseVisualStyleBackColor = true;
			// 
			// chkVictoryEliminateMajorEmpires
			// 
			this.chkVictoryEliminateMajorEmpires.AutoSize = true;
			this.chkVictoryEliminateMajorEmpires.Location = new System.Drawing.Point(10, 52);
			this.chkVictoryEliminateMajorEmpires.Name = "chkVictoryEliminateMajorEmpires";
			this.chkVictoryEliminateMajorEmpires.Size = new System.Drawing.Size(365, 19);
			this.chkVictoryEliminateMajorEmpires.TabIndex = 1;
			this.chkVictoryEliminateMajorEmpires.Text = "Eliminate all other major empires (minor empires may survive)";
			this.chkVictoryEliminateMajorEmpires.UseVisualStyleBackColor = true;
			// 
			// label45
			// 
			this.label45.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label45.Location = new System.Drawing.Point(7, 7);
			this.label45.Name = "label45";
			this.label45.Size = new System.Drawing.Size(532, 41);
			this.label45.TabIndex = 0;
			this.label45.Text = "The standard victory condition is elimination of all other major and minor empire" +
    "s. Additional optional victory conditions can be selected below.";
			// 
			// tabSettings
			// 
			this.tabSettings.BackColor = System.Drawing.Color.Black;
			this.tabSettings.Controls.Add(this.chkAllowAnalysis);
			this.tabSettings.Controls.Add(this.label60);
			this.tabSettings.Controls.Add(this.chkColonizeOnlyHWSurface);
			this.tabSettings.Controls.Add(this.label59);
			this.tabSettings.Controls.Add(this.chkColonizeOnlyBreathable);
			this.tabSettings.Controls.Add(this.label58);
			this.tabSettings.Controls.Add(this.chkUniqueRuins);
			this.tabSettings.Controls.Add(this.label57);
			this.tabSettings.Controls.Add(this.chkRandomRuins);
			this.tabSettings.Controls.Add(this.label56);
			this.tabSettings.Controls.Add(this.chkAllowIntel);
			this.tabSettings.Controls.Add(this.label55);
			this.tabSettings.Controls.Add(this.chkAllowSurrender);
			this.tabSettings.Controls.Add(this.label54);
			this.tabSettings.Controls.Add(this.label53);
			this.tabSettings.Controls.Add(this.label52);
			this.tabSettings.Controls.Add(this.ddlAllowedTrades);
			this.tabSettings.Controls.Add(this.chkHumansVsAI);
			this.tabSettings.Controls.Add(this.txtGalaxyName);
			this.tabSettings.Controls.Add(this.labelName);
			this.tabSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabSettings.Location = new System.Drawing.Point(4, 29);
			this.tabSettings.Name = "tabSettings";
			this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
			this.tabSettings.Size = new System.Drawing.Size(545, 540);
			this.tabSettings.TabIndex = 5;
			this.tabSettings.Text = "Settings";
			// 
			// chkAllowAnalysis
			// 
			this.chkAllowAnalysis.AutoSize = true;
			this.chkAllowAnalysis.Checked = true;
			this.chkAllowAnalysis.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAllowAnalysis.Location = new System.Drawing.Point(164, 122);
			this.chkAllowAnalysis.Name = "chkAllowAnalysis";
			this.chkAllowAnalysis.Size = new System.Drawing.Size(15, 14);
			this.chkAllowAnalysis.TabIndex = 44;
			this.chkAllowAnalysis.UseVisualStyleBackColor = true;
			// 
			// label60
			// 
			this.label60.AutoSize = true;
			this.label60.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label60.Location = new System.Drawing.Point(10, 121);
			this.label60.Margin = new System.Windows.Forms.Padding(3);
			this.label60.Name = "label60";
			this.label60.Size = new System.Drawing.Size(83, 15);
			this.label60.TabIndex = 43;
			this.label60.Text = "Allow Analysis";
			// 
			// chkColonizeOnlyHWSurface
			// 
			this.chkColonizeOnlyHWSurface.AutoSize = true;
			this.chkColonizeOnlyHWSurface.Location = new System.Drawing.Point(164, 203);
			this.chkColonizeOnlyHWSurface.Name = "chkColonizeOnlyHWSurface";
			this.chkColonizeOnlyHWSurface.Size = new System.Drawing.Size(15, 14);
			this.chkColonizeOnlyHWSurface.TabIndex = 42;
			this.chkColonizeOnlyHWSurface.UseVisualStyleBackColor = true;
			// 
			// label59
			// 
			this.label59.AutoSize = true;
			this.label59.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label59.Location = new System.Drawing.Point(10, 202);
			this.label59.Margin = new System.Windows.Forms.Padding(3);
			this.label59.Name = "label59";
			this.label59.Size = new System.Drawing.Size(150, 15);
			this.label59.TabIndex = 41;
			this.label59.Text = "Colonize Only HW Surface";
			// 
			// chkColonizeOnlyBreathable
			// 
			this.chkColonizeOnlyBreathable.AutoSize = true;
			this.chkColonizeOnlyBreathable.Location = new System.Drawing.Point(164, 183);
			this.chkColonizeOnlyBreathable.Name = "chkColonizeOnlyBreathable";
			this.chkColonizeOnlyBreathable.Size = new System.Drawing.Size(15, 14);
			this.chkColonizeOnlyBreathable.TabIndex = 40;
			this.chkColonizeOnlyBreathable.UseVisualStyleBackColor = true;
			// 
			// label58
			// 
			this.label58.AutoSize = true;
			this.label58.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label58.Location = new System.Drawing.Point(10, 182);
			this.label58.Margin = new System.Windows.Forms.Padding(3);
			this.label58.Name = "label58";
			this.label58.Size = new System.Drawing.Size(145, 15);
			this.label58.TabIndex = 39;
			this.label58.Text = "Colonize Only Breathable";
			// 
			// chkUniqueRuins
			// 
			this.chkUniqueRuins.AutoSize = true;
			this.chkUniqueRuins.Checked = true;
			this.chkUniqueRuins.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkUniqueRuins.Location = new System.Drawing.Point(164, 162);
			this.chkUniqueRuins.Name = "chkUniqueRuins";
			this.chkUniqueRuins.Size = new System.Drawing.Size(15, 14);
			this.chkUniqueRuins.TabIndex = 38;
			this.chkUniqueRuins.UseVisualStyleBackColor = true;
			// 
			// label57
			// 
			this.label57.AutoSize = true;
			this.label57.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label57.Location = new System.Drawing.Point(10, 161);
			this.label57.Margin = new System.Windows.Forms.Padding(3);
			this.label57.Name = "label57";
			this.label57.Size = new System.Drawing.Size(82, 15);
			this.label57.TabIndex = 37;
			this.label57.Text = "Unique Ruins";
			// 
			// chkRandomRuins
			// 
			this.chkRandomRuins.AutoSize = true;
			this.chkRandomRuins.Checked = true;
			this.chkRandomRuins.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkRandomRuins.Location = new System.Drawing.Point(164, 142);
			this.chkRandomRuins.Name = "chkRandomRuins";
			this.chkRandomRuins.Size = new System.Drawing.Size(15, 14);
			this.chkRandomRuins.TabIndex = 36;
			this.chkRandomRuins.UseVisualStyleBackColor = true;
			// 
			// label56
			// 
			this.label56.AutoSize = true;
			this.label56.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label56.Location = new System.Drawing.Point(10, 141);
			this.label56.Margin = new System.Windows.Forms.Padding(3);
			this.label56.Name = "label56";
			this.label56.Size = new System.Drawing.Size(90, 15);
			this.label56.TabIndex = 35;
			this.label56.Text = "Random Ruins";
			// 
			// chkAllowIntel
			// 
			this.chkAllowIntel.AutoSize = true;
			this.chkAllowIntel.Checked = true;
			this.chkAllowIntel.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAllowIntel.Location = new System.Drawing.Point(164, 102);
			this.chkAllowIntel.Name = "chkAllowIntel";
			this.chkAllowIntel.Size = new System.Drawing.Size(15, 14);
			this.chkAllowIntel.TabIndex = 34;
			this.chkAllowIntel.UseVisualStyleBackColor = true;
			// 
			// label55
			// 
			this.label55.AutoSize = true;
			this.label55.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label55.Location = new System.Drawing.Point(10, 101);
			this.label55.Margin = new System.Windows.Forms.Padding(3);
			this.label55.Name = "label55";
			this.label55.Size = new System.Drawing.Size(102, 15);
			this.label55.TabIndex = 33;
			this.label55.Text = "Allow Intelligence";
			// 
			// chkAllowSurrender
			// 
			this.chkAllowSurrender.AutoSize = true;
			this.chkAllowSurrender.Checked = true;
			this.chkAllowSurrender.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAllowSurrender.Location = new System.Drawing.Point(164, 82);
			this.chkAllowSurrender.Name = "chkAllowSurrender";
			this.chkAllowSurrender.Size = new System.Drawing.Size(15, 14);
			this.chkAllowSurrender.TabIndex = 32;
			this.chkAllowSurrender.UseVisualStyleBackColor = true;
			// 
			// label54
			// 
			this.label54.AutoSize = true;
			this.label54.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label54.Location = new System.Drawing.Point(10, 81);
			this.label54.Margin = new System.Windows.Forms.Padding(3);
			this.label54.Name = "label54";
			this.label54.Size = new System.Drawing.Size(94, 15);
			this.label54.TabIndex = 31;
			this.label54.Text = "Allow Surrender";
			// 
			// label53
			// 
			this.label53.AutoSize = true;
			this.label53.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label53.Location = new System.Drawing.Point(8, 56);
			this.label53.Margin = new System.Windows.Forms.Padding(3);
			this.label53.Name = "label53";
			this.label53.Size = new System.Drawing.Size(91, 15);
			this.label53.TabIndex = 30;
			this.label53.Text = "Allowed Trades";
			// 
			// label52
			// 
			this.label52.AutoSize = true;
			this.label52.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label52.Location = new System.Drawing.Point(8, 32);
			this.label52.Margin = new System.Windows.Forms.Padding(3);
			this.label52.Name = "label52";
			this.label52.Size = new System.Drawing.Size(84, 15);
			this.label52.TabIndex = 29;
			this.label52.Text = "Humans vs. AI";
			// 
			// ddlAllowedTrades
			// 
			this.ddlAllowedTrades.DisplayMember = "Name";
			this.ddlAllowedTrades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlAllowedTrades.FormattingEnabled = true;
			this.ddlAllowedTrades.Location = new System.Drawing.Point(164, 53);
			this.ddlAllowedTrades.Name = "ddlAllowedTrades";
			this.ddlAllowedTrades.Size = new System.Drawing.Size(152, 23);
			this.ddlAllowedTrades.TabIndex = 28;
			this.ddlAllowedTrades.ValueMember = "Value";
			// 
			// chkHumansVsAI
			// 
			this.chkHumansVsAI.AutoSize = true;
			this.chkHumansVsAI.Location = new System.Drawing.Point(164, 33);
			this.chkHumansVsAI.Name = "chkHumansVsAI";
			this.chkHumansVsAI.Size = new System.Drawing.Size(15, 14);
			this.chkHumansVsAI.TabIndex = 27;
			this.chkHumansVsAI.UseVisualStyleBackColor = true;
			// 
			// txtGalaxyName
			// 
			this.txtGalaxyName.Location = new System.Drawing.Point(164, 6);
			this.txtGalaxyName.Name = "txtGalaxyName";
			this.txtGalaxyName.Size = new System.Drawing.Size(152, 21);
			this.txtGalaxyName.TabIndex = 26;
			// 
			// labelName
			// 
			this.labelName.AutoSize = true;
			this.labelName.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.labelName.Location = new System.Drawing.Point(7, 9);
			this.labelName.Margin = new System.Windows.Forms.Padding(3);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(78, 15);
			this.labelName.TabIndex = 25;
			this.labelName.Text = "Game Name";
			// 
			// btnStart
			// 
			this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStart.BackColor = System.Drawing.Color.Black;
			this.btnStart.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnStart.Location = new System.Drawing.Point(478, 579);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 23);
			this.btnStart.TabIndex = 0;
			this.btnStart.Text = "Start";
			this.btnStart.UseVisualStyleBackColor = false;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.BackColor = System.Drawing.Color.Black;
			this.btnCancel.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnCancel.Location = new System.Drawing.Point(397, 579);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnLoadSetup
			// 
			this.btnLoadSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnLoadSetup.BackColor = System.Drawing.Color.Black;
			this.btnLoadSetup.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnLoadSetup.Location = new System.Drawing.Point(6, 579);
			this.btnLoadSetup.Name = "btnLoadSetup";
			this.btnLoadSetup.Size = new System.Drawing.Size(75, 23);
			this.btnLoadSetup.TabIndex = 2;
			this.btnLoadSetup.Text = "Load Setup";
			this.btnLoadSetup.UseVisualStyleBackColor = false;
			this.btnLoadSetup.Click += new System.EventHandler(this.btnLoadSetup_Click);
			// 
			// btnSaveSetup
			// 
			this.btnSaveSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSaveSetup.BackColor = System.Drawing.Color.Black;
			this.btnSaveSetup.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnSaveSetup.Location = new System.Drawing.Point(87, 579);
			this.btnSaveSetup.Name = "btnSaveSetup";
			this.btnSaveSetup.Size = new System.Drawing.Size(75, 23);
			this.btnSaveSetup.TabIndex = 3;
			this.btnSaveSetup.Text = "Save Setup";
			this.btnSaveSetup.UseVisualStyleBackColor = false;
			this.btnSaveSetup.Click += new System.EventHandler(this.btnSaveSetup_Click);
			// 
			// progressBar
			// 
			this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar.Location = new System.Drawing.Point(168, 585);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(223, 14);
			this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBar.TabIndex = 5;
			this.progressBar.Visible = false;
			// 
			// GameSetupForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(565, 611);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.btnSaveSetup);
			this.Controls.Add(this.btnLoadSetup);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.tabs);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "GameSetupForm";
			this.ShowInTaskbar = false;
			this.Text = "Game Setup";
			this.Load += new System.EventHandler(this.GameSetupForm_Load);
			this.tabs.ResumeLayout(false);
			this.tabGalaxy.ResumeLayout(false);
			this.tabGalaxy.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.spnSeed)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnSystemGroups)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnHeight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.warpPointPlacementStrategyBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnStarSystems)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.galaxyTemplateBindingSource)).EndInit();
			this.tabResources.ResumeLayout(false);
			this.tabResources.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picMiningGraph)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnMiningRate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnStartValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picValueGraph)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnHomeworldValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnDepletionTurnRemote)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnDepletionTurnStandard)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnMaxValuePlanet)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnMaxSpawnValueAsteroid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnMinSpawnValueAsteroid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnMinValueAsteroid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnMaxSpawnValuePlanet)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnMinSpawnValuePlanet)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnMinValuePlanet)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnDepletionResourceRemote)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnBonusRemote)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnRateRemote)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnDepletionResourceStandard)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnBonusStandard)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnRateStandard)).EndInit();
			this.tabTechnology.ResumeLayout(false);
			this.tabTechnology.PerformLayout();
			this.tabEmpires.ResumeLayout(false);
			this.tabEmpires.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.spnResearchPerUnspentEmpirePoint)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnMaxDispersion)).EndInit();
			this.gamePanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spnResourceStorage)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnMinorEmpires)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnRandomAIs)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnEmpirePoints)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnHomeworlds)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnStartResearch)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnStartResources)).EndInit();
			this.tabVictory.ResumeLayout(false);
			this.tabVictory.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.spnVictoryDelay)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnVictoryPeace)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnVictoryTech)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnVictoryScorePercent)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnVictoryTurns)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnVictoryScore)).EndInit();
			this.tabSettings.ResumeLayout(false);
			this.tabSettings.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.GameTabControl tabs;
		private System.Windows.Forms.TabPage tabGalaxy;
		private Controls.GameButton btnStart;
		private Controls.GameButton btnCancel;
		private System.Windows.Forms.TabPage tabTechnology;
		private System.Windows.Forms.TabPage tabEmpires;
		private System.Windows.Forms.TabPage tabVictory;
		private System.Windows.Forms.TabPage tabSettings;
		private System.Windows.Forms.ComboBox ddlGalaxyType;
		private System.Windows.Forms.Label lblGalaxyType;
		private System.Windows.Forms.Label txtGalaxyTypeDescription;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown spnStarSystems;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox ddlWarpPointLocation;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox chkOmniscient;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox chkAllSystemsExplored;
		private System.Windows.Forms.ComboBox ddlEventFrequency;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.NumericUpDown spnHeight;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown spnWidth;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox ddlMaximumEventSeverity;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.BindingSource galaxyTemplateBindingSource;
		private Controls.GameButton btnLoadSetup;
		private Controls.GameButton btnSaveSetup;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Label txtWarpPointLocation;
		private System.Windows.Forms.BindingSource warpPointPlacementStrategyBindingSource;
		private System.Windows.Forms.NumericUpDown spnSystemGroups;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TabPage tabResources;
		private Controls.GameButton btnLoadResourcePreset;
		private System.Windows.Forms.ComboBox ddlPresets;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.NumericUpDown spnDepletionResourceStandard;
		private System.Windows.Forms.NumericUpDown spnBonusStandard;
		private System.Windows.Forms.NumericUpDown spnRateStandard;
		private System.Windows.Forms.NumericUpDown spnDepletionResourceRemote;
		private System.Windows.Forms.NumericUpDown spnBonusRemote;
		private System.Windows.Forms.NumericUpDown spnRateRemote;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.NumericUpDown spnMaxValuePlanet;
		private System.Windows.Forms.NumericUpDown spnMaxSpawnValueAsteroid;
		private System.Windows.Forms.NumericUpDown spnMinSpawnValueAsteroid;
		private System.Windows.Forms.NumericUpDown spnMinValueAsteroid;
		private System.Windows.Forms.NumericUpDown spnMaxSpawnValuePlanet;
		private System.Windows.Forms.NumericUpDown spnMinSpawnValuePlanet;
		private System.Windows.Forms.NumericUpDown spnMinValuePlanet;
		private System.Windows.Forms.CheckBox chkBonusDepletionRemote;
		private System.Windows.Forms.CheckBox chkBonusDepletionStandard;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.NumericUpDown spnDepletionTurnRemote;
		private System.Windows.Forms.NumericUpDown spnDepletionTurnStandard;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.NumericUpDown spnHomeworldValue;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.PictureBox picValueGraph;
		private System.Windows.Forms.NumericUpDown spnMiningRate;
		private System.Windows.Forms.NumericUpDown spnStartValue;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.PictureBox picMiningGraph;
		private Controls.GameButton btnRefreshGraphs;
		private System.Windows.Forms.CheckBox chkRemote;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.CheckBox chkLimitRemote;
		private System.Windows.Forms.CheckBox chkLimitStandard;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.ComboBox ddlStartTech;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.CheckedListBox lstTechs;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.Label label37;
		private System.Windows.Forms.Label label39;
		private System.Windows.Forms.Label label38;
		private System.Windows.Forms.ComboBox ddlEmpirePlacement;
		private System.Windows.Forms.NumericUpDown spnHomeworlds;
		private System.Windows.Forms.NumericUpDown spnStartResearch;
		private System.Windows.Forms.NumericUpDown spnStartResources;
		private System.Windows.Forms.NumericUpDown spnMinorEmpires;
		private System.Windows.Forms.NumericUpDown spnRandomAIs;
		private System.Windows.Forms.NumericUpDown spnEmpirePoints;
		private System.Windows.Forms.ComboBox ddlScoreDisplay;
		private System.Windows.Forms.NumericUpDown spnResourceStorage;
		private System.Windows.Forms.Label label40;
		private System.Windows.Forms.Label label41;
		private Controls.GamePanel gamePanel1;
		private System.Windows.Forms.ListView lstEmpires;
		private Controls.GameButton btnCreateEmpire;
		private Controls.GameButton btnLoadEmpire;
		private Controls.GameButton btnEditEmpire;
		private Controls.GameButton btnRemoveEmpire;
		private Controls.GameButton btnSaveEmpire;
		private System.Windows.Forms.ComboBox ddlHomeworldSize;
		private System.Windows.Forms.Label label42;
		private System.Windows.Forms.Label label44;
		private System.Windows.Forms.NumericUpDown spnMaxDispersion;
		private System.Windows.Forms.Label label43;
		private Controls.GameButton btnToggleAI;
		private Controls.GameButton btnEmpireBottom;
		private Controls.GameButton btnEmpireTop;
		private Controls.GameButton btnEmpireDown;
		private Controls.GameButton btnEmpireUp;
		private System.Windows.Forms.Label label45;
		private System.Windows.Forms.CheckBox chkVictoryEliminateMajorEmpires;
		private System.Windows.Forms.Label label46;
		private System.Windows.Forms.NumericUpDown spnVictoryTurns;
		private System.Windows.Forms.CheckBox chkVictoryTurns;
		private System.Windows.Forms.NumericUpDown spnVictoryScore;
		private System.Windows.Forms.CheckBox chkVictoryScore;
		private System.Windows.Forms.Label label47;
		private System.Windows.Forms.NumericUpDown spnVictoryScorePercent;
		private System.Windows.Forms.CheckBox chkVictoryScorePercent;
		private System.Windows.Forms.Label label48;
		private System.Windows.Forms.NumericUpDown spnVictoryTech;
		private System.Windows.Forms.CheckBox chkVictoryTech;
		private System.Windows.Forms.Label label49;
		private System.Windows.Forms.NumericUpDown spnVictoryPeace;
		private System.Windows.Forms.CheckBox chkVictoryPeace;
		private System.Windows.Forms.Label label50;
		private System.Windows.Forms.Label label51;
		private System.Windows.Forms.NumericUpDown spnVictoryDelay;
		private System.Windows.Forms.TextBox txtGalaxyName;
		private System.Windows.Forms.Label labelName;
		private System.Windows.Forms.CheckBox chkHumansVsAI;
		private System.Windows.Forms.Label label53;
		private System.Windows.Forms.Label label52;
		private System.Windows.Forms.ComboBox ddlAllowedTrades;
		private System.Windows.Forms.CheckBox chkAllowSurrender;
		private System.Windows.Forms.Label label54;
		private System.Windows.Forms.CheckBox chkAllowIntel;
		private System.Windows.Forms.Label label55;
		private System.Windows.Forms.CheckBox chkUniqueRuins;
		private System.Windows.Forms.Label label57;
		private System.Windows.Forms.CheckBox chkRandomRuins;
		private System.Windows.Forms.Label label56;
		private System.Windows.Forms.CheckBox chkColonizeOnlyBreathable;
		private System.Windows.Forms.Label label58;
		private System.Windows.Forms.CheckBox chkColonizeOnlyHWSurface;
		private System.Windows.Forms.Label label59;
		private System.Windows.Forms.CheckBox chkAllowAnalysis;
		private System.Windows.Forms.Label label60;
		private System.Windows.Forms.ComboBox ddlTechCost;
		private System.Windows.Forms.Label label61;
		private System.Windows.Forms.Label label62;
		private System.Windows.Forms.ComboBox ddlTechUniqueness;
		private System.Windows.Forms.Label label63;
		private Controls.GameButton btnPreviewMap;
		private Controls.GalaxyView galaxyView;
		private System.Windows.Forms.NumericUpDown spnResearchPerUnspentEmpirePoint;
		private System.Windows.Forms.Label label64;
		private System.Windows.Forms.Label lblMaxBonusResearchFromEmpirePoints;
		private System.Windows.Forms.Label label65;
		private System.Windows.Forms.NumericUpDown spnSeed;
	}
}