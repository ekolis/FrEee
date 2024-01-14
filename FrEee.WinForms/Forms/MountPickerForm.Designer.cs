using FrEee.Interfaces;
namespace FrEee.WinForms.Forms;

partial class MountPickerForm
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
		this.gridMounts = new System.Windows.Forms.DataGridView();
		this.IconCol = new System.Windows.Forms.DataGridViewImageColumn();
		this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.mountBindingSource = new System.Windows.Forms.BindingSource(this.components);
		this.btnOk = new FrEee.WinForms.Controls.GameButton();
		this.btnCancel = new FrEee.WinForms.Controls.GameButton();
		((System.ComponentModel.ISupportInitialize)(this.gridMounts)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.mountBindingSource)).BeginInit();
		this.SuspendLayout();
		// 
		// gridMounts
		// 
		this.gridMounts.AllowUserToAddRows = false;
		this.gridMounts.AllowUserToDeleteRows = false;
		this.gridMounts.AllowUserToResizeRows = false;
		this.gridMounts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.gridMounts.AutoGenerateColumns = false;
		this.gridMounts.BackgroundColor = System.Drawing.Color.Black;
		this.gridMounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gridMounts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IconCol,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
		this.gridMounts.DataSource = this.mountBindingSource;
		this.gridMounts.Location = new System.Drawing.Point(13, 12);
		this.gridMounts.MultiSelect = false;
		this.gridMounts.Name = "gridMounts";
		this.gridMounts.ReadOnly = true;
		this.gridMounts.RowHeadersVisible = false;
		this.gridMounts.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Black;
		this.gridMounts.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
		this.gridMounts.RowTemplate.Height = 32;
		this.gridMounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gridMounts.Size = new System.Drawing.Size(457, 373);
		this.gridMounts.TabIndex = 1;
		this.gridMounts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridMounts_CellDoubleClick);
		this.gridMounts.SelectionChanged += new System.EventHandler(this.gridMounts_SelectionChanged);
		// 
		// Icon
		// 
		this.IconCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		this.IconCol.DataPropertyName = "Icon";
		this.IconCol.HeaderText = "Icon";
		this.IconCol.Name = "Icon";
		this.IconCol.ReadOnly = true;
		this.IconCol.Width = 32;
		// 
		// dataGridViewTextBoxColumn1
		// 
		this.dataGridViewTextBoxColumn1.DataPropertyName = "Name";
		this.dataGridViewTextBoxColumn1.HeaderText = "Name";
		this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
		this.dataGridViewTextBoxColumn1.ReadOnly = true;
		this.dataGridViewTextBoxColumn1.Width = 150;
		// 
		// dataGridViewTextBoxColumn2
		// 
		this.dataGridViewTextBoxColumn2.DataPropertyName = "Description";
		this.dataGridViewTextBoxColumn2.HeaderText = "Description";
		this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
		this.dataGridViewTextBoxColumn2.ReadOnly = true;
		this.dataGridViewTextBoxColumn2.Width = 250;
		// 
		// mountBindingSource
		// 
		this.mountBindingSource.AllowNew = false;
		this.mountBindingSource.DataSource = typeof(FrEee.Objects.Technology.Mount);
		// 
		// btnOk
		// 
		this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.btnOk.BackColor = System.Drawing.Color.Black;
		this.btnOk.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnOk.Location = new System.Drawing.Point(330, 391);
		this.btnOk.Name = "btnOk";
		this.btnOk.Size = new System.Drawing.Size(140, 29);
		this.btnOk.TabIndex = 24;
		this.btnOk.Text = "OK";
		this.btnOk.UseVisualStyleBackColor = false;
		this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
		// 
		// btnCancel
		// 
		this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		this.btnCancel.BackColor = System.Drawing.Color.Black;
		this.btnCancel.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnCancel.Location = new System.Drawing.Point(13, 391);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(140, 29);
		this.btnCancel.TabIndex = 25;
		this.btnCancel.Text = "Cancel";
		this.btnCancel.UseVisualStyleBackColor = false;
		this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
		// 
		// MountPickerForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.ClientSize = new System.Drawing.Size(482, 430);
		this.Controls.Add(this.btnCancel);
		this.Controls.Add(this.btnOk);
		this.Controls.Add(this.gridMounts);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "MountPickerForm";
		this.ShowInTaskbar = false;
		this.Text = "Select a mount";
		((System.ComponentModel.ISupportInitialize)(this.gridMounts)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.mountBindingSource)).EndInit();
		this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.DataGridView gridMounts;
	private Controls.GameButton btnOk;
	private Controls.GameButton btnCancel;
	private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
	private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
	private System.Windows.Forms.BindingSource mountBindingSource;
	private System.Windows.Forms.DataGridViewImageColumn IconCol;
}