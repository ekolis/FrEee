namespace FrEee.WinForms.Forms;

partial class ScoresForm
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
		this.graph = new FrEee.WinForms.Controls.LineGraph();
		this.grid = new FrEee.WinForms.Controls.GameGridView();
		this.SuspendLayout();
		// 
		// graph
		// 
		this.graph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.graph.Location = new System.Drawing.Point(12, 12);
		this.graph.Name = "graph";
		this.graph.RoundMaxToMultipleOfPowerOfTen = true;
		this.graph.Size = new System.Drawing.Size(709, 310);
		this.graph.TabIndex = 0;
		this.graph.Text = "graph";
		this.graph.Title = null;
		// 
		// grid
		// 
		this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
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
		this.grid.Location = new System.Drawing.Point(12, 328);
		this.grid.Name = "grid";
		this.grid.PrependMenuItems = null;
		this.grid.ResetGridConfigs = null;
		this.grid.ShowConfigs = false;
		this.grid.Size = new System.Drawing.Size(709, 105);
		this.grid.TabIndex = 1;
		// 
		// ScoresForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.ClientSize = new System.Drawing.Size(733, 445);
		this.Controls.Add(this.grid);
		this.Controls.Add(this.graph);
		this.Name = "ScoresForm";
		this.Text = "Scores";
		this.Load += new System.EventHandler(this.ScoresForm_Load);
		this.ResumeLayout(false);

	}

	#endregion

	private Controls.LineGraph graph;
	private Controls.GameGridView grid;
}