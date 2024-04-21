namespace FrEee.UI.WinForms.Forms;

partial class DesignListForm
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
		this.ddlVehicleType = new System.Windows.Forms.ComboBox();
		this.chkForeign = new System.Windows.Forms.CheckBox();
		this.chkHideObsolete = new System.Windows.Forms.CheckBox();
		this.btnClose = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnGroundSimulator = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnSpaceSimulator = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnObsolete = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnUpgrade = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnEdit = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnCopy = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnCreate = new FrEee.UI.WinForms.Controls.GameButton();
		this.gamePanel2 = new FrEee.UI.WinForms.Controls.GamePanel();
		this.designReport = new FrEee.UI.WinForms.Controls.DesignReport();
		this.gamePanel1 = new FrEee.UI.WinForms.Controls.GamePanel();
		this.lstDesigns = new System.Windows.Forms.ListView();
		this.colDesign = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
		this.colTotalCost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
		this.btnStrategy = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnDelete = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnExportAll = new FrEee.UI.WinForms.Controls.GameButton();
		this.gamePanel2.SuspendLayout();
		this.gamePanel1.SuspendLayout();
		this.SuspendLayout();
		// 
		// ddlVehicleType
		// 
		this.ddlVehicleType.DisplayMember = "Name";
		this.ddlVehicleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.ddlVehicleType.FormattingEnabled = true;
		this.ddlVehicleType.Location = new System.Drawing.Point(12, 13);
		this.ddlVehicleType.Name = "ddlVehicleType";
		this.ddlVehicleType.Size = new System.Drawing.Size(200, 21);
		this.ddlVehicleType.TabIndex = 2;
		this.ddlVehicleType.ValueMember = "VehicleTypes";
		this.ddlVehicleType.SelectedIndexChanged += new System.EventHandler(this.ddlVehicleType_SelectedIndexChanged);
		// 
		// chkForeign
		// 
		this.chkForeign.AutoSize = true;
		this.chkForeign.Location = new System.Drawing.Point(111, 40);
		this.chkForeign.Name = "chkForeign";
		this.chkForeign.Size = new System.Drawing.Size(102, 17);
		this.chkForeign.TabIndex = 3;
		this.chkForeign.Text = "Foreign Designs";
		this.chkForeign.UseVisualStyleBackColor = true;
		this.chkForeign.CheckedChanged += new System.EventHandler(this.chkForeign_CheckedChanged);
		// 
		// chkHideObsolete
		// 
		this.chkHideObsolete.AutoSize = true;
		this.chkHideObsolete.Checked = true;
		this.chkHideObsolete.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkHideObsolete.Location = new System.Drawing.Point(12, 40);
		this.chkHideObsolete.Name = "chkHideObsolete";
		this.chkHideObsolete.Size = new System.Drawing.Size(93, 17);
		this.chkHideObsolete.TabIndex = 4;
		this.chkHideObsolete.Text = "Hide Obsolete";
		this.chkHideObsolete.UseVisualStyleBackColor = true;
		this.chkHideObsolete.CheckedChanged += new System.EventHandler(this.chkHideObsolete_CheckedChanged);
		// 
		// btnClose
		// 
		this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.btnClose.BackColor = System.Drawing.Color.Black;
		this.btnClose.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnClose.Location = new System.Drawing.Point(551, 440);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(151, 29);
		this.btnClose.TabIndex = 14;
		this.btnClose.Text = "Close";
		this.btnClose.UseVisualStyleBackColor = false;
		this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
		// 
		// btnGroundSimulator
		// 
		this.btnGroundSimulator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.btnGroundSimulator.BackColor = System.Drawing.Color.Black;
		this.btnGroundSimulator.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnGroundSimulator.Location = new System.Drawing.Point(551, 257);
		this.btnGroundSimulator.Name = "btnGroundSimulator";
		this.btnGroundSimulator.Size = new System.Drawing.Size(151, 29);
		this.btnGroundSimulator.TabIndex = 13;
		this.btnGroundSimulator.Text = "Ground Simulator";
		this.btnGroundSimulator.UseVisualStyleBackColor = false;
		this.btnGroundSimulator.Click += new System.EventHandler(this.btnGroundSimulator_Click);
		// 
		// btnSpaceSimulator
		// 
		this.btnSpaceSimulator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.btnSpaceSimulator.BackColor = System.Drawing.Color.Black;
		this.btnSpaceSimulator.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnSpaceSimulator.Location = new System.Drawing.Point(550, 222);
		this.btnSpaceSimulator.Name = "btnSpaceSimulator";
		this.btnSpaceSimulator.Size = new System.Drawing.Size(151, 29);
		this.btnSpaceSimulator.TabIndex = 12;
		this.btnSpaceSimulator.Text = "Space Simulator";
		this.btnSpaceSimulator.UseVisualStyleBackColor = false;
		this.btnSpaceSimulator.Click += new System.EventHandler(this.btnSpaceSimulator_Click);
		// 
		// btnObsolete
		// 
		this.btnObsolete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.btnObsolete.BackColor = System.Drawing.Color.Black;
		this.btnObsolete.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnObsolete.Location = new System.Drawing.Point(551, 152);
		this.btnObsolete.Name = "btnObsolete";
		this.btnObsolete.Size = new System.Drawing.Size(151, 29);
		this.btnObsolete.TabIndex = 11;
		this.btnObsolete.Text = "Obsolete";
		this.btnObsolete.UseVisualStyleBackColor = false;
		this.btnObsolete.Click += new System.EventHandler(this.btnObsolete_Click);
		// 
		// btnUpgrade
		// 
		this.btnUpgrade.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.btnUpgrade.BackColor = System.Drawing.Color.Black;
		this.btnUpgrade.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnUpgrade.Location = new System.Drawing.Point(551, 117);
		this.btnUpgrade.Name = "btnUpgrade";
		this.btnUpgrade.Size = new System.Drawing.Size(151, 29);
		this.btnUpgrade.TabIndex = 10;
		this.btnUpgrade.Text = "Upgrade";
		this.btnUpgrade.UseVisualStyleBackColor = false;
		this.btnUpgrade.Click += new System.EventHandler(this.btnUpgrade_Click);
		// 
		// btnEdit
		// 
		this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.btnEdit.BackColor = System.Drawing.Color.Black;
		this.btnEdit.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnEdit.Location = new System.Drawing.Point(551, 82);
		this.btnEdit.Name = "btnEdit";
		this.btnEdit.Size = new System.Drawing.Size(151, 29);
		this.btnEdit.TabIndex = 9;
		this.btnEdit.Text = "Edit";
		this.btnEdit.UseVisualStyleBackColor = false;
		this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
		// 
		// btnCopy
		// 
		this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.btnCopy.BackColor = System.Drawing.Color.Black;
		this.btnCopy.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnCopy.Location = new System.Drawing.Point(551, 47);
		this.btnCopy.Name = "btnCopy";
		this.btnCopy.Size = new System.Drawing.Size(151, 29);
		this.btnCopy.TabIndex = 8;
		this.btnCopy.Text = "Copy";
		this.btnCopy.UseVisualStyleBackColor = false;
		this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
		// 
		// btnCreate
		// 
		this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.btnCreate.BackColor = System.Drawing.Color.Black;
		this.btnCreate.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnCreate.Location = new System.Drawing.Point(551, 12);
		this.btnCreate.Name = "btnCreate";
		this.btnCreate.Size = new System.Drawing.Size(151, 29);
		this.btnCreate.TabIndex = 7;
		this.btnCreate.Text = "Create";
		this.btnCreate.UseVisualStyleBackColor = false;
		this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
		// 
		// gamePanel2
		// 
		this.gamePanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.gamePanel2.BackColor = System.Drawing.Color.Black;
		this.gamePanel2.BorderColor = System.Drawing.Color.CornflowerBlue;
		this.gamePanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.gamePanel2.Controls.Add(this.designReport);
		this.gamePanel2.ForeColor = System.Drawing.Color.White;
		this.gamePanel2.Location = new System.Drawing.Point(219, 12);
		this.gamePanel2.Name = "gamePanel2";
		this.gamePanel2.Padding = new System.Windows.Forms.Padding(3);
		this.gamePanel2.Size = new System.Drawing.Size(325, 457);
		this.gamePanel2.TabIndex = 6;
		// 
		// designReport
		// 
		this.designReport.BackColor = System.Drawing.Color.Black;
		this.designReport.Design = null;
		this.designReport.Dock = System.Windows.Forms.DockStyle.Fill;
		this.designReport.ForeColor = System.Drawing.Color.White;
		this.designReport.Location = new System.Drawing.Point(3, 3);
		this.designReport.Name = "designReport";
		this.designReport.Size = new System.Drawing.Size(317, 414);
		this.designReport.TabIndex = 1;
		// 
		// gamePanel1
		// 
		this.gamePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
		this.gamePanel1.BackColor = System.Drawing.Color.Black;
		this.gamePanel1.BorderColor = System.Drawing.Color.CornflowerBlue;
		this.gamePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.gamePanel1.Controls.Add(this.lstDesigns);
		this.gamePanel1.ForeColor = System.Drawing.Color.White;
		this.gamePanel1.Location = new System.Drawing.Point(12, 63);
		this.gamePanel1.Name = "gamePanel1";
		this.gamePanel1.Padding = new System.Windows.Forms.Padding(3);
		this.gamePanel1.Size = new System.Drawing.Size(200, 406);
		this.gamePanel1.TabIndex = 1;
		// 
		// lstDesigns
		// 
		this.lstDesigns.BackColor = System.Drawing.Color.Black;
		this.lstDesigns.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.lstDesigns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDesign,
            this.colTotalCost});
		this.lstDesigns.Dock = System.Windows.Forms.DockStyle.Fill;
		this.lstDesigns.ForeColor = System.Drawing.Color.White;
		this.lstDesigns.FullRowSelect = true;
		this.lstDesigns.Location = new System.Drawing.Point(3, 3);
		this.lstDesigns.Name = "lstDesigns";
		this.lstDesigns.Size = new System.Drawing.Size(192, 398);
		this.lstDesigns.TabIndex = 1;
		this.lstDesigns.UseCompatibleStateImageBehavior = false;
		this.lstDesigns.View = System.Windows.Forms.View.Details;
		this.lstDesigns.SelectedIndexChanged += new System.EventHandler(this.lstDesigns_SelectedIndexChanged);
		// 
		// colDesign
		// 
		this.colDesign.Text = "Design";
		this.colDesign.Width = 125;
		// 
		// colTotalCost
		// 
		this.colTotalCost.Text = "Cost";
		this.colTotalCost.Width = 50;
		// 
		// btnStrategy
		// 
		this.btnStrategy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.btnStrategy.BackColor = System.Drawing.Color.Black;
		this.btnStrategy.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnStrategy.Location = new System.Drawing.Point(550, 292);
		this.btnStrategy.Name = "btnStrategy";
		this.btnStrategy.Size = new System.Drawing.Size(151, 29);
		this.btnStrategy.TabIndex = 15;
		this.btnStrategy.Text = "Strategy";
		this.btnStrategy.UseVisualStyleBackColor = false;
		this.btnStrategy.Click += new System.EventHandler(this.btnStrategy_Click);
		// 
		// btnDelete
		// 
		this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.btnDelete.BackColor = System.Drawing.Color.Black;
		this.btnDelete.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnDelete.Location = new System.Drawing.Point(551, 187);
		this.btnDelete.Name = "btnDelete";
		this.btnDelete.Size = new System.Drawing.Size(151, 29);
		this.btnDelete.TabIndex = 16;
		this.btnDelete.Text = "Delete";
		this.btnDelete.UseVisualStyleBackColor = false;
		this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
		// 
		// btnExportAll
		// 
		this.btnExportAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.btnExportAll.BackColor = System.Drawing.Color.Black;
		this.btnExportAll.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnExportAll.Location = new System.Drawing.Point(550, 327);
		this.btnExportAll.Name = "btnExportAll";
		this.btnExportAll.Size = new System.Drawing.Size(151, 29);
		this.btnExportAll.TabIndex = 17;
		this.btnExportAll.Text = "Export All";
		this.btnExportAll.UseVisualStyleBackColor = false;
		this.btnExportAll.Click += new System.EventHandler(this.btnExportAll_Click);
		// 
		// DesignListForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.ClientSize = new System.Drawing.Size(714, 481);
		this.Controls.Add(this.btnExportAll);
		this.Controls.Add(this.btnDelete);
		this.Controls.Add(this.btnStrategy);
		this.Controls.Add(this.btnClose);
		this.Controls.Add(this.btnGroundSimulator);
		this.Controls.Add(this.btnSpaceSimulator);
		this.Controls.Add(this.btnObsolete);
		this.Controls.Add(this.btnUpgrade);
		this.Controls.Add(this.btnEdit);
		this.Controls.Add(this.btnCopy);
		this.Controls.Add(this.btnCreate);
		this.Controls.Add(this.gamePanel2);
		this.Controls.Add(this.chkHideObsolete);
		this.Controls.Add(this.chkForeign);
		this.Controls.Add(this.ddlVehicleType);
		this.Controls.Add(this.gamePanel1);
		this.MaximumSize = new System.Drawing.Size(730, 9999);
		this.Name = "DesignListForm";
		this.ShowInTaskbar = false;
		this.Text = "Designs";
		this.gamePanel2.ResumeLayout(false);
		this.gamePanel1.ResumeLayout(false);
		this.ResumeLayout(false);
		this.PerformLayout();

	}

	#endregion

	private Controls.GamePanel gamePanel1;
	private System.Windows.Forms.ListView lstDesigns;
	private System.Windows.Forms.ComboBox ddlVehicleType;
	private System.Windows.Forms.CheckBox chkForeign;
	private System.Windows.Forms.CheckBox chkHideObsolete;
	private Controls.GamePanel gamePanel2;
	private Controls.DesignReport designReport;
	private Controls.GameButton btnCreate;
	private Controls.GameButton btnCopy;
	private Controls.GameButton btnEdit;
	private Controls.GameButton btnUpgrade;
	private Controls.GameButton btnObsolete;
	private Controls.GameButton btnSpaceSimulator;
	private Controls.GameButton btnGroundSimulator;
	private Controls.GameButton btnClose;
	private System.Windows.Forms.ColumnHeader colTotalCost;
	private System.Windows.Forms.ColumnHeader colDesign;
        private Controls.GameButton btnStrategy;
	private Controls.GameButton btnDelete;
	private Controls.GameButton btnExportAll;
}