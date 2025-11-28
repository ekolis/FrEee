namespace FrEee.UI.WinForms.Forms;

partial class VehicleDesignForm
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

	protected override string WikiPage => "VehicleDesign";

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VehicleDesignForm));
        label1 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        ddlRole = new System.Windows.Forms.ComboBox();
        label3 = new System.Windows.Forms.Label();
        ddlName = new System.Windows.Forms.ComboBox();
        label4 = new System.Windows.Forms.Label();
        label5 = new System.Windows.Forms.Label();
        label6 = new System.Windows.Forms.Label();
        label7 = new System.Windows.Forms.Label();
        label8 = new System.Windows.Forms.Label();
        chkOnlyLatest = new System.Windows.Forms.CheckBox();
        chkFilterByMount = new System.Windows.Forms.CheckBox();
        btnWeaponsReport = new Controls.GameButton();
        btnCancel = new Controls.GameButton();
        btnSave = new Controls.GameButton();
        pnlStats = new Controls.GamePanel();
        txtEngines = new System.Windows.Forms.Label();
        label14 = new System.Windows.Forms.Label();
        txtEvasion = new System.Windows.Forms.Label();
        label18 = new System.Windows.Forms.Label();
        txtAccuracy = new System.Windows.Forms.Label();
        label11 = new System.Windows.Forms.Label();
        txtRange = new System.Windows.Forms.Label();
        label22 = new System.Windows.Forms.Label();
        txtHull = new System.Windows.Forms.Label();
        txtArmor = new System.Windows.Forms.Label();
        txtShields = new System.Windows.Forms.Label();
        txtSpeed = new System.Windows.Forms.Label();
        txtSupplyStorage = new System.Windows.Forms.Label();
        label17 = new System.Windows.Forms.Label();
        txtCargo = new System.Windows.Forms.Label();
        label15 = new System.Windows.Forms.Label();
        label13 = new System.Windows.Forms.Label();
        label12 = new System.Windows.Forms.Label();
        txtSpaceFree = new System.Windows.Forms.Label();
        resCostRad = new Controls.ResourceDisplay();
        resCostOrg = new Controls.ResourceDisplay();
        resCostMin = new Controls.ResourceDisplay();
        label10 = new System.Windows.Forms.Label();
        label9 = new System.Windows.Forms.Label();
        gamePanel4 = new Controls.GamePanel();
        txtDetailDescription = new System.Windows.Forms.Label();
        resDetailRad = new Controls.ResourceDisplay();
        resDetailOrg = new Controls.ResourceDisplay();
        resDetailMin = new Controls.ResourceDisplay();
        txtDetailSize = new System.Windows.Forms.Label();
        txtDetailName = new System.Windows.Forms.Label();
        picDetailIcon = new Controls.GamePictureBox();
        gamePanel3 = new Controls.GamePanel();
        lstWarnings = new System.Windows.Forms.TextBox();
        gamePanel2 = new Controls.GamePanel();
        lstComponentsInstalled = new System.Windows.Forms.ListView();
        gamePanel1 = new Controls.GamePanel();
        lstComponentsAvailable = new System.Windows.Forms.ListView();
        btnHull = new Controls.GameButton();
        txtIteration = new System.Windows.Forms.Label();
        picPortrait = new Controls.GamePictureBox();
        btnMountInfo = new Controls.GameButton();
        ddlMount = new System.Windows.Forms.ComboBox();
        btnHelp = new Controls.GameButton();
        btnMount = new Controls.GameButton();
        pnlStats.SuspendLayout();
        gamePanel4.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)picDetailIcon).BeginInit();
        gamePanel3.SuspendLayout();
        gamePanel2.SuspendLayout();
        gamePanel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)picPortrait).BeginInit();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.ForeColor = System.Drawing.Color.CornflowerBlue;
        label1.Location = new System.Drawing.Point(15, 15);
        label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(29, 15);
        label1.TabIndex = 0;
        label1.Text = "Hull";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.ForeColor = System.Drawing.Color.CornflowerBlue;
        label2.Location = new System.Drawing.Point(15, 47);
        label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(30, 15);
        label2.TabIndex = 3;
        label2.Text = "Role";
        // 
        // ddlRole
        // 
        ddlRole.BackColor = System.Drawing.Color.Black;
        ddlRole.Enabled = false;
        ddlRole.ForeColor = System.Drawing.Color.White;
        ddlRole.FormattingEnabled = true;
        ddlRole.Location = new System.Drawing.Point(93, 44);
        ddlRole.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        ddlRole.Name = "ddlRole";
        ddlRole.Size = new System.Drawing.Size(194, 23);
        ddlRole.TabIndex = 4;
        ddlRole.TextChanged += ddlRole_TextChanged;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.ForeColor = System.Drawing.Color.CornflowerBlue;
        label3.Location = new System.Drawing.Point(15, 81);
        label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(39, 15);
        label3.TabIndex = 5;
        label3.Text = "Name";
        // 
        // ddlName
        // 
        ddlName.BackColor = System.Drawing.Color.Black;
        ddlName.Enabled = false;
        ddlName.ForeColor = System.Drawing.Color.White;
        ddlName.FormattingEnabled = true;
        ddlName.Location = new System.Drawing.Point(93, 77);
        ddlName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        ddlName.Name = "ddlName";
        ddlName.Size = new System.Drawing.Size(163, 23);
        ddlName.TabIndex = 6;
        ddlName.TextChanged += ddlName_TextChanged;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.ForeColor = System.Drawing.Color.CornflowerBlue;
        label4.Location = new System.Drawing.Point(15, 160);
        label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(127, 15);
        label4.TabIndex = 8;
        label4.Text = "Components Available";
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.ForeColor = System.Drawing.Color.CornflowerBlue;
        label5.Location = new System.Drawing.Point(296, 160);
        label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(123, 15);
        label5.TabIndex = 10;
        label5.Text = "Components Installed";
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.ForeColor = System.Drawing.Color.CornflowerBlue;
        label6.Location = new System.Drawing.Point(15, 114);
        label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label6.Name = "label6";
        label6.Size = new System.Drawing.Size(65, 15);
        label6.TabIndex = 13;
        label6.Text = "Use Mount";
        // 
        // label7
        // 
        label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        label7.AutoSize = true;
        label7.ForeColor = System.Drawing.Color.CornflowerBlue;
        label7.Location = new System.Drawing.Point(14, 608);
        label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(57, 15);
        label7.TabIndex = 18;
        label7.Text = "Warnings";
        // 
        // label8
        // 
        label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        label8.AutoSize = true;
        label8.ForeColor = System.Drawing.Color.CornflowerBlue;
        label8.Location = new System.Drawing.Point(296, 608);
        label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label8.Name = "label8";
        label8.Size = new System.Drawing.Size(109, 15);
        label8.TabIndex = 20;
        label8.Text = "Component Details";
        // 
        // chkOnlyLatest
        // 
        chkOnlyLatest.AutoSize = true;
        chkOnlyLatest.Checked = true;
        chkOnlyLatest.CheckState = System.Windows.Forms.CheckState.Checked;
        chkOnlyLatest.Location = new System.Drawing.Point(14, 187);
        chkOnlyLatest.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        chkOnlyLatest.Name = "chkOnlyLatest";
        chkOnlyLatest.Size = new System.Drawing.Size(85, 19);
        chkOnlyLatest.TabIndex = 21;
        chkOnlyLatest.Text = "Only Latest";
        chkOnlyLatest.UseVisualStyleBackColor = true;
        chkOnlyLatest.CheckedChanged += chkOnlyLatest_CheckedChanged;
        // 
        // chkFilterByMount
        // 
        chkFilterByMount.AutoSize = true;
        chkFilterByMount.Location = new System.Drawing.Point(113, 187);
        chkFilterByMount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        chkFilterByMount.Name = "chkFilterByMount";
        chkFilterByMount.Size = new System.Drawing.Size(107, 19);
        chkFilterByMount.TabIndex = 28;
        chkFilterByMount.Text = "Filter By Mount";
        chkFilterByMount.UseVisualStyleBackColor = true;
        chkFilterByMount.CheckedChanged += chkFilterByMount_CheckedChanged;
        // 
        // btnWeaponsReport
        // 
        btnWeaponsReport.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        btnWeaponsReport.BackColor = System.Drawing.Color.Black;
        btnWeaponsReport.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnWeaponsReport.Location = new System.Drawing.Point(643, 179);
        btnWeaponsReport.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnWeaponsReport.Name = "btnWeaponsReport";
        btnWeaponsReport.Size = new System.Drawing.Size(170, 33);
        btnWeaponsReport.TabIndex = 26;
        btnWeaponsReport.Text = "Weapons Report";
        btnWeaponsReport.UseVisualStyleBackColor = false;
        // 
        // btnCancel
        // 
        btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        btnCancel.BackColor = System.Drawing.Color.Black;
        btnCancel.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnCancel.Location = new System.Drawing.Point(643, 646);
        btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new System.Drawing.Size(170, 33);
        btnCancel.TabIndex = 24;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = false;
        btnCancel.Click += btnCancel_Click;
        // 
        // btnSave
        // 
        btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        btnSave.BackColor = System.Drawing.Color.Black;
        btnSave.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnSave.Location = new System.Drawing.Point(643, 687);
        btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnSave.Name = "btnSave";
        btnSave.Size = new System.Drawing.Size(170, 33);
        btnSave.TabIndex = 23;
        btnSave.Text = "Save";
        btnSave.UseVisualStyleBackColor = false;
        btnSave.Click += btnSave_Click;
        // 
        // pnlStats
        // 
        pnlStats.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        pnlStats.BackColor = System.Drawing.Color.Black;
        pnlStats.BorderColor = System.Drawing.Color.CornflowerBlue;
        pnlStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        pnlStats.Controls.Add(txtEngines);
        pnlStats.Controls.Add(label14);
        pnlStats.Controls.Add(txtEvasion);
        pnlStats.Controls.Add(label18);
        pnlStats.Controls.Add(txtAccuracy);
        pnlStats.Controls.Add(label11);
        pnlStats.Controls.Add(txtRange);
        pnlStats.Controls.Add(label22);
        pnlStats.Controls.Add(txtHull);
        pnlStats.Controls.Add(txtArmor);
        pnlStats.Controls.Add(txtShields);
        pnlStats.Controls.Add(txtSpeed);
        pnlStats.Controls.Add(txtSupplyStorage);
        pnlStats.Controls.Add(label17);
        pnlStats.Controls.Add(txtCargo);
        pnlStats.Controls.Add(label15);
        pnlStats.Controls.Add(label13);
        pnlStats.Controls.Add(label12);
        pnlStats.Controls.Add(txtSpaceFree);
        pnlStats.Controls.Add(resCostRad);
        pnlStats.Controls.Add(resCostOrg);
        pnlStats.Controls.Add(resCostMin);
        pnlStats.Controls.Add(label10);
        pnlStats.Controls.Add(label9);
        pnlStats.ForeColor = System.Drawing.Color.White;
        pnlStats.Location = new System.Drawing.Point(300, 9);
        pnlStats.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        pnlStats.Name = "pnlStats";
        pnlStats.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
        pnlStats.Size = new System.Drawing.Size(513, 147);
        pnlStats.TabIndex = 22;
        // 
        // txtEngines
        // 
        txtEngines.AutoSize = true;
        txtEngines.Location = new System.Drawing.Point(98, 115);
        txtEngines.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        txtEngines.Name = "txtEngines";
        txtEngines.Size = new System.Drawing.Size(30, 15);
        txtEngines.TabIndex = 28;
        txtEngines.Text = "0 / 0";
        // 
        // label14
        // 
        label14.AutoSize = true;
        label14.ForeColor = System.Drawing.Color.CornflowerBlue;
        label14.Location = new System.Drawing.Point(4, 115);
        label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label14.Name = "label14";
        label14.Size = new System.Drawing.Size(48, 15);
        label14.TabIndex = 27;
        label14.Text = "Engines";
        // 
        // txtEvasion
        // 
        txtEvasion.AutoSize = true;
        txtEvasion.Location = new System.Drawing.Point(295, 72);
        txtEvasion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        txtEvasion.Name = "txtEvasion";
        txtEvasion.Size = new System.Drawing.Size(31, 15);
        txtEvasion.TabIndex = 26;
        txtEvasion.Text = "+0%";
        // 
        // label18
        // 
        label18.AutoSize = true;
        label18.ForeColor = System.Drawing.Color.CornflowerBlue;
        label18.Location = new System.Drawing.Point(209, 70);
        label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label18.Name = "label18";
        label18.Size = new System.Drawing.Size(47, 15);
        label18.TabIndex = 25;
        label18.Text = "Evasion";
        // 
        // txtAccuracy
        // 
        txtAccuracy.AutoSize = true;
        txtAccuracy.Location = new System.Drawing.Point(295, 57);
        txtAccuracy.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        txtAccuracy.Name = "txtAccuracy";
        txtAccuracy.Size = new System.Drawing.Size(31, 15);
        txtAccuracy.TabIndex = 24;
        txtAccuracy.Text = "+0%";
        // 
        // label11
        // 
        label11.AutoSize = true;
        label11.ForeColor = System.Drawing.Color.CornflowerBlue;
        label11.Location = new System.Drawing.Point(209, 57);
        label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label11.Name = "label11";
        label11.Size = new System.Drawing.Size(56, 15);
        label11.TabIndex = 23;
        label11.Text = "Accuracy";
        // 
        // txtRange
        // 
        txtRange.AutoSize = true;
        txtRange.Location = new System.Drawing.Point(98, 100);
        txtRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        txtRange.Name = "txtRange";
        txtRange.Size = new System.Drawing.Size(53, 15);
        txtRange.TabIndex = 22;
        txtRange.Text = "0 sectors";
        // 
        // label22
        // 
        label22.AutoSize = true;
        label22.ForeColor = System.Drawing.Color.CornflowerBlue;
        label22.Location = new System.Drawing.Point(4, 100);
        label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label22.Name = "label22";
        label22.Size = new System.Drawing.Size(40, 15);
        label22.TabIndex = 21;
        label22.Text = "Range";
        // 
        // txtHull
        // 
        txtHull.AutoSize = true;
        txtHull.Location = new System.Drawing.Point(295, 37);
        txtHull.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        txtHull.Name = "txtHull";
        txtHull.Size = new System.Drawing.Size(36, 15);
        txtHull.TabIndex = 20;
        txtHull.Text = "0 hull";
        // 
        // txtArmor
        // 
        txtArmor.AutoSize = true;
        txtArmor.Location = new System.Drawing.Point(295, 20);
        txtArmor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        txtArmor.Name = "txtArmor";
        txtArmor.Size = new System.Drawing.Size(48, 15);
        txtArmor.TabIndex = 19;
        txtArmor.Text = "0 armor";
        // 
        // txtShields
        // 
        txtShields.AutoSize = true;
        txtShields.Location = new System.Drawing.Point(295, 5);
        txtShields.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        txtShields.Name = "txtShields";
        txtShields.Size = new System.Drawing.Size(110, 15);
        txtShields.TabIndex = 18;
        txtShields.Text = "0 shields (+0 regen)";
        // 
        // txtSpeed
        // 
        txtSpeed.AutoSize = true;
        txtSpeed.Location = new System.Drawing.Point(98, 72);
        txtSpeed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        txtSpeed.Name = "txtSpeed";
        txtSpeed.Size = new System.Drawing.Size(80, 15);
        txtSpeed.TabIndex = 17;
        txtSpeed.Text = "0 sectors/turn";
        // 
        // txtSupplyStorage
        // 
        txtSupplyStorage.AutoSize = true;
        txtSupplyStorage.Location = new System.Drawing.Point(98, 87);
        txtSupplyStorage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        txtSupplyStorage.Name = "txtSupplyStorage";
        txtSupplyStorage.Size = new System.Drawing.Size(13, 15);
        txtSupplyStorage.TabIndex = 16;
        txtSupplyStorage.Text = "0";
        // 
        // label17
        // 
        label17.AutoSize = true;
        label17.ForeColor = System.Drawing.Color.CornflowerBlue;
        label17.Location = new System.Drawing.Point(4, 85);
        label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label17.Name = "label17";
        label17.Size = new System.Drawing.Size(51, 15);
        label17.TabIndex = 15;
        label17.Text = "Supplies";
        // 
        // txtCargo
        // 
        txtCargo.AutoSize = true;
        txtCargo.Location = new System.Drawing.Point(296, 100);
        txtCargo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        txtCargo.Name = "txtCargo";
        txtCargo.Size = new System.Drawing.Size(25, 15);
        txtCargo.TabIndex = 14;
        txtCargo.Text = "0kT";
        // 
        // label15
        // 
        label15.AutoSize = true;
        label15.ForeColor = System.Drawing.Color.CornflowerBlue;
        label15.Location = new System.Drawing.Point(209, 100);
        label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label15.Name = "label15";
        label15.Size = new System.Drawing.Size(82, 15);
        label15.TabIndex = 13;
        label15.Text = "Cargo Storage";
        // 
        // label13
        // 
        label13.AutoSize = true;
        label13.ForeColor = System.Drawing.Color.CornflowerBlue;
        label13.Location = new System.Drawing.Point(209, 5);
        label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label13.Name = "label13";
        label13.Size = new System.Drawing.Size(58, 15);
        label13.TabIndex = 11;
        label13.Text = "Durability";
        // 
        // label12
        // 
        label12.AutoSize = true;
        label12.ForeColor = System.Drawing.Color.CornflowerBlue;
        label12.Location = new System.Drawing.Point(4, 70);
        label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label12.Name = "label12";
        label12.Size = new System.Drawing.Size(39, 15);
        label12.TabIndex = 10;
        label12.Text = "Speed";
        // 
        // txtSpaceFree
        // 
        txtSpaceFree.AutoSize = true;
        txtSpaceFree.Location = new System.Drawing.Point(98, 3);
        txtSpaceFree.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        txtSpaceFree.Name = "txtSpaceFree";
        txtSpaceFree.Size = new System.Drawing.Size(54, 15);
        txtSpaceFree.TabIndex = 9;
        txtSpaceFree.Text = "0kT / 0kT";
        // 
        // resCostRad
        // 
        resCostRad.Amount = 0;
        resCostRad.BackColor = System.Drawing.Color.Black;
        resCostRad.Change = null;
        resCostRad.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
        resCostRad.Location = new System.Drawing.Point(91, 54);
        resCostRad.Margin = new System.Windows.Forms.Padding(0);
        resCostRad.Name = "resCostRad";
        resCostRad.ResourceName = "Radioactives";
        resCostRad.Size = new System.Drawing.Size(92, 17);
        resCostRad.TabIndex = 8;
        // 
        // resCostOrg
        // 
        resCostOrg.Amount = 0;
        resCostOrg.BackColor = System.Drawing.Color.Black;
        resCostOrg.Change = null;
        resCostOrg.ForeColor = System.Drawing.Color.FromArgb(0, 192, 0);
        resCostOrg.Location = new System.Drawing.Point(91, 37);
        resCostOrg.Margin = new System.Windows.Forms.Padding(0);
        resCostOrg.Name = "resCostOrg";
        resCostOrg.ResourceName = "Organics";
        resCostOrg.Size = new System.Drawing.Size(92, 17);
        resCostOrg.TabIndex = 7;
        // 
        // resCostMin
        // 
        resCostMin.Amount = 0;
        resCostMin.BackColor = System.Drawing.Color.Black;
        resCostMin.Change = null;
        resCostMin.ForeColor = System.Drawing.Color.FromArgb(128, 128, 255);
        resCostMin.Location = new System.Drawing.Point(91, 20);
        resCostMin.Margin = new System.Windows.Forms.Padding(0);
        resCostMin.Name = "resCostMin";
        resCostMin.ResourceName = "Minerals";
        resCostMin.Size = new System.Drawing.Size(92, 17);
        resCostMin.TabIndex = 6;
        // 
        // label10
        // 
        label10.AutoSize = true;
        label10.ForeColor = System.Drawing.Color.CornflowerBlue;
        label10.Location = new System.Drawing.Point(4, 20);
        label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label10.Name = "label10";
        label10.Size = new System.Drawing.Size(31, 15);
        label10.TabIndex = 2;
        label10.Text = "Cost";
        // 
        // label9
        // 
        label9.AutoSize = true;
        label9.ForeColor = System.Drawing.Color.CornflowerBlue;
        label9.Location = new System.Drawing.Point(4, 5);
        label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        label9.Name = "label9";
        label9.Size = new System.Drawing.Size(63, 15);
        label9.TabIndex = 1;
        label9.Text = "Space Free";
        // 
        // gamePanel4
        // 
        gamePanel4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        gamePanel4.BackColor = System.Drawing.Color.Black;
        gamePanel4.BorderColor = System.Drawing.Color.CornflowerBlue;
        gamePanel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        gamePanel4.Controls.Add(txtDetailDescription);
        gamePanel4.Controls.Add(resDetailRad);
        gamePanel4.Controls.Add(resDetailOrg);
        gamePanel4.Controls.Add(resDetailMin);
        gamePanel4.Controls.Add(txtDetailSize);
        gamePanel4.Controls.Add(txtDetailName);
        gamePanel4.Controls.Add(picDetailIcon);
        gamePanel4.ForeColor = System.Drawing.Color.White;
        gamePanel4.Location = new System.Drawing.Point(295, 627);
        gamePanel4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        gamePanel4.Name = "gamePanel4";
        gamePanel4.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
        gamePanel4.Size = new System.Drawing.Size(340, 93);
        gamePanel4.TabIndex = 19;
        // 
        // txtDetailDescription
        // 
        txtDetailDescription.Location = new System.Drawing.Point(4, 53);
        txtDetailDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        txtDetailDescription.Name = "txtDetailDescription";
        txtDetailDescription.Size = new System.Drawing.Size(230, 35);
        txtDetailDescription.TabIndex = 6;
        txtDetailDescription.Text = "label9";
        // 
        // resDetailRad
        // 
        resDetailRad.Amount = 0;
        resDetailRad.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        resDetailRad.BackColor = System.Drawing.Color.Black;
        resDetailRad.Change = null;
        resDetailRad.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
        resDetailRad.Location = new System.Drawing.Point(240, 70);
        resDetailRad.Margin = new System.Windows.Forms.Padding(0);
        resDetailRad.Name = "resDetailRad";
        resDetailRad.ResourceName = "Radioactives";
        resDetailRad.Size = new System.Drawing.Size(94, 17);
        resDetailRad.TabIndex = 5;
        // 
        // resDetailOrg
        // 
        resDetailOrg.Amount = 0;
        resDetailOrg.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        resDetailOrg.BackColor = System.Drawing.Color.Black;
        resDetailOrg.Change = null;
        resDetailOrg.ForeColor = System.Drawing.Color.FromArgb(0, 192, 0);
        resDetailOrg.Location = new System.Drawing.Point(240, 52);
        resDetailOrg.Margin = new System.Windows.Forms.Padding(0);
        resDetailOrg.Name = "resDetailOrg";
        resDetailOrg.ResourceName = "Organics";
        resDetailOrg.Size = new System.Drawing.Size(94, 17);
        resDetailOrg.TabIndex = 4;
        // 
        // resDetailMin
        // 
        resDetailMin.Amount = 0;
        resDetailMin.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        resDetailMin.BackColor = System.Drawing.Color.Black;
        resDetailMin.Change = null;
        resDetailMin.ForeColor = System.Drawing.Color.FromArgb(128, 128, 255);
        resDetailMin.Location = new System.Drawing.Point(240, 35);
        resDetailMin.Margin = new System.Windows.Forms.Padding(0);
        resDetailMin.Name = "resDetailMin";
        resDetailMin.ResourceName = "Minerals";
        resDetailMin.Size = new System.Drawing.Size(94, 17);
        resDetailMin.TabIndex = 3;
        // 
        // txtDetailSize
        // 
        txtDetailSize.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        txtDetailSize.Location = new System.Drawing.Point(278, 8);
        txtDetailSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        txtDetailSize.Name = "txtDetailSize";
        txtDetailSize.Size = new System.Drawing.Size(54, 15);
        txtDetailSize.TabIndex = 2;
        txtDetailSize.Text = "(no size)";
        txtDetailSize.TextAlign = System.Drawing.ContentAlignment.TopRight;
        // 
        // txtDetailName
        // 
        txtDetailName.AutoSize = true;
        txtDetailName.Location = new System.Drawing.Point(52, 8);
        txtDetailName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        txtDetailName.Name = "txtDetailName";
        txtDetailName.Size = new System.Drawing.Size(94, 15);
        txtDetailName.TabIndex = 1;
        txtDetailName.Text = "(no component)";
        // 
        // picDetailIcon
        // 
        picDetailIcon.BackColor = System.Drawing.Color.Black;
        picDetailIcon.ForeColor = System.Drawing.Color.White;
        picDetailIcon.Image = (System.Drawing.Image)resources.GetObject("picDetailIcon.Image");
        picDetailIcon.Location = new System.Drawing.Point(8, 8);
        picDetailIcon.Margin = new System.Windows.Forms.Padding(0);
        picDetailIcon.Name = "picDetailIcon";
        picDetailIcon.Size = new System.Drawing.Size(37, 37);
        picDetailIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        picDetailIcon.TabIndex = 0;
        picDetailIcon.TabStop = false;
        // 
        // gamePanel3
        // 
        gamePanel3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        gamePanel3.BackColor = System.Drawing.Color.Black;
        gamePanel3.BorderColor = System.Drawing.Color.CornflowerBlue;
        gamePanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        gamePanel3.Controls.Add(lstWarnings);
        gamePanel3.ForeColor = System.Drawing.Color.White;
        gamePanel3.Location = new System.Drawing.Point(14, 627);
        gamePanel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        gamePanel3.Name = "gamePanel3";
        gamePanel3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
        gamePanel3.Size = new System.Drawing.Size(273, 93);
        gamePanel3.TabIndex = 17;
        // 
        // lstWarnings
        // 
        lstWarnings.BackColor = System.Drawing.Color.Black;
        lstWarnings.BorderStyle = System.Windows.Forms.BorderStyle.None;
        lstWarnings.Dock = System.Windows.Forms.DockStyle.Fill;
        lstWarnings.ForeColor = System.Drawing.Color.Salmon;
        lstWarnings.Location = new System.Drawing.Point(4, 3);
        lstWarnings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        lstWarnings.Multiline = true;
        lstWarnings.Name = "lstWarnings";
        lstWarnings.PlaceholderText = "(No warnings)";
        lstWarnings.ReadOnly = true;
        lstWarnings.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        lstWarnings.Size = new System.Drawing.Size(263, 85);
        lstWarnings.TabIndex = 16;
        // 
        // gamePanel2
        // 
        gamePanel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        gamePanel2.BackColor = System.Drawing.Color.Black;
        gamePanel2.BorderColor = System.Drawing.Color.CornflowerBlue;
        gamePanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        gamePanel2.Controls.Add(lstComponentsInstalled);
        gamePanel2.ForeColor = System.Drawing.Color.White;
        gamePanel2.Location = new System.Drawing.Point(295, 179);
        gamePanel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        gamePanel2.Name = "gamePanel2";
        gamePanel2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
        gamePanel2.Size = new System.Drawing.Size(340, 424);
        gamePanel2.TabIndex = 9;
        // 
        // lstComponentsInstalled
        // 
        lstComponentsInstalled.BackColor = System.Drawing.Color.Black;
        lstComponentsInstalled.BorderStyle = System.Windows.Forms.BorderStyle.None;
        lstComponentsInstalled.Dock = System.Windows.Forms.DockStyle.Fill;
        lstComponentsInstalled.ForeColor = System.Drawing.Color.White;
        lstComponentsInstalled.HoverSelection = true;
        lstComponentsInstalled.Location = new System.Drawing.Point(4, 3);
        lstComponentsInstalled.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        lstComponentsInstalled.Name = "lstComponentsInstalled";
        lstComponentsInstalled.Size = new System.Drawing.Size(330, 416);
        lstComponentsInstalled.TabIndex = 0;
        lstComponentsInstalled.UseCompatibleStateImageBehavior = false;
        lstComponentsInstalled.View = System.Windows.Forms.View.Tile;
        lstComponentsInstalled.ItemMouseHover += lstComponentsInstalled_ItemMouseHover;
        lstComponentsInstalled.MouseDown += lstComponentsInstalled_MouseDown;
        // 
        // gamePanel1
        // 
        gamePanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        gamePanel1.BackColor = System.Drawing.Color.Black;
        gamePanel1.BorderColor = System.Drawing.Color.CornflowerBlue;
        gamePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        gamePanel1.Controls.Add(lstComponentsAvailable);
        gamePanel1.ForeColor = System.Drawing.Color.White;
        gamePanel1.Location = new System.Drawing.Point(14, 209);
        gamePanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        gamePanel1.Name = "gamePanel1";
        gamePanel1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
        gamePanel1.Size = new System.Drawing.Size(273, 394);
        gamePanel1.TabIndex = 7;
        // 
        // lstComponentsAvailable
        // 
        lstComponentsAvailable.BackColor = System.Drawing.Color.Black;
        lstComponentsAvailable.BorderStyle = System.Windows.Forms.BorderStyle.None;
        lstComponentsAvailable.Dock = System.Windows.Forms.DockStyle.Fill;
        lstComponentsAvailable.ForeColor = System.Drawing.Color.White;
        lstComponentsAvailable.HoverSelection = true;
        lstComponentsAvailable.Location = new System.Drawing.Point(4, 3);
        lstComponentsAvailable.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        lstComponentsAvailable.Name = "lstComponentsAvailable";
        lstComponentsAvailable.Size = new System.Drawing.Size(263, 386);
        lstComponentsAvailable.TabIndex = 0;
        lstComponentsAvailable.UseCompatibleStateImageBehavior = false;
        lstComponentsAvailable.View = System.Windows.Forms.View.Tile;
        lstComponentsAvailable.ItemMouseHover += lstComponentsAvailable_ItemMouseHover;
        lstComponentsAvailable.MouseDown += lstComponentsAvailable_MouseDown;
        // 
        // btnHull
        // 
        btnHull.BackColor = System.Drawing.Color.Black;
        btnHull.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnHull.Location = new System.Drawing.Point(93, 9);
        btnHull.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnHull.Name = "btnHull";
        btnHull.Size = new System.Drawing.Size(195, 27);
        btnHull.TabIndex = 2;
        btnHull.Text = "(choose)";
        btnHull.UseVisualStyleBackColor = false;
        btnHull.Click += btnHull_Click;
        // 
        // txtIteration
        // 
        txtIteration.AutoSize = true;
        txtIteration.Location = new System.Drawing.Point(260, 81);
        txtIteration.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        txtIteration.Name = "txtIteration";
        txtIteration.Size = new System.Drawing.Size(24, 15);
        txtIteration.TabIndex = 29;
        txtIteration.Text = "Iter";
        // 
        // picPortrait
        // 
        picPortrait.BackColor = System.Drawing.Color.Black;
        picPortrait.ForeColor = System.Drawing.Color.White;
        picPortrait.Image = (System.Drawing.Image)resources.GetObject("picPortrait.Image");
        picPortrait.Location = new System.Drawing.Point(643, 220);
        picPortrait.Margin = new System.Windows.Forms.Padding(0);
        picPortrait.Name = "picPortrait";
        picPortrait.Size = new System.Drawing.Size(170, 168);
        picPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        picPortrait.TabIndex = 30;
        picPortrait.TabStop = false;
        picPortrait.MouseDown += picPortrait_MouseDown;
        // 
        // btnMountInfo
        // 
        btnMountInfo.BackColor = System.Drawing.Color.Black;
        btnMountInfo.Enabled = false;
        btnMountInfo.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnMountInfo.Location = new System.Drawing.Point(264, 108);
        btnMountInfo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnMountInfo.Name = "btnMountInfo";
        btnMountInfo.Size = new System.Drawing.Size(23, 27);
        btnMountInfo.TabIndex = 26;
        btnMountInfo.Text = "?";
        btnMountInfo.UseVisualStyleBackColor = false;
        btnMountInfo.Click += btnMountInfo_Click;
        // 
        // ddlMount
        // 
        ddlMount.BackColor = System.Drawing.Color.Black;
        ddlMount.DisplayMember = "ShortName";
        ddlMount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        ddlMount.Enabled = false;
        ddlMount.ForeColor = System.Drawing.Color.White;
        ddlMount.FormattingEnabled = true;
        ddlMount.Location = new System.Drawing.Point(93, 111);
        ddlMount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        ddlMount.Name = "ddlMount";
        ddlMount.Size = new System.Drawing.Size(131, 23);
        ddlMount.TabIndex = 31;
        ddlMount.ValueMember = "Mount";
        ddlMount.SelectedIndexChanged += ddlMount_SelectedIndexChanged;
        // 
        // btnHelp
        // 
        btnHelp.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        btnHelp.BackColor = System.Drawing.Color.Black;
        btnHelp.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnHelp.Location = new System.Drawing.Point(643, 570);
        btnHelp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnHelp.Name = "btnHelp";
        btnHelp.Size = new System.Drawing.Size(170, 33);
        btnHelp.TabIndex = 32;
        btnHelp.Text = "Help";
        btnHelp.UseVisualStyleBackColor = false;
        btnHelp.Click += btnHelp_Click;
        // 
        // btnMount
        // 
        btnMount.BackColor = System.Drawing.Color.Black;
        btnMount.ForeColor = System.Drawing.Color.CornflowerBlue;
        btnMount.Location = new System.Drawing.Point(233, 108);
        btnMount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btnMount.Name = "btnMount";
        btnMount.Size = new System.Drawing.Size(23, 27);
        btnMount.TabIndex = 33;
        btnMount.Text = "...";
        btnMount.UseVisualStyleBackColor = false;
        btnMount.Click += btnMount_Click;
        // 
        // VehicleDesignForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.Black;
        ClientSize = new System.Drawing.Size(827, 734);
        Controls.Add(btnMount);
        Controls.Add(btnHelp);
        Controls.Add(ddlMount);
        Controls.Add(picPortrait);
        Controls.Add(txtIteration);
        Controls.Add(chkFilterByMount);
        Controls.Add(btnMountInfo);
        Controls.Add(btnWeaponsReport);
        Controls.Add(btnCancel);
        Controls.Add(btnSave);
        Controls.Add(pnlStats);
        Controls.Add(chkOnlyLatest);
        Controls.Add(label8);
        Controls.Add(gamePanel4);
        Controls.Add(label7);
        Controls.Add(gamePanel3);
        Controls.Add(label6);
        Controls.Add(label5);
        Controls.Add(gamePanel2);
        Controls.Add(label4);
        Controls.Add(gamePanel1);
        Controls.Add(ddlName);
        Controls.Add(label3);
        Controls.Add(ddlRole);
        Controls.Add(label2);
        Controls.Add(btnHull);
        Controls.Add(label1);
        Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        MaximumSize = new System.Drawing.Size(843, 11531);
        MinimumSize = new System.Drawing.Size(843, 563);
        Name = "VehicleDesignForm";
        ShowInTaskbar = false;
        Text = "Vehicle Designer";
        FormClosing += VehicleDesignForm_FormClosing;
        pnlStats.ResumeLayout(false);
        pnlStats.PerformLayout();
        gamePanel4.ResumeLayout(false);
        gamePanel4.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)picDetailIcon).EndInit();
        gamePanel3.ResumeLayout(false);
        gamePanel3.PerformLayout();
        gamePanel2.ResumeLayout(false);
        gamePanel1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)picPortrait).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Label label1;
	private Controls.GameButton btnHull;
	private System.Windows.Forms.Label label2;
	private System.Windows.Forms.ComboBox ddlRole;
	private System.Windows.Forms.Label label3;
	private System.Windows.Forms.ComboBox ddlName;
	private Controls.GamePanel gamePanel1;
	private System.Windows.Forms.ListView lstComponentsAvailable;
	private System.Windows.Forms.Label label4;
	private Controls.GamePanel gamePanel2;
	private System.Windows.Forms.ListView lstComponentsInstalled;
	private System.Windows.Forms.Label label5;
	private System.Windows.Forms.Label label6;
	private Controls.GamePanel gamePanel3;
	private System.Windows.Forms.TextBox lstWarnings;
	private System.Windows.Forms.Label label7;
	private Controls.GamePanel gamePanel4;
	private System.Windows.Forms.Label label8;
	private FrEee.UI.WinForms.Controls.GamePictureBox picDetailIcon;
	private System.Windows.Forms.Label txtDetailName;
	private System.Windows.Forms.Label txtDetailSize;
	private FrEee.UI.WinForms.Controls.ResourceDisplay resDetailRad;
	private FrEee.UI.WinForms.Controls.ResourceDisplay resDetailOrg;
	private FrEee.UI.WinForms.Controls.ResourceDisplay resDetailMin;
	private System.Windows.Forms.Label txtDetailDescription;
	private System.Windows.Forms.CheckBox chkOnlyLatest;
	private Controls.GamePanel pnlStats;
	private FrEee.UI.WinForms.Controls.ResourceDisplay resCostRad;
	private FrEee.UI.WinForms.Controls.ResourceDisplay resCostOrg;
	private FrEee.UI.WinForms.Controls.ResourceDisplay resCostMin;
	private System.Windows.Forms.Label label10;
	private System.Windows.Forms.Label label9;
	private System.Windows.Forms.Label label13;
	private System.Windows.Forms.Label label12;
	private System.Windows.Forms.Label txtSpaceFree;
	private System.Windows.Forms.Label txtCargo;
	private System.Windows.Forms.Label label15;
	private System.Windows.Forms.Label txtSupplyStorage;
	private System.Windows.Forms.Label label17;
	private System.Windows.Forms.Label txtSpeed;
	private System.Windows.Forms.Label txtHull;
	private System.Windows.Forms.Label txtArmor;
	private System.Windows.Forms.Label txtShields;
	private System.Windows.Forms.Label txtRange;
	private System.Windows.Forms.Label label22;
	private System.Windows.Forms.Label txtEvasion;
	private System.Windows.Forms.Label label18;
	private System.Windows.Forms.Label txtAccuracy;
	private System.Windows.Forms.Label label11;
	private Controls.GameButton btnSave;
	private Controls.GameButton btnCancel;
	private Controls.GameButton btnWeaponsReport;
	private System.Windows.Forms.CheckBox chkFilterByMount;
	private System.Windows.Forms.Label txtIteration;
	private FrEee.UI.WinForms.Controls.GamePictureBox picPortrait;
	private Controls.GameButton btnMountInfo;
	private System.Windows.Forms.ComboBox ddlMount;
	private Controls.GameButton btnHelp;
	private System.Windows.Forms.Label txtEngines;
	private System.Windows.Forms.Label label14;
	private Controls.GameButton btnMount;
    
}