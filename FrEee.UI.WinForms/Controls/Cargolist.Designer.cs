namespace FrEee.UI.WinForms.Controls;

    partial class CargoList
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
		this.tree = new System.Windows.Forms.TreeView();
		this.lblTonnage = new System.Windows.Forms.Label();
		this.SuspendLayout();
		// 
		// tree
		// 
		this.tree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.tree.BackColor = System.Drawing.Color.Black;
		this.tree.ForeColor = System.Drawing.Color.White;
		this.tree.Location = new System.Drawing.Point(0, 36);
		this.tree.Name = "tree";
		this.tree.Size = new System.Drawing.Size(150, 114);
		this.tree.TabIndex = 26;
		this.tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tree_AfterSelect);
		// 
		// lblTonnage
		// 
		this.lblTonnage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.lblTonnage.Location = new System.Drawing.Point(0, 0);
		this.lblTonnage.Name = "lblTonnage";
		this.lblTonnage.Size = new System.Drawing.Size(150, 33);
		this.lblTonnage.TabIndex = 27;
		// 
		// CargoList
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.Controls.Add(this.lblTonnage);
		this.Controls.Add(this.tree);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "CargoList";
		this.ResumeLayout(false);

        }

        #endregion

	private System.Windows.Forms.TreeView tree;
	private System.Windows.Forms.Label lblTonnage;

}
