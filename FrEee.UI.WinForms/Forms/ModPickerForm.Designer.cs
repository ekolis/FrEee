namespace FrEee.WinForms.Forms;

partial class ModPickerForm
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
		this.lstMods = new System.Windows.Forms.ListView();
		this.txtName = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.txtVersion = new System.Windows.Forms.Label();
		this.txtAuthor = new System.Windows.Forms.Label();
		this.txtEmail = new System.Windows.Forms.Label();
		this.txtWebsite = new System.Windows.Forms.Label();
		this.txtDescription = new System.Windows.Forms.Label();
		this.btnLoad = new FrEee.WinForms.Controls.GameButton();
		this.btnCancel = new FrEee.WinForms.Controls.GameButton();
		this.txtFolder = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.btnEdit = new FrEee.WinForms.Controls.GameButton();
		this.gamePanel1.SuspendLayout();
		this.SuspendLayout();
		// 
		// gamePanel1
		// 
		this.gamePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
		this.gamePanel1.BackColor = System.Drawing.Color.Black;
		this.gamePanel1.BorderColor = System.Drawing.Color.CornflowerBlue;
		this.gamePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.gamePanel1.Controls.Add(this.lstMods);
		this.gamePanel1.ForeColor = System.Drawing.Color.White;
		this.gamePanel1.Location = new System.Drawing.Point(12, 12);
		this.gamePanel1.Name = "gamePanel1";
		this.gamePanel1.Padding = new System.Windows.Forms.Padding(3);
		this.gamePanel1.Size = new System.Drawing.Size(201, 417);
		this.gamePanel1.TabIndex = 1;
		// 
		// lstMods
		// 
		this.lstMods.BackColor = System.Drawing.Color.Black;
		this.lstMods.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.lstMods.Dock = System.Windows.Forms.DockStyle.Fill;
		this.lstMods.ForeColor = System.Drawing.Color.White;
		this.lstMods.Location = new System.Drawing.Point(3, 3);
		this.lstMods.Name = "lstMods";
		this.lstMods.Size = new System.Drawing.Size(193, 409);
		this.lstMods.TabIndex = 1;
		this.lstMods.UseCompatibleStateImageBehavior = false;
		this.lstMods.View = System.Windows.Forms.View.List;
		this.lstMods.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstMods_ItemSelectionChanged);
		this.lstMods.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstMods_MouseDoubleClick);
		// 
		// txtName
		// 
		this.txtName.AutoSize = true;
		this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtName.Location = new System.Drawing.Point(220, 13);
		this.txtName.Name = "txtName";
		this.txtName.Size = new System.Drawing.Size(98, 20);
		this.txtName.TabIndex = 2;
		this.txtName.Text = "Stock FrEee";
		// 
		// label1
		// 
		this.label1.AutoSize = true;
		this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label1.Location = new System.Drawing.Point(241, 36);
		this.label1.Margin = new System.Windows.Forms.Padding(3);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(45, 13);
		this.label1.TabIndex = 3;
		this.label1.Text = "Version:";
		// 
		// label2
		// 
		this.label2.AutoSize = true;
		this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label2.Location = new System.Drawing.Point(241, 73);
		this.label2.Margin = new System.Windows.Forms.Padding(3);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(41, 13);
		this.label2.TabIndex = 4;
		this.label2.Text = "Author:";
		// 
		// label3
		// 
		this.label3.AutoSize = true;
		this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label3.Location = new System.Drawing.Point(241, 92);
		this.label3.Margin = new System.Windows.Forms.Padding(3);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(35, 13);
		this.label3.TabIndex = 5;
		this.label3.Text = "Email:";
		// 
		// label4
		// 
		this.label4.AutoSize = true;
		this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label4.Location = new System.Drawing.Point(241, 111);
		this.label4.Margin = new System.Windows.Forms.Padding(3);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(49, 13);
		this.label4.TabIndex = 6;
		this.label4.Text = "Website:";
		// 
		// label5
		// 
		this.label5.AutoSize = true;
		this.label5.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label5.Location = new System.Drawing.Point(241, 130);
		this.label5.Margin = new System.Windows.Forms.Padding(3);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(63, 13);
		this.label5.TabIndex = 7;
		this.label5.Text = "Description:";
		// 
		// txtVersion
		// 
		this.txtVersion.AutoSize = true;
		this.txtVersion.ForeColor = System.Drawing.Color.White;
		this.txtVersion.Location = new System.Drawing.Point(312, 35);
		this.txtVersion.Margin = new System.Windows.Forms.Padding(3);
		this.txtVersion.Name = "txtVersion";
		this.txtVersion.Size = new System.Drawing.Size(47, 13);
		this.txtVersion.TabIndex = 8;
		this.txtVersion.Text = "(version)";
		// 
		// txtAuthor
		// 
		this.txtAuthor.AutoSize = true;
		this.txtAuthor.ForeColor = System.Drawing.Color.White;
		this.txtAuthor.Location = new System.Drawing.Point(312, 73);
		this.txtAuthor.Margin = new System.Windows.Forms.Padding(3);
		this.txtAuthor.Name = "txtAuthor";
		this.txtAuthor.Size = new System.Drawing.Size(43, 13);
		this.txtAuthor.TabIndex = 9;
		this.txtAuthor.Text = "(author)";
		// 
		// txtEmail
		// 
		this.txtEmail.AutoSize = true;
		this.txtEmail.Cursor = System.Windows.Forms.Cursors.Hand;
		this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtEmail.ForeColor = System.Drawing.Color.Teal;
		this.txtEmail.Location = new System.Drawing.Point(312, 92);
		this.txtEmail.Margin = new System.Windows.Forms.Padding(3);
		this.txtEmail.Name = "txtEmail";
		this.txtEmail.Size = new System.Drawing.Size(37, 13);
		this.txtEmail.TabIndex = 10;
		this.txtEmail.Text = "(email)";
		this.txtEmail.Click += new System.EventHandler(this.txtEmail_Click);
		// 
		// txtWebsite
		// 
		this.txtWebsite.AutoSize = true;
		this.txtWebsite.Cursor = System.Windows.Forms.Cursors.Hand;
		this.txtWebsite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtWebsite.ForeColor = System.Drawing.Color.Teal;
		this.txtWebsite.Location = new System.Drawing.Point(312, 111);
		this.txtWebsite.Margin = new System.Windows.Forms.Padding(3);
		this.txtWebsite.Name = "txtWebsite";
		this.txtWebsite.Size = new System.Drawing.Size(49, 13);
		this.txtWebsite.TabIndex = 11;
		this.txtWebsite.Text = "(website)";
		this.txtWebsite.Click += new System.EventHandler(this.txtWebsite_Click);
		// 
		// txtDescription
		// 
		this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtDescription.ForeColor = System.Drawing.Color.White;
		this.txtDescription.Location = new System.Drawing.Point(310, 130);
		this.txtDescription.Margin = new System.Windows.Forms.Padding(3);
		this.txtDescription.Name = "txtDescription";
		this.txtDescription.Size = new System.Drawing.Size(240, 270);
		this.txtDescription.TabIndex = 12;
		this.txtDescription.Text = "(description)";
		// 
		// btnLoad
		// 
		this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.btnLoad.BackColor = System.Drawing.Color.Black;
		this.btnLoad.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnLoad.Location = new System.Drawing.Point(475, 406);
		this.btnLoad.Name = "btnLoad";
		this.btnLoad.Size = new System.Drawing.Size(75, 23);
		this.btnLoad.TabIndex = 13;
		this.btnLoad.Text = "Load";
		this.btnLoad.UseVisualStyleBackColor = false;
		this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
		// 
		// btnCancel
		// 
		this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.btnCancel.BackColor = System.Drawing.Color.Black;
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnCancel.Location = new System.Drawing.Point(315, 406);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(75, 23);
		this.btnCancel.TabIndex = 14;
		this.btnCancel.Text = "Cancel";
		this.btnCancel.UseVisualStyleBackColor = false;
		this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
		// 
		// txtFolder
		// 
		this.txtFolder.AutoSize = true;
		this.txtFolder.ForeColor = System.Drawing.Color.White;
		this.txtFolder.Location = new System.Drawing.Point(312, 54);
		this.txtFolder.Margin = new System.Windows.Forms.Padding(3);
		this.txtFolder.Name = "txtFolder";
		this.txtFolder.Size = new System.Drawing.Size(39, 13);
		this.txtFolder.TabIndex = 16;
		this.txtFolder.Text = "(folder)";
		// 
		// label7
		// 
		this.label7.AutoSize = true;
		this.label7.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label7.Location = new System.Drawing.Point(241, 54);
		this.label7.Margin = new System.Windows.Forms.Padding(3);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(39, 13);
		this.label7.TabIndex = 15;
		this.label7.Text = "Folder:";
		// 
		// btnEdit
		// 
		this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.btnEdit.BackColor = System.Drawing.Color.Black;
		this.btnEdit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnEdit.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnEdit.Location = new System.Drawing.Point(394, 406);
		this.btnEdit.Name = "btnEdit";
		this.btnEdit.Size = new System.Drawing.Size(75, 23);
		this.btnEdit.TabIndex = 17;
		this.btnEdit.Text = "Edit";
		this.btnEdit.UseVisualStyleBackColor = false;
		this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
		// 
		// ModPickerForm
		// 
		this.AcceptButton = this.btnLoad;
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.CancelButton = this.btnCancel;
		this.ClientSize = new System.Drawing.Size(562, 441);
		this.Controls.Add(this.btnEdit);
		this.Controls.Add(this.txtFolder);
		this.Controls.Add(this.label7);
		this.Controls.Add(this.btnCancel);
		this.Controls.Add(this.btnLoad);
		this.Controls.Add(this.txtDescription);
		this.Controls.Add(this.txtWebsite);
		this.Controls.Add(this.txtEmail);
		this.Controls.Add(this.txtAuthor);
		this.Controls.Add(this.txtVersion);
		this.Controls.Add(this.label5);
		this.Controls.Add(this.label4);
		this.Controls.Add(this.label3);
		this.Controls.Add(this.label2);
		this.Controls.Add(this.label1);
		this.Controls.Add(this.txtName);
		this.Controls.Add(this.gamePanel1);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "ModPickerForm";
		this.ShowInTaskbar = false;
		this.Text = "FrEee";
		this.gamePanel1.ResumeLayout(false);
		this.ResumeLayout(false);
		this.PerformLayout();

	}

	#endregion

	private Controls.GamePanel gamePanel1;
	private System.Windows.Forms.ListView lstMods;
	private System.Windows.Forms.Label txtName;
	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.Label label2;
	private System.Windows.Forms.Label label3;
	private System.Windows.Forms.Label label4;
	private System.Windows.Forms.Label label5;
	private System.Windows.Forms.Label txtVersion;
	private System.Windows.Forms.Label txtAuthor;
	private System.Windows.Forms.Label txtEmail;
	private System.Windows.Forms.Label txtWebsite;
	private System.Windows.Forms.Label txtDescription;
	private Controls.GameButton btnLoad;
	private Controls.GameButton btnCancel;
	private System.Windows.Forms.Label txtFolder;
	private System.Windows.Forms.Label label7;
	private Controls.GameButton btnEdit;
}