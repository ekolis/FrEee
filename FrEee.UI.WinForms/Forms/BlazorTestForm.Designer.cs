namespace FrEee.UI.WinForms.Forms
{
	partial class BlazorTestForm
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
			panel = new System.Windows.Forms.Panel();
			btnProgressBar = new System.Windows.Forms.Button();
			btnResourceDisplay = new System.Windows.Forms.Button();
			SuspendLayout();
			// 
			// panel
			// 
			panel.Dock = System.Windows.Forms.DockStyle.Bottom;
			panel.Location = new System.Drawing.Point(0, 122);
			panel.Name = "panel";
			panel.Size = new System.Drawing.Size(800, 328);
			panel.TabIndex = 0;
			// 
			// btnProgressBar
			// 
			btnProgressBar.Location = new System.Drawing.Point(23, 13);
			btnProgressBar.Name = "btnProgressBar";
			btnProgressBar.Size = new System.Drawing.Size(187, 23);
			btnProgressBar.TabIndex = 1;
			btnProgressBar.Text = "Progress Bar";
			btnProgressBar.UseVisualStyleBackColor = true;
			btnProgressBar.Click += btnProgressBar_Click;
			// 
			// btnResourceDisplay
			// 
			btnResourceDisplay.Location = new System.Drawing.Point(23, 42);
			btnResourceDisplay.Name = "btnResourceDisplay";
			btnResourceDisplay.Size = new System.Drawing.Size(187, 23);
			btnResourceDisplay.TabIndex = 2;
			btnResourceDisplay.Text = "Resource Dsiplay";
			btnResourceDisplay.UseVisualStyleBackColor = true;
			btnResourceDisplay.Click += btnResourceDisplay_Click;
			// 
			// BlazorTestForm
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(800, 450);
			Controls.Add(btnResourceDisplay);
			Controls.Add(btnProgressBar);
			Controls.Add(panel);
			Name = "BlazorTestForm";
			Text = "BlazorTestForm";
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Panel panel;
		private System.Windows.Forms.Button btnProgressBar;
		private System.Windows.Forms.Button btnResourceDisplay;
	}
}