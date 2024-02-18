namespace FrEee.UI.WinForms.Forms;

partial class CultureComparisonForm
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
		this.gridCultures = new System.Windows.Forms.DataGridView();
		this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.productionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.researchDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.intelligenceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tradeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.spaceCombatDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groundCombatDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.happinessDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.maintenanceReductionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.constructionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.repairDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cultureBindingSource = new System.Windows.Forms.BindingSource(this.components);
		this.btnClose = new FrEee.UI.WinForms.Controls.GameButton();
		((System.ComponentModel.ISupportInitialize)(this.gridCultures)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.cultureBindingSource)).BeginInit();
		this.SuspendLayout();
		// 
		// gridCultures
		// 
		this.gridCultures.AllowUserToAddRows = false;
		this.gridCultures.AllowUserToDeleteRows = false;
		this.gridCultures.AllowUserToOrderColumns = true;
		this.gridCultures.AllowUserToResizeRows = false;
		this.gridCultures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.gridCultures.AutoGenerateColumns = false;
		this.gridCultures.BackgroundColor = System.Drawing.Color.Black;
		this.gridCultures.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gridCultures.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.productionDataGridViewTextBoxColumn,
            this.researchDataGridViewTextBoxColumn,
            this.intelligenceDataGridViewTextBoxColumn,
            this.tradeDataGridViewTextBoxColumn,
            this.spaceCombatDataGridViewTextBoxColumn,
            this.groundCombatDataGridViewTextBoxColumn,
            this.happinessDataGridViewTextBoxColumn,
            this.maintenanceReductionDataGridViewTextBoxColumn,
            this.constructionDataGridViewTextBoxColumn,
            this.repairDataGridViewTextBoxColumn});
		this.gridCultures.DataSource = this.cultureBindingSource;
		this.gridCultures.Location = new System.Drawing.Point(13, 13);
		this.gridCultures.Name = "gridCultures";
		this.gridCultures.ReadOnly = true;
		this.gridCultures.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gridCultures.Size = new System.Drawing.Size(680, 395);
		this.gridCultures.TabIndex = 0;
		this.gridCultures.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridCultures_CellFormatting);
		// 
		// nameDataGridViewTextBoxColumn
		// 
		this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
		this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
		this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
		this.nameDataGridViewTextBoxColumn.ReadOnly = true;
		// 
		// productionDataGridViewTextBoxColumn
		// 
		this.productionDataGridViewTextBoxColumn.DataPropertyName = "Production";
		this.productionDataGridViewTextBoxColumn.HeaderText = "Prod";
		this.productionDataGridViewTextBoxColumn.Name = "productionDataGridViewTextBoxColumn";
		this.productionDataGridViewTextBoxColumn.ReadOnly = true;
		// 
		// researchDataGridViewTextBoxColumn
		// 
		this.researchDataGridViewTextBoxColumn.DataPropertyName = "Research";
		this.researchDataGridViewTextBoxColumn.HeaderText = "Res";
		this.researchDataGridViewTextBoxColumn.Name = "researchDataGridViewTextBoxColumn";
		this.researchDataGridViewTextBoxColumn.ReadOnly = true;
		// 
		// intelligenceDataGridViewTextBoxColumn
		// 
		this.intelligenceDataGridViewTextBoxColumn.DataPropertyName = "Intelligence";
		this.intelligenceDataGridViewTextBoxColumn.HeaderText = "Intel";
		this.intelligenceDataGridViewTextBoxColumn.Name = "intelligenceDataGridViewTextBoxColumn";
		this.intelligenceDataGridViewTextBoxColumn.ReadOnly = true;
		// 
		// tradeDataGridViewTextBoxColumn
		// 
		this.tradeDataGridViewTextBoxColumn.DataPropertyName = "Trade";
		this.tradeDataGridViewTextBoxColumn.HeaderText = "Trade";
		this.tradeDataGridViewTextBoxColumn.Name = "tradeDataGridViewTextBoxColumn";
		this.tradeDataGridViewTextBoxColumn.ReadOnly = true;
		// 
		// spaceCombatDataGridViewTextBoxColumn
		// 
		this.spaceCombatDataGridViewTextBoxColumn.DataPropertyName = "SpaceCombat";
		this.spaceCombatDataGridViewTextBoxColumn.HeaderText = "Sp Combat";
		this.spaceCombatDataGridViewTextBoxColumn.Name = "spaceCombatDataGridViewTextBoxColumn";
		this.spaceCombatDataGridViewTextBoxColumn.ReadOnly = true;
		// 
		// groundCombatDataGridViewTextBoxColumn
		// 
		this.groundCombatDataGridViewTextBoxColumn.DataPropertyName = "GroundCombat";
		this.groundCombatDataGridViewTextBoxColumn.HeaderText = "Gr Combat";
		this.groundCombatDataGridViewTextBoxColumn.Name = "groundCombatDataGridViewTextBoxColumn";
		this.groundCombatDataGridViewTextBoxColumn.ReadOnly = true;
		// 
		// happinessDataGridViewTextBoxColumn
		// 
		this.happinessDataGridViewTextBoxColumn.DataPropertyName = "Happiness";
		this.happinessDataGridViewTextBoxColumn.HeaderText = "Happy";
		this.happinessDataGridViewTextBoxColumn.Name = "happinessDataGridViewTextBoxColumn";
		this.happinessDataGridViewTextBoxColumn.ReadOnly = true;
		// 
		// maintenanceReductionDataGridViewTextBoxColumn
		// 
		this.maintenanceReductionDataGridViewTextBoxColumn.DataPropertyName = "MaintenanceReduction";
		this.maintenanceReductionDataGridViewTextBoxColumn.HeaderText = "Maint Red";
		this.maintenanceReductionDataGridViewTextBoxColumn.Name = "maintenanceReductionDataGridViewTextBoxColumn";
		this.maintenanceReductionDataGridViewTextBoxColumn.ReadOnly = true;
		// 
		// constructionDataGridViewTextBoxColumn
		// 
		this.constructionDataGridViewTextBoxColumn.DataPropertyName = "Construction";
		this.constructionDataGridViewTextBoxColumn.HeaderText = "Constr";
		this.constructionDataGridViewTextBoxColumn.Name = "constructionDataGridViewTextBoxColumn";
		this.constructionDataGridViewTextBoxColumn.ReadOnly = true;
		// 
		// repairDataGridViewTextBoxColumn
		// 
		this.repairDataGridViewTextBoxColumn.DataPropertyName = "Repair";
		this.repairDataGridViewTextBoxColumn.HeaderText = "Repair";
		this.repairDataGridViewTextBoxColumn.Name = "repairDataGridViewTextBoxColumn";
		this.repairDataGridViewTextBoxColumn.ReadOnly = true;
		// 
		// cultureBindingSource
		// 
		this.cultureBindingSource.AllowNew = false;
		this.cultureBindingSource.DataSource = typeof(FrEee.Objects.Civilization.Culture);
		// 
		// btnClose
		// 
		this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.btnClose.BackColor = System.Drawing.Color.Black;
		this.btnClose.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnClose.Location = new System.Drawing.Point(618, 414);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(75, 23);
		this.btnClose.TabIndex = 1;
		this.btnClose.Text = "Close";
		this.btnClose.UseVisualStyleBackColor = false;
		this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
		// 
		// CultureComparisonForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.ClientSize = new System.Drawing.Size(705, 450);
		this.Controls.Add(this.btnClose);
		this.Controls.Add(this.gridCultures);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "CultureComparisonForm";
		this.Text = "Culture Comparison";
		((System.ComponentModel.ISupportInitialize)(this.gridCultures)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.cultureBindingSource)).EndInit();
		this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.DataGridView gridCultures;
	private Controls.GameButton btnClose;
	private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
	private System.Windows.Forms.DataGridViewTextBoxColumn productionDataGridViewTextBoxColumn;
	private System.Windows.Forms.DataGridViewTextBoxColumn researchDataGridViewTextBoxColumn;
	private System.Windows.Forms.DataGridViewTextBoxColumn intelligenceDataGridViewTextBoxColumn;
	private System.Windows.Forms.DataGridViewTextBoxColumn tradeDataGridViewTextBoxColumn;
	private System.Windows.Forms.DataGridViewTextBoxColumn spaceCombatDataGridViewTextBoxColumn;
	private System.Windows.Forms.DataGridViewTextBoxColumn groundCombatDataGridViewTextBoxColumn;
	private System.Windows.Forms.DataGridViewTextBoxColumn happinessDataGridViewTextBoxColumn;
	private System.Windows.Forms.DataGridViewTextBoxColumn maintenanceReductionDataGridViewTextBoxColumn;
	private System.Windows.Forms.DataGridViewTextBoxColumn constructionDataGridViewTextBoxColumn;
	private System.Windows.Forms.DataGridViewTextBoxColumn repairDataGridViewTextBoxColumn;
	private System.Windows.Forms.BindingSource cultureBindingSource;
}