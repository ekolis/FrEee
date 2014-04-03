namespace FrEee.WinForms.MogreCombatRender.StrategiesDesigner
{
    partial class UserControlBaseObj
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
            this.lbl_FunctName = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_FunctName
            // 
            this.lbl_FunctName.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lbl_FunctName, 2);
            this.lbl_FunctName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_FunctName.Location = new System.Drawing.Point(23, 0);
            this.lbl_FunctName.Name = "lbl_FunctName";
            this.lbl_FunctName.Size = new System.Drawing.Size(192, 13);
            this.lbl_FunctName.TabIndex = 0;
            this.lbl_FunctName.Text = "Name";
            this.lbl_FunctName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbl_FunctName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UserControl_System_MouseDown);
            this.lbl_FunctName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UserControl_System_MouseMove);
            this.lbl_FunctName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UserControl_System_MouseUp);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_FunctName, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(239, 20);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // UserControlBasebx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UserControlBasebx";
            this.Size = new System.Drawing.Size(239, 20);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_FunctName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
