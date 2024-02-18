namespace FrEee.UI.WinForms.Forms;

partial class GenericPickerForm
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
		this.btnCancel = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnOk = new FrEee.UI.WinForms.Controls.GameButton();
		this.lstObjects = new System.Windows.Forms.ListView();
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
		// lstObjects
		// 
		this.lstObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.lstObjects.BackColor = System.Drawing.Color.Black;
		this.lstObjects.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.lstObjects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
		this.lstObjects.ForeColor = System.Drawing.Color.White;
		this.lstObjects.FullRowSelect = true;
		this.lstObjects.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
		this.lstObjects.Location = new System.Drawing.Point(12, 12);
		this.lstObjects.Name = "lstObjects";
		this.lstObjects.Size = new System.Drawing.Size(335, 371);
		this.lstObjects.TabIndex = 28;
		this.lstObjects.UseCompatibleStateImageBehavior = false;
		this.lstObjects.View = System.Windows.Forms.View.Details;
		this.lstObjects.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstSpaceObjects_ItemSelectionChanged);
		this.lstObjects.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstObjects_MouseDoubleClick);
		// 
		// columnHeader1
		// 
		this.columnHeader1.Width = 25;
		// 
		// GenericPickerForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.ClientSize = new System.Drawing.Size(359, 430);
		this.Controls.Add(this.lstObjects);
		this.Controls.Add(this.btnCancel);
		this.Controls.Add(this.btnOk);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "GenericPickerForm";
		this.ShowInTaskbar = false;
		this.Text = "Select an object";
		this.ResumeLayout(false);

	}

	#endregion

	private Controls.GameButton btnCancel;
	private Controls.GameButton btnOk;
	private System.Windows.Forms.ListView lstObjects;
	private System.Windows.Forms.ColumnHeader columnHeader1;
}