namespace FrEee.WinForms.Forms;

partial class SpaceObjectPickerForm
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
		this.btnCancel = new FrEee.WinForms.Controls.GameButton();
		this.btnOk = new FrEee.WinForms.Controls.GameButton();
		this.lstSpaceObjects = new System.Windows.Forms.ListView();
		this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
		this.SuspendLayout();
		// 
		// btnCancel
		// 
		this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		this.btnCancel.BackColor = System.Drawing.Color.Black;
		this.btnCancel.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnCancel.Location = new System.Drawing.Point(12, 389);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(140, 29);
		this.btnCancel.TabIndex = 27;
		this.btnCancel.Text = "Cancel";
		this.btnCancel.UseVisualStyleBackColor = false;
		this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
		// 
		// btnOk
		// 
		this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.btnOk.BackColor = System.Drawing.Color.Black;
		this.btnOk.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnOk.Location = new System.Drawing.Point(207, 389);
		this.btnOk.Name = "btnOk";
		this.btnOk.Size = new System.Drawing.Size(140, 29);
		this.btnOk.TabIndex = 26;
		this.btnOk.Text = "OK";
		this.btnOk.UseVisualStyleBackColor = false;
		this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
		// 
		// lstSpaceObjects
		// 
		this.lstSpaceObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.lstSpaceObjects.BackColor = System.Drawing.Color.Black;
		this.lstSpaceObjects.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.lstSpaceObjects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
		this.lstSpaceObjects.ForeColor = System.Drawing.Color.White;
		this.lstSpaceObjects.FullRowSelect = true;
		this.lstSpaceObjects.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
		this.lstSpaceObjects.Location = new System.Drawing.Point(12, 12);
		this.lstSpaceObjects.Name = "lstSpaceObjects";
		this.lstSpaceObjects.Size = new System.Drawing.Size(335, 371);
		this.lstSpaceObjects.TabIndex = 28;
		this.lstSpaceObjects.UseCompatibleStateImageBehavior = false;
		this.lstSpaceObjects.View = System.Windows.Forms.View.Details;
		this.lstSpaceObjects.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstSpaceObjects_ItemSelectionChanged);
		this.lstSpaceObjects.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstSpaceObjects_MouseDoubleClick);
		// 
		// columnHeader1
		// 
		this.columnHeader1.Width = 25;
		// 
		// SpaceObjectPickerForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.ClientSize = new System.Drawing.Size(359, 430);
		this.Controls.Add(this.lstSpaceObjects);
		this.Controls.Add(this.btnCancel);
		this.Controls.Add(this.btnOk);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "SpaceObjectPickerForm";
		this.ShowInTaskbar = false;
		this.Text = "Select a space object";
		this.ResumeLayout(false);

	}

	#endregion

	private Controls.GameButton btnCancel;
	private Controls.GameButton btnOk;
	private System.Windows.Forms.ListView lstSpaceObjects;
	private System.Windows.Forms.ColumnHeader columnHeader1;
}