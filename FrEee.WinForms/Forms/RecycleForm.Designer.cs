namespace FrEee.WinForms.Forms
{
	partial class RecycleForm
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
			this.treeVehicles = new System.Windows.Forms.TreeView();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// treeVehicles
			// 
			this.treeVehicles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.treeVehicles.BackColor = System.Drawing.Color.Black;
			this.treeVehicles.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.treeVehicles.CheckBoxes = true;
			this.treeVehicles.ForeColor = System.Drawing.Color.White;
			this.treeVehicles.HideSelection = false;
			this.treeVehicles.Location = new System.Drawing.Point(8, 24);
			this.treeVehicles.Name = "treeVehicles";
			this.treeVehicles.Size = new System.Drawing.Size(286, 491);
			this.treeVehicles.TabIndex = 19;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label1.Location = new System.Drawing.Point(5, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(215, 16);
			this.label1.TabIndex = 20;
			this.label1.Text = "Vehicles/Colonies/Cargo/Facilities";
			// 
			// RecycleForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(764, 519);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.treeVehicles);
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "RecycleForm";
			this.Text = "Recycle";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TreeView treeVehicles;
		private System.Windows.Forms.Label label1;
	}
}