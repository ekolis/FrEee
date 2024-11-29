using FrEee.Processes.Construction;

namespace FrEee.UI.WinForms.Forms;

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
		this.IconCol = new System.Windows.Forms.DataGridViewImageColumn();
		this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.isSpaceYardQueueDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.isColonyQueueDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
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
		this.constructionQueueBindingSource = new System.Windows.Forms.BindingSource(this.components);
		this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.gamePanel1 = new FrEee.UI.WinForms.Controls.GamePanel();
		this.galaxyView = new FrEee.UI.WinForms.Controls.GalaxyView();
		this.gamePanel2 = new FrEee.UI.WinForms.Controls.GamePanel();
		this.starSystemView = new FrEee.UI.WinForms.Controls.StarSystemView();
		((System.ComponentModel.ISupportInitialize)(this.gridQueues)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.constructionQueueBindingSource)).BeginInit();
		this.gamePanel1.SuspendLayout();
		this.gamePanel2.SuspendLayout();
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
		this.gridQueues.Location = new System.Drawing.Point(13, 198);
		this.gridQueues.Name = "gridQueues";
		this.gridQueues.ReadOnly = true;
		this.gridQueues.RowHeadersVisible = false;
		this.gridQueues.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Black;
		this.gridQueues.RowTemplate.Height = 32;
		this.gridQueues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gridQueues.Size = new System.Drawing.Size(809, 351);
		this.gridQueues.TabIndex = 0;
		this.gridQueues.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridQueues_CellDoubleClick);
		this.gridQueues.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridQueues_CellFormatting);
		this.gridQueues.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridQueues_CellMouseEnter);
		this.gridQueues.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridQueues_CellMouseLeave);
		this.gridQueues.SelectionChanged += new System.EventHandler(this.gridQueues_SelectionChanged);
		// 
		// IconCol
		// 
		this.IconCol.DataPropertyName = "Icon";
		this.IconCol.HeaderText = "Icon";
		this.IconCol.Name = "IconCol";
		this.IconCol.ReadOnly = true;
		this.IconCol.Width = 34;
		// 
		// NameColumn
		// 
		this.NameColumn.DataPropertyName = "Name";
		this.NameColumn.HeaderText = "Name";
		this.NameColumn.Name = "NameColumn";
		this.NameColumn.ReadOnly = true;
		this.NameColumn.Width = 60;
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
		// constructionQueueBindingSource
		// 
		this.constructionQueueBindingSource.AllowNew = false;
		this.constructionQueueBindingSource.DataSource = typeof(ConstructionQueue);
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
		// gamePanel1
		// 
		this.gamePanel1.BackColor = System.Drawing.Color.Black;
		this.gamePanel1.BorderColor = System.Drawing.Color.CornflowerBlue;
		this.gamePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.gamePanel1.Controls.Add(this.galaxyView);
		this.gamePanel1.ForeColor = System.Drawing.Color.White;
		this.gamePanel1.Location = new System.Drawing.Point(13, 12);
		this.gamePanel1.Name = "gamePanel1";
		this.gamePanel1.Padding = new System.Windows.Forms.Padding(3);
		this.gamePanel1.Size = new System.Drawing.Size(240, 180);
		this.gamePanel1.TabIndex = 3;
		// 
		// galaxyView
		// 
		this.galaxyView.BackColor = System.Drawing.Color.Black;
		this.galaxyView.Dock = System.Windows.Forms.DockStyle.Fill;
		this.galaxyView.Location = new System.Drawing.Point(3, 3);
		this.galaxyView.Name = "galaxyView";
		this.galaxyView.SelectedStarSystem = null;
		this.galaxyView.Size = new System.Drawing.Size(232, 172);
		this.galaxyView.TabIndex = 2;
		this.galaxyView.Text = "galaxyView1";
		// 
		// gamePanel2
		// 
		this.gamePanel2.BackColor = System.Drawing.Color.Black;
		this.gamePanel2.BorderColor = System.Drawing.Color.CornflowerBlue;
		this.gamePanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.gamePanel2.Controls.Add(this.starSystemView);
		this.gamePanel2.ForeColor = System.Drawing.Color.White;
		this.gamePanel2.Location = new System.Drawing.Point(259, 12);
		this.gamePanel2.Name = "gamePanel2";
		this.gamePanel2.Padding = new System.Windows.Forms.Padding(3);
		this.gamePanel2.Size = new System.Drawing.Size(180, 180);
		this.gamePanel2.TabIndex = 4;
		// 
		// starSystemView
		// 
		this.starSystemView.BackColor = System.Drawing.Color.Black;
		this.starSystemView.Dock = System.Windows.Forms.DockStyle.Fill;
		this.starSystemView.DrawText = false;
		this.starSystemView.Location = new System.Drawing.Point(3, 3);
		this.starSystemView.Name = "starSystemView";
		this.starSystemView.SelectedSector = null;
		this.starSystemView.SelectedSpaceObject = null;
		this.starSystemView.Size = new System.Drawing.Size(172, 172);
		this.starSystemView.StarSystem = null;
		this.starSystemView.TabIndex = 3;
		this.starSystemView.Text = "starSystemView1";
		// 
		// ConstructionQueueListForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.ClientSize = new System.Drawing.Size(834, 561);
		this.Controls.Add(this.gamePanel2);
		this.Controls.Add(this.gamePanel1);
		this.Controls.Add(this.gridQueues);
		this.DoubleBuffered = true;
		this.Name = "ConstructionQueueListForm";
		this.ShowInTaskbar = false;
		this.Text = "Construction Queues";
		this.Load += new System.EventHandler(this.ConstructionQueueListForm_Load);
		((System.ComponentModel.ISupportInitialize)(this.gridQueues)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.constructionQueueBindingSource)).EndInit();
		this.gamePanel1.ResumeLayout(false);
		this.gamePanel2.ResumeLayout(false);
		this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.DataGridView gridQueues;
	private System.Windows.Forms.BindingSource constructionQueueBindingSource;
	private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
	private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
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
	private Controls.GamePanel gamePanel1;
	private Controls.GalaxyView galaxyView;
	private Controls.GamePanel gamePanel2;
	private Controls.StarSystemView starSystemView;
}