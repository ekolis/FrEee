namespace FrEee.Gui.Controls
{
	partial class StarSystemView
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
			this.tblSectors = new FrEee.Gui.Controls.GameTableLayoutPanel();
			this.SuspendLayout();
			// 
			// tblSectors
			// 
			this.tblSectors.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tblSectors.ColumnCount = 2;
			this.tblSectors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tblSectors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tblSectors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tblSectors.Location = new System.Drawing.Point(0, 0);
			this.tblSectors.Name = "tblSectors";
			this.tblSectors.RowCount = 2;
			this.tblSectors.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tblSectors.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tblSectors.Size = new System.Drawing.Size(150, 150);
			this.tblSectors.TabIndex = 0;
			// 
			// StarSystemView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.Controls.Add(this.tblSectors);
			this.DoubleBuffered = true;
			this.Name = "StarSystemView";
			this.ResumeLayout(false);

		}

		#endregion

		private GameTableLayoutPanel tblSectors;


	}
}
