namespace FrEee.WinForms.Forms
{
    partial class MogreCombatForm
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
			this.gameButton1 = new FrEee.WinForms.Controls.GameButton();
			this.gameButton2 = new FrEee.WinForms.Controls.GameButton();
			this.gameButton3 = new FrEee.WinForms.Controls.GameButton();
			this.gameButton4 = new FrEee.WinForms.Controls.GameButton();
			this.gameButton5 = new FrEee.WinForms.Controls.GameButton();
			this.gameButton6 = new FrEee.WinForms.Controls.GameButton();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// gameButton1
			// 
			this.gameButton1.BackColor = System.Drawing.Color.Black;
			this.gameButton1.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.gameButton1.Location = new System.Drawing.Point(3, 3);
			this.gameButton1.Name = "gameButton1";
			this.gameButton1.Size = new System.Drawing.Size(78, 34);
			this.gameButton1.TabIndex = 6;
			this.gameButton1.Text = "Back to Start";
			this.gameButton1.UseVisualStyleBackColor = false;
			// 
			// gameButton2
			// 
			this.gameButton2.BackColor = System.Drawing.Color.Black;
			this.gameButton2.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.gameButton2.Location = new System.Drawing.Point(87, 3);
			this.gameButton2.Name = "gameButton2";
			this.gameButton2.Size = new System.Drawing.Size(78, 34);
			this.gameButton2.TabIndex = 8;
			this.gameButton2.Text = "Play";
			this.gameButton2.UseVisualStyleBackColor = false;
			// 
			// gameButton3
			// 
			this.gameButton3.BackColor = System.Drawing.Color.Black;
			this.gameButton3.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.gameButton3.Location = new System.Drawing.Point(171, 3);
			this.gameButton3.Name = "gameButton3";
			this.gameButton3.Size = new System.Drawing.Size(87, 34);
			this.gameButton3.TabIndex = 9;
			this.gameButton3.Text = "Fast Foward";
			this.gameButton3.UseVisualStyleBackColor = false;
			// 
			// gameButton4
			// 
			this.gameButton4.BackColor = System.Drawing.Color.Black;
			this.gameButton4.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.gameButton4.Location = new System.Drawing.Point(3, 43);
			this.gameButton4.Name = "gameButton4";
			this.gameButton4.Size = new System.Drawing.Size(78, 34);
			this.gameButton4.TabIndex = 10;
			this.gameButton4.Text = "Previous Ship";
			this.gameButton4.UseVisualStyleBackColor = false;
			// 
			// gameButton5
			// 
			this.gameButton5.BackColor = System.Drawing.Color.Black;
			this.gameButton5.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.gameButton5.Location = new System.Drawing.Point(87, 43);
			this.gameButton5.Name = "gameButton5";
			this.gameButton5.Size = new System.Drawing.Size(78, 34);
			this.gameButton5.TabIndex = 11;
			this.gameButton5.Text = "Free Cam";
			this.gameButton5.UseVisualStyleBackColor = false;
			// 
			// gameButton6
			// 
			this.gameButton6.BackColor = System.Drawing.Color.Black;
			this.gameButton6.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.gameButton6.Location = new System.Drawing.Point(171, 43);
			this.gameButton6.Name = "gameButton6";
			this.gameButton6.Size = new System.Drawing.Size(87, 34);
			this.gameButton6.TabIndex = 12;
			this.gameButton6.Text = "Next Ship";
			this.gameButton6.UseVisualStyleBackColor = false;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(3, 121);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(261, 430);
			this.richTextBox1.TabIndex = 7;
			this.richTextBox1.Text = "";
			// 
			// MogreCombatForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(954, 554);
			this.Controls.Add(this.gameButton1);
			this.Controls.Add(this.gameButton2);
			this.Controls.Add(this.gameButton3);
			this.Controls.Add(this.gameButton4);
			this.Controls.Add(this.gameButton5);
			this.Controls.Add(this.gameButton6);
			this.Controls.Add(this.richTextBox1);
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "MogreCombatForm";
			this.Text = "Space Combat";
			this.ResumeLayout(false);

        }

        #endregion

		private Controls.GameButton gameButton1;
		private Controls.GameButton gameButton2;
		private Controls.GameButton gameButton3;
		private Controls.GameButton gameButton4;
		private Controls.GameButton gameButton5;
		private Controls.GameButton gameButton6;
		private System.Windows.Forms.RichTextBox richTextBox1;

	}
}