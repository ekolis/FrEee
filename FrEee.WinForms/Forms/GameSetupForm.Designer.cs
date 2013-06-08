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
			this.ddlEventSeverity = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.spnHeight = new System.Windows.Forms.NumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.spnWidth = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.ddlEventFrequency = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.ddlResources = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.chkOmniscient = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.chkAllSystemsExplored = new System.Windows.Forms.CheckBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.ddlWarpPoints = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.spnStarSystems = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.txtGalaxyTypeDescription = new System.Windows.Forms.Label();
			this.ddlGalaxyType = new System.Windows.Forms.ComboBox();
			this.galaxyTemplateBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.lblGalaxyType = new System.Windows.Forms.Label();
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
			this.labelName = new System.Windows.Forms.Label();
			this.txtGalaxyName = new System.Windows.Forms.TextBox();
			this.tabs.SuspendLayout();
			this.tabGalaxy.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spnHeight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnStarSystems)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.galaxyTemplateBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// tabs
			// 
			this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabs.Controls.Add(this.tabGalaxy);
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
			this.tabs.Size = new System.Drawing.Size(500, 479);
			this.tabs.TabBackColor = System.Drawing.Color.Black;
			this.tabs.TabForeColor = System.Drawing.Color.CornflowerBlue;
			this.tabs.TabIndex = 0;
			// 
			// tabGalaxy
			// 
			this.tabGalaxy.BackColor = System.Drawing.Color.Black;
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
			this.tabGalaxy.Controls.Add(this.ddlResources);
			this.tabGalaxy.Controls.Add(this.label5);
			this.tabGalaxy.Controls.Add(this.chkOmniscient);
			this.tabGalaxy.Controls.Add(this.label4);
			this.tabGalaxy.Controls.Add(this.chkAllSystemsExplored);
			this.tabGalaxy.Controls.Add(this.comboBox1);
			this.tabGalaxy.Controls.Add(this.label3);
			this.tabGalaxy.Controls.Add(this.ddlWarpPoints);
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
			this.tabGalaxy.Size = new System.Drawing.Size(492, 446);
			this.tabGalaxy.TabIndex = 0;
			this.tabGalaxy.Text = "Galaxy";
			// 
			// ddlEventSeverity
			// 
			this.ddlEventSeverity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlEventSeverity.FormattingEnabled = true;
			this.ddlEventSeverity.Items.AddRange(new object[] {
            "Percentage Value",
            "Finite Pool"});
			this.ddlEventSeverity.Location = new System.Drawing.Point(114, 319);
			this.ddlEventSeverity.Name = "ddlEventSeverity";
			this.ddlEventSeverity.Size = new System.Drawing.Size(152, 23);
			this.ddlEventSeverity.TabIndex = 22;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label10.Location = new System.Drawing.Point(11, 322);
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
			this.ddlEventFrequency.Items.AddRange(new object[] {
            "Percentage Value",
            "Finite Pool"});
			this.ddlEventFrequency.Location = new System.Drawing.Point(114, 290);
			this.ddlEventFrequency.Name = "ddlEventFrequency";
			this.ddlEventFrequency.Size = new System.Drawing.Size(152, 23);
			this.ddlEventFrequency.TabIndex = 15;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label6.Location = new System.Drawing.Point(11, 293);
			this.label6.Margin = new System.Windows.Forms.Padding(3);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(97, 15);
			this.label6.TabIndex = 14;
			this.label6.Text = "Event Frequency";
			// 
			// ddlResources
			// 
			this.ddlResources.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlResources.FormattingEnabled = true;
			this.ddlResources.Items.AddRange(new object[] {
            "Percentage Value",
            "Finite Pool"});
			this.ddlResources.Location = new System.Drawing.Point(114, 261);
			this.ddlResources.Name = "ddlResources";
			this.ddlResources.Size = new System.Drawing.Size(152, 23);
			this.ddlResources.TabIndex = 13;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label5.Location = new System.Drawing.Point(11, 264);
			this.label5.Margin = new System.Windows.Forms.Padding(3);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(66, 15);
			this.label5.TabIndex = 12;
			this.label5.Text = "Resources";
			// 
			// chkOmniscient
			// 
			this.chkOmniscient.AutoSize = true;
			this.chkOmniscient.Location = new System.Drawing.Point(22, 239);
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
			this.label4.Location = new System.Drawing.Point(11, 193);
			this.label4.Margin = new System.Windows.Forms.Padding(3);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(66, 15);
			this.label4.TabIndex = 10;
			this.label4.Text = "Fog of War";
			// 
			// chkAllSystemsExplored
			// 
			this.chkAllSystemsExplored.AutoSize = true;
			this.chkAllSystemsExplored.Location = new System.Drawing.Point(22, 214);
			this.chkAllSystemsExplored.Name = "chkAllSystemsExplored";
			this.chkAllSystemsExplored.Size = new System.Drawing.Size(140, 19);
			this.chkAllSystemsExplored.TabIndex = 9;
			this.chkAllSystemsExplored.Text = "All Systems Explored";
			this.chkAllSystemsExplored.UseVisualStyleBackColor = true;
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
            "System Edges",
            "Random"});
			this.comboBox1.Location = new System.Drawing.Point(114, 162);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(152, 23);
			this.comboBox1.TabIndex = 8;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label3.Location = new System.Drawing.Point(9, 165);
			this.label3.Margin = new System.Windows.Forms.Padding(3);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(76, 15);
			this.label3.TabIndex = 7;
			this.label3.Text = "WP Location";
			// 
			// ddlWarpPoints
			// 
			this.ddlWarpPoints.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlWarpPoints.FormattingEnabled = true;
			this.ddlWarpPoints.Items.AddRange(new object[] {
            "Connected",
            "Disconnected",
            "None"});
			this.ddlWarpPoints.Location = new System.Drawing.Point(114, 133);
			this.ddlWarpPoints.Name = "ddlWarpPoints";
			this.ddlWarpPoints.Size = new System.Drawing.Size(152, 23);
			this.ddlWarpPoints.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label2.Location = new System.Drawing.Point(9, 136);
			this.label2.Margin = new System.Windows.Forms.Padding(3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(73, 15);
			this.label2.TabIndex = 5;
			this.label2.Text = "Warp Points";
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
			// tabTechnology
			// 
			this.tabTechnology.BackColor = System.Drawing.Color.Black;
			this.tabTechnology.Location = new System.Drawing.Point(4, 29);
			this.tabTechnology.Name = "tabTechnology";
			this.tabTechnology.Padding = new System.Windows.Forms.Padding(3);
			this.tabTechnology.Size = new System.Drawing.Size(492, 446);
			this.tabTechnology.TabIndex = 2;
			this.tabTechnology.Text = "Technology";
			// 
			// tabEmpires
			// 
			this.tabEmpires.BackColor = System.Drawing.Color.Black;
			this.tabEmpires.Location = new System.Drawing.Point(4, 29);
			this.tabEmpires.Name = "tabEmpires";
			this.tabEmpires.Padding = new System.Windows.Forms.Padding(3);
			this.tabEmpires.Size = new System.Drawing.Size(492, 446);
			this.tabEmpires.TabIndex = 3;
			this.tabEmpires.Text = "Empires";
			// 
			// tabVictory
			// 
			this.tabVictory.BackColor = System.Drawing.Color.Black;
			this.tabVictory.Location = new System.Drawing.Point(4, 29);
			this.tabVictory.Name = "tabVictory";
			this.tabVictory.Padding = new System.Windows.Forms.Padding(3);
			this.tabVictory.Size = new System.Drawing.Size(492, 446);
			this.tabVictory.TabIndex = 4;
			this.tabVictory.Text = "Victory";
			// 
			// tabSettings
			// 
			this.tabSettings.BackColor = System.Drawing.Color.Black;
			this.tabSettings.Location = new System.Drawing.Point(4, 29);
			this.tabSettings.Name = "tabSettings";
			this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
			this.tabSettings.Size = new System.Drawing.Size(492, 446);
			this.tabSettings.TabIndex = 5;
			this.tabSettings.Text = "Settings";
			// 
			// tabMechanics
			// 
			this.tabMechanics.BackColor = System.Drawing.Color.Black;
			this.tabMechanics.Location = new System.Drawing.Point(4, 29);
			this.tabMechanics.Name = "tabMechanics";
			this.tabMechanics.Padding = new System.Windows.Forms.Padding(3);
			this.tabMechanics.Size = new System.Drawing.Size(492, 446);
			this.tabMechanics.TabIndex = 6;
			this.tabMechanics.Text = "Mechanics";
			// 
			// btnStart
			// 
			this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStart.BackColor = System.Drawing.Color.Black;
			this.btnStart.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnStart.Location = new System.Drawing.Point(425, 485);
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
			this.btnCancel.Location = new System.Drawing.Point(344, 485);
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
			this.progressBar.Size = new System.Drawing.Size(170, 14);
			this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBar.TabIndex = 5;
			this.progressBar.Visible = false;
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
			// txtGalaxyName
			// 
			this.txtGalaxyName.Location = new System.Drawing.Point(114, 4);
			this.txtGalaxyName.Name = "txtGalaxyName";
			this.txtGalaxyName.Size = new System.Drawing.Size(152, 21);
			this.txtGalaxyName.TabIndex = 24;
			this.txtGalaxyName.TextChanged += new System.EventHandler(this.txtGalaxyName_TextChanged);
			// 
			// GameSetupForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(512, 517);
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
			((System.ComponentModel.ISupportInitialize)(this.spnHeight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnStarSystems)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.galaxyTemplateBindingSource)).EndInit();
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
		private System.Windows.Forms.ComboBox ddlWarpPoints;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox chkOmniscient;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox chkAllSystemsExplored;
		private System.Windows.Forms.ComboBox ddlResources;
		private System.Windows.Forms.Label label5;
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
	}
}