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
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.RateMinerals = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.RateOrganics = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.RateRadioactives = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.FirstItemIcon = new System.Windows.Forms.DataGridViewImageColumn();
			this.FirstItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.FirstItemEta = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Eta = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.CargoStorageFree = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.CargoStorageFreeInSector = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.FacilitySlotsFree = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.galaxyView = new FrEee.WinForms.Controls.GalaxyView();
			this.IconCol = new System.Windows.Forms.DataGridViewImageColumn();
			this.isSpaceYardQueueDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.isColonyQueueDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.constructionQueueBindingSource = new System.Windows.Forms.BindingSource(this.components);
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
			this.gridQueues.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.gridQueues.BackgroundColor = System.Drawing.Color.Black;
			this.gridQueues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridQueues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IconCol,
            this.NameColumn,
            this.isSpaceYardQueueDataGridViewCheckBoxColumn,
            this.isColonyQueueDataGridViewCheckBoxColumn,
            this.RateMinerals,
            this.RateOrganics,
            this.RateRadioactives,
            this.FirstItemIcon,
            this.FirstItemName,
            this.FirstItemEta,
            this.Eta,
            this.CargoStorageFree,
            this.CargoStorageFreeInSector,
            this.FacilitySlotsFree});
			this.gridQueues.DataSource = this.constructionQueueBindingSource;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridQueues.DefaultCellStyle = dataGridViewCellStyle1;
			this.gridQueues.Location = new System.Drawing.Point(13, 126);
			this.gridQueues.Name = "gridQueues";
			this.gridQueues.ReadOnly = true;
			this.gridQueues.RowHeadersVisible = false;
			this.gridQueues.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Black;
			this.gridQueues.RowTemplate.Height = 32;
			this.gridQueues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridQueues.Size = new System.Drawing.Size(822, 356);
			this.gridQueues.TabIndex = 0;
			this.gridQueues.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridQueues_CellDoubleClick);
			this.gridQueues.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridQueues_CellFormatting);
			this.gridQueues.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.gridQueues_RowStateChanged);
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
			// NameColumn
			// 
			this.NameColumn.DataPropertyName = "Name";
			this.NameColumn.HeaderText = "Name";
			this.NameColumn.Name = "NameColumn";
			this.NameColumn.ReadOnly = true;
			this.NameColumn.Width = 60;
			// 
			// RateMinerals
			// 
			this.RateMinerals.DataPropertyName = "RateMinerals";
			this.RateMinerals.HeaderText = "Min";
			this.RateMinerals.Name = "RateMinerals";
			this.RateMinerals.ReadOnly = true;
			this.RateMinerals.Width = 49;
			// 
			// RateOrganics
			// 
			this.RateOrganics.DataPropertyName = "RateOrganics";
			this.RateOrganics.HeaderText = "Org";
			this.RateOrganics.Name = "RateOrganics";
			this.RateOrganics.ReadOnly = true;
			this.RateOrganics.Width = 49;
			// 
			// RateRadioactives
			// 
			this.RateRadioactives.DataPropertyName = "RateRadioactives";
			this.RateRadioactives.HeaderText = "Rad";
			this.RateRadioactives.Name = "RateRadioactives";
			this.RateRadioactives.ReadOnly = true;
			this.RateRadioactives.Width = 52;
			// 
			// FirstItemIcon
			// 
			this.FirstItemIcon.DataPropertyName = "FirstItemIcon";
			this.FirstItemIcon.HeaderText = "Icon";
			this.FirstItemIcon.MinimumWidth = 32;
			this.FirstItemIcon.Name = "FirstItemIcon";
			this.FirstItemIcon.ReadOnly = true;
			this.FirstItemIcon.Width = 34;
			// 
			// FirstItemName
			// 
			this.FirstItemName.DataPropertyName = "FirstItemName";
			this.FirstItemName.HeaderText = "Building";
			this.FirstItemName.Name = "FirstItemName";
			this.FirstItemName.ReadOnly = true;
			this.FirstItemName.Width = 69;
			// 
			// FirstItemEta
			// 
			this.FirstItemEta.DataPropertyName = "FirstItemEta";
			this.FirstItemEta.HeaderText = "1st ETA";
			this.FirstItemEta.Name = "FirstItemEta";
			this.FirstItemEta.ReadOnly = true;
			this.FirstItemEta.Width = 70;
			// 
			// Eta
			// 
			this.Eta.DataPropertyName = "Eta";
			this.Eta.HeaderText = "Queue ETA";
			this.Eta.Name = "Eta";
			this.Eta.ReadOnly = true;
			this.Eta.Width = 88;
			// 
			// CargoStorageFree
			// 
			this.CargoStorageFree.DataPropertyName = "CargoStorageFree";
			this.CargoStorageFree.HeaderText = "Cargo Free";
			this.CargoStorageFree.Name = "CargoStorageFree";
			this.CargoStorageFree.ReadOnly = true;
			this.CargoStorageFree.Width = 84;
			// 
			// CargoStorageFreeInSector
			// 
			this.CargoStorageFreeInSector.DataPropertyName = "CargoStorageFreeInSector";
			this.CargoStorageFreeInSector.HeaderText = "In Sector";
			this.CargoStorageFreeInSector.Name = "CargoStorageFreeInSector";
			this.CargoStorageFreeInSector.ReadOnly = true;
			this.CargoStorageFreeInSector.Width = 75;
			// 
			// FacilitySlotsFree
			// 
			this.FacilitySlotsFree.DataPropertyName = "FacilitySlotsFree";
			this.FacilitySlotsFree.HeaderText = "Facil Free";
			this.FacilitySlotsFree.Name = "FacilitySlotsFree";
			this.FacilitySlotsFree.ReadOnly = true;
			this.FacilitySlotsFree.Width = 78;
			// 
			// galaxyView
			// 
			this.galaxyView.BackColor = System.Drawing.Color.Black;
			this.galaxyView.Location = new System.Drawing.Point(13, 13);
			this.galaxyView.Name = "galaxyView";
			this.galaxyView.SelectedStarSystem = null;
			this.galaxyView.Size = new System.Drawing.Size(212, 107);
			this.galaxyView.TabIndex = 1;
			this.galaxyView.Text = "galaxyView1";
			// 
			// IconCol
			// 
			this.IconCol.DataPropertyName = "Icon";
			this.IconCol.HeaderText = "Icon";
			this.IconCol.Name = "IconCol";
			this.IconCol.ReadOnly = true;
			this.IconCol.Width = 34;
			// 
			// isSpaceYardQueueDataGridViewCheckBoxColumn
			// 
			this.isSpaceYardQueueDataGridViewCheckBoxColumn.DataPropertyName = "IsSpaceYardQueue";
			this.isSpaceYardQueueDataGridViewCheckBoxColumn.HeaderText = "SY";
			this.isSpaceYardQueueDataGridViewCheckBoxColumn.Name = "isSpaceYardQueueDataGridViewCheckBoxColumn";
			this.isSpaceYardQueueDataGridViewCheckBoxColumn.ReadOnly = true;
			this.isSpaceYardQueueDataGridViewCheckBoxColumn.Width = 27;
			// 
			// isColonyQueueDataGridViewCheckBoxColumn
			// 
			this.isColonyQueueDataGridViewCheckBoxColumn.DataPropertyName = "IsColonyQueue";
			this.isColonyQueueDataGridViewCheckBoxColumn.HeaderText = "Col";
			this.isColonyQueueDataGridViewCheckBoxColumn.Name = "isColonyQueueDataGridViewCheckBoxColumn";
			this.isColonyQueueDataGridViewCheckBoxColumn.ReadOnly = true;
			this.isColonyQueueDataGridViewCheckBoxColumn.Width = 28;
			// 
			// constructionQueueBindingSource
			// 
			this.constructionQueueBindingSource.AllowNew = false;
			this.constructionQueueBindingSource.DataSource = typeof(FrEee.Game.Objects.Civilization.ConstructionQueue);
			// 
			// ConstructionQueueListForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(847, 494);
			this.Controls.Add(this.galaxyView);
			this.Controls.Add(this.gridQueues);
			this.DoubleBuffered = true;
			this.Name = "ConstructionQueueListForm";
			this.ShowInTaskbar = false;
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
		private Controls.GalaxyView galaxyView;
		private System.Windows.Forms.DataGridViewImageColumn IconCol;
		private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn isSpaceYardQueueDataGridViewCheckBoxColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn isColonyQueueDataGridViewCheckBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn RateMinerals;
		private System.Windows.Forms.DataGridViewTextBoxColumn RateOrganics;
		private System.Windows.Forms.DataGridViewTextBoxColumn RateRadioactives;
		private System.Windows.Forms.DataGridViewImageColumn FirstItemIcon;
		private System.Windows.Forms.DataGridViewTextBoxColumn FirstItemName;
		private System.Windows.Forms.DataGridViewTextBoxColumn FirstItemEta;
		private System.Windows.Forms.DataGridViewTextBoxColumn Eta;
		private System.Windows.Forms.DataGridViewTextBoxColumn CargoStorageFree;
		private System.Windows.Forms.DataGridViewTextBoxColumn CargoStorageFreeInSector;
		private System.Windows.Forms.DataGridViewTextBoxColumn FacilitySlotsFree;
	}
}