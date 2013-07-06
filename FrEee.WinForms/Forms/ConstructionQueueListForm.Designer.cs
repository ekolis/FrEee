namespace FrEee.WinForms.Forms
{
	partial class ConstructionQueueListForm
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
			this.constructionQueueBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.IconColumn = new System.Windows.Forms.DataGridViewImageColumn();
			this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.isSpaceYardQueueDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.isColonyQueueDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.FirstItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.FirstItemEta = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Eta = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.gridQueues)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.constructionQueueBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// gridQueues
			// 
			this.gridQueues.AllowUserToAddRows = false;
			this.gridQueues.AllowUserToDeleteRows = false;
			this.gridQueues.AllowUserToOrderColumns = true;
			this.gridQueues.AllowUserToResizeRows = false;
			this.gridQueues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridQueues.AutoGenerateColumns = false;
			this.gridQueues.BackgroundColor = System.Drawing.Color.Black;
			this.gridQueues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridQueues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IconColumn,
            this.NameColumn,
            this.isSpaceYardQueueDataGridViewCheckBoxColumn,
            this.isColonyQueueDataGridViewCheckBoxColumn,
            this.Rate,
            this.FirstItemName,
            this.FirstItemEta,
            this.Eta});
			this.gridQueues.DataSource = this.constructionQueueBindingSource;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridQueues.DefaultCellStyle = dataGridViewCellStyle1;
			this.gridQueues.Location = new System.Drawing.Point(13, 13);
			this.gridQueues.Name = "gridQueues";
			this.gridQueues.ReadOnly = true;
			this.gridQueues.RowHeadersVisible = false;
			this.gridQueues.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Black;
			this.gridQueues.RowTemplate.Height = 32;
			this.gridQueues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridQueues.Size = new System.Drawing.Size(784, 469);
			this.gridQueues.TabIndex = 0;
			this.gridQueues.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridQueues_CellDoubleClick);
			// 
			// constructionQueueBindingSource
			// 
			this.constructionQueueBindingSource.AllowNew = false;
			this.constructionQueueBindingSource.DataSource = typeof(FrEee.Game.Objects.Civilization.ConstructionQueue);
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.DataPropertyName = "Rate";
			this.dataGridViewTextBoxColumn1.HeaderText = "Rate";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.DataPropertyName = "Rate";
			this.dataGridViewTextBoxColumn2.HeaderText = "Rate";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			// 
			// IconColumn
			// 
			this.IconColumn.DataPropertyName = "Icon";
			this.IconColumn.HeaderText = "Icon";
			this.IconColumn.Name = "Icon";
			this.IconColumn.ReadOnly = true;
			this.IconColumn.Width = 32;
			// 
			// NameColumn
			// 
			this.NameColumn.DataPropertyName = "Name";
			this.NameColumn.HeaderText = "Name";
			this.NameColumn.Name = "NameColumn";
			this.NameColumn.ReadOnly = true;
			// 
			// isSpaceYardQueueDataGridViewCheckBoxColumn
			// 
			this.isSpaceYardQueueDataGridViewCheckBoxColumn.DataPropertyName = "IsSpaceYardQueue";
			this.isSpaceYardQueueDataGridViewCheckBoxColumn.HeaderText = "SY";
			this.isSpaceYardQueueDataGridViewCheckBoxColumn.Name = "isSpaceYardQueueDataGridViewCheckBoxColumn";
			this.isSpaceYardQueueDataGridViewCheckBoxColumn.ReadOnly = true;
			this.isSpaceYardQueueDataGridViewCheckBoxColumn.Width = 32;
			// 
			// isColonyQueueDataGridViewCheckBoxColumn
			// 
			this.isColonyQueueDataGridViewCheckBoxColumn.DataPropertyName = "IsColonyQueue";
			this.isColonyQueueDataGridViewCheckBoxColumn.HeaderText = "Col";
			this.isColonyQueueDataGridViewCheckBoxColumn.Name = "isColonyQueueDataGridViewCheckBoxColumn";
			this.isColonyQueueDataGridViewCheckBoxColumn.ReadOnly = true;
			this.isColonyQueueDataGridViewCheckBoxColumn.Width = 32;
			// 
			// Rate
			// 
			this.Rate.DataPropertyName = "Rate";
			this.Rate.HeaderText = "Rate";
			this.Rate.Name = "Rate";
			this.Rate.ReadOnly = true;
			this.Rate.Width = 200;
			// 
			// FirstItemName
			// 
			this.FirstItemName.DataPropertyName = "FirstItemName";
			this.FirstItemName.HeaderText = "Building";
			this.FirstItemName.Name = "FirstItemName";
			this.FirstItemName.ReadOnly = true;
			// 
			// FirstItemEta
			// 
			this.FirstItemEta.DataPropertyName = "FirstItemEta";
			this.FirstItemEta.HeaderText = "1st ETA";
			this.FirstItemEta.Name = "FirstItemEta";
			this.FirstItemEta.ReadOnly = true;
			// 
			// Eta
			// 
			this.Eta.DataPropertyName = "Eta";
			this.Eta.HeaderText = "Queue ETA";
			this.Eta.Name = "Eta";
			this.Eta.ReadOnly = true;
			// 
			// ConstructionQueueListForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(809, 494);
			this.Controls.Add(this.gridQueues);
			this.DoubleBuffered = true;
			this.Name = "ConstructionQueueListForm";
			this.Text = "Construction Queues";
			this.Load += new System.EventHandler(this.ConstructionQueueListForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridQueues)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.constructionQueueBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView gridQueues;
		private System.Windows.Forms.BindingSource constructionQueueBindingSource;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewImageColumn IconColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn isSpaceYardQueueDataGridViewCheckBoxColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn isColonyQueueDataGridViewCheckBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
		private System.Windows.Forms.DataGridViewTextBoxColumn FirstItemName;
		private System.Windows.Forms.DataGridViewTextBoxColumn FirstItemEta;
		private System.Windows.Forms.DataGridViewTextBoxColumn Eta;
	}
}