using FrEee.Game.Interfaces;
namespace FrEee.WinForms.Forms
{
	partial class HullPickerForm
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
			this.ddlVehicleType = new System.Windows.Forms.ComboBox();
			this.gridHulls = new System.Windows.Forms.DataGridView();
			this.lstAbilities = new System.Windows.Forms.ListView();
			this.Icon = new System.Windows.Forms.DataGridViewImageColumn();
			this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.sizeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.costDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.iHullBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.btnCancel = new FrEee.WinForms.Controls.GameButton();
			this.btnOk = new FrEee.WinForms.Controls.GameButton();
			((System.ComponentModel.ISupportInitialize)(this.gridHulls)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iHullBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// ddlVehicleType
			// 
			this.ddlVehicleType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ddlVehicleType.DisplayMember = "Name";
			this.ddlVehicleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlVehicleType.FormattingEnabled = true;
			this.ddlVehicleType.Location = new System.Drawing.Point(13, 13);
			this.ddlVehicleType.Name = "ddlVehicleType";
			this.ddlVehicleType.Size = new System.Drawing.Size(457, 21);
			this.ddlVehicleType.TabIndex = 0;
			this.ddlVehicleType.ValueMember = "VehicleType";
			this.ddlVehicleType.SelectedIndexChanged += new System.EventHandler(this.ddlVehicleType_SelectedIndexChanged);
			// 
			// gridHulls
			// 
			this.gridHulls.AllowUserToAddRows = false;
			this.gridHulls.AllowUserToDeleteRows = false;
			this.gridHulls.AllowUserToResizeRows = false;
			this.gridHulls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridHulls.AutoGenerateColumns = false;
			this.gridHulls.BackgroundColor = System.Drawing.Color.Black;
			this.gridHulls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridHulls.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Icon,
            this.nameDataGridViewTextBoxColumn,
            this.sizeDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.costDataGridViewTextBoxColumn});
			this.gridHulls.DataSource = this.iHullBindingSource;
			this.gridHulls.Location = new System.Drawing.Point(13, 41);
			this.gridHulls.MultiSelect = false;
			this.gridHulls.Name = "gridHulls";
			this.gridHulls.ReadOnly = true;
			this.gridHulls.RowHeadersVisible = false;
			this.gridHulls.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Black;
			this.gridHulls.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
			this.gridHulls.RowTemplate.Height = 32;
			this.gridHulls.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridHulls.Size = new System.Drawing.Size(457, 319);
			this.gridHulls.TabIndex = 1;
			this.gridHulls.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridHulls_CellDoubleClick);
			this.gridHulls.SelectionChanged += new System.EventHandler(this.gridHulls_SelectionChanged);
			// 
			// lstAbilities
			// 
			this.lstAbilities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstAbilities.BackColor = System.Drawing.Color.Black;
			this.lstAbilities.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstAbilities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.lstAbilities.ForeColor = System.Drawing.Color.White;
			this.lstAbilities.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lstAbilities.Location = new System.Drawing.Point(13, 367);
			this.lstAbilities.Name = "lstAbilities";
			this.lstAbilities.Size = new System.Drawing.Size(457, 108);
			this.lstAbilities.TabIndex = 26;
			this.lstAbilities.UseCompatibleStateImageBehavior = false;
			this.lstAbilities.View = System.Windows.Forms.View.List;
			// 
			// Icon
			// 
			this.Icon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.Icon.DataPropertyName = "Icon";
			this.Icon.HeaderText = "Icon";
			this.Icon.Name = "Icon";
			this.Icon.ReadOnly = true;
			this.Icon.Width = 32;
			// 
			// nameDataGridViewTextBoxColumn
			// 
			this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
			this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
			this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
			this.nameDataGridViewTextBoxColumn.ReadOnly = true;
			this.nameDataGridViewTextBoxColumn.Width = 60;
			// 
			// sizeDataGridViewTextBoxColumn
			// 
			this.sizeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.sizeDataGridViewTextBoxColumn.DataPropertyName = "Size";
			this.sizeDataGridViewTextBoxColumn.HeaderText = "Size";
			this.sizeDataGridViewTextBoxColumn.Name = "sizeDataGridViewTextBoxColumn";
			this.sizeDataGridViewTextBoxColumn.ReadOnly = true;
			this.sizeDataGridViewTextBoxColumn.Width = 52;
			// 
			// descriptionDataGridViewTextBoxColumn
			// 
			this.descriptionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
			this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
			this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
			this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
			this.descriptionDataGridViewTextBoxColumn.Width = 85;
			// 
			// costDataGridViewTextBoxColumn
			// 
			this.costDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.costDataGridViewTextBoxColumn.DataPropertyName = "Cost";
			this.costDataGridViewTextBoxColumn.HeaderText = "Cost";
			this.costDataGridViewTextBoxColumn.Name = "costDataGridViewTextBoxColumn";
			this.costDataGridViewTextBoxColumn.ReadOnly = true;
			this.costDataGridViewTextBoxColumn.Width = 53;
			// 
			// iHullBindingSource
			// 
			this.iHullBindingSource.DataSource = typeof(FrEee.Game.Interfaces.IHull);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Width = 0;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCancel.BackColor = System.Drawing.Color.Black;
			this.btnCancel.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnCancel.Location = new System.Drawing.Point(13, 481);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(140, 29);
			this.btnCancel.TabIndex = 25;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.BackColor = System.Drawing.Color.Black;
			this.btnOk.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnOk.Location = new System.Drawing.Point(330, 481);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(140, 29);
			this.btnOk.TabIndex = 24;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = false;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// HullPickerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(482, 520);
			this.Controls.Add(this.lstAbilities);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.gridHulls);
			this.Controls.Add(this.ddlVehicleType);
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "HullPickerForm";
			this.ShowInTaskbar = false;
			this.Text = "Select a hull";
			((System.ComponentModel.ISupportInitialize)(this.gridHulls)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iHullBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox ddlVehicleType;
		private System.Windows.Forms.DataGridView gridHulls;
		private Controls.GameButton btnOk;
		private Controls.GameButton btnCancel;
		private System.Windows.Forms.DataGridViewImageColumn IconColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn sizeDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn costDataGridViewTextBoxColumn;
		private System.Windows.Forms.BindingSource iHullBindingSource;
		private System.Windows.Forms.DataGridViewImageColumn IconCol;
		private System.Windows.Forms.DataGridViewImageColumn Icon;
		private System.Windows.Forms.ListView lstAbilities;
		private System.Windows.Forms.ColumnHeader columnHeader1;
	}
}