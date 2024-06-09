using FrEee.Processes.Setup.WarpPointPlacementStrategies;

namespace FrEee.UI.WinForms.Forms;

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
        components = new System.ComponentModel.Container();
        Objects.GalaxyViewModes.PresenceMode presenceMode1 = new Objects.GalaxyViewModes.PresenceMode();
        tabs = new Controls.GameTabControl();
        tabGalaxy = new System.Windows.Forms.TabPage();
        btnGenerateSeed = new Controls.GameButton();
        label65 = new System.Windows.Forms.Label();
        spnSeed = new System.Windows.Forms.NumericUpDown();
        btnPreviewMap = new Controls.GameButton();
        galaxyView = new Controls.GalaxyView();
        label62 = new System.Windows.Forms.Label();
        label11 = new System.Windows.Forms.Label();
        spnSystemGroups = new System.Windows.Forms.NumericUpDown();
        txtWarpPointLocation = new System.Windows.Forms.Label();
        ddlMaximumEventSeverity = new System.Windows.Forms.ComboBox();
        label10 = new System.Windows.Forms.Label();
        label9 = new System.Windows.Forms.Label();
        spnHeight = new System.Windows.Forms.NumericUpDown();
        label8 = new System.Windows.Forms.Label();
        spnWidth = new System.Windows.Forms.NumericUpDown();
        label7 = new System.Windows.Forms.Label();
        ddlEventFrequency = new System.Windows.Forms.ComboBox();
        label6 = new System.Windows.Forms.Label();
        chkOmniscient = new System.Windows.Forms.CheckBox();
        label4 = new System.Windows.Forms.Label();
        chkAllSystemsExplored = new System.Windows.Forms.CheckBox();
        ddlWarpPointLocation = new System.Windows.Forms.ComboBox();
        warpPointPlacementStrategyBindingSource = new System.Windows.Forms.BindingSource(components);
        label3 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        spnStarSystems = new System.Windows.Forms.NumericUpDown();
        label1 = new System.Windows.Forms.Label();
        txtGalaxyTypeDescription = new System.Windows.Forms.Label();
        ddlGalaxyType = new System.Windows.Forms.ComboBox();
        galaxyTemplateBindingSource = new System.Windows.Forms.BindingSource(components);
        lblGalaxyType = new System.Windows.Forms.Label();
        tabResources = new System.Windows.Forms.TabPage();
        chkLimitRemote = new System.Windows.Forms.CheckBox();
        chkLimitStandard = new System.Windows.Forms.CheckBox();
        label29 = new System.Windows.Forms.Label();
        label28 = new System.Windows.Forms.Label();
        chkRemote = new System.Windows.Forms.CheckBox();
        btnRefreshGraphs = new Controls.GameButton();
        picMiningGraph = new System.Windows.Forms.PictureBox();
        spnMiningRate = new System.Windows.Forms.NumericUpDown();
        spnStartValue = new System.Windows.Forms.NumericUpDown();
        label27 = new System.Windows.Forms.Label();
        label26 = new System.Windows.Forms.Label();
        picValueGraph = new System.Windows.Forms.PictureBox();
        spnHomeworldValue = new System.Windows.Forms.NumericUpDown();
        label25 = new System.Windows.Forms.Label();
        chkBonusDepletionRemote = new System.Windows.Forms.CheckBox();
        chkBonusDepletionStandard = new System.Windows.Forms.CheckBox();
        label17 = new System.Windows.Forms.Label();
        spnDepletionTurnRemote = new System.Windows.Forms.NumericUpDown();
        spnDepletionTurnStandard = new System.Windows.Forms.NumericUpDown();
        label24 = new System.Windows.Forms.Label();
        spnMaxValuePlanet = new System.Windows.Forms.NumericUpDown();
        spnMaxSpawnValueAsteroid = new System.Windows.Forms.NumericUpDown();
        spnMinSpawnValueAsteroid = new System.Windows.Forms.NumericUpDown();
        spnMinValueAsteroid = new System.Windows.Forms.NumericUpDown();
        spnMaxSpawnValuePlanet = new System.Windows.Forms.NumericUpDown();
        spnMinSpawnValuePlanet = new System.Windows.Forms.NumericUpDown();
        spnMinValuePlanet = new System.Windows.Forms.NumericUpDown();
        label23 = new System.Windows.Forms.Label();
        label22 = new System.Windows.Forms.Label();
        label21 = new System.Windows.Forms.Label();
        label20 = new System.Windows.Forms.Label();
        label19 = new System.Windows.Forms.Label();
        label18 = new System.Windows.Forms.Label();
        spnDepletionResourceRemote = new System.Windows.Forms.NumericUpDown();
        spnBonusRemote = new System.Windows.Forms.NumericUpDown();
        spnRateRemote = new System.Windows.Forms.NumericUpDown();
        spnDepletionResourceStandard = new System.Windows.Forms.NumericUpDown();
        spnBonusStandard = new System.Windows.Forms.NumericUpDown();
        spnRateStandard = new System.Windows.Forms.NumericUpDown();
        label16 = new System.Windows.Forms.Label();
        label15 = new System.Windows.Forms.Label();
        label14 = new System.Windows.Forms.Label();
        label13 = new System.Windows.Forms.Label();
        label12 = new System.Windows.Forms.Label();
        btnLoadResourcePreset = new Controls.GameButton();
        ddlPresets = new System.Windows.Forms.ComboBox();
        label5 = new System.Windows.Forms.Label();
        tabTechnology = new System.Windows.Forms.TabPage();
        ddlTechUniqueness = new System.Windows.Forms.ComboBox();
        label63 = new System.Windows.Forms.Label();
        ddlTechCost = new System.Windows.Forms.ComboBox();
        label61 = new System.Windows.Forms.Label();
        label31 = new System.Windows.Forms.Label();
        lstTechs = new System.Windows.Forms.CheckedListBox();
        ddlStartTech = new System.Windows.Forms.ComboBox();
        label30 = new System.Windows.Forms.Label();
        tabEmpires = new System.Windows.Forms.TabPage();
        lblMaxBonusResearchFromEmpirePoints = new System.Windows.Forms.Label();
        spnResearchPerUnspentEmpirePoint = new System.Windows.Forms.NumericUpDown();
        label64 = new System.Windows.Forms.Label();
        btnEmpireBottom = new Controls.GameButton();
        btnEmpireTop = new Controls.GameButton();
        btnEmpireDown = new Controls.GameButton();
        btnEmpireUp = new Controls.GameButton();
        btnToggleAI = new Controls.GameButton();
        label44 = new System.Windows.Forms.Label();
        spnMaxDispersion = new System.Windows.Forms.NumericUpDown();
        label43 = new System.Windows.Forms.Label();
        ddlHomeworldSize = new System.Windows.Forms.ComboBox();
        label42 = new System.Windows.Forms.Label();
        btnSaveEmpire = new Controls.GameButton();
        btnRemoveEmpire = new Controls.GameButton();
        btnEditEmpire = new Controls.GameButton();
        btnLoadEmpire = new Controls.GameButton();
        btnCreateEmpire = new Controls.GameButton();
        gamePanel1 = new Controls.GamePanel();
        lstEmpires = new System.Windows.Forms.ListView();
        label41 = new System.Windows.Forms.Label();
        spnResourceStorage = new System.Windows.Forms.NumericUpDown();
        label40 = new System.Windows.Forms.Label();
        spnMinorEmpires = new System.Windows.Forms.NumericUpDown();
        spnRandomAIs = new System.Windows.Forms.NumericUpDown();
        spnEmpirePoints = new System.Windows.Forms.NumericUpDown();
        ddlScoreDisplay = new System.Windows.Forms.ComboBox();
        ddlEmpirePlacement = new System.Windows.Forms.ComboBox();
        spnHomeworlds = new System.Windows.Forms.NumericUpDown();
        spnStartResearch = new System.Windows.Forms.NumericUpDown();
        spnStartResources = new System.Windows.Forms.NumericUpDown();
        label39 = new System.Windows.Forms.Label();
        label38 = new System.Windows.Forms.Label();
        label37 = new System.Windows.Forms.Label();
        label36 = new System.Windows.Forms.Label();
        label35 = new System.Windows.Forms.Label();
        label34 = new System.Windows.Forms.Label();
        label33 = new System.Windows.Forms.Label();
        label32 = new System.Windows.Forms.Label();
        tabVictory = new System.Windows.Forms.TabPage();
        label51 = new System.Windows.Forms.Label();
        spnVictoryDelay = new System.Windows.Forms.NumericUpDown();
        label50 = new System.Windows.Forms.Label();
        label49 = new System.Windows.Forms.Label();
        spnVictoryPeace = new System.Windows.Forms.NumericUpDown();
        chkVictoryPeace = new System.Windows.Forms.CheckBox();
        label48 = new System.Windows.Forms.Label();
        spnVictoryTech = new System.Windows.Forms.NumericUpDown();
        chkVictoryTech = new System.Windows.Forms.CheckBox();
        label47 = new System.Windows.Forms.Label();
        spnVictoryScorePercent = new System.Windows.Forms.NumericUpDown();
        chkVictoryScorePercent = new System.Windows.Forms.CheckBox();
        label46 = new System.Windows.Forms.Label();
        spnVictoryTurns = new System.Windows.Forms.NumericUpDown();
        chkVictoryTurns = new System.Windows.Forms.CheckBox();
        spnVictoryScore = new System.Windows.Forms.NumericUpDown();
        chkVictoryScore = new System.Windows.Forms.CheckBox();
        chkVictoryEliminateMajorEmpires = new System.Windows.Forms.CheckBox();
        label45 = new System.Windows.Forms.Label();
        tabSettings = new System.Windows.Forms.TabPage();
        chkAllowAnalysis = new System.Windows.Forms.CheckBox();
        label60 = new System.Windows.Forms.Label();
        chkColonizeOnlyHWSurface = new System.Windows.Forms.CheckBox();
        label59 = new System.Windows.Forms.Label();
        chkColonizeOnlyBreathable = new System.Windows.Forms.CheckBox();
        label58 = new System.Windows.Forms.Label();
        chkUniqueRuins = new System.Windows.Forms.CheckBox();
        label57 = new System.Windows.Forms.Label();
        chkRandomRuins = new System.Windows.Forms.CheckBox();
        label56 = new System.Windows.Forms.Label();
        chkAllowIntel = new System.Windows.Forms.CheckBox();
        label55 = new System.Windows.Forms.Label();
        chkAllowSurrender = new System.Windows.Forms.CheckBox();
        label54 = new System.Windows.Forms.Label();
        label53 = new System.Windows.Forms.Label();
        label52 = new System.Windows.Forms.Label();
        ddlAllowedTrades = new System.Windows.Forms.ComboBox();
        chkHumansVsAI = new System.Windows.Forms.CheckBox();
        txtGalaxyName = new System.Windows.Forms.TextBox();
        labelName = new System.Windows.Forms.Label();
        btnStart = new Controls.GameButton();
        btnCancel = new Controls.GameButton();
        btnLoadSetup = new Controls.GameButton();
        btnSaveSetup = new Controls.GameButton();
        progressBar = new System.Windows.Forms.ProgressBar();
        blazorWebView1 = new Microsoft.AspNetCore.Components.WebView.WindowsForms.BlazorWebView();
        tabs.SuspendLayout();
        tabGalaxy.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)spnSeed).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnSystemGroups).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnHeight).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnWidth).BeginInit();
        ((System.ComponentModel.ISupportInitialize)warpPointPlacementStrategyBindingSource).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnStarSystems).BeginInit();
        ((System.ComponentModel.ISupportInitialize)galaxyTemplateBindingSource).BeginInit();
        tabResources.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)picMiningGraph).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnMiningRate).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnStartValue).BeginInit();
        ((System.ComponentModel.ISupportInitialize)picValueGraph).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnHomeworldValue).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnDepletionTurnRemote).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnDepletionTurnStandard).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnMaxValuePlanet).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnMaxSpawnValueAsteroid).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnMinSpawnValueAsteroid).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnMinValueAsteroid).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnMaxSpawnValuePlanet).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnMinSpawnValuePlanet).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnMinValuePlanet).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnDepletionResourceRemote).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnBonusRemote).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnRateRemote).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnDepletionResourceStandard).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnBonusStandard).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnRateStandard).BeginInit();
        tabTechnology.SuspendLayout();
        tabEmpires.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)spnResearchPerUnspentEmpirePoint).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnMaxDispersion).BeginInit();
        gamePanel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)spnResourceStorage).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnMinorEmpires).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnRandomAIs).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnEmpirePoints).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnHomeworlds).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnStartResearch).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnStartResources).BeginInit();
        tabVictory.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)spnVictoryDelay).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnVictoryPeace).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnVictoryTech).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnVictoryScorePercent).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnVictoryTurns).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnVictoryScore).BeginInit();
        tabSettings.SuspendLayout();
        SuspendLayout();
        // 
        // tabs
        // 
        tabs.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        tabs.Controls.Add(tabGalaxy);
        tabs.Controls.Add(tabResources);
        tabs.Controls.Add(tabTechnology);
        tabs.Controls.Add(tabEmpires);
        tabs.Controls.Add(tabVictory);
        tabs.Controls.Add(tabSettings);
        tabs.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
        tabs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        tabs.Location = new System.Drawing.Point(0, 0);
        tabs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        tabs.Name = "tabs";
        tabs.SelectedIndex = 0;
        tabs.SelectedTabBackColor = System.Drawing.Color.CornflowerBlue;
        tabs.SelectedTabForeColor = System.Drawing.Color.Black;
        tabs.Size = new System.Drawing.Size(645, 661);
        tabs.TabBackColor = System.Drawing.Color.Black;
        tabs.TabBorderColor = System.Drawing.Color.CornflowerBlue;
        tabs.TabForeColor = System.Drawing.Color.CornflowerBlue;
        tabs.TabIndex = 0;
        // 
        // tabGalaxy
        // 
        tabGalaxy.BackColor = System.Drawing.Color.Black;
        tabGalaxy.Controls.Add(blazorWebView1);
        tabGalaxy.Controls.Add(btnGenerateSeed);
        tabGalaxy.Controls.Add(label65);
        tabGalaxy.Controls.Add(spnSeed);
        tabGalaxy.Controls.Add(btnPreviewMap);
        tabGalaxy.Controls.Add(galaxyView);
        tabGalaxy.Controls.Add(label62);
        tabGalaxy.Controls.Add(label11);
        tabGalaxy.Controls.Add(spnSystemGroups);
        tabGalaxy.Controls.Add(txtWarpPointLocation);
        tabGalaxy.Controls.Add(ddlMaximumEventSeverity);
        tabGalaxy.Controls.Add(label10);
        tabGalaxy.Controls.Add(label9);
        tabGalaxy.Controls.Add(spnHeight);
        tabGalaxy.Controls.Add(label8);
        tabGalaxy.Controls.Add(spnWidth);
        tabGalaxy.Controls.Add(label7);
        tabGalaxy.Controls.Add(ddlEventFrequency);
        tabGalaxy.Controls.Add(label6);
        tabGalaxy.Controls.Add(chkOmniscient);
        tabGalaxy.Controls.Add(label4);
        tabGalaxy.Controls.Add(chkAllSystemsExplored);
        tabGalaxy.Controls.Add(ddlWarpPointLocation);
        tabGalaxy.Controls.Add(label3);
        tabGalaxy.Controls.Add(label2);
        tabGalaxy.Controls.Add(spnStarSystems);
        tabGalaxy.Controls.Add(label1);
        tabGalaxy.Controls.Add(txtGalaxyTypeDescription);
        tabGalaxy.Controls.Add(ddlGalaxyType);
        tabGalaxy.Controls.Add(lblGalaxyType);
        tabGalaxy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        tabGalaxy.Location = new System.Drawing.Point(4, 29);
        tabGalaxy.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        tabGalaxy.Name = "tabGalaxy";
        tabGalaxy.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
        tabGalaxy.Size = new System.Drawing.Size(637, 628);
        tabGalaxy.TabIndex = 0;
        tabGalaxy.Text = "Galaxy";
        // 
        // btnGenerateSeed
        // 
        btnGenerateSeed.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        btnGenerateSeed.BackColor = System.Drawing.Color.Black;
        btnGenerateSeed.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnGenerateSeed.Location = new System.Drawing.Point(509, 7);
        btnGenerateSeed.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnGenerateSeed.Name = "btnGenerateSeed";
        btnGenerateSeed.Size = new System.Drawing.Size(113, 27);
        btnGenerateSeed.TabIndex = 32;
        btnGenerateSeed.Text = "Generate";
        btnGenerateSeed.UseVisualStyleBackColor = false;
        btnGenerateSeed.Click += btnGenerateSeed_Click;
        // 
        // label65
        // 
        label65.AutoSize = true;
        label65.ForeColor = System.Drawing.Color.CornflowerBlue;
        label65.Location = new System.Drawing.Point(317, 10);
        label65.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label65.Name = "label65";
        label65.Size = new System.Drawing.Size(36, 15);
        label65.TabIndex = 31;
        label65.Text = "Seed";
        // 
        // spnSeed
        // 
        spnSeed.Location = new System.Drawing.Point(366, 8);
        spnSeed.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnSeed.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
        spnSeed.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
        spnSeed.Name = "spnSeed";
        spnSeed.Size = new System.Drawing.Size(135, 21);
        spnSeed.TabIndex = 30;
        // 
        // btnPreviewMap
        // 
        btnPreviewMap.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        btnPreviewMap.BackColor = System.Drawing.Color.Black;
        btnPreviewMap.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnPreviewMap.Location = new System.Drawing.Point(516, 158);
        btnPreviewMap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnPreviewMap.Name = "btnPreviewMap";
        btnPreviewMap.Size = new System.Drawing.Size(113, 27);
        btnPreviewMap.TabIndex = 10;
        btnPreviewMap.Text = "Preview Map";
        btnPreviewMap.UseVisualStyleBackColor = false;
        btnPreviewMap.Click += btnPreviewMap_Click;
        // 
        // galaxyView
        // 
        galaxyView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        galaxyView.BackColor = System.Drawing.Color.Black;
        galaxyView.Location = new System.Drawing.Point(16, 381);
        galaxyView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
