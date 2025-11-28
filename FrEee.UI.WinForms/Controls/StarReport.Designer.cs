using FrEee.UI.WinForms.Controls.Blazor;
using FrEee.UI.WinForms.Controls;

namespace FrEee.UI.WinForms.Controls;

partial class StarReport
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

	#region Component Designer generated code

	/// <summary> 
	/// Required method for Designer support - do not modify 
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
		System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Star is unstable and could explode at any time.");
		this.txtAge = new System.Windows.Forms.Label();
		this.lblAge = new System.Windows.Forms.Label();
		this.txtBrightness = new System.Windows.Forms.Label();
		this.lblBrightness = new System.Windows.Forms.Label();
		this.txtSizeColor = new System.Windows.Forms.Label();
		this.txtName = new System.Windows.Forms.Label();
		this.lstAbilities = new System.Windows.Forms.ListView();
		this.lblDescription = new System.Windows.Forms.Label();
		this.txtDescription = new System.Windows.Forms.Label();
		this.picPortrait = new FrEee.UI.WinForms.Controls.GamePictureBox();
		this.label1 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)(this.picPortrait)).BeginInit();
		this.SuspendLayout();
		// 
		// txtAge
		// 
		this.txtAge.AutoSize = true;
		this.txtAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtAge.Location = new System.Drawing.Point(163, 105);
		this.txtAge.Name = "txtAge";
		this.txtAge.Size = new System.Drawing.Size(26, 15);
		this.txtAge.TabIndex = 26;
		this.txtAge.Text = "Old";
		// 
		// lblAge
		// 
		this.lblAge.AutoSize = true;
		this.lblAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblAge.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblAge.Location = new System.Drawing.Point(148, 90);
		this.lblAge.Name = "lblAge";
		this.lblAge.Size = new System.Drawing.Size(28, 15);
		this.lblAge.TabIndex = 25;
		this.lblAge.Text = "Age";
		// 
		// txtBrightness
		// 
		this.txtBrightness.AutoSize = true;
		this.txtBrightness.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtBrightness.Location = new System.Drawing.Point(163, 75);
		this.txtBrightness.Name = "txtBrightness";
		this.txtBrightness.Size = new System.Drawing.Size(30, 15);
		this.txtBrightness.TabIndex = 24;
		this.txtBrightness.Text = "Dim";
		// 
		// lblBrightness
		// 
		this.lblBrightness.AutoSize = true;
		this.lblBrightness.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblBrightness.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblBrightness.Location = new System.Drawing.Point(148, 60);
		this.lblBrightness.Name = "lblBrightness";
		this.lblBrightness.Size = new System.Drawing.Size(65, 15);
		this.lblBrightness.TabIndex = 23;
		this.lblBrightness.Text = "Brightness";
		// 
		// txtSizeColor
		// 
		this.txtSizeColor.AutoSize = true;
		this.txtSizeColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtSizeColor.Location = new System.Drawing.Point(163, 28);
		this.txtSizeColor.Name = "txtSizeColor";
		this.txtSizeColor.Size = new System.Drawing.Size(90, 15);
		this.txtSizeColor.TabIndex = 22;
		this.txtSizeColor.Text = "Large Red Star";
		// 
		// txtName
		// 
		this.txtName.AutoSize = true;
		this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
		this.txtName.Location = new System.Drawing.Point(147, 8);
		this.txtName.Name = "txtName";
		this.txtName.Size = new System.Drawing.Size(93, 20);
		this.txtName.TabIndex = 21;
		this.txtName.Text = "Tudran Star";
		// 
		// lstAbilities
		// 
		this.lstAbilities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.lstAbilities.BackColor = System.Drawing.Color.Black;
		this.lstAbilities.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.lstAbilities.ForeColor = System.Drawing.Color.White;
		this.lstAbilities.HideSelection = false;
		this.lstAbilities.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
		this.lstAbilities.Location = new System.Drawing.Point(4, 169);
		this.lstAbilities.Name = "lstAbilities";
		this.lstAbilities.Size = new System.Drawing.Size(315, 329);
		this.lstAbilities.TabIndex = 27;
		this.lstAbilities.UseCompatibleStateImageBehavior = false;
		this.lstAbilities.View = System.Windows.Forms.View.List;
		// 
		// lblDescription
		// 
		this.lblDescription.AutoSize = true;
		this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.lblDescription.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblDescription.Location = new System.Drawing.Point(148, 120);
		this.lblDescription.Name = "lblDescription";
		this.lblDescription.Size = new System.Drawing.Size(69, 15);
		this.lblDescription.TabIndex = 28;
		this.lblDescription.Text = "Description";
		// 
		// txtDescription
		// 
		this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtDescription.Location = new System.Drawing.Point(163, 135);
		this.txtDescription.Name = "txtDescription";
		this.txtDescription.Size = new System.Drawing.Size(156, 27);
		this.txtDescription.TabIndex = 29;
		this.txtDescription.Text = "An old red star.";
		// 
		// picPortrait
		// 
		this.picPortrait.Location = new System.Drawing.Point(3, 34);
		this.picPortrait.Name = "picPortrait";
		this.picPortrait.Size = new System.Drawing.Size(128, 128);
		this.picPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.picPortrait.TabIndex = 52;
		this.picPortrait.TabStop = false;
		this.picPortrait.Click += new System.EventHandler(this.picPortrait_Click);
		// 
		// label1
		// 
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.label1.Location = new System.Drawing.Point(166, 45);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(47, 15);
		this.label1.TabIndex = 56;
		this.label1.Text = "Current";
		// 
		// StarReport
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.Controls.Add(this.label1);
		this.Controls.Add(this.picPortrait);
		this.Controls.Add(this.txtDescription);
		this.Controls.Add(this.lblDescription);
		this.Controls.Add(this.lstAbilities);
		this.Controls.Add(this.txtAge);
		this.Controls.Add(this.lblAge);
		this.Controls.Add(this.txtBrightness);
		this.Controls.Add(this.lblBrightness);
		this.Controls.Add(this.txtSizeColor);
		this.Controls.Add(this.txtName);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "StarReport";
		this.Size = new System.Drawing.Size(322, 501);
		((System.ComponentModel.ISupportInitialize)(this.picPortrait)).EndInit();
		this.ResumeLayout(false);
		this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Label txtAge;
	private System.Windows.Forms.Label lblAge;
	private System.Windows.Forms.Label txtBrightness;
	private System.Windows.Forms.Label lblBrightness;
	private System.Windows.Forms.Label txtSizeColor;
	private System.Windows.Forms.Label txtName;
	private System.Windows.Forms.ListView lstAbilities;
	private System.Windows.Forms.Label lblDescription;
	private System.Windows.Forms.Label txtDescription;
	private GamePictureBox picPortrait;
	private System.Windows.Forms.Label label1;
}
