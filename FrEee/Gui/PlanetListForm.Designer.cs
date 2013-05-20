namespace FrEee.Gui
{
	partial class PlanetListForm
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
            this.pnlHeader = new FrEee.Gui.Controls.GamePanel();
            this.galaxyView = new FrEee.Gui.Controls.GalaxyView();
            this.txtAvailable = new System.Windows.Forms.Label();
            this.txtShips = new System.Windows.Forms.Label();
            this.txtBreathable = new System.Windows.Forms.Label();
            this.txtUncolonized = new System.Windows.Forms.Label();
            this.txtNonAligned = new System.Windows.Forms.Label();
            this.txtAllies = new System.Windows.Forms.Label();
            this.txtEnemies = new System.Windows.Forms.Label();
            this.txtColonizable = new System.Windows.Forms.Label();
            this.txtPlanets = new System.Windows.Forms.Label();
            this.txtSystems = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gridPlanets = new System.Windows.Forms.DataGridView();
            this.iconDataGridViewImageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sizeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.surfaceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.atmosphereDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resourceValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ownerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.planetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPlanets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.planetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Black;
            this.pnlHeader.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.pnlHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHeader.Controls.Add(this.galaxyView);
            this.pnlHeader.Controls.Add(this.txtAvailable);
            this.pnlHeader.Controls.Add(this.txtShips);
            this.pnlHeader.Controls.Add(this.txtBreathable);
            this.pnlHeader.Controls.Add(this.txtUncolonized);
            this.pnlHeader.Controls.Add(this.txtNonAligned);
            this.pnlHeader.Controls.Add(this.txtAllies);
            this.pnlHeader.Controls.Add(this.txtEnemies);
            this.pnlHeader.Controls.Add(this.txtColonizable);
            this.pnlHeader.Controls.Add(this.txtPlanets);
            this.pnlHeader.Controls.Add(this.txtSystems);
            this.pnlHeader.Controls.Add(this.label10);
            this.pnlHeader.Controls.Add(this.label9);
            this.pnlHeader.Controls.Add(this.label8);
            this.pnlHeader.Controls.Add(this.label7);
            this.pnlHeader.Controls.Add(this.label6);
            this.pnlHeader.Controls.Add(this.label5);
            this.pnlHeader.Controls.Add(this.label4);
            this.pnlHeader.Controls.Add(this.label3);
            this.pnlHeader.Controls.Add(this.label2);
            this.pnlHeader.Controls.Add(this.label1);
            this.pnlHeader.ForeColor = System.Drawing.Color.White;
            this.pnlHeader.Location = new System.Drawing.Point(13, 13);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(759, 163);
            this.pnlHeader.TabIndex = 0;
            // 
            // galaxyView
            // 
            this.galaxyView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.galaxyView.BackColor = System.Drawing.Color.Black;
            this.galaxyView.Galaxy = null;
            this.galaxyView.Location = new System.Drawing.Point(195, 4);
            this.galaxyView.Name = "galaxyView";
            this.galaxyView.SelectedStarSystem = null;
            this.galaxyView.Size = new System.Drawing.Size(559, 154);
            this.galaxyView.TabIndex = 20;
            this.galaxyView.Text = "galaxyView1";
            // 
            // txtAvailable
            // 
            this.txtAvailable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAvailable.AutoSize = true;
            this.txtAvailable.Location = new System.Drawing.Point(176, 141);
            this.txtAvailable.Name = "txtAvailable";
            this.txtAvailable.Size = new System.Drawing.Size(13, 13);
            this.txtAvailable.TabIndex = 19;
            this.txtAvailable.Text = "0";
            this.txtAvailable.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtShips
            // 
            this.txtShips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtShips.AutoSize = true;
            this.txtShips.Location = new System.Drawing.Point(176, 128);
            this.txtShips.Name = "txtShips";
            this.txtShips.Size = new System.Drawing.Size(13, 13);
            this.txtShips.TabIndex = 18;
            this.txtShips.Text = "0";
            this.txtShips.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtBreathable
            // 
            this.txtBreathable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBreathable.AutoSize = true;
            this.txtBreathable.Location = new System.Drawing.Point(176, 95);
            this.txtBreathable.Name = "txtBreathable";
            this.txtBreathable.Size = new System.Drawing.Size(13, 13);
            this.txtBreathable.TabIndex = 17;
            this.txtBreathable.Text = "0";
            this.txtBreathable.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtUncolonized
            // 
            this.txtUncolonized.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUncolonized.AutoSize = true;
            this.txtUncolonized.Location = new System.Drawing.Point(176, 82);
            this.txtUncolonized.Name = "txtUncolonized";
            this.txtUncolonized.Size = new System.Drawing.Size(13, 13);
            this.txtUncolonized.TabIndex = 16;
            this.txtUncolonized.Text = "0";
            this.txtUncolonized.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtNonAligned
            // 
            this.txtNonAligned.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNonAligned.AutoSize = true;
            this.txtNonAligned.Location = new System.Drawing.Point(176, 69);
            this.txtNonAligned.Name = "txtNonAligned";
            this.txtNonAligned.Size = new System.Drawing.Size(13, 13);
            this.txtNonAligned.TabIndex = 15;
            this.txtNonAligned.Text = "0";
            this.txtNonAligned.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtAllies
            // 
            this.txtAllies.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAllies.AutoSize = true;
            this.txtAllies.Location = new System.Drawing.Point(176, 56);
            this.txtAllies.Name = "txtAllies";
            this.txtAllies.Size = new System.Drawing.Size(13, 13);
            this.txtAllies.TabIndex = 14;
            this.txtAllies.Text = "0";
            this.txtAllies.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtEnemies
            // 
            this.txtEnemies.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEnemies.AutoSize = true;
            this.txtEnemies.Location = new System.Drawing.Point(176, 43);
            this.txtEnemies.Name = "txtEnemies";
            this.txtEnemies.Size = new System.Drawing.Size(13, 13);
            this.txtEnemies.TabIndex = 13;
            this.txtEnemies.Text = "0";
            this.txtEnemies.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtColonizable
            // 
            this.txtColonizable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtColonizable.AutoSize = true;
            this.txtColonizable.Location = new System.Drawing.Point(176, 30);
            this.txtColonizable.Name = "txtColonizable";
            this.txtColonizable.Size = new System.Drawing.Size(13, 13);
            this.txtColonizable.TabIndex = 12;
            this.txtColonizable.Text = "0";
            this.txtColonizable.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtPlanets
            // 
            this.txtPlanets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlanets.AutoSize = true;
            this.txtPlanets.Location = new System.Drawing.Point(176, 17);
            this.txtPlanets.Name = "txtPlanets";
            this.txtPlanets.Size = new System.Drawing.Size(13, 13);
            this.txtPlanets.TabIndex = 11;
            this.txtPlanets.Text = "0";
            this.txtPlanets.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtSystems
            // 
            this.txtSystems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSystems.AutoSize = true;
            this.txtSystems.Location = new System.Drawing.Point(176, 4);
            this.txtSystems.Name = "txtSystems";
            this.txtSystems.Size = new System.Drawing.Size(13, 13);
            this.txtSystems.TabIndex = 10;
            this.txtSystems.Text = "0";
            this.txtSystems.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label10.Location = new System.Drawing.Point(15, 141);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Available";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label9.Location = new System.Drawing.Point(4, 128);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Colonizing Ships";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label8.Location = new System.Drawing.Point(15, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(141, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Uncolonized and Breathable";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label7.Location = new System.Drawing.Point(15, 82);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Uncolonized";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label6.Location = new System.Drawing.Point(15, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "owned by Non-Aligned";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label5.Location = new System.Drawing.Point(15, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "owned by Allies";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label4.Location = new System.Drawing.Point(15, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "owned by Enemies";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label3.Location = new System.Drawing.Point(4, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Colonizable Planets";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new System.Drawing.Point(4, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Known Planets";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Known Systems";
            // 
            // gridPlanets
            // 
            this.gridPlanets.AllowUserToAddRows = false;
            this.gridPlanets.AllowUserToDeleteRows = false;
            this.gridPlanets.AllowUserToOrderColumns = true;
            this.gridPlanets.AllowUserToResizeRows = false;
            this.gridPlanets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPlanets.AutoGenerateColumns = false;
            this.gridPlanets.BackgroundColor = System.Drawing.Color.Black;
            this.gridPlanets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPlanets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iconDataGridViewImageColumn,
            this.nameDataGridViewTextBoxColumn,
            this.sizeDataGridViewTextBoxColumn,
            this.surfaceDataGridViewTextBoxColumn,
            this.atmosphereDataGridViewTextBoxColumn,
            this.resourceValueDataGridViewTextBoxColumn,
            this.ownerDataGridViewTextBoxColumn});
            this.gridPlanets.DataSource = this.planetBindingSource;
            this.gridPlanets.Location = new System.Drawing.Point(13, 183);
            this.gridPlanets.Name = "gridPlanets";
            this.gridPlanets.ReadOnly = true;
            this.gridPlanets.RowHeadersVisible = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            this.gridPlanets.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridPlanets.RowTemplate.Height = 32;
            this.gridPlanets.RowTemplate.ReadOnly = true;
            this.gridPlanets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridPlanets.Size = new System.Drawing.Size(759, 366);
            this.gridPlanets.TabIndex = 1;
            this.gridPlanets.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gridPlanets_DataError);
            this.gridPlanets.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPlanets_RowEnter);
            this.gridPlanets.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPlanets_RowLeave);
            // 
            // iconDataGridViewImageColumn
            // 
            this.iconDataGridViewImageColumn.DataPropertyName = "Icon";
            this.iconDataGridViewImageColumn.HeaderText = "Icon";
            this.iconDataGridViewImageColumn.Name = "iconDataGridViewImageColumn";
            this.iconDataGridViewImageColumn.ReadOnly = true;
            this.iconDataGridViewImageColumn.Width = 32;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sizeDataGridViewTextBoxColumn
            // 
            this.sizeDataGridViewTextBoxColumn.DataPropertyName = "Size";
            this.sizeDataGridViewTextBoxColumn.HeaderText = "Size";
            this.sizeDataGridViewTextBoxColumn.Name = "sizeDataGridViewTextBoxColumn";
            this.sizeDataGridViewTextBoxColumn.ReadOnly = true;
            this.sizeDataGridViewTextBoxColumn.Width = 50;
            // 
            // surfaceDataGridViewTextBoxColumn
            // 
            this.surfaceDataGridViewTextBoxColumn.DataPropertyName = "Surface";
            this.surfaceDataGridViewTextBoxColumn.HeaderText = "Surface";
            this.surfaceDataGridViewTextBoxColumn.Name = "surfaceDataGridViewTextBoxColumn";
            this.surfaceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // atmosphereDataGridViewTextBoxColumn
            // 
            this.atmosphereDataGridViewTextBoxColumn.DataPropertyName = "Atmosphere";
            this.atmosphereDataGridViewTextBoxColumn.HeaderText = "Atmosphere";
            this.atmosphereDataGridViewTextBoxColumn.Name = "atmosphereDataGridViewTextBoxColumn";
            this.atmosphereDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // resourceValueDataGridViewTextBoxColumn
            // 
            this.resourceValueDataGridViewTextBoxColumn.DataPropertyName = "ResourceValue";
            this.resourceValueDataGridViewTextBoxColumn.HeaderText = "ResourceValue";
            this.resourceValueDataGridViewTextBoxColumn.Name = "resourceValueDataGridViewTextBoxColumn";
            this.resourceValueDataGridViewTextBoxColumn.ReadOnly = true;
            this.resourceValueDataGridViewTextBoxColumn.Width = 200;
            // 
            // ownerDataGridViewTextBoxColumn
            // 
            this.ownerDataGridViewTextBoxColumn.DataPropertyName = "Owner";
            this.ownerDataGridViewTextBoxColumn.HeaderText = "Owner";
            this.ownerDataGridViewTextBoxColumn.Name = "ownerDataGridViewTextBoxColumn";
            this.ownerDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // planetBindingSource
            // 
            this.planetBindingSource.AllowNew = false;
            this.planetBindingSource.DataSource = typeof(FrEee.Game.Planet);
            // 
            // PlanetListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.gridPlanets);
            this.Controls.Add(this.pnlHeader);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "PlanetListForm";
            this.Text = "Planets";
            this.Load += new System.EventHandler(this.PlanetListForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPlanets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.planetBindingSource)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private Controls.GamePanel pnlHeader;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label txtBreathable;
		private System.Windows.Forms.Label txtUncolonized;
		private System.Windows.Forms.Label txtNonAligned;
		private System.Windows.Forms.Label txtAllies;
		private System.Windows.Forms.Label txtEnemies;
		private System.Windows.Forms.Label txtColonizable;
		private System.Windows.Forms.Label txtPlanets;
		private System.Windows.Forms.Label txtSystems;
		private System.Windows.Forms.Label txtAvailable;
		private System.Windows.Forms.Label txtShips;
		private Controls.GalaxyView galaxyView;
		private System.Windows.Forms.DataGridView gridPlanets;
		private System.Windows.Forms.BindingSource planetBindingSource;
		private System.Windows.Forms.DataGridViewImageColumn iconDataGridViewImageColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn sizeDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn surfaceDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn atmosphereDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn resourceValueDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn ownerDataGridViewTextBoxColumn;
	}
}