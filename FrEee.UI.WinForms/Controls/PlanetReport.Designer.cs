using FrEee.UI.WinForms.Controls.Blazor;
using FrEee.UI.WinForms.Controls;

namespace FrEee.UI.WinForms.Controls;

partial class PlanetReport
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
		System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("1x Space Yard");
		System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("6x Mineral Miner");
		System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("3x Organics Farm");
		System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("2x Radioactives Extraction");
		System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("2000M Jraenar (Happy: 150)");
		System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("1000M Eee (Jubilant: 0)");
		System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("10x \"Buster\" class Weapon Platform");
		System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("500x \"Guard\" class Troop");
		this.gameTabControl1 = new FrEee.UI.WinForms.Controls.GameTabControl();
		this.pageDetail = new System.Windows.Forms.TabPage();
		this.txtAge = new System.Windows.Forms.Label();
		this.picPortrait = new FrEee.UI.WinForms.Controls.GamePictureBox();
		this.pnlColony = new FrEee.UI.WinForms.Controls.GamePanel();
		this.txtConstructionTime = new System.Windows.Forms.Label();
		this.lblConstructionTime = new System.Windows.Forms.Label();
		this.txtConstructionItem = new System.Windows.Forms.Label();
		this.lblConstructionItem = new System.Windows.Forms.Label();
		this.resIntel = new FrEee.UI.WinForms.Controls.ResourceDisplay();
		this.resResearch = new FrEee.UI.WinForms.Controls.ResourceDisplay();
		this.label1 = new System.Windows.Forms.Label();
		this.txtMood = new System.Windows.Forms.Label();
		this.lblMood = new System.Windows.Forms.Label();
		this.txtPopulation = new System.Windows.Forms.Label();
		this.lblPopulation = new System.Windows.Forms.Label();
		this.txtColonyType = new System.Windows.Forms.Label();
		this.lblColonyType = new System.Windows.Forms.Label();
		this.resIncomeMinerals = new FrEee.UI.WinForms.Controls.ResourceDisplay();
		this.resIncomeOrganics = new FrEee.UI.WinForms.Controls.ResourceDisplay();
		this.resIncomeRadioactives = new FrEee.UI.WinForms.Controls.ResourceDisplay();
		this.lblIncome = new System.Windows.Forms.Label();
		this.txtDescription = new System.Windows.Forms.Label();
		this.txtValueRadioactives = new System.Windows.Forms.Label();
		this.txtValueOrganics = new System.Windows.Forms.Label();
		this.txtValueMinerals = new System.Windows.Forms.Label();
		this.lblValue = new System.Windows.Forms.Label();
		this.txtConditions = new System.Windows.Forms.Label();
		this.lblConditions = new System.Windows.Forms.Label();
		this.txtAtmosphere = new System.Windows.Forms.Label();
		this.lblAtmosphere = new System.Windows.Forms.Label();
		this.txtSizeSurface = new System.Windows.Forms.Label();
		this.txtName = new System.Windows.Forms.Label();
		this.picOwnerFlag = new System.Windows.Forms.PictureBox();
		this.pageFacil = new System.Windows.Forms.TabPage();
		this.txtFacilitySlotsFree = new System.Windows.Forms.Label();
		this.lstFacilitiesDetail = new System.Windows.Forms.ListView();
		this.pageRaces = new System.Windows.Forms.TabPage();
		this.lblPopulationSpaceFree = new System.Windows.Forms.Label();
		this.lstRaces = new System.Windows.Forms.ListView();
		this.pageCargo = new System.Windows.Forms.TabPage();
		this.txtCargoSpaceFree = new System.Windows.Forms.Label();
		this.lstCargoDetail = new System.Windows.Forms.ListView();
		this.pageOrders = new System.Windows.Forms.TabPage();
		this.chkOnHold = new System.Windows.Forms.CheckBox();
		this.chkRepeat = new System.Windows.Forms.CheckBox();
		this.btnOrdersClear = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnOrderDelete = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnOrderDown = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnOrderUp = new FrEee.UI.WinForms.Controls.GameButton();
		this.lstOrdersDetail = new System.Windows.Forms.ListBox();
		this.pageAbility = new System.Windows.Forms.TabPage();
		this.abilityTreeView = new FrEee.UI.WinForms.Controls.AbilityTreeView();
		this.gameTabControl1.SuspendLayout();
		this.pageDetail.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.picPortrait)).BeginInit();
		this.pnlColony.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.picOwnerFlag)).BeginInit();
		this.pageFacil.SuspendLayout();
		this.pageRaces.SuspendLayout();
		this.pageCargo.SuspendLayout();
		this.pageOrders.SuspendLayout();
		this.pageAbility.SuspendLayout();
		this.SuspendLayout();
		// 
		// gameTabControl1
		// 
		this.gameTabControl1.Controls.Add(this.pageDetail);
		this.gameTabControl1.Controls.Add(this.pageFacil);
		this.gameTabControl1.Controls.Add(this.pageRaces);
		this.gameTabControl1.Controls.Add(this.pageCargo);
		this.gameTabControl1.Controls.Add(this.pageOrders);
		this.gameTabControl1.Controls.Add(this.pageAbility);
		this.gameTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gameTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
		this.gameTabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.gameTabControl1.Location = new System.Drawing.Point(0, 0);
		this.gameTabControl1.Name = "gameTabControl1";
		this.gameTabControl1.SelectedIndex = 0;
		this.gameTabControl1.SelectedTabBackColor = System.Drawing.Color.SkyBlue;
		this.gameTabControl1.SelectedTabForeColor = System.Drawing.Color.Black;
		this.gameTabControl1.Size = new System.Drawing.Size(375, 459);
		this.gameTabControl1.TabBackColor = System.Drawing.Color.Black;
		this.gameTabControl1.TabBorderColor = System.Drawing.Color.CornflowerBlue;
		this.gameTabControl1.TabForeColor = System.Drawing.Color.CornflowerBlue;
		this.gameTabControl1.TabIndex = 1;
		// 
		// pageDetail
		// 
		this.pageDetail.AutoScroll = true;
		this.pageDetail.BackColor = System.Drawing.Color.Black;
		this.pageDetail.Controls.Add(this.txtAge);
		this.pageDetail.Controls.Add(this.picPortrait);
		this.pageDetail.Controls.Add(this.pnlColony);
		this.pageDetail.Controls.Add(this.txtDescription);
		this.pageDetail.Controls.Add(this.txtValueRadioactives);
		this.pageDetail.Controls.Add(this.txtValueOrganics);
		this.pageDetail.Controls.Add(this.txtValueMinerals);
		this.pageDetail.Controls.Add(this.lblValue);
		this.pageDetail.Controls.Add(this.txtConditions);
		this.pageDetail.Controls.Add(this.lblConditions);
		this.pageDetail.Controls.Add(this.txtAtmosphere);
		this.pageDetail.Controls.Add(this.lblAtmosphere);
		this.pageDetail.Controls.Add(this.txtSizeSurface);
		this.pageDetail.Controls.Add(this.txtName);
		this.pageDetail.Controls.Add(this.picOwnerFlag);
		this.pageDetail.Location = new System.Drawing.Point(4, 29);
		this.pageDetail.Name = "pageDetail";
		this.pageDetail.Padding = new System.Windows.Forms.Padding(3);
		this.pageDetail.Size = new System.Drawing.Size(367, 426);
		this.pageDetail.TabIndex = 0;
		this.pageDetail.Text = "Detail";
		// 
		// txtAge
		// 
		this.txtAge.AutoSize = true;
		this.txtAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtAge.Location = new System.Drawing.Point(162, 41);
		this.txtAge.Name = "txtAge";
		this.txtAge.Size = new System.Drawing.Size(47, 15);
		this.txtAge.TabIndex = 56;
		this.txtAge.Text = "Current";
		// 
		// picPortrait
		// 
		this.picPortrait.Location = new System.Drawing.Point(7, 33);
		this.picPortrait.Name = "picPortrait";
		this.picPortrait.Size = new System.Drawing.Size(128, 128);
		this.picPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.picPortrait.TabIndex = 67;
		this.picPortrait.TabStop = false;
		this.picPortrait.Click += new System.EventHandler(this.picPortrait_Click);
		// 
		// pnlColony
		// 
		this.pnlColony.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.pnlColony.BackColor = System.Drawing.Color.Black;
		this.pnlColony.BorderColor = System.Drawing.Color.CornflowerBlue;
		this.pnlColony.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pnlColony.Controls.Add(this.txtConstructionTime);
		this.pnlColony.Controls.Add(this.lblConstructionTime);
		this.pnlColony.Controls.Add(this.txtConstructionItem);
		this.pnlColony.Controls.Add(this.lblConstructionItem);
		this.pnlColony.Controls.Add(this.resIntel);
		this.pnlColony.Controls.Add(this.resResearch);
		this.pnlColony.Controls.Add(this.label1);
		this.pnlColony.Controls.Add(this.txtMood);
		this.pnlColony.Controls.Add(this.lblMood);
		this.pnlColony.Controls.Add(this.txtPopulation);
		this.pnlColony.Controls.Add(this.lblPopulation);
		this.pnlColony.Controls.Add(this.txtColonyType);
		this.pnlColony.Controls.Add(this.lblColonyType);
		this.pnlColony.Controls.Add(this.resIncomeMinerals);
		this.pnlColony.Controls.Add(this.resIncomeOrganics);
		this.pnlColony.Controls.Add(this.resIncomeRadioactives);
		this.pnlColony.Controls.Add(this.lblIncome);
		this.pnlColony.ForeColor = System.Drawing.Color.White;
		this.pnlColony.Location = new System.Drawing.Point(0, 267);
		this.pnlColony.Name = "pnlColony";
		this.pnlColony.Padding = new System.Windows.Forms.Padding(3);
		this.pnlColony.Size = new System.Drawing.Size(367, 153);
		this.pnlColony.TabIndex = 66;
		// 
		// txtConstructionTime
		// 
		this.txtConstructionTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.txtConstructionTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtConstructionTime.Location = new System.Drawing.Point(199, 123);
		this.txtConstructionTime.Name = "txtConstructionTime";
		this.txtConstructionTime.Size = new System.Drawing.Size(153, 15);
		this.txtConstructionTime.TabIndex = 84;
		this.txtConstructionTime.Text = "0.3 years";
		this.txtConstructionTime.TextAlign = System.Drawing.ContentAlignment.TopRight;
		// 
		// lblConstructionTime
		// 
		this.lblConstructionTime.AutoSize = true;
		this.lblConstructionTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblConstructionTime.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblConstructionTime.Location = new System.Drawing.Point(5, 123);
		this.lblConstructionTime.Name = "lblConstructionTime";
		this.lblConstructionTime.Size = new System.Drawing.Size(99, 15);
		this.lblConstructionTime.TabIndex = 83;
		this.lblConstructionTime.Text = "Time Remaining";
		// 
		// txtConstructionItem
		// 
		this.txtConstructionItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.txtConstructionItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtConstructionItem.Location = new System.Drawing.Point(199, 108);
		this.txtConstructionItem.Name = "txtConstructionItem";
		this.txtConstructionItem.Size = new System.Drawing.Size(153, 15);
		this.txtConstructionItem.TabIndex = 82;
		this.txtConstructionItem.Text = "Barracuda IV";
		this.txtConstructionItem.TextAlign = System.Drawing.ContentAlignment.TopRight;
		// 
		// lblConstructionItem
		// 
		this.lblConstructionItem.AutoSize = true;
		this.lblConstructionItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblConstructionItem.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblConstructionItem.Location = new System.Drawing.Point(4, 108);
		this.lblConstructionItem.Name = "lblConstructionItem";
		this.lblConstructionItem.Size = new System.Drawing.Size(112, 15);
		this.lblConstructionItem.TabIndex = 81;
		this.lblConstructionItem.Text = "Under Construction";
		// 
		// resIntel
		// 
		this.resIntel.Amount = 1000;
		this.resIntel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.resIntel.BackColor = System.Drawing.Color.Black;
		this.resIntel.Change = null;
		this.resIntel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.resIntel.ForeColor = System.Drawing.Color.White;
		this.resIntel.Location = new System.Drawing.Point(202, 77);
		this.resIntel.Margin = new System.Windows.Forms.Padding(0);
		this.resIntel.Name = "resIntel";
		this.resIntel.ResourceName = "Intelligence";
		this.resIntel.Size = new System.Drawing.Size(86, 20);
		this.resIntel.TabIndex = 80;
		// 
		// resResearch
		// 
		this.resResearch.Amount = 25000;
		this.resResearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.resResearch.BackColor = System.Drawing.Color.Black;
		this.resResearch.Change = null;
		this.resResearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.resResearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
		this.resResearch.Location = new System.Drawing.Point(119, 77);
		this.resResearch.Margin = new System.Windows.Forms.Padding(0);
		this.resResearch.Name = "resResearch";
		this.resResearch.ResourceName = "Research";
		this.resResearch.Size = new System.Drawing.Size(81, 20);
		this.resResearch.TabIndex = 79;
		// 
		// label1
		// 
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label1.Location = new System.Drawing.Point(6, 82);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(55, 15);
		this.label1.TabIndex = 78;
		this.label1.Text = "Res/Intel";
		// 
		// txtMood
		// 
		this.txtMood.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.txtMood.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtMood.Location = new System.Drawing.Point(199, 33);
		this.txtMood.Name = "txtMood";
		this.txtMood.Size = new System.Drawing.Size(153, 15);
		this.txtMood.TabIndex = 77;
		this.txtMood.Text = "Happy";
		this.txtMood.TextAlign = System.Drawing.ContentAlignment.TopRight;
		// 
		// lblMood
		// 
		this.lblMood.AutoSize = true;
		this.lblMood.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblMood.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblMood.Location = new System.Drawing.Point(6, 33);
		this.lblMood.Name = "lblMood";
		this.lblMood.Size = new System.Drawing.Size(39, 15);
		this.lblMood.TabIndex = 76;
		this.lblMood.Text = "Mood";
		// 
		// txtPopulation
		// 
		this.txtPopulation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.txtPopulation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtPopulation.Location = new System.Drawing.Point(133, 18);
		this.txtPopulation.Name = "txtPopulation";
		this.txtPopulation.Size = new System.Drawing.Size(219, 15);
		this.txtPopulation.TabIndex = 73;
		this.txtPopulation.Text = "2400M / 4000M";
		this.txtPopulation.TextAlign = System.Drawing.ContentAlignment.TopRight;
		// 
		// lblPopulation
		// 
		this.lblPopulation.AutoSize = true;
		this.lblPopulation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblPopulation.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblPopulation.Location = new System.Drawing.Point(6, 18);
		this.lblPopulation.Name = "lblPopulation";
		this.lblPopulation.Size = new System.Drawing.Size(66, 15);
		this.lblPopulation.TabIndex = 72;
		this.lblPopulation.Text = "Population";
		// 
		// txtColonyType
		// 
		this.txtColonyType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.txtColonyType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtColonyType.Location = new System.Drawing.Point(133, 3);
		this.txtColonyType.Name = "txtColonyType";
		this.txtColonyType.Size = new System.Drawing.Size(219, 15);
		this.txtColonyType.TabIndex = 71;
		this.txtColonyType.Text = "Homeworld";
		this.txtColonyType.TextAlign = System.Drawing.ContentAlignment.TopRight;
		// 
		// lblColonyType
		// 
		this.lblColonyType.AutoSize = true;
		this.lblColonyType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblColonyType.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblColonyType.Location = new System.Drawing.Point(6, 3);
		this.lblColonyType.Name = "lblColonyType";
		this.lblColonyType.Size = new System.Drawing.Size(73, 15);
		this.lblColonyType.TabIndex = 70;
		this.lblColonyType.Text = "Colony Type";
		// 
		// resIncomeMinerals
		// 
		this.resIncomeMinerals.Amount = 25000;
		this.resIncomeMinerals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.resIncomeMinerals.BackColor = System.Drawing.Color.Black;
		this.resIncomeMinerals.Change = null;
		this.resIncomeMinerals.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.resIncomeMinerals.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
		this.resIncomeMinerals.Location = new System.Drawing.Point(119, 58);
		this.resIncomeMinerals.Margin = new System.Windows.Forms.Padding(0);
		this.resIncomeMinerals.Name = "resIncomeMinerals";
		this.resIncomeMinerals.ResourceName = "Minerals";
		this.resIncomeMinerals.Size = new System.Drawing.Size(81, 20);
		this.resIncomeMinerals.TabIndex = 69;
		// 
		// resIncomeOrganics
		// 
		this.resIncomeOrganics.Amount = 10000;
		this.resIncomeOrganics.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.resIncomeOrganics.BackColor = System.Drawing.Color.Black;
		this.resIncomeOrganics.Change = null;
		this.resIncomeOrganics.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.resIncomeOrganics.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
		this.resIncomeOrganics.Location = new System.Drawing.Point(200, 57);
		this.resIncomeOrganics.Margin = new System.Windows.Forms.Padding(0);
		this.resIncomeOrganics.Name = "resIncomeOrganics";
		this.resIncomeOrganics.ResourceName = "Organics";
		this.resIncomeOrganics.Size = new System.Drawing.Size(85, 20);
		this.resIncomeOrganics.TabIndex = 68;
		// 
		// resIncomeRadioactives
		// 
		this.resIncomeRadioactives.Amount = 5000;
		this.resIncomeRadioactives.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.resIncomeRadioactives.BackColor = System.Drawing.Color.Black;
		this.resIncomeRadioactives.Change = null;
		this.resIncomeRadioactives.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.resIncomeRadioactives.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
		this.resIncomeRadioactives.Location = new System.Drawing.Point(285, 57);
		this.resIncomeRadioactives.Margin = new System.Windows.Forms.Padding(0);
		this.resIncomeRadioactives.Name = "resIncomeRadioactives";
		this.resIncomeRadioactives.ResourceName = "Radioactives";
		this.resIncomeRadioactives.Size = new System.Drawing.Size(73, 20);
		this.resIncomeRadioactives.TabIndex = 67;
		// 
		// lblIncome
		// 
		this.lblIncome.AutoSize = true;
		this.lblIncome.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblIncome.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblIncome.Location = new System.Drawing.Point(6, 58);
		this.lblIncome.Name = "lblIncome";
		this.lblIncome.Size = new System.Drawing.Size(48, 15);
		this.lblIncome.TabIndex = 66;
		this.lblIncome.Text = "Income";
		// 
		// txtDescription
		// 
		this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtDescription.Location = new System.Drawing.Point(10, 163);
		this.txtDescription.Name = "txtDescription";
		this.txtDescription.Size = new System.Drawing.Size(338, 101);
		this.txtDescription.TabIndex = 50;
		this.txtDescription.Text = "Large planet with an extended troposphere.";
		// 
		// txtValueRadioactives
		// 
		this.txtValueRadioactives.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtValueRadioactives.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
		this.txtValueRadioactives.Location = new System.Drawing.Point(227, 133);
		this.txtValueRadioactives.Name = "txtValueRadioactives";
		this.txtValueRadioactives.Size = new System.Drawing.Size(45, 23);
		this.txtValueRadioactives.TabIndex = 49;
		this.txtValueRadioactives.Text = "150%";
		// 
		// txtValueOrganics
		// 
		this.txtValueOrganics.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtValueOrganics.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
		this.txtValueOrganics.Location = new System.Drawing.Point(190, 133);
		this.txtValueOrganics.Name = "txtValueOrganics";
		this.txtValueOrganics.Size = new System.Drawing.Size(45, 23);
		this.txtValueOrganics.TabIndex = 48;
		this.txtValueOrganics.Text = "150%";
		// 
		// txtValueMinerals
		// 
		this.txtValueMinerals.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtValueMinerals.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
		this.txtValueMinerals.Location = new System.Drawing.Point(156, 133);
		this.txtValueMinerals.Name = "txtValueMinerals";
		this.txtValueMinerals.Size = new System.Drawing.Size(45, 23);
		this.txtValueMinerals.TabIndex = 47;
		this.txtValueMinerals.Text = "150%";
		// 
		// lblValue
		// 
		this.lblValue.AutoSize = true;
		this.lblValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblValue.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblValue.Location = new System.Drawing.Point(144, 118);
		this.lblValue.Name = "lblValue";
		this.lblValue.Size = new System.Drawing.Size(38, 15);
		this.lblValue.TabIndex = 16;
		this.lblValue.Text = "Value";
		// 
		// txtConditions
		// 
		this.txtConditions.AutoSize = true;
		this.txtConditions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtConditions.Location = new System.Drawing.Point(159, 103);
		this.txtConditions.Name = "txtConditions";
		this.txtConditions.Size = new System.Drawing.Size(50, 15);
		this.txtConditions.TabIndex = 15;
		this.txtConditions.Text = "Optimal";
		// 
		// lblConditions
		// 
		this.lblConditions.AutoSize = true;
		this.lblConditions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblConditions.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblConditions.Location = new System.Drawing.Point(144, 88);
		this.lblConditions.Name = "lblConditions";
		this.lblConditions.Size = new System.Drawing.Size(65, 15);
		this.lblConditions.TabIndex = 14;
		this.lblConditions.Text = "Conditions";
		// 
		// txtAtmosphere
		// 
		this.txtAtmosphere.AutoSize = true;
		this.txtAtmosphere.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtAtmosphere.Location = new System.Drawing.Point(159, 73);
		this.txtAtmosphere.Name = "txtAtmosphere";
		this.txtAtmosphere.Size = new System.Drawing.Size(56, 15);
		this.txtAtmosphere.TabIndex = 13;
		this.txtAtmosphere.Text = "Methane";
		// 
		// lblAtmosphere
		// 
		this.lblAtmosphere.AutoSize = true;
		this.lblAtmosphere.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblAtmosphere.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblAtmosphere.Location = new System.Drawing.Point(144, 58);
		this.lblAtmosphere.Name = "lblAtmosphere";
		this.lblAtmosphere.Size = new System.Drawing.Size(73, 15);
		this.lblAtmosphere.TabIndex = 12;
		this.lblAtmosphere.Text = "Atmosphere";
		// 
		// txtSizeSurface
		// 
		this.txtSizeSurface.AutoSize = true;
		this.txtSizeSurface.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtSizeSurface.Location = new System.Drawing.Point(159, 26);
		this.txtSizeSurface.Name = "txtSizeSurface";
		this.txtSizeSurface.Size = new System.Drawing.Size(108, 15);
		this.txtSizeSurface.TabIndex = 11;
		this.txtSizeSurface.Text = "Large Rock Planet";
		// 
		// txtName
		// 
		this.txtName.AutoSize = true;
		this.txtName.Location = new System.Drawing.Point(143, 6);
		this.txtName.Name = "txtName";
		this.txtName.Size = new System.Drawing.Size(79, 20);
		this.txtName.TabIndex = 10;
		this.txtName.Text = "Tudran IV";
		// 
		// picOwnerFlag
		// 
		this.picOwnerFlag.Location = new System.Drawing.Point(6, 6);
		this.picOwnerFlag.Name = "picOwnerFlag";
		this.picOwnerFlag.Size = new System.Drawing.Size(34, 20);
		this.picOwnerFlag.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.picOwnerFlag.TabIndex = 8;
		this.picOwnerFlag.TabStop = false;
		// 
		// pageFacil
		// 
		this.pageFacil.BackColor = System.Drawing.Color.Black;
		this.pageFacil.Controls.Add(this.txtFacilitySlotsFree);
		this.pageFacil.Controls.Add(this.lstFacilitiesDetail);
		this.pageFacil.Location = new System.Drawing.Point(4, 29);
		this.pageFacil.Name = "pageFacil";
		this.pageFacil.Padding = new System.Windows.Forms.Padding(3);
		this.pageFacil.Size = new System.Drawing.Size(367, 426);
		this.pageFacil.TabIndex = 2;
		this.pageFacil.Text = "Facil";
		// 
		// txtFacilitySlotsFree
		// 
		this.txtFacilitySlotsFree.AutoSize = true;
		this.txtFacilitySlotsFree.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtFacilitySlotsFree.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.txtFacilitySlotsFree.Location = new System.Drawing.Point(3, 3);
		this.txtFacilitySlotsFree.Name = "txtFacilitySlotsFree";
		this.txtFacilitySlotsFree.Size = new System.Drawing.Size(83, 15);
		this.txtFacilitySlotsFree.TabIndex = 36;
		this.txtFacilitySlotsFree.Text = "0/12 slots free";
		// 
		// lstFacilitiesDetail
		// 
		this.lstFacilitiesDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.lstFacilitiesDetail.BackColor = System.Drawing.Color.Black;
		this.lstFacilitiesDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.lstFacilitiesDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lstFacilitiesDetail.ForeColor = System.Drawing.Color.White;
		this.lstFacilitiesDetail.HideSelection = false;
		this.lstFacilitiesDetail.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4});
		this.lstFacilitiesDetail.Location = new System.Drawing.Point(0, 22);
		this.lstFacilitiesDetail.Name = "lstFacilitiesDetail";
		this.lstFacilitiesDetail.Size = new System.Drawing.Size(367, 408);
		this.lstFacilitiesDetail.TabIndex = 24;
		this.lstFacilitiesDetail.UseCompatibleStateImageBehavior = false;
		this.lstFacilitiesDetail.View = System.Windows.Forms.View.Tile;
		this.lstFacilitiesDetail.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstFacilitiesDetail_MouseDown);
		// 
		// pageRaces
		// 
		this.pageRaces.BackColor = System.Drawing.Color.Black;
		this.pageRaces.Controls.Add(this.lblPopulationSpaceFree);
		this.pageRaces.Controls.Add(this.lstRaces);
		this.pageRaces.Location = new System.Drawing.Point(4, 29);
		this.pageRaces.Name = "pageRaces";
		this.pageRaces.Padding = new System.Windows.Forms.Padding(3);
		this.pageRaces.Size = new System.Drawing.Size(367, 426);
		this.pageRaces.TabIndex = 5;
		this.pageRaces.Text = "Races";
		// 
		// lblPopulationSpaceFree
		// 
		this.lblPopulationSpaceFree.AutoSize = true;
		this.lblPopulationSpaceFree.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblPopulationSpaceFree.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblPopulationSpaceFree.Location = new System.Drawing.Point(3, 3);
		this.lblPopulationSpaceFree.Name = "lblPopulationSpaceFree";
		this.lblPopulationSpaceFree.Size = new System.Drawing.Size(118, 15);
		this.lblPopulationSpaceFree.TabIndex = 37;
		this.lblPopulationSpaceFree.Text = "1000M / 4000M free";
		// 
		// lstRaces
		// 
		this.lstRaces.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.lstRaces.BackColor = System.Drawing.Color.Black;
		this.lstRaces.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.lstRaces.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lstRaces.ForeColor = System.Drawing.Color.White;
		this.lstRaces.HideSelection = false;
		this.lstRaces.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem5,
            listViewItem6});
		this.lstRaces.Location = new System.Drawing.Point(-1, 21);
		this.lstRaces.Name = "lstRaces";
		this.lstRaces.Size = new System.Drawing.Size(368, 399);
		this.lstRaces.TabIndex = 24;
		this.lstRaces.UseCompatibleStateImageBehavior = false;
		this.lstRaces.View = System.Windows.Forms.View.Tile;
		// 
		// pageCargo
		// 
		this.pageCargo.BackColor = System.Drawing.Color.Black;
		this.pageCargo.Controls.Add(this.txtCargoSpaceFree);
		this.pageCargo.Controls.Add(this.lstCargoDetail);
		this.pageCargo.Location = new System.Drawing.Point(4, 29);
		this.pageCargo.Name = "pageCargo";
		this.pageCargo.Padding = new System.Windows.Forms.Padding(3);
		this.pageCargo.Size = new System.Drawing.Size(367, 426);
		this.pageCargo.TabIndex = 3;
		this.pageCargo.Text = "Cargo";
		// 
		// txtCargoSpaceFree
		// 
		this.txtCargoSpaceFree.AutoSize = true;
		this.txtCargoSpaceFree.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtCargoSpaceFree.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.txtCargoSpaceFree.Location = new System.Drawing.Point(3, 3);
		this.txtCargoSpaceFree.Name = "txtCargoSpaceFree";
		this.txtCargoSpaceFree.Size = new System.Drawing.Size(122, 15);
		this.txtCargoSpaceFree.TabIndex = 37;
		this.txtCargoSpaceFree.Text = "1000kT / 4000kT free";
		// 
		// lstCargoDetail
		// 
		this.lstCargoDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.lstCargoDetail.BackColor = System.Drawing.Color.Black;
		this.lstCargoDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.lstCargoDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lstCargoDetail.ForeColor = System.Drawing.Color.White;
		this.lstCargoDetail.HideSelection = false;
		this.lstCargoDetail.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem7,
            listViewItem8});
		this.lstCargoDetail.Location = new System.Drawing.Point(-1, 21);
		this.lstCargoDetail.Name = "lstCargoDetail";
		this.lstCargoDetail.Size = new System.Drawing.Size(368, 399);
		this.lstCargoDetail.TabIndex = 24;
		this.lstCargoDetail.UseCompatibleStateImageBehavior = false;
		this.lstCargoDetail.View = System.Windows.Forms.View.Tile;
		// 
		// pageOrders
		// 
		this.pageOrders.BackColor = System.Drawing.Color.Black;
		this.pageOrders.Controls.Add(this.chkOnHold);
		this.pageOrders.Controls.Add(this.chkRepeat);
		this.pageOrders.Controls.Add(this.btnOrdersClear);
		this.pageOrders.Controls.Add(this.btnOrderDelete);
		this.pageOrders.Controls.Add(this.btnOrderDown);
		this.pageOrders.Controls.Add(this.btnOrderUp);
		this.pageOrders.Controls.Add(this.lstOrdersDetail);
		this.pageOrders.Location = new System.Drawing.Point(4, 29);
		this.pageOrders.Name = "pageOrders";
		this.pageOrders.Padding = new System.Windows.Forms.Padding(3);
		this.pageOrders.Size = new System.Drawing.Size(367, 426);
		this.pageOrders.TabIndex = 1;
		this.pageOrders.Text = "Orders";
		// 
		// chkOnHold
		// 
		this.chkOnHold.AutoSize = true;
		this.chkOnHold.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
		this.chkOnHold.Location = new System.Drawing.Point(118, 6);
		this.chkOnHold.Name = "chkOnHold";
		this.chkOnHold.Size = new System.Drawing.Size(111, 19);
		this.chkOnHold.TabIndex = 21;
		this.chkOnHold.Text = "Orders On Hold";
		this.chkOnHold.UseVisualStyleBackColor = true;
		this.chkOnHold.CheckedChanged += new System.EventHandler(this.chkOnHold_CheckedChanged);
		// 
		// chkRepeat
		// 
		this.chkRepeat.AutoSize = true;
		this.chkRepeat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
		this.chkRepeat.Location = new System.Drawing.Point(6, 6);
		this.chkRepeat.Name = "chkRepeat";
		this.chkRepeat.Size = new System.Drawing.Size(106, 19);
		this.chkRepeat.TabIndex = 19;
		this.chkRepeat.Text = "Repeat Orders";
		this.chkRepeat.UseVisualStyleBackColor = true;
		this.chkRepeat.CheckedChanged += new System.EventHandler(this.chkRepeat_CheckedChanged);
		// 
		// btnOrdersClear
		// 
		this.btnOrdersClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.btnOrdersClear.BackColor = System.Drawing.Color.Black;
		this.btnOrdersClear.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnOrdersClear.Location = new System.Drawing.Point(299, 443);
		this.btnOrdersClear.Name = "btnOrdersClear";
		this.btnOrdersClear.Size = new System.Drawing.Size(57, 33);
		this.btnOrdersClear.TabIndex = 4;
		this.btnOrdersClear.Text = "Clear";
		this.btnOrdersClear.UseVisualStyleBackColor = false;
		// 
		// btnOrderDelete
		// 
		this.btnOrderDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.btnOrderDelete.BackColor = System.Drawing.Color.Black;
		this.btnOrderDelete.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnOrderDelete.Location = new System.Drawing.Point(227, 443);
		this.btnOrderDelete.Name = "btnOrderDelete";
		this.btnOrderDelete.Size = new System.Drawing.Size(57, 33);
		this.btnOrderDelete.TabIndex = 3;
		this.btnOrderDelete.Text = "Del";
		this.btnOrderDelete.UseVisualStyleBackColor = false;
		// 
		// btnOrderDown
		// 
		this.btnOrderDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		this.btnOrderDown.BackColor = System.Drawing.Color.Black;
		this.btnOrderDown.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnOrderDown.Location = new System.Drawing.Point(80, 443);
		this.btnOrderDown.Name = "btnOrderDown";
		this.btnOrderDown.Size = new System.Drawing.Size(57, 33);
		this.btnOrderDown.TabIndex = 2;
		this.btnOrderDown.Text = "Dn";
		this.btnOrderDown.UseVisualStyleBackColor = false;
		// 
		// btnOrderUp
		// 
		this.btnOrderUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		this.btnOrderUp.BackColor = System.Drawing.Color.Black;
		this.btnOrderUp.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnOrderUp.Location = new System.Drawing.Point(8, 443);
		this.btnOrderUp.Name = "btnOrderUp";
		this.btnOrderUp.Size = new System.Drawing.Size(57, 33);
		this.btnOrderUp.TabIndex = 1;
		this.btnOrderUp.Text = "Up";
		this.btnOrderUp.UseVisualStyleBackColor = false;
		// 
		// lstOrdersDetail
		// 
		this.lstOrdersDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.lstOrdersDetail.BackColor = System.Drawing.Color.Black;
		this.lstOrdersDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.lstOrdersDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
		this.lstOrdersDetail.ForeColor = System.Drawing.Color.White;
		this.lstOrdersDetail.FormattingEnabled = true;
		this.lstOrdersDetail.ItemHeight = 15;
		this.lstOrdersDetail.Items.AddRange(new object[] {
            "Launch All Fighters"});
		this.lstOrdersDetail.Location = new System.Drawing.Point(3, 40);
		this.lstOrdersDetail.Name = "lstOrdersDetail";
		this.lstOrdersDetail.Size = new System.Drawing.Size(361, 375);
		this.lstOrdersDetail.TabIndex = 0;
		this.lstOrdersDetail.SelectedIndexChanged += new System.EventHandler(this.lstOrdersDetail_SelectedIndexChanged);
		// 
		// pageAbility
		// 
		this.pageAbility.BackColor = System.Drawing.Color.Black;
		this.pageAbility.Controls.Add(this.abilityTreeView);
		this.pageAbility.Location = new System.Drawing.Point(4, 29);
		this.pageAbility.Name = "pageAbility";
		this.pageAbility.Padding = new System.Windows.Forms.Padding(3);
		this.pageAbility.Size = new System.Drawing.Size(367, 426);
		this.pageAbility.TabIndex = 4;
		this.pageAbility.Text = "Ability";
		// 
		// abilityTreeView
		// 
		this.abilityTreeView.Abilities = null;
		this.abilityTreeView.BackColor = System.Drawing.Color.Black;
		this.abilityTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
		this.abilityTreeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.abilityTreeView.ForeColor = System.Drawing.Color.White;
		this.abilityTreeView.IntrinsicAbilities = null;
		this.abilityTreeView.Location = new System.Drawing.Point(3, 3);
		this.abilityTreeView.Name = "abilityTreeView";
		this.abilityTreeView.Size = new System.Drawing.Size(361, 420);
		this.abilityTreeView.TabIndex = 0;
		// 
		// PlanetReport
		// 
		this.AutoScroll = true;
		this.BackColor = System.Drawing.Color.Black;
		this.Controls.Add(this.gameTabControl1);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "PlanetReport";
		this.Size = new System.Drawing.Size(375, 459);
		this.gameTabControl1.ResumeLayout(false);
		this.pageDetail.ResumeLayout(false);
		this.pageDetail.PerformLayout();
		((System.ComponentModel.ISupportInitialize)(this.picPortrait)).EndInit();
		this.pnlColony.ResumeLayout(false);
		this.pnlColony.PerformLayout();
		((System.ComponentModel.ISupportInitialize)(this.picOwnerFlag)).EndInit();
		this.pageFacil.ResumeLayout(false);
		this.pageFacil.PerformLayout();
		this.pageRaces.ResumeLayout(false);
		this.pageRaces.PerformLayout();
		this.pageCargo.ResumeLayout(false);
		this.pageCargo.PerformLayout();
		this.pageOrders.ResumeLayout(false);
		this.pageOrders.PerformLayout();
		this.pageAbility.ResumeLayout(false);
		this.ResumeLayout(false);

	}

	#endregion

	private GameTabControl gameTabControl1;
	private System.Windows.Forms.TabPage pageDetail;
	private System.Windows.Forms.Label lblValue;
	private System.Windows.Forms.Label txtConditions;
	private System.Windows.Forms.Label lblConditions;
	private System.Windows.Forms.Label txtAtmosphere;
	private System.Windows.Forms.Label lblAtmosphere;
	private System.Windows.Forms.Label txtSizeSurface;
	private System.Windows.Forms.Label txtName;
	private System.Windows.Forms.PictureBox picOwnerFlag;
	private System.Windows.Forms.TabPage pageOrders;
	private GameButton btnOrdersClear;
	private GameButton btnOrderDelete;
	private GameButton btnOrderDown;
	private GameButton btnOrderUp;
	private System.Windows.Forms.ListBox lstOrdersDetail;
	private System.Windows.Forms.TabPage pageFacil;
	private System.Windows.Forms.TabPage pageCargo;
	private System.Windows.Forms.Label txtCargoSpaceFree;
	private System.Windows.Forms.ListView lstCargoDetail;
	private System.Windows.Forms.TabPage pageAbility;
	private System.Windows.Forms.Label txtValueRadioactives;
	private System.Windows.Forms.Label txtValueOrganics;
	private System.Windows.Forms.Label txtValueMinerals;
	private System.Windows.Forms.Label txtDescription;
	private System.Windows.Forms.Label txtFacilitySlotsFree;
	private System.Windows.Forms.ListView lstFacilitiesDetail;
	private GamePanel pnlColony;
	private System.Windows.Forms.Label txtConstructionTime;
	private System.Windows.Forms.Label lblConstructionTime;
	private System.Windows.Forms.Label txtConstructionItem;
	private System.Windows.Forms.Label lblConstructionItem;
	private ResourceDisplay resIntel;
	private ResourceDisplay resResearch;
	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.Label txtMood;
	private System.Windows.Forms.Label lblMood;
	private System.Windows.Forms.Label txtPopulation;
	private System.Windows.Forms.Label lblPopulation;
	private System.Windows.Forms.Label txtColonyType;
	private System.Windows.Forms.Label lblColonyType;
	private ResourceDisplay resIncomeMinerals;
	private ResourceDisplay resIncomeOrganics;
	private ResourceDisplay resIncomeRadioactives;
	private System.Windows.Forms.Label lblIncome;
	private GamePictureBox picPortrait;
	private AbilityTreeView abilityTreeView;
	private System.Windows.Forms.Label txtAge;
	private System.Windows.Forms.TabPage pageRaces;
	private System.Windows.Forms.Label lblPopulationSpaceFree;
	private System.Windows.Forms.ListView lstRaces;
	private System.Windows.Forms.CheckBox chkRepeat;
	private System.Windows.Forms.CheckBox chkOnHold;
}
