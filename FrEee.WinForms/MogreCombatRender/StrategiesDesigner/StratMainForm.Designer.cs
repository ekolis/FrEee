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
            this.pBx = new System.Windows.Forms.PictureBox();
            this.btnAddBlock = new FrEee.WinForms.Controls.GameButton();
            this.btn_SaveStrategy = new FrEee.WinForms.Controls.GameButton();
            this.btn_LoadStrategy = new FrEee.WinForms.Controls.GameButton();
            this.txtBx_Name = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBx)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.tableLayoutPanel1.Controls.Add(this.pBx, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnAddBlock, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_LoadStrategy, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_SaveStrategy, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtBx_Name, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(669, 427);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // pBx
            // 
            this.pBx.BackColor = System.Drawing.Color.Black;
            this.tableLayoutPanel1.SetColumnSpan(this.pBx, 4);
            this.pBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pBx.Location = new System.Drawing.Point(3, 35);
            this.pBx.Name = "pBx";
            this.tableLayoutPanel1.SetRowSpan(this.pBx, 2);
            this.pBx.Size = new System.Drawing.Size(567, 389);
            this.pBx.TabIndex = 6;
            this.pBx.TabStop = false;
            // 
            // btnAddBlock
            // 
            this.btnAddBlock.BackColor = System.Drawing.Color.Black;
            this.btnAddBlock.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.btnAddBlock.Location = new System.Drawing.Point(85, 3);
            this.btnAddBlock.Name = "btnAddBlock";
            this.btnAddBlock.Size = new System.Drawing.Size(75, 23);
            this.btnAddBlock.TabIndex = 4;
            this.btnAddBlock.Text = "Add";
            this.btnAddBlock.UseVisualStyleBackColor = true;
            this.btnAddBlock.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_SaveStrategy
            // 
            this.btn_SaveStrategy.BackColor = System.Drawing.Color.Black;
            this.btn_SaveStrategy.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.btn_SaveStrategy.Location = new System.Drawing.Point(167, 3);
            this.btn_SaveStrategy.Name = "btn_SaveStrategy";
            this.btn_SaveStrategy.Size = new System.Drawing.Size(75, 23);
            this.btn_SaveStrategy.TabIndex = 10;
            this.btn_SaveStrategy.Text = "Save";
            this.btn_SaveStrategy.UseVisualStyleBackColor = true;
            this.btn_SaveStrategy.Click += new System.EventHandler(this.btn_SaveStrategy_Click);
            // 
            // btn_LoadStrategy
            // 
            this.btn_LoadStrategy.BackColor = System.Drawing.Color.Black;
            this.btn_LoadStrategy.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.btn_LoadStrategy.Location = new System.Drawing.Point(3, 3);
            this.btn_LoadStrategy.Name = "btn_LoadStrategy";
            this.btn_LoadStrategy.Size = new System.Drawing.Size(75, 23);
            this.btn_LoadStrategy.TabIndex = 11;
            this.btn_LoadStrategy.Text = "Load";
            this.btn_LoadStrategy.UseVisualStyleBackColor = true;
            this.btn_LoadStrategy.Click += new System.EventHandler(this.btn_LoadStrategy_Click);
            // 
            // txtBx_Name
            // 
            this.txtBx_Name.BackColor = System.Drawing.Color.Black;
            this.txtBx_Name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBx_Name.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.txtBx_Name.Location = new System.Drawing.Point(252, 6);
            this.txtBx_Name.Margin = new System.Windows.Forms.Padding(6);
            this.txtBx_Name.Name = "txtBx_Name";
            this.txtBx_Name.Size = new System.Drawing.Size(315, 20);
            this.txtBx_Name.TabIndex = 12;
            this.txtBx_Name.Text = "Unnamed Strategy";
            // 
            // StratMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(669, 427);
            this.Controls.Add(this.tableLayoutPanel1);
            this.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.Name = "StratMainForm";
            this.Text = "StratMainForm";
            this.Resize += new System.EventHandler(this.StratMainForm_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBx)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        //private FrEee.WinForms.Controls.GameTableLayoutPanel
        private FrEee.WinForms.Controls.GameTableLayoutPanel tableLayoutPanel1;
        private FrEee.WinForms.Controls.GameButton btnAddBlock;
        private System.Windows.Forms.PictureBox pBx;
        private Controls.GameButton btn_SaveStrategy;
        private Controls.GameButton btn_LoadStrategy;
        private System.Windows.Forms.TextBox txtBx_Name;

    }
}