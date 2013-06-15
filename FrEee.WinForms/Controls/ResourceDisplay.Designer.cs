namespace FrEee.WinForms.Controls
{
	partial class ResourceDisplay
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
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.lblAmount = new System.Windows.Forms.Label();
			this.picIcon = new System.Windows.Forms.PictureBox();
			this.flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.picIcon);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.MaximumSize = new System.Drawing.Size(9999, 20);
			this.flowLayoutPanel1.MinimumSize = new System.Drawing.Size(0, 20);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(128, 20);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// lblAmount
			// 
			this.lblAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblAmount.Location = new System.Drawing.Point(0, 0);
			this.lblAmount.Margin = new System.Windows.Forms.Padding(3);
			this.lblAmount.MaximumSize = new System.Drawing.Size(9999, 20);
			this.lblAmount.MinimumSize = new System.Drawing.Size(0, 20);
			this.lblAmount.Name = "lblAmount";
			this.lblAmount.Size = new System.Drawing.Size(93, 20);
			this.lblAmount.TabIndex = 4;
			this.lblAmount.Text = "50.00k (+50.00k)";
			this.lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// picIcon
			// 
			this.picIcon.Location = new System.Drawing.Point(108, 0);
			this.picIcon.Margin = new System.Windows.Forms.Padding(0);
			this.picIcon.Name = "picIcon";
			this.picIcon.Size = new System.Drawing.Size(20, 20);
			this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picIcon.TabIndex = 6;
			this.picIcon.TabStop = false;
			// 
			// ResourceDisplay
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.Controls.Add(this.lblAmount);
			this.Controls.Add(this.flowLayoutPanel1);
			this.ForeColor = System.Drawing.Color.White;
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "ResourceDisplay";
			this.Size = new System.Drawing.Size(128, 20);
			this.SizeChanged += new System.EventHandler(this.ResourceDisplay_SizeChanged);
			this.flowLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.PictureBox picIcon;
		private System.Windows.Forms.Label lblAmount;

	}
}
