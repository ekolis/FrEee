namespace FrEee.WinForms.Forms
{
	partial class HostConsoleForm
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
			this.gridEmpires = new System.Windows.Forms.DataGridView();
			this.Insignia = new System.Windows.Forms.DataGridViewImageColumn();
			this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.plrUploadStatusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.empireStatusBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.btnClose = new FrEee.WinForms.Controls.GameButton();
			this.btnProcess = new FrEee.WinForms.Controls.GameButton();
			this.btnToggleAI = new FrEee.WinForms.Controls.GameButton();
			this.btnPlayerView = new FrEee.WinForms.Controls.GameButton();
			this.btnEdit = new FrEee.WinForms.Controls.GameButton();
			((System.ComponentModel.ISupportInitialize)(this.gridEmpires)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.empireStatusBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// gridEmpires
			// 
			this.gridEmpires.AllowUserToAddRows = false;
			this.gridEmpires.AllowUserToDeleteRows = false;
			this.gridEmpires.AllowUserToResizeRows = false;
			this.gridEmpires.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridEmpires.AutoGenerateColumns = false;
			this.gridEmpires.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.gridEmpires.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.gridEmpires.BackgroundColor = System.Drawing.Color.Black;
			this.gridEmpires.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridEmpires.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Insignia,
            this.nameDataGridViewTextBoxColumn,
            this.plrUploadStatusDataGridViewTextBoxColumn});
			this.gridEmpires.DataSource = this.empireStatusBindingSource;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridEmpires.DefaultCellStyle = dataGridViewCellStyle1;
			this.gridEmpires.GridColor = System.Drawing.Color.White;
			this.gridEmpires.Location = new System.Drawing.Point(12, 12);
			this.gridEmpires.MultiSelect = false;
			this.gridEmpires.Name = "gridEmpires";
			this.gridEmpires.ReadOnly = true;
			this.gridEmpires.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.gridEmpires.RowHeadersVisible = false;
			this.gridEmpires.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridEmpires.Size = new System.Drawing.Size(480, 300);
			this.gridEmpires.TabIndex = 0;
			// 
			// Insignia
			// 
			this.Insignia.DataPropertyName = "Insignia";
			this.Insignia.HeaderText = "Icon";
			this.Insignia.Name = "Insignia";
			this.Insignia.ReadOnly = true;
			this.Insignia.Width = 34;
			// 
			// nameDataGridViewTextBoxColumn
			// 
			this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
			this.nameDataGridViewTextBoxColumn.HeaderText = "Empire";
			this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
			this.nameDataGridViewTextBoxColumn.ReadOnly = true;
			this.nameDataGridViewTextBoxColumn.Width = 64;
			// 
			// plrUploadStatusDataGridViewTextBoxColumn
			// 
			this.plrUploadStatusDataGridViewTextBoxColumn.DataPropertyName = "PlrUploadStatus";
			this.plrUploadStatusDataGridViewTextBoxColumn.HeaderText = "Status";
			this.plrUploadStatusDataGridViewTextBoxColumn.Name = "plrUploadStatusDataGridViewTextBoxColumn";
			this.plrUploadStatusDataGridViewTextBoxColumn.ReadOnly = true;
			this.plrUploadStatusDataGridViewTextBoxColumn.Width = 62;
			// 
			// empireStatusBindingSource
			// 
			this.empireStatusBindingSource.AllowNew = false;
			this.empireStatusBindingSource.DataSource = typeof(FrEee.Game.Objects.Civilization.EmpireStatus);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.BackColor = System.Drawing.Color.Black;
			this.btnClose.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnClose.Location = new System.Drawing.Point(417, 326);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnProcess
			// 
			this.btnProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnProcess.BackColor = System.Drawing.Color.Black;
			this.btnProcess.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnProcess.Location = new System.Drawing.Point(336, 326);
			this.btnProcess.Name = "btnProcess";
			this.btnProcess.Size = new System.Drawing.Size(75, 23);
			this.btnProcess.TabIndex = 2;
			this.btnProcess.Text = "Process Turn";
			this.btnProcess.UseVisualStyleBackColor = false;
			this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
			// 
			// btnToggleAI
			// 
			this.btnToggleAI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnToggleAI.BackColor = System.Drawing.Color.Black;
			this.btnToggleAI.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnToggleAI.Location = new System.Drawing.Point(255, 326);
			this.btnToggleAI.Name = "btnToggleAI";
			this.btnToggleAI.Size = new System.Drawing.Size(75, 23);
			this.btnToggleAI.TabIndex = 3;
			this.btnToggleAI.Text = "Toggle AI";
			this.btnToggleAI.UseVisualStyleBackColor = false;
			this.btnToggleAI.Click += new System.EventHandler(this.btnToggleAI_Click);
			// 
			// btnPlayerView
			// 
			this.btnPlayerView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPlayerView.BackColor = System.Drawing.Color.Black;
			this.btnPlayerView.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnPlayerView.Location = new System.Drawing.Point(12, 326);
			this.btnPlayerView.Name = "btnPlayerView";
			this.btnPlayerView.Size = new System.Drawing.Size(75, 23);
			this.btnPlayerView.TabIndex = 5;
			this.btnPlayerView.Text = "Player View";
			this.btnPlayerView.UseVisualStyleBackColor = false;
			this.btnPlayerView.Click += new System.EventHandler(this.btnPlayerView_Click);
			// 
			// btnEdit
			// 
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEdit.BackColor = System.Drawing.Color.Black;
			this.btnEdit.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnEdit.Location = new System.Drawing.Point(93, 326);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(75, 23);
			this.btnEdit.TabIndex = 6;
			this.btnEdit.Text = "Edit Game";
			this.btnEdit.UseVisualStyleBackColor = false;
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// HostConsoleForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(504, 361);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.btnPlayerView);
			this.Controls.Add(this.btnToggleAI);
			this.Controls.Add(this.btnProcess);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.gridEmpires);
			this.MinimumSize = new System.Drawing.Size(520, 200);
			this.Name = "HostConsoleForm";
			this.Text = "Host Console";
			((System.ComponentModel.ISupportInitialize)(this.gridEmpires)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.empireStatusBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView gridEmpires;
		private System.Windows.Forms.BindingSource empireStatusBindingSource;
		private System.Windows.Forms.DataGridViewImageColumn Insignia;
		private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn plrUploadStatusDataGridViewTextBoxColumn;
		private Controls.GameButton btnClose;
		private Controls.GameButton btnProcess;
		private Controls.GameButton btnToggleAI;
		private Controls.GameButton btnPlayerView;
		private Controls.GameButton btnEdit;
	}
}