namespace FrEee.WinForms.Forms
{
	partial class ResearchForm
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			FrEee.Utility.Resource resource1 = new FrEee.Utility.Resource();
			this.gridQueues = new System.Windows.Forms.DataGridView();
			this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.CurrentLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.NextLevelCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Progress = new FrEee.WinForms.DataGridView.DataGridViewProgressColumn();
			this.Spending = new FrEee.WinForms.DataGridView.DataGridViewProgressColumn();
			this.technologyBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.ddlGroup = new System.Windows.Forms.ComboBox();
			this.lblPoints = new System.Windows.Forms.Label();
			this.txtTechName = new System.Windows.Forms.Label();
			this.lblSpending = new System.Windows.Forms.Label();
			this.sldSpending = new System.Windows.Forms.TrackBar();
			this.lblResults = new System.Windows.Forms.Label();
			this.btnSave = new FrEee.WinForms.Controls.GameButton();
			this.btnDelete = new FrEee.WinForms.Controls.GameButton();
			this.btnDown = new FrEee.WinForms.Controls.GameButton();
			this.btnBottom = new FrEee.WinForms.Controls.GameButton();
			this.btnClear = new FrEee.WinForms.Controls.GameButton();
			this.btnUp = new FrEee.WinForms.Controls.GameButton();
			this.btnTop = new FrEee.WinForms.Controls.GameButton();
			this.gamePanel2 = new FrEee.WinForms.Controls.GamePanel();
			this.lstQueue = new System.Windows.Forms.ListBox();
			this.btnAddToQueue = new FrEee.WinForms.Controls.GameButton();
			this.gamePanel1 = new FrEee.WinForms.Controls.GamePanel();
			this.lstResults = new System.Windows.Forms.ListView();
			this.dataGridViewProgressColumn1 = new FrEee.WinForms.DataGridView.DataGridViewProgressColumn();
			this.btnCancel = new FrEee.WinForms.Controls.GameButton();
			this.resRes = new FrEee.WinForms.Controls.ResourceDisplay();
			((System.ComponentModel.ISupportInitialize)(this.gridQueues)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.technologyBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sldSpending)).BeginInit();
			this.gamePanel2.SuspendLayout();
			this.gamePanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// gridQueues
			// 
			this.gridQueues.AllowUserToAddRows = false;
			this.gridQueues.AllowUserToDeleteRows = false;
			this.gridQueues.AllowUserToOrderColumns = true;
			this.gridQueues.AllowUserToResizeRows = false;
			this.gridQueues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridQueues.AutoGenerateColumns = false;
			this.gridQueues.BackgroundColor = System.Drawing.Color.Black;
			this.gridQueues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridQueues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.CurrentLevel,
            this.NextLevelCost,
            this.Progress,
            this.Spending});
			this.gridQueues.DataSource = this.technologyBindingSource;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridQueues.DefaultCellStyle = dataGridViewCellStyle1;
			this.gridQueues.Location = new System.Drawing.Point(12, 40);
			this.gridQueues.Name = "gridQueues";
			this.gridQueues.ReadOnly = true;
			this.gridQueues.RowHeadersVisible = false;
			this.gridQueues.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Black;
			this.gridQueues.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
			this.gridQueues.RowTemplate.Height = 32;
			this.gridQueues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridQueues.Size = new System.Drawing.Size(508, 509);
			this.gridQueues.TabIndex = 1;
			this.gridQueues.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridQueues_RowEnter);
			// 
			// nameDataGridViewTextBoxColumn
			// 
			this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
			this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
			this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
			this.nameDataGridViewTextBoxColumn.ReadOnly = true;
			this.nameDataGridViewTextBoxColumn.Width = 150;
			// 
			// CurrentLevel
			// 
			this.CurrentLevel.DataPropertyName = "CurrentLevel";
			this.CurrentLevel.HeaderText = "Level";
			this.CurrentLevel.Name = "CurrentLevel";
			this.CurrentLevel.ReadOnly = true;
			this.CurrentLevel.Width = 50;
			// 
			// NextLevelCost
			// 
			this.NextLevelCost.DataPropertyName = "NextLevelCost";
			this.NextLevelCost.HeaderText = "Cost";
			this.NextLevelCost.Name = "NextLevelCost";
			this.NextLevelCost.ReadOnly = true;
			this.NextLevelCost.Width = 75;
			// 
			// Progress
			// 
			this.Progress.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.Progress.DataPropertyName = "Progress";
			this.Progress.HeaderText = "Progress";
			this.Progress.Name = "Progress";
			this.Progress.ProgressDisplayMode = FrEee.WinForms.DataGridView.ProgressDisplayMode.Eta;
			this.Progress.ReadOnly = true;
			// 
			// Spending
			// 
			this.Spending.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.Spending.DataPropertyName = "Spending";
			this.Spending.HeaderText = "Spending";
			this.Spending.Name = "Spending";
			this.Spending.ProgressDisplayMode = FrEee.WinForms.DataGridView.ProgressDisplayMode.Percentage;
			this.Spending.ReadOnly = true;
			// 
			// technologyBindingSource
			// 
			this.technologyBindingSource.AllowNew = false;
			this.technologyBindingSource.DataSource = typeof(FrEee.Game.Objects.Technology.Technology);
			// 
			// ddlGroup
			// 
			this.ddlGroup.DisplayMember = "Text";
			this.ddlGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlGroup.FormattingEnabled = true;
			this.ddlGroup.Location = new System.Drawing.Point(340, 13);
			this.ddlGroup.Name = "ddlGroup";
			this.ddlGroup.Size = new System.Drawing.Size(180, 21);
			this.ddlGroup.TabIndex = 2;
			this.ddlGroup.ValueMember = "GroupName";
			this.ddlGroup.SelectedIndexChanged += new System.EventHandler(this.ddlGroup_SelectedIndexChanged);
			// 
			// lblPoints
			// 
			this.lblPoints.AutoSize = true;
			this.lblPoints.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblPoints.Location = new System.Drawing.Point(13, 13);
			this.lblPoints.Name = "lblPoints";
			this.lblPoints.Size = new System.Drawing.Size(134, 13);
			this.lblPoints.TabIndex = 3;
			this.lblPoints.Text = "Research Points Available:";
			// 
			// txtTechName
			// 
			this.txtTechName.AutoSize = true;
			this.txtTechName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTechName.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.txtTechName.Location = new System.Drawing.Point(538, 14);
			this.txtTechName.Name = "txtTechName";
			this.txtTechName.Size = new System.Drawing.Size(124, 20);
			this.txtTechName.TabIndex = 5;
			this.txtTechName.Text = "(No Technology)";
			// 
			// lblSpending
			// 
			this.lblSpending.AutoSize = true;
			this.lblSpending.Location = new System.Drawing.Point(550, 40);
			this.lblSpending.Name = "lblSpending";
			this.lblSpending.Size = new System.Drawing.Size(52, 13);
			this.lblSpending.TabIndex = 6;
			this.lblSpending.Text = "Spending";
			// 
			// sldSpending
			// 
			this.sldSpending.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sldSpending.LargeChange = 10;
			this.sldSpending.Location = new System.Drawing.Point(553, 57);
			this.sldSpending.Maximum = 100;
			this.sldSpending.Name = "sldSpending";
			this.sldSpending.Size = new System.Drawing.Size(194, 45);
			this.sldSpending.TabIndex = 7;
			this.sldSpending.TickFrequency = 10;
			this.sldSpending.Scroll += new System.EventHandler(this.sldSpending_Scroll);
			// 
			// lblResults
			// 
			this.lblResults.AutoSize = true;
			this.lblResults.Location = new System.Drawing.Point(550, 89);
			this.lblResults.Name = "lblResults";
			this.lblResults.Size = new System.Drawing.Size(90, 13);
			this.lblResults.TabIndex = 8;
			this.lblResults.Text = "Expected Results";
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.BackColor = System.Drawing.Color.Black;
			this.btnSave.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnSave.Location = new System.Drawing.Point(653, 528);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(94, 30);
			this.btnSave.TabIndex = 24;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = false;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDelete.BackColor = System.Drawing.Color.Black;
			this.btnDelete.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnDelete.Location = new System.Drawing.Point(686, 491);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(61, 30);
			this.btnDelete.TabIndex = 23;
			this.btnDelete.Text = "Delete";
			this.btnDelete.UseVisualStyleBackColor = false;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnDown
			// 
			this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDown.BackColor = System.Drawing.Color.Black;
			this.btnDown.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnDown.Location = new System.Drawing.Point(619, 491);
			this.btnDown.Name = "btnDown";
			this.btnDown.Size = new System.Drawing.Size(61, 30);
			this.btnDown.TabIndex = 22;
			this.btnDown.Text = "Down";
			this.btnDown.UseVisualStyleBackColor = false;
			this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
			// 
			// btnBottom
			// 
			this.btnBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBottom.BackColor = System.Drawing.Color.Black;
			this.btnBottom.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnBottom.Location = new System.Drawing.Point(553, 492);
			this.btnBottom.Name = "btnBottom";
			this.btnBottom.Size = new System.Drawing.Size(61, 30);
			this.btnBottom.TabIndex = 21;
			this.btnBottom.Text = "Bottom";
			this.btnBottom.UseVisualStyleBackColor = false;
			this.btnBottom.Click += new System.EventHandler(this.btnBottom_Click);
			// 
			// btnClear
			// 
			this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClear.BackColor = System.Drawing.Color.Black;
			this.btnClear.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnClear.Location = new System.Drawing.Point(686, 457);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(61, 30);
			this.btnClear.TabIndex = 20;
			this.btnClear.Text = "Clear";
			this.btnClear.UseVisualStyleBackColor = false;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// btnUp
			// 
			this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUp.BackColor = System.Drawing.Color.Black;
			this.btnUp.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnUp.Location = new System.Drawing.Point(619, 457);
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(61, 30);
			this.btnUp.TabIndex = 19;
			this.btnUp.Text = "Up";
			this.btnUp.UseVisualStyleBackColor = false;
			this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
			// 
			// btnTop
			// 
			this.btnTop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnTop.BackColor = System.Drawing.Color.Black;
			this.btnTop.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnTop.Location = new System.Drawing.Point(553, 457);
			this.btnTop.Name = "btnTop";
			this.btnTop.Size = new System.Drawing.Size(61, 30);
			this.btnTop.TabIndex = 18;
			this.btnTop.Text = "Top";
			this.btnTop.UseVisualStyleBackColor = false;
			this.btnTop.Click += new System.EventHandler(this.btnTop_Click);
			// 
			// gamePanel2
			// 
			this.gamePanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.gamePanel2.BackColor = System.Drawing.Color.Black;
			this.gamePanel2.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.gamePanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.gamePanel2.Controls.Add(this.lstQueue);
			this.gamePanel2.ForeColor = System.Drawing.Color.White;
			this.gamePanel2.Location = new System.Drawing.Point(553, 325);
			this.gamePanel2.Name = "gamePanel2";
			this.gamePanel2.Padding = new System.Windows.Forms.Padding(3);
			this.gamePanel2.Size = new System.Drawing.Size(194, 126);
			this.gamePanel2.TabIndex = 12;
			// 
			// lstQueue
			// 
			this.lstQueue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstQueue.BackColor = System.Drawing.Color.Black;
			this.lstQueue.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstQueue.ForeColor = System.Drawing.Color.White;
			this.lstQueue.FormattingEnabled = true;
			this.lstQueue.Location = new System.Drawing.Point(3, 3);
			this.lstQueue.Name = "lstQueue";
			this.lstQueue.Size = new System.Drawing.Size(186, 117);
			this.lstQueue.TabIndex = 0;
			// 
			// btnAddToQueue
			// 
			this.btnAddToQueue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddToQueue.BackColor = System.Drawing.Color.Black;
			this.btnAddToQueue.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnAddToQueue.Location = new System.Drawing.Point(553, 287);
			this.btnAddToQueue.Name = "btnAddToQueue";
			this.btnAddToQueue.Size = new System.Drawing.Size(194, 32);
			this.btnAddToQueue.TabIndex = 11;
			this.btnAddToQueue.Text = "Add to Queue";
			this.btnAddToQueue.UseVisualStyleBackColor = false;
			this.btnAddToQueue.Click += new System.EventHandler(this.btnAddToQueue_Click);
			// 
			// gamePanel1
			// 
			this.gamePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gamePanel1.BackColor = System.Drawing.Color.Black;
			this.gamePanel1.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.gamePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.gamePanel1.Controls.Add(this.lstResults);
			this.gamePanel1.ForeColor = System.Drawing.Color.White;
			this.gamePanel1.Location = new System.Drawing.Point(553, 117);
			this.gamePanel1.Name = "gamePanel1";
			this.gamePanel1.Padding = new System.Windows.Forms.Padding(3);
			this.gamePanel1.Size = new System.Drawing.Size(194, 164);
			this.gamePanel1.TabIndex = 10;
			// 
			// lstResults
			// 
			this.lstResults.BackColor = System.Drawing.Color.Black;
			this.lstResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstResults.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstResults.ForeColor = System.Drawing.Color.White;
			this.lstResults.Location = new System.Drawing.Point(3, 3);
			this.lstResults.Name = "lstResults";
			this.lstResults.Size = new System.Drawing.Size(186, 156);
			this.lstResults.TabIndex = 10;
			this.lstResults.UseCompatibleStateImageBehavior = false;
			this.lstResults.View = System.Windows.Forms.View.List;
			// 
			// dataGridViewProgressColumn1
			// 
			this.dataGridViewProgressColumn1.BarColor = System.Drawing.Color.Silver;
			this.dataGridViewProgressColumn1.DataPropertyName = "Progress";
			this.dataGridViewProgressColumn1.HeaderText = "Progress";
			this.dataGridViewProgressColumn1.Name = "dataGridViewProgressColumn1";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.BackColor = System.Drawing.Color.Black;
			this.btnCancel.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnCancel.Location = new System.Drawing.Point(553, 528);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(94, 30);
			this.btnCancel.TabIndex = 25;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// resRes
			// 
			this.resRes.Amount = 0;
			this.resRes.BackColor = System.Drawing.Color.Black;
			this.resRes.Change = null;
			this.resRes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.resRes.Location = new System.Drawing.Point(150, 9);
			this.resRes.Margin = new System.Windows.Forms.Padding(0);
			this.resRes.Name = "resRes";
			resource1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			resource1.IsGlobal = false;
			resource1.IsLocal = false;
			resource1.Name = "Research";
			resource1.PictureName = "Resource4";
			this.resRes.Resource = resource1;
			this.resRes.ResourceName = "Research";
			this.resRes.Size = new System.Drawing.Size(88, 20);
			this.resRes.TabIndex = 26;
			// 
			// ResearchForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(759, 561);
			this.Controls.Add(this.resRes);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnDown);
			this.Controls.Add(this.btnBottom);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnUp);
			this.Controls.Add(this.btnTop);
			this.Controls.Add(this.gamePanel2);
			this.Controls.Add(this.btnAddToQueue);
			this.Controls.Add(this.gamePanel1);
			this.Controls.Add(this.lblResults);
			this.Controls.Add(this.sldSpending);
			this.Controls.Add(this.lblSpending);
			this.Controls.Add(this.txtTechName);
			this.Controls.Add(this.lblPoints);
			this.Controls.Add(this.ddlGroup);
			this.Controls.Add(this.gridQueues);
			this.ForeColor = System.Drawing.Color.White;
			this.MaximumSize = new System.Drawing.Size(775, 9999);
			this.MinimumSize = new System.Drawing.Size(775, 600);
			this.Name = "ResearchForm";
			this.Text = "Research";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ResearchForm_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.gridQueues)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.technologyBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sldSpending)).EndInit();
			this.gamePanel2.ResumeLayout(false);
			this.gamePanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView gridQueues;
		private System.Windows.Forms.BindingSource technologyBindingSource;
		private DataGridView.DataGridViewProgressColumn dataGridViewProgressColumn1;
		private System.Windows.Forms.ComboBox ddlGroup;
		private System.Windows.Forms.Label lblPoints;
		private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn CurrentLevel;
		private System.Windows.Forms.DataGridViewTextBoxColumn NextLevelCost;
		private DataGridView.DataGridViewProgressColumn Progress;
		private DataGridView.DataGridViewProgressColumn Spending;
		private System.Windows.Forms.Label txtTechName;
		private System.Windows.Forms.Label lblSpending;
		private System.Windows.Forms.TrackBar sldSpending;
		private System.Windows.Forms.Label lblResults;
		private Controls.GamePanel gamePanel1;
		private System.Windows.Forms.ListView lstResults;
		private Controls.GameButton btnAddToQueue;
		private Controls.GamePanel gamePanel2;
		private System.Windows.Forms.ListBox lstQueue;
		private Controls.GameButton btnDelete;
		private Controls.GameButton btnDown;
		private Controls.GameButton btnBottom;
		private Controls.GameButton btnClear;
		private Controls.GameButton btnUp;
		private Controls.GameButton btnTop;
		private Controls.GameButton btnSave;
		private Controls.GameButton btnCancel;
		private Controls.ResourceDisplay resRes;

	}
}