namespace FrEee.UI.WinForms.Controls;

partial class GameGridView
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
		this.components = new System.ComponentModel.Container();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		this.gridData = new System.Windows.Forms.DataGridView();
		this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.noFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.exactlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.differentFromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.atLeastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.atMostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.clearAllFiltersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.pnlConfigsBorder = new FrEee.UI.WinForms.Controls.GamePanel();
		this.pnlConfigs = new System.Windows.Forms.FlowLayoutPanel();
		this.pnlConfigEdit = new FrEee.UI.WinForms.Controls.GamePanel();
		this.btnReset = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnColumns = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnDeleteConfig = new FrEee.UI.WinForms.Controls.GameButton();
		this.label17 = new System.Windows.Forms.Label();
		this.txtConfigName = new System.Windows.Forms.TextBox();
		((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
		this.contextMenu.SuspendLayout();
		this.pnlConfigsBorder.SuspendLayout();
		this.pnlConfigEdit.SuspendLayout();
		this.SuspendLayout();
		// 
		// gridData
		// 
		this.gridData.AllowUserToAddRows = false;
		this.gridData.AllowUserToDeleteRows = false;
		this.gridData.AllowUserToOrderColumns = true;
		this.gridData.AllowUserToResizeRows = false;
		this.gridData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.gridData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
		this.gridData.BackgroundColor = System.Drawing.Color.Black;
		this.gridData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gridData.ContextMenuStrip = this.contextMenu;
		this.gridData.Location = new System.Drawing.Point(103, 43);
		this.gridData.Name = "gridData";
		this.gridData.ReadOnly = true;
		this.gridData.RowHeadersVisible = false;
		dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
		this.gridData.RowsDefaultCellStyle = dataGridViewCellStyle1;
		this.gridData.RowTemplate.Height = 32;
		this.gridData.RowTemplate.ReadOnly = true;
		this.gridData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gridData.Size = new System.Drawing.Size(584, 316);
		this.gridData.TabIndex = 31;
		this.gridData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridData_CellFormatting);
		this.gridData.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridData_CellMouseDown);
		this.gridData.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridData_CellMouseEnter);
		this.gridData.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridData_ColumnHeaderMouseClick);
		this.gridData.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gridData_DataError);
		this.gridData.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridData_RowEnter);
		this.gridData.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridData_RowLeave);
		this.gridData.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gridData_MouseDoubleClick);
		// 
		// contextMenu
		// 
		this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noFilterToolStripMenuItem,
            this.exactlyToolStripMenuItem,
            this.differentFromToolStripMenuItem,
            this.atLeastToolStripMenuItem,
            this.atMostToolStripMenuItem,
            this.clearAllFiltersToolStripMenuItem});
		this.contextMenu.Name = "contextMenu";
		this.contextMenu.Size = new System.Drawing.Size(153, 136);
		// 
		// noFilterToolStripMenuItem
		// 
		this.noFilterToolStripMenuItem.Name = "noFilterToolStripMenuItem";
		this.noFilterToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
		this.noFilterToolStripMenuItem.Text = "&No Filter";
		this.noFilterToolStripMenuItem.Click += new System.EventHandler(this.noFilterToolStripMenuItem_Click);
		// 
		// exactlyToolStripMenuItem
		// 
		this.exactlyToolStripMenuItem.Name = "exactlyToolStripMenuItem";
		this.exactlyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
		this.exactlyToolStripMenuItem.Text = "&Exactly";
		this.exactlyToolStripMenuItem.Click += new System.EventHandler(this.exactlyToolStripMenuItem_Click);
		// 
		// differentFromToolStripMenuItem
		// 
		this.differentFromToolStripMenuItem.Name = "differentFromToolStripMenuItem";
		this.differentFromToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
		this.differentFromToolStripMenuItem.Text = "&Different From";
		this.differentFromToolStripMenuItem.Click += new System.EventHandler(this.differentFromToolStripMenuItem_Click);
		// 
		// atLeastToolStripMenuItem
		// 
		this.atLeastToolStripMenuItem.Name = "atLeastToolStripMenuItem";
		this.atLeastToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
		this.atLeastToolStripMenuItem.Text = "At &Least";
		this.atLeastToolStripMenuItem.Click += new System.EventHandler(this.atLeastToolStripMenuItem_Click);
		// 
		// atMostToolStripMenuItem
		// 
		this.atMostToolStripMenuItem.Name = "atMostToolStripMenuItem";
		this.atMostToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
		this.atMostToolStripMenuItem.Text = "At &Most";
		this.atMostToolStripMenuItem.Click += new System.EventHandler(this.atMostToolStripMenuItem_Click);
		// 
		// clearAllFiltersToolStripMenuItem
		// 
		this.clearAllFiltersToolStripMenuItem.Name = "clearAllFiltersToolStripMenuItem";
		this.clearAllFiltersToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
		this.clearAllFiltersToolStripMenuItem.Text = "&Clear All Filters";
		this.clearAllFiltersToolStripMenuItem.Click += new System.EventHandler(this.clearAllFiltersToolStripMenuItem_Click);
		// 
		// pnlConfigsBorder
		// 
		this.pnlConfigsBorder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
		this.pnlConfigsBorder.BackColor = System.Drawing.Color.Black;
		this.pnlConfigsBorder.BorderColor = System.Drawing.Color.CornflowerBlue;
		this.pnlConfigsBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pnlConfigsBorder.Controls.Add(this.pnlConfigs);
		this.pnlConfigsBorder.ForeColor = System.Drawing.Color.White;
		this.pnlConfigsBorder.Location = new System.Drawing.Point(3, 43);
		this.pnlConfigsBorder.Name = "pnlConfigsBorder";
		this.pnlConfigsBorder.Padding = new System.Windows.Forms.Padding(3);
		this.pnlConfigsBorder.Size = new System.Drawing.Size(94, 316);
		this.pnlConfigsBorder.TabIndex = 32;
		// 
		// pnlConfigs
		// 
		this.pnlConfigs.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
		this.pnlConfigs.Location = new System.Drawing.Point(3, 3);
		this.pnlConfigs.Name = "pnlConfigs";
		this.pnlConfigs.Size = new System.Drawing.Size(86, 308);
		this.pnlConfigs.TabIndex = 2;
		// 
		// pnlConfigEdit
		// 
		this.pnlConfigEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.pnlConfigEdit.BackColor = System.Drawing.Color.Black;
		this.pnlConfigEdit.BorderColor = System.Drawing.Color.CornflowerBlue;
		this.pnlConfigEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pnlConfigEdit.Controls.Add(this.btnReset);
		this.pnlConfigEdit.Controls.Add(this.btnColumns);
		this.pnlConfigEdit.Controls.Add(this.btnDeleteConfig);
		this.pnlConfigEdit.Controls.Add(this.label17);
		this.pnlConfigEdit.Controls.Add(this.txtConfigName);
		this.pnlConfigEdit.ForeColor = System.Drawing.Color.White;
		this.pnlConfigEdit.Location = new System.Drawing.Point(103, 3);
		this.pnlConfigEdit.Name = "pnlConfigEdit";
		this.pnlConfigEdit.Padding = new System.Windows.Forms.Padding(3);
		this.pnlConfigEdit.Size = new System.Drawing.Size(584, 34);
		this.pnlConfigEdit.TabIndex = 30;
		// 
		// btnReset
		// 
		this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.btnReset.BackColor = System.Drawing.Color.Black;
		this.btnReset.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnReset.Location = new System.Drawing.Point(395, 6);
		this.btnReset.Name = "btnReset";
		this.btnReset.Size = new System.Drawing.Size(75, 19);
		this.btnReset.TabIndex = 30;
		this.btnReset.Text = "Reset";
		this.btnReset.UseVisualStyleBackColor = false;
		this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
		// 
		// btnColumns
		// 
		this.btnColumns.BackColor = System.Drawing.Color.Black;
		this.btnColumns.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnColumns.Location = new System.Drawing.Point(186, 7);
		this.btnColumns.Name = "btnColumns";
		this.btnColumns.Size = new System.Drawing.Size(75, 19);
		this.btnColumns.TabIndex = 29;
		this.btnColumns.Text = "Columns";
		this.btnColumns.UseVisualStyleBackColor = false;
		this.btnColumns.Click += new System.EventHandler(this.btnColumns_Click);
		// 
		// btnDeleteConfig
		// 
		this.btnDeleteConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.btnDeleteConfig.BackColor = System.Drawing.Color.Black;
		this.btnDeleteConfig.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnDeleteConfig.Location = new System.Drawing.Point(314, 6);
		this.btnDeleteConfig.Name = "btnDeleteConfig";
		this.btnDeleteConfig.Size = new System.Drawing.Size(75, 19);
		this.btnDeleteConfig.TabIndex = 28;
		this.btnDeleteConfig.Text = "Delete";
		this.btnDeleteConfig.UseVisualStyleBackColor = false;
		this.btnDeleteConfig.Click += new System.EventHandler(this.btnDeleteConfig_Click);
		// 
		// label17
		// 
		this.label17.AutoSize = true;
		this.label17.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label17.Location = new System.Drawing.Point(6, 9);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(68, 13);
		this.label17.TabIndex = 26;
		this.label17.Text = "Config Name";
		// 
		// txtConfigName
		// 
		this.txtConfigName.Location = new System.Drawing.Point(80, 6);
		this.txtConfigName.Name = "txtConfigName";
		this.txtConfigName.Size = new System.Drawing.Size(100, 20);
		this.txtConfigName.TabIndex = 25;
		this.txtConfigName.TextChanged += new System.EventHandler(this.txtConfigName_TextChanged);
		// 
		// GameGridView
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.Controls.Add(this.pnlConfigsBorder);
		this.Controls.Add(this.gridData);
		this.Controls.Add(this.pnlConfigEdit);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "GameGridView";
		this.Size = new System.Drawing.Size(690, 362);
		((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
		this.contextMenu.ResumeLayout(false);
		this.pnlConfigsBorder.ResumeLayout(false);
		this.pnlConfigEdit.ResumeLayout(false);
		this.pnlConfigEdit.PerformLayout();
		this.ResumeLayout(false);

	}

	#endregion

	private GamePanel pnlConfigsBorder;
	private System.Windows.Forms.FlowLayoutPanel pnlConfigs;
	private System.Windows.Forms.DataGridView gridData;
	private GamePanel pnlConfigEdit;
	private GameButton btnReset;
	private GameButton btnColumns;
	private GameButton btnDeleteConfig;
	private System.Windows.Forms.Label label17;
	private System.Windows.Forms.TextBox txtConfigName;
	private System.Windows.Forms.ContextMenuStrip contextMenu;
	private System.Windows.Forms.ToolStripMenuItem exactlyToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem atLeastToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem atMostToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem noFilterToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem differentFromToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem clearAllFiltersToolStripMenuItem;

}
