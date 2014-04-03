namespace FrEee.WinForms.MogreCombatRender.StrategiesDesigner
{
    partial class StratMainForm 
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
            this.tableLayoutPanel1 = new FrEee.WinForms.Controls.GameTableLayoutPanel();
            this.btnHelmAdd = new FrEee.WinForms.Controls.GameButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pBx = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBx)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel1.Controls.Add(this.btnHelmAdd, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.pBx, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(669, 427);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // btnHelmAdd
            // 
            this.btnHelmAdd.BackColor = System.Drawing.Color.Black;
            this.btnHelmAdd.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.btnHelmAdd.Location = new System.Drawing.Point(85, 3);
            this.btnHelmAdd.Name = "btnHelmAdd";
            this.btnHelmAdd.Size = new System.Drawing.Size(75, 23);
            this.btnHelmAdd.TabIndex = 4;
            this.btnHelmAdd.Text = "Add";
            this.btnHelmAdd.UseVisualStyleBackColor = true;
            this.btnHelmAdd.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(590, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "WayPoint";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(590, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "FireControl";
            // 
            // pBx
            // 
            this.pBx.BackColor = System.Drawing.Color.Black;
            this.tableLayoutPanel1.SetColumnSpan(this.pBx, 2);
            this.pBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pBx.Location = new System.Drawing.Point(3, 45);
            this.pBx.Name = "pBx";
            this.tableLayoutPanel1.SetRowSpan(this.pBx, 3);
            this.pBx.Size = new System.Drawing.Size(561, 379);
            this.pBx.TabIndex = 6;
            this.pBx.TabStop = false;
            // 
            // StratMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 427);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "StratMainForm";
            this.Text = "StratMainForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBx)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        //private FrEee.WinForms.Controls.GameTableLayoutPanel
        private FrEee.WinForms.Controls.GameTableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private FrEee.WinForms.Controls.GameButton btnHelmAdd;
        private System.Windows.Forms.PictureBox pBx;
        private System.Windows.Forms.Label label3;
    }
}