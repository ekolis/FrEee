namespace FrEee.WinForms.Controls
{
	partial class DesignReport
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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("6x Ion Engine");
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("2x Phased Shield Generator");
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("3x Anti-Proton Beam");
			this.txtRole = new System.Windows.Forms.Label();
			this.lblRole = new System.Windows.Forms.Label();
			this.txtHull = new System.Windows.Forms.Label();
			this.lblHull = new System.Windows.Forms.Label();
			this.txtVehicleType = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.Label();
			this.picPortrait = new FrEee.WinForms.Controls.GamePictureBox();
			this.gamePanel1 = new FrEee.WinForms.Controls.GamePanel();
			this.lstComponents = new System.Windows.Forms.ListView();
			this.txtStrategy = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtDate = new System.Windows.Forms.Label();
			this.lblDate = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.picPortrait)).BeginInit();
			this.gamePanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtRole
			// 
			this.txtRole.AutoSize = true;
			this.txtRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtRole.Location = new System.Drawing.Point(163, 105);
			this.txtRole.Name = "txtRole";
			this.txtRole.Size = new System.Drawing.Size(67, 15);
			this.txtRole.TabIndex = 26;
			this.txtRole.Text = "Attack Ship";
			// 
			// lblRole
			// 
			this.lblRole.AutoSize = true;
			this.lblRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblRole.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblRole.Location = new System.Drawing.Point(148, 90);
			this.lblRole.Name = "lblRole";
			this.lblRole.Size = new System.Drawing.Size(33, 15);
			this.lblRole.TabIndex = 25;
			this.lblRole.Text = "Role";
			// 
			// txtHull
			// 
			this.txtHull.AutoSize = true;
			this.txtHull.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtHull.Location = new System.Drawing.Point(163, 75);
			this.txtHull.Name = "txtHull";
			this.txtHull.Size = new System.Drawing.Size(91, 15);
			this.txtHull.TabIndex = 24;
			this.txtHull.Text = "Cruiser (500kT)";
			// 
			// lblHull
			// 
			this.lblHull.AutoSize = true;
			this.lblHull.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHull.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblHull.Location = new System.Drawing.Point(148, 60);
			this.lblHull.Name = "lblHull";
			this.lblHull.Size = new System.Drawing.Size(29, 15);
			this.lblHull.TabIndex = 23;
			this.lblHull.Text = "Hull";
			// 
			// txtVehicleType
			// 
			this.txtVehicleType.AutoSize = true;
			this.txtVehicleType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtVehicleType.Location = new System.Drawing.Point(163, 28);
			this.txtVehicleType.Name = "txtVehicleType";
			this.txtVehicleType.Size = new System.Drawing.Size(32, 15);
			this.txtVehicleType.TabIndex = 22;
			this.txtVehicleType.Text = "Ship";
			// 
			// txtName
			// 
			this.txtName.AutoSize = true;
			this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
			this.txtName.Location = new System.Drawing.Point(147, 8);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(68, 20);
			this.txtName.TabIndex = 21;
			this.txtName.Text = "Avenger";
			// 
			// picPortrait
			// 
			this.picPortrait.Location = new System.Drawing.Point(3, 34);
			this.picPortrait.Name = "picPortrait";
			this.picPortrait.Size = new System.Drawing.Size(128, 128);
			this.picPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picPortrait.TabIndex = 52;
			this.picPortrait.TabStop = false;
			this.picPortrait.Click += new System.EventHandler(this.picPortrait_Click);
			// 
			// gamePanel1
			// 
			this.gamePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gamePanel1.BackColor = System.Drawing.Color.Black;
			this.gamePanel1.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.gamePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.gamePanel1.Controls.Add(this.lstComponents);
			this.gamePanel1.ForeColor = System.Drawing.Color.White;
			this.gamePanel1.Location = new System.Drawing.Point(3, 183);
			this.gamePanel1.Name = "gamePanel1";
			this.gamePanel1.Padding = new System.Windows.Forms.Padding(3);
			this.gamePanel1.Size = new System.Drawing.Size(316, 315);
			this.gamePanel1.TabIndex = 53;
			// 
			// lstComponents
			// 
			this.lstComponents.BackColor = System.Drawing.Color.Black;
			this.lstComponents.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstComponents.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstComponents.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstComponents.ForeColor = System.Drawing.Color.White;
			this.lstComponents.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
			this.lstComponents.Location = new System.Drawing.Point(3, 3);
			this.lstComponents.Name = "lstComponents";
			this.lstComponents.Size = new System.Drawing.Size(308, 307);
			this.lstComponents.TabIndex = 23;
			this.lstComponents.UseCompatibleStateImageBehavior = false;
			this.lstComponents.View = System.Windows.Forms.View.Tile;
			// 
			// txtStrategy
			// 
			this.txtStrategy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtStrategy.AutoSize = true;
			this.txtStrategy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtStrategy.Location = new System.Drawing.Point(163, 135);
			this.txtStrategy.Name = "txtStrategy";
			this.txtStrategy.Size = new System.Drawing.Size(90, 15);
			this.txtStrategy.TabIndex = 55;
			this.txtStrategy.Text = "Optimal Range";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label2.Location = new System.Drawing.Point(148, 120);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(51, 15);
			this.label2.TabIndex = 54;
			this.label2.Text = "Strategy";
			// 
			// txtDate
			// 
			this.txtDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDate.AutoSize = true;
			this.txtDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDate.Location = new System.Drawing.Point(163, 165);
			this.txtDate.Name = "txtDate";
			this.txtDate.Size = new System.Drawing.Size(45, 15);
			this.txtDate.TabIndex = 59;
			this.txtDate.Text = "2403.6";
			// 
			// lblDate
			// 
			this.lblDate.AutoSize = true;
			this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDate.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblDate.Location = new System.Drawing.Point(148, 150);
			this.lblDate.Name = "lblDate";
			this.lblDate.Size = new System.Drawing.Size(33, 15);
			this.lblDate.TabIndex = 58;
			this.lblDate.Text = "Date";
			// 
			// DesignReport
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.Controls.Add(this.txtDate);
			this.Controls.Add(this.lblDate);
			this.Controls.Add(this.txtStrategy);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.gamePanel1);
			this.Controls.Add(this.picPortrait);
			this.Controls.Add(this.txtRole);
			this.Controls.Add(this.lblRole);
			this.Controls.Add(this.txtHull);
			this.Controls.Add(this.lblHull);
			this.Controls.Add(this.txtVehicleType);
			this.Controls.Add(this.txtName);
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "DesignReport";
			this.Size = new System.Drawing.Size(322, 501);
			((System.ComponentModel.ISupportInitialize)(this.picPortrait)).EndInit();
			this.gamePanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label txtRole;
		private System.Windows.Forms.Label lblRole;
		private System.Windows.Forms.Label txtHull;
		private System.Windows.Forms.Label lblHull;
		private System.Windows.Forms.Label txtVehicleType;
		private System.Windows.Forms.Label txtName;
		private GamePictureBox picPortrait;
		private GamePanel gamePanel1;
		private System.Windows.Forms.ListView lstComponents;
		private System.Windows.Forms.Label txtStrategy;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label txtDate;
		private System.Windows.Forms.Label lblDate;
	}
}
