namespace FrEee.WinForms.Forms
{
    partial class MainMenuForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenuForm));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.tblButtonPanel = new System.Windows.Forms.TableLayoutPanel();
			this.btnQuit = new FrEee.WinForms.Controls.GameButton();
			this.btnScenario = new FrEee.WinForms.Controls.GameButton();
			this.btnNew = new FrEee.WinForms.Controls.GameButton();
			this.btnCredits = new FrEee.WinForms.Controls.GameButton();
			this.btnMods = new FrEee.WinForms.Controls.GameButton();
			this.btnQuickStart = new FrEee.WinForms.Controls.GameButton();
			this.btnLoad = new FrEee.WinForms.Controls.GameButton();
			this.btnResume = new FrEee.WinForms.Controls.GameButton();
			this.btnOptions = new FrEee.WinForms.Controls.GameButton();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.tblButtonPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 826F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tblButtonPanel, 1, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1154, 934);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(7, 6);
			this.pictureBox1.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(812, 904);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// tblButtonPanel
			// 
			this.tblButtonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tblButtonPanel.ColumnCount = 1;
			this.tblButtonPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tblButtonPanel.Controls.Add(this.btnQuit, 0, 8);
			this.tblButtonPanel.Controls.Add(this.btnScenario, 0, 5);
			this.tblButtonPanel.Controls.Add(this.btnNew, 0, 1);
			this.tblButtonPanel.Controls.Add(this.btnCredits, 0, 7);
			this.tblButtonPanel.Controls.Add(this.btnMods, 0, 0);
			this.tblButtonPanel.Controls.Add(this.btnQuickStart, 0, 2);
			this.tblButtonPanel.Controls.Add(this.btnLoad, 0, 4);
			this.tblButtonPanel.Controls.Add(this.btnResume, 0, 3);
			this.tblButtonPanel.Controls.Add(this.btnOptions, 0, 6);
			this.tblButtonPanel.Location = new System.Drawing.Point(833, 6);
			this.tblButtonPanel.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
			this.tblButtonPanel.Name = "tblButtonPanel";
			this.tblButtonPanel.RowCount = 9;
			this.tblButtonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblButtonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblButtonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblButtonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblButtonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblButtonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblButtonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblButtonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblButtonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblButtonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
			this.tblButtonPanel.Size = new System.Drawing.Size(314, 904);
			this.tblButtonPanel.TabIndex = 3;
			// 
			// btnQuit
			// 
			this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnQuit.BackColor = System.Drawing.Color.Black;
			this.btnQuit.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnQuit.Location = new System.Drawing.Point(10, 828);
			this.btnQuit.Margin = new System.Windows.Forms.Padding(10, 12, 10, 10);
			this.btnQuit.Name = "btnQuit";
			this.btnQuit.Padding = new System.Windows.Forms.Padding(9, 10, 9, 10);
			this.btnQuit.Size = new System.Drawing.Size(294, 80);
			this.btnQuit.TabIndex = 8;
			this.btnQuit.Text = "Quit";
			this.btnQuit.UseVisualStyleBackColor = false;
			this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
			// 
			// btnScenario
			// 
			this.btnScenario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnScenario.BackColor = System.Drawing.Color.Black;
			this.btnScenario.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnScenario.Location = new System.Drawing.Point(10, 522);
			this.btnScenario.Margin = new System.Windows.Forms.Padding(10, 12, 10, 10);
			this.btnScenario.Name = "btnScenario";
			this.btnScenario.Padding = new System.Windows.Forms.Padding(9, 10, 9, 10);
			this.btnScenario.Size = new System.Drawing.Size(294, 80);
			this.btnScenario.TabIndex = 5;
			this.btnScenario.Text = "Scenario";
			this.btnScenario.UseVisualStyleBackColor = false;
			this.btnScenario.Click += new System.EventHandler(this.btnScenario_Click);
			// 
			// btnNew
			// 
			this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNew.BackColor = System.Drawing.Color.Black;
			this.btnNew.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnNew.Location = new System.Drawing.Point(10, 114);
			this.btnNew.Margin = new System.Windows.Forms.Padding(10, 12, 10, 10);
			this.btnNew.Name = "btnNew";
			this.btnNew.Padding = new System.Windows.Forms.Padding(9, 10, 9, 10);
			this.btnNew.Size = new System.Drawing.Size(294, 80);
			this.btnNew.TabIndex = 1;
			this.btnNew.Text = "New";
			this.btnNew.UseVisualStyleBackColor = false;
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// btnCredits
			// 
			this.btnCredits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCredits.BackColor = System.Drawing.Color.Black;
			this.btnCredits.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnCredits.Location = new System.Drawing.Point(10, 726);
			this.btnCredits.Margin = new System.Windows.Forms.Padding(10, 12, 10, 10);
			this.btnCredits.Name = "btnCredits";
			this.btnCredits.Padding = new System.Windows.Forms.Padding(9, 10, 9, 10);
			this.btnCredits.Size = new System.Drawing.Size(294, 80);
			this.btnCredits.TabIndex = 7;
			this.btnCredits.Text = "Credits";
			this.btnCredits.UseVisualStyleBackColor = false;
			this.btnCredits.Click += new System.EventHandler(this.btnCredits_Click);
			// 
			// btnMods
			// 
			this.btnMods.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMods.BackColor = System.Drawing.Color.Black;
			this.btnMods.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnMods.Location = new System.Drawing.Point(10, 12);
			this.btnMods.Margin = new System.Windows.Forms.Padding(10, 12, 10, 10);
			this.btnMods.Name = "btnMods";
			this.btnMods.Padding = new System.Windows.Forms.Padding(9, 10, 9, 10);
			this.btnMods.Size = new System.Drawing.Size(294, 80);
			this.btnMods.TabIndex = 4;
			this.btnMods.Text = "Mods";
			this.btnMods.UseVisualStyleBackColor = false;
			this.btnMods.Click += new System.EventHandler(this.btnMods_Click);
			// 
			// btnQuickStart
			// 
			this.btnQuickStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnQuickStart.BackColor = System.Drawing.Color.Black;
			this.btnQuickStart.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnQuickStart.Location = new System.Drawing.Point(10, 216);
			this.btnQuickStart.Margin = new System.Windows.Forms.Padding(10, 12, 10, 10);
			this.btnQuickStart.Name = "btnQuickStart";
			this.btnQuickStart.Padding = new System.Windows.Forms.Padding(9, 10, 9, 10);
			this.btnQuickStart.Size = new System.Drawing.Size(294, 80);
			this.btnQuickStart.TabIndex = 0;
			this.btnQuickStart.Text = "Quickstart";
			this.btnQuickStart.UseVisualStyleBackColor = false;
			this.btnQuickStart.Click += new System.EventHandler(this.btnQuickStart_Click);
			// 
			// btnLoad
			// 
			this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLoad.BackColor = System.Drawing.Color.Black;
			this.btnLoad.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnLoad.Location = new System.Drawing.Point(10, 420);
			this.btnLoad.Margin = new System.Windows.Forms.Padding(10, 12, 10, 10);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Padding = new System.Windows.Forms.Padding(9, 10, 9, 10);
			this.btnLoad.Size = new System.Drawing.Size(294, 80);
			this.btnLoad.TabIndex = 3;
			this.btnLoad.Text = "Load";
			this.btnLoad.UseVisualStyleBackColor = false;
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// btnResume
			// 
			this.btnResume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnResume.BackColor = System.Drawing.Color.Black;
			this.btnResume.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnResume.Location = new System.Drawing.Point(10, 318);
			this.btnResume.Margin = new System.Windows.Forms.Padding(10, 12, 10, 10);
			this.btnResume.Name = "btnResume";
			this.btnResume.Padding = new System.Windows.Forms.Padding(9, 10, 9, 10);
			this.btnResume.Size = new System.Drawing.Size(294, 80);
			this.btnResume.TabIndex = 2;
			this.btnResume.Text = "Resume";
			this.btnResume.UseVisualStyleBackColor = false;
			this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
			// 
			// btnOptions
			// 
			this.btnOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOptions.BackColor = System.Drawing.Color.Black;
			this.btnOptions.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnOptions.Location = new System.Drawing.Point(10, 624);
			this.btnOptions.Margin = new System.Windows.Forms.Padding(10, 12, 10, 10);
			this.btnOptions.Name = "btnOptions";
			this.btnOptions.Padding = new System.Windows.Forms.Padding(9, 10, 9, 10);
			this.btnOptions.Size = new System.Drawing.Size(294, 80);
			this.btnOptions.TabIndex = 6;
			this.btnOptions.Text = "Options";
			this.btnOptions.UseVisualStyleBackColor = false;
			this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
			// 
			// MainMenuForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(1150, 934);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainMenuForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainMenuForm_FormClosed);
			this.Load += new System.EventHandler(this.MainMenuForm_Load);
			this.VisibleChanged += new System.EventHandler(this.MainMenuForm_VisibleChanged);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainMenuForm_KeyDown);
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.tblButtonPanel.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tblButtonPanel;
        private Controls.GameButton btnQuit;
        private Controls.GameButton btnCredits;
        private Controls.GameButton btnScenario;
        private Controls.GameButton btnMods;
        private Controls.GameButton btnLoad;
        private Controls.GameButton btnResume;
        private Controls.GameButton btnNew;
        private Controls.GameButton btnOptions;
		private Controls.GameButton btnQuickStart;

    }
}