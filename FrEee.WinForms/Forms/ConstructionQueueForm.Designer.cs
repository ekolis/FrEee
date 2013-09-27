namespace FrEee.WinForms.Forms
{
	partial class ConstructionQueueForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.chkOnlyLatest = new System.Windows.Forms.CheckBox();
			this.chkEmergency = new System.Windows.Forms.CheckBox();
			this.chkRepeat = new System.Windows.Forms.CheckBox();
			this.chkOnHold = new System.Windows.Forms.CheckBox();
			this.btnBottom = new FrEee.WinForms.Controls.GameButton();
			this.btnTop = new FrEee.WinForms.Controls.GameButton();
			this.btnDown = new FrEee.WinForms.Controls.GameButton();
			this.btnUp = new FrEee.WinForms.Controls.GameButton();
			this.btnCancel = new FrEee.WinForms.Controls.GameButton();
			this.btnOK = new FrEee.WinForms.Controls.GameButton();
			this.btnClear = new FrEee.WinForms.Controls.GameButton();
			this.btnLoadQueue = new FrEee.WinForms.Controls.GameButton();
			this.btnSaveQueue = new FrEee.WinForms.Controls.GameButton();
			this.btnClearMoveTo = new FrEee.WinForms.Controls.GameButton();
			this.btnSetMoveTo = new FrEee.WinForms.Controls.GameButton();
			this.gamePanel2 = new FrEee.WinForms.Controls.GamePanel();
			this.lstQueue = new System.Windows.Forms.ListView();
			this.Item = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ETA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.gamePanel1 = new FrEee.WinForms.Controls.GamePanel();
			this.txtName = new System.Windows.Forms.Label();
			this.resCostRad = new FrEee.WinForms.Controls.ResourceDisplay();
			this.resCostOrg = new FrEee.WinForms.Controls.ResourceDisplay();
			this.resCostMin = new FrEee.WinForms.Controls.ResourceDisplay();
			this.label2 = new System.Windows.Forms.Label();
			this.gameTabControl1 = new FrEee.WinForms.Controls.GameTabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.lstShips = new System.Windows.Forms.ListView();
			this.colShipItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colShipETA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.lstFacilities = new System.Windows.Forms.ListView();
			this.colFacilityItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colFacilityETA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.lstUpgrades = new System.Windows.Forms.ListView();
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.resRadioactivesRate = new FrEee.WinForms.Controls.ResourceDisplay();
			this.resOrganicsRate = new FrEee.WinForms.Controls.ResourceDisplay();
			this.resMineralsRate = new FrEee.WinForms.Controls.ResourceDisplay();
			this.btnDelete = new FrEee.WinForms.Controls.GameButton();
			this.txtCargoStorageFree = new System.Windows.Forms.Label();
			this.txtFacilitySlotsFree = new System.Windows.Forms.Label();
			this.gamePanel2.SuspendLayout();
			this.gamePanel1.SuspendLayout();
			this.gameTabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(298, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(95, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Construction Rate:";
			// 
			// chkOnlyLatest
			// 
			this.chkOnlyLatest.AutoSize = true;
			this.chkOnlyLatest.Checked = true;
			this.chkOnlyLatest.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkOnlyLatest.Location = new System.Drawing.Point(656, 40);
			this.chkOnlyLatest.Name = "chkOnlyLatest";
			this.chkOnlyLatest.Size = new System.Drawing.Size(79, 17);
			this.chkOnlyLatest.TabIndex = 8;
			this.chkOnlyLatest.Text = "Only Latest";
			this.chkOnlyLatest.UseVisualStyleBackColor = true;
			this.chkOnlyLatest.CheckedChanged += new System.EventHandler(this.chkOnlyLatest_CheckedChanged);
			// 
			// chkEmergency
			// 
			this.chkEmergency.AutoSize = true;
			this.chkEmergency.Location = new System.Drawing.Point(656, 63);
			this.chkEmergency.Name = "chkEmergency";
			this.chkEmergency.Size = new System.Drawing.Size(105, 17);
			this.chkEmergency.TabIndex = 9;
			this.chkEmergency.Text = "Emergency Build";
			this.chkEmergency.UseVisualStyleBackColor = true;
			// 
			// chkRepeat
			// 
			this.chkRepeat.AutoSize = true;
			this.chkRepeat.Location = new System.Drawing.Point(656, 86);
			this.chkRepeat.Name = "chkRepeat";
			this.chkRepeat.Size = new System.Drawing.Size(87, 17);
			this.chkRepeat.TabIndex = 10;
			this.chkRepeat.Text = "Repeat Build";
			this.chkRepeat.UseVisualStyleBackColor = true;
			// 
			// chkOnHold
			// 
			this.chkOnHold.AutoSize = true;
			this.chkOnHold.Location = new System.Drawing.Point(656, 109);
			this.chkOnHold.Name = "chkOnHold";
			this.chkOnHold.Size = new System.Drawing.Size(65, 17);
			this.chkOnHold.TabIndex = 11;
			this.chkOnHold.Text = "On Hold";
			this.chkOnHold.UseVisualStyleBackColor = true;
			// 
			// btnBottom
			// 
			this.btnBottom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBottom.BackColor = System.Drawing.Color.Black;
			this.btnBottom.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnBottom.Location = new System.Drawing.Point(743, 375);
			this.btnBottom.Name = "btnBottom";
			this.btnBottom.Size = new System.Drawing.Size(87, 29);
			this.btnBottom.TabIndex = 23;
			this.btnBottom.Text = "Bottom";
			this.btnBottom.UseVisualStyleBackColor = false;
			this.btnBottom.Click += new System.EventHandler(this.btnBottom_Click);
			// 
			// btnTop
			// 
			this.btnTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnTop.BackColor = System.Drawing.Color.Black;
			this.btnTop.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnTop.Location = new System.Drawing.Point(743, 339);
			this.btnTop.Name = "btnTop";
			this.btnTop.Size = new System.Drawing.Size(87, 29);
			this.btnTop.TabIndex = 22;
			this.btnTop.Text = "Top";
			this.btnTop.UseVisualStyleBackColor = false;
			this.btnTop.Click += new System.EventHandler(this.btnTop_Click);
			// 
			// btnDown
			// 
			this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDown.BackColor = System.Drawing.Color.Black;
			this.btnDown.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnDown.Location = new System.Drawing.Point(656, 375);
			this.btnDown.Name = "btnDown";
			this.btnDown.Size = new System.Drawing.Size(87, 29);
			this.btnDown.TabIndex = 21;
			this.btnDown.Text = "Down";
			this.btnDown.UseVisualStyleBackColor = false;
			this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
			// 
			// btnUp
			// 
			this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUp.BackColor = System.Drawing.Color.Black;
			this.btnUp.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnUp.Location = new System.Drawing.Point(656, 339);
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(87, 29);
			this.btnUp.TabIndex = 20;
			this.btnUp.Text = "Up";
			this.btnUp.UseVisualStyleBackColor = false;
			this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.BackColor = System.Drawing.Color.Black;
			this.btnCancel.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnCancel.Location = new System.Drawing.Point(656, 410);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(174, 29);
			this.btnCancel.TabIndex = 19;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.BackColor = System.Drawing.Color.Black;
			this.btnOK.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnOK.Location = new System.Drawing.Point(656, 445);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(174, 29);
			this.btnOK.TabIndex = 18;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnClear
			// 
			this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClear.BackColor = System.Drawing.Color.Black;
			this.btnClear.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnClear.Location = new System.Drawing.Point(656, 304);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(174, 29);
			this.btnClear.TabIndex = 16;
			this.btnClear.Text = "Clear Queue";
			this.btnClear.UseVisualStyleBackColor = false;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// btnLoadQueue
			// 
			this.btnLoadQueue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLoadQueue.BackColor = System.Drawing.Color.Black;
			this.btnLoadQueue.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnLoadQueue.Location = new System.Drawing.Point(656, 234);
			this.btnLoadQueue.Name = "btnLoadQueue";
			this.btnLoadQueue.Size = new System.Drawing.Size(174, 29);
			this.btnLoadQueue.TabIndex = 15;
			this.btnLoadQueue.Text = "Load Queue";
			this.btnLoadQueue.UseVisualStyleBackColor = false;
			// 
			// btnSaveQueue
			// 
			this.btnSaveQueue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaveQueue.BackColor = System.Drawing.Color.Black;
			this.btnSaveQueue.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnSaveQueue.Location = new System.Drawing.Point(656, 199);
			this.btnSaveQueue.Name = "btnSaveQueue";
			this.btnSaveQueue.Size = new System.Drawing.Size(174, 29);
			this.btnSaveQueue.TabIndex = 14;
			this.btnSaveQueue.Text = "Save Queue";
			this.btnSaveQueue.UseVisualStyleBackColor = false;
			// 
			// btnClearMoveTo
			// 
			this.btnClearMoveTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClearMoveTo.BackColor = System.Drawing.Color.Black;
			this.btnClearMoveTo.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnClearMoveTo.Location = new System.Drawing.Point(656, 164);
			this.btnClearMoveTo.Name = "btnClearMoveTo";
			this.btnClearMoveTo.Size = new System.Drawing.Size(174, 29);
			this.btnClearMoveTo.TabIndex = 13;
			this.btnClearMoveTo.Text = "Clear Move To";
			this.btnClearMoveTo.UseVisualStyleBackColor = false;
			// 
			// btnSetMoveTo
			// 
			this.btnSetMoveTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSetMoveTo.BackColor = System.Drawing.Color.Black;
			this.btnSetMoveTo.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnSetMoveTo.Location = new System.Drawing.Point(656, 129);
			this.btnSetMoveTo.Name = "btnSetMoveTo";
			this.btnSetMoveTo.Size = new System.Drawing.Size(174, 29);
			this.btnSetMoveTo.TabIndex = 12;
			this.btnSetMoveTo.Text = "Set Move To";
			this.btnSetMoveTo.UseVisualStyleBackColor = false;
			// 
			// gamePanel2
			// 
			this.gamePanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gamePanel2.BackColor = System.Drawing.Color.Black;
			this.gamePanel2.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.gamePanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.gamePanel2.Controls.Add(this.lstQueue);
			this.gamePanel2.ForeColor = System.Drawing.Color.White;
			this.gamePanel2.Location = new System.Drawing.Point(301, 86);
			this.gamePanel2.Name = "gamePanel2";
			this.gamePanel2.Padding = new System.Windows.Forms.Padding(3);
			this.gamePanel2.Size = new System.Drawing.Size(349, 388);
			this.gamePanel2.TabIndex = 7;
			// 
			// lstQueue
			// 
			this.lstQueue.BackColor = System.Drawing.Color.Black;
			this.lstQueue.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstQueue.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Item,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.ETA});
			this.lstQueue.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstQueue.ForeColor = System.Drawing.Color.White;
			this.lstQueue.FullRowSelect = true;
			this.lstQueue.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lstQueue.HoverSelection = true;
			this.lstQueue.Location = new System.Drawing.Point(3, 3);
			this.lstQueue.Name = "lstQueue";
			this.lstQueue.Size = new System.Drawing.Size(341, 380);
			this.lstQueue.TabIndex = 6;
			this.lstQueue.TileSize = new System.Drawing.Size(32, 32);
			this.lstQueue.UseCompatibleStateImageBehavior = false;
			this.lstQueue.View = System.Windows.Forms.View.Details;
			this.lstQueue.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstQueue_MouseDoubleClick);
			this.lstQueue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstQueue_MouseDown);
			// 
			// Item
			// 
			this.Item.Text = "Item";
			this.Item.Width = 140;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Min";
			this.columnHeader1.Width = 55;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Org";
			this.columnHeader2.Width = 55;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Rad";
			this.columnHeader3.Width = 55;
			// 
			// ETA
			// 
			this.ETA.Text = "ETA";
			// 
			// gamePanel1
			// 
			this.gamePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gamePanel1.BackColor = System.Drawing.Color.Black;
			this.gamePanel1.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.gamePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.gamePanel1.Controls.Add(this.txtName);
			this.gamePanel1.Controls.Add(this.resCostRad);
			this.gamePanel1.Controls.Add(this.resCostOrg);
			this.gamePanel1.Controls.Add(this.resCostMin);
			this.gamePanel1.Controls.Add(this.label2);
			this.gamePanel1.Controls.Add(this.gameTabControl1);
			this.gamePanel1.ForeColor = System.Drawing.Color.White;
			this.gamePanel1.Location = new System.Drawing.Point(10, 13);
			this.gamePanel1.Name = "gamePanel1";
			this.gamePanel1.Padding = new System.Windows.Forms.Padding(3);
			this.gamePanel1.Size = new System.Drawing.Size(282, 461);
			this.gamePanel1.TabIndex = 6;
			// 
			// txtName
			// 
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.txtName.AutoSize = true;
			this.txtName.Location = new System.Drawing.Point(3, 405);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(50, 13);
			this.txtName.TabIndex = 17;
			this.txtName.Text = "(No Item)";
			// 
			// resCostRad
			// 
			this.resCostRad.Amount = 0;
			this.resCostRad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.resCostRad.BackColor = System.Drawing.Color.Black;
			this.resCostRad.Change = null;
			this.resCostRad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.resCostRad.Location = new System.Drawing.Point(178, 431);
			this.resCostRad.Margin = new System.Windows.Forms.Padding(0);
			this.resCostRad.Name = "resCostRad";
			this.resCostRad.ResourceName = "Radioactives";
			this.resCostRad.Size = new System.Drawing.Size(86, 20);
			this.resCostRad.TabIndex = 16;
			// 
			// resCostOrg
			// 
			this.resCostOrg.Amount = 0;
			this.resCostOrg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.resCostOrg.BackColor = System.Drawing.Color.Black;
			this.resCostOrg.Change = null;
			this.resCostOrg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.resCostOrg.Location = new System.Drawing.Point(92, 431);
			this.resCostOrg.Margin = new System.Windows.Forms.Padding(0);
			this.resCostOrg.Name = "resCostOrg";
			this.resCostOrg.ResourceName = "Organics";
			this.resCostOrg.Size = new System.Drawing.Size(86, 20);
			this.resCostOrg.TabIndex = 15;
			// 
			// resCostMin
			// 
			this.resCostMin.Amount = 0;
			this.resCostMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.resCostMin.BackColor = System.Drawing.Color.Black;
			this.resCostMin.Change = null;
			this.resCostMin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.resCostMin.Location = new System.Drawing.Point(6, 431);
			this.resCostMin.Margin = new System.Windows.Forms.Padding(0);
			this.resCostMin.Name = "resCostMin";
			this.resCostMin.ResourceName = "Minerals";
			this.resCostMin.Size = new System.Drawing.Size(86, 20);
			this.resCostMin.TabIndex = 14;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 418);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(31, 13);
			this.label2.TabIndex = 13;
			this.label2.Text = "Cost:";
			// 
			// gameTabControl1
			// 
			this.gameTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gameTabControl1.Controls.Add(this.tabPage1);
			this.gameTabControl1.Controls.Add(this.tabPage3);
			this.gameTabControl1.Controls.Add(this.tabPage2);
			this.gameTabControl1.Controls.Add(this.tabPage4);
			this.gameTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.gameTabControl1.Location = new System.Drawing.Point(3, 3);
			this.gameTabControl1.Name = "gameTabControl1";
			this.gameTabControl1.SelectedIndex = 0;
			this.gameTabControl1.SelectedTabBackColor = System.Drawing.Color.CornflowerBlue;
			this.gameTabControl1.SelectedTabForeColor = System.Drawing.Color.Black;
			this.gameTabControl1.Size = new System.Drawing.Size(274, 399);
			this.gameTabControl1.TabBackColor = System.Drawing.Color.Black;
			this.gameTabControl1.TabForeColor = System.Drawing.Color.CornflowerBlue;
			this.gameTabControl1.TabIndex = 1;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.Color.Black;
			this.tabPage1.Controls.Add(this.lstShips);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(266, 373);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Ships";
			// 
			// lstShips
			// 
			this.lstShips.BackColor = System.Drawing.Color.Black;
			this.lstShips.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstShips.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colShipItem,
            this.colShipETA});
			this.lstShips.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstShips.ForeColor = System.Drawing.Color.White;
			this.lstShips.FullRowSelect = true;
			this.lstShips.HoverSelection = true;
			this.lstShips.Location = new System.Drawing.Point(3, 3);
			this.lstShips.Name = "lstShips";
			this.lstShips.Size = new System.Drawing.Size(260, 367);
			this.lstShips.TabIndex = 13;
			this.lstShips.TileSize = new System.Drawing.Size(32, 32);
			this.lstShips.UseCompatibleStateImageBehavior = false;
			this.lstShips.View = System.Windows.Forms.View.Details;
			this.lstShips.ItemMouseHover += new System.Windows.Forms.ListViewItemMouseHoverEventHandler(this.lstShips_ItemMouseHover);
			this.lstShips.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstShips_MouseDown);
			this.lstShips.MouseLeave += new System.EventHandler(this.lstShips_MouseLeave);
			// 
			// colShipItem
			// 
			this.colShipItem.Text = "Item";
			this.colShipItem.Width = 200;
			// 
			// colShipETA
			// 
			this.colShipETA.Text = "ETA";
			this.colShipETA.Width = 40;
			// 
			// tabPage3
			// 
			this.tabPage3.BackColor = System.Drawing.Color.Black;
			this.tabPage3.Controls.Add(this.lstFacilities);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(266, 373);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Facilities";
			// 
			// lstFacilities
			// 
			this.lstFacilities.BackColor = System.Drawing.Color.Black;
			this.lstFacilities.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstFacilities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFacilityItem,
            this.colFacilityETA});
			this.lstFacilities.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstFacilities.ForeColor = System.Drawing.Color.White;
			this.lstFacilities.FullRowSelect = true;
			this.lstFacilities.HoverSelection = true;
			this.lstFacilities.Location = new System.Drawing.Point(0, 0);
			this.lstFacilities.Name = "lstFacilities";
			this.lstFacilities.Size = new System.Drawing.Size(266, 373);
			this.lstFacilities.TabIndex = 7;
			this.lstFacilities.TileSize = new System.Drawing.Size(32, 32);
			this.lstFacilities.UseCompatibleStateImageBehavior = false;
			this.lstFacilities.View = System.Windows.Forms.View.Details;
			this.lstFacilities.ItemMouseHover += new System.Windows.Forms.ListViewItemMouseHoverEventHandler(this.lstFacilities_ItemMouseHover);
			this.lstFacilities.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstFacilities_MouseDown);
			this.lstFacilities.MouseLeave += new System.EventHandler(this.lstFacilities_MouseLeave);
			// 
			// colFacilityItem
			// 
			this.colFacilityItem.Text = "Item";
			this.colFacilityItem.Width = 200;
			// 
			// colFacilityETA
			// 
			this.colFacilityETA.Text = "ETA";
			this.colFacilityETA.Width = 40;
			// 
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.Color.Black;
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(266, 373);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Units";
			// 
			// tabPage4
			// 
			this.tabPage4.BackColor = System.Drawing.Color.Black;
			this.tabPage4.Controls.Add(this.lstUpgrades);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(266, 373);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Upgrades";
			// 
			// lstUpgrades
			// 
			this.lstUpgrades.BackColor = System.Drawing.Color.Black;
			this.lstUpgrades.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstUpgrades.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5});
			this.lstUpgrades.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstUpgrades.ForeColor = System.Drawing.Color.White;
			this.lstUpgrades.FullRowSelect = true;
			this.lstUpgrades.HoverSelection = true;
			this.lstUpgrades.Location = new System.Drawing.Point(0, 0);
			this.lstUpgrades.Name = "lstUpgrades";
			this.lstUpgrades.Size = new System.Drawing.Size(266, 373);
			this.lstUpgrades.TabIndex = 8;
			this.lstUpgrades.TileSize = new System.Drawing.Size(32, 32);
			this.lstUpgrades.UseCompatibleStateImageBehavior = false;
			this.lstUpgrades.View = System.Windows.Forms.View.Details;
			this.lstUpgrades.ItemMouseHover += new System.Windows.Forms.ListViewItemMouseHoverEventHandler(this.lstUpgrades_ItemMouseHover);
			this.lstUpgrades.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstUpgrades_MouseDown);
			this.lstUpgrades.MouseLeave += new System.EventHandler(this.lstUpgrades_MouseLeave);
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Item";
			this.columnHeader4.Width = 200;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "ETA";
			this.columnHeader5.Width = 40;
			// 
			// resRadioactivesRate
			// 
			this.resRadioactivesRate.Amount = 0;
			this.resRadioactivesRate.BackColor = System.Drawing.Color.Black;
			this.resRadioactivesRate.Change = null;
			this.resRadioactivesRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.resRadioactivesRate.Location = new System.Drawing.Point(544, 13);
			this.resRadioactivesRate.Margin = new System.Windows.Forms.Padding(0);
			this.resRadioactivesRate.Name = "resRadioactivesRate";
			this.resRadioactivesRate.ResourceName = "Radioactives";
			this.resRadioactivesRate.Size = new System.Drawing.Size(72, 20);
			this.resRadioactivesRate.TabIndex = 4;
			// 
			// resOrganicsRate
			// 
			this.resOrganicsRate.Amount = 0;
			this.resOrganicsRate.BackColor = System.Drawing.Color.Black;
			this.resOrganicsRate.Change = null;
			this.resOrganicsRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.resOrganicsRate.Location = new System.Drawing.Point(472, 13);
			this.resOrganicsRate.Margin = new System.Windows.Forms.Padding(0);
			this.resOrganicsRate.Name = "resOrganicsRate";
			this.resOrganicsRate.ResourceName = "Organics";
			this.resOrganicsRate.Size = new System.Drawing.Size(72, 20);
			this.resOrganicsRate.TabIndex = 3;
			// 
			// resMineralsRate
			// 
			this.resMineralsRate.Amount = 0;
			this.resMineralsRate.BackColor = System.Drawing.Color.Black;
			this.resMineralsRate.Change = null;
			this.resMineralsRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.resMineralsRate.Location = new System.Drawing.Point(400, 13);
			this.resMineralsRate.Margin = new System.Windows.Forms.Padding(0);
			this.resMineralsRate.Name = "resMineralsRate";
			this.resMineralsRate.ResourceName = "Minerals";
			this.resMineralsRate.Size = new System.Drawing.Size(72, 20);
			this.resMineralsRate.TabIndex = 2;
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDelete.BackColor = System.Drawing.Color.Black;
			this.btnDelete.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnDelete.Location = new System.Drawing.Point(656, 269);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(174, 29);
			this.btnDelete.TabIndex = 24;
			this.btnDelete.Text = "Delete";
			this.btnDelete.UseVisualStyleBackColor = false;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// txtCargoStorageFree
			// 
			this.txtCargoStorageFree.AutoSize = true;
			this.txtCargoStorageFree.Location = new System.Drawing.Point(298, 39);
			this.txtCargoStorageFree.Name = "txtCargoStorageFree";
			this.txtCargoStorageFree.Size = new System.Drawing.Size(102, 13);
			this.txtCargoStorageFree.TabIndex = 25;
			this.txtCargoStorageFree.Text = "Cargo Storage Free:";
			// 
			// txtFacilitySlotsFree
			// 
			this.txtFacilitySlotsFree.AutoSize = true;
			this.txtFacilitySlotsFree.Location = new System.Drawing.Point(298, 63);
			this.txtFacilitySlotsFree.Name = "txtFacilitySlotsFree";
			this.txtFacilitySlotsFree.Size = new System.Drawing.Size(92, 13);
			this.txtFacilitySlotsFree.TabIndex = 26;
			this.txtFacilitySlotsFree.Text = "Facility Slots Free:";
			// 
			// ConstructionQueueForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(832, 486);
			this.Controls.Add(this.txtFacilitySlotsFree);
			this.Controls.Add(this.txtCargoStorageFree);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnBottom);
			this.Controls.Add(this.btnTop);
			this.Controls.Add(this.btnDown);
			this.Controls.Add(this.btnUp);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnLoadQueue);
			this.Controls.Add(this.btnSaveQueue);
			this.Controls.Add(this.btnClearMoveTo);
			this.Controls.Add(this.btnSetMoveTo);
			this.Controls.Add(this.chkOnHold);
			this.Controls.Add(this.chkRepeat);
			this.Controls.Add(this.chkEmergency);
			this.Controls.Add(this.chkOnlyLatest);
			this.Controls.Add(this.gamePanel2);
			this.Controls.Add(this.gamePanel1);
			this.Controls.Add(this.resRadioactivesRate);
			this.Controls.Add(this.resOrganicsRate);
			this.Controls.Add(this.resMineralsRate);
			this.Controls.Add(this.label1);
			this.ForeColor = System.Drawing.Color.White;
			this.MaximumSize = new System.Drawing.Size(849, 99998);
			this.Name = "ConstructionQueueForm";
			this.ShowInTaskbar = false;
			this.Text = "Construction Queue";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConstructionQueueForm_FormClosing);
			this.Load += new System.EventHandler(this.ConstructionQueueForm_Load);
			this.gamePanel2.ResumeLayout(false);
			this.gamePanel1.ResumeLayout(false);
			this.gamePanel1.PerformLayout();
			this.gameTabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private Controls.ResourceDisplay resMineralsRate;
		private Controls.ResourceDisplay resOrganicsRate;
		private Controls.ResourceDisplay resRadioactivesRate;
		private Controls.GamePanel gamePanel1;
		private Controls.GameTabControl gameTabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage4;
		private Controls.GamePanel gamePanel2;
		private System.Windows.Forms.ListView lstQueue;
		private System.Windows.Forms.ListView lstFacilities;
		private System.Windows.Forms.CheckBox chkOnlyLatest;
		private System.Windows.Forms.CheckBox chkEmergency;
		private System.Windows.Forms.CheckBox chkRepeat;
		private System.Windows.Forms.CheckBox chkOnHold;
		private Controls.GameButton btnSetMoveTo;
		private Controls.GameButton btnClearMoveTo;
		private Controls.GameButton btnSaveQueue;
		private Controls.GameButton btnLoadQueue;
		private Controls.GameButton btnClear;
		private System.Windows.Forms.ColumnHeader Item;
		private Controls.GameButton btnOK;
		private Controls.GameButton btnCancel;
		private System.Windows.Forms.ColumnHeader ETA;
		private System.Windows.Forms.ListView lstShips;
		private System.Windows.Forms.Label txtName;
		private Controls.ResourceDisplay resCostRad;
		private Controls.ResourceDisplay resCostOrg;
		private Controls.ResourceDisplay resCostMin;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ColumnHeader colShipItem;
		private System.Windows.Forms.ColumnHeader colShipETA;
		private System.Windows.Forms.ColumnHeader colFacilityItem;
		private System.Windows.Forms.ColumnHeader colFacilityETA;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private Controls.GameButton btnUp;
		private Controls.GameButton btnDown;
		private Controls.GameButton btnTop;
		private Controls.GameButton btnBottom;
		private Controls.GameButton btnDelete;
		private System.Windows.Forms.ListView lstUpgrades;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.Label txtCargoStorageFree;
		private System.Windows.Forms.Label txtFacilitySlotsFree;
	}
}