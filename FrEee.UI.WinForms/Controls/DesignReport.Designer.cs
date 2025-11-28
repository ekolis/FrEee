using FrEee.UI.WinForms.Controls.Blazor;
using FrEee.UI.WinForms.Controls;

namespace FrEee.UI.WinForms.Controls;

partial class DesignReport
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
		System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("6x Ion Engine");
		System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("2x Phased Shield Generator");
		System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("3x Anti-Proton Beam");
		this.tabs = new FrEee.UI.WinForms.Controls.GameTabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.pnlStats = new FrEee.UI.WinForms.Controls.GamePanel();
		this.txtEvasion = new System.Windows.Forms.Label();
		this.label18 = new System.Windows.Forms.Label();
		this.txtAccuracy = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.txtHullHitpoints = new System.Windows.Forms.Label();
		this.txtArmorHitpoints = new System.Windows.Forms.Label();
		this.txtShieldsHitpoints = new System.Windows.Forms.Label();
		this.txtCargo = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.txtRange = new System.Windows.Forms.Label();
		this.label22 = new System.Windows.Forms.Label();
		this.txtSpeed = new System.Windows.Forms.Label();
		this.txtSupplyStorage = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.resCostRad = new FrEee.UI.WinForms.Controls.ResourceDisplay();
		this.resCostOrg = new FrEee.UI.WinForms.Controls.ResourceDisplay();
		this.resCostMin = new FrEee.UI.WinForms.Controls.ResourceDisplay();
		this.label10 = new System.Windows.Forms.Label();
		this.txtDate = new System.Windows.Forms.Label();
		this.lblDate = new System.Windows.Forms.Label();
		this.txtStrategy = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.picPortrait = new FrEee.UI.WinForms.Controls.GamePictureBox();
		this.txtRole = new System.Windows.Forms.Label();
		this.lblRole = new System.Windows.Forms.Label();
		this.txtHull = new System.Windows.Forms.Label();
		this.lblHull = new System.Windows.Forms.Label();
		this.txtVehicleType = new System.Windows.Forms.Label();
		this.txtName = new System.Windows.Forms.Label();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.lstComponents = new System.Windows.Forms.ListView();
		this.resMaintRad = new FrEee.UI.WinForms.Controls.ResourceDisplay();
		this.resMaintOrg = new FrEee.UI.WinForms.Controls.ResourceDisplay();
		this.resMaintMin = new FrEee.UI.WinForms.Controls.ResourceDisplay();
		this.tabs.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.pnlStats.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.picPortrait)).BeginInit();
		this.tabPage2.SuspendLayout();
		this.SuspendLayout();
		// 
		// tabs
		// 
		this.tabs.Controls.Add(this.tabPage1);
		this.tabs.Controls.Add(this.tabPage2);
		this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabs.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
		this.tabs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.tabs.Location = new System.Drawing.Point(0, 0);
		this.tabs.Name = "tabs";
		this.tabs.SelectedIndex = 0;
		this.tabs.SelectedTabBackColor = System.Drawing.Color.CornflowerBlue;
		this.tabs.SelectedTabForeColor = System.Drawing.Color.Black;
		this.tabs.Size = new System.Drawing.Size(322, 501);
		this.tabs.TabBackColor = System.Drawing.Color.Black;
		this.tabs.TabBorderColor = System.Drawing.Color.CornflowerBlue;
		this.tabs.TabForeColor = System.Drawing.Color.CornflowerBlue;
		this.tabs.TabIndex = 62;
		// 
		// tabPage1
		// 
		this.tabPage1.BackColor = System.Drawing.Color.Black;
		this.tabPage1.Controls.Add(this.pnlStats);
		this.tabPage1.Controls.Add(this.txtDate);
		this.tabPage1.Controls.Add(this.lblDate);
		this.tabPage1.Controls.Add(this.txtStrategy);
		this.tabPage1.Controls.Add(this.label2);
		this.tabPage1.Controls.Add(this.picPortrait);
		this.tabPage1.Controls.Add(this.txtRole);
		this.tabPage1.Controls.Add(this.lblRole);
		this.tabPage1.Controls.Add(this.txtHull);
		this.tabPage1.Controls.Add(this.lblHull);
		this.tabPage1.Controls.Add(this.txtVehicleType);
		this.tabPage1.Controls.Add(this.txtName);
		this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.tabPage1.Location = new System.Drawing.Point(4, 29);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(314, 468);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "Detail";
		// 
		// pnlStats
		// 
		this.pnlStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.pnlStats.BackColor = System.Drawing.Color.Black;
		this.pnlStats.BorderColor = System.Drawing.Color.CornflowerBlue;
		this.pnlStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pnlStats.Controls.Add(this.resMaintRad);
		this.pnlStats.Controls.Add(this.resMaintOrg);
		this.pnlStats.Controls.Add(this.resMaintMin);
		this.pnlStats.Controls.Add(this.txtEvasion);
		this.pnlStats.Controls.Add(this.label18);
		this.pnlStats.Controls.Add(this.txtAccuracy);
		this.pnlStats.Controls.Add(this.label11);
		this.pnlStats.Controls.Add(this.txtHullHitpoints);
		this.pnlStats.Controls.Add(this.txtArmorHitpoints);
		this.pnlStats.Controls.Add(this.txtShieldsHitpoints);
		this.pnlStats.Controls.Add(this.txtCargo);
		this.pnlStats.Controls.Add(this.label15);
		this.pnlStats.Controls.Add(this.label13);
		this.pnlStats.Controls.Add(this.txtRange);
		this.pnlStats.Controls.Add(this.label22);
		this.pnlStats.Controls.Add(this.txtSpeed);
		this.pnlStats.Controls.Add(this.txtSupplyStorage);
		this.pnlStats.Controls.Add(this.label17);
		this.pnlStats.Controls.Add(this.label12);
		this.pnlStats.Controls.Add(this.resCostRad);
		this.pnlStats.Controls.Add(this.resCostOrg);
		this.pnlStats.Controls.Add(this.resCostMin);
		this.pnlStats.Controls.Add(this.label10);
		this.pnlStats.ForeColor = System.Drawing.Color.White;
		this.pnlStats.Location = new System.Drawing.Point(6, 187);
		this.pnlStats.Name = "pnlStats";
		this.pnlStats.Padding = new System.Windows.Forms.Padding(3);
		this.pnlStats.Size = new System.Drawing.Size(305, 191);
		this.pnlStats.TabIndex = 71;
		// 
		// txtEvasion
		// 
		this.txtEvasion.AutoSize = true;
		this.txtEvasion.Location = new System.Drawing.Point(99, 151);
		this.txtEvasion.Name = "txtEvasion";
		this.txtEvasion.Size = new System.Drawing.Size(32, 15);
		this.txtEvasion.TabIndex = 38;
		this.txtEvasion.Text = "+0%";
		// 
		// label18
		// 
		this.label18.AutoSize = true;
		this.label18.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label18.Location = new System.Drawing.Point(5, 150);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(50, 15);
		this.label18.TabIndex = 37;
		this.label18.Text = "Evasion";
		// 
		// txtAccuracy
		// 
		this.txtAccuracy.AutoSize = true;
		this.txtAccuracy.Location = new System.Drawing.Point(99, 135);
		this.txtAccuracy.Name = "txtAccuracy";
		this.txtAccuracy.Size = new System.Drawing.Size(32, 15);
		this.txtAccuracy.TabIndex = 36;
		this.txtAccuracy.Text = "+0%";
		// 
		// label11
		// 
		this.label11.AutoSize = true;
		this.label11.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label11.Location = new System.Drawing.Point(5, 135);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(55, 15);
		this.label11.TabIndex = 35;
		this.label11.Text = "Accuracy";
		// 
		// txtHullHitpoints
		// 
		this.txtHullHitpoints.AutoSize = true;
		this.txtHullHitpoints.Location = new System.Drawing.Point(99, 118);
		this.txtHullHitpoints.Name = "txtHullHitpoints";
		this.txtHullHitpoints.Size = new System.Drawing.Size(37, 15);
		this.txtHullHitpoints.TabIndex = 34;
		this.txtHullHitpoints.Text = "0 hull";
		// 
		// txtArmorHitpoints
		// 
		this.txtArmorHitpoints.AutoSize = true;
		this.txtArmorHitpoints.Location = new System.Drawing.Point(99, 103);
		this.txtArmorHitpoints.Name = "txtArmorHitpoints";
		this.txtArmorHitpoints.Size = new System.Drawing.Size(50, 15);
		this.txtArmorHitpoints.TabIndex = 33;
		this.txtArmorHitpoints.Text = "0 armor";
		// 
		// txtShieldsHitpoints
		// 
		this.txtShieldsHitpoints.AutoSize = true;
		this.txtShieldsHitpoints.Location = new System.Drawing.Point(99, 90);
		this.txtShieldsHitpoints.Name = "txtShieldsHitpoints";
		this.txtShieldsHitpoints.Size = new System.Drawing.Size(116, 15);
		this.txtShieldsHitpoints.TabIndex = 32;
		this.txtShieldsHitpoints.Text = "0 shields (+0 regen)";
		// 
		// txtCargo
		// 
		this.txtCargo.AutoSize = true;
		this.txtCargo.Location = new System.Drawing.Point(100, 164);
		this.txtCargo.Name = "txtCargo";
		this.txtCargo.Size = new System.Drawing.Size(27, 15);
		this.txtCargo.TabIndex = 31;
		this.txtCargo.Text = "0kT";
		// 
		// label15
		// 
		this.label15.AutoSize = true;
		this.label15.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label15.Location = new System.Drawing.Point(5, 164);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(86, 15);
		this.label15.TabIndex = 30;
		this.label15.Text = "Cargo Storage";
		// 
		// label13
		// 
		this.label13.AutoSize = true;
		this.label13.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label13.Location = new System.Drawing.Point(5, 90);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(58, 15);
		this.label13.TabIndex = 29;
		this.label13.Text = "Durability";
		// 
		// txtRange
		// 
		this.txtRange.AutoSize = true;
		this.txtRange.Location = new System.Drawing.Point(99, 75);
		this.txtRange.Name = "txtRange";
		this.txtRange.Size = new System.Drawing.Size(56, 15);
		this.txtRange.TabIndex = 22;
		this.txtRange.Text = "0 sectors";
		// 
		// label22
		// 
		this.label22.AutoSize = true;
		this.label22.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label22.Location = new System.Drawing.Point(5, 75);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(44, 15);
		this.label22.TabIndex = 21;
		this.label22.Text = "Range";
		// 
		// txtSpeed
		// 
		this.txtSpeed.AutoSize = true;
		this.txtSpeed.Location = new System.Drawing.Point(100, 48);
		this.txtSpeed.Name = "txtSpeed";
		this.txtSpeed.Size = new System.Drawing.Size(80, 15);
		this.txtSpeed.TabIndex = 17;
		this.txtSpeed.Text = "0 sectors/turn";
		// 
		// txtSupplyStorage
		// 
		this.txtSupplyStorage.AutoSize = true;
		this.txtSupplyStorage.Location = new System.Drawing.Point(100, 61);
		this.txtSupplyStorage.Name = "txtSupplyStorage";
		this.txtSupplyStorage.Size = new System.Drawing.Size(14, 15);
		this.txtSupplyStorage.TabIndex = 16;
		this.txtSupplyStorage.Text = "0";
		// 
		// label17
		// 
		this.label17.AutoSize = true;
		this.label17.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label17.Location = new System.Drawing.Point(6, 60);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(90, 15);
		this.label17.TabIndex = 15;
		this.label17.Text = "Supply Storage";
		// 
		// label12
		// 
		this.label12.AutoSize = true;
		this.label12.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label12.Location = new System.Drawing.Point(6, 47);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(43, 15);
		this.label12.TabIndex = 10;
		this.label12.Text = "Speed";
		// 
		// resCostRad
		// 
		this.resCostRad.Amount = 0;
		this.resCostRad.BackColor = System.Drawing.Color.Black;
		this.resCostRad.Change = null;
		this.resCostRad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
		this.resCostRad.Location = new System.Drawing.Point(100, 33);
		this.resCostRad.Margin = new System.Windows.Forms.Padding(0);
		this.resCostRad.Name = "resCostRad";
		this.resCostRad.ResourceName = "Radioactives";
		this.resCostRad.Size = new System.Drawing.Size(79, 15);
		this.resCostRad.TabIndex = 8;
		// 
		// resCostOrg
		// 
		this.resCostOrg.Amount = 0;
		this.resCostOrg.BackColor = System.Drawing.Color.Black;
		this.resCostOrg.Change = null;
		this.resCostOrg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
		this.resCostOrg.Location = new System.Drawing.Point(100, 18);
		this.resCostOrg.Margin = new System.Windows.Forms.Padding(0);
		this.resCostOrg.Name = "resCostOrg";
		this.resCostOrg.ResourceName = "Organics";
		this.resCostOrg.Size = new System.Drawing.Size(79, 15);
		this.resCostOrg.TabIndex = 7;
		// 
		// resCostMin
		// 
		this.resCostMin.Amount = 0;
		this.resCostMin.BackColor = System.Drawing.Color.Black;
		this.resCostMin.Change = null;
		this.resCostMin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
		this.resCostMin.Location = new System.Drawing.Point(100, 3);
		this.resCostMin.Margin = new System.Windows.Forms.Padding(0);
		this.resCostMin.Name = "resCostMin";
		this.resCostMin.ResourceName = "Minerals";
		this.resCostMin.Size = new System.Drawing.Size(79, 15);
		this.resCostMin.TabIndex = 6;
		// 
		// label10
		// 
		this.label10.AutoSize = true;
		this.label10.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label10.Location = new System.Drawing.Point(6, 3);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(65, 15);
		this.label10.TabIndex = 2;
		this.label10.Text = "Cost/Maint";
		// 
		// txtDate
		// 
		this.txtDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtDate.AutoSize = true;
		this.txtDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtDate.Location = new System.Drawing.Point(163, 160);
		this.txtDate.Name = "txtDate";
		this.txtDate.Size = new System.Drawing.Size(45, 15);
		this.txtDate.TabIndex = 70;
		this.txtDate.Text = "2403.6";
		// 
		// lblDate
		// 
		this.lblDate.AutoSize = true;
		this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblDate.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblDate.Location = new System.Drawing.Point(148, 145);
		this.lblDate.Name = "lblDate";
		this.lblDate.Size = new System.Drawing.Size(33, 15);
		this.lblDate.TabIndex = 69;
		this.lblDate.Text = "Date";
		// 
		// txtStrategy
		// 
		this.txtStrategy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtStrategy.AutoSize = true;
		this.txtStrategy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtStrategy.Location = new System.Drawing.Point(163, 130);
		this.txtStrategy.Name = "txtStrategy";
		this.txtStrategy.Size = new System.Drawing.Size(90, 15);
		this.txtStrategy.TabIndex = 68;
		this.txtStrategy.Text = "Optimal Range";
		// 
		// label2
		// 
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label2.Location = new System.Drawing.Point(148, 115);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(51, 15);
		this.label2.TabIndex = 67;
		this.label2.Text = "Strategy";
		// 
		// picPortrait
		// 
		this.picPortrait.Location = new System.Drawing.Point(3, 29);
		this.picPortrait.Name = "picPortrait";
		this.picPortrait.Size = new System.Drawing.Size(128, 128);
		this.picPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.picPortrait.TabIndex = 66;
		this.picPortrait.TabStop = false;
		this.picPortrait.Click += new System.EventHandler(this.picPortrait_Click);
		// 
		// txtRole
		// 
		this.txtRole.AutoSize = true;
		this.txtRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtRole.Location = new System.Drawing.Point(163, 100);
		this.txtRole.Name = "txtRole";
		this.txtRole.Size = new System.Drawing.Size(67, 15);
		this.txtRole.TabIndex = 65;
		this.txtRole.Text = "Attack Ship";
		// 
		// lblRole
		// 
		this.lblRole.AutoSize = true;
		this.lblRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblRole.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblRole.Location = new System.Drawing.Point(148, 85);
		this.lblRole.Name = "lblRole";
		this.lblRole.Size = new System.Drawing.Size(33, 15);
		this.lblRole.TabIndex = 64;
		this.lblRole.Text = "Role";
		// 
		// txtHull
		// 
		this.txtHull.AutoSize = true;
		this.txtHull.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtHull.Location = new System.Drawing.Point(163, 70);
		this.txtHull.Name = "txtHull";
		this.txtHull.Size = new System.Drawing.Size(91, 15);
		this.txtHull.TabIndex = 63;
		this.txtHull.Text = "Cruiser (500kT)";
		// 
		// lblHull
		// 
		this.lblHull.AutoSize = true;
		this.lblHull.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblHull.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblHull.Location = new System.Drawing.Point(148, 55);
		this.lblHull.Name = "lblHull";
		this.lblHull.Size = new System.Drawing.Size(29, 15);
		this.lblHull.TabIndex = 62;
		this.lblHull.Text = "Hull";
		// 
		// txtVehicleType
		// 
		this.txtVehicleType.AutoSize = true;
		this.txtVehicleType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtVehicleType.Location = new System.Drawing.Point(163, 23);
		this.txtVehicleType.Name = "txtVehicleType";
		this.txtVehicleType.Size = new System.Drawing.Size(32, 15);
		this.txtVehicleType.TabIndex = 61;
		this.txtVehicleType.Text = "Ship";
		// 
		// txtName
		// 
		this.txtName.AutoSize = true;
		this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
		this.txtName.Location = new System.Drawing.Point(147, 3);
		this.txtName.Name = "txtName";
		this.txtName.Size = new System.Drawing.Size(68, 20);
		this.txtName.TabIndex = 60;
		this.txtName.Text = "Avenger";
		// 
		// tabPage2
		// 
		this.tabPage2.BackColor = System.Drawing.Color.Black;
		this.tabPage2.Controls.Add(this.lstComponents);
		this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.tabPage2.Location = new System.Drawing.Point(4, 29);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(314, 468);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "Comps";
		// 
		// lstComponents
		// 
		this.lstComponents.BackColor = System.Drawing.Color.Black;
		this.lstComponents.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.lstComponents.Dock = System.Windows.Forms.DockStyle.Fill;
		this.lstComponents.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lstComponents.ForeColor = System.Drawing.Color.White;
		this.lstComponents.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
		this.lstComponents.Location = new System.Drawing.Point(3, 3);
		this.lstComponents.Name = "lstComponents";
		this.lstComponents.Size = new System.Drawing.Size(308, 462);
		this.lstComponents.TabIndex = 24;
		this.lstComponents.UseCompatibleStateImageBehavior = false;
		this.lstComponents.View = System.Windows.Forms.View.Tile;
		this.lstComponents.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstComponents_MouseDown);
		// 
		// resMaintRad
		// 
		this.resMaintRad.Amount = 0;
		this.resMaintRad.BackColor = System.Drawing.Color.Black;
		this.resMaintRad.Change = null;
		this.resMaintRad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
		this.resMaintRad.Location = new System.Drawing.Point(192, 38);
		this.resMaintRad.Margin = new System.Windows.Forms.Padding(0);
		this.resMaintRad.Name = "resMaintRad";
		this.resMaintRad.ResourceName = "Radioactives";
		this.resMaintRad.Size = new System.Drawing.Size(92, 17);
		this.resMaintRad.TabIndex = 41;
		// 
		// resMaintOrg
		// 
		this.resMaintOrg.Amount = 0;
		this.resMaintOrg.BackColor = System.Drawing.Color.Black;
		this.resMaintOrg.Change = null;
		this.resMaintOrg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
		this.resMaintOrg.Location = new System.Drawing.Point(192, 21);
		this.resMaintOrg.Margin = new System.Windows.Forms.Padding(0);
		this.resMaintOrg.Name = "resMaintOrg";
		this.resMaintOrg.ResourceName = "Organics";
		this.resMaintOrg.Size = new System.Drawing.Size(92, 17);
		this.resMaintOrg.TabIndex = 40;
		// 
		// resMaintMin
		// 
		this.resMaintMin.Amount = 0;
		this.resMaintMin.BackColor = System.Drawing.Color.Black;
		this.resMaintMin.Change = null;
		this.resMaintMin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
		this.resMaintMin.Location = new System.Drawing.Point(192, 3);
		this.resMaintMin.Margin = new System.Windows.Forms.Padding(0);
		this.resMaintMin.Name = "resMaintMin";
		this.resMaintMin.ResourceName = "Minerals";
		this.resMaintMin.Size = new System.Drawing.Size(92, 17);
		this.resMaintMin.TabIndex = 39;
		// 
		// DesignReport
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.Controls.Add(this.tabs);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "DesignReport";
		this.Size = new System.Drawing.Size(322, 501);
		this.tabs.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage1.PerformLayout();
		this.pnlStats.ResumeLayout(false);
		this.pnlStats.PerformLayout();
		((System.ComponentModel.ISupportInitialize)(this.picPortrait)).EndInit();
		this.tabPage2.ResumeLayout(false);
		this.ResumeLayout(false);

	}

	#endregion

	private GameTabControl tabs;
	private System.Windows.Forms.TabPage tabPage1;
	private System.Windows.Forms.Label txtDate;
	private System.Windows.Forms.Label lblDate;
	private System.Windows.Forms.Label txtStrategy;
	private System.Windows.Forms.Label label2;
	private GamePictureBox picPortrait;
	private System.Windows.Forms.Label txtRole;
	private System.Windows.Forms.Label lblRole;
	private System.Windows.Forms.Label txtHull;
	private System.Windows.Forms.Label lblHull;
	private System.Windows.Forms.Label txtVehicleType;
	private System.Windows.Forms.Label txtName;
	private System.Windows.Forms.TabPage tabPage2;
	private System.Windows.Forms.ListView lstComponents;
	private GamePanel pnlStats;
	private System.Windows.Forms.Label txtEvasion;
	private System.Windows.Forms.Label label18;
	private System.Windows.Forms.Label txtAccuracy;
	private System.Windows.Forms.Label label11;
	private System.Windows.Forms.Label txtHullHitpoints;
	private System.Windows.Forms.Label txtArmorHitpoints;
	private System.Windows.Forms.Label txtShieldsHitpoints;
	private System.Windows.Forms.Label txtCargo;
	private System.Windows.Forms.Label label15;
	private System.Windows.Forms.Label label13;
	private System.Windows.Forms.Label txtRange;
	private System.Windows.Forms.Label label22;
	private System.Windows.Forms.Label txtSpeed;
	private System.Windows.Forms.Label txtSupplyStorage;
	private System.Windows.Forms.Label label17;
	private System.Windows.Forms.Label label12;
	private ResourceDisplay resCostRad;
	private ResourceDisplay resCostOrg;
	private ResourceDisplay resCostMin;
	private System.Windows.Forms.Label label10;
	private ResourceDisplay resMaintRad;
	private ResourceDisplay resMaintOrg;
	private ResourceDisplay resMaintMin;
}
