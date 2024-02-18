namespace FrEee.WinForms.Forms;

partial class RecycleForm
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
		this.treeVehicles = new System.Windows.Forms.TreeView();
		this.label1 = new System.Windows.Forms.Label();
		this.btnScrap = new FrEee.WinForms.Controls.GameButton();
		this.btnMothball = new FrEee.WinForms.Controls.GameButton();
		this.btnUnmothball = new FrEee.WinForms.Controls.GameButton();
		this.btnRefit = new FrEee.WinForms.Controls.GameButton();
		this.btnAnalyze = new FrEee.WinForms.Controls.GameButton();
		this.btnOK = new FrEee.WinForms.Controls.GameButton();
		this.btnCancel = new FrEee.WinForms.Controls.GameButton();
		this.SuspendLayout();
		// 
		// treeVehicles
		// 
		this.treeVehicles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.treeVehicles.BackColor = System.Drawing.Color.Black;
		this.treeVehicles.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.treeVehicles.CheckBoxes = true;
		this.treeVehicles.ForeColor = System.Drawing.Color.White;
		this.treeVehicles.HideSelection = false;
		this.treeVehicles.Location = new System.Drawing.Point(8, 24);
		this.treeVehicles.Name = "treeVehicles";
		this.treeVehicles.Size = new System.Drawing.Size(523, 491);
		this.treeVehicles.TabIndex = 19;
		this.treeVehicles.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeVehicles_AfterCheck);
		// 
		// label1
		// 
		this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label1.Location = new System.Drawing.Point(5, 5);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(215, 16);
		this.label1.TabIndex = 20;
		this.label1.Text = "Vehicles/Colonies/Cargo/Facilities";
		// 
		// btnScrap
		// 
		this.btnScrap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.btnScrap.BackColor = System.Drawing.Color.Black;
		this.btnScrap.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnScrap.Location = new System.Drawing.Point(537, 24);
		this.btnScrap.Name = "btnScrap";
		this.btnScrap.Size = new System.Drawing.Size(75, 23);
		this.btnScrap.TabIndex = 21;
		this.btnScrap.Text = "Scrap";
		this.btnScrap.UseVisualStyleBackColor = false;
		this.btnScrap.Click += new System.EventHandler(this.btnScrap_Click);
		// 
		// btnMothball
		// 
		this.btnMothball.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.btnMothball.BackColor = System.Drawing.Color.Black;
		this.btnMothball.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnMothball.Location = new System.Drawing.Point(537, 53);
		this.btnMothball.Name = "btnMothball";
		this.btnMothball.Size = new System.Drawing.Size(75, 23);
		this.btnMothball.TabIndex = 22;
		this.btnMothball.Text = "Mothball";
		this.btnMothball.UseVisualStyleBackColor = false;
		this.btnMothball.Click += new System.EventHandler(this.btnMothball_Click);
		// 
		// btnUnmothball
		// 
		this.btnUnmothball.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.btnUnmothball.BackColor = System.Drawing.Color.Black;
		this.btnUnmothball.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnUnmothball.Location = new System.Drawing.Point(537, 82);
		this.btnUnmothball.Name = "btnUnmothball";
		this.btnUnmothball.Size = new System.Drawing.Size(75, 23);
		this.btnUnmothball.TabIndex = 23;
		this.btnUnmothball.Text = "Unmothball";
		this.btnUnmothball.UseVisualStyleBackColor = false;
		this.btnUnmothball.Click += new System.EventHandler(this.btnUnmothball_Click);
		// 
		// btnRefit
		// 
		this.btnRefit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.btnRefit.BackColor = System.Drawing.Color.Black;
		this.btnRefit.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnRefit.Location = new System.Drawing.Point(538, 111);
		this.btnRefit.Name = "btnRefit";
		this.btnRefit.Size = new System.Drawing.Size(75, 23);
		this.btnRefit.TabIndex = 24;
		this.btnRefit.Text = "Refit";
		this.btnRefit.UseVisualStyleBackColor = false;
		this.btnRefit.Click += new System.EventHandler(this.btnRefit_Click);
		// 
		// btnAnalyze
		// 
		this.btnAnalyze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.btnAnalyze.BackColor = System.Drawing.Color.Black;
		this.btnAnalyze.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnAnalyze.Location = new System.Drawing.Point(537, 140);
		this.btnAnalyze.Name = "btnAnalyze";
		this.btnAnalyze.Size = new System.Drawing.Size(75, 23);
		this.btnAnalyze.TabIndex = 25;
		this.btnAnalyze.Text = "Analyze";
		this.btnAnalyze.UseVisualStyleBackColor = false;
		this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
		// 
		// btnOK
		// 
		this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.btnOK.BackColor = System.Drawing.Color.Black;
		this.btnOK.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnOK.Location = new System.Drawing.Point(537, 484);
		this.btnOK.Name = "btnOK";
		this.btnOK.Size = new System.Drawing.Size(75, 23);
		this.btnOK.TabIndex = 27;
		this.btnOK.Text = "OK";
		this.btnOK.UseVisualStyleBackColor = false;
		this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
		// 
		// btnCancel
		// 
		this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.btnCancel.BackColor = System.Drawing.Color.Black;
		this.btnCancel.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnCancel.Location = new System.Drawing.Point(537, 455);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(75, 23);
		this.btnCancel.TabIndex = 28;
		this.btnCancel.Text = "Cancel";
		this.btnCancel.UseVisualStyleBackColor = false;
		this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
		// 
		// RecycleForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.ClientSize = new System.Drawing.Size(625, 519);
		this.Controls.Add(this.btnCancel);
		this.Controls.Add(this.btnOK);
		this.Controls.Add(this.btnAnalyze);
		this.Controls.Add(this.btnRefit);
		this.Controls.Add(this.btnUnmothball);
		this.Controls.Add(this.btnMothball);
		this.Controls.Add(this.btnScrap);
		this.Controls.Add(this.label1);
		this.Controls.Add(this.treeVehicles);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "RecycleForm";
		this.Text = "Recycle";
		this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RecycleForm_FormClosing);
		this.ResumeLayout(false);
		this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.TreeView treeVehicles;
	private System.Windows.Forms.Label label1;
	private Controls.GameButton btnScrap;
	private Controls.GameButton btnMothball;
	private Controls.GameButton btnUnmothball;
	private Controls.GameButton btnRefit;
	private Controls.GameButton btnAnalyze;
	private Controls.GameButton btnOK;
	private Controls.GameButton btnCancel;
}