namespace FrEee.WinForms.Forms;

partial class BattleResultsForm
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
		this.grid = new FrEee.WinForms.Controls.GameGridView();
		this.btnReplay = new FrEee.WinForms.Controls.GameButton();
		this.btnClose = new FrEee.WinForms.Controls.GameButton();
		this.btnGoTo = new FrEee.WinForms.Controls.GameButton();
		this.SuspendLayout();
		// 
		// grid
		// 
		this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.grid.AppendMenuItems = null;
		this.grid.BackColor = System.Drawing.Color.Black;
		this.grid.CreateDefaultGridConfig = null;
		this.grid.CurrentGridConfig = null;
		this.grid.Data = new object[0];
		this.grid.DataType = typeof(object);
		this.grid.ForeColor = System.Drawing.Color.White;
		this.grid.GridConfigs = null;
		this.grid.LoadCurrentGridConfig = null;
		this.grid.LoadGridConfigs = null;
		this.grid.Location = new System.Drawing.Point(12, 12);
		this.grid.Name = "grid";
		this.grid.PrependMenuItems = null;
		this.grid.ResetGridConfigs = null;
		this.grid.ShowConfigs = false;
		this.grid.Size = new System.Drawing.Size(671, 605);
		this.grid.TabIndex = 2;
		// 
		// btnReplay
		// 
		this.btnReplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.btnReplay.BackColor = System.Drawing.Color.Black;
		this.btnReplay.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnReplay.Location = new System.Drawing.Point(527, 623);
		this.btnReplay.Name = "btnReplay";
		this.btnReplay.Size = new System.Drawing.Size(75, 23);
		this.btnReplay.TabIndex = 1;
		this.btnReplay.Text = "Replay";
		this.btnReplay.UseVisualStyleBackColor = false;
		this.btnReplay.Click += new System.EventHandler(this.btnReplay_Click);
		// 
		// btnClose
		// 
		this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.btnClose.BackColor = System.Drawing.Color.Black;
		this.btnClose.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnClose.Location = new System.Drawing.Point(608, 623);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(75, 23);
		this.btnClose.TabIndex = 0;
		this.btnClose.Text = "Close";
		this.btnClose.UseVisualStyleBackColor = false;
		this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
		// 
		// btnGoTo
		// 
		this.btnGoTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.btnGoTo.BackColor = System.Drawing.Color.Black;
		this.btnGoTo.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnGoTo.Location = new System.Drawing.Point(446, 623);
		this.btnGoTo.Name = "btnGoTo";
		this.btnGoTo.Size = new System.Drawing.Size(75, 23);
		this.btnGoTo.TabIndex = 3;
		this.btnGoTo.Text = "Go To";
		this.btnGoTo.UseVisualStyleBackColor = false;
		this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
		// 
		// BattleResultsForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.ClientSize = new System.Drawing.Size(695, 658);
		this.Controls.Add(this.btnGoTo);
		this.Controls.Add(this.grid);
		this.Controls.Add(this.btnReplay);
		this.Controls.Add(this.btnClose);
		this.Name = "BattleResultsForm";
		this.Text = "Battle Results";
		this.ResumeLayout(false);

	}

	#endregion

	private Controls.GameButton btnClose;
	private Controls.GameButton btnReplay;
	private Controls.GameGridView grid;
	private Controls.GameButton btnGoTo;
}