//        galaxyView.Mode = presenceMode1;
        galaxyView.Name = "galaxyView";
        galaxyView.SelectedStarSystem = null;
        galaxyView.Size = new System.Drawing.Size(612, 235);
        galaxyView.TabIndex = 29;
        galaxyView.Text = "galaxyView2";
        // 
        // label62
        // 
        label62.AutoSize = true;
        label62.Location = new System.Drawing.Point(317, 322);
        label62.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label62.Name = "label62";
        label62.Size = new System.Drawing.Size(158, 15);
        label62.TabIndex = 28;
        label62.Text = "Per mille per player per turn";
        // 
        // label11
        // 
        label11.AutoSize = true;
        label11.Location = new System.Drawing.Point(223, 128);
        label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label11.Name = "label11";
        label11.Size = new System.Drawing.Size(287, 15);
        label11.TabIndex = 27;
        label11.Text = "Fewer groups means more warp point connections.";
        // 
        // spnSystemGroups
        // 
        spnSystemGroups.Location = new System.Drawing.Point(133, 126);
        spnSystemGroups.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnSystemGroups.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        spnSystemGroups.Name = "spnSystemGroups";
        spnSystemGroups.Size = new System.Drawing.Size(83, 21);
        spnSystemGroups.TabIndex = 4;
        spnSystemGroups.Value = new decimal(new int[] { 1, 0, 0, 0 });
        spnSystemGroups.ValueChanged += spnSystemGroups_ValueChanged;
        // 
        // txtWarpPointLocation
        // 
        txtWarpPointLocation.Location = new System.Drawing.Point(22, 188);
        txtWarpPointLocation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        txtWarpPointLocation.MaximumSize = new System.Drawing.Size(545, 42);
        txtWarpPointLocation.Name = "txtWarpPointLocation";
        txtWarpPointLocation.Size = new System.Drawing.Size(545, 42);
        txtWarpPointLocation.TabIndex = 25;
        txtWarpPointLocation.Text = "Choose a warp point placement option.";
        // 
        // ddlMaximumEventSeverity
        // 
        ddlMaximumEventSeverity.DisplayMember = "Value";
        ddlMaximumEventSeverity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        ddlMaximumEventSeverity.Enabled = false;
        ddlMaximumEventSeverity.FormattingEnabled = true;
        ddlMaximumEventSeverity.Location = new System.Drawing.Point(133, 352);
        ddlMaximumEventSeverity.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        ddlMaximumEventSeverity.Name = "ddlMaximumEventSeverity";
        ddlMaximumEventSeverity.Size = new System.Drawing.Size(177, 23);
        ddlMaximumEventSeverity.TabIndex = 9;
        // 
        // label10
        // 
        label10.AutoSize = true;
        label10.ForeColor = System.Drawing.Color.CornflowerBlue;
        label10.Location = new System.Drawing.Point(13, 355);
        label10.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label10.Name = "label10";
        label10.Size = new System.Drawing.Size(82, 15);
        label10.TabIndex = 21;
        label10.Text = "Event Severity";
        // 
        // label9
        // 
        label9.AutoSize = true;
        label9.ForeColor = System.Drawing.Color.White;
        label9.Location = new System.Drawing.Point(281, 65);
        label9.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label9.Name = "label9";
        label9.Size = new System.Drawing.Size(63, 15);
        label9.TabIndex = 20;
        label9.Text = "light-years";
        // 
        // spnHeight
        // 
        spnHeight.Location = new System.Drawing.Point(218, 62);
        spnHeight.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnHeight.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        spnHeight.Name = "spnHeight";
        spnHeight.Size = new System.Drawing.Size(56, 21);
        spnHeight.TabIndex = 2;
        spnHeight.Value = new decimal(new int[] { 30, 0, 0, 0 });
        spnHeight.ValueChanged += spnHeight_ValueChanged;
        // 
        // label8
        // 
        label8.AutoSize = true;
        label8.ForeColor = System.Drawing.Color.White;
        label8.Location = new System.Drawing.Point(196, 65);
        label8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label8.Name = "label8";
        label8.Size = new System.Drawing.Size(13, 15);
        label8.TabIndex = 18;
        label8.Text = "x";
        // 
        // spnWidth
        // 
        spnWidth.Location = new System.Drawing.Point(133, 62);
        spnWidth.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnWidth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        spnWidth.Name = "spnWidth";
        spnWidth.Size = new System.Drawing.Size(56, 21);
        spnWidth.TabIndex = 1;
        spnWidth.Value = new decimal(new int[] { 40, 0, 0, 0 });
        spnWidth.ValueChanged += spnWidth_ValueChanged;
        // 
        // label7
        // 
        label7.AutoSize = true;
        label7.ForeColor = System.Drawing.Color.CornflowerBlue;
        label7.Location = new System.Drawing.Point(13, 65);
        label7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(73, 15);
        label7.TabIndex = 16;
        label7.Text = "Dimensions";
        // 
        // ddlEventFrequency
        // 
        ddlEventFrequency.DisplayMember = "Name";
        ddlEventFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        ddlEventFrequency.Enabled = false;
        ddlEventFrequency.FormattingEnabled = true;
        ddlEventFrequency.Location = new System.Drawing.Point(133, 318);
        ddlEventFrequency.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        ddlEventFrequency.Name = "ddlEventFrequency";
        ddlEventFrequency.Size = new System.Drawing.Size(177, 23);
        ddlEventFrequency.TabIndex = 8;
        ddlEventFrequency.ValueMember = "Value";
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.ForeColor = System.Drawing.Color.CornflowerBlue;
        label6.Location = new System.Drawing.Point(13, 322);
        label6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label6.Name = "label6";
        label6.Size = new System.Drawing.Size(97, 15);
        label6.TabIndex = 14;
        label6.Text = "Event Frequency";
        // 
        // chkOmniscient
        // 
        chkOmniscient.AutoSize = true;
        chkOmniscient.Location = new System.Drawing.Point(26, 290);
        chkOmniscient.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        chkOmniscient.Name = "chkOmniscient";
        chkOmniscient.Size = new System.Drawing.Size(231, 19);
        chkOmniscient.TabIndex = 7;
        chkOmniscient.Text = "Omniscient View of Explored Systems";
        chkOmniscient.UseVisualStyleBackColor = true;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.ForeColor = System.Drawing.Color.CornflowerBlue;
        label4.Location = new System.Drawing.Point(13, 237);
        label4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(66, 15);
        label4.TabIndex = 10;
        label4.Text = "Fog of War";
        // 
        // chkAllSystemsExplored
        // 
        chkAllSystemsExplored.AutoSize = true;
        chkAllSystemsExplored.Location = new System.Drawing.Point(26, 261);
        chkAllSystemsExplored.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        chkAllSystemsExplored.Name = "chkAllSystemsExplored";
        chkAllSystemsExplored.Size = new System.Drawing.Size(140, 19);
        chkAllSystemsExplored.TabIndex = 6;
        chkAllSystemsExplored.Text = "All Systems Explored";
        chkAllSystemsExplored.UseVisualStyleBackColor = true;
        // 
        // ddlWarpPointLocation
        // 
        ddlWarpPointLocation.DataSource = warpPointPlacementStrategyBindingSource;
        ddlWarpPointLocation.DisplayMember = "Name";
        ddlWarpPointLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        ddlWarpPointLocation.FormattingEnabled = true;
        ddlWarpPointLocation.Location = new System.Drawing.Point(133, 158);
        ddlWarpPointLocation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        ddlWarpPointLocation.Name = "ddlWarpPointLocation";
        ddlWarpPointLocation.Size = new System.Drawing.Size(177, 23);
        ddlWarpPointLocation.TabIndex = 5;
        ddlWarpPointLocation.SelectedIndexChanged += ddlWarpPointLocation_SelectedIndexChanged;
        // 
        // warpPointPlacementStrategyBindingSource
        // 
        warpPointPlacementStrategyBindingSource.DataSource = typeof(WarpPointPlacementStrategy);
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.ForeColor = System.Drawing.Color.CornflowerBlue;
        label3.Location = new System.Drawing.Point(10, 162);
        label3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(88, 15);
        label3.TabIndex = 7;
        label3.Text = "WP Placement";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.ForeColor = System.Drawing.Color.CornflowerBlue;
        label2.Location = new System.Drawing.Point(10, 128);
        label2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(90, 15);
        label2.TabIndex = 5;
        label2.Text = "System Groups";
        // 
        // spnStarSystems
        // 
        spnStarSystems.Location = new System.Drawing.Point(133, 93);
        spnStarSystems.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnStarSystems.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        spnStarSystems.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        spnStarSystems.Name = "spnStarSystems";
        spnStarSystems.Size = new System.Drawing.Size(83, 21);
        spnStarSystems.TabIndex = 3;
        spnStarSystems.Value = new decimal(new int[] { 30, 0, 0, 0 });
        spnStarSystems.ValueChanged += spnStarSystems_ValueChanged;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.ForeColor = System.Drawing.Color.CornflowerBlue;
        label1.Location = new System.Drawing.Point(10, 96);
        label1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(78, 15);
        label1.TabIndex = 3;
        label1.Text = "Star Systems";
        // 
        // txtGalaxyTypeDescription
        // 
        txtGalaxyTypeDescription.AutoSize = true;
        txtGalaxyTypeDescription.Location = new System.Drawing.Point(22, 37);
        txtGalaxyTypeDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        txtGalaxyTypeDescription.Name = "txtGalaxyTypeDescription";
        txtGalaxyTypeDescription.Size = new System.Drawing.Size(125, 15);
        txtGalaxyTypeDescription.TabIndex = 2;
        txtGalaxyTypeDescription.Text = "Choose a galaxy type.";
        // 
        // ddlGalaxyType
        // 
        ddlGalaxyType.DataSource = galaxyTemplateBindingSource;
        ddlGalaxyType.DisplayMember = "Name";
        ddlGalaxyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        ddlGalaxyType.FormattingEnabled = true;
        ddlGalaxyType.Location = new System.Drawing.Point(133, 7);
        ddlGalaxyType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        ddlGalaxyType.Name = "ddlGalaxyType";
        ddlGalaxyType.Size = new System.Drawing.Size(177, 23);
        ddlGalaxyType.TabIndex = 0;
        ddlGalaxyType.SelectedIndexChanged += ddlGalaxyType_SelectedIndexChanged;
        // 
        // galaxyTemplateBindingSource
        // 
        galaxyTemplateBindingSource.AllowNew = false;
        galaxyTemplateBindingSource.DataSource = typeof(Modding.Templates.GalaxyTemplate);
        // 
        // lblGalaxyType
        // 
        lblGalaxyType.AutoSize = true;
        lblGalaxyType.ForeColor = System.Drawing.Color.CornflowerBlue;
        lblGalaxyType.Location = new System.Drawing.Point(10, 12);
        lblGalaxyType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        lblGalaxyType.Name = "lblGalaxyType";
        lblGalaxyType.Size = new System.Drawing.Size(33, 15);
        lblGalaxyType.TabIndex = 0;
        lblGalaxyType.Text = "Type";
        // 
        // tabResources
        // 
        tabResources.BackColor = System.Drawing.Color.Black;
        tabResources.Controls.Add(chkLimitRemote);
        tabResources.Controls.Add(chkLimitStandard);
        tabResources.Controls.Add(label29);
        tabResources.Controls.Add(label28);
        tabResources.Controls.Add(chkRemote);
        tabResources.Controls.Add(btnRefreshGraphs);
        tabResources.Controls.Add(picMiningGraph);
        tabResources.Controls.Add(spnMiningRate);
        tabResources.Controls.Add(spnStartValue);
        tabResources.Controls.Add(label27);
        tabResources.Controls.Add(label26);
        tabResources.Controls.Add(picValueGraph);
        tabResources.Controls.Add(spnHomeworldValue);
        tabResources.Controls.Add(label25);
        tabResources.Controls.Add(chkBonusDepletionRemote);
        tabResources.Controls.Add(chkBonusDepletionStandard);
        tabResources.Controls.Add(label17);
        tabResources.Controls.Add(spnDepletionTurnRemote);
        tabResources.Controls.Add(spnDepletionTurnStandard);
        tabResources.Controls.Add(label24);
        tabResources.Controls.Add(spnMaxValuePlanet);
        tabResources.Controls.Add(spnMaxSpawnValueAsteroid);
        tabResources.Controls.Add(spnMinSpawnValueAsteroid);
        tabResources.Controls.Add(spnMinValueAsteroid);
        tabResources.Controls.Add(spnMaxSpawnValuePlanet);
        tabResources.Controls.Add(spnMinSpawnValuePlanet);
        tabResources.Controls.Add(spnMinValuePlanet);
        tabResources.Controls.Add(label23);
        tabResources.Controls.Add(label22);
        tabResources.Controls.Add(label21);
        tabResources.Controls.Add(label20);
        tabResources.Controls.Add(label19);
        tabResources.Controls.Add(label18);
        tabResources.Controls.Add(spnDepletionResourceRemote);
        tabResources.Controls.Add(spnBonusRemote);
        tabResources.Controls.Add(spnRateRemote);
        tabResources.Controls.Add(spnDepletionResourceStandard);
        tabResources.Controls.Add(spnBonusStandard);
        tabResources.Controls.Add(spnRateStandard);
        tabResources.Controls.Add(label16);
        tabResources.Controls.Add(label15);
        tabResources.Controls.Add(label14);
        tabResources.Controls.Add(label13);
        tabResources.Controls.Add(label12);
        tabResources.Controls.Add(btnLoadResourcePreset);
        tabResources.Controls.Add(ddlPresets);
        tabResources.Controls.Add(label5);
        tabResources.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        tabResources.Location = new System.Drawing.Point(4, 29);
        tabResources.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        tabResources.Name = "tabResources";
        tabResources.Size = new System.Drawing.Size(637, 628);
        tabResources.TabIndex = 7;
        tabResources.Text = "Resources";
        // 
        // chkLimitRemote
        // 
        chkLimitRemote.AutoSize = true;
        chkLimitRemote.Location = new System.Drawing.Point(300, 96);
        chkLimitRemote.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        chkLimitRemote.Name = "chkLimitRemote";
        chkLimitRemote.Size = new System.Drawing.Size(15, 14);
        chkLimitRemote.TabIndex = 62;
        chkLimitRemote.UseVisualStyleBackColor = true;
        // 
        // chkLimitStandard
        // 
        chkLimitStandard.AutoSize = true;
        chkLimitStandard.Location = new System.Drawing.Point(178, 96);
        chkLimitStandard.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        chkLimitStandard.Name = "chkLimitStandard";
        chkLimitStandard.Size = new System.Drawing.Size(15, 14);
        chkLimitStandard.TabIndex = 61;
        chkLimitStandard.UseVisualStyleBackColor = true;
        // 
        // label29
        // 
        label29.AutoSize = true;
        label29.ForeColor = System.Drawing.Color.CornflowerBlue;
        label29.Location = new System.Drawing.Point(9, 95);
        label29.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label29.Name = "label29";
        label29.Size = new System.Drawing.Size(114, 15);
        label29.TabIndex = 60;
        label29.Text = "Limit Rate To Value";
        // 
        // label28
        // 
        label28.AutoSize = true;
        label28.ForeColor = System.Drawing.Color.CornflowerBlue;
        label28.Location = new System.Drawing.Point(426, 387);
        label28.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label28.Name = "label28";
        label28.Size = new System.Drawing.Size(142, 15);
        label28.TabIndex = 59;
        label28.Text = "Graphs display 100 turns";
        // 
        // chkRemote
        // 
        chkRemote.AutoSize = true;
        chkRemote.Location = new System.Drawing.Point(434, 98);
        chkRemote.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        chkRemote.Name = "chkRemote";
        chkRemote.Size = new System.Drawing.Size(70, 19);
        chkRemote.TabIndex = 58;
        chkRemote.Text = "Remote";
        chkRemote.UseVisualStyleBackColor = true;
        chkRemote.CheckedChanged += chkRemote_CheckedChanged;
        // 
        // btnRefreshGraphs
        // 
        btnRefreshGraphs.BackColor = System.Drawing.Color.Black;
        btnRefreshGraphs.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnRefreshGraphs.Location = new System.Drawing.Point(534, 95);
        btnRefreshGraphs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnRefreshGraphs.Name = "btnRefreshGraphs";
        btnRefreshGraphs.Size = new System.Drawing.Size(98, 27);
        btnRefreshGraphs.TabIndex = 57;
        btnRefreshGraphs.Text = "Refresh";
        btnRefreshGraphs.UseVisualStyleBackColor = false;
        btnRefreshGraphs.Click += btnRefreshGraphs_Click;
        // 
        // picMiningGraph
        // 
        picMiningGraph.Location = new System.Drawing.Point(429, 257);
        picMiningGraph.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        picMiningGraph.Name = "picMiningGraph";
        picMiningGraph.Size = new System.Drawing.Size(203, 122);
        picMiningGraph.TabIndex = 56;
        picMiningGraph.TabStop = false;
        picMiningGraph.Paint += picMiningGraph_Paint;
        // 
        // spnMiningRate
        // 
        spnMiningRate.Increment = new decimal(new int[] { 100, 0, 0, 0 });
        spnMiningRate.Location = new System.Drawing.Point(534, 68);
        spnMiningRate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnMiningRate.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        spnMiningRate.Name = "spnMiningRate";
        spnMiningRate.Size = new System.Drawing.Size(98, 21);
        spnMiningRate.TabIndex = 55;
        spnMiningRate.Value = new decimal(new int[] { 1000, 0, 0, 0 });
        spnMiningRate.ValueChanged += spnMiningRate_ValueChanged;
        // 
        // spnStartValue
        // 
        spnStartValue.Location = new System.Drawing.Point(534, 37);
        spnStartValue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnStartValue.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
        spnStartValue.Name = "spnStartValue";
        spnStartValue.Size = new System.Drawing.Size(98, 21);
        spnStartValue.TabIndex = 54;
        spnStartValue.Value = new decimal(new int[] { 120, 0, 0, 0 });
        spnStartValue.ValueChanged += spnStartValue_ValueChanged;
        // 
        // label27
        // 
        label27.AutoSize = true;
        label27.ForeColor = System.Drawing.Color.CornflowerBlue;
        label27.Location = new System.Drawing.Point(426, 70);
        label27.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label27.Name = "label27";
        label27.Size = new System.Drawing.Size(74, 15);
        label27.TabIndex = 53;
        label27.Text = "Mining Rate";
        // 
        // label26
        // 
        label26.AutoSize = true;
        label26.ForeColor = System.Drawing.Color.CornflowerBlue;
        label26.Location = new System.Drawing.Point(426, 37);
        label26.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label26.Name = "label26";
        label26.Size = new System.Drawing.Size(66, 15);
        label26.TabIndex = 52;
        label26.Text = "Start Value";
        // 
        // picValueGraph
        // 
        picValueGraph.Location = new System.Drawing.Point(429, 128);
        picValueGraph.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        picValueGraph.Name = "picValueGraph";
        picValueGraph.Size = new System.Drawing.Size(203, 122);
        picValueGraph.TabIndex = 51;
        picValueGraph.TabStop = false;
        picValueGraph.Paint += picValueGraph_Paint;
        // 
        // spnHomeworldValue
        // 
        spnHomeworldValue.Location = new System.Drawing.Point(178, 346);
        spnHomeworldValue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnHomeworldValue.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
        spnHomeworldValue.Name = "spnHomeworldValue";
        spnHomeworldValue.Size = new System.Drawing.Size(111, 21);
        spnHomeworldValue.TabIndex = 50;
        spnHomeworldValue.ThousandsSeparator = true;
        spnHomeworldValue.Value = new decimal(new int[] { 120, 0, 0, 0 });
        // 
        // label25
        // 
        label25.AutoSize = true;
        label25.ForeColor = System.Drawing.Color.CornflowerBlue;
        label25.Location = new System.Drawing.Point(9, 348);
        label25.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label25.Name = "label25";
        label25.Size = new System.Drawing.Size(105, 15);
        label25.TabIndex = 49;
        label25.Text = "Homeworld Value";
        // 
        // chkBonusDepletionRemote
        // 
        chkBonusDepletionRemote.AutoSize = true;
        chkBonusDepletionRemote.Location = new System.Drawing.Point(300, 186);
        chkBonusDepletionRemote.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        chkBonusDepletionRemote.Name = "chkBonusDepletionRemote";
        chkBonusDepletionRemote.Size = new System.Drawing.Size(15, 14);
        chkBonusDepletionRemote.TabIndex = 48;
        chkBonusDepletionRemote.UseVisualStyleBackColor = true;
        // 
        // chkBonusDepletionStandard
        // 
        chkBonusDepletionStandard.AutoSize = true;
        chkBonusDepletionStandard.Location = new System.Drawing.Point(178, 186);
        chkBonusDepletionStandard.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        chkBonusDepletionStandard.Name = "chkBonusDepletionStandard";
        chkBonusDepletionStandard.Size = new System.Drawing.Size(15, 14);
        chkBonusDepletionStandard.TabIndex = 47;
        chkBonusDepletionStandard.UseVisualStyleBackColor = true;
        // 
        // label17
        // 
        label17.AutoSize = true;
        label17.ForeColor = System.Drawing.Color.CornflowerBlue;
        label17.Location = new System.Drawing.Point(9, 185);
        label17.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label17.Name = "label17";
        label17.Size = new System.Drawing.Size(136, 15);
        label17.TabIndex = 46;
        label17.Text = "Bonus Affects Depletion";
        // 
        // spnDepletionTurnRemote
        // 
        spnDepletionTurnRemote.Location = new System.Drawing.Point(300, 209);
        spnDepletionTurnRemote.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnDepletionTurnRemote.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        spnDepletionTurnRemote.Name = "spnDepletionTurnRemote";
        spnDepletionTurnRemote.Size = new System.Drawing.Size(111, 21);
        spnDepletionTurnRemote.TabIndex = 45;
        spnDepletionTurnRemote.ThousandsSeparator = true;
        spnDepletionTurnRemote.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // spnDepletionTurnStandard
        // 
        spnDepletionTurnStandard.Location = new System.Drawing.Point(178, 209);
        spnDepletionTurnStandard.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnDepletionTurnStandard.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        spnDepletionTurnStandard.Name = "spnDepletionTurnStandard";
        spnDepletionTurnStandard.Size = new System.Drawing.Size(111, 21);
        spnDepletionTurnStandard.TabIndex = 44;
        spnDepletionTurnStandard.ThousandsSeparator = true;
        // 
        // label24
        // 
        label24.AutoSize = true;
        label24.ForeColor = System.Drawing.Color.CornflowerBlue;
        label24.Location = new System.Drawing.Point(9, 211);
        label24.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label24.Name = "label24";
        label24.Size = new System.Drawing.Size(132, 15);
        label24.TabIndex = 43;
        label24.Text = "Depletion / Turn Mined";
        // 
        // spnMaxValuePlanet
        // 
        spnMaxValuePlanet.Location = new System.Drawing.Point(178, 408);
        spnMaxValuePlanet.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnMaxValuePlanet.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
        spnMaxValuePlanet.Name = "spnMaxValuePlanet";
        spnMaxValuePlanet.Size = new System.Drawing.Size(111, 21);
        spnMaxValuePlanet.TabIndex = 41;
        spnMaxValuePlanet.ThousandsSeparator = true;
        spnMaxValuePlanet.Value = new decimal(new int[] { 250, 0, 0, 0 });
        // 
        // spnMaxSpawnValueAsteroid
        // 
        spnMaxSpawnValueAsteroid.Location = new System.Drawing.Point(300, 377);
        spnMaxSpawnValueAsteroid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnMaxSpawnValueAsteroid.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
        spnMaxSpawnValueAsteroid.Name = "spnMaxSpawnValueAsteroid";
        spnMaxSpawnValueAsteroid.Size = new System.Drawing.Size(111, 21);
        spnMaxSpawnValueAsteroid.TabIndex = 40;
        spnMaxSpawnValueAsteroid.ThousandsSeparator = true;
        spnMaxSpawnValueAsteroid.Value = new decimal(new int[] { 300, 0, 0, 0 });
        // 
        // spnMinSpawnValueAsteroid
        // 
        spnMinSpawnValueAsteroid.Location = new System.Drawing.Point(300, 315);
        spnMinSpawnValueAsteroid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnMinSpawnValueAsteroid.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
        spnMinSpawnValueAsteroid.Name = "spnMinSpawnValueAsteroid";
        spnMinSpawnValueAsteroid.Size = new System.Drawing.Size(111, 21);
        spnMinSpawnValueAsteroid.TabIndex = 39;
        spnMinSpawnValueAsteroid.ThousandsSeparator = true;
        spnMinSpawnValueAsteroid.Value = new decimal(new int[] { 50, 0, 0, 0 });
        // 
        // spnMinValueAsteroid
        // 
        spnMinValueAsteroid.Location = new System.Drawing.Point(300, 284);
        spnMinValueAsteroid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnMinValueAsteroid.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
        spnMinValueAsteroid.Name = "spnMinValueAsteroid";
        spnMinValueAsteroid.Size = new System.Drawing.Size(111, 21);
        spnMinValueAsteroid.TabIndex = 38;
        spnMinValueAsteroid.ThousandsSeparator = true;
        // 
        // spnMaxSpawnValuePlanet
        // 
        spnMaxSpawnValuePlanet.Location = new System.Drawing.Point(178, 377);
        spnMaxSpawnValuePlanet.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnMaxSpawnValuePlanet.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
        spnMaxSpawnValuePlanet.Name = "spnMaxSpawnValuePlanet";
        spnMaxSpawnValuePlanet.Size = new System.Drawing.Size(111, 21);
        spnMaxSpawnValuePlanet.TabIndex = 37;
        spnMaxSpawnValuePlanet.ThousandsSeparator = true;
        spnMaxSpawnValuePlanet.Value = new decimal(new int[] { 150, 0, 0, 0 });
        // 
        // spnMinSpawnValuePlanet
        // 
        spnMinSpawnValuePlanet.Location = new System.Drawing.Point(178, 315);
        spnMinSpawnValuePlanet.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnMinSpawnValuePlanet.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
        spnMinSpawnValuePlanet.Name = "spnMinSpawnValuePlanet";
        spnMinSpawnValuePlanet.Size = new System.Drawing.Size(111, 21);
        spnMinSpawnValuePlanet.TabIndex = 36;
        spnMinSpawnValuePlanet.ThousandsSeparator = true;
        // 
        // spnMinValuePlanet
        // 
        spnMinValuePlanet.Location = new System.Drawing.Point(178, 284);
        spnMinValuePlanet.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnMinValuePlanet.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
        spnMinValuePlanet.Name = "spnMinValuePlanet";
        spnMinValuePlanet.Size = new System.Drawing.Size(111, 21);
        spnMinValuePlanet.TabIndex = 35;
        spnMinValuePlanet.ThousandsSeparator = true;
        // 
        // label23
        // 
        label23.AutoSize = true;
        label23.ForeColor = System.Drawing.Color.CornflowerBlue;
        label23.Location = new System.Drawing.Point(296, 257);
        label23.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label23.Name = "label23";
        label23.Size = new System.Drawing.Size(57, 15);
        label23.TabIndex = 34;
        label23.Text = "Asteroids";
        // 
        // label22
        // 
        label22.AutoSize = true;
        label22.ForeColor = System.Drawing.Color.CornflowerBlue;
        label22.Location = new System.Drawing.Point(175, 257);
        label22.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label22.Name = "label22";
        label22.Size = new System.Drawing.Size(48, 15);
        label22.TabIndex = 33;
        label22.Text = "Planets";
        // 
        // label21
        // 
        label21.AutoSize = true;
        label21.ForeColor = System.Drawing.Color.CornflowerBlue;
        label21.Location = new System.Drawing.Point(9, 411);
        label21.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label21.Name = "label21";
        label21.Size = new System.Drawing.Size(65, 15);
        label21.TabIndex = 32;
        label21.Text = "Max Value";
        // 
        // label20
        // 
        label20.AutoSize = true;
        label20.ForeColor = System.Drawing.Color.CornflowerBlue;
        label20.Location = new System.Drawing.Point(9, 284);
        label20.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label20.Name = "label20";
        label20.Size = new System.Drawing.Size(62, 15);
        label20.TabIndex = 31;
        label20.Text = "Min Value";
        // 
        // label19
        // 
        label19.AutoSize = true;
        label19.ForeColor = System.Drawing.Color.CornflowerBlue;
        label19.Location = new System.Drawing.Point(9, 380);
        label19.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label19.Name = "label19";
        label19.Size = new System.Drawing.Size(106, 15);
        label19.TabIndex = 30;
        label19.Text = "Max Spawn Value";
        // 
        // label18
        // 
        label18.AutoSize = true;
        label18.ForeColor = System.Drawing.Color.CornflowerBlue;
        label18.Location = new System.Drawing.Point(9, 317);
        label18.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label18.Name = "label18";
        label18.Size = new System.Drawing.Size(103, 15);
        label18.TabIndex = 29;
        label18.Text = "Min Spawn Value";
        // 
        // spnDepletionResourceRemote
        // 
        spnDepletionResourceRemote.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
        spnDepletionResourceRemote.Location = new System.Drawing.Point(300, 150);
        spnDepletionResourceRemote.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnDepletionResourceRemote.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
        spnDepletionResourceRemote.Name = "spnDepletionResourceRemote";
        spnDepletionResourceRemote.Size = new System.Drawing.Size(111, 21);
        spnDepletionResourceRemote.TabIndex = 27;
        // 
        // spnBonusRemote
        // 
        spnBonusRemote.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
        spnBonusRemote.Location = new System.Drawing.Point(300, 119);
        spnBonusRemote.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnBonusRemote.Name = "spnBonusRemote";
        spnBonusRemote.Size = new System.Drawing.Size(111, 21);
        spnBonusRemote.TabIndex = 26;
        spnBonusRemote.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // spnRateRemote
        // 
        spnRateRemote.Location = new System.Drawing.Point(300, 59);
        spnRateRemote.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnRateRemote.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        spnRateRemote.Name = "spnRateRemote";
        spnRateRemote.Size = new System.Drawing.Size(111, 21);
        spnRateRemote.TabIndex = 25;
        // 
        // spnDepletionResourceStandard
        // 
        spnDepletionResourceStandard.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
        spnDepletionResourceStandard.Location = new System.Drawing.Point(178, 150);
        spnDepletionResourceStandard.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnDepletionResourceStandard.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
        spnDepletionResourceStandard.Name = "spnDepletionResourceStandard";
        spnDepletionResourceStandard.Size = new System.Drawing.Size(111, 21);
        spnDepletionResourceStandard.TabIndex = 23;
        // 
        // spnBonusStandard
        // 
        spnBonusStandard.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
        spnBonusStandard.Location = new System.Drawing.Point(178, 119);
        spnBonusStandard.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnBonusStandard.Name = "spnBonusStandard";
        spnBonusStandard.Size = new System.Drawing.Size(111, 21);
        spnBonusStandard.TabIndex = 22;
        spnBonusStandard.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // spnRateStandard
        // 
        spnRateStandard.Location = new System.Drawing.Point(178, 59);
        spnRateStandard.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnRateStandard.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        spnRateStandard.Name = "spnRateStandard";
        spnRateStandard.Size = new System.Drawing.Size(111, 21);
        spnRateStandard.TabIndex = 21;
        // 
        // label16
        // 
        label16.AutoSize = true;
        label16.ForeColor = System.Drawing.Color.CornflowerBlue;
        label16.Location = new System.Drawing.Point(9, 152);
        label16.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label16.Name = "label16";
        label16.Size = new System.Drawing.Size(122, 15);
        label16.TabIndex = 19;
        label16.Text = "Depletion / Resource";
        // 
        // label15
        // 
        label15.AutoSize = true;
        label15.ForeColor = System.Drawing.Color.CornflowerBlue;
        label15.Location = new System.Drawing.Point(9, 121);
        label15.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label15.Name = "label15";
        label15.Size = new System.Drawing.Size(90, 15);
        label15.TabIndex = 18;
        label15.Text = "Value % Bonus";
        // 
        // label14
        // 
        label14.AutoSize = true;
        label14.ForeColor = System.Drawing.Color.CornflowerBlue;
        label14.Location = new System.Drawing.Point(9, 61);
        label14.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label14.Name = "label14";
        label14.Size = new System.Drawing.Size(47, 15);
        label14.TabIndex = 17;
        label14.Text = "Rate %";
        // 
        // label13
        // 
        label13.AutoSize = true;
        label13.ForeColor = System.Drawing.Color.CornflowerBlue;
        label13.Location = new System.Drawing.Point(296, 37);
        label13.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label13.Name = "label13";
        label13.Size = new System.Drawing.Size(92, 15);
        label13.TabIndex = 16;
        label13.Text = "Remote Mining";
        // 
        // label12
        // 
        label12.AutoSize = true;
        label12.ForeColor = System.Drawing.Color.CornflowerBlue;
        label12.Location = new System.Drawing.Point(175, 37);
        label12.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label12.Name = "label12";
        label12.Size = new System.Drawing.Size(98, 15);
        label12.TabIndex = 15;
        label12.Text = "Standard Mining";
        // 
        // btnLoadResourcePreset
        // 
        btnLoadResourcePreset.BackColor = System.Drawing.Color.Black;
        btnLoadResourcePreset.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnLoadResourcePreset.Location = new System.Drawing.Point(364, 2);
        btnLoadResourcePreset.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnLoadResourcePreset.Name = "btnLoadResourcePreset";
        btnLoadResourcePreset.Size = new System.Drawing.Size(88, 27);
        btnLoadResourcePreset.TabIndex = 13;
        btnLoadResourcePreset.Text = "Load";
        btnLoadResourcePreset.UseVisualStyleBackColor = false;
        btnLoadResourcePreset.Click += btnLoadResourcePreset_Click;
        // 
        // ddlPresets
        // 
        ddlPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        ddlPresets.FormattingEnabled = true;
        ddlPresets.Items.AddRange(new object[] { "Standard, Remote Mining Depletes", "Standard, Remote Mining Doesn't Deplete", "Finite" });
        ddlPresets.Location = new System.Drawing.Point(66, 3);
        ddlPresets.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        ddlPresets.Name = "ddlPresets";
        ddlPresets.Size = new System.Drawing.Size(290, 23);
        ddlPresets.TabIndex = 12;
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.ForeColor = System.Drawing.Color.CornflowerBlue;
        label5.Location = new System.Drawing.Point(4, 7);
        label5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(48, 15);
        label5.TabIndex = 11;
        label5.Text = "Presets";
        // 
        // tabTechnology
        // 
        tabTechnology.BackColor = System.Drawing.Color.Black;
        tabTechnology.Controls.Add(ddlTechUniqueness);
        tabTechnology.Controls.Add(label63);
        tabTechnology.Controls.Add(ddlTechCost);
        tabTechnology.Controls.Add(label61);
        tabTechnology.Controls.Add(label31);
        tabTechnology.Controls.Add(lstTechs);
        tabTechnology.Controls.Add(ddlStartTech);
        tabTechnology.Controls.Add(label30);
        tabTechnology.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        tabTechnology.Location = new System.Drawing.Point(4, 29);
        tabTechnology.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        tabTechnology.Name = "tabTechnology";
        tabTechnology.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
        tabTechnology.Size = new System.Drawing.Size(637, 628);
        tabTechnology.TabIndex = 2;
        tabTechnology.Text = "Technology";
        // 
        // ddlTechUniqueness
        // 
        ddlTechUniqueness.DisplayMember = "Name";
        ddlTechUniqueness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        ddlTechUniqueness.FormattingEnabled = true;
        ddlTechUniqueness.Location = new System.Drawing.Point(495, 7);
        ddlTechUniqueness.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        ddlTechUniqueness.Name = "ddlTechUniqueness";
        ddlTechUniqueness.Size = new System.Drawing.Size(140, 23);
        ddlTechUniqueness.TabIndex = 19;
        ddlTechUniqueness.ValueMember = "Value";
        // 
        // label63
        // 
        label63.AutoSize = true;
        label63.ForeColor = System.Drawing.Color.CornflowerBlue;
        label63.Location = new System.Drawing.Point(288, 10);
        label63.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label63.Name = "label63";
        label63.Size = new System.Drawing.Size(179, 15);
        label63.TabIndex = 18;
        label63.Text = "Tech Known By Other Players is";
        // 
        // ddlTechCost
        // 
        ddlTechCost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        ddlTechCost.FormattingEnabled = true;
        ddlTechCost.Items.AddRange(new object[] { "Low", "Medium", "High" });
        ddlTechCost.Location = new System.Drawing.Point(140, 40);
        ddlTechCost.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        ddlTechCost.Name = "ddlTechCost";
        ddlTechCost.Size = new System.Drawing.Size(140, 23);
        ddlTechCost.TabIndex = 17;
        // 
        // label61
        // 
        label61.AutoSize = true;
        label61.ForeColor = System.Drawing.Color.CornflowerBlue;
        label61.Location = new System.Drawing.Point(4, 44);
        label61.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label61.Name = "label61";
        label61.Size = new System.Drawing.Size(97, 15);
        label61.TabIndex = 16;
        label61.Text = "Technology Cost";
        // 
        // label31
        // 
        label31.AutoSize = true;
        label31.ForeColor = System.Drawing.Color.CornflowerBlue;
        label31.Location = new System.Drawing.Point(4, 97);
        label31.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label31.Name = "label31";
        label31.Size = new System.Drawing.Size(133, 15);
        label31.TabIndex = 15;
        label31.Text = "Available Technologies";
        // 
        // lstTechs
        // 
        lstTechs.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        lstTechs.BackColor = System.Drawing.Color.Black;
        lstTechs.BorderStyle = System.Windows.Forms.BorderStyle.None;
        lstTechs.CheckOnClick = true;
        lstTechs.ColumnWidth = 200;
        lstTechs.ForeColor = System.Drawing.Color.White;
        lstTechs.FormattingEnabled = true;
        lstTechs.Location = new System.Drawing.Point(10, 121);
        lstTechs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        lstTechs.MultiColumn = true;
        lstTechs.Name = "lstTechs";
        lstTechs.Size = new System.Drawing.Size(618, 480);
        lstTechs.TabIndex = 14;
        // 
        // ddlStartTech
        // 
        ddlStartTech.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        ddlStartTech.FormattingEnabled = true;
        ddlStartTech.Items.AddRange(new object[] { "Low", "Medium", "High" });
        ddlStartTech.Location = new System.Drawing.Point(140, 7);
        ddlStartTech.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        ddlStartTech.Name = "ddlStartTech";
        ddlStartTech.Size = new System.Drawing.Size(140, 23);
        ddlStartTech.TabIndex = 13;
        // 
        // label30
        // 
        label30.AutoSize = true;
        label30.ForeColor = System.Drawing.Color.CornflowerBlue;
        label30.Location = new System.Drawing.Point(4, 10);
        label30.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label30.Name = "label30";
        label30.Size = new System.Drawing.Size(111, 15);
        label30.TabIndex = 12;
        label30.Text = "Starting Tech Level";
        // 
        // tabEmpires
        // 
        tabEmpires.BackColor = System.Drawing.Color.Black;
        tabEmpires.Controls.Add(lblMaxBonusResearchFromEmpirePoints);
        tabEmpires.Controls.Add(spnResearchPerUnspentEmpirePoint);
        tabEmpires.Controls.Add(label64);
        tabEmpires.Controls.Add(btnEmpireBottom);
        tabEmpires.Controls.Add(btnEmpireTop);
        tabEmpires.Controls.Add(btnEmpireDown);
        tabEmpires.Controls.Add(btnEmpireUp);
        tabEmpires.Controls.Add(btnToggleAI);
        tabEmpires.Controls.Add(label44);
        tabEmpires.Controls.Add(spnMaxDispersion);
        tabEmpires.Controls.Add(label43);
        tabEmpires.Controls.Add(ddlHomeworldSize);
        tabEmpires.Controls.Add(label42);
        tabEmpires.Controls.Add(btnSaveEmpire);
        tabEmpires.Controls.Add(btnRemoveEmpire);
        tabEmpires.Controls.Add(btnEditEmpire);
        tabEmpires.Controls.Add(btnLoadEmpire);
        tabEmpires.Controls.Add(btnCreateEmpire);
        tabEmpires.Controls.Add(gamePanel1);
        tabEmpires.Controls.Add(label41);
        tabEmpires.Controls.Add(spnResourceStorage);
        tabEmpires.Controls.Add(label40);
        tabEmpires.Controls.Add(spnMinorEmpires);
        tabEmpires.Controls.Add(spnRandomAIs);
        tabEmpires.Controls.Add(spnEmpirePoints);
        tabEmpires.Controls.Add(ddlScoreDisplay);
        tabEmpires.Controls.Add(ddlEmpirePlacement);
        tabEmpires.Controls.Add(spnHomeworlds);
        tabEmpires.Controls.Add(spnStartResearch);
        tabEmpires.Controls.Add(spnStartResources);
        tabEmpires.Controls.Add(label39);
        tabEmpires.Controls.Add(label38);
        tabEmpires.Controls.Add(label37);
        tabEmpires.Controls.Add(label36);
        tabEmpires.Controls.Add(label35);
        tabEmpires.Controls.Add(label34);
        tabEmpires.Controls.Add(label33);
        tabEmpires.Controls.Add(label32);
        tabEmpires.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        tabEmpires.Location = new System.Drawing.Point(4, 29);
        tabEmpires.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        tabEmpires.Name = "tabEmpires";
        tabEmpires.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
        tabEmpires.Size = new System.Drawing.Size(637, 628);
        tabEmpires.TabIndex = 3;
        tabEmpires.Text = "Empires";
        // 
        // lblMaxBonusResearchFromEmpirePoints
        // 
        lblMaxBonusResearchFromEmpirePoints.AutoSize = true;
        lblMaxBonusResearchFromEmpirePoints.ForeColor = System.Drawing.Color.White;
        lblMaxBonusResearchFromEmpirePoints.Location = new System.Drawing.Point(295, 99);
        lblMaxBonusResearchFromEmpirePoints.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        lblMaxBonusResearchFromEmpirePoints.Name = "lblMaxBonusResearchFromEmpirePoints";
        lblMaxBonusResearchFromEmpirePoints.Size = new System.Drawing.Size(235, 15);
        lblMaxBonusResearchFromEmpirePoints.TabIndex = 50;
        lblMaxBonusResearchFromEmpirePoints.Text = "Empires can earn up to 0 bonus research.";
        // 
        // spnResearchPerUnspentEmpirePoint
        // 
        spnResearchPerUnspentEmpirePoint.Location = new System.Drawing.Point(524, 66);
        spnResearchPerUnspentEmpirePoint.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnResearchPerUnspentEmpirePoint.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        spnResearchPerUnspentEmpirePoint.Name = "spnResearchPerUnspentEmpirePoint";
        spnResearchPerUnspentEmpirePoint.Size = new System.Drawing.Size(105, 21);
        spnResearchPerUnspentEmpirePoint.TabIndex = 3;
        spnResearchPerUnspentEmpirePoint.ThousandsSeparator = true;
        spnResearchPerUnspentEmpirePoint.ValueChanged += spnResearchPerUnspentEmpirePoint_ValueChanged;
        // 
        // label64
        // 
        label64.AutoSize = true;
        label64.ForeColor = System.Drawing.Color.CornflowerBlue;
        label64.Location = new System.Drawing.Point(294, 68);
        label64.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label64.Name = "label64";
        label64.Size = new System.Drawing.Size(187, 15);
        label64.TabIndex = 48;
        label64.Text = "Bonus Per Unspent Empire Point\r\n";
        // 
        // btnEmpireBottom
        // 
        btnEmpireBottom.BackColor = System.Drawing.Color.Black;
        btnEmpireBottom.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnEmpireBottom.Location = new System.Drawing.Point(483, 560);
        btnEmpireBottom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnEmpireBottom.Name = "btnEmpireBottom";
        btnEmpireBottom.Size = new System.Drawing.Size(118, 36);
        btnEmpireBottom.TabIndex = 47;
        btnEmpireBottom.Text = "To Bottom";
        btnEmpireBottom.UseVisualStyleBackColor = false;
        btnEmpireBottom.Click += btnEmpireBottom_Click;
        // 
        // btnEmpireTop
        // 
        btnEmpireTop.BackColor = System.Drawing.Color.Black;
        btnEmpireTop.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnEmpireTop.Location = new System.Drawing.Point(483, 517);
        btnEmpireTop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnEmpireTop.Name = "btnEmpireTop";
        btnEmpireTop.Size = new System.Drawing.Size(118, 36);
        btnEmpireTop.TabIndex = 46;
        btnEmpireTop.Text = "To Top";
        btnEmpireTop.UseVisualStyleBackColor = false;
        btnEmpireTop.Click += btnEmpireTop_Click;
        // 
        // btnEmpireDown
        // 
        btnEmpireDown.BackColor = System.Drawing.Color.Black;
        btnEmpireDown.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnEmpireDown.Location = new System.Drawing.Point(358, 560);
        btnEmpireDown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnEmpireDown.Name = "btnEmpireDown";
        btnEmpireDown.Size = new System.Drawing.Size(118, 36);
        btnEmpireDown.TabIndex = 45;
        btnEmpireDown.Text = "Move Down";
        btnEmpireDown.UseVisualStyleBackColor = false;
        btnEmpireDown.Click += btnEmpireDown_Click;
        // 
        // btnEmpireUp
        // 
        btnEmpireUp.BackColor = System.Drawing.Color.Black;
        btnEmpireUp.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnEmpireUp.Location = new System.Drawing.Point(358, 517);
        btnEmpireUp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnEmpireUp.Name = "btnEmpireUp";
        btnEmpireUp.Size = new System.Drawing.Size(118, 36);
        btnEmpireUp.TabIndex = 44;
        btnEmpireUp.Text = "Move Up";
        btnEmpireUp.UseVisualStyleBackColor = false;
        btnEmpireUp.Click += btnEmpireUp_Click;
        // 
        // btnToggleAI
        // 
        btnToggleAI.BackColor = System.Drawing.Color.Black;
        btnToggleAI.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnToggleAI.Location = new System.Drawing.Point(483, 474);
        btnToggleAI.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnToggleAI.Name = "btnToggleAI";
        btnToggleAI.Size = new System.Drawing.Size(118, 36);
        btnToggleAI.TabIndex = 43;
        btnToggleAI.Text = "Toggle AI";
        btnToggleAI.UseVisualStyleBackColor = false;
        btnToggleAI.Click += btnToggleAI_Click;
        // 
        // label44
        // 
        label44.AutoSize = true;
        label44.Location = new System.Drawing.Point(295, 196);
        label44.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label44.Name = "label44";
        label44.Size = new System.Drawing.Size(173, 15);
        label44.TabIndex = 42;
        label44.Text = "warps from central homeworld";
        // 
        // spnMaxDispersion
        // 
        spnMaxDispersion.Location = new System.Drawing.Point(147, 195);
        spnMaxDispersion.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnMaxDispersion.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
        spnMaxDispersion.Name = "spnMaxDispersion";
        spnMaxDispersion.Size = new System.Drawing.Size(140, 21);
        spnMaxDispersion.TabIndex = 7;
        spnMaxDispersion.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // label43
        // 
        label43.AutoSize = true;
        label43.ForeColor = System.Drawing.Color.CornflowerBlue;
        label43.Location = new System.Drawing.Point(10, 197);
        label43.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label43.Name = "label43";
        label43.Size = new System.Drawing.Size(93, 15);
        label43.TabIndex = 40;
        label43.Text = "Max Dispersion";
        // 
        // ddlHomeworldSize
        // 
        ddlHomeworldSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        ddlHomeworldSize.FormattingEnabled = true;
        ddlHomeworldSize.Location = new System.Drawing.Point(146, 128);
        ddlHomeworldSize.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        ddlHomeworldSize.Name = "ddlHomeworldSize";
        ddlHomeworldSize.Size = new System.Drawing.Size(192, 23);
        ddlHomeworldSize.TabIndex = 5;
        // 
        // label42
        // 
        label42.AutoSize = true;
        label42.ForeColor = System.Drawing.Color.CornflowerBlue;
        label42.Location = new System.Drawing.Point(9, 132);
        label42.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label42.Name = "label42";
        label42.Size = new System.Drawing.Size(98, 15);
        label42.TabIndex = 38;
        label42.Text = "Homeworld Size";
        // 
        // btnSaveEmpire
        // 
        btnSaveEmpire.BackColor = System.Drawing.Color.Black;
        btnSaveEmpire.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnSaveEmpire.Location = new System.Drawing.Point(483, 432);
        btnSaveEmpire.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnSaveEmpire.Name = "btnSaveEmpire";
        btnSaveEmpire.Size = new System.Drawing.Size(118, 36);
        btnSaveEmpire.TabIndex = 37;
        btnSaveEmpire.Text = "Save";
        btnSaveEmpire.UseVisualStyleBackColor = false;
        btnSaveEmpire.Click += btnSaveEmpire_Click;
        // 
        // btnRemoveEmpire
        // 
        btnRemoveEmpire.BackColor = System.Drawing.Color.Black;
        btnRemoveEmpire.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnRemoveEmpire.Location = new System.Drawing.Point(358, 474);
        btnRemoveEmpire.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnRemoveEmpire.Name = "btnRemoveEmpire";
        btnRemoveEmpire.Size = new System.Drawing.Size(118, 36);
        btnRemoveEmpire.TabIndex = 36;
        btnRemoveEmpire.Text = "Remove";
        btnRemoveEmpire.UseVisualStyleBackColor = false;
        btnRemoveEmpire.Click += btnRemoveEmpire_Click;
        // 
        // btnEditEmpire
        // 
        btnEditEmpire.BackColor = System.Drawing.Color.Black;
        btnEditEmpire.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnEditEmpire.Location = new System.Drawing.Point(483, 389);
        btnEditEmpire.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnEditEmpire.Name = "btnEditEmpire";
        btnEditEmpire.Size = new System.Drawing.Size(118, 36);
        btnEditEmpire.TabIndex = 35;
        btnEditEmpire.Text = "Edit";
        btnEditEmpire.UseVisualStyleBackColor = false;
        btnEditEmpire.Click += btnEditEmpire_Click;
        // 
        // btnLoadEmpire
        // 
        btnLoadEmpire.BackColor = System.Drawing.Color.Black;
        btnLoadEmpire.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnLoadEmpire.Location = new System.Drawing.Point(358, 432);
        btnLoadEmpire.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnLoadEmpire.Name = "btnLoadEmpire";
        btnLoadEmpire.Size = new System.Drawing.Size(118, 36);
        btnLoadEmpire.TabIndex = 34;
        btnLoadEmpire.Text = "Load";
        btnLoadEmpire.UseVisualStyleBackColor = false;
        btnLoadEmpire.Click += btnLoadEmpire_Click;
        // 
        // btnCreateEmpire
        // 
        btnCreateEmpire.BackColor = System.Drawing.Color.Black;
        btnCreateEmpire.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnCreateEmpire.Location = new System.Drawing.Point(358, 389);
        btnCreateEmpire.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnCreateEmpire.Name = "btnCreateEmpire";
        btnCreateEmpire.Size = new System.Drawing.Size(118, 36);
        btnCreateEmpire.TabIndex = 12;
        btnCreateEmpire.Text = "Create";
        btnCreateEmpire.UseVisualStyleBackColor = false;
        btnCreateEmpire.Click += btnCreateEmpire_Click;
        // 
        // gamePanel1
        // 
        gamePanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        gamePanel1.BackColor = System.Drawing.Color.Black;
        gamePanel1.BorderColor = System.Drawing.Color.CornflowerBlue;
        gamePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        gamePanel1.Controls.Add(lstEmpires);
        gamePanel1.ForeColor = System.Drawing.Color.White;
        gamePanel1.Location = new System.Drawing.Point(13, 389);
        gamePanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        gamePanel1.Name = "gamePanel1";
        gamePanel1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
        gamePanel1.Size = new System.Drawing.Size(338, 227);
        gamePanel1.TabIndex = 32;
        // 
        // lstEmpires
        // 
        lstEmpires.BackColor = System.Drawing.Color.Black;
        lstEmpires.BorderStyle = System.Windows.Forms.BorderStyle.None;
        lstEmpires.Dock = System.Windows.Forms.DockStyle.Fill;
        lstEmpires.ForeColor = System.Drawing.Color.White;
        lstEmpires.Location = new System.Drawing.Point(4, 3);
        lstEmpires.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        lstEmpires.Name = "lstEmpires";
        lstEmpires.Size = new System.Drawing.Size(328, 219);
        lstEmpires.TabIndex = 0;
        lstEmpires.UseCompatibleStateImageBehavior = false;
        lstEmpires.View = System.Windows.Forms.View.List;
        // 
        // label41
        // 
        label41.AutoSize = true;
        label41.ForeColor = System.Drawing.Color.CornflowerBlue;
        label41.Location = new System.Drawing.Point(14, 365);
        label41.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label41.Name = "label41";
        label41.Size = new System.Drawing.Size(90, 15);
        label41.TabIndex = 31;
        label41.Text = "Player Empires";
        // 
        // spnResourceStorage
        // 
        spnResourceStorage.Location = new System.Drawing.Point(147, 37);
        spnResourceStorage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnResourceStorage.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        spnResourceStorage.Name = "spnResourceStorage";
        spnResourceStorage.Size = new System.Drawing.Size(140, 21);
        spnResourceStorage.TabIndex = 1;
        spnResourceStorage.Value = new decimal(new int[] { 50000, 0, 0, 0 });
        spnResourceStorage.ValueChanged += spnResourceStorage_ValueChanged;
        // 
        // label40
        // 
        label40.AutoSize = true;
        label40.ForeColor = System.Drawing.Color.CornflowerBlue;
        label40.Location = new System.Drawing.Point(7, 39);
        label40.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label40.Name = "label40";
        label40.Size = new System.Drawing.Size(106, 15);
        label40.TabIndex = 29;
        label40.Text = "Resource Storage";
        // 
        // spnMinorEmpires
        // 
        spnMinorEmpires.Location = new System.Drawing.Point(146, 322);
        spnMinorEmpires.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnMinorEmpires.Name = "spnMinorEmpires";
        spnMinorEmpires.Size = new System.Drawing.Size(140, 21);
        spnMinorEmpires.TabIndex = 11;
        // 
        // spnRandomAIs
        // 
        spnRandomAIs.Location = new System.Drawing.Point(146, 291);
        spnRandomAIs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnRandomAIs.Name = "spnRandomAIs";
        spnRandomAIs.Size = new System.Drawing.Size(140, 21);
        spnRandomAIs.TabIndex = 10;
        // 
        // spnEmpirePoints
        // 
        spnEmpirePoints.Location = new System.Drawing.Point(146, 260);
        spnEmpirePoints.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnEmpirePoints.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        spnEmpirePoints.Name = "spnEmpirePoints";
        spnEmpirePoints.Size = new System.Drawing.Size(140, 21);
        spnEmpirePoints.TabIndex = 9;
        spnEmpirePoints.Value = new decimal(new int[] { 2000, 0, 0, 0 });
        spnEmpirePoints.ValueChanged += spnEmpirePoints_ValueChanged;
        // 
        // ddlScoreDisplay
        // 
        ddlScoreDisplay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        ddlScoreDisplay.FormattingEnabled = true;
        ddlScoreDisplay.Items.AddRange(new object[] { "Own Only (No Rankings)", "Own Only (Ranked)", "Allies Only (No Rankings)", "Allies Only (Ranked)", "All" });
        ddlScoreDisplay.Location = new System.Drawing.Point(146, 226);
        ddlScoreDisplay.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        ddlScoreDisplay.Name = "ddlScoreDisplay";
        ddlScoreDisplay.Size = new System.Drawing.Size(193, 23);
        ddlScoreDisplay.TabIndex = 8;
        // 
        // ddlEmpirePlacement
        // 
        ddlEmpirePlacement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        ddlEmpirePlacement.FormattingEnabled = true;
        ddlEmpirePlacement.Items.AddRange(new object[] { "Can Start In Same System", "Different Systems", "Equidistant" });
        ddlEmpirePlacement.Location = new System.Drawing.Point(147, 162);
        ddlEmpirePlacement.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        ddlEmpirePlacement.Name = "ddlEmpirePlacement";
        ddlEmpirePlacement.Size = new System.Drawing.Size(192, 23);
        ddlEmpirePlacement.TabIndex = 6;
        // 
        // spnHomeworlds
        // 
        spnHomeworlds.Location = new System.Drawing.Point(146, 97);
        spnHomeworlds.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnHomeworlds.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
        spnHomeworlds.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        spnHomeworlds.Name = "spnHomeworlds";
        spnHomeworlds.Size = new System.Drawing.Size(140, 21);
        spnHomeworlds.TabIndex = 4;
        spnHomeworlds.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // spnStartResearch
        // 
        spnStartResearch.Location = new System.Drawing.Point(147, 66);
        spnStartResearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnStartResearch.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        spnStartResearch.Name = "spnStartResearch";
        spnStartResearch.Size = new System.Drawing.Size(140, 21);
        spnStartResearch.TabIndex = 2;
        spnStartResearch.Value = new decimal(new int[] { 20000, 0, 0, 0 });
        // 
        // spnStartResources
        // 
        spnStartResources.Location = new System.Drawing.Point(147, 8);
        spnStartResources.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnStartResources.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        spnStartResources.Name = "spnStartResources";
        spnStartResources.Size = new System.Drawing.Size(140, 21);
        spnStartResources.TabIndex = 0;
        spnStartResources.Value = new decimal(new int[] { 20000, 0, 0, 0 });
        // 
        // label39
        // 
        label39.AutoSize = true;
        label39.ForeColor = System.Drawing.Color.CornflowerBlue;
        label39.Location = new System.Drawing.Point(10, 324);
        label39.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label39.Name = "label39";
        label39.Size = new System.Drawing.Size(88, 15);
        label39.TabIndex = 20;
        label39.Text = "Minor Empires";
        // 
        // label38
        // 
        label38.AutoSize = true;
        label38.ForeColor = System.Drawing.Color.CornflowerBlue;
        label38.Location = new System.Drawing.Point(10, 293);
        label38.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label38.Name = "label38";
        label38.Size = new System.Drawing.Size(74, 15);
        label38.TabIndex = 19;
        label38.Text = "Random AIs";
        // 
        // label37
        // 
        label37.AutoSize = true;
        label37.ForeColor = System.Drawing.Color.CornflowerBlue;
        label37.Location = new System.Drawing.Point(10, 260);
        label37.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label37.Name = "label37";
        label37.Size = new System.Drawing.Size(84, 15);
        label37.TabIndex = 18;
        label37.Text = "Empire Points";
        // 
        // label36
        // 
        label36.AutoSize = true;
        label36.ForeColor = System.Drawing.Color.CornflowerBlue;
        label36.Location = new System.Drawing.Point(10, 230);
        label36.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label36.Name = "label36";
        label36.Size = new System.Drawing.Size(82, 15);
        label36.TabIndex = 17;
        label36.Text = "Score Display";
        // 
        // label35
        // 
        label35.AutoSize = true;
        label35.ForeColor = System.Drawing.Color.CornflowerBlue;
        label35.Location = new System.Drawing.Point(10, 165);
        label35.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label35.Name = "label35";
        label35.Size = new System.Drawing.Size(66, 15);
        label35.TabIndex = 16;
        label35.Text = "Placement";
        // 
        // label34
        // 
        label34.AutoSize = true;
        label34.ForeColor = System.Drawing.Color.CornflowerBlue;
        label34.Location = new System.Drawing.Point(9, 99);
        label34.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label34.Name = "label34";
        label34.Size = new System.Drawing.Size(77, 15);
        label34.TabIndex = 15;
        label34.Text = "Homeworlds";
        // 
        // label33
        // 
        label33.AutoSize = true;
        label33.ForeColor = System.Drawing.Color.CornflowerBlue;
        label33.Location = new System.Drawing.Point(7, 68);
        label33.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label33.Name = "label33";
        label33.Size = new System.Drawing.Size(105, 15);
        label33.TabIndex = 14;
        label33.Text = "Starting Research";
        // 
        // label32
        // 
        label32.AutoSize = true;
        label32.ForeColor = System.Drawing.Color.CornflowerBlue;
        label32.Location = new System.Drawing.Point(7, 10);
        label32.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label32.Name = "label32";
        label32.Size = new System.Drawing.Size(111, 15);
        label32.TabIndex = 13;
        label32.Text = "Starting Resources";
        // 
        // tabVictory
        // 
        tabVictory.BackColor = System.Drawing.Color.Black;
        tabVictory.Controls.Add(label51);
        tabVictory.Controls.Add(spnVictoryDelay);
        tabVictory.Controls.Add(label50);
        tabVictory.Controls.Add(label49);
        tabVictory.Controls.Add(spnVictoryPeace);
        tabVictory.Controls.Add(chkVictoryPeace);
        tabVictory.Controls.Add(label48);
        tabVictory.Controls.Add(spnVictoryTech);
        tabVictory.Controls.Add(chkVictoryTech);
        tabVictory.Controls.Add(label47);
        tabVictory.Controls.Add(spnVictoryScorePercent);
        tabVictory.Controls.Add(chkVictoryScorePercent);
        tabVictory.Controls.Add(label46);
        tabVictory.Controls.Add(spnVictoryTurns);
        tabVictory.Controls.Add(chkVictoryTurns);
        tabVictory.Controls.Add(spnVictoryScore);
        tabVictory.Controls.Add(chkVictoryScore);
        tabVictory.Controls.Add(chkVictoryEliminateMajorEmpires);
        tabVictory.Controls.Add(label45);
        tabVictory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
        tabVictory.Location = new System.Drawing.Point(4, 29);
        tabVictory.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        tabVictory.Name = "tabVictory";
        tabVictory.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
        tabVictory.Size = new System.Drawing.Size(637, 628);
        tabVictory.TabIndex = 4;
        tabVictory.Text = "Victory";
        // 
        // label51
        // 
        label51.AutoSize = true;
        label51.ForeColor = System.Drawing.Color.CornflowerBlue;
        label51.Location = new System.Drawing.Point(430, 260);
        label51.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label51.Name = "label51";
        label51.Size = new System.Drawing.Size(34, 15);
        label51.TabIndex = 18;
        label51.Text = "turns";
        // 
        // spnVictoryDelay
        // 
        spnVictoryDelay.Increment = new decimal(new int[] { 10, 0, 0, 0 });
        spnVictoryDelay.Location = new System.Drawing.Point(284, 257);
        spnVictoryDelay.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnVictoryDelay.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        spnVictoryDelay.Name = "spnVictoryDelay";
        spnVictoryDelay.Size = new System.Drawing.Size(140, 21);
        spnVictoryDelay.TabIndex = 17;
        spnVictoryDelay.ThousandsSeparator = true;
        // 
        // label50
        // 
        label50.AutoSize = true;
        label50.ForeColor = System.Drawing.Color.CornflowerBlue;
        label50.Location = new System.Drawing.Point(9, 260);
        label50.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label50.Name = "label50";
        label50.Size = new System.Drawing.Size(229, 15);
        label50.TabIndex = 16;
        label50.Text = "Custom victory conditions take effect after";
        // 
        // label49
        // 
        label49.AutoSize = true;
        label49.Location = new System.Drawing.Point(368, 205);
        label49.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label49.Name = "label49";
        label49.Size = new System.Drawing.Size(181, 15);
        label49.TabIndex = 15;
        label49.Text = "turns (Non-Aggression or better)";
        // 
        // spnVictoryPeace
        // 
        spnVictoryPeace.Increment = new decimal(new int[] { 10, 0, 0, 0 });
        spnVictoryPeace.Location = new System.Drawing.Point(220, 203);
        spnVictoryPeace.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnVictoryPeace.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
        spnVictoryPeace.Name = "spnVictoryPeace";
        spnVictoryPeace.Size = new System.Drawing.Size(140, 21);
        spnVictoryPeace.TabIndex = 14;
        spnVictoryPeace.ThousandsSeparator = true;
        spnVictoryPeace.Value = new decimal(new int[] { 10, 0, 0, 0 });
        // 
        // chkVictoryPeace
        // 
        chkVictoryPeace.AutoSize = true;
        chkVictoryPeace.Location = new System.Drawing.Point(12, 204);
        chkVictoryPeace.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        chkVictoryPeace.Name = "chkVictoryPeace";
        chkVictoryPeace.Size = new System.Drawing.Size(173, 19);
        chkVictoryPeace.TabIndex = 13;
        chkVictoryPeace.Text = "Maintain galactic peace for";
        chkVictoryPeace.UseVisualStyleBackColor = true;
        // 
        // label48
        // 
        label48.AutoSize = true;
        label48.Location = new System.Drawing.Point(258, 177);
        label48.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label48.Name = "label48";
        label48.Size = new System.Drawing.Size(270, 15);
        label48.TabIndex = 12;
        label48.Text = "% of all non-racial, non-unique technology levels";
        // 
        // spnVictoryTech
        // 
        spnVictoryTech.Increment = new decimal(new int[] { 10, 0, 0, 0 });
        spnVictoryTech.Location = new System.Drawing.Point(111, 174);
        spnVictoryTech.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnVictoryTech.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
        spnVictoryTech.Name = "spnVictoryTech";
        spnVictoryTech.Size = new System.Drawing.Size(140, 21);
        spnVictoryTech.TabIndex = 11;
        spnVictoryTech.ThousandsSeparator = true;
        spnVictoryTech.Value = new decimal(new int[] { 100, 0, 0, 0 });
        // 
        // chkVictoryTech
        // 
        chkVictoryTech.AutoSize = true;
        chkVictoryTech.Location = new System.Drawing.Point(12, 175);
        chkVictoryTech.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        chkVictoryTech.Name = "chkVictoryTech";
        chkVictoryTech.Size = new System.Drawing.Size(79, 19);
        chkVictoryTech.TabIndex = 10;
        chkVictoryTech.Text = "Research";
        chkVictoryTech.UseVisualStyleBackColor = true;
        // 
        // label47
        // 
        label47.AutoSize = true;
        label47.Location = new System.Drawing.Point(288, 119);
        label47.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label47.Name = "label47";
        label47.Size = new System.Drawing.Size(205, 15);
        label47.TabIndex = 9;
        label47.Text = "% of the second place player's score";
        // 
        // spnVictoryScorePercent
        // 
        spnVictoryScorePercent.Increment = new decimal(new int[] { 10, 0, 0, 0 });
        spnVictoryScorePercent.Location = new System.Drawing.Point(141, 117);
        spnVictoryScorePercent.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnVictoryScorePercent.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        spnVictoryScorePercent.Minimum = new decimal(new int[] { 150, 0, 0, 0 });
        spnVictoryScorePercent.Name = "spnVictoryScorePercent";
        spnVictoryScorePercent.Size = new System.Drawing.Size(140, 21);
        spnVictoryScorePercent.TabIndex = 8;
        spnVictoryScorePercent.ThousandsSeparator = true;
        spnVictoryScorePercent.Value = new decimal(new int[] { 200, 0, 0, 0 });
        // 
        // chkVictoryScorePercent
        // 
        chkVictoryScorePercent.AutoSize = true;
        chkVictoryScorePercent.Location = new System.Drawing.Point(12, 118);
        chkVictoryScorePercent.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        chkVictoryScorePercent.Name = "chkVictoryScorePercent";
        chkVictoryScorePercent.Size = new System.Drawing.Size(105, 19);
        chkVictoryScorePercent.TabIndex = 7;
        chkVictoryScorePercent.Text = "Reach a score";
        chkVictoryScorePercent.UseVisualStyleBackColor = true;
        // 
        // label46
        // 
        label46.AutoSize = true;
        label46.Location = new System.Drawing.Point(261, 148);
        label46.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label46.Name = "label46";
        label46.Size = new System.Drawing.Size(34, 15);
        label46.TabIndex = 6;
        label46.Text = "turns";
        // 
        // spnVictoryTurns
        // 
        spnVictoryTurns.Increment = new decimal(new int[] { 10, 0, 0, 0 });
        spnVictoryTurns.Location = new System.Drawing.Point(114, 145);
        spnVictoryTurns.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnVictoryTurns.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        spnVictoryTurns.Name = "spnVictoryTurns";
        spnVictoryTurns.Size = new System.Drawing.Size(140, 21);
        spnVictoryTurns.TabIndex = 5;
        spnVictoryTurns.ThousandsSeparator = true;
        spnVictoryTurns.Value = new decimal(new int[] { 100, 0, 0, 0 });
        // 
        // chkVictoryTurns
        // 
        chkVictoryTurns.AutoSize = true;
        chkVictoryTurns.Location = new System.Drawing.Point(12, 147);
        chkVictoryTurns.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        chkVictoryTurns.Name = "chkVictoryTurns";
        chkVictoryTurns.Size = new System.Drawing.Size(82, 19);
        chkVictoryTurns.TabIndex = 4;
        chkVictoryTurns.Text = "Survive for";
        chkVictoryTurns.UseVisualStyleBackColor = true;
        // 
        // spnVictoryScore
        // 
        spnVictoryScore.Increment = new decimal(new int[] { 10000, 0, 0, 0 });
        spnVictoryScore.Location = new System.Drawing.Point(156, 88);
        spnVictoryScore.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        spnVictoryScore.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
        spnVictoryScore.Minimum = new decimal(new int[] { 10000, 0, 0, 0 });
        spnVictoryScore.Name = "spnVictoryScore";
        spnVictoryScore.Size = new System.Drawing.Size(140, 21);
        spnVictoryScore.TabIndex = 3;
        spnVictoryScore.ThousandsSeparator = true;
        spnVictoryScore.Value = new decimal(new int[] { 5000000, 0, 0, 0 });
        // 
        // chkVictoryScore
        // 
        chkVictoryScore.AutoSize = true;
        chkVictoryScore.Location = new System.Drawing.Point(12, 89);
        chkVictoryScore.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        chkVictoryScore.Name = "chkVictoryScore";
        chkVictoryScore.Size = new System.Drawing.Size(118, 19);
        chkVictoryScore.TabIndex = 2;
        chkVictoryScore.Text = "Reach a score of";
        chkVictoryScore.UseVisualStyleBackColor = true;
        // 
        // chkVictoryEliminateMajorEmpires
        // 
        chkVictoryEliminateMajorEmpires.AutoSize = true;
        chkVictoryEliminateMajorEmpires.Location = new System.Drawing.Point(12, 60);
        chkVictoryEliminateMajorEmpires.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        chkVictoryEliminateMajorEmpires.Name = "chkVictoryEliminateMajorEmpires";
        chkVictoryEliminateMajorEmpires.Size = new System.Drawing.Size(365, 19);
        chkVictoryEliminateMajorEmpires.TabIndex = 1;
        chkVictoryEliminateMajorEmpires.Text = "Eliminate all other major empires (minor empires may survive)";
        chkVictoryEliminateMajorEmpires.UseVisualStyleBackColor = true;
        // 
        // label45
        // 
        label45.ForeColor = System.Drawing.Color.CornflowerBlue;
        label45.Location = new System.Drawing.Point(8, 8);
        label45.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label45.Name = "label45";
        label45.Size = new System.Drawing.Size(621, 47);
        label45.TabIndex = 0;
        label45.Text = "The standard victory condition is elimination of all other major and minor empires. Additional optional victory conditions can be selected below.";
        // 
        // tabSettings
        // 
        tabSettings.BackColor = System.Drawing.Color.Black;
        tabSettings.Controls.Add(chkAllowAnalysis);
        tabSettings.Controls.Add(label60);
        tabSettings.Controls.Add(chkColonizeOnlyHWSurface);
        tabSettings.Controls.Add(label59);
        tabSettings.Controls.Add(chkColonizeOnlyBreathable);
        tabSettings.Controls.Add(label58);
        tabSettings.Controls.Add(chkUniqueRuins);
        tabSettings.Controls.Add(label57);
        tabSettings.Controls.Add(chkRandomRuins);
        tabSettings.Controls.Add(label56);
        tabSettings.Controls.Add(chkAllowIntel);
        tabSettings.Controls.Add(label55);
        tabSettings.Controls.Add(chkAllowSurrender);
        tabSettings.Controls.Add(label54);
        tabSettings.Controls.Add(label53);
        tabSettings.Controls.Add(label52);
        tabSettings.Controls.Add(ddlAllowedTrades);
        tabSettings.Controls.Add(chkHumansVsAI);
        tabSettings.Controls.Add(txtGalaxyName);
        tabSettings.Controls.Add(labelName);
        tabSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        tabSettings.Location = new System.Drawing.Point(4, 29);
        tabSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        tabSettings.Name = "tabSettings";
        tabSettings.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
        tabSettings.Size = new System.Drawing.Size(637, 628);
        tabSettings.TabIndex = 5;
        tabSettings.Text = "Settings";
        // 
        // chkAllowAnalysis
        // 
        chkAllowAnalysis.AutoSize = true;
        chkAllowAnalysis.Checked = true;
        chkAllowAnalysis.CheckState = System.Windows.Forms.CheckState.Checked;
        chkAllowAnalysis.Location = new System.Drawing.Point(191, 141);
        chkAllowAnalysis.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        chkAllowAnalysis.Name = "chkAllowAnalysis";
        chkAllowAnalysis.Size = new System.Drawing.Size(15, 14);
        chkAllowAnalysis.TabIndex = 44;
        chkAllowAnalysis.UseVisualStyleBackColor = true;
        // 
        // label60
        // 
        label60.AutoSize = true;
        label60.ForeColor = System.Drawing.Color.CornflowerBlue;
        label60.Location = new System.Drawing.Point(12, 140);
        label60.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label60.Name = "label60";
        label60.Size = new System.Drawing.Size(83, 15);
        label60.TabIndex = 43;
        label60.Text = "Allow Analysis";
        // 
        // chkColonizeOnlyHWSurface
        // 
        chkColonizeOnlyHWSurface.AutoSize = true;
        chkColonizeOnlyHWSurface.Location = new System.Drawing.Point(191, 234);
        chkColonizeOnlyHWSurface.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        chkColonizeOnlyHWSurface.Name = "chkColonizeOnlyHWSurface";
        chkColonizeOnlyHWSurface.Size = new System.Drawing.Size(15, 14);
        chkColonizeOnlyHWSurface.TabIndex = 42;
        chkColonizeOnlyHWSurface.UseVisualStyleBackColor = true;
        // 
        // label59
        // 
        label59.AutoSize = true;
        label59.ForeColor = System.Drawing.Color.CornflowerBlue;
        label59.Location = new System.Drawing.Point(12, 233);
        label59.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label59.Name = "label59";
        label59.Size = new System.Drawing.Size(150, 15);
        label59.TabIndex = 41;
        label59.Text = "Colonize Only HW Surface";
        // 
        // chkColonizeOnlyBreathable
        // 
        chkColonizeOnlyBreathable.AutoSize = true;
        chkColonizeOnlyBreathable.Location = new System.Drawing.Point(191, 211);
        chkColonizeOnlyBreathable.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        chkColonizeOnlyBreathable.Name = "chkColonizeOnlyBreathable";
        chkColonizeOnlyBreathable.Size = new System.Drawing.Size(15, 14);
        chkColonizeOnlyBreathable.TabIndex = 40;
        chkColonizeOnlyBreathable.UseVisualStyleBackColor = true;
        // 
        // label58
        // 
        label58.AutoSize = true;
        label58.ForeColor = System.Drawing.Color.CornflowerBlue;
        label58.Location = new System.Drawing.Point(12, 210);
        label58.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label58.Name = "label58";
        label58.Size = new System.Drawing.Size(145, 15);
        label58.TabIndex = 39;
        label58.Text = "Colonize Only Breathable";
        // 
        // chkUniqueRuins
        // 
        chkUniqueRuins.AutoSize = true;
        chkUniqueRuins.Checked = true;
        chkUniqueRuins.CheckState = System.Windows.Forms.CheckState.Checked;
        chkUniqueRuins.Location = new System.Drawing.Point(191, 187);
        chkUniqueRuins.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        chkUniqueRuins.Name = "chkUniqueRuins";
        chkUniqueRuins.Size = new System.Drawing.Size(15, 14);
        chkUniqueRuins.TabIndex = 38;
        chkUniqueRuins.UseVisualStyleBackColor = true;
        // 
        // label57
        // 
        label57.AutoSize = true;
        label57.ForeColor = System.Drawing.Color.CornflowerBlue;
        label57.Location = new System.Drawing.Point(12, 186);
        label57.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label57.Name = "label57";
        label57.Size = new System.Drawing.Size(82, 15);
        label57.TabIndex = 37;
        label57.Text = "Unique Ruins";
        // 
        // chkRandomRuins
        // 
        chkRandomRuins.AutoSize = true;
        chkRandomRuins.Checked = true;
        chkRandomRuins.CheckState = System.Windows.Forms.CheckState.Checked;
        chkRandomRuins.Location = new System.Drawing.Point(191, 164);
        chkRandomRuins.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        chkRandomRuins.Name = "chkRandomRuins";
        chkRandomRuins.Size = new System.Drawing.Size(15, 14);
        chkRandomRuins.TabIndex = 36;
        chkRandomRuins.UseVisualStyleBackColor = true;
        // 
        // label56
        // 
        label56.AutoSize = true;
        label56.ForeColor = System.Drawing.Color.CornflowerBlue;
        label56.Location = new System.Drawing.Point(12, 163);
        label56.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label56.Name = "label56";
        label56.Size = new System.Drawing.Size(90, 15);
        label56.TabIndex = 35;
        label56.Text = "Random Ruins";
        // 
        // chkAllowIntel
        // 
        chkAllowIntel.AutoSize = true;
        chkAllowIntel.Checked = true;
        chkAllowIntel.CheckState = System.Windows.Forms.CheckState.Checked;
        chkAllowIntel.Location = new System.Drawing.Point(191, 118);
        chkAllowIntel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        chkAllowIntel.Name = "chkAllowIntel";
        chkAllowIntel.Size = new System.Drawing.Size(15, 14);
        chkAllowIntel.TabIndex = 34;
        chkAllowIntel.UseVisualStyleBackColor = true;
        // 
        // label55
        // 
        label55.AutoSize = true;
        label55.ForeColor = System.Drawing.Color.CornflowerBlue;
        label55.Location = new System.Drawing.Point(12, 117);
        label55.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label55.Name = "label55";
        label55.Size = new System.Drawing.Size(102, 15);
        label55.TabIndex = 33;
        label55.Text = "Allow Intelligence";
        // 
        // chkAllowSurrender
        // 
        chkAllowSurrender.AutoSize = true;
        chkAllowSurrender.Checked = true;
        chkAllowSurrender.CheckState = System.Windows.Forms.CheckState.Checked;
        chkAllowSurrender.Location = new System.Drawing.Point(191, 95);
        chkAllowSurrender.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        chkAllowSurrender.Name = "chkAllowSurrender";
        chkAllowSurrender.Size = new System.Drawing.Size(15, 14);
        chkAllowSurrender.TabIndex = 32;
        chkAllowSurrender.UseVisualStyleBackColor = true;
        // 
        // label54
        // 
        label54.AutoSize = true;
        label54.ForeColor = System.Drawing.Color.CornflowerBlue;
        label54.Location = new System.Drawing.Point(12, 93);
        label54.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label54.Name = "label54";
        label54.Size = new System.Drawing.Size(94, 15);
        label54.TabIndex = 31;
        label54.Text = "Allow Surrender";
        // 
        // label53
        // 
        label53.AutoSize = true;
        label53.ForeColor = System.Drawing.Color.CornflowerBlue;
        label53.Location = new System.Drawing.Point(9, 65);
        label53.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label53.Name = "label53";
        label53.Size = new System.Drawing.Size(91, 15);
        label53.TabIndex = 30;
        label53.Text = "Allowed Trades";
        // 
        // label52
        // 
        label52.AutoSize = true;
        label52.ForeColor = System.Drawing.Color.CornflowerBlue;
        label52.Location = new System.Drawing.Point(9, 37);
        label52.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        label52.Name = "label52";
        label52.Size = new System.Drawing.Size(84, 15);
        label52.TabIndex = 29;
        label52.Text = "Humans vs. AI";
        // 
        // ddlAllowedTrades
        // 
        ddlAllowedTrades.DisplayMember = "Name";
        ddlAllowedTrades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        ddlAllowedTrades.FormattingEnabled = true;
        ddlAllowedTrades.Location = new System.Drawing.Point(191, 61);
        ddlAllowedTrades.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        ddlAllowedTrades.Name = "ddlAllowedTrades";
        ddlAllowedTrades.Size = new System.Drawing.Size(177, 23);
        ddlAllowedTrades.TabIndex = 28;
        ddlAllowedTrades.ValueMember = "Value";
        // 
        // chkHumansVsAI
        // 
        chkHumansVsAI.AutoSize = true;
        chkHumansVsAI.Location = new System.Drawing.Point(191, 38);
        chkHumansVsAI.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        chkHumansVsAI.Name = "chkHumansVsAI";
        chkHumansVsAI.Size = new System.Drawing.Size(15, 14);
        chkHumansVsAI.TabIndex = 27;
        chkHumansVsAI.UseVisualStyleBackColor = true;
        // 
        // txtGalaxyName
        // 
        txtGalaxyName.Location = new System.Drawing.Point(191, 7);
        txtGalaxyName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        txtGalaxyName.Name = "txtGalaxyName";
        txtGalaxyName.Size = new System.Drawing.Size(177, 21);
        txtGalaxyName.TabIndex = 26;
        // 
        // labelName
        // 
        labelName.AutoSize = true;
        labelName.ForeColor = System.Drawing.Color.CornflowerBlue;
        labelName.Location = new System.Drawing.Point(8, 10);
        labelName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        labelName.Name = "labelName";
        labelName.Size = new System.Drawing.Size(78, 15);
        labelName.TabIndex = 25;
        labelName.Text = "Game Name";
        // 
        // btnStart
        // 
        btnStart.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        btnStart.BackColor = System.Drawing.Color.Black;
        btnStart.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnStart.Location = new System.Drawing.Point(558, 668);
        btnStart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnStart.Name = "btnStart";
        btnStart.Size = new System.Drawing.Size(88, 27);
        btnStart.TabIndex = 0;
        btnStart.Text = "Start";
        btnStart.UseVisualStyleBackColor = false;
        btnStart.Click += btnStart_Click;
        // 
        // btnCancel
        // 
        btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        btnCancel.BackColor = System.Drawing.Color.Black;
        btnCancel.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnCancel.Location = new System.Drawing.Point(463, 668);
        btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new System.Drawing.Size(88, 27);
        btnCancel.TabIndex = 1;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = false;
        btnCancel.Click += btnCancel_Click;
        // 
        // btnLoadSetup
        // 
        btnLoadSetup.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        btnLoadSetup.BackColor = System.Drawing.Color.Black;
        btnLoadSetup.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnLoadSetup.Location = new System.Drawing.Point(7, 668);
        btnLoadSetup.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnLoadSetup.Name = "btnLoadSetup";
        btnLoadSetup.Size = new System.Drawing.Size(88, 27);
        btnLoadSetup.TabIndex = 2;
        btnLoadSetup.Text = "Load Setup";
        btnLoadSetup.UseVisualStyleBackColor = false;
        btnLoadSetup.Click += btnLoadSetup_Click;
        // 
        // btnSaveSetup
        // 
        btnSaveSetup.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        btnSaveSetup.BackColor = System.Drawing.Color.Black;
        btnSaveSetup.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnSaveSetup.Location = new System.Drawing.Point(102, 668);
        btnSaveSetup.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnSaveSetup.Name = "btnSaveSetup";
        btnSaveSetup.Size = new System.Drawing.Size(88, 27);
        btnSaveSetup.TabIndex = 3;
        btnSaveSetup.Text = "Save Setup";
        btnSaveSetup.UseVisualStyleBackColor = false;
        btnSaveSetup.Click += btnSaveSetup_Click;
        // 
        // progressBar
        // 
        progressBar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        progressBar.Location = new System.Drawing.Point(196, 675);
        progressBar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        progressBar.Name = "progressBar";
        progressBar.Size = new System.Drawing.Size(260, 16);
        progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
        progressBar.TabIndex = 5;
        progressBar.Visible = false;
        // 
        // blazorWebView1
        // 
        blazorWebView1.Location = new System.Drawing.Point(347, 205);
        blazorWebView1.Name = "blazorWebView1";
        blazorWebView1.Size = new System.Drawing.Size(275, 104);
        blazorWebView1.StartPath = "/";
        blazorWebView1.TabIndex = 33;
        blazorWebView1.Text = "blazorWebView1";
        // 
        // GameSetupForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.Black;
        ClientSize = new System.Drawing.Size(659, 705);
        Controls.Add(progressBar);
        Controls.Add(btnSaveSetup);
        Controls.Add(btnLoadSetup);
        Controls.Add(btnCancel);
        Controls.Add(btnStart);
        Controls.Add(tabs);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        MaximizeBox = false;
        Name = "GameSetupForm";
        ShowInTaskbar = false;
        Text = "Game Setup";
        Load += GameSetupForm_Load;
        tabs.ResumeLayout(false);
        tabGalaxy.ResumeLayout(false);
        tabGalaxy.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)spnSeed).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnSystemGroups).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnHeight).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnWidth).EndInit();
        ((System.ComponentModel.ISupportInitialize)warpPointPlacementStrategyBindingSource).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnStarSystems).EndInit();
        ((System.ComponentModel.ISupportInitialize)galaxyTemplateBindingSource).EndInit();
        tabResources.ResumeLayout(false);
        tabResources.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)picMiningGraph).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnMiningRate).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnStartValue).EndInit();
        ((System.ComponentModel.ISupportInitialize)picValueGraph).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnHomeworldValue).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnDepletionTurnRemote).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnDepletionTurnStandard).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnMaxValuePlanet).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnMaxSpawnValueAsteroid).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnMinSpawnValueAsteroid).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnMinValueAsteroid).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnMaxSpawnValuePlanet).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnMinSpawnValuePlanet).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnMinValuePlanet).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnDepletionResourceRemote).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnBonusRemote).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnRateRemote).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnDepletionResourceStandard).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnBonusStandard).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnRateStandard).EndInit();
        tabTechnology.ResumeLayout(false);
        tabTechnology.PerformLayout();
        tabEmpires.ResumeLayout(false);
        tabEmpires.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)spnResearchPerUnspentEmpirePoint).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnMaxDispersion).EndInit();
        gamePanel1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)spnResourceStorage).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnMinorEmpires).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnRandomAIs).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnEmpirePoints).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnHomeworlds).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnStartResearch).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnStartResources).EndInit();
        tabVictory.ResumeLayout(false);
        tabVictory.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)spnVictoryDelay).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnVictoryPeace).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnVictoryTech).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnVictoryScorePercent).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnVictoryTurns).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnVictoryScore).EndInit();
        tabSettings.ResumeLayout(false);
        tabSettings.PerformLayout();
        ResumeLayout(false);
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
	private Controls.GameButton btnGenerateSeed;
    private Microsoft.AspNetCore.Components.WebView.WindowsForms.BlazorWebView blazorWebView1;
}