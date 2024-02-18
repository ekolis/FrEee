
namespace FrEee.WinForms.Forms;

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
		this.ddlVehicleType = new System.Windows.Forms.ComboBox();
		this.lstAbilities = new System.Windows.Forms.ListView();
		this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
		this.lstHulls = new System.Windows.Forms.ListView();
		this.colHull = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
		this.colSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
		this.colMin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
		this.colOrg = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
		this.colRad = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
		this.colDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
		this.btnCancel = new FrEee.WinForms.Controls.GameButton();
		this.btnOk = new FrEee.WinForms.Controls.GameButton();
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
		// columnHeader1
		// 
		this.columnHeader1.Width = 0;
		// 
		// lstHulls
		// 
		this.lstHulls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.lstHulls.BackColor = System.Drawing.Color.Black;
		this.lstHulls.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.lstHulls.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colHull,
            this.colSize,
            this.colMin,
            this.colOrg,
            this.colRad,
            this.colDescription});
		this.lstHulls.ForeColor = System.Drawing.Color.White;
		this.lstHulls.FullRowSelect = true;
		this.lstHulls.Location = new System.Drawing.Point(13, 41);
		this.lstHulls.Name = "lstHulls";
		this.lstHulls.Size = new System.Drawing.Size(457, 320);
		this.lstHulls.TabIndex = 27;
		this.lstHulls.UseCompatibleStateImageBehavior = false;
		this.lstHulls.View = System.Windows.Forms.View.Details;
		this.lstHulls.SelectedIndexChanged += new System.EventHandler(this.lstHulls_SelectedIndexChanged);
		this.lstHulls.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstHulls_MouseDoubleClick);
		// 
		// colHull
		// 
		this.colHull.Text = "Hull";
		this.colHull.Width = 120;
		// 
		// colSize
		// 
		this.colSize.Text = "Size";
		// 
		// colMin
		// 
		this.colMin.Text = "Min";
		this.colMin.Width = 40;
		// 
		// colOrg
		// 
		this.colOrg.Text = "Org";
		this.colOrg.Width = 40;
		// 
		// colRad
		// 
		this.colRad.Text = "Rad";
		this.colRad.Width = 40;
		// 
		// colDescription
		// 
		this.colDescription.Text = "Description";
		this.colDescription.Width = 150;
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
		this.Controls.Add(this.lstHulls);
		this.Controls.Add(this.lstAbilities);
		this.Controls.Add(this.btnCancel);
		this.Controls.Add(this.btnOk);
		this.Controls.Add(this.ddlVehicleType);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "HullPickerForm";
		this.ShowInTaskbar = false;
		this.Text = "Select a hull";
		this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.ComboBox ddlVehicleType;
	private Controls.GameButton btnOk;
	private Controls.GameButton btnCancel;
	//private System.Windows.Forms.DataGridViewImageColumn Icon;
	private System.Windows.Forms.ListView lstAbilities;
	private System.Windows.Forms.ColumnHeader columnHeader1;
	private System.Windows.Forms.ListView lstHulls;
	private System.Windows.Forms.ColumnHeader colHull;
	private System.Windows.Forms.ColumnHeader colSize;
	private System.Windows.Forms.ColumnHeader colMin;
	private System.Windows.Forms.ColumnHeader colOrg;
	private System.Windows.Forms.ColumnHeader colRad;
	private System.Windows.Forms.ColumnHeader colDescription;
}