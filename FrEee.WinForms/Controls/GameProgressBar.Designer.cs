namespace FrEee.WinForms.Controls
{
	partial class GameProgressBar
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.lblRight = new System.Windows.Forms.Label();
			this.lblCenter = new System.Windows.Forms.Label();
			this.lblLeft = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.lblRight, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.lblCenter, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.lblLeft, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(362, 26);
			this.tableLayoutPanel1.TabIndex = 3;
			// 
			// lblRight
			// 
			this.lblRight.BackColor = System.Drawing.Color.Transparent;
			this.lblRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblRight.Location = new System.Drawing.Point(231, 0);
			this.lblRight.Margin = new System.Windows.Forms.Padding(0);
			this.lblRight.Name = "lblRight";
			this.lblRight.Size = new System.Drawing.Size(131, 26);
			this.lblRight.TabIndex = 4;
			this.lblRight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblCenter
			// 
			this.lblCenter.BackColor = System.Drawing.Color.Transparent;
			this.lblCenter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblCenter.Location = new System.Drawing.Point(131, 0);
			this.lblCenter.Margin = new System.Windows.Forms.Padding(0);
			this.lblCenter.Name = "lblCenter";
			this.lblCenter.Size = new System.Drawing.Size(100, 26);
			this.lblCenter.TabIndex = 3;
			this.lblCenter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblLeft
			// 
			this.lblLeft.BackColor = System.Drawing.Color.Transparent;
			this.lblLeft.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblLeft.Location = new System.Drawing.Point(0, 0);
			this.lblLeft.Margin = new System.Windows.Forms.Padding(0);
			this.lblLeft.Name = "lblLeft";
			this.lblLeft.Size = new System.Drawing.Size(131, 26);
			this.lblLeft.TabIndex = 1;
			this.lblLeft.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// GameProgressBar
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.Controls.Add(this.tableLayoutPanel1);
			this.ForeColor = System.Drawing.Color.White;
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "GameProgressBar";
			this.Size = new System.Drawing.Size(368, 32);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label lblRight;
		private System.Windows.Forms.Label lblCenter;
		private System.Windows.Forms.Label lblLeft;

	}
}
