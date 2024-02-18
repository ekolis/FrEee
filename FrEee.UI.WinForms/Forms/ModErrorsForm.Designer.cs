namespace FrEee.WinForms.Forms;

partial class ModErrorsForm
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
		this.gamePanel1 = new FrEee.WinForms.Controls.GamePanel();
		this.lstFiles = new System.Windows.Forms.ListView();
		this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
		this.gamePanel2 = new FrEee.WinForms.Controls.GamePanel();
		this.lstErrors = new System.Windows.Forms.ListView();
		this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
		this.pnlDetails = new FrEee.WinForms.Controls.GamePanel();
		this.lblField = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.lblRecord = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.btnOK = new FrEee.WinForms.Controls.GameButton();
		this.btnCancel = new FrEee.WinForms.Controls.GameButton();
		this.lblNoErrors = new System.Windows.Forms.Label();
		this.lblMessage = new System.Windows.Forms.Label();
		this.gamePanel1.SuspendLayout();
		this.gamePanel2.SuspendLayout();
		this.pnlDetails.SuspendLayout();
		this.SuspendLayout();
		// 
		// gamePanel1
		// 
		this.gamePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
		this.gamePanel1.BackColor = System.Drawing.Color.Black;
		this.gamePanel1.BorderColor = System.Drawing.Color.CornflowerBlue;
		this.gamePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.gamePanel1.Controls.Add(this.lstFiles);
		this.gamePanel1.ForeColor = System.Drawing.Color.White;
		this.gamePanel1.Location = new System.Drawing.Point(12, 12);
		this.gamePanel1.Name = "gamePanel1";
		this.gamePanel1.Padding = new System.Windows.Forms.Padding(3);
		this.gamePanel1.Size = new System.Drawing.Size(200, 467);
		this.gamePanel1.TabIndex = 1;
		// 
		// lstFiles
		// 
		this.lstFiles.BackColor = System.Drawing.Color.Black;
		this.lstFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
		this.lstFiles.Dock = System.Windows.Forms.DockStyle.Fill;
		this.lstFiles.ForeColor = System.Drawing.Color.White;
		this.lstFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
		this.lstFiles.Location = new System.Drawing.Point(3, 3);
		this.lstFiles.Name = "lstFiles";
		this.lstFiles.Size = new System.Drawing.Size(192, 459);
		this.lstFiles.TabIndex = 1;
		this.lstFiles.UseCompatibleStateImageBehavior = false;
		this.lstFiles.View = System.Windows.Forms.View.Details;
		this.lstFiles.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstFiles_ItemSelectionChanged);
		// 
		// columnHeader1
		// 
		this.columnHeader1.Width = 188;
		// 
		// gamePanel2
		// 
		this.gamePanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.gamePanel2.BackColor = System.Drawing.Color.Black;
		this.gamePanel2.BorderColor = System.Drawing.Color.CornflowerBlue;
		this.gamePanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.gamePanel2.Controls.Add(this.lstErrors);
		this.gamePanel2.ForeColor = System.Drawing.Color.White;
		this.gamePanel2.Location = new System.Drawing.Point(214, 12);
		this.gamePanel2.Name = "gamePanel2";
		this.gamePanel2.Padding = new System.Windows.Forms.Padding(3);
		this.gamePanel2.Size = new System.Drawing.Size(380, 278);
		this.gamePanel2.TabIndex = 2;
		// 
		// lstErrors
		// 
		this.lstErrors.BackColor = System.Drawing.Color.Black;
		this.lstErrors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
		this.lstErrors.Dock = System.Windows.Forms.DockStyle.Fill;
		this.lstErrors.ForeColor = System.Drawing.Color.White;
		this.lstErrors.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
		this.lstErrors.Location = new System.Drawing.Point(3, 3);
		this.lstErrors.Name = "lstErrors";
		this.lstErrors.Size = new System.Drawing.Size(372, 270);
		this.lstErrors.TabIndex = 1;
		this.lstErrors.UseCompatibleStateImageBehavior = false;
		this.lstErrors.View = System.Windows.Forms.View.Details;
		this.lstErrors.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstErrors_ItemSelectionChanged);
		// 
		// columnHeader2
		// 
		this.columnHeader2.Width = 368;
		// 
		// pnlDetails
		// 
		this.pnlDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.pnlDetails.BackColor = System.Drawing.Color.Black;
		this.pnlDetails.BorderColor = System.Drawing.Color.CornflowerBlue;
		this.pnlDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pnlDetails.Controls.Add(this.lblMessage);
		this.pnlDetails.Controls.Add(this.lblField);
		this.pnlDetails.Controls.Add(this.label3);
		this.pnlDetails.Controls.Add(this.lblRecord);
		this.pnlDetails.Controls.Add(this.label1);
		this.pnlDetails.ForeColor = System.Drawing.Color.White;
		this.pnlDetails.Location = new System.Drawing.Point(214, 296);
		this.pnlDetails.Name = "pnlDetails";
		this.pnlDetails.Padding = new System.Windows.Forms.Padding(3);
		this.pnlDetails.Size = new System.Drawing.Size(380, 157);
		this.pnlDetails.TabIndex = 4;
		// 
		// lblField
		// 
		this.lblField.AutoSize = true;
		this.lblField.Location = new System.Drawing.Point(18, 132);
		this.lblField.Name = "lblField";
		this.lblField.Size = new System.Drawing.Size(50, 13);
		this.lblField.TabIndex = 3;
		this.lblField.Text = "Field Info";
		// 
		// label3
		// 
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(5, 119);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(32, 13);
		this.label3.TabIndex = 2;
		this.label3.Text = "Field:";
		// 
		// lblRecord
		// 
		this.lblRecord.AutoSize = true;
		this.lblRecord.Location = new System.Drawing.Point(19, 88);
		this.lblRecord.Name = "lblRecord";
		this.lblRecord.Size = new System.Drawing.Size(63, 13);
		this.lblRecord.TabIndex = 1;
		this.lblRecord.Text = "Record Info";
		// 
		// label1
		// 
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 75);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(45, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Record:";
		// 
		// btnOK
		// 
		this.btnOK.BackColor = System.Drawing.Color.Black;
		this.btnOK.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnOK.Location = new System.Drawing.Point(519, 459);
		this.btnOK.Name = "btnOK";
		this.btnOK.Size = new System.Drawing.Size(75, 23);
		this.btnOK.TabIndex = 5;
		this.btnOK.Text = "OK";
		this.btnOK.UseVisualStyleBackColor = false;
		this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
		// 
		// btnCancel
		// 
		this.btnCancel.BackColor = System.Drawing.Color.Black;
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnCancel.Location = new System.Drawing.Point(438, 459);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(75, 23);
		this.btnCancel.TabIndex = 6;
		this.btnCancel.Text = "Cancel";
		this.btnCancel.UseVisualStyleBackColor = false;
		this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
		// 
		// lblNoErrors
		// 
		this.lblNoErrors.AutoSize = true;
		this.lblNoErrors.Location = new System.Drawing.Point(381, 462);
		this.lblNoErrors.Name = "lblNoErrors";
		this.lblNoErrors.Size = new System.Drawing.Size(51, 13);
		this.lblNoErrors.TabIndex = 7;
		this.lblNoErrors.Text = "No Errors";
		// 
		// lblMessage
		// 
		this.lblMessage.Location = new System.Drawing.Point(6, 3);
		this.lblMessage.Name = "lblMessage";
		this.lblMessage.Size = new System.Drawing.Size(369, 72);
		this.lblMessage.TabIndex = 4;
		this.lblMessage.Text = "Message";
		// 
		// ModErrorsForm
		// 
		this.AcceptButton = this.btnOK;
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.CancelButton = this.btnCancel;
		this.ClientSize = new System.Drawing.Size(606, 491);
		this.Controls.Add(this.lblNoErrors);
		this.Controls.Add(this.btnCancel);
		this.Controls.Add(this.btnOK);
		this.Controls.Add(this.pnlDetails);
		this.Controls.Add(this.gamePanel2);
		this.Controls.Add(this.gamePanel1);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "ModErrorsForm";
		this.Text = "Mod Errors";
		this.gamePanel1.ResumeLayout(false);
		this.gamePanel2.ResumeLayout(false);
		this.pnlDetails.ResumeLayout(false);
		this.pnlDetails.PerformLayout();
		this.ResumeLayout(false);
		this.PerformLayout();

	}

	#endregion

	private Controls.GamePanel gamePanel1;
	private System.Windows.Forms.ListView lstFiles;
	private Controls.GamePanel gamePanel2;
	private System.Windows.Forms.ListView lstErrors;
	private Controls.GamePanel pnlDetails;
	private Controls.GameButton btnOK;
	private Controls.GameButton btnCancel;
	private System.Windows.Forms.Label lblNoErrors;
	private System.Windows.Forms.Label lblRecord;
	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.Label lblField;
	private System.Windows.Forms.Label label3;
	private System.Windows.Forms.ColumnHeader columnHeader1;
	private System.Windows.Forms.ColumnHeader columnHeader2;
	private System.Windows.Forms.Label lblMessage;
}