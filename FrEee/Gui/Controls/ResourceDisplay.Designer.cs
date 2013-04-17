namespace FrEee.Gui.Controls
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
			this.lblAmount = new System.Windows.Forms.Label();
			this.picIcon = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// lblAmount
			// 
			this.lblAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblAmount.Location = new System.Drawing.Point(3, 0);
			this.lblAmount.Name = "lblAmount";
			this.lblAmount.Size = new System.Drawing.Size(74, 20);
			this.lblAmount.TabIndex = 0;
			this.lblAmount.Text = "0 (+0)";
			this.lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// picIcon
			// 
			this.picIcon.Location = new System.Drawing.Point(83, 0);
			this.picIcon.Name = "picIcon";
			this.picIcon.Size = new System.Drawing.Size(20, 20);
			this.picIcon.TabIndex = 1;
			this.picIcon.TabStop = false;
			// 
			// GameResourceDisplay
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.Controls.Add(this.picIcon);
			this.Controls.Add(this.lblAmount);
			this.ForeColor = System.Drawing.Color.White;
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "GameResourceDisplay";
			this.Size = new System.Drawing.Size(106, 20);
			((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblAmount;
		private System.Windows.Forms.PictureBox picIcon;
	}
}
