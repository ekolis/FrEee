namespace FrEee.WinForms.Forms;

partial class TechTreeForm
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
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.gamePanel2 = new FrEee.WinForms.Controls.GamePanel();
		this.lstUnlocks = new System.Windows.Forms.ListView();
		this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
		this.gamePanel1 = new FrEee.WinForms.Controls.GamePanel();
		this.lstRequired = new System.Windows.Forms.ListView();
		this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
		this.ddlItems = new System.Windows.Forms.ComboBox();
		this.lblPrereqs = new System.Windows.Forms.Label();
		this.lblDetailsHeader = new System.Windows.Forms.Label();
		this.lblUnlocks = new System.Windows.Forms.Label();
		this.ddlType = new System.Windows.Forms.ComboBox();
		this.pnlDetails = new FrEee.WinForms.Controls.GamePanel();
		this.btlReset = new FrEee.WinForms.Controls.GameButton();
		this.tableLayoutPanel1.SuspendLayout();
		this.gamePanel2.SuspendLayout();
		this.gamePanel1.SuspendLayout();
		this.SuspendLayout();
		// 
		// tableLayoutPanel1
		// 
		this.tableLayoutPanel1.ColumnCount = 3;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
		this.tableLayoutPanel1.Controls.Add(this.gamePanel2, 2, 2);
		this.tableLayoutPanel1.Controls.Add(this.gamePanel1, 0, 2);
		this.tableLayoutPanel1.Controls.Add(this.ddlItems, 1, 0);
		this.tableLayoutPanel1.Controls.Add(this.lblPrereqs, 0, 1);
		this.tableLayoutPanel1.Controls.Add(this.lblDetailsHeader, 1, 1);
		this.tableLayoutPanel1.Controls.Add(this.lblUnlocks, 2, 1);
		this.tableLayoutPanel1.Controls.Add(this.ddlType, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.pnlDetails, 1, 2);
		this.tableLayoutPanel1.Controls.Add(this.btlReset, 2, 0);
		this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.RowCount = 3;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
		this.tableLayoutPanel1.Size = new System.Drawing.Size(953, 634);
		this.tableLayoutPanel1.TabIndex = 0;
		// 
		// gamePanel2
		// 
		this.gamePanel2.BackColor = System.Drawing.Color.Black;
		this.gamePanel2.BorderColor = System.Drawing.Color.CornflowerBlue;
		this.gamePanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.gamePanel2.Controls.Add(this.lstUnlocks);
		this.gamePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gamePanel2.ForeColor = System.Drawing.Color.White;
		this.gamePanel2.Location = new System.Drawing.Point(756, 55);
		this.gamePanel2.Name = "gamePanel2";
		this.gamePanel2.Padding = new System.Windows.Forms.Padding(3);
		this.gamePanel2.Size = new System.Drawing.Size(194, 576);
		this.gamePanel2.TabIndex = 8;
		// 
		// lstUnlocks
		// 
		this.lstUnlocks.BackColor = System.Drawing.Color.Black;
		this.lstUnlocks.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.lstUnlocks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
		this.lstUnlocks.Dock = System.Windows.Forms.DockStyle.Fill;
		this.lstUnlocks.ForeColor = System.Drawing.Color.White;
		this.lstUnlocks.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
		this.lstUnlocks.Location = new System.Drawing.Point(3, 3);
		this.lstUnlocks.Name = "lstUnlocks";
		this.lstUnlocks.Size = new System.Drawing.Size(186, 568);
		this.lstUnlocks.TabIndex = 3;
		this.lstUnlocks.UseCompatibleStateImageBehavior = false;
		this.lstUnlocks.View = System.Windows.Forms.View.Details;
		this.lstUnlocks.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstUnlocks_MouseDoubleClick);
		// 
		// columnHeader2
		// 
		this.columnHeader2.Width = 186;
		// 
		// gamePanel1
		// 
		this.gamePanel1.BackColor = System.Drawing.Color.Black;
		this.gamePanel1.BorderColor = System.Drawing.Color.CornflowerBlue;
		this.gamePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.gamePanel1.Controls.Add(this.lstRequired);
		this.gamePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gamePanel1.ForeColor = System.Drawing.Color.White;
		this.gamePanel1.Location = new System.Drawing.Point(3, 55);
		this.gamePanel1.Name = "gamePanel1";
		this.gamePanel1.Padding = new System.Windows.Forms.Padding(3);
		this.gamePanel1.Size = new System.Drawing.Size(194, 576);
		this.gamePanel1.TabIndex = 7;
		// 
		// lstRequired
		// 
		this.lstRequired.BackColor = System.Drawing.Color.Black;
		this.lstRequired.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.lstRequired.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
		this.lstRequired.Dock = System.Windows.Forms.DockStyle.Fill;
		this.lstRequired.ForeColor = System.Drawing.Color.White;
		this.lstRequired.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
		this.lstRequired.Location = new System.Drawing.Point(3, 3);
		this.lstRequired.Name = "lstRequired";
		this.lstRequired.Size = new System.Drawing.Size(186, 568);
		this.lstRequired.TabIndex = 2;
		this.lstRequired.UseCompatibleStateImageBehavior = false;
		this.lstRequired.View = System.Windows.Forms.View.Details;
		this.lstRequired.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstRequired_MouseDoubleClick);
		// 
		// columnHeader1
		// 
		this.columnHeader1.Width = 186;
		// 
		// ddlItems
		// 
		this.ddlItems.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ddlItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.ddlItems.FormattingEnabled = true;
		this.ddlItems.Location = new System.Drawing.Point(203, 3);
		this.ddlItems.Name = "ddlItems";
		this.ddlItems.Size = new System.Drawing.Size(547, 21);
		this.ddlItems.TabIndex = 4;
		this.ddlItems.SelectedIndexChanged += new System.EventHandler(this.ddlItems_SelectedIndexChanged);
		// 
		// lblPrereqs
		// 
		this.lblPrereqs.AutoSize = true;
		this.lblPrereqs.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblPrereqs.Location = new System.Drawing.Point(3, 32);
		this.lblPrereqs.Name = "lblPrereqs";
		this.lblPrereqs.Size = new System.Drawing.Size(67, 13);
		this.lblPrereqs.TabIndex = 0;
		this.lblPrereqs.Text = "Prerequisites";
		// 
		// lblDetailsHeader
		// 
		this.lblDetailsHeader.AutoSize = true;
		this.lblDetailsHeader.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblDetailsHeader.Location = new System.Drawing.Point(203, 32);
		this.lblDetailsHeader.Name = "lblDetailsHeader";
		this.lblDetailsHeader.Size = new System.Drawing.Size(39, 13);
		this.lblDetailsHeader.TabIndex = 1;
		this.lblDetailsHeader.Text = "Details";
		// 
		// lblUnlocks
		// 
		this.lblUnlocks.AutoSize = true;
		this.lblUnlocks.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblUnlocks.Location = new System.Drawing.Point(756, 32);
		this.lblUnlocks.Name = "lblUnlocks";
		this.lblUnlocks.Size = new System.Drawing.Size(46, 13);
		this.lblUnlocks.TabIndex = 2;
		this.lblUnlocks.Text = "Unlocks";
		// 
		// ddlType
		// 
		this.ddlType.DisplayMember = "Name";
		this.ddlType.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ddlType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.ddlType.FormattingEnabled = true;
		this.ddlType.Location = new System.Drawing.Point(3, 3);
		this.ddlType.Name = "ddlType";
		this.ddlType.Size = new System.Drawing.Size(194, 21);
		this.ddlType.TabIndex = 3;
		this.ddlType.SelectedIndexChanged += new System.EventHandler(this.ddlType_SelectedIndexChanged);
		// 
		// pnlDetails
		// 
		this.pnlDetails.BackColor = System.Drawing.Color.Black;
		this.pnlDetails.BorderColor = System.Drawing.Color.CornflowerBlue;
		this.pnlDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pnlDetails.Dock = System.Windows.Forms.DockStyle.Fill;
		this.pnlDetails.ForeColor = System.Drawing.Color.White;
		this.pnlDetails.Location = new System.Drawing.Point(203, 55);
		this.pnlDetails.Name = "pnlDetails";
		this.pnlDetails.Padding = new System.Windows.Forms.Padding(3);
		this.pnlDetails.Size = new System.Drawing.Size(547, 576);
		this.pnlDetails.TabIndex = 6;
		// 
		// btlReset
		// 
		this.btlReset.BackColor = System.Drawing.Color.Black;
		this.btlReset.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btlReset.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btlReset.Location = new System.Drawing.Point(756, 3);
		this.btlReset.Name = "btlReset";
		this.btlReset.Size = new System.Drawing.Size(194, 26);
		this.btlReset.TabIndex = 9;
		this.btlReset.Text = "Reset";
		this.btlReset.UseVisualStyleBackColor = false;
		this.btlReset.Click += new System.EventHandler(this.btlReset_Click);
		// 
		// TechTreeForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.ClientSize = new System.Drawing.Size(953, 634);
		this.Controls.Add(this.tableLayoutPanel1);
		this.Name = "TechTreeForm";
		this.Text = "Tech Tree";
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.PerformLayout();
		this.gamePanel2.ResumeLayout(false);
		this.gamePanel1.ResumeLayout(false);
		this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private System.Windows.Forms.Label lblPrereqs;
	private System.Windows.Forms.Label lblDetailsHeader;
	private System.Windows.Forms.Label lblUnlocks;
	private System.Windows.Forms.ComboBox ddlType;
	private System.Windows.Forms.ComboBox ddlItems;
	private Controls.GamePanel pnlDetails;
	private Controls.GamePanel gamePanel1;
	private System.Windows.Forms.ListView lstRequired;
	private Controls.GamePanel gamePanel2;
	private System.Windows.Forms.ColumnHeader columnHeader1;
	private System.Windows.Forms.ListView lstUnlocks;
	private System.Windows.Forms.ColumnHeader columnHeader2;
	private Controls.GameButton btlReset;
}