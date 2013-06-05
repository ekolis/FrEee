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
			this.gridQueues = new System.Windows.Forms.DataGridView();
			this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.CurrentLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.NextLevelCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Progress = new FrEee.WinForms.DataGridView.DataGridViewProgressColumn();
			this.Spending = new FrEee.WinForms.DataGridView.DataGridViewProgressColumn();
			this.technologyBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.ddlGroup = new System.Windows.Forms.ComboBox();
			this.lblPoints = new System.Windows.Forms.Label();
			this.txtPoints = new System.Windows.Forms.Label();
			this.dataGridViewProgressColumn1 = new FrEee.WinForms.DataGridView.DataGridViewProgressColumn();
			((System.ComponentModel.ISupportInitialize)(this.gridQueues)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.technologyBindingSource)).BeginInit();
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
			this.gridQueues.Size = new System.Drawing.Size(508, 418);
			this.gridQueues.TabIndex = 1;
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
			// txtPoints
			// 
			this.txtPoints.AutoSize = true;
			this.txtPoints.Location = new System.Drawing.Point(154, 13);
			this.txtPoints.Name = "txtPoints";
			this.txtPoints.Size = new System.Drawing.Size(13, 13);
			this.txtPoints.TabIndex = 4;
			this.txtPoints.Text = "0";
			// 
			// dataGridViewProgressColumn1
			// 
			this.dataGridViewProgressColumn1.BarColor = System.Drawing.Color.Silver;
			this.dataGridViewProgressColumn1.DataPropertyName = "Progress";
			this.dataGridViewProgressColumn1.HeaderText = "Progress";
			this.dataGridViewProgressColumn1.Name = "dataGridViewProgressColumn1";
			// 
			// ResearchForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(759, 470);
			this.Controls.Add(this.txtPoints);
			this.Controls.Add(this.lblPoints);
			this.Controls.Add(this.ddlGroup);
			this.Controls.Add(this.gridQueues);
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "ResearchForm";
			this.Text = "Research";
			((System.ComponentModel.ISupportInitialize)(this.gridQueues)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.technologyBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView gridQueues;
		private System.Windows.Forms.BindingSource technologyBindingSource;
		private DataGridView.DataGridViewProgressColumn dataGridViewProgressColumn1;
		private System.Windows.Forms.ComboBox ddlGroup;
		private System.Windows.Forms.Label lblPoints;
		private System.Windows.Forms.Label txtPoints;
		private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn CurrentLevel;
		private System.Windows.Forms.DataGridViewTextBoxColumn NextLevelCost;
		private DataGridView.DataGridViewProgressColumn Progress;
		private DataGridView.DataGridViewProgressColumn Spending;

	}
}