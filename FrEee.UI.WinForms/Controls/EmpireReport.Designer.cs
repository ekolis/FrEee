using FrEee.UI.WinForms.Controls.Blazor;

namespace FrEee.UI.WinForms.Controls;

partial class EmpireReport
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
		this.gameTabControl1 = new FrEee.UI.WinForms.Controls.GameTabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.txtPortrait = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.txtShipset = new System.Windows.Forms.Label();
		this.txtLeader = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtCulture = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.picInsignia = new FrEee.UI.WinForms.Controls.Blazor.GamePictureBox();
		this.txtName = new System.Windows.Forms.Label();
		this.picPortrait = new FrEee.UI.WinForms.Controls.Blazor.GamePictureBox();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.tabPage3 = new System.Windows.Forms.TabPage();
		this.gameTabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.picInsignia)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.picPortrait)).BeginInit();
		this.SuspendLayout();
		// 
		// gameTabControl1
		// 
		this.gameTabControl1.Controls.Add(this.tabPage1);
		this.gameTabControl1.Controls.Add(this.tabPage2);
		this.gameTabControl1.Controls.Add(this.tabPage3);
		this.gameTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gameTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
		this.gameTabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
		this.gameTabControl1.Location = new System.Drawing.Point(0, 0);
		this.gameTabControl1.Name = "gameTabControl1";
		this.gameTabControl1.SelectedIndex = 0;
		this.gameTabControl1.SelectedTabBackColor = System.Drawing.Color.CornflowerBlue;
		this.gameTabControl1.SelectedTabForeColor = System.Drawing.Color.Black;
		this.gameTabControl1.Size = new System.Drawing.Size(401, 458);
		this.gameTabControl1.TabBackColor = System.Drawing.Color.Black;
		this.gameTabControl1.TabForeColor = System.Drawing.Color.CornflowerBlue;
		this.gameTabControl1.TabIndex = 27;
		// 
		// tabPage1
		// 
		this.tabPage1.BackColor = System.Drawing.Color.Black;
		this.tabPage1.Controls.Add(this.txtPortrait);
		this.tabPage1.Controls.Add(this.label7);
		this.tabPage1.Controls.Add(this.txtShipset);
		this.tabPage1.Controls.Add(this.txtLeader);
		this.tabPage1.Controls.Add(this.label3);
		this.tabPage1.Controls.Add(this.label2);
		this.tabPage1.Controls.Add(this.txtCulture);
		this.tabPage1.Controls.Add(this.label4);
		this.tabPage1.Controls.Add(this.picInsignia);
		this.tabPage1.Controls.Add(this.txtName);
		this.tabPage1.Controls.Add(this.picPortrait);
		this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
		this.tabPage1.Location = new System.Drawing.Point(4, 29);
		this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(393, 425);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "General";
		// 
		// txtPortrait
		// 
		this.txtPortrait.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtPortrait.ForeColor = System.Drawing.Color.White;
		this.txtPortrait.Location = new System.Drawing.Point(264, 54);
		this.txtPortrait.Name = "txtPortrait";
		this.txtPortrait.Size = new System.Drawing.Size(128, 18);
		this.txtPortrait.TabIndex = 41;
		this.txtPortrait.Text = "(portrait)";
		this.txtPortrait.TextAlign = System.Drawing.ContentAlignment.TopRight;
		// 
		// label7
		// 
		this.label7.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label7.Location = new System.Drawing.Point(130, 54);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(128, 18);
		this.label7.TabIndex = 40;
		this.label7.Text = "Portrait";
		// 
		// txtShipset
		// 
		this.txtShipset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtShipset.ForeColor = System.Drawing.Color.White;
		this.txtShipset.Location = new System.Drawing.Point(264, 72);
		this.txtShipset.Name = "txtShipset";
		this.txtShipset.Size = new System.Drawing.Size(128, 18);
		this.txtShipset.TabIndex = 38;
		this.txtShipset.Text = "(shipset)";
		this.txtShipset.TextAlign = System.Drawing.ContentAlignment.TopRight;
		// 
		// txtLeader
		// 
		this.txtLeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtLeader.ForeColor = System.Drawing.Color.White;
		this.txtLeader.Location = new System.Drawing.Point(264, 36);
		this.txtLeader.Name = "txtLeader";
		this.txtLeader.Size = new System.Drawing.Size(128, 18);
		this.txtLeader.TabIndex = 37;
		this.txtLeader.Text = "(leader)";
		this.txtLeader.TextAlign = System.Drawing.ContentAlignment.TopRight;
		// 
		// label3
		// 
		this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label3.Location = new System.Drawing.Point(130, 72);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(128, 18);
		this.label3.TabIndex = 36;
		this.label3.Text = "Shipset";
		// 
		// label2
		// 
		this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label2.Location = new System.Drawing.Point(130, 36);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(128, 18);
		this.label2.TabIndex = 35;
		this.label2.Text = "Leader";
		// 
		// txtCulture
		// 
		this.txtCulture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtCulture.ForeColor = System.Drawing.Color.White;
		this.txtCulture.Location = new System.Drawing.Point(263, 90);
		this.txtCulture.Name = "txtCulture";
		this.txtCulture.Size = new System.Drawing.Size(128, 18);
		this.txtCulture.TabIndex = 34;
		this.txtCulture.Text = "(culture)";
		this.txtCulture.TextAlign = System.Drawing.ContentAlignment.TopRight;
		// 
		// label4
		// 
		this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label4.Location = new System.Drawing.Point(130, 90);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(128, 18);
		this.label4.TabIndex = 33;
		this.label4.Text = "Culture";
		// 
		// picInsignia
		// 
		this.picInsignia.Location = new System.Drawing.Point(130, 1);
		this.picInsignia.Name = "picInsignia";
		this.picInsignia.Size = new System.Drawing.Size(32, 32);
		this.picInsignia.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.picInsignia.TabIndex = 32;
		this.picInsignia.TabStop = false;
		// 
		// txtName
		// 
		this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtName.Location = new System.Drawing.Point(168, 1);
		this.txtName.Name = "txtName";
		this.txtName.Size = new System.Drawing.Size(223, 32);
		this.txtName.TabIndex = 31;
		this.txtName.Text = "(Name)";
		// 
		// picPortrait
		// 
		this.picPortrait.Location = new System.Drawing.Point(-4, 0);
		this.picPortrait.Name = "picPortrait";
		this.picPortrait.Size = new System.Drawing.Size(128, 128);
		this.picPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.picPortrait.TabIndex = 30;
		this.picPortrait.TabStop = false;
		// 
		// tabPage2
		// 
		this.tabPage2.BackColor = System.Drawing.Color.Black;
		this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
		this.tabPage2.Location = new System.Drawing.Point(4, 29);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(393, 425);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "Race";
		// 
		// tabPage3
		// 
		this.tabPage3.BackColor = System.Drawing.Color.Black;
		this.tabPage3.Location = new System.Drawing.Point(4, 29);
		this.tabPage3.Name = "tabPage3";
		this.tabPage3.Size = new System.Drawing.Size(393, 425);
		this.tabPage3.TabIndex = 2;
		this.tabPage3.Text = "Technology";
		// 
		// EmpireReport
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.Controls.Add(this.gameTabControl1);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "EmpireReport";
		this.Size = new System.Drawing.Size(401, 458);
		this.gameTabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)(this.picInsignia)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.picPortrait)).EndInit();
		this.ResumeLayout(false);

	}

	#endregion

	private GameTabControl gameTabControl1;
	private System.Windows.Forms.TabPage tabPage1;
	private System.Windows.Forms.TabPage tabPage2;
	private System.Windows.Forms.Label txtPortrait;
	private System.Windows.Forms.Label label7;
	private System.Windows.Forms.Label txtShipset;
	private System.Windows.Forms.Label txtLeader;
	private System.Windows.Forms.Label label3;
	private System.Windows.Forms.Label label2;
	private System.Windows.Forms.Label txtCulture;
	private System.Windows.Forms.Label label4;
	private GamePictureBox picInsignia;
	private System.Windows.Forms.Label txtName;
	private GamePictureBox picPortrait;
	private System.Windows.Forms.TabPage tabPage3;
}
