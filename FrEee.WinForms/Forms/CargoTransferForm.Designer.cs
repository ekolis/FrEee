namespace FrEee.WinForms.Forms
{
    partial class CargoTransferForm
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtFromContainer = new System.Windows.Forms.Label();
			this.ddlToContainer = new System.Windows.Forms.ComboBox();
			this.clTo = new FrEee.WinForms.Controls.CargoList();
			this.clFrom = new FrEee.WinForms.Controls.CargoList();
			this.clLoad = new FrEee.WinForms.Controls.CargoList();
			this.clDrop = new FrEee.WinForms.Controls.CargoList();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.82701F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.17299F));
			this.tableLayoutPanel1.Controls.Add(this.clDrop, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.clLoad, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.clTo, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtFromContainer, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.ddlToContainer, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.clFrom, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 561);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label2.Location = new System.Drawing.Point(362, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(68, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Cargo To:";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label1.Location = new System.Drawing.Point(3, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Cargo From:";
			// 
			// txtFromContainer
			// 
			this.txtFromContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.txtFromContainer.AutoSize = true;
			this.txtFromContainer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtFromContainer.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.txtFromContainer.Location = new System.Drawing.Point(3, 32);
			this.txtFromContainer.Name = "txtFromContainer";
			this.txtFromContainer.Size = new System.Drawing.Size(99, 16);
			this.txtFromContainer.TabIndex = 4;
			this.txtFromContainer.Text = "From Container";
			// 
			// ddlToContainer
			// 
			this.ddlToContainer.DisplayMember = "Name";
			this.ddlToContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ddlToContainer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlToContainer.FormattingEnabled = true;
			this.ddlToContainer.Location = new System.Drawing.Point(362, 27);
			this.ddlToContainer.Name = "ddlToContainer";
			this.ddlToContainer.Size = new System.Drawing.Size(419, 21);
			this.ddlToContainer.TabIndex = 5;
			this.ddlToContainer.SelectedIndexChanged += new System.EventHandler(this.ddlToContainer_SelectedIndexChanged);
			// 
			// clTo
			// 
			this.clTo.BackColor = System.Drawing.Color.Black;
			this.clTo.CargoContainer = null;
			this.clTo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.clTo.ForeColor = System.Drawing.Color.White;
			this.clTo.Location = new System.Drawing.Point(362, 51);
			this.clTo.Name = "clTo";
			this.clTo.Size = new System.Drawing.Size(419, 238);
			this.clTo.TabIndex = 7;
			// 
			// clFrom
			// 
			this.clFrom.BackColor = System.Drawing.Color.Black;
			this.clFrom.CargoContainer = null;
			this.clFrom.Dock = System.Windows.Forms.DockStyle.Fill;
			this.clFrom.ForeColor = System.Drawing.Color.White;
			this.clFrom.Location = new System.Drawing.Point(3, 51);
			this.clFrom.Name = "clFrom";
			this.clFrom.Size = new System.Drawing.Size(353, 238);
			this.clFrom.TabIndex = 6;
			// 
			// clLoad
			// 
			this.clLoad.BackColor = System.Drawing.Color.Black;
			this.clLoad.CargoContainer = null;
			this.clLoad.Dock = System.Windows.Forms.DockStyle.Fill;
			this.clLoad.ForeColor = System.Drawing.Color.White;
			this.clLoad.Location = new System.Drawing.Point(3, 295);
			this.clLoad.Name = "clLoad";
			this.clLoad.Size = new System.Drawing.Size(353, 238);
			this.clLoad.TabIndex = 9;
			// 
			// clDrop
			// 
			this.clDrop.BackColor = System.Drawing.Color.Black;
			this.clDrop.CargoContainer = null;
			this.clDrop.Dock = System.Windows.Forms.DockStyle.Fill;
			this.clDrop.ForeColor = System.Drawing.Color.White;
			this.clDrop.Location = new System.Drawing.Point(362, 295);
			this.clDrop.Name = "clDrop";
			this.clDrop.Size = new System.Drawing.Size(419, 238);
			this.clDrop.TabIndex = 10;
			// 
			// CargoTransferForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(784, 561);
			this.Controls.Add(this.tableLayoutPanel1);
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "CargoTransferForm";
			this.Text = "Transfer Cargo";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label txtFromContainer;
		private System.Windows.Forms.ComboBox ddlToContainer;
		private Controls.CargoList clTo;
		private Controls.CargoList clFrom;
		private Controls.CargoList clDrop;
		private Controls.CargoList clLoad;
    }
}