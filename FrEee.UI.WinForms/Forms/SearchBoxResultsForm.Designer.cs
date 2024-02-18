namespace FrEee.UI.WinForms.Forms;

partial class SearchBoxResultsForm
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
		this.gamePanel1 = new FrEee.UI.WinForms.Controls.GamePanel();
		this.listView = new System.Windows.Forms.ListView();
		gamePanel1.Controls.Add(listView);
		this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
		this.SuspendLayout();
		// 
		// gamePanel1
		// 
		this.gamePanel1.BackColor = System.Drawing.Color.Black;
		this.gamePanel1.BorderColor = System.Drawing.Color.CornflowerBlue;
		this.gamePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.gamePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gamePanel1.ForeColor = System.Drawing.Color.White;
		this.gamePanel1.Location = new System.Drawing.Point(0, 0);
		this.gamePanel1.Name = "gamePanel1";
		this.gamePanel1.Padding = new System.Windows.Forms.Padding(3);
		this.gamePanel1.Size = new System.Drawing.Size(284, 261);
		this.gamePanel1.TabIndex = 1;
		// 
		// listView
		// 
		this.listView.BackColor = System.Drawing.Color.Black;
		this.listView.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
		this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
		this.listView.ForeColor = System.Drawing.Color.White;
		this.listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
		this.listView.Location = new System.Drawing.Point(3, 3);
		this.listView.Name = "listView";
		this.listView.Size = new System.Drawing.Size(276, 253);
		this.listView.TabIndex = 1;
		this.listView.UseCompatibleStateImageBehavior = false;
		this.listView.View = System.Windows.Forms.View.Details;
		this.listView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView_ItemSelectionChanged);
		this.listView.SizeChanged += new System.EventHandler(this.listView_SizeChanged);
		// 
		// SearchBoxResultsForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.ClientSize = new System.Drawing.Size(284, 261);
		this.Controls.Add(this.gamePanel1);
		this.ForeColor = System.Drawing.Color.White;
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		this.Name = "SearchBoxResultsForm";
		this.ShowInTaskbar = false;
		this.Text = "SearchBoxResultsForm";
		this.ResumeLayout(false);

	}

	#endregion

	private Controls.GamePanel gamePanel1;
	private System.Windows.Forms.ListView listView;
	private System.Windows.Forms.ColumnHeader columnHeader1;
}