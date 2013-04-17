namespace FrEee.Gui.Controls
{
	partial class SectorView
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
			this.picIcon = new System.Windows.Forms.PictureBox();
			this.picOwnerFlag = new System.Windows.Forms.PictureBox();
			this.lblObjectCount = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picOwnerFlag)).BeginInit();
			this.SuspendLayout();
			// 
			// picIcon
			// 
			this.picIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.picIcon.BackColor = System.Drawing.Color.Transparent;
			this.picIcon.Location = new System.Drawing.Point(8, 8);
			this.picIcon.Name = "picIcon";
			this.picIcon.Size = new System.Drawing.Size(32, 32);
			this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picIcon.TabIndex = 0;
			this.picIcon.TabStop = false;
			// 
			// picOwnerFlag
			// 
			this.picOwnerFlag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.picOwnerFlag.BackColor = System.Drawing.Color.Transparent;
			this.picOwnerFlag.Location = new System.Drawing.Point(29, 3);
			this.picOwnerFlag.Name = "picOwnerFlag";
			this.picOwnerFlag.Size = new System.Drawing.Size(16, 12);
			this.picOwnerFlag.TabIndex = 1;
			this.picOwnerFlag.TabStop = false;
			// 
			// lblObjectCount
			// 
			this.lblObjectCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblObjectCount.AutoSize = true;
			this.lblObjectCount.BackColor = System.Drawing.Color.Transparent;
			this.lblObjectCount.ForeColor = System.Drawing.Color.White;
			this.lblObjectCount.Location = new System.Drawing.Point(42, 30);
			this.lblObjectCount.Name = "lblObjectCount";
			this.lblObjectCount.Size = new System.Drawing.Size(0, 13);
			this.lblObjectCount.TabIndex = 2;
			this.lblObjectCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// SectorView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.lblObjectCount);
			this.Controls.Add(this.picOwnerFlag);
			this.Controls.Add(this.picIcon);
			this.DoubleBuffered = true;
			this.Name = "SectorView";
			this.Size = new System.Drawing.Size(48, 48);
			((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picOwnerFlag)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox picIcon;
		private System.Windows.Forms.PictureBox picOwnerFlag;
		private System.Windows.Forms.Label lblObjectCount;
	}
}
