namespace FrEee.UI.WinForms.Forms;

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
        tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
        gamePanel2 = new Controls.GamePanel();
        lstUnlocks = new System.Windows.Forms.ListView();
        columnHeader2 = new System.Windows.Forms.ColumnHeader();
        gamePanel1 = new Controls.GamePanel();
        lstRequired = new System.Windows.Forms.ListView();
        columnHeader1 = new System.Windows.Forms.ColumnHeader();
        ddlItems = new System.Windows.Forms.ComboBox();
        lblPrereqs = new System.Windows.Forms.Label();
        lblDetailsHeader = new System.Windows.Forms.Label();
        lblUnlocks = new System.Windows.Forms.Label();
        ddlType = new System.Windows.Forms.ComboBox();
        pnlDetails = new Controls.GamePanel();
        btlReset = new Controls.GameButton();
        tableLayoutPanel1.SuspendLayout();
        gamePanel2.SuspendLayout();
        gamePanel1.SuspendLayout();
        SuspendLayout();
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 3;
        tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 233F));
        tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 233F));
        tableLayoutPanel1.Controls.Add(gamePanel2, 2, 2);
        tableLayoutPanel1.Controls.Add(gamePanel1, 0, 2);
        tableLayoutPanel1.Controls.Add(ddlItems, 1, 0);
        tableLayoutPanel1.Controls.Add(lblPrereqs, 0, 1);
        tableLayoutPanel1.Controls.Add(lblDetailsHeader, 1, 1);
        tableLayoutPanel1.Controls.Add(lblUnlocks, 2, 1);
        tableLayoutPanel1.Controls.Add(ddlType, 0, 0);
        tableLayoutPanel1.Controls.Add(pnlDetails, 1, 2);
        tableLayoutPanel1.Controls.Add(btlReset, 2, 0);
        tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
        tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
        tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 3;
        tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
        tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
        tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel1.Size = new System.Drawing.Size(1112, 732);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // gamePanel2
        // 
        gamePanel2.BackColor = System.Drawing.Color.Black;
        gamePanel2.BorderColor = System.Drawing.Color.CornflowerBlue;
        gamePanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        gamePanel2.Controls.Add(lstUnlocks);
        gamePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
        gamePanel2.ForeColor = System.Drawing.Color.White;
        gamePanel2.Location = new System.Drawing.Point(883, 63);
        gamePanel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        gamePanel2.Name = "gamePanel2";
        gamePanel2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
        gamePanel2.Size = new System.Drawing.Size(225, 666);
        gamePanel2.TabIndex = 8;
        // 
        // lstUnlocks
        // 
        lstUnlocks.BackColor = System.Drawing.Color.Black;
        lstUnlocks.BorderStyle = System.Windows.Forms.BorderStyle.None;
        lstUnlocks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { columnHeader2 });
        lstUnlocks.Dock = System.Windows.Forms.DockStyle.Fill;
        lstUnlocks.ForeColor = System.Drawing.Color.White;
        lstUnlocks.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
        lstUnlocks.Location = new System.Drawing.Point(4, 3);
        lstUnlocks.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        lstUnlocks.Name = "lstUnlocks";
        lstUnlocks.Size = new System.Drawing.Size(215, 658);
        lstUnlocks.TabIndex = 3;
        lstUnlocks.UseCompatibleStateImageBehavior = false;
        lstUnlocks.View = System.Windows.Forms.View.Details;
        lstUnlocks.MouseDoubleClick += lstUnlocks_MouseDoubleClick;
        // 
        // columnHeader2
        // 
        columnHeader2.Width = 186;
        // 
        // gamePanel1
        // 
        gamePanel1.BackColor = System.Drawing.Color.Black;
        gamePanel1.BorderColor = System.Drawing.Color.CornflowerBlue;
        gamePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        gamePanel1.Controls.Add(lstRequired);
        gamePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
        gamePanel1.ForeColor = System.Drawing.Color.White;
        gamePanel1.Location = new System.Drawing.Point(4, 63);
        gamePanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        gamePanel1.Name = "gamePanel1";
        gamePanel1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
        gamePanel1.Size = new System.Drawing.Size(225, 666);
        gamePanel1.TabIndex = 7;
        // 
        // lstRequired
        // 
        lstRequired.BackColor = System.Drawing.Color.Black;
        lstRequired.BorderStyle = System.Windows.Forms.BorderStyle.None;
        lstRequired.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { columnHeader1 });
        lstRequired.Dock = System.Windows.Forms.DockStyle.Fill;
        lstRequired.ForeColor = System.Drawing.Color.White;
        lstRequired.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
        lstRequired.Location = new System.Drawing.Point(4, 3);
        lstRequired.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        lstRequired.Name = "lstRequired";
        lstRequired.Size = new System.Drawing.Size(215, 658);
        lstRequired.TabIndex = 2;
        lstRequired.UseCompatibleStateImageBehavior = false;
        lstRequired.View = System.Windows.Forms.View.Details;
        lstRequired.MouseDoubleClick += lstRequired_MouseDoubleClick;
        // 
        // columnHeader1
        // 
        columnHeader1.Width = 186;
        // 
        // ddlItems
        // 
        ddlItems.Dock = System.Windows.Forms.DockStyle.Fill;
        ddlItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        ddlItems.FormattingEnabled = true;
        ddlItems.Location = new System.Drawing.Point(237, 3);
        ddlItems.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        ddlItems.Name = "ddlItems";
        ddlItems.Size = new System.Drawing.Size(638, 23);
        ddlItems.TabIndex = 4;
        ddlItems.SelectedIndexChanged += ddlItems_SelectedIndexChanged;
        // 
        // lblPrereqs
        // 
        lblPrereqs.AutoSize = true;
        lblPrereqs.ForeColor = System.Drawing.Color.CornflowerBlue;
        lblPrereqs.Location = new System.Drawing.Point(4, 37);
        lblPrereqs.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        lblPrereqs.Name = "lblPrereqs";
        lblPrereqs.Size = new System.Drawing.Size(74, 15);
        lblPrereqs.TabIndex = 0;
        lblPrereqs.Text = "Prerequisites";
        // 
        // lblDetailsHeader
        // 
        lblDetailsHeader.AutoSize = true;
        lblDetailsHeader.ForeColor = System.Drawing.Color.CornflowerBlue;
        lblDetailsHeader.Location = new System.Drawing.Point(237, 37);
        lblDetailsHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        lblDetailsHeader.Name = "lblDetailsHeader";
        lblDetailsHeader.Size = new System.Drawing.Size(42, 15);
        lblDetailsHeader.TabIndex = 1;
        lblDetailsHeader.Text = "Details";
        // 
        // lblUnlocks
        // 
        lblUnlocks.AutoSize = true;
        lblUnlocks.ForeColor = System.Drawing.Color.CornflowerBlue;
        lblUnlocks.Location = new System.Drawing.Point(883, 37);
        lblUnlocks.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        lblUnlocks.Name = "lblUnlocks";
        lblUnlocks.Size = new System.Drawing.Size(49, 15);
        lblUnlocks.TabIndex = 2;
        lblUnlocks.Text = "Unlocks";
        // 
        // ddlType
        // 
        ddlType.DisplayMember = "Name";
        ddlType.Dock = System.Windows.Forms.DockStyle.Fill;
        ddlType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        ddlType.FormattingEnabled = true;
        ddlType.Location = new System.Drawing.Point(4, 3);
        ddlType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        ddlType.Name = "ddlType";
        ddlType.Size = new System.Drawing.Size(225, 23);
        ddlType.TabIndex = 3;
        ddlType.SelectedIndexChanged += ddlType_SelectedIndexChanged;
        // 
        // pnlDetails
        // 
        pnlDetails.BackColor = System.Drawing.Color.Black;
        pnlDetails.BorderColor = System.Drawing.Color.CornflowerBlue;
        pnlDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        pnlDetails.Dock = System.Windows.Forms.DockStyle.Fill;
        pnlDetails.ForeColor = System.Drawing.Color.White;
        pnlDetails.Location = new System.Drawing.Point(237, 63);
        pnlDetails.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        pnlDetails.Name = "pnlDetails";
        pnlDetails.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
        pnlDetails.Size = new System.Drawing.Size(638, 666);
        pnlDetails.TabIndex = 6;
        // 
        // btlReset
        // 
        btlReset.BackColor = System.Drawing.Color.Black;
        btlReset.Dock = System.Windows.Forms.DockStyle.Fill;
        btlReset.ForeColor = System.Drawing.Color.CornflowerBlue;
        btlReset.Location = new System.Drawing.Point(883, 3);
        btlReset.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        btlReset.Name = "btlReset";
        btlReset.Size = new System.Drawing.Size(225, 31);
        btlReset.TabIndex = 9;
        btlReset.Text = "Reset";
        btlReset.UseVisualStyleBackColor = false;
        btlReset.Click += btlReset_Click;
        // 
        // TechTreeForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.Black;
        ClientSize = new System.Drawing.Size(1112, 732);
        Controls.Add(tableLayoutPanel1);
        Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        Name = "TechTreeForm";
        Text = "Tech Tree";
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        gamePanel2.ResumeLayout(false);
        gamePanel1.ResumeLayout(false);
        ResumeLayout(false);
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