namespace FrEee.WinForms.Controls
{
    partial class Cargolist
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Used: Free: Total: ");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("3x \"Buster\" class Weapon Platform");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("10x \"Guard\" class Troop");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("100x Eee Population");
            this.lstCargoDetail = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lstCargoDetail
            // 
            this.lstCargoDetail.BackColor = System.Drawing.Color.Black;
            this.lstCargoDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstCargoDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCargoDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstCargoDetail.ForeColor = System.Drawing.Color.White;
            this.lstCargoDetail.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4});
            this.lstCargoDetail.Location = new System.Drawing.Point(0, 0);
            this.lstCargoDetail.Name = "lstCargoDetail";
            this.lstCargoDetail.Size = new System.Drawing.Size(150, 150);
            this.lstCargoDetail.TabIndex = 25;
            this.lstCargoDetail.UseCompatibleStateImageBehavior = false;
            this.lstCargoDetail.View = System.Windows.Forms.View.List;
            // 
            // Cargolist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.lstCargoDetail);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "Cargolist";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstCargoDetail;
    }
}
