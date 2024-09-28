namespace FrEee.UI.WinForms.Forms;

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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmpireSetupForm));
        txtPointsAvailable = new System.Windows.Forms.Label();
        btnCancel = new Controls.GameButton();
        btnOK = new Controls.GameButton();
        tabGeneral = new System.Windows.Forms.TabPage();
        label8 = new System.Windows.Forms.Label();
        ddlDesignNames = new System.Windows.Forms.ComboBox();
        label2 = new System.Windows.Forms.Label();
        gamePanel1 = new Controls.GamePanel();
        txtRaceHappiness = new System.Windows.Forms.Label();
        ddlRaceHappiness = new System.Windows.Forms.ComboBox();
        label19 = new System.Windows.Forms.Label();
        picRacePopulationIcon = new Controls.Blazor.GamePictureBox();
        label18 = new System.Windows.Forms.Label();
        ddlRacePopulationIcon = new System.Windows.Forms.ComboBox();
        ddlRaceNativeAtmosphere = new System.Windows.Forms.ComboBox();
        label17 = new System.Windows.Forms.Label();
        txtRaceName = new System.Windows.Forms.TextBox();
        ddlRaceNativeSurface = new System.Windows.Forms.ComboBox();
        label13 = new System.Windows.Forms.Label();
        label15 = new System.Windows.Forms.Label();
        chkAIsCanUse = new System.Windows.Forms.CheckBox();
        label23 = new System.Windows.Forms.Label();
        btnCompareCultures = new Controls.GameButton();
        txtCulture = new System.Windows.Forms.Label();
        label22 = new System.Windows.Forms.Label();
        ddlCulture = new System.Windows.Forms.ComboBox();
        ddlAI = new System.Windows.Forms.ComboBox();
        picShipset = new Controls.Blazor.GamePictureBox();
        ddlShipset = new System.Windows.Forms.ComboBox();
        picInsignia = new Controls.Blazor.GamePictureBox();
        ddlInsignia = new System.Windows.Forms.ComboBox();
        picColor = new Controls.Blazor.GamePictureBox();
        spnColorBlue = new System.Windows.Forms.NumericUpDown();
        spnColorGreen = new System.Windows.Forms.NumericUpDown();
        spnColorRed = new System.Windows.Forms.NumericUpDown();
        picLeaderPortrait = new Controls.Blazor.GamePictureBox();
        ddlLeaderPortrait = new System.Windows.Forms.ComboBox();
        txtLeaderName = new System.Windows.Forms.TextBox();
        txtName = new System.Windows.Forms.TextBox();
        label7 = new System.Windows.Forms.Label();
        label6 = new System.Windows.Forms.Label();
        label5 = new System.Windows.Forms.Label();
        label4 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        label1 = new System.Windows.Forms.Label();
        label32 = new System.Windows.Forms.Label();
        tabAptitudes = new System.Windows.Forms.TabPage();
        aptitudePicker = new Controls.AptitudePicker();
        tabTraits = new System.Windows.Forms.TabPage();
        raceTraitPicker = new Controls.TraitPicker();
        tabs = new Controls.GameTabControl();
        tabGeneral.SuspendLayout();
        gamePanel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)picRacePopulationIcon).BeginInit();
        ((System.ComponentModel.ISupportInitialize)picShipset).BeginInit();
        ((System.ComponentModel.ISupportInitialize)picInsignia).BeginInit();
        ((System.ComponentModel.ISupportInitialize)picColor).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnColorBlue).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnColorGreen).BeginInit();
        ((System.ComponentModel.ISupportInitialize)spnColorRed).BeginInit();
        ((System.ComponentModel.ISupportInitialize)picLeaderPortrait).BeginInit();
        tabAptitudes.SuspendLayout();
        tabTraits.SuspendLayout();
        tabs.SuspendLayout();
        SuspendLayout();
        // 
        // txtPointsAvailable
        // 
        txtPointsAvailable.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        txtPointsAvailable.Location = new System.Drawing.Point(183, 770);
        txtPointsAvailable.Name = "txtPointsAvailable";
        txtPointsAvailable.Size = new System.Drawing.Size(172, 17);
        txtPointsAvailable.TabIndex = 5;
        txtPointsAvailable.Text = "Points Available: 0 / 0";
        txtPointsAvailable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // btnCancel
        // 
        btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        btnCancel.BackColor = System.Drawing.Color.Black;
        btnCancel.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnCancel.Location = new System.Drawing.Point(362, 767);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new System.Drawing.Size(80, 22);
        btnCancel.TabIndex = 3;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = false;
        btnCancel.Click += btnCancel_Click;
        // 
        // btnOK
        // 
        btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        btnOK.BackColor = System.Drawing.Color.Black;
        btnOK.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnOK.Location = new System.Drawing.Point(448, 767);
        btnOK.Name = "btnOK";
        btnOK.Size = new System.Drawing.Size(80, 22);
        btnOK.TabIndex = 4;
        btnOK.Text = "OK";
        btnOK.UseVisualStyleBackColor = false;
        btnOK.Click += btnOK_Click;
        // 
        // tabGeneral
        // 
        tabGeneral.BackColor = System.Drawing.Color.Black;
        tabGeneral.Controls.Add(label8);
        tabGeneral.Controls.Add(ddlDesignNames);
        tabGeneral.Controls.Add(label2);
        tabGeneral.Controls.Add(gamePanel1);
        tabGeneral.Controls.Add(chkAIsCanUse);
        tabGeneral.Controls.Add(label23);
        tabGeneral.Controls.Add(btnCompareCultures);
        tabGeneral.Controls.Add(txtCulture);
        tabGeneral.Controls.Add(label22);
        tabGeneral.Controls.Add(ddlCulture);
        tabGeneral.Controls.Add(ddlAI);
        tabGeneral.Controls.Add(picShipset);
        tabGeneral.Controls.Add(ddlShipset);
        tabGeneral.Controls.Add(picInsignia);
        tabGeneral.Controls.Add(ddlInsignia);
        tabGeneral.Controls.Add(picColor);
        tabGeneral.Controls.Add(spnColorBlue);
        tabGeneral.Controls.Add(spnColorGreen);
        tabGeneral.Controls.Add(spnColorRed);
        tabGeneral.Controls.Add(picLeaderPortrait);
        tabGeneral.Controls.Add(ddlLeaderPortrait);
        tabGeneral.Controls.Add(txtLeaderName);
        tabGeneral.Controls.Add(txtName);
        tabGeneral.Controls.Add(label7);
        tabGeneral.Controls.Add(label6);
        tabGeneral.Controls.Add(label5);
        tabGeneral.Controls.Add(label4);
        tabGeneral.Controls.Add(label3);
        tabGeneral.Controls.Add(label1);
        tabGeneral.Controls.Add(label32);
        tabGeneral.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        tabGeneral.Location = new System.Drawing.Point(4, 29);
        tabGeneral.Name = "tabGeneral";
        tabGeneral.Padding = new System.Windows.Forms.Padding(3);
        tabGeneral.Size = new System.Drawing.Size(519, 722);
        tabGeneral.TabIndex = 0;
        tabGeneral.Text = "General";
        // 
        // label8
        // 
        label8.AutoSize = true;
        label8.ForeColor = System.Drawing.Color.CornflowerBlue;
        label8.Location = new System.Drawing.Point(9, 437);
        label8.Margin = new System.Windows.Forms.Padding(3);
        label8.Name = "label8";
        label8.Size = new System.Drawing.Size(89, 15);
        label8.TabIndex = 100;
        label8.Text = "Design Names";
        // 
        // ddlDesignNames
        // 
        ddlDesignNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        ddlDesignNames.FormattingEnabled = true;
        ddlDesignNames.Location = new System.Drawing.Point(138, 434);
        ddlDesignNames.Name = "ddlDesignNames";
        ddlDesignNames.Size = new System.Drawing.Size(180, 23);
        ddlDesignNames.TabIndex = 99;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.ForeColor = System.Drawing.Color.CornflowerBlue;
        label2.Location = new System.Drawing.Point(9, 463);
        label2.Margin = new System.Windows.Forms.Padding(3);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(81, 15);
        label2.TabIndex = 15;
        label2.Text = "Primary Race";
        // 
        // gamePanel1
        // 
        gamePanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        gamePanel1.BackColor = System.Drawing.Color.Black;
        gamePanel1.BorderColor = System.Drawing.Color.CornflowerBlue;
        gamePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        gamePanel1.Controls.Add(txtRaceHappiness);
        gamePanel1.Controls.Add(ddlRaceHappiness);
        gamePanel1.Controls.Add(label19);
        gamePanel1.Controls.Add(picRacePopulationIcon);
        gamePanel1.Controls.Add(label18);
        gamePanel1.Controls.Add(ddlRacePopulationIcon);
        gamePanel1.Controls.Add(ddlRaceNativeAtmosphere);
        gamePanel1.Controls.Add(label17);
        gamePanel1.Controls.Add(txtRaceName);
        gamePanel1.Controls.Add(ddlRaceNativeSurface);
        gamePanel1.Controls.Add(label13);
        gamePanel1.Controls.Add(label15);
        gamePanel1.ForeColor = System.Drawing.Color.White;
        gamePanel1.Location = new System.Drawing.Point(9, 484);
        gamePanel1.Name = "gamePanel1";
        gamePanel1.Padding = new System.Windows.Forms.Padding(3);
        gamePanel1.Size = new System.Drawing.Size(494, 232);
        gamePanel1.TabIndex = 98;
        // 
        // txtRaceHappiness
        // 
        txtRaceHappiness.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        txtRaceHappiness.Location = new System.Drawing.Point(25, 143);
        txtRaceHappiness.Name = "txtRaceHappiness";
        txtRaceHappiness.Size = new System.Drawing.Size(297, 84);
        txtRaceHappiness.TabIndex = 99;
        txtRaceHappiness.Text = "Please choose a happiness model.";
        // 
        // ddlRaceHappiness
        // 
        ddlRaceHappiness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        ddlRaceHappiness.FormattingEnabled = true;
        ddlRaceHappiness.Location = new System.Drawing.Point(141, 119);
        ddlRaceHappiness.Name = "ddlRaceHappiness";
        ddlRaceHappiness.Size = new System.Drawing.Size(180, 23);
        ddlRaceHappiness.TabIndex = 92;
        // 
        // label19
        // 
        label19.AutoSize = true;
        label19.ForeColor = System.Drawing.Color.CornflowerBlue;
        label19.Location = new System.Drawing.Point(9, 122);
        label19.Margin = new System.Windows.Forms.Padding(3);
        label19.Name = "label19";
        label19.Size = new System.Drawing.Size(66, 15);
        label19.TabIndex = 98;
        label19.Text = "Happiness";
        // 
        // picRacePopulationIcon
        // 
        picRacePopulationIcon.BackColor = System.Drawing.Color.Black;
        picRacePopulationIcon.ForeColor = System.Drawing.Color.White;
        picRacePopulationIcon.Image = (System.Drawing.Image)resources.GetObject("picRacePopulationIcon.Image");
        picRacePopulationIcon.Location = new System.Drawing.Point(328, 33);
        picRacePopulationIcon.Margin = new System.Windows.Forms.Padding(0);
        picRacePopulationIcon.Name = "picRacePopulationIcon";
        picRacePopulationIcon.Size = new System.Drawing.Size(22, 20);
        picRacePopulationIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        picRacePopulationIcon.TabIndex = 97;
        picRacePopulationIcon.TabStop = false;
        // 
        // label18
        // 
        label18.AutoSize = true;
        label18.ForeColor = System.Drawing.Color.CornflowerBlue;
        label18.Location = new System.Drawing.Point(10, 36);
        label18.Margin = new System.Windows.Forms.Padding(3);
        label18.Name = "label18";
        label18.Size = new System.Drawing.Size(92, 15);
        label18.TabIndex = 96;
        label18.Text = "Population Icon";
        // 
        // ddlRacePopulationIcon
        // 
        ddlRacePopulationIcon.FormattingEnabled = true;
        ddlRacePopulationIcon.Location = new System.Drawing.Point(142, 33);
        ddlRacePopulationIcon.Name = "ddlRacePopulationIcon";
        ddlRacePopulationIcon.Size = new System.Drawing.Size(180, 23);
        ddlRacePopulationIcon.TabIndex = 89;
        ddlRacePopulationIcon.TextChanged += ddlRacePopulationIcon_TextChanged;
        ddlRacePopulationIcon.Leave += ddlPic_Leave;
        // 
        // ddlRaceNativeAtmosphere
        // 
        ddlRaceNativeAtmosphere.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        ddlRaceNativeAtmosphere.FormattingEnabled = true;
        ddlRaceNativeAtmosphere.Location = new System.Drawing.Point(142, 90);
        ddlRaceNativeAtmosphere.Name = "ddlRaceNativeAtmosphere";
        ddlRaceNativeAtmosphere.Size = new System.Drawing.Size(180, 23);
        ddlRaceNativeAtmosphere.TabIndex = 91;
        // 
        // label17
        // 
        label17.AutoSize = true;
        label17.ForeColor = System.Drawing.Color.CornflowerBlue;
        label17.Location = new System.Drawing.Point(10, 93);
        label17.Margin = new System.Windows.Forms.Padding(3);
        label17.Name = "label17";
        label17.Size = new System.Drawing.Size(110, 15);
        label17.TabIndex = 95;
        label17.Text = "Native Atmosphere";
        // 
        // txtRaceName
        // 
        txtRaceName.Location = new System.Drawing.Point(141, 6);
        txtRaceName.Name = "txtRaceName";
        txtRaceName.Size = new System.Drawing.Size(180, 21);
        txtRaceName.TabIndex = 88;
        // 
        // ddlRaceNativeSurface
        // 
        ddlRaceNativeSurface.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        ddlRaceNativeSurface.FormattingEnabled = true;
        ddlRaceNativeSurface.Location = new System.Drawing.Point(142, 61);
        ddlRaceNativeSurface.Name = "ddlRaceNativeSurface";
        ddlRaceNativeSurface.Size = new System.Drawing.Size(180, 23);
        ddlRaceNativeSurface.TabIndex = 90;
        // 
        // label13
        // 
        label13.AutoSize = true;
        label13.ForeColor = System.Drawing.Color.CornflowerBlue;
        label13.Location = new System.Drawing.Point(10, 64);
        label13.Margin = new System.Windows.Forms.Padding(3);
        label13.Name = "label13";
        label13.Size = new System.Drawing.Size(86, 15);
        label13.TabIndex = 94;
        label13.Text = "Native Surface";
        // 
        // label15
        // 
        label15.AutoSize = true;
        label15.ForeColor = System.Drawing.Color.CornflowerBlue;
        label15.Location = new System.Drawing.Point(9, 9);
        label15.Margin = new System.Windows.Forms.Padding(3);
        label15.Name = "label15";
        label15.Size = new System.Drawing.Size(41, 15);
        label15.TabIndex = 93;
        label15.Text = "Name";
        // 
        // chkAIsCanUse
        // 
        chkAIsCanUse.AutoSize = true;
        chkAIsCanUse.Location = new System.Drawing.Point(138, 30);
        chkAIsCanUse.Name = "chkAIsCanUse";
        chkAIsCanUse.Size = new System.Drawing.Size(55, 19);
        chkAIsCanUse.TabIndex = 2;
        chkAIsCanUse.Text = "Allow";
        chkAIsCanUse.UseVisualStyleBackColor = true;
        // 
        // label23
        // 
        label23.AutoSize = true;
        label23.ForeColor = System.Drawing.Color.CornflowerBlue;
        label23.Location = new System.Drawing.Point(9, 30);
        label23.Margin = new System.Windows.Forms.Padding(3);
        label23.Name = "label23";
        label23.Size = new System.Drawing.Size(116, 15);
        label23.TabIndex = 97;
        label23.Text = "AI Empires Can Use";
        // 
        // btnCompareCultures
        // 
        btnCompareCultures.BackColor = System.Drawing.Color.Black;
        btnCompareCultures.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnCompareCultures.Location = new System.Drawing.Point(238, 293);
        btnCompareCultures.Name = "btnCompareCultures";
        btnCompareCultures.Size = new System.Drawing.Size(80, 22);
        btnCompareCultures.TabIndex = 21;
        btnCompareCultures.Text = "Compare";
        btnCompareCultures.UseVisualStyleBackColor = false;
        btnCompareCultures.Click += btnCompareCultures_Click;
        // 
        // txtCulture
        // 
        txtCulture.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        txtCulture.Location = new System.Drawing.Point(22, 291);
        txtCulture.Name = "txtCulture";
        txtCulture.Size = new System.Drawing.Size(210, 140);
        txtCulture.TabIndex = 94;
        txtCulture.Text = "Please choose a culture.";
        // 
        // label22
        // 
        label22.AutoSize = true;
        label22.ForeColor = System.Drawing.Color.CornflowerBlue;
        label22.Location = new System.Drawing.Point(9, 267);
        label22.Margin = new System.Windows.Forms.Padding(3);
        label22.Name = "label22";
        label22.Size = new System.Drawing.Size(46, 15);
        label22.TabIndex = 93;
        label22.Text = "Culture";
        // 
        // ddlCulture
        // 
        ddlCulture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        ddlCulture.FormattingEnabled = true;
        ddlCulture.Location = new System.Drawing.Point(138, 264);
        ddlCulture.Name = "ddlCulture";
        ddlCulture.Size = new System.Drawing.Size(180, 23);
        ddlCulture.TabIndex = 19;
        ddlCulture.SelectedIndexChanged += ddlCulture_SelectedIndexChanged;
        // 
        // ddlAI
        // 
        ddlAI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        ddlAI.FormattingEnabled = true;
        ddlAI.Location = new System.Drawing.Point(138, 235);
        ddlAI.Name = "ddlAI";
        ddlAI.Size = new System.Drawing.Size(180, 23);
        ddlAI.TabIndex = 15;
        // 
        // picShipset
        // 
        picShipset.BackColor = System.Drawing.Color.Black;
        picShipset.ForeColor = System.Drawing.Color.White;
        picShipset.Image = (System.Drawing.Image)resources.GetObject("picShipset.Image");
        picShipset.Location = new System.Drawing.Point(324, 206);
        picShipset.Margin = new System.Windows.Forms.Padding(0);
        picShipset.Name = "picShipset";
        picShipset.Size = new System.Drawing.Size(137, 126);
        picShipset.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        picShipset.TabIndex = 41;
        picShipset.TabStop = false;
        // 
        // ddlShipset
        // 
        ddlShipset.FormattingEnabled = true;
        ddlShipset.Location = new System.Drawing.Point(138, 206);
        ddlShipset.Name = "ddlShipset";
        ddlShipset.Size = new System.Drawing.Size(180, 23);
        ddlShipset.TabIndex = 13;
        ddlShipset.TextChanged += ddlShipset_TextChanged;
        ddlShipset.Leave += ddlPic_Leave;
        // 
        // picInsignia
        // 
        picInsignia.BackColor = System.Drawing.Color.Black;
        picInsignia.ForeColor = System.Drawing.Color.White;
        picInsignia.Image = (System.Drawing.Image)resources.GetObject("picInsignia.Image");
        picInsignia.Location = new System.Drawing.Point(322, 178);
        picInsignia.Margin = new System.Windows.Forms.Padding(0);
        picInsignia.Name = "picInsignia";
        picInsignia.Size = new System.Drawing.Size(22, 20);
        picInsignia.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        picInsignia.TabIndex = 38;
        picInsignia.TabStop = false;
        // 
        // ddlInsignia
        // 
        ddlInsignia.FormattingEnabled = true;
        ddlInsignia.Location = new System.Drawing.Point(138, 178);
        ddlInsignia.Name = "ddlInsignia";
        ddlInsignia.Size = new System.Drawing.Size(180, 23);
        ddlInsignia.TabIndex = 11;
        ddlInsignia.TextChanged += ddlInsignia_TextChanged;
        ddlInsignia.Leave += ddlPic_Leave;
        // 
        // picColor
        // 
        picColor.BackColor = System.Drawing.Color.Black;
        picColor.ForeColor = System.Drawing.Color.White;
        picColor.Image = (System.Drawing.Image)resources.GetObject("picColor.Image");
        picColor.Location = new System.Drawing.Point(324, 152);
        picColor.Margin = new System.Windows.Forms.Padding(0);
        picColor.Name = "picColor";
        picColor.Size = new System.Drawing.Size(22, 20);
        picColor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        picColor.TabIndex = 35;
        picColor.TabStop = false;
        // 
        // spnColorBlue
        // 
        spnColorBlue.Increment = new decimal(new int[] { 85, 0, 0, 0 });
        spnColorBlue.Location = new System.Drawing.Point(253, 152);
        spnColorBlue.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
        spnColorBlue.Name = "spnColorBlue";
        spnColorBlue.Size = new System.Drawing.Size(51, 21);
        spnColorBlue.TabIndex = 9;
        spnColorBlue.Value = new decimal(new int[] { 255, 0, 0, 0 });
        spnColorBlue.ValueChanged += spnColor_ValueChanged;
        spnColorBlue.Leave += spnColor_Leave;
        // 
        // spnColorGreen
        // 
        spnColorGreen.Increment = new decimal(new int[] { 85, 0, 0, 0 });
        spnColorGreen.Location = new System.Drawing.Point(195, 152);
        spnColorGreen.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
        spnColorGreen.Name = "spnColorGreen";
        spnColorGreen.Size = new System.Drawing.Size(51, 21);
        spnColorGreen.TabIndex = 8;
        spnColorGreen.Value = new decimal(new int[] { 255, 0, 0, 0 });
        spnColorGreen.ValueChanged += spnColor_ValueChanged;
        spnColorGreen.Leave += spnColor_Leave;
        // 
        // spnColorRed
        // 
        spnColorRed.Increment = new decimal(new int[] { 85, 0, 0, 0 });
        spnColorRed.Location = new System.Drawing.Point(138, 152);
        spnColorRed.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
        spnColorRed.Name = "spnColorRed";
        spnColorRed.Size = new System.Drawing.Size(51, 21);
        spnColorRed.TabIndex = 7;
        spnColorRed.Value = new decimal(new int[] { 255, 0, 0, 0 });
        spnColorRed.ValueChanged += spnColor_ValueChanged;
        spnColorRed.Leave += spnColor_Leave;
        // 
        // picLeaderPortrait
        // 
        picLeaderPortrait.BackColor = System.Drawing.Color.Black;
        picLeaderPortrait.ForeColor = System.Drawing.Color.White;
        picLeaderPortrait.Image = (System.Drawing.Image)resources.GetObject("picLeaderPortrait.Image");
        picLeaderPortrait.Location = new System.Drawing.Point(324, 82);
        picLeaderPortrait.Margin = new System.Windows.Forms.Padding(0);
        picLeaderPortrait.Name = "picLeaderPortrait";
        picLeaderPortrait.Size = new System.Drawing.Size(68, 63);
        picLeaderPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        picLeaderPortrait.TabIndex = 25;
        picLeaderPortrait.TabStop = false;
        // 
        // ddlLeaderPortrait
        // 
        ddlLeaderPortrait.FormattingEnabled = true;
        ddlLeaderPortrait.Location = new System.Drawing.Point(138, 82);
        ddlLeaderPortrait.Name = "ddlLeaderPortrait";
        ddlLeaderPortrait.Size = new System.Drawing.Size(180, 23);
        ddlLeaderPortrait.TabIndex = 5;
        ddlLeaderPortrait.TextChanged += ddlLeaderPortrait_TextChanged;
        ddlLeaderPortrait.Leave += ddlPic_Leave;
        // 
        // txtLeaderName
        // 
        txtLeaderName.Location = new System.Drawing.Point(138, 54);
        txtLeaderName.Name = "txtLeaderName";
        txtLeaderName.Size = new System.Drawing.Size(180, 21);
        txtLeaderName.TabIndex = 3;
        // 
        // txtName
        // 
        txtName.Location = new System.Drawing.Point(138, 3);
        txtName.Name = "txtName";
        txtName.Size = new System.Drawing.Size(180, 21);
        txtName.TabIndex = 0;
        // 
        // label7
        // 
        label7.AutoSize = true;
        label7.ForeColor = System.Drawing.Color.CornflowerBlue;
        label7.Location = new System.Drawing.Point(9, 238);
        label7.Margin = new System.Windows.Forms.Padding(3);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(17, 15);
        label7.TabIndex = 21;
        label7.Text = "AI";
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.ForeColor = System.Drawing.Color.CornflowerBlue;
        label6.Location = new System.Drawing.Point(6, 85);
        label6.Margin = new System.Windows.Forms.Padding(3);
        label6.Name = "label6";
        label6.Size = new System.Drawing.Size(88, 15);
        label6.TabIndex = 20;
        label6.Text = "Leader Portrait";
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.ForeColor = System.Drawing.Color.CornflowerBlue;
        label5.Location = new System.Drawing.Point(9, 210);
        label5.Margin = new System.Windows.Forms.Padding(3);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(48, 15);
        label5.TabIndex = 19;
        label5.Text = "Shipset";
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.ForeColor = System.Drawing.Color.CornflowerBlue;
        label4.Location = new System.Drawing.Point(6, 181);
        label4.Margin = new System.Windows.Forms.Padding(3);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(50, 15);
        label4.TabIndex = 18;
        label4.Text = "Insignia";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.ForeColor = System.Drawing.Color.CornflowerBlue;
        label3.Location = new System.Drawing.Point(6, 154);
        label3.Margin = new System.Windows.Forms.Padding(3);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(65, 15);
        label3.TabIndex = 17;
        label3.Text = "Color RGB";
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.ForeColor = System.Drawing.Color.CornflowerBlue;
        label1.Location = new System.Drawing.Point(6, 57);
        label1.Margin = new System.Windows.Forms.Padding(3);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(83, 15);
        label1.TabIndex = 15;
        label1.Text = "Leader Name";
        // 
        // label32
        // 
        label32.AutoSize = true;
        label32.ForeColor = System.Drawing.Color.CornflowerBlue;
        label32.Location = new System.Drawing.Point(6, 6);
        label32.Margin = new System.Windows.Forms.Padding(3);
        label32.Name = "label32";
        label32.Size = new System.Drawing.Size(41, 15);
        label32.TabIndex = 14;
        label32.Text = "Name";
        // 
        // tabAptitudes
        // 
        tabAptitudes.BackColor = System.Drawing.Color.Black;
        tabAptitudes.Controls.Add(aptitudePicker);
        tabAptitudes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        tabAptitudes.Location = new System.Drawing.Point(4, 29);
        tabAptitudes.Name = "tabAptitudes";
        tabAptitudes.Padding = new System.Windows.Forms.Padding(3);
        tabAptitudes.Size = new System.Drawing.Size(519, 722);
        tabAptitudes.TabIndex = 4;
        tabAptitudes.Text = "Aptitudes";
        // 
        // aptitudePicker
        // 
        aptitudePicker.BackColor = System.Drawing.Color.Black;
        aptitudePicker.Dock = System.Windows.Forms.DockStyle.Fill;
        aptitudePicker.ForeColor = System.Drawing.Color.White;
        aptitudePicker.Location = new System.Drawing.Point(3, 3);
        aptitudePicker.Margin = new System.Windows.Forms.Padding(4);
        aptitudePicker.Name = "aptitudePicker";
        aptitudePicker.Size = new System.Drawing.Size(513, 716);
        aptitudePicker.TabIndex = 0;
        aptitudePicker.Values = null;
        aptitudePicker.AptitudeValueChanged += aptitudePicker_AptitudeValueChanged;
        // 
        // tabTraits
        // 
        tabTraits.BackColor = System.Drawing.Color.Black;
        tabTraits.Controls.Add(raceTraitPicker);
        tabTraits.Location = new System.Drawing.Point(4, 29);
        tabTraits.Name = "tabTraits";
        tabTraits.Size = new System.Drawing.Size(519, 722);
        tabTraits.TabIndex = 5;
        tabTraits.Text = "Traits";
        // 
        // raceTraitPicker
        // 
        raceTraitPicker.BackColor = System.Drawing.Color.Black;
        raceTraitPicker.Dock = System.Windows.Forms.DockStyle.Fill;
        raceTraitPicker.ForeColor = System.Drawing.Color.White;
        raceTraitPicker.Location = new System.Drawing.Point(0, 0);
        raceTraitPicker.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
        raceTraitPicker.Name = "raceTraitPicker";
        raceTraitPicker.Size = new System.Drawing.Size(519, 722);
        raceTraitPicker.TabIndex = 0;
        raceTraitPicker.Traits = null;
        raceTraitPicker.TraitToggled += raceTraitPicker_TraitToggled;
        // 
        // tabs
        // 
        tabs.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        tabs.Controls.Add(tabGeneral);
        tabs.Controls.Add(tabTraits);
        tabs.Controls.Add(tabAptitudes);
        tabs.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
        tabs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        tabs.Location = new System.Drawing.Point(10, 9);
        tabs.Margin = new System.Windows.Forms.Padding(0);
        tabs.Name = "tabs";
        tabs.SelectedIndex = 0;
        tabs.SelectedTabBackColor = System.Drawing.Color.CornflowerBlue;
        tabs.SelectedTabForeColor = System.Drawing.Color.Black;
        tabs.Size = new System.Drawing.Size(527, 755);
        tabs.TabBackColor = System.Drawing.Color.Black;
        tabs.TabBorderColor = System.Drawing.Color.CornflowerBlue;
        tabs.TabForeColor = System.Drawing.Color.CornflowerBlue;
        tabs.TabIndex = 0;
        // 
        // EmpireSetupForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        BackColor = System.Drawing.Color.Black;
        ClientSize = new System.Drawing.Size(546, 802);
        Controls.Add(txtPointsAvailable);
        Controls.Add(btnCancel);
        Controls.Add(btnOK);
        Controls.Add(tabs);
        Name = "EmpireSetupForm";
        ShowInTaskbar = false;
        Text = "Empire Setup";
        tabGeneral.ResumeLayout(false);
        tabGeneral.PerformLayout();
        gamePanel1.ResumeLayout(false);
        gamePanel1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)picRacePopulationIcon).EndInit();
        ((System.ComponentModel.ISupportInitialize)picShipset).EndInit();
        ((System.ComponentModel.ISupportInitialize)picInsignia).EndInit();
        ((System.ComponentModel.ISupportInitialize)picColor).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnColorBlue).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnColorGreen).EndInit();
        ((System.ComponentModel.ISupportInitialize)spnColorRed).EndInit();
        ((System.ComponentModel.ISupportInitialize)picLeaderPortrait).EndInit();
        tabAptitudes.ResumeLayout(false);
        tabTraits.ResumeLayout(false);
        tabs.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Controls.GameButton btnOK;
	private Controls.GameButton btnCancel;
	private System.Windows.Forms.Label txtPointsAvailable;
	private System.Windows.Forms.TabPage tabGeneral;
	private System.Windows.Forms.CheckBox chkAIsCanUse;
	private System.Windows.Forms.Label label23;
	private Controls.GameButton btnCompareCultures;
	private System.Windows.Forms.Label txtCulture;
	private System.Windows.Forms.Label label22;
	private System.Windows.Forms.ComboBox ddlCulture;
	private System.Windows.Forms.ComboBox ddlAI;
	private FrEee.UI.WinForms.Controls.Blazor.GamePictureBox picShipset;
	private System.Windows.Forms.ComboBox ddlShipset;
	private FrEee.UI.WinForms.Controls.Blazor.GamePictureBox picInsignia;
	private System.Windows.Forms.ComboBox ddlInsignia;
	private FrEee.UI.WinForms.Controls.Blazor.GamePictureBox picColor;
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
	private FrEee.UI.WinForms.Controls.Blazor.GamePictureBox picRacePopulationIcon;
	private System.Windows.Forms.Label label18;
	private System.Windows.Forms.ComboBox ddlRacePopulationIcon;
	private System.Windows.Forms.ComboBox ddlRaceNativeAtmosphere;
	private System.Windows.Forms.Label label17;
	private System.Windows.Forms.TextBox txtRaceName;
	private System.Windows.Forms.ComboBox ddlRaceNativeSurface;
	private System.Windows.Forms.Label label13;
	private System.Windows.Forms.Label label15;
	private FrEee.UI.WinForms.Controls.Blazor.GamePictureBox picLeaderPortrait;
	private System.Windows.Forms.ComboBox ddlLeaderPortrait;
	private System.Windows.Forms.TextBox txtLeaderName;
	private System.Windows.Forms.Label label6;
	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.Label label8;
	private System.Windows.Forms.ComboBox ddlDesignNames;
}