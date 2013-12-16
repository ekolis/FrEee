namespace FrEee.WinForms.Controls
{
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.gameTableLayoutPanel1 = new FrEee.WinForms.Controls.GameTableLayoutPanel();
			this.pnlConfigsBorder = new FrEee.WinForms.Controls.GamePanel();
			this.pnlConfigs = new System.Windows.Forms.FlowLayoutPanel();
			this.gridData = new System.Windows.Forms.DataGridView();
			this.gamePanel2 = new FrEee.WinForms.Controls.GamePanel();
			this.btnReset = new FrEee.WinForms.Controls.GameButton();
			this.btnColumns = new FrEee.WinForms.Controls.GameButton();
			this.btnDeleteConfig = new FrEee.WinForms.Controls.GameButton();
			this.label17 = new System.Windows.Forms.Label();
			this.txtConfigName = new System.Windows.Forms.TextBox();
			this.gameTableLayoutPanel1.SuspendLayout();
			this.pnlConfigsBorder.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
			this.gamePanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// gameTableLayoutPanel1
			// 
			this.gameTableLayoutPanel1.ColumnCount = 2;
			this.gameTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.gameTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.gameTableLayoutPanel1.Controls.Add(this.pnlConfigsBorder, 0, 1);
			this.gameTableLayoutPanel1.Controls.Add(this.gridData, 1, 1);
			this.gameTableLayoutPanel1.Controls.Add(this.gamePanel2, 1, 0);
			this.gameTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gameTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.gameTableLayoutPanel1.Name = "gameTableLayoutPanel1";
			this.gameTableLayoutPanel1.RowCount = 2;
			this.gameTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.gameTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.gameTableLayoutPanel1.Size = new System.Drawing.Size(690, 362);
			this.gameTableLayoutPanel1.TabIndex = 0;
			// 
			// pnlConfigsBorder
			// 
			this.pnlConfigsBorder.BackColor = System.Drawing.Color.Black;
			this.pnlConfigsBorder.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.pnlConfigsBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlConfigsBorder.Controls.Add(this.pnlConfigs);
			this.pnlConfigsBorder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlConfigsBorder.ForeColor = System.Drawing.Color.White;
			this.pnlConfigsBorder.Location = new System.Drawing.Point(3, 43);
			this.pnlConfigsBorder.Name = "pnlConfigsBorder";
			this.pnlConfigsBorder.Padding = new System.Windows.Forms.Padding(3);
			this.pnlConfigsBorder.Size = new System.Drawing.Size(94, 316);
			this.pnlConfigsBorder.TabIndex = 29;
			// 
			// pnlConfigs
			// 
			this.pnlConfigs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlConfigs.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.pnlConfigs.Location = new System.Drawing.Point(3, 3);
			this.pnlConfigs.Name = "pnlConfigs";
			this.pnlConfigs.Size = new System.Drawing.Size(86, 308);
			this.pnlConfigs.TabIndex = 2;
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
			this.gridData.Location = new System.Drawing.Point(103, 43);
			this.gridData.Name = "gridData";
			this.gridData.ReadOnly = true;
			this.gridData.RowHeadersVisible = false;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
			this.gridData.RowsDefaultCellStyle = dataGridViewCellStyle2;
			this.gridData.RowTemplate.Height = 32;
			this.gridData.RowTemplate.ReadOnly = true;
			this.gridData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridData.Size = new System.Drawing.Size(584, 316);
			this.gridData.TabIndex = 28;
			this.gridData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridData_CellFormatting);
			this.gridData.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridData_CellMouseEnter);
			this.gridData.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridData_ColumnHeaderMouseClick);
			this.gridData.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gridData_DataError);
			this.gridData.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridData_RowEnter);
			this.gridData.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridData_RowLeave);
			// 
			// gamePanel2
			// 
			this.gamePanel2.BackColor = System.Drawing.Color.Black;
			this.gamePanel2.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.gamePanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.gamePanel2.Controls.Add(this.btnReset);
			this.gamePanel2.Controls.Add(this.btnColumns);
			this.gamePanel2.Controls.Add(this.btnDeleteConfig);
			this.gamePanel2.Controls.Add(this.label17);
			this.gamePanel2.Controls.Add(this.txtConfigName);
			this.gamePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gamePanel2.ForeColor = System.Drawing.Color.White;
			this.gamePanel2.Location = new System.Drawing.Point(103, 3);
			this.gamePanel2.Name = "gamePanel2";
			this.gamePanel2.Padding = new System.Windows.Forms.Padding(3);
			this.gamePanel2.Size = new System.Drawing.Size(584, 34);
			this.gamePanel2.TabIndex = 27;
			// 
			// btnReset
			// 
			this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnReset.BackColor = System.Drawing.Color.Black;
			this.btnReset.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnReset.Location = new System.Drawing.Point(501, 6);
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
			this.btnDeleteConfig.Location = new System.Drawing.Point(420, 6);
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
			// 
			// GameGridView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.Controls.Add(this.gameTableLayoutPanel1);
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "GameGridView";
			this.Size = new System.Drawing.Size(690, 362);
			this.gameTableLayoutPanel1.ResumeLayout(false);
			this.pnlConfigsBorder.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
			this.gamePanel2.ResumeLayout(false);
			this.gamePanel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private GameTableLayoutPanel gameTableLayoutPanel1;
		private GamePanel gamePanel2;
		private GameButton btnReset;
		private GameButton btnColumns;
		private GameButton btnDeleteConfig;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.TextBox txtConfigName;
		private System.Windows.Forms.DataGridView gridData;
		private GamePanel pnlConfigsBorder;
		private System.Windows.Forms.FlowLayoutPanel pnlConfigs;
	}
}
