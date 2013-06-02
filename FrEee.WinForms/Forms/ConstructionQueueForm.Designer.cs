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
			this.btnCancel = new FrEee.WinForms.Controls.GameButton();
			this.btnOK = new FrEee.WinForms.Controls.GameButton();
			this.btnReorder = new FrEee.WinForms.Controls.GameButton();
			this.btnClear = new FrEee.WinForms.Controls.GameButton();
			this.btnLoadQueue = new FrEee.WinForms.Controls.GameButton();
			this.btnSaveQueue = new FrEee.WinForms.Controls.GameButton();
			this.btnClearMoveTo = new FrEee.WinForms.Controls.GameButton();
			this.btnSetMoveTo = new FrEee.WinForms.Controls.GameButton();
			this.gamePanel2 = new FrEee.WinForms.Controls.GamePanel();
			this.lstQueue = new System.Windows.Forms.ListView();
			this.Item = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Progress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.lstFacilities = new System.Windows.Forms.ListView();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.resRadioactivesRate = new FrEee.WinForms.Controls.ResourceDisplay();
			this.resOrganicsRate = new FrEee.WinForms.Controls.ResourceDisplay();
			this.resMineralsRate = new FrEee.WinForms.Controls.ResourceDisplay();
			this.gamePanel2.SuspendLayout();
			this.gamePanel1.SuspendLayout();
			this.gameTabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(253, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(95, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Construction Rate:";
			// 
			// chkOnlyLatest
			// 
			this.chkOnlyLatest.AutoSize = true;
			this.chkOnlyLatest.Location = new System.Drawing.Point(611, 39);
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
			this.chkEmergency.Location = new System.Drawing.Point(611, 62);
			this.chkEmergency.Name = "chkEmergency";
			this.chkEmergency.Size = new System.Drawing.Size(105, 17);
			this.chkEmergency.TabIndex = 9;
			this.chkEmergency.Text = "Emergency Build";
			this.chkEmergency.UseVisualStyleBackColor = true;
			// 
			// chkRepeat
			// 
			this.chkRepeat.AutoSize = true;
			this.chkRepeat.Location = new System.Drawing.Point(611, 85);
			this.chkRepeat.Name = "chkRepeat";
			this.chkRepeat.Size = new System.Drawing.Size(87, 17);
			this.chkRepeat.TabIndex = 10;
			this.chkRepeat.Text = "Repeat Build";
			this.chkRepeat.UseVisualStyleBackColor = true;
			// 
			// chkOnHold
			// 
			this.chkOnHold.AutoSize = true;
			this.chkOnHold.Location = new System.Drawing.Point(611, 108);
			this.chkOnHold.Name = "chkOnHold";
			this.chkOnHold.Size = new System.Drawing.Size(65, 17);
			this.chkOnHold.TabIndex = 11;
			this.chkOnHold.Text = "On Hold";
			this.chkOnHold.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.BackColor = System.Drawing.Color.Black;
			this.btnCancel.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnCancel.Location = new System.Drawing.Point(611, 398);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(169, 29);
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
			this.btnOK.Location = new System.Drawing.Point(611, 433);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(169, 29);
			this.btnOK.TabIndex = 18;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnReorder
			// 
			this.btnReorder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnReorder.BackColor = System.Drawing.Color.Black;
			this.btnReorder.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnReorder.Location = new System.Drawing.Point(611, 304);
			this.btnReorder.Name = "btnReorder";
			this.btnReorder.Size = new System.Drawing.Size(169, 29);
			this.btnReorder.TabIndex = 17;
			this.btnReorder.Text = "Reorder Queue";
			this.btnReorder.UseVisualStyleBackColor = false;
			// 
			// btnClear
			// 
			this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClear.BackColor = System.Drawing.Color.Black;
			this.btnClear.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnClear.Location = new System.Drawing.Point(611, 269);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(169, 29);
			this.btnClear.TabIndex = 16;
			this.btnClear.Text = "Clear Queue";
			this.btnClear.UseVisualStyleBackColor = false;
			// 
			// btnLoadQueue
			// 
			this.btnLoadQueue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLoadQueue.BackColor = System.Drawing.Color.Black;
			this.btnLoadQueue.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnLoadQueue.Location = new System.Drawing.Point(611, 234);
			this.btnLoadQueue.Name = "btnLoadQueue";
			this.btnLoadQueue.Size = new System.Drawing.Size(169, 29);
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
			this.btnSaveQueue.Location = new System.Drawing.Point(611, 199);
			this.btnSaveQueue.Name = "btnSaveQueue";
			this.btnSaveQueue.Size = new System.Drawing.Size(169, 29);
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
			this.btnClearMoveTo.Location = new System.Drawing.Point(611, 164);
			this.btnClearMoveTo.Name = "btnClearMoveTo";
			this.btnClearMoveTo.Size = new System.Drawing.Size(169, 29);
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
			this.btnSetMoveTo.Location = new System.Drawing.Point(611, 129);
			this.btnSetMoveTo.Name = "btnSetMoveTo";
			this.btnSetMoveTo.Size = new System.Drawing.Size(169, 29);
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
			this.gamePanel2.Location = new System.Drawing.Point(256, 36);
			this.gamePanel2.Name = "gamePanel2";
			this.gamePanel2.Padding = new System.Windows.Forms.Padding(3);
			this.gamePanel2.Size = new System.Drawing.Size(349, 426);
			this.gamePanel2.TabIndex = 7;
			// 
			// lstQueue
			// 
			this.lstQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstQueue.BackColor = System.Drawing.Color.Black;
			this.lstQueue.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstQueue.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Item,
            this.Progress,
            this.ETA});
			this.lstQueue.ForeColor = System.Drawing.Color.White;
			this.lstQueue.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lstQueue.Location = new System.Drawing.Point(3, 3);
			this.lstQueue.Name = "lstQueue";
			this.lstQueue.Size = new System.Drawing.Size(341, 418);
			this.lstQueue.TabIndex = 6;
			this.lstQueue.TileSize = new System.Drawing.Size(32, 32);
			this.lstQueue.UseCompatibleStateImageBehavior = false;
			this.lstQueue.View = System.Windows.Forms.View.Details;
			this.lstQueue.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstQueue_MouseDoubleClick);
			// 
			// Item
			// 
			this.Item.Text = "Item";
			this.Item.Width = 200;
			// 
			// Progress
			// 
			this.Progress.Text = "Progress";
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
			this.gamePanel1.Size = new System.Drawing.Size(240, 449);
			this.gamePanel1.TabIndex = 6;
			// 
			// txtName
			// 
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.txtName.AutoSize = true;
			this.txtName.Location = new System.Drawing.Point(3, 393);
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
			this.resCostRad.Location = new System.Drawing.Point(153, 419);
			this.resCostRad.Margin = new System.Windows.Forms.Padding(0);
			this.resCostRad.Name = "resCostRad";
			this.resCostRad.ResourceColor = System.Drawing.Color.Empty;
			this.resCostRad.Size = new System.Drawing.Size(69, 20);
			this.resCostRad.TabIndex = 16;
			// 
			// resCostOrg
			// 
			this.resCostOrg.Amount = 0;
			this.resCostOrg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.resCostOrg.BackColor = System.Drawing.Color.Black;
			this.resCostOrg.Change = null;
			this.resCostOrg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.resCostOrg.Location = new System.Drawing.Point(75, 419);
			this.resCostOrg.Margin = new System.Windows.Forms.Padding(0);
			this.resCostOrg.Name = "resCostOrg";
			this.resCostOrg.ResourceColor = System.Drawing.Color.Empty;
			this.resCostOrg.Size = new System.Drawing.Size(69, 20);
			this.resCostOrg.TabIndex = 15;
			// 
			// resCostMin
			// 
			this.resCostMin.Amount = 0;
			this.resCostMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.resCostMin.BackColor = System.Drawing.Color.Black;
			this.resCostMin.Change = null;
			this.resCostMin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.resCostMin.Location = new System.Drawing.Point(6, 419);
			this.resCostMin.Margin = new System.Windows.Forms.Padding(0);
			this.resCostMin.Name = "resCostMin";
			this.resCostMin.ResourceColor = System.Drawing.Color.Empty;
			this.resCostMin.Size = new System.Drawing.Size(69, 20);
			this.resCostMin.TabIndex = 14;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 406);
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
			this.gameTabControl1.Size = new System.Drawing.Size(232, 387);
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
			this.tabPage1.Size = new System.Drawing.Size(224, 361);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Ships";
			// 
			// lstShips
			// 
			this.lstShips.BackColor = System.Drawing.Color.Black;
			this.lstShips.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstShips.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstShips.ForeColor = System.Drawing.Color.White;
			this.lstShips.Location = new System.Drawing.Point(3, 3);
			this.lstShips.Name = "lstShips";
			this.lstShips.Size = new System.Drawing.Size(218, 355);
			this.lstShips.TabIndex = 13;
			this.lstShips.TileSize = new System.Drawing.Size(32, 32);
			this.lstShips.UseCompatibleStateImageBehavior = false;
			this.lstShips.View = System.Windows.Forms.View.SmallIcon;
			this.lstShips.ItemMouseHover += new System.Windows.Forms.ListViewItemMouseHoverEventHandler(this.lstShips_ItemMouseHover);
			this.lstShips.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstShips_MouseDoubleClick);
			this.lstShips.MouseLeave += new System.EventHandler(this.lstShips_MouseLeave);
			// 
			// tabPage3
			// 
			this.tabPage3.BackColor = System.Drawing.Color.Black;
			this.tabPage3.Controls.Add(this.lstFacilities);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(224, 361);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Facilities";
			// 
			// lstFacilities
			// 
			this.lstFacilities.BackColor = System.Drawing.Color.Black;
			this.lstFacilities.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstFacilities.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstFacilities.ForeColor = System.Drawing.Color.White;
			this.lstFacilities.Location = new System.Drawing.Point(0, 0);
			this.lstFacilities.Name = "lstFacilities";
			this.lstFacilities.Size = new System.Drawing.Size(224, 361);
			this.lstFacilities.TabIndex = 7;
			this.lstFacilities.TileSize = new System.Drawing.Size(32, 32);
			this.lstFacilities.UseCompatibleStateImageBehavior = false;
			this.lstFacilities.View = System.Windows.Forms.View.SmallIcon;
			this.lstFacilities.ItemMouseHover += new System.Windows.Forms.ListViewItemMouseHoverEventHandler(this.lstFacilities_ItemMouseHover);
			this.lstFacilities.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstFacilities_MouseDoubleClick);
			this.lstFacilities.MouseLeave += new System.EventHandler(this.lstFacilities_MouseLeave);
			// 
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.Color.Black;
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(224, 361);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Units";
			// 
			// tabPage4
			// 
			this.tabPage4.BackColor = System.Drawing.Color.Black;
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(224, 361);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Upgrades";
			// 
			// resRadioactivesRate
			// 
			this.resRadioactivesRate.Amount = 0;
			this.resRadioactivesRate.BackColor = System.Drawing.Color.Black;
			this.resRadioactivesRate.Change = null;
			this.resRadioactivesRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.resRadioactivesRate.Location = new System.Drawing.Point(499, 13);
			this.resRadioactivesRate.Margin = new System.Windows.Forms.Padding(0);
			this.resRadioactivesRate.Name = "resRadioactivesRate";
			this.resRadioactivesRate.ResourceColor = System.Drawing.Color.Empty;
			this.resRadioactivesRate.Size = new System.Drawing.Size(72, 20);
			this.resRadioactivesRate.TabIndex = 4;
			// 
			// resOrganicsRate
			// 
			this.resOrganicsRate.Amount = 0;
			this.resOrganicsRate.BackColor = System.Drawing.Color.Black;
			this.resOrganicsRate.Change = null;
			this.resOrganicsRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.resOrganicsRate.Location = new System.Drawing.Point(427, 13);
			this.resOrganicsRate.Margin = new System.Windows.Forms.Padding(0);
			this.resOrganicsRate.Name = "resOrganicsRate";
			this.resOrganicsRate.ResourceColor = System.Drawing.Color.Empty;
			this.resOrganicsRate.Size = new System.Drawing.Size(72, 20);
			this.resOrganicsRate.TabIndex = 3;
			// 
			// resMineralsRate
			// 
			this.resMineralsRate.Amount = 0;
			this.resMineralsRate.BackColor = System.Drawing.Color.Black;
			this.resMineralsRate.Change = null;
			this.resMineralsRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.resMineralsRate.Location = new System.Drawing.Point(355, 13);
			this.resMineralsRate.Margin = new System.Windows.Forms.Padding(0);
			this.resMineralsRate.Name = "resMineralsRate";
			this.resMineralsRate.ResourceColor = System.Drawing.Color.Empty;
			this.resMineralsRate.Size = new System.Drawing.Size(72, 20);
			this.resMineralsRate.TabIndex = 2;
			// 
			// ConstructionQueueForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(784, 474);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnReorder);
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
			this.MaximumSize = new System.Drawing.Size(800, 99999);
			this.Name = "ConstructionQueueForm";
			this.Text = "Construction Queue";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConstructionQueueForm_FormClosing);
			this.Load += new System.EventHandler(this.ConstructionQueueForm_Load);
			this.gamePanel2.ResumeLayout(false);
			this.gamePanel1.ResumeLayout(false);
			this.gamePanel1.PerformLayout();
			this.gameTabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
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
		private Controls.GameButton btnReorder;
		private System.Windows.Forms.ColumnHeader Item;
		private System.Windows.Forms.ColumnHeader Progress;
		private Controls.GameButton btnOK;
		private Controls.GameButton btnCancel;
		private System.Windows.Forms.ColumnHeader ETA;
		private System.Windows.Forms.ListView lstShips;
		private System.Windows.Forms.Label txtName;
		private Controls.ResourceDisplay resCostRad;
		private Controls.ResourceDisplay resCostOrg;
		private Controls.ResourceDisplay resCostMin;
		private System.Windows.Forms.Label label2;
	}
}