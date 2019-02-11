namespace FrEee.WinForms.Forms
{
	partial class EmpireSetupForm
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
			this.txtPointsAvailable = new System.Windows.Forms.Label();
			this.btnSaveRace = new FrEee.WinForms.Controls.GameButton();
			this.btnLoadRace = new FrEee.WinForms.Controls.GameButton();
			this.btnCancel = new FrEee.WinForms.Controls.GameButton();
			this.btnOK = new FrEee.WinForms.Controls.GameButton();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.label2 = new System.Windows.Forms.Label();
			this.gamePanel1 = new FrEee.WinForms.Controls.GamePanel();
			this.txtRaceHappiness = new System.Windows.Forms.Label();
			this.ddlRaceHappiness = new System.Windows.Forms.ComboBox();
			this.label19 = new System.Windows.Forms.Label();
			this.picRacePopulationIcon = new FrEee.WinForms.Controls.GamePictureBox();
			this.label18 = new System.Windows.Forms.Label();
			this.ddlRacePopulationIcon = new System.Windows.Forms.ComboBox();
			this.ddlRaceNativeAtmosphere = new System.Windows.Forms.ComboBox();
			this.label17 = new System.Windows.Forms.Label();
			this.txtRaceName = new System.Windows.Forms.TextBox();
			this.ddlRaceNativeSurface = new System.Windows.Forms.ComboBox();
			this.label13 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.chkAIsCanUse = new System.Windows.Forms.CheckBox();
			this.label23 = new System.Windows.Forms.Label();
			this.btnCompareCultures = new FrEee.WinForms.Controls.GameButton();
			this.txtCulture = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.ddlCulture = new System.Windows.Forms.ComboBox();
			this.ddlAI = new System.Windows.Forms.ComboBox();
			this.picShipset = new FrEee.WinForms.Controls.GamePictureBox();
			this.ddlShipset = new System.Windows.Forms.ComboBox();
			this.picInsignia = new FrEee.WinForms.Controls.GamePictureBox();
			this.ddlInsignia = new System.Windows.Forms.ComboBox();
			this.picColor = new FrEee.WinForms.Controls.GamePictureBox();
			this.spnColorBlue = new System.Windows.Forms.NumericUpDown();
			this.spnColorGreen = new System.Windows.Forms.NumericUpDown();
			this.spnColorRed = new System.Windows.Forms.NumericUpDown();
			this.picLeaderPortrait = new FrEee.WinForms.Controls.GamePictureBox();
			this.ddlLeaderPortrait = new System.Windows.Forms.ComboBox();
			this.txtLeaderName = new System.Windows.Forms.TextBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label32 = new System.Windows.Forms.Label();
			this.tabAptitudes = new System.Windows.Forms.TabPage();
			this.aptitudePicker = new FrEee.WinForms.Controls.AptitudePicker();
			this.tabTraits = new System.Windows.Forms.TabPage();
			this.raceTraitPicker = new FrEee.WinForms.Controls.TraitPicker();
			this.tabs = new FrEee.WinForms.Controls.GameTabControl();
			this.label8 = new System.Windows.Forms.Label();
			this.ddlDesignNames = new System.Windows.Forms.ComboBox();
			this.tabGeneral.SuspendLayout();
			this.gamePanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picRacePopulationIcon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picShipset)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picInsignia)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picColor)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnColorBlue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnColorGreen)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnColorRed)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picLeaderPortrait)).BeginInit();
			this.tabAptitudes.SuspendLayout();
			this.tabTraits.SuspendLayout();
			this.tabs.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtPointsAvailable
			// 
			this.txtPointsAvailable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPointsAvailable.Location = new System.Drawing.Point(183, 770);
			this.txtPointsAvailable.Name = "txtPointsAvailable";
			this.txtPointsAvailable.Size = new System.Drawing.Size(172, 17);
			this.txtPointsAvailable.TabIndex = 5;
			this.txtPointsAvailable.Text = "Points Available: 0 / 0";
			this.txtPointsAvailable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnSaveRace
			// 
			this.btnSaveRace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSaveRace.BackColor = System.Drawing.Color.Black;
			this.btnSaveRace.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnSaveRace.Location = new System.Drawing.Point(96, 767);
			this.btnSaveRace.Name = "btnSaveRace";
			this.btnSaveRace.Size = new System.Drawing.Size(80, 22);
			this.btnSaveRace.TabIndex = 2;
			this.btnSaveRace.Text = "Save Race";
			this.btnSaveRace.UseVisualStyleBackColor = false;
			this.btnSaveRace.Click += new System.EventHandler(this.btnSaveRace_Click);
			// 
			// btnLoadRace
			// 
			this.btnLoadRace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnLoadRace.BackColor = System.Drawing.Color.Black;
			this.btnLoadRace.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnLoadRace.Location = new System.Drawing.Point(10, 767);
			this.btnLoadRace.Name = "btnLoadRace";
			this.btnLoadRace.Size = new System.Drawing.Size(80, 22);
			this.btnLoadRace.TabIndex = 1;
			this.btnLoadRace.Text = "Load Race";
			this.btnLoadRace.UseVisualStyleBackColor = false;
			this.btnLoadRace.Click += new System.EventHandler(this.btnLoadRace_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.BackColor = System.Drawing.Color.Black;
			this.btnCancel.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnCancel.Location = new System.Drawing.Point(362, 767);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(80, 22);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.BackColor = System.Drawing.Color.Black;
			this.btnOK.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnOK.Location = new System.Drawing.Point(448, 767);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(80, 22);
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// tabGeneral
			// 
			this.tabGeneral.BackColor = System.Drawing.Color.Black;
			this.tabGeneral.Controls.Add(this.label8);
			this.tabGeneral.Controls.Add(this.ddlDesignNames);
			this.tabGeneral.Controls.Add(this.label2);
			this.tabGeneral.Controls.Add(this.gamePanel1);
			this.tabGeneral.Controls.Add(this.chkAIsCanUse);
			this.tabGeneral.Controls.Add(this.label23);
			this.tabGeneral.Controls.Add(this.btnCompareCultures);
			this.tabGeneral.Controls.Add(this.txtCulture);
			this.tabGeneral.Controls.Add(this.label22);
			this.tabGeneral.Controls.Add(this.ddlCulture);
			this.tabGeneral.Controls.Add(this.ddlAI);
			this.tabGeneral.Controls.Add(this.picShipset);
			this.tabGeneral.Controls.Add(this.ddlShipset);
			this.tabGeneral.Controls.Add(this.picInsignia);
			this.tabGeneral.Controls.Add(this.ddlInsignia);
			this.tabGeneral.Controls.Add(this.picColor);
			this.tabGeneral.Controls.Add(this.spnColorBlue);
			this.tabGeneral.Controls.Add(this.spnColorGreen);
			this.tabGeneral.Controls.Add(this.spnColorRed);
			this.tabGeneral.Controls.Add(this.picLeaderPortrait);
			this.tabGeneral.Controls.Add(this.ddlLeaderPortrait);
			this.tabGeneral.Controls.Add(this.txtLeaderName);
			this.tabGeneral.Controls.Add(this.txtName);
			this.tabGeneral.Controls.Add(this.label7);
			this.tabGeneral.Controls.Add(this.label6);
			this.tabGeneral.Controls.Add(this.label5);
			this.tabGeneral.Controls.Add(this.label4);
			this.tabGeneral.Controls.Add(this.label3);
			this.tabGeneral.Controls.Add(this.label1);
			this.tabGeneral.Controls.Add(this.label32);
			this.tabGeneral.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabGeneral.Location = new System.Drawing.Point(4, 29);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.tabGeneral.Size = new System.Drawing.Size(519, 722);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label2.Location = new System.Drawing.Point(9, 463);
			this.label2.Margin = new System.Windows.Forms.Padding(3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(81, 15);
			this.label2.TabIndex = 15;
			this.label2.Text = "Primary Race";
			// 
			// gamePanel1
			// 
			this.gamePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gamePanel1.BackColor = System.Drawing.Color.Black;
			this.gamePanel1.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.gamePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.gamePanel1.Controls.Add(this.txtRaceHappiness);
			this.gamePanel1.Controls.Add(this.ddlRaceHappiness);
			this.gamePanel1.Controls.Add(this.label19);
			this.gamePanel1.Controls.Add(this.picRacePopulationIcon);
			this.gamePanel1.Controls.Add(this.label18);
			this.gamePanel1.Controls.Add(this.ddlRacePopulationIcon);
			this.gamePanel1.Controls.Add(this.ddlRaceNativeAtmosphere);
			this.gamePanel1.Controls.Add(this.label17);
			this.gamePanel1.Controls.Add(this.txtRaceName);
			this.gamePanel1.Controls.Add(this.ddlRaceNativeSurface);
			this.gamePanel1.Controls.Add(this.label13);
			this.gamePanel1.Controls.Add(this.label15);
			this.gamePanel1.ForeColor = System.Drawing.Color.White;
			this.gamePanel1.Location = new System.Drawing.Point(9, 484);
			this.gamePanel1.Name = "gamePanel1";
			this.gamePanel1.Padding = new System.Windows.Forms.Padding(3);
			this.gamePanel1.Size = new System.Drawing.Size(494, 232);
			this.gamePanel1.TabIndex = 98;
			// 
			// txtRaceHappiness
			// 
			this.txtRaceHappiness.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.txtRaceHappiness.Location = new System.Drawing.Point(25, 143);
			this.txtRaceHappiness.Name = "txtRaceHappiness";
			this.txtRaceHappiness.Size = new System.Drawing.Size(297, 84);
			this.txtRaceHappiness.TabIndex = 99;
			this.txtRaceHappiness.Text = "Please choose a happiness model.";
			// 
			// ddlRaceHappiness
			// 
			this.ddlRaceHappiness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlRaceHappiness.FormattingEnabled = true;
			this.ddlRaceHappiness.Location = new System.Drawing.Point(141, 119);
			this.ddlRaceHappiness.Name = "ddlRaceHappiness";
			this.ddlRaceHappiness.Size = new System.Drawing.Size(180, 23);
			this.ddlRaceHappiness.TabIndex = 92;
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label19.Location = new System.Drawing.Point(9, 122);
			this.label19.Margin = new System.Windows.Forms.Padding(3);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(66, 15);
			this.label19.TabIndex = 98;
			this.label19.Text = "Happiness";
			// 
			// picRacePopulationIcon
			// 
			this.picRacePopulationIcon.Location = new System.Drawing.Point(328, 33);
			this.picRacePopulationIcon.Name = "picRacePopulationIcon";
			this.picRacePopulationIcon.Size = new System.Drawing.Size(22, 20);
			this.picRacePopulationIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picRacePopulationIcon.TabIndex = 97;
			this.picRacePopulationIcon.TabStop = false;
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label18.Location = new System.Drawing.Point(10, 36);
			this.label18.Margin = new System.Windows.Forms.Padding(3);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(92, 15);
			this.label18.TabIndex = 96;
			this.label18.Text = "Population Icon";
			// 
			// ddlRacePopulationIcon
			// 
			this.ddlRacePopulationIcon.FormattingEnabled = true;
			this.ddlRacePopulationIcon.Location = new System.Drawing.Point(142, 33);
			this.ddlRacePopulationIcon.Name = "ddlRacePopulationIcon";
			this.ddlRacePopulationIcon.Size = new System.Drawing.Size(180, 23);
			this.ddlRacePopulationIcon.TabIndex = 89;
			this.ddlRacePopulationIcon.TextChanged += new System.EventHandler(this.ddlRacePopulationIcon_TextChanged);
			this.ddlRacePopulationIcon.Leave += new System.EventHandler(this.ddlPic_Leave);
			// 
			// ddlRaceNativeAtmosphere
			// 
			this.ddlRaceNativeAtmosphere.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlRaceNativeAtmosphere.FormattingEnabled = true;
			this.ddlRaceNativeAtmosphere.Location = new System.Drawing.Point(142, 90);
			this.ddlRaceNativeAtmosphere.Name = "ddlRaceNativeAtmosphere";
			this.ddlRaceNativeAtmosphere.Size = new System.Drawing.Size(180, 23);
			this.ddlRaceNativeAtmosphere.TabIndex = 91;
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label17.Location = new System.Drawing.Point(10, 93);
			this.label17.Margin = new System.Windows.Forms.Padding(3);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(110, 15);
			this.label17.TabIndex = 95;
			this.label17.Text = "Native Atmosphere";
			// 
			// txtRaceName
			// 
			this.txtRaceName.Location = new System.Drawing.Point(141, 6);
			this.txtRaceName.Name = "txtRaceName";
			this.txtRaceName.Size = new System.Drawing.Size(180, 21);
			this.txtRaceName.TabIndex = 88;
			// 
			// ddlRaceNativeSurface
			// 
			this.ddlRaceNativeSurface.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlRaceNativeSurface.FormattingEnabled = true;
			this.ddlRaceNativeSurface.Location = new System.Drawing.Point(142, 61);
			this.ddlRaceNativeSurface.Name = "ddlRaceNativeSurface";
			this.ddlRaceNativeSurface.Size = new System.Drawing.Size(180, 23);
			this.ddlRaceNativeSurface.TabIndex = 90;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label13.Location = new System.Drawing.Point(10, 64);
			this.label13.Margin = new System.Windows.Forms.Padding(3);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(86, 15);
			this.label13.TabIndex = 94;
			this.label13.Text = "Native Surface";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label15.Location = new System.Drawing.Point(9, 9);
			this.label15.Margin = new System.Windows.Forms.Padding(3);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(41, 15);
			this.label15.TabIndex = 93;
			this.label15.Text = "Name";
			// 
			// chkAIsCanUse
			// 
			this.chkAIsCanUse.AutoSize = true;
			this.chkAIsCanUse.Location = new System.Drawing.Point(138, 30);
			this.chkAIsCanUse.Name = "chkAIsCanUse";
			this.chkAIsCanUse.Size = new System.Drawing.Size(55, 19);
			this.chkAIsCanUse.TabIndex = 2;
			this.chkAIsCanUse.Text = "Allow";
			this.chkAIsCanUse.UseVisualStyleBackColor = true;
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label23.Location = new System.Drawing.Point(9, 30);
			this.label23.Margin = new System.Windows.Forms.Padding(3);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(116, 15);
			this.label23.TabIndex = 97;
			this.label23.Text = "AI Empires Can Use";
			// 
			// btnCompareCultures
			// 
			this.btnCompareCultures.BackColor = System.Drawing.Color.Black;
			this.btnCompareCultures.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnCompareCultures.Location = new System.Drawing.Point(238, 293);
			this.btnCompareCultures.Name = "btnCompareCultures";
			this.btnCompareCultures.Size = new System.Drawing.Size(80, 22);
			this.btnCompareCultures.TabIndex = 21;
			this.btnCompareCultures.Text = "Compare";
			this.btnCompareCultures.UseVisualStyleBackColor = false;
			this.btnCompareCultures.Click += new System.EventHandler(this.btnCompareCultures_Click);
			// 
			// txtCulture
			// 
			this.txtCulture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.txtCulture.Location = new System.Drawing.Point(22, 291);
			this.txtCulture.Name = "txtCulture";
			this.txtCulture.Size = new System.Drawing.Size(210, 140);
			this.txtCulture.TabIndex = 94;
			this.txtCulture.Text = "Please choose a culture.";
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label22.Location = new System.Drawing.Point(9, 267);
			this.label22.Margin = new System.Windows.Forms.Padding(3);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(46, 15);
			this.label22.TabIndex = 93;
			this.label22.Text = "Culture";
			// 
			// ddlCulture
			// 
			this.ddlCulture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlCulture.FormattingEnabled = true;
			this.ddlCulture.Location = new System.Drawing.Point(138, 264);
			this.ddlCulture.Name = "ddlCulture";
			this.ddlCulture.Size = new System.Drawing.Size(180, 23);
			this.ddlCulture.TabIndex = 19;
			this.ddlCulture.SelectedIndexChanged += new System.EventHandler(this.ddlCulture_SelectedIndexChanged);
			// 
			// ddlAI
			// 
			this.ddlAI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlAI.FormattingEnabled = true;
			this.ddlAI.Location = new System.Drawing.Point(138, 235);
			this.ddlAI.Name = "ddlAI";
			this.ddlAI.Size = new System.Drawing.Size(180, 23);
			this.ddlAI.TabIndex = 15;
			// 
			// picShipset
			// 
			this.picShipset.Location = new System.Drawing.Point(324, 206);
			this.picShipset.Name = "picShipset";
			this.picShipset.Size = new System.Drawing.Size(137, 126);
			this.picShipset.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picShipset.TabIndex = 41;
			this.picShipset.TabStop = false;
			// 
			// ddlShipset
			// 
			this.ddlShipset.FormattingEnabled = true;
			this.ddlShipset.Location = new System.Drawing.Point(138, 206);
			this.ddlShipset.Name = "ddlShipset";
			this.ddlShipset.Size = new System.Drawing.Size(180, 23);
			this.ddlShipset.TabIndex = 13;
			this.ddlShipset.TextChanged += new System.EventHandler(this.ddlShipset_TextChanged);
			this.ddlShipset.Leave += new System.EventHandler(this.ddlPic_Leave);
			// 
			// picInsignia
			// 
			this.picInsignia.Location = new System.Drawing.Point(322, 178);
			this.picInsignia.Name = "picInsignia";
			this.picInsignia.Size = new System.Drawing.Size(22, 20);
			this.picInsignia.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picInsignia.TabIndex = 38;
			this.picInsignia.TabStop = false;
			// 
			// ddlInsignia
			// 
			this.ddlInsignia.FormattingEnabled = true;
			this.ddlInsignia.Location = new System.Drawing.Point(138, 178);
			this.ddlInsignia.Name = "ddlInsignia";
			this.ddlInsignia.Size = new System.Drawing.Size(180, 23);
			this.ddlInsignia.TabIndex = 11;
			this.ddlInsignia.TextChanged += new System.EventHandler(this.ddlInsignia_TextChanged);
			this.ddlInsignia.Leave += new System.EventHandler(this.ddlPic_Leave);
			// 
			// picColor
			// 
			this.picColor.Location = new System.Drawing.Point(324, 152);
			this.picColor.Name = "picColor";
			this.picColor.Size = new System.Drawing.Size(22, 20);
			this.picColor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picColor.TabIndex = 35;
			this.picColor.TabStop = false;
			// 
			// spnColorBlue
			// 
			this.spnColorBlue.Increment = new decimal(new int[] {
            85,
            0,
            0,
            0});
			this.spnColorBlue.Location = new System.Drawing.Point(253, 152);
			this.spnColorBlue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.spnColorBlue.Name = "spnColorBlue";
			this.spnColorBlue.Size = new System.Drawing.Size(51, 21);
			this.spnColorBlue.TabIndex = 9;
			this.spnColorBlue.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.spnColorBlue.ValueChanged += new System.EventHandler(this.spnColor_ValueChanged);
			this.spnColorBlue.Leave += new System.EventHandler(this.spnColor_Leave);
			// 
			// spnColorGreen
			// 
			this.spnColorGreen.Increment = new decimal(new int[] {
            85,
            0,
            0,
            0});
			this.spnColorGreen.Location = new System.Drawing.Point(195, 152);
			this.spnColorGreen.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.spnColorGreen.Name = "spnColorGreen";
			this.spnColorGreen.Size = new System.Drawing.Size(51, 21);
			this.spnColorGreen.TabIndex = 8;
			this.spnColorGreen.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.spnColorGreen.ValueChanged += new System.EventHandler(this.spnColor_ValueChanged);
			this.spnColorGreen.Leave += new System.EventHandler(this.spnColor_Leave);
			// 
			// spnColorRed
			// 
			this.spnColorRed.Increment = new decimal(new int[] {
            85,
            0,
            0,
            0});
			this.spnColorRed.Location = new System.Drawing.Point(138, 152);
			this.spnColorRed.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.spnColorRed.Name = "spnColorRed";
			this.spnColorRed.Size = new System.Drawing.Size(51, 21);
			this.spnColorRed.TabIndex = 7;
			this.spnColorRed.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.spnColorRed.ValueChanged += new System.EventHandler(this.spnColor_ValueChanged);
			this.spnColorRed.Leave += new System.EventHandler(this.spnColor_Leave);
			// 
			// picLeaderPortrait
			// 
			this.picLeaderPortrait.Location = new System.Drawing.Point(324, 82);
			this.picLeaderPortrait.Name = "picLeaderPortrait";
			this.picLeaderPortrait.Size = new System.Drawing.Size(68, 63);
			this.picLeaderPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picLeaderPortrait.TabIndex = 25;
			this.picLeaderPortrait.TabStop = false;
			// 
			// ddlLeaderPortrait
			// 
			this.ddlLeaderPortrait.FormattingEnabled = true;
			this.ddlLeaderPortrait.Location = new System.Drawing.Point(138, 82);
			this.ddlLeaderPortrait.Name = "ddlLeaderPortrait";
			this.ddlLeaderPortrait.Size = new System.Drawing.Size(180, 23);
			this.ddlLeaderPortrait.TabIndex = 5;
			this.ddlLeaderPortrait.TextChanged += new System.EventHandler(this.ddlLeaderPortrait_TextChanged);
			this.ddlLeaderPortrait.Leave += new System.EventHandler(this.ddlPic_Leave);
			// 
			// txtLeaderName
			// 
			this.txtLeaderName.Location = new System.Drawing.Point(138, 54);
			this.txtLeaderName.Name = "txtLeaderName";
			this.txtLeaderName.Size = new System.Drawing.Size(180, 21);
			this.txtLeaderName.TabIndex = 3;
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(138, 3);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(180, 21);
			this.txtName.TabIndex = 0;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label7.Location = new System.Drawing.Point(9, 238);
			this.label7.Margin = new System.Windows.Forms.Padding(3);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(17, 15);
			this.label7.TabIndex = 21;
			this.label7.Text = "AI";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label6.Location = new System.Drawing.Point(6, 85);
			this.label6.Margin = new System.Windows.Forms.Padding(3);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(88, 15);
			this.label6.TabIndex = 20;
			this.label6.Text = "Leader Portrait";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label5.Location = new System.Drawing.Point(9, 210);
			this.label5.Margin = new System.Windows.Forms.Padding(3);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 15);
			this.label5.TabIndex = 19;
			this.label5.Text = "Shipset";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label4.Location = new System.Drawing.Point(6, 181);
			this.label4.Margin = new System.Windows.Forms.Padding(3);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(50, 15);
			this.label4.TabIndex = 18;
			this.label4.Text = "Insignia";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label3.Location = new System.Drawing.Point(6, 154);
			this.label3.Margin = new System.Windows.Forms.Padding(3);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(65, 15);
			this.label3.TabIndex = 17;
			this.label3.Text = "Color RGB";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label1.Location = new System.Drawing.Point(6, 57);
			this.label1.Margin = new System.Windows.Forms.Padding(3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(83, 15);
			this.label1.TabIndex = 15;
			this.label1.Text = "Leader Name";
			// 
			// label32
			// 
			this.label32.AutoSize = true;
			this.label32.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label32.Location = new System.Drawing.Point(6, 6);
			this.label32.Margin = new System.Windows.Forms.Padding(3);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(41, 15);
			this.label32.TabIndex = 14;
			this.label32.Text = "Name";
			// 
			// tabAptitudes
			// 
			this.tabAptitudes.BackColor = System.Drawing.Color.Black;
			this.tabAptitudes.Controls.Add(this.aptitudePicker);
			this.tabAptitudes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabAptitudes.Location = new System.Drawing.Point(4, 29);
			this.tabAptitudes.Name = "tabAptitudes";
			this.tabAptitudes.Padding = new System.Windows.Forms.Padding(3);
			this.tabAptitudes.Size = new System.Drawing.Size(519, 642);
			this.tabAptitudes.TabIndex = 4;
			this.tabAptitudes.Text = "Aptitudes";
			// 
			// aptitudePicker
			// 
			this.aptitudePicker.BackColor = System.Drawing.Color.Black;
			this.aptitudePicker.Dock = System.Windows.Forms.DockStyle.Fill;
			this.aptitudePicker.ForeColor = System.Drawing.Color.White;
			this.aptitudePicker.Location = new System.Drawing.Point(3, 3);
			this.aptitudePicker.Margin = new System.Windows.Forms.Padding(4);
			this.aptitudePicker.Name = "aptitudePicker";
			this.aptitudePicker.Size = new System.Drawing.Size(513, 636);
			this.aptitudePicker.TabIndex = 0;
			this.aptitudePicker.Values = null;
			this.aptitudePicker.AptitudeValueChanged += new FrEee.WinForms.Controls.AptitudePicker.AptitudeValueChangedDelegate(this.aptitudePicker_AptitudeValueChanged);
			// 
			// tabTraits
			// 
			this.tabTraits.BackColor = System.Drawing.Color.Black;
			this.tabTraits.Controls.Add(this.raceTraitPicker);
			this.tabTraits.Location = new System.Drawing.Point(4, 29);
			this.tabTraits.Name = "tabTraits";
			this.tabTraits.Size = new System.Drawing.Size(519, 642);
			this.tabTraits.TabIndex = 5;
			this.tabTraits.Text = "Traits";
			// 
			// raceTraitPicker
			// 
			this.raceTraitPicker.BackColor = System.Drawing.Color.Black;
			this.raceTraitPicker.Dock = System.Windows.Forms.DockStyle.Fill;
			this.raceTraitPicker.ForeColor = System.Drawing.Color.White;
			this.raceTraitPicker.Location = new System.Drawing.Point(0, 0);
			this.raceTraitPicker.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.raceTraitPicker.Name = "raceTraitPicker";
			this.raceTraitPicker.Size = new System.Drawing.Size(519, 642);
			this.raceTraitPicker.TabIndex = 0;
			this.raceTraitPicker.Traits = null;
			this.raceTraitPicker.TraitToggled += new FrEee.WinForms.Controls.TraitPicker.TraitToggledDelegate(this.raceTraitPicker_TraitToggled);
			// 
			// tabs
			// 
			this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabs.Controls.Add(this.tabGeneral);
			this.tabs.Controls.Add(this.tabTraits);
			this.tabs.Controls.Add(this.tabAptitudes);
			this.tabs.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.tabs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabs.Location = new System.Drawing.Point(10, 9);
			this.tabs.Margin = new System.Windows.Forms.Padding(0);
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.SelectedTabBackColor = System.Drawing.Color.CornflowerBlue;
			this.tabs.SelectedTabForeColor = System.Drawing.Color.Black;
			this.tabs.Size = new System.Drawing.Size(527, 755);
			this.tabs.TabBackColor = System.Drawing.Color.Black;
			this.tabs.TabBorderColor = System.Drawing.Color.CornflowerBlue;
			this.tabs.TabForeColor = System.Drawing.Color.CornflowerBlue;
			this.tabs.TabIndex = 0;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label8.Location = new System.Drawing.Point(9, 437);
			this.label8.Margin = new System.Windows.Forms.Padding(3);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(89, 15);
			this.label8.TabIndex = 100;
			this.label8.Text = "Design Names";
			// 
			// ddlDesignNames
			// 
			this.ddlDesignNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlDesignNames.FormattingEnabled = true;
			this.ddlDesignNames.Location = new System.Drawing.Point(138, 434);
			this.ddlDesignNames.Name = "ddlDesignNames";
			this.ddlDesignNames.Size = new System.Drawing.Size(180, 23);
			this.ddlDesignNames.TabIndex = 99;
			// 
			// EmpireSetupForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(546, 802);
			this.Controls.Add(this.txtPointsAvailable);
			this.Controls.Add(this.btnSaveRace);
			this.Controls.Add(this.btnLoadRace);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.tabs);
			this.Name = "EmpireSetupForm";
			this.ShowInTaskbar = false;
			this.Text = "Empire Setup";
			this.tabGeneral.ResumeLayout(false);
			this.tabGeneral.PerformLayout();
			this.gamePanel1.ResumeLayout(false);
			this.gamePanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picRacePopulationIcon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picShipset)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picInsignia)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picColor)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnColorBlue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnColorGreen)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnColorRed)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picLeaderPortrait)).EndInit();
			this.tabAptitudes.ResumeLayout(false);
			this.tabTraits.ResumeLayout(false);
			this.tabs.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.GameButton btnOK;
		private Controls.GameButton btnCancel;
		private Controls.GameButton btnLoadRace;
		private Controls.GameButton btnSaveRace;
		private System.Windows.Forms.Label txtPointsAvailable;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.CheckBox chkAIsCanUse;
		private System.Windows.Forms.Label label23;
		private Controls.GameButton btnCompareCultures;
		private System.Windows.Forms.Label txtCulture;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.ComboBox ddlCulture;
		private System.Windows.Forms.ComboBox ddlAI;
		private Controls.GamePictureBox picShipset;
		private System.Windows.Forms.ComboBox ddlShipset;
		private Controls.GamePictureBox picInsignia;
		private System.Windows.Forms.ComboBox ddlInsignia;
		private Controls.GamePictureBox picColor;
		private System.Windows.Forms.NumericUpDown spnColorBlue;
		private System.Windows.Forms.NumericUpDown spnColorGreen;
		private System.Windows.Forms.NumericUpDown spnColorRed;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.TabPage tabAptitudes;
		private Controls.AptitudePicker aptitudePicker;
		private System.Windows.Forms.TabPage tabTraits;
		private Controls.TraitPicker raceTraitPicker;
		private Controls.GameTabControl tabs;
		private System.Windows.Forms.Label label2;
		private Controls.GamePanel gamePanel1;
		private System.Windows.Forms.Label txtRaceHappiness;
		private System.Windows.Forms.ComboBox ddlRaceHappiness;
		private System.Windows.Forms.Label label19;
		private Controls.GamePictureBox picRacePopulationIcon;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.ComboBox ddlRacePopulationIcon;
		private System.Windows.Forms.ComboBox ddlRaceNativeAtmosphere;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.TextBox txtRaceName;
		private System.Windows.Forms.ComboBox ddlRaceNativeSurface;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label15;
		private Controls.GamePictureBox picLeaderPortrait;
		private System.Windows.Forms.ComboBox ddlLeaderPortrait;
		private System.Windows.Forms.TextBox txtLeaderName;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox ddlDesignNames;
	}
}