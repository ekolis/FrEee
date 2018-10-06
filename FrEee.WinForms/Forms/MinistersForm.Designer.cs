namespace FrEee.WinForms.Forms
{
	partial class MinistersForm
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
			this.btnClose = new FrEee.WinForms.Controls.GameButton();
			this.lstMinisters = new System.Windows.Forms.CheckedListBox();
			this.btnDisableAll = new FrEee.WinForms.Controls.GameButton();
			this.btnEnableAll = new FrEee.WinForms.Controls.GameButton();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.BackColor = System.Drawing.Color.Black;
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnClose.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnClose.Location = new System.Drawing.Point(622, 649);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(100, 23);
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// lstMinisters
			// 
			this.lstMinisters.BackColor = System.Drawing.Color.Black;
			this.lstMinisters.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstMinisters.CheckOnClick = true;
			this.lstMinisters.ForeColor = System.Drawing.Color.White;
			this.lstMinisters.FormattingEnabled = true;
			this.lstMinisters.Location = new System.Drawing.Point(13, 13);
			this.lstMinisters.Name = "lstMinisters";
			this.lstMinisters.Size = new System.Drawing.Size(709, 630);
			this.lstMinisters.TabIndex = 3;
			// 
			// btnDisableAll
			// 
			this.btnDisableAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDisableAll.BackColor = System.Drawing.Color.Black;
			this.btnDisableAll.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnDisableAll.Location = new System.Drawing.Point(516, 649);
			this.btnDisableAll.Name = "btnDisableAll";
			this.btnDisableAll.Size = new System.Drawing.Size(100, 23);
			this.btnDisableAll.TabIndex = 4;
			this.btnDisableAll.Text = "Disable All";
			this.btnDisableAll.UseVisualStyleBackColor = false;
			this.btnDisableAll.Click += new System.EventHandler(this.btnDisableAll_Click);
			// 
			// btnEnableAll
			// 
			this.btnEnableAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEnableAll.BackColor = System.Drawing.Color.Black;
			this.btnEnableAll.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnEnableAll.Location = new System.Drawing.Point(410, 649);
			this.btnEnableAll.Name = "btnEnableAll";
			this.btnEnableAll.Size = new System.Drawing.Size(100, 23);
			this.btnEnableAll.TabIndex = 5;
			this.btnEnableAll.Text = "Enable All";
			this.btnEnableAll.UseVisualStyleBackColor = false;
			this.btnEnableAll.Click += new System.EventHandler(this.btnEnableAll_Click);
			// 
			// MinistersForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(734, 688);
			this.Controls.Add(this.btnEnableAll);
			this.Controls.Add(this.btnDisableAll);
			this.Controls.Add(this.lstMinisters);
			this.Controls.Add(this.btnClose);
			this.Name = "MinistersForm";
			this.Text = "Ministers";
			this.Load += new System.EventHandler(this.MinistersForm_Load);
			this.ResumeLayout(false);

		}

		#endregion
		private Controls.GameButton btnClose;
		private System.Windows.Forms.CheckedListBox lstMinisters;
		private Controls.GameButton btnDisableAll;
		private Controls.GameButton btnEnableAll;
	}
}