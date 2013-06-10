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
			this.tabs = new FrEee.WinForms.Controls.GameTabControl();
			this.tabGalaxy = new System.Windows.Forms.TabPage();
			this.label11 = new System.Windows.Forms.Label();
			this.spnSystemGroups = new System.Windows.Forms.NumericUpDown();
			this.txtWarpPointLocation = new System.Windows.Forms.Label();
			this.txtGalaxyName = new System.Windows.Forms.TextBox();
			this.labelName = new System.Windows.Forms.Label();
			this.ddlEventSeverity = new System.Windows.Forms.ComboBox();
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
			this.tabEmpires = new System.Windows.Forms.TabPage();
			this.tabVictory = new System.Windows.Forms.TabPage();
			this.tabSettings = new System.Windows.Forms.TabPage();
			this.tabMechanics = new System.Windows.Forms.TabPage();
			this.btnStart = new FrEee.WinForms.Controls.GameButton();
			this.btnCancel = new FrEee.WinForms.Controls.GameButton();
			this.btnLoadSetup = new FrEee.WinForms.Controls.GameButton();
			this.btnSaveSetup = new FrEee.WinForms.Controls.GameButton();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.label30 = new System.Windows.Forms.Label();
			this.tabs.SuspendLayout();
			this.tabGalaxy.SuspendLayout();
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
			this.tabs.Controls.Add(this.tabMechanics);
			this.tabs.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.tabs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabs.Location = new System.Drawing.Point(0, 0);
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.SelectedTabBackColor = System.Drawing.Color.CornflowerBlue;
			this.tabs.SelectedTabForeColor = System.Drawing.Color.Black;
			this.tabs.Size = new System.Drawing.Size(553, 479);
			this.tabs.TabBackColor = System.Drawing.Color.Black;
			this.tabs.TabForeColor = System.Drawing.Color.CornflowerBlue;
			this.tabs.TabIndex = 0;
			// 
			// tabGalaxy
			// 
			this.tabGalaxy.BackColor = System.Drawing.Color.Black;
			this.tabGalaxy.Controls.Add(this.label11);
			this.tabGalaxy.Controls.Add(this.spnSystemGroups);
			this.tabGalaxy.Controls.Add(this.txtWarpPointLocation);
			this.tabGalaxy.Controls.Add(this.txtGalaxyName);
			this.tabGalaxy.Controls.Add(this.labelName);
			this.tabGalaxy.Controls.Add(this.ddlEventSeverity);
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
			this.tabGalaxy.Size = new System.Drawing.Size(545, 446);
			this.tabGalaxy.TabIndex = 0;
			this.tabGalaxy.Text = "Galaxy";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(191, 136);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(287, 15);
			this.label11.TabIndex = 27;
			this.label11.Text = "Fewer groups means more warp point connections.";
			// 
			// spnSystemGroups
			// 
			this.spnSystemGroups.Location = new System.Drawing.Point(114, 134);
			this.spnSystemGroups.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.spnSystemGroups.Name = "spnSystemGroups";
			this.spnSystemGroups.Size = new System.Drawing.Size(71, 21);
			this.spnSystemGroups.TabIndex = 26;
			this.spnSystemGroups.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.spnSystemGroups.ValueChanged += new System.EventHandler(this.spnSystemGroups_ValueChanged);
			// 
			// txtWarpPointLocation
			// 
			this.txtWarpPointLocation.Location = new System.Drawing.Point(19, 188);
			this.txtWarpPointLocation.Margin = new System.Windows.Forms.Padding(3);
			this.txtWarpPointLocation.MaximumSize = new System.Drawing.Size(467, 36);
			this.txtWarpPointLocation.Name = "txtWarpPointLocation";
			this.txtWarpPointLocation.Size = new System.Drawing.Size(467, 36);
			this.txtWarpPointLocation.TabIndex = 25;
			this.txtWarpPointLocation.Text = "Choose a warp point placement option.";
			// 
			// txtGalaxyName
			// 
			this.txtGalaxyName.Location = new System.Drawing.Point(114, 4);
			this.txtGalaxyName.Name = "txtGalaxyName";
			this.txtGalaxyName.Size = new System.Drawing.Size(152, 21);
			this.txtGalaxyName.TabIndex = 24;
			// 
			// labelName
			// 
			this.labelName.AutoSize = true;
			this.labelName.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.labelName.Location = new System.Drawing.Point(11, 7);
			this.labelName.Margin = new System.Windows.Forms.Padding(3);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(41, 15);
			this.labelName.TabIndex = 23;
			this.labelName.Text = "Name";
			// 
			// ddlEventSeverity
			// 
			this.ddlEventSeverity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlEventSeverity.FormattingEnabled = true;
			this.ddlEventSeverity.Location = new System.Drawing.Point(114, 330);
			this.ddlEventSeverity.Name = "ddlEventSeverity";
			this.ddlEventSeverity.Size = new System.Drawing.Size(152, 23);
			this.ddlEventSeverity.TabIndex = 22;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label10.Location = new System.Drawing.Point(11, 333);
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
			this.label9.Location = new System.Drawing.Point(241, 81);
			this.label9.Margin = new System.Windows.Forms.Padding(3);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(63, 15);
			this.label9.TabIndex = 20;
			this.label9.Text = "light-years";
			// 
			// spnHeight
			// 
			this.spnHeight.Location = new System.Drawing.Point(187, 79);
			this.spnHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.spnHeight.Name = "spnHeight";
			this.spnHeight.Size = new System.Drawing.Size(48, 21);
			this.spnHeight.TabIndex = 19;
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
			this.label8.Location = new System.Drawing.Point(168, 81);
			this.label8.Margin = new System.Windows.Forms.Padding(3);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(13, 15);
			this.label8.TabIndex = 18;
			this.label8.Text = "x";
			// 
			// spnWidth
			// 
			this.spnWidth.Location = new System.Drawing.Point(114, 79);
			this.spnWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.spnWidth.Name = "spnWidth";
			this.spnWidth.Size = new System.Drawing.Size(48, 21);
			this.spnWidth.TabIndex = 17;
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
			this.label7.Location = new System.Drawing.Point(11, 81);
			this.label7.Margin = new System.Windows.Forms.Padding(3);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(73, 15);
			this.label7.TabIndex = 16;
			this.label7.Text = "Dimensions";
			// 
			// ddlEventFrequency
			// 
			this.ddlEventFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlEventFrequency.FormattingEnabled = true;
			this.ddlEventFrequency.Location = new System.Drawing.Point(114, 301);
			this.ddlEventFrequency.Name = "ddlEventFrequency";
			this.ddlEventFrequency.Size = new System.Drawing.Size(152, 23);
			this.ddlEventFrequency.TabIndex = 15;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label6.Location = new System.Drawing.Point(11, 304);
			this.label6.Margin = new System.Windows.Forms.Padding(3);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(97, 15);
			this.label6.TabIndex = 14;
			this.label6.Text = "Event Frequency";
			// 
			// chkOmniscient
			// 
			this.chkOmniscient.AutoSize = true;
			this.chkOmniscient.Location = new System.Drawing.Point(22, 276);
			this.chkOmniscient.Name = "chkOmniscient";
			this.chkOmniscient.Size = new System.Drawing.Size(231, 19);
			this.chkOmniscient.TabIndex = 11;
			this.chkOmniscient.Text = "Omniscient View of Explored Systems";
			this.chkOmniscient.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label4.Location = new System.Drawing.Point(11, 230);
			this.label4.Margin = new System.Windows.Forms.Padding(3);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(66, 15);
			this.label4.TabIndex = 10;
			this.label4.Text = "Fog of War";
			// 
			// chkAllSystemsExplored
			// 
			this.chkAllSystemsExplored.AutoSize = true;
			this.chkAllSystemsExplored.Location = new System.Drawing.Point(22, 251);
			this.chkAllSystemsExplored.Name = "chkAllSystemsExplored";
			this.chkAllSystemsExplored.Size = new System.Drawing.Size(140, 19);
			this.chkAllSystemsExplored.TabIndex = 9;
			this.chkAllSystemsExplored.Text = "All Systems Explored";
			this.chkAllSystemsExplored.UseVisualStyleBackColor = true;
			// 
			// ddlWarpPointLocation
			// 
			this.ddlWarpPointLocation.DataSource = this.warpPointPlacementStrategyBindingSource;
			this.ddlWarpPointLocation.DisplayMember = "Name";
			this.ddlWarpPointLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlWarpPointLocation.FormattingEnabled = true;
			this.ddlWarpPointLocation.Location = new System.Drawing.Point(114, 162);
			this.ddlWarpPointLocation.Name = "ddlWarpPointLocation";
			this.ddlWarpPointLocation.Size = new System.Drawing.Size(152, 23);
			this.ddlWarpPointLocation.TabIndex = 8;
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
			this.label3.Location = new System.Drawing.Point(9, 165);
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
			this.label2.Location = new System.Drawing.Point(9, 136);
			this.label2.Margin = new System.Windows.Forms.Padding(3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(90, 15);
			this.label2.TabIndex = 5;
			this.label2.Text = "System Groups";
			// 
			// spnStarSystems
			// 
			this.spnStarSystems.Location = new System.Drawing.Point(114, 106);
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
			this.spnStarSystems.TabIndex = 4;
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
			this.label1.Location = new System.Drawing.Point(9, 108);
			this.label1.Margin = new System.Windows.Forms.Padding(3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(78, 15);
			this.label1.TabIndex = 3;
			this.label1.Text = "Star Systems";
			// 
			// txtGalaxyTypeDescription
			// 
			this.txtGalaxyTypeDescription.AutoSize = true;
			this.txtGalaxyTypeDescription.Location = new System.Drawing.Point(19, 57);
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
			this.ddlGalaxyType.Location = new System.Drawing.Point(114, 31);
			this.ddlGalaxyType.Name = "ddlGalaxyType";
			this.ddlGalaxyType.Size = new System.Drawing.Size(152, 23);
			this.ddlGalaxyType.TabIndex = 1;
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
			this.lblGalaxyType.Location = new System.Drawing.Point(9, 35);
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
			this.tabResources.Size = new System.Drawing.Size(545, 446);
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
			this.tabTechnology.Controls.Add(this.label30);
			this.tabTechnology.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabTechnology.Location = new System.Drawing.Point(4, 29);
			this.tabTechnology.Name = "tabTechnology";
			this.tabTechnology.Padding = new System.Windows.Forms.Padding(3);
			this.tabTechnology.Size = new System.Drawing.Size(545, 446);
			this.tabTechnology.TabIndex = 2;
			this.tabTechnology.Text = "Technology";
			// 
			// tabEmpires
			// 
			this.tabEmpires.BackColor = System.Drawing.Color.Black;
			this.tabEmpires.Location = new System.Drawing.Point(4, 29);
			this.tabEmpires.Name = "tabEmpires";
			this.tabEmpires.Padding = new System.Windows.Forms.Padding(3);
			this.tabEmpires.Size = new System.Drawing.Size(545, 446);
			this.tabEmpires.TabIndex = 3;
			this.tabEmpires.Text = "Empires";
			// 
			// tabVictory
			// 
			this.tabVictory.BackColor = System.Drawing.Color.Black;
			this.tabVictory.Location = new System.Drawing.Point(4, 29);
			this.tabVictory.Name = "tabVictory";
			this.tabVictory.Padding = new System.Windows.Forms.Padding(3);
			this.tabVictory.Size = new System.Drawing.Size(545, 446);
			this.tabVictory.TabIndex = 4;
			this.tabVictory.Text = "Victory";
			// 
			// tabSettings
			// 
			this.tabSettings.BackColor = System.Drawing.Color.Black;
			this.tabSettings.Location = new System.Drawing.Point(4, 29);
			this.tabSettings.Name = "tabSettings";
			this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
			this.tabSettings.Size = new System.Drawing.Size(545, 446);
			this.tabSettings.TabIndex = 5;
			this.tabSettings.Text = "Settings";
			// 
			// tabMechanics
			// 
			this.tabMechanics.BackColor = System.Drawing.Color.Black;
			this.tabMechanics.Location = new System.Drawing.Point(4, 29);
			this.tabMechanics.Name = "tabMechanics";
			this.tabMechanics.Padding = new System.Windows.Forms.Padding(3);
			this.tabMechanics.Size = new System.Drawing.Size(545, 446);
			this.tabMechanics.TabIndex = 6;
			this.tabMechanics.Text = "Mechanics";
			// 
			// btnStart
			// 
			this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStart.BackColor = System.Drawing.Color.Black;
			this.btnStart.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnStart.Location = new System.Drawing.Point(478, 485);
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
			this.btnCancel.Location = new System.Drawing.Point(397, 485);
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
			this.btnLoadSetup.Location = new System.Drawing.Point(6, 485);
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
			this.btnSaveSetup.Location = new System.Drawing.Point(87, 485);
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
			this.progressBar.Location = new System.Drawing.Point(168, 491);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(223, 14);
			this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBar.TabIndex = 5;
			this.progressBar.Visible = false;
			// 
			// label30
			// 
			this.label30.AutoSize = true;
			this.label30.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label30.Location = new System.Drawing.Point(3, 6);
			this.label30.Margin = new System.Windows.Forms.Padding(3);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(111, 15);
			this.label30.TabIndex = 12;
			this.label30.Text = "Starting Tech Level";
			// 
			// GameSetupForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(565, 517);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.btnSaveSetup);
			this.Controls.Add(this.btnLoadSetup);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.tabs);
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "GameSetupForm";
			this.Text = "Game Setup";
			this.tabs.ResumeLayout(false);
			this.tabGalaxy.ResumeLayout(false);
			this.tabGalaxy.PerformLayout();
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
		private System.Windows.Forms.TabPage tabMechanics;
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
		private System.Windows.Forms.ComboBox ddlEventSeverity;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.BindingSource galaxyTemplateBindingSource;
		private Controls.GameButton btnLoadSetup;
		private Controls.GameButton btnSaveSetup;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.TextBox txtGalaxyName;
		private System.Windows.Forms.Label labelName;
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
	}
}