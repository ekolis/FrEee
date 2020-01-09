namespace FrEee.WinForms.Controls
{
	partial class StormReport
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
			this.abilityTreeView = new FrEee.WinForms.Controls.AbilityTreeView();
			this.picPortrait = new FrEee.WinForms.Controls.GamePictureBox();
			this.txtDescription = new System.Windows.Forms.Label();
			this.txtSize = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.Label();
			this.txtAge = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.picPortrait)).BeginInit();
			this.SuspendLayout();
			// 
			// abilityTreeView
			// 
			this.abilityTreeView.Abilities = null;
			this.abilityTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.abilityTreeView.BackColor = System.Drawing.Color.Black;
			this.abilityTreeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.abilityTreeView.ForeColor = System.Drawing.Color.White;
			this.abilityTreeView.IntrinsicAbilities = null;
			this.abilityTreeView.Location = new System.Drawing.Point(3, 199);
			this.abilityTreeView.Name = "abilityTreeView";
			this.abilityTreeView.Size = new System.Drawing.Size(314, 257);
			this.abilityTreeView.TabIndex = 59;
			// 
			// picPortrait
			// 
			this.picPortrait.Location = new System.Drawing.Point(3, 29);
			this.picPortrait.Name = "picPortrait";
			this.picPortrait.Size = new System.Drawing.Size(128, 128);
			this.picPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picPortrait.TabIndex = 58;
			this.picPortrait.TabStop = false;
			// 
			// txtDescription
			// 
			this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDescription.Location = new System.Drawing.Point(7, 160);
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(296, 36);
			this.txtDescription.TabIndex = 57;
			this.txtDescription.Text = "A storm composed of electrostatic gases.";
			// 
			// txtSize
			// 
			this.txtSize.AutoSize = true;
			this.txtSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSize.Location = new System.Drawing.Point(156, 23);
			this.txtSize.Name = "txtSize";
			this.txtSize.Size = new System.Drawing.Size(89, 15);
			this.txtSize.TabIndex = 56;
			this.txtSize.Text = "Medium Storm";
			// 
			// txtName
			// 
			this.txtName.AutoSize = true;
			this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtName.Location = new System.Drawing.Point(140, 3);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(52, 20);
			this.txtName.TabIndex = 55;
			this.txtName.Text = "Storm";
			// 
			// txtAge
			// 
			this.txtAge.AutoSize = true;
			this.txtAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtAge.Location = new System.Drawing.Point(156, 38);
			this.txtAge.Name = "txtAge";
			this.txtAge.Size = new System.Drawing.Size(47, 15);
			this.txtAge.TabIndex = 60;
			this.txtAge.Text = "Current";
			// 
			// StormReport
			// 
			this.AutoScroll = true;
			this.BackColor = System.Drawing.Color.Black;
			this.Controls.Add(this.txtAge);
			this.Controls.Add(this.abilityTreeView);
			this.Controls.Add(this.picPortrait);
			this.Controls.Add(this.txtDescription);
			this.Controls.Add(this.txtSize);
			this.Controls.Add(this.txtName);
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "StormReport";
			this.Size = new System.Drawing.Size(320, 459);
			((System.ComponentModel.ISupportInitialize)(this.picPortrait)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private AbilityTreeView abilityTreeView;
		private GamePictureBox picPortrait;
		private System.Windows.Forms.Label txtDescription;
		private System.Windows.Forms.Label txtSize;
		private System.Windows.Forms.Label txtName;
		private System.Windows.Forms.Label txtAge;

	}
}
