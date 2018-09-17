namespace FrEee.WinForms.Forms
{
	partial class EmpireListForm
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
			this.tabDiplomacy = new System.Windows.Forms.TabPage();
			this.btnDelete = new FrEee.WinForms.Controls.GameButton();
			this.btnCompose = new FrEee.WinForms.Controls.GameButton();
			this.btnReply = new FrEee.WinForms.Controls.GameButton();
			this.lstMessages = new System.Windows.Forms.ListView();
			this.colPortrait = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colFrom = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colTo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label13 = new System.Windows.Forms.Label();
			this.txtTreaty = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tabIntel = new System.Windows.Forms.TabPage();
			this.btnBottom = new FrEee.WinForms.Controls.GameButton();
			this.btnTop = new FrEee.WinForms.Controls.GameButton();
			this.btnDown = new FrEee.WinForms.Controls.GameButton();
			this.btnUp = new FrEee.WinForms.Controls.GameButton();
			this.btnDelIntel = new FrEee.WinForms.Controls.GameButton();
			this.btnAdd = new FrEee.WinForms.Controls.GameButton();
			this.lstIntel = new System.Windows.Forms.ListView();
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.report = new FrEee.WinForms.Controls.EmpireReport();
			this.tabBudget = new System.Windows.Forms.TabPage();
			this.rqdSpoiledDeficit = new FrEee.WinForms.Controls.ResourceQuantityDisplay();
			this.label14 = new System.Windows.Forms.Label();
			this.lblBudgetWarning = new System.Windows.Forms.Label();
			this.rqdStorage = new FrEee.WinForms.Controls.ResourceQuantityDisplay();
			this.rqdStored = new FrEee.WinForms.Controls.ResourceQuantityDisplay();
			this.rqdNet = new FrEee.WinForms.Controls.ResourceQuantityDisplay();
			this.rqdTributesOut = new FrEee.WinForms.Controls.ResourceQuantityDisplay();
			this.rqdMaintenance = new FrEee.WinForms.Controls.ResourceQuantityDisplay();
			this.rqdConstruction = new FrEee.WinForms.Controls.ResourceQuantityDisplay();
			this.rqExpenses = new FrEee.WinForms.Controls.ResourceQuantityDisplay();
			this.rqdTributesIn = new FrEee.WinForms.Controls.ResourceQuantityDisplay();
			this.rqdTrade = new FrEee.WinForms.Controls.ResourceQuantityDisplay();
			this.rqdExtraction = new FrEee.WinForms.Controls.ResourceQuantityDisplay();
			this.rqdIncome = new FrEee.WinForms.Controls.ResourceQuantityDisplay();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tabPlayerInfo = new System.Windows.Forms.TabPage();
			this.txtNotes = new System.Windows.Forms.Label();
			this.lnkWebsite = new System.Windows.Forms.LinkLabel();
			this.txtDiscord = new System.Windows.Forms.Label();
			this.txtIrc = new System.Windows.Forms.Label();
			this.lnkEmail = new System.Windows.Forms.LinkLabel();
			this.lnkPbw = new System.Windows.Forms.LinkLabel();
			this.txtName = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.btnClose = new FrEee.WinForms.Controls.GameButton();
			this.gamePanel1 = new FrEee.WinForms.Controls.GamePanel();
			this.lstEmpires = new System.Windows.Forms.ListView();
			this.btnMinisters = new FrEee.WinForms.Controls.GameButton();
			this.btnAvoidSystems = new FrEee.WinForms.Controls.GameButton();
			this.btnWaypoints = new FrEee.WinForms.Controls.GameButton();
			this.btnScores = new FrEee.WinForms.Controls.GameButton();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.tabs.SuspendLayout();
			this.tabDiplomacy.SuspendLayout();
			this.tabIntel.SuspendLayout();
			this.tabBudget.SuspendLayout();
			this.tabPlayerInfo.SuspendLayout();
			this.gamePanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabs
			// 
			this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabs.Controls.Add(this.tabDiplomacy);
			this.tabs.Controls.Add(this.tabIntel);
			this.tabs.Controls.Add(this.tabBudget);
			this.tabs.Controls.Add(this.tabPlayerInfo);
			this.tabs.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.tabs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
			this.tabs.Location = new System.Drawing.Point(12, 188);
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.SelectedTabBackColor = System.Drawing.Color.CornflowerBlue;
			this.tabs.SelectedTabForeColor = System.Drawing.Color.Black;
			this.tabs.Size = new System.Drawing.Size(710, 459);
			this.tabs.TabBackColor = System.Drawing.Color.Black;
			this.tabs.TabBorderColor = System.Drawing.Color.CornflowerBlue;
			this.tabs.TabForeColor = System.Drawing.Color.CornflowerBlue;
			this.tabs.TabIndex = 2;
			// 
			// tabDiplomacy
			// 
			this.tabDiplomacy.BackColor = System.Drawing.Color.Black;
			this.tabDiplomacy.Controls.Add(this.btnDelete);
			this.tabDiplomacy.Controls.Add(this.btnCompose);
			this.tabDiplomacy.Controls.Add(this.btnReply);
			this.tabDiplomacy.Controls.Add(this.lstMessages);
			this.tabDiplomacy.Controls.Add(this.label13);
			this.tabDiplomacy.Controls.Add(this.txtTreaty);
			this.tabDiplomacy.Controls.Add(this.label1);
			this.tabDiplomacy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.tabDiplomacy.Location = new System.Drawing.Point(4, 29);
			this.tabDiplomacy.Name = "tabDiplomacy";
			this.tabDiplomacy.Padding = new System.Windows.Forms.Padding(3);
			this.tabDiplomacy.Size = new System.Drawing.Size(702, 426);
			this.tabDiplomacy.TabIndex = 0;
			this.tabDiplomacy.Text = "Diplomacy";
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDelete.BackColor = System.Drawing.Color.Black;
			this.btnDelete.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnDelete.Location = new System.Drawing.Point(384, 364);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(100, 23);
			this.btnDelete.TabIndex = 20;
			this.btnDelete.Text = "Delete";
			this.btnDelete.UseVisualStyleBackColor = false;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnCompose
			// 
			this.btnCompose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCompose.BackColor = System.Drawing.Color.Black;
			this.btnCompose.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnCompose.Location = new System.Drawing.Point(596, 364);
			this.btnCompose.Name = "btnCompose";
			this.btnCompose.Size = new System.Drawing.Size(100, 23);
			this.btnCompose.TabIndex = 19;
			this.btnCompose.Text = "Compose";
			this.btnCompose.UseVisualStyleBackColor = false;
			this.btnCompose.Click += new System.EventHandler(this.btnCompose_Click);
			// 
			// btnReply
			// 
			this.btnReply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnReply.BackColor = System.Drawing.Color.Black;
			this.btnReply.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnReply.Location = new System.Drawing.Point(490, 364);
			this.btnReply.Name = "btnReply";
			this.btnReply.Size = new System.Drawing.Size(100, 23);
			this.btnReply.TabIndex = 18;
			this.btnReply.Text = "Reply";
			this.btnReply.UseVisualStyleBackColor = false;
			this.btnReply.Click += new System.EventHandler(this.btnReply_Click);
			// 
			// lstMessages
			// 
			this.lstMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstMessages.BackColor = System.Drawing.Color.Black;
			this.lstMessages.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPortrait,
            this.colFrom,
            this.colTo,
            this.colText});
			this.lstMessages.ForeColor = System.Drawing.Color.White;
			this.lstMessages.FullRowSelect = true;
			this.lstMessages.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lstMessages.Location = new System.Drawing.Point(9, 61);
			this.lstMessages.Name = "lstMessages";
			this.lstMessages.Size = new System.Drawing.Size(687, 297);
			this.lstMessages.TabIndex = 6;
			this.lstMessages.UseCompatibleStateImageBehavior = false;
			this.lstMessages.View = System.Windows.Forms.View.Details;
			this.lstMessages.SizeChanged += new System.EventHandler(this.lstMessages_SizeChanged);
			this.lstMessages.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstMessages_MouseDoubleClick);
			// 
			// colPortrait
			// 
			this.colPortrait.Text = "";
			this.colPortrait.Width = 64;
			// 
			// colFrom
			// 
			this.colFrom.Text = "From";
			this.colFrom.Width = 100;
			// 
			// colTo
			// 
			this.colTo.Text = "To";
			this.colTo.Width = 100;
			// 
			// colText
			// 
			this.colText.Text = "Text";
			this.colText.Width = 400;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label13.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label13.Location = new System.Drawing.Point(6, 41);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(93, 17);
			this.label13.TabIndex = 5;
			this.label13.Text = "Message Log";
			// 
			// txtTreaty
			// 
			this.txtTreaty.AutoSize = true;
			this.txtTreaty.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.txtTreaty.ForeColor = System.Drawing.Color.White;
			this.txtTreaty.Location = new System.Drawing.Point(109, 3);
			this.txtTreaty.Name = "txtTreaty";
			this.txtTreaty.Size = new System.Drawing.Size(66, 17);
			this.txtTreaty.TabIndex = 1;
			this.txtTreaty.Text = "Unknown";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label1.Location = new System.Drawing.Point(6, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(97, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "Treaty Status:";
			// 
			// tabIntel
			// 
			this.tabIntel.BackColor = System.Drawing.Color.Black;
			this.tabIntel.Controls.Add(this.btnBottom);
			this.tabIntel.Controls.Add(this.btnTop);
			this.tabIntel.Controls.Add(this.btnDown);
			this.tabIntel.Controls.Add(this.btnUp);
			this.tabIntel.Controls.Add(this.btnDelIntel);
			this.tabIntel.Controls.Add(this.btnAdd);
			this.tabIntel.Controls.Add(this.lstIntel);
			this.tabIntel.Controls.Add(this.report);
			this.tabIntel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.tabIntel.Location = new System.Drawing.Point(4, 29);
			this.tabIntel.Name = "tabIntel";
			this.tabIntel.Padding = new System.Windows.Forms.Padding(3);
			this.tabIntel.Size = new System.Drawing.Size(702, 426);
			this.tabIntel.TabIndex = 3;
			this.tabIntel.Text = "Intel";
			// 
			// btnBottom
			// 
			this.btnBottom.BackColor = System.Drawing.Color.Black;
			this.btnBottom.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnBottom.Location = new System.Drawing.Point(261, 64);
			this.btnBottom.Name = "btnBottom";
			this.btnBottom.Size = new System.Drawing.Size(53, 23);
			this.btnBottom.TabIndex = 22;
			this.btnBottom.Text = "Bottom";
			this.btnBottom.UseVisualStyleBackColor = false;
			// 
			// btnTop
			// 
			this.btnTop.BackColor = System.Drawing.Color.Black;
			this.btnTop.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnTop.Location = new System.Drawing.Point(202, 64);
			this.btnTop.Name = "btnTop";
			this.btnTop.Size = new System.Drawing.Size(53, 23);
			this.btnTop.TabIndex = 21;
			this.btnTop.Text = "Top";
			this.btnTop.UseVisualStyleBackColor = false;
			// 
			// btnDown
			// 
			this.btnDown.BackColor = System.Drawing.Color.Black;
			this.btnDown.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnDown.Location = new System.Drawing.Point(261, 35);
			this.btnDown.Name = "btnDown";
			this.btnDown.Size = new System.Drawing.Size(53, 23);
			this.btnDown.TabIndex = 20;
			this.btnDown.Text = "Down";
			this.btnDown.UseVisualStyleBackColor = false;
			// 
			// btnUp
			// 
			this.btnUp.BackColor = System.Drawing.Color.Black;
			this.btnUp.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnUp.Location = new System.Drawing.Point(202, 35);
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(53, 23);
			this.btnUp.TabIndex = 19;
			this.btnUp.Text = "Up";
			this.btnUp.UseVisualStyleBackColor = false;
			// 
			// btnDelIntel
			// 
			this.btnDelIntel.BackColor = System.Drawing.Color.Black;
			this.btnDelIntel.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnDelIntel.Location = new System.Drawing.Point(261, 6);
			this.btnDelIntel.Name = "btnDelIntel";
			this.btnDelIntel.Size = new System.Drawing.Size(53, 23);
			this.btnDelIntel.TabIndex = 18;
			this.btnDelIntel.Text = "Del";
			this.btnDelIntel.UseVisualStyleBackColor = false;
			// 
			// btnAdd
			// 
			this.btnAdd.BackColor = System.Drawing.Color.Black;
			this.btnAdd.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnAdd.Location = new System.Drawing.Point(202, 6);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(53, 23);
			this.btnAdd.TabIndex = 17;
			this.btnAdd.Text = "Add";
			this.btnAdd.UseVisualStyleBackColor = false;
			// 
			// lstIntel
			// 
			this.lstIntel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lstIntel.BackColor = System.Drawing.Color.Black;
			this.lstIntel.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstIntel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
			this.lstIntel.ForeColor = System.Drawing.Color.White;
			this.lstIntel.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lstIntel.Location = new System.Drawing.Point(3, 6);
			this.lstIntel.Name = "lstIntel";
			this.lstIntel.Size = new System.Drawing.Size(193, 390);
			this.lstIntel.TabIndex = 16;
			this.lstIntel.UseCompatibleStateImageBehavior = false;
			this.lstIntel.View = System.Windows.Forms.View.Details;
			// 
			// report
			// 
			this.report.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.report.BackColor = System.Drawing.Color.Black;
			this.report.Empire = null;
			this.report.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.report.ForeColor = System.Drawing.Color.White;
			this.report.Location = new System.Drawing.Point(320, 3);
			this.report.Name = "report";
			this.report.Size = new System.Drawing.Size(376, 390);
			this.report.TabIndex = 5;
			// 
			// tabBudget
			// 
			this.tabBudget.BackColor = System.Drawing.Color.Black;
			this.tabBudget.Controls.Add(this.rqdSpoiledDeficit);
			this.tabBudget.Controls.Add(this.label14);
			this.tabBudget.Controls.Add(this.lblBudgetWarning);
			this.tabBudget.Controls.Add(this.rqdStorage);
			this.tabBudget.Controls.Add(this.rqdStored);
			this.tabBudget.Controls.Add(this.rqdNet);
			this.tabBudget.Controls.Add(this.rqdTributesOut);
			this.tabBudget.Controls.Add(this.rqdMaintenance);
			this.tabBudget.Controls.Add(this.rqdConstruction);
			this.tabBudget.Controls.Add(this.rqExpenses);
			this.tabBudget.Controls.Add(this.rqdTributesIn);
			this.tabBudget.Controls.Add(this.rqdTrade);
			this.tabBudget.Controls.Add(this.rqdExtraction);
			this.tabBudget.Controls.Add(this.rqdIncome);
			this.tabBudget.Controls.Add(this.label12);
			this.tabBudget.Controls.Add(this.label11);
			this.tabBudget.Controls.Add(this.label10);
			this.tabBudget.Controls.Add(this.label6);
			this.tabBudget.Controls.Add(this.label7);
			this.tabBudget.Controls.Add(this.label8);
			this.tabBudget.Controls.Add(this.label9);
			this.tabBudget.Controls.Add(this.label5);
			this.tabBudget.Controls.Add(this.label4);
			this.tabBudget.Controls.Add(this.label3);
			this.tabBudget.Controls.Add(this.label2);
			this.tabBudget.Location = new System.Drawing.Point(4, 29);
			this.tabBudget.Name = "tabBudget";
			this.tabBudget.Size = new System.Drawing.Size(702, 426);
			this.tabBudget.TabIndex = 2;
			this.tabBudget.Text = "Budget";
			// 
			// rqdSpoiledDeficit
			// 
			this.rqdSpoiledDeficit.BackColor = System.Drawing.Color.Black;
			this.rqdSpoiledDeficit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.rqdSpoiledDeficit.ForeColor = System.Drawing.Color.White;
			this.rqdSpoiledDeficit.Location = new System.Drawing.Point(177, 360);
			this.rqdSpoiledDeficit.Name = "rqdSpoiledDeficit";
			this.rqdSpoiledDeficit.ResourceQuantity = null;
			this.rqdSpoiledDeficit.Size = new System.Drawing.Size(413, 24);
			this.rqdSpoiledDeficit.TabIndex = 26;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label14.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label14.Location = new System.Drawing.Point(3, 360);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(114, 17);
			this.label14.TabIndex = 25;
			this.label14.Text = "Spoilage / Deficit";
			// 
			// lblBudgetWarning
			// 
			this.lblBudgetWarning.AutoSize = true;
			this.lblBudgetWarning.ForeColor = System.Drawing.Color.Red;
			this.lblBudgetWarning.Location = new System.Drawing.Point(17, 395);
			this.lblBudgetWarning.Name = "lblBudgetWarning";
			this.lblBudgetWarning.Size = new System.Drawing.Size(320, 20);
			this.lblBudgetWarning.TabIndex = 24;
			this.lblBudgetWarning.Text = "Budget is an estimate based on known intel.";
			// 
			// rqdStorage
			// 
			this.rqdStorage.BackColor = System.Drawing.Color.Black;
			this.rqdStorage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.rqdStorage.ForeColor = System.Drawing.Color.White;
			this.rqdStorage.Location = new System.Drawing.Point(177, 330);
			this.rqdStorage.Name = "rqdStorage";
			this.rqdStorage.ResourceQuantity = null;
			this.rqdStorage.Size = new System.Drawing.Size(413, 24);
			this.rqdStorage.TabIndex = 23;
			// 
			// rqdStored
			// 
			this.rqdStored.BackColor = System.Drawing.Color.Black;
			this.rqdStored.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.rqdStored.ForeColor = System.Drawing.Color.White;
			this.rqdStored.Location = new System.Drawing.Point(177, 300);
			this.rqdStored.Name = "rqdStored";
			this.rqdStored.ResourceQuantity = null;
			this.rqdStored.Size = new System.Drawing.Size(413, 24);
			this.rqdStored.TabIndex = 22;
			// 
			// rqdNet
			// 
			this.rqdNet.BackColor = System.Drawing.Color.Black;
			this.rqdNet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.rqdNet.ForeColor = System.Drawing.Color.White;
			this.rqdNet.Location = new System.Drawing.Point(177, 270);
			this.rqdNet.Name = "rqdNet";
			this.rqdNet.ResourceQuantity = null;
			this.rqdNet.Size = new System.Drawing.Size(413, 24);
			this.rqdNet.TabIndex = 21;
			// 
			// rqdTributesOut
			// 
			this.rqdTributesOut.BackColor = System.Drawing.Color.Black;
			this.rqdTributesOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.rqdTributesOut.ForeColor = System.Drawing.Color.White;
			this.rqdTributesOut.Location = new System.Drawing.Point(177, 228);
			this.rqdTributesOut.Name = "rqdTributesOut";
			this.rqdTributesOut.ResourceQuantity = null;
			this.rqdTributesOut.Size = new System.Drawing.Size(413, 24);
			this.rqdTributesOut.TabIndex = 20;
			// 
			// rqdMaintenance
			// 
			this.rqdMaintenance.BackColor = System.Drawing.Color.Black;
			this.rqdMaintenance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.rqdMaintenance.ForeColor = System.Drawing.Color.White;
			this.rqdMaintenance.Location = new System.Drawing.Point(177, 198);
			this.rqdMaintenance.Name = "rqdMaintenance";
			this.rqdMaintenance.ResourceQuantity = null;
			this.rqdMaintenance.Size = new System.Drawing.Size(413, 24);
			this.rqdMaintenance.TabIndex = 19;
			// 
			// rqdConstruction
			// 
			this.rqdConstruction.BackColor = System.Drawing.Color.Black;
			this.rqdConstruction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.rqdConstruction.ForeColor = System.Drawing.Color.White;
			this.rqdConstruction.Location = new System.Drawing.Point(177, 168);
			this.rqdConstruction.Name = "rqdConstruction";
			this.rqdConstruction.ResourceQuantity = null;
			this.rqdConstruction.Size = new System.Drawing.Size(413, 24);
			this.rqdConstruction.TabIndex = 18;
			// 
			// rqExpenses
			// 
			this.rqExpenses.BackColor = System.Drawing.Color.Black;
			this.rqExpenses.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.rqExpenses.ForeColor = System.Drawing.Color.White;
			this.rqExpenses.Location = new System.Drawing.Point(177, 138);
			this.rqExpenses.Name = "rqExpenses";
			this.rqExpenses.ResourceQuantity = null;
			this.rqExpenses.Size = new System.Drawing.Size(413, 24);
			this.rqExpenses.TabIndex = 17;
			// 
			// rqdTributesIn
			// 
			this.rqdTributesIn.BackColor = System.Drawing.Color.Black;
			this.rqdTributesIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.rqdTributesIn.ForeColor = System.Drawing.Color.White;
			this.rqdTributesIn.Location = new System.Drawing.Point(177, 93);
			this.rqdTributesIn.Name = "rqdTributesIn";
			this.rqdTributesIn.ResourceQuantity = null;
			this.rqdTributesIn.Size = new System.Drawing.Size(413, 24);
			this.rqdTributesIn.TabIndex = 16;
			// 
			// rqdTrade
			// 
			this.rqdTrade.BackColor = System.Drawing.Color.Black;
			this.rqdTrade.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.rqdTrade.ForeColor = System.Drawing.Color.White;
			this.rqdTrade.Location = new System.Drawing.Point(177, 63);
			this.rqdTrade.Name = "rqdTrade";
			this.rqdTrade.ResourceQuantity = null;
			this.rqdTrade.Size = new System.Drawing.Size(413, 24);
			this.rqdTrade.TabIndex = 15;
			// 
			// rqdExtraction
			// 
			this.rqdExtraction.BackColor = System.Drawing.Color.Black;
			this.rqdExtraction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.rqdExtraction.ForeColor = System.Drawing.Color.White;
			this.rqdExtraction.Location = new System.Drawing.Point(177, 33);
			this.rqdExtraction.Name = "rqdExtraction";
			this.rqdExtraction.ResourceQuantity = null;
			this.rqdExtraction.Size = new System.Drawing.Size(413, 24);
			this.rqdExtraction.TabIndex = 14;
			// 
			// rqdIncome
			// 
			this.rqdIncome.BackColor = System.Drawing.Color.Black;
			this.rqdIncome.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.rqdIncome.ForeColor = System.Drawing.Color.White;
			this.rqdIncome.Location = new System.Drawing.Point(177, 3);
			this.rqdIncome.Name = "rqdIncome";
			this.rqdIncome.ResourceQuantity = null;
			this.rqdIncome.Size = new System.Drawing.Size(413, 24);
			this.rqdIncome.TabIndex = 12;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label12.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label12.Location = new System.Drawing.Point(3, 330);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(116, 17);
			this.label12.TabIndex = 11;
			this.label12.Text = "Storage Capacity";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label11.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label11.Location = new System.Drawing.Point(3, 300);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(122, 17);
			this.label11.TabIndex = 10;
			this.label11.Text = "Stored Resources";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label10.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label10.Location = new System.Drawing.Point(3, 270);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(79, 17);
			this.label10.TabIndex = 9;
			this.label10.Text = "Net Income";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label6.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label6.Location = new System.Drawing.Point(18, 228);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(60, 17);
			this.label6.TabIndex = 8;
			this.label6.Text = "Tributes";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label7.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label7.Location = new System.Drawing.Point(18, 198);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(89, 17);
			this.label7.TabIndex = 7;
			this.label7.Text = "Maintenance";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label8.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label8.Location = new System.Drawing.Point(18, 168);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(87, 17);
			this.label8.TabIndex = 6;
			this.label8.Text = "Construction";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label9.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label9.Location = new System.Drawing.Point(3, 138);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(69, 17);
			this.label9.TabIndex = 5;
			this.label9.Text = "Expenses";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label5.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label5.Location = new System.Drawing.Point(18, 93);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(60, 17);
			this.label5.TabIndex = 4;
			this.label5.Text = "Tributes";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label4.Location = new System.Drawing.Point(18, 63);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(46, 17);
			this.label4.TabIndex = 3;
			this.label4.Text = "Trade";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label3.Location = new System.Drawing.Point(18, 33);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(135, 17);
			this.label3.TabIndex = 2;
			this.label3.Text = "Resource Extraction";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label2.Location = new System.Drawing.Point(3, 3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 17);
			this.label2.TabIndex = 1;
			this.label2.Text = "Income";
			// 
			// tabPlayerInfo
			// 
			this.tabPlayerInfo.BackColor = System.Drawing.Color.Black;
			this.tabPlayerInfo.Controls.Add(this.txtNotes);
			this.tabPlayerInfo.Controls.Add(this.lnkWebsite);
			this.tabPlayerInfo.Controls.Add(this.txtDiscord);
			this.tabPlayerInfo.Controls.Add(this.txtIrc);
			this.tabPlayerInfo.Controls.Add(this.lnkEmail);
			this.tabPlayerInfo.Controls.Add(this.lnkPbw);
			this.tabPlayerInfo.Controls.Add(this.txtName);
			this.tabPlayerInfo.Controls.Add(this.label15);
			this.tabPlayerInfo.Controls.Add(this.label16);
			this.tabPlayerInfo.Controls.Add(this.label17);
			this.tabPlayerInfo.Controls.Add(this.label18);
			this.tabPlayerInfo.Controls.Add(this.label19);
			this.tabPlayerInfo.Controls.Add(this.label20);
			this.tabPlayerInfo.Controls.Add(this.label21);
			this.tabPlayerInfo.Location = new System.Drawing.Point(4, 29);
			this.tabPlayerInfo.Name = "tabPlayerInfo";
			this.tabPlayerInfo.Size = new System.Drawing.Size(702, 426);
			this.tabPlayerInfo.TabIndex = 4;
			this.tabPlayerInfo.Text = "Player Info";
			// 
			// txtNotes
			// 
			this.txtNotes.AutoSize = true;
			this.txtNotes.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.txtNotes.Location = new System.Drawing.Point(93, 158);
			this.txtNotes.Margin = new System.Windows.Forms.Padding(3);
			this.txtNotes.Name = "txtNotes";
			this.txtNotes.Size = new System.Drawing.Size(61, 20);
			this.txtNotes.TabIndex = 46;
			this.txtNotes.Text = "(Notes)";
			// 
			// lnkWebsite
			// 
			this.lnkWebsite.AutoSize = true;
			this.lnkWebsite.Location = new System.Drawing.Point(93, 133);
			this.lnkWebsite.Name = "lnkWebsite";
			this.lnkWebsite.Size = new System.Drawing.Size(58, 20);
			this.lnkWebsite.TabIndex = 45;
			this.lnkWebsite.TabStop = true;
			this.lnkWebsite.Text = "(Email)";
			this.lnkWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkWebsite_LinkClicked);
			// 
			// txtDiscord
			// 
			this.txtDiscord.AutoSize = true;
			this.txtDiscord.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.txtDiscord.Location = new System.Drawing.Point(93, 107);
			this.txtDiscord.Margin = new System.Windows.Forms.Padding(3);
			this.txtDiscord.Name = "txtDiscord";
			this.txtDiscord.Size = new System.Drawing.Size(73, 20);
			this.txtDiscord.TabIndex = 44;
			this.txtDiscord.Text = "(Discord)";
			// 
			// txtIrc
			// 
			this.txtIrc.AutoSize = true;
			this.txtIrc.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.txtIrc.Location = new System.Drawing.Point(93, 81);
			this.txtIrc.Margin = new System.Windows.Forms.Padding(3);
			this.txtIrc.Name = "txtIrc";
			this.txtIrc.Size = new System.Drawing.Size(47, 20);
			this.txtIrc.TabIndex = 43;
			this.txtIrc.Text = "(IRC)";
			// 
			// lnkEmail
			// 
			this.lnkEmail.AutoSize = true;
			this.lnkEmail.Location = new System.Drawing.Point(93, 55);
			this.lnkEmail.Name = "lnkEmail";
			this.lnkEmail.Size = new System.Drawing.Size(58, 20);
			this.lnkEmail.TabIndex = 42;
			this.lnkEmail.TabStop = true;
			this.lnkEmail.Text = "(Email)";
			this.lnkEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEmail_LinkClicked);
			// 
			// lnkPbw
			// 
			this.lnkPbw.AutoSize = true;
			this.lnkPbw.Location = new System.Drawing.Point(93, 29);
			this.lnkPbw.Name = "lnkPbw";
			this.lnkPbw.Size = new System.Drawing.Size(55, 20);
			this.lnkPbw.TabIndex = 41;
			this.lnkPbw.TabStop = true;
			this.lnkPbw.Text = "(PBW)";
			this.lnkPbw.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkPbw_LinkClicked);
			// 
			// txtName
			// 
			this.txtName.AutoSize = true;
			this.txtName.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.txtName.Location = new System.Drawing.Point(93, 3);
			this.txtName.Margin = new System.Windows.Forms.Padding(3);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(61, 20);
			this.txtName.TabIndex = 38;
			this.txtName.Text = "(Name)";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label15.Location = new System.Drawing.Point(3, 133);
			this.label15.Margin = new System.Windows.Forms.Padding(3);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(67, 20);
			this.label15.TabIndex = 37;
			this.label15.Text = "Website";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label16.Location = new System.Drawing.Point(3, 158);
			this.label16.Margin = new System.Windows.Forms.Padding(3);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(51, 20);
			this.label16.TabIndex = 36;
			this.label16.Text = "Notes";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label17.Location = new System.Drawing.Point(2, 107);
			this.label17.Margin = new System.Windows.Forms.Padding(3);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(63, 20);
			this.label17.TabIndex = 35;
			this.label17.Text = "Discord";
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label18.Location = new System.Drawing.Point(3, 81);
			this.label18.Margin = new System.Windows.Forms.Padding(3);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(37, 20);
			this.label18.TabIndex = 34;
			this.label18.Text = "IRC";
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label19.Location = new System.Drawing.Point(2, 55);
			this.label19.Margin = new System.Windows.Forms.Padding(3);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(48, 20);
			this.label19.TabIndex = 33;
			this.label19.Text = "Email";
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label20.Location = new System.Drawing.Point(3, 29);
			this.label20.Margin = new System.Windows.Forms.Padding(3);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(45, 20);
			this.label20.TabIndex = 32;
			this.label20.Text = "PBW";
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label21.Location = new System.Drawing.Point(3, 3);
			this.label21.Margin = new System.Windows.Forms.Padding(3);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(51, 20);
			this.label21.TabIndex = 31;
			this.label21.Text = "Name";
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.BackColor = System.Drawing.Color.Black;
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnClose.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnClose.Location = new System.Drawing.Point(622, 649);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(100, 23);
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// gamePanel1
			// 
			this.gamePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gamePanel1.BackColor = System.Drawing.Color.Black;
			this.gamePanel1.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.gamePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.gamePanel1.Controls.Add(this.lstEmpires);
			this.gamePanel1.ForeColor = System.Drawing.Color.White;
			this.gamePanel1.Location = new System.Drawing.Point(12, 12);
			this.gamePanel1.Name = "gamePanel1";
			this.gamePanel1.Padding = new System.Windows.Forms.Padding(3);
			this.gamePanel1.Size = new System.Drawing.Size(604, 169);
			this.gamePanel1.TabIndex = 1;
			// 
			// lstEmpires
			// 
			this.lstEmpires.BackColor = System.Drawing.Color.Black;
			this.lstEmpires.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstEmpires.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstEmpires.ForeColor = System.Drawing.Color.White;
			this.lstEmpires.HideSelection = false;
			this.lstEmpires.Location = new System.Drawing.Point(3, 3);
			this.lstEmpires.Name = "lstEmpires";
			this.lstEmpires.Size = new System.Drawing.Size(596, 161);
			this.lstEmpires.TabIndex = 1;
			this.lstEmpires.UseCompatibleStateImageBehavior = false;
			this.lstEmpires.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstEmpires_ItemSelectionChanged);
			// 
			// btnMinisters
			// 
			this.btnMinisters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMinisters.BackColor = System.Drawing.Color.Black;
			this.btnMinisters.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnMinisters.Location = new System.Drawing.Point(622, 39);
			this.btnMinisters.Name = "btnMinisters";
			this.btnMinisters.Size = new System.Drawing.Size(100, 23);
			this.btnMinisters.TabIndex = 3;
			this.btnMinisters.Text = "Ministers";
			this.btnMinisters.UseVisualStyleBackColor = false;
			this.btnMinisters.Click += new System.EventHandler(this.btnMinisters_Click);
			// 
			// btnAvoidSystems
			// 
			this.btnAvoidSystems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAvoidSystems.BackColor = System.Drawing.Color.Black;
			this.btnAvoidSystems.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnAvoidSystems.Location = new System.Drawing.Point(622, 68);
			this.btnAvoidSystems.Name = "btnAvoidSystems";
			this.btnAvoidSystems.Size = new System.Drawing.Size(100, 23);
			this.btnAvoidSystems.TabIndex = 4;
			this.btnAvoidSystems.Text = "Avoid Systems";
			this.btnAvoidSystems.UseVisualStyleBackColor = false;
			this.btnAvoidSystems.Click += new System.EventHandler(this.btnAvoidSystems_Click);
			// 
			// btnWaypoints
			// 
			this.btnWaypoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnWaypoints.BackColor = System.Drawing.Color.Black;
			this.btnWaypoints.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnWaypoints.Location = new System.Drawing.Point(622, 97);
			this.btnWaypoints.Name = "btnWaypoints";
			this.btnWaypoints.Size = new System.Drawing.Size(100, 23);
			this.btnWaypoints.TabIndex = 4;
			this.btnWaypoints.Text = "Waypoints";
			this.btnWaypoints.UseVisualStyleBackColor = false;
			this.btnWaypoints.Click += new System.EventHandler(this.btnWaypoints_Click);
			// 
			// btnScores
			// 
			this.btnScores.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnScores.BackColor = System.Drawing.Color.Black;
			this.btnScores.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnScores.Location = new System.Drawing.Point(622, 126);
			this.btnScores.Name = "btnScores";
			this.btnScores.Size = new System.Drawing.Size(100, 23);
			this.btnScores.TabIndex = 5;
			this.btnScores.Text = "Scores";
			this.btnScores.UseVisualStyleBackColor = false;
			this.btnScores.Click += new System.EventHandler(this.btnScores_Click);
			// 
			// EmpireListForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(734, 688);
			this.Controls.Add(this.btnScores);
			this.Controls.Add(this.btnWaypoints);
			this.Controls.Add(this.btnAvoidSystems);
			this.Controls.Add(this.btnMinisters);
			this.Controls.Add(this.tabs);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.gamePanel1);
			this.Name = "EmpireListForm";
			this.Text = "Empires";
			this.tabs.ResumeLayout(false);
			this.tabDiplomacy.ResumeLayout(false);
			this.tabDiplomacy.PerformLayout();
			this.tabIntel.ResumeLayout(false);
			this.tabBudget.ResumeLayout(false);
			this.tabBudget.PerformLayout();
			this.tabPlayerInfo.ResumeLayout(false);
			this.tabPlayerInfo.PerformLayout();
			this.gamePanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.GamePanel gamePanel1;
		private System.Windows.Forms.ListView lstEmpires;
		private Controls.GameButton btnClose;
		private System.Windows.Forms.TabPage tabDiplomacy;
		private Controls.GameTabControl tabs;
		private System.Windows.Forms.TabPage tabBudget;
		private System.Windows.Forms.Label txtTreaty;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private Controls.ResourceQuantityDisplay rqdIncome;
		private Controls.ResourceQuantityDisplay rqdStored;
		private Controls.ResourceQuantityDisplay rqdNet;
		private Controls.ResourceQuantityDisplay rqdTributesOut;
		private Controls.ResourceQuantityDisplay rqdMaintenance;
		private Controls.ResourceQuantityDisplay rqdConstruction;
		private Controls.ResourceQuantityDisplay rqExpenses;
		private Controls.ResourceQuantityDisplay rqdTributesIn;
		private Controls.ResourceQuantityDisplay rqdTrade;
		private Controls.ResourceQuantityDisplay rqdExtraction;
		private Controls.ResourceQuantityDisplay rqdStorage;
		private System.Windows.Forms.ListView lstMessages;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TabPage tabIntel;
		private Controls.EmpireReport report;
		private Controls.GameButton btnBottom;
		private Controls.GameButton btnTop;
		private Controls.GameButton btnDown;
		private Controls.GameButton btnUp;
		private Controls.GameButton btnDelIntel;
		private Controls.GameButton btnAdd;
		private System.Windows.Forms.ListView lstIntel;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Label lblBudgetWarning;
		private Controls.GameButton btnReply;
		private Controls.GameButton btnCompose;
		private Controls.GameButton btnDelete;
		private System.Windows.Forms.ColumnHeader colFrom;
		private System.Windows.Forms.ColumnHeader colTo;
		private System.Windows.Forms.ColumnHeader colText;
		private System.Windows.Forms.ColumnHeader colPortrait;
		private Controls.GameButton btnMinisters;
		private Controls.GameButton btnAvoidSystems;
		private Controls.GameButton btnWaypoints;
		private Controls.ResourceQuantityDisplay rqdSpoiledDeficit;
		private System.Windows.Forms.Label label14;
		private Controls.GameButton btnScores;
		private System.Windows.Forms.TabPage tabPlayerInfo;
		private System.Windows.Forms.Label txtName;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label txtDiscord;
		private System.Windows.Forms.Label txtIrc;
		private System.Windows.Forms.LinkLabel lnkEmail;
		private System.Windows.Forms.LinkLabel lnkPbw;
		private System.Windows.Forms.Label txtNotes;
		private System.Windows.Forms.LinkLabel lnkWebsite;
		private System.Windows.Forms.ToolTip toolTip;
	}
}