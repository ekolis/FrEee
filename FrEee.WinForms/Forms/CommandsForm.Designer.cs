namespace FrEee.WinForms.Forms
{
	partial class CommandsForm
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
			this.lstCommands = new System.Windows.Forms.ListView();
			this.SuspendLayout();
			// 
			// lstCommands
			// 
			this.lstCommands.BackColor = System.Drawing.Color.Black;
			this.lstCommands.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstCommands.ForeColor = System.Drawing.Color.White;
			this.lstCommands.FullRowSelect = true;
			this.lstCommands.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lstCommands.Location = new System.Drawing.Point(0, 0);
			this.lstCommands.Name = "lstCommands";
			this.lstCommands.ShowItemToolTips = true;
			this.lstCommands.Size = new System.Drawing.Size(774, 492);
			this.lstCommands.TabIndex = 0;
			this.lstCommands.UseCompatibleStateImageBehavior = false;
			this.lstCommands.View = System.Windows.Forms.View.Details;
			// 
			// CommandsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(774, 492);
			this.Controls.Add(this.lstCommands);
			this.Name = "CommandsForm";
			this.Text = "Commands";
			this.Load += new System.EventHandler(this.CommandsForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lstCommands;

	}
}