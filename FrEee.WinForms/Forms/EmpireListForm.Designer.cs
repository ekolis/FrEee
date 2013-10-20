namespace FrEee.WinForms.Forms
{
	partial class EmpireListForm
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
			this.tabs = new FrEee.WinForms.Controls.GameTabControl();
			this.tabDiplomacy = new System.Windows.Forms.TabPage();
			this.report = new FrEee.WinForms.Controls.EmpireReport();
			this.txtTreaty = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tabBudget = new System.Windows.Forms.TabPage();
			this.rqdIncome = new FrEee.WinForms.Controls.ResourceQuantityDisplay();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnClose = new FrEee.WinForms.Controls.GameButton();
			this.gamePanel1 = new FrEee.WinForms.Controls.GamePanel();
			this.lstEmpires = new System.Windows.Forms.ListView();
			this.rqdExtraction = new FrEee.WinForms.Controls.ResourceQuantityDisplay();
			this.rqdTrade = new FrEee.WinForms.Controls.ResourceQuantityDisplay();
			this.rqdTributesIn = new FrEee.WinForms.Controls.ResourceQuantityDisplay();
			this.rqExpenses = new FrEee.WinForms.Controls.ResourceQuantityDisplay();
			this.rqdConstruction = new FrEee.WinForms.Controls.ResourceQuantityDisplay();
			this.rqdMaintenance = new FrEee.WinForms.Controls.ResourceQuantityDisplay();
			this.rqdTributesOut = new FrEee.WinForms.Controls.ResourceQuantityDisplay();
			this.rqdNet = new FrEee.WinForms.Controls.ResourceQuantityDisplay();
			this.rqdStored = new FrEee.WinForms.Controls.ResourceQuantityDisplay();
			this.rqdSpoiled = new FrEee.WinForms.Controls.ResourceQuantityDisplay();
			this.tabs.SuspendLayout();
			this.tabDiplomacy.SuspendLayout();
			this.tabBudget.SuspendLayout();
			this.gamePanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabs
			// 
			this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabs.Controls.Add(this.tabDiplomacy);
			this.tabs.Controls.Add(this.tabBudget);
			this.tabs.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.tabs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
			this.tabs.Location = new System.Drawing.Point(12, 188);
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.SelectedTabBackColor = System.Drawing.Color.CornflowerBlue;
			this.tabs.SelectedTabForeColor = System.Drawing.Color.Black;
			this.tabs.Size = new System.Drawing.Size(710, 432);
			this.tabs.TabBackColor = System.Drawing.Color.Black;
			this.tabs.TabForeColor = System.Drawing.Color.CornflowerBlue;
			this.tabs.TabIndex = 3;
			// 
			// tabDiplomacy
			// 
			this.tabDiplomacy.BackColor = System.Drawing.Color.Black;
			this.tabDiplomacy.Controls.Add(this.report);
			this.tabDiplomacy.Controls.Add(this.txtTreaty);
			this.tabDiplomacy.Controls.Add(this.label1);
			this.tabDiplomacy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.tabDiplomacy.Location = new System.Drawing.Point(4, 29);
			this.tabDiplomacy.Name = "tabDiplomacy";
			this.tabDiplomacy.Padding = new System.Windows.Forms.Padding(3);
			this.tabDiplomacy.Size = new System.Drawing.Size(702, 399);
			this.tabDiplomacy.TabIndex = 0;
			this.tabDiplomacy.Text = "Diplomacy";
			// 
			// report
			// 
			this.report.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.report.BackColor = System.Drawing.Color.Black;
			this.report.Empire = null;
			this.report.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.report.ForeColor = System.Drawing.Color.White;
			this.report.Location = new System.Drawing.Point(314, 6);
			this.report.Name = "report";
			this.report.Size = new System.Drawing.Size(382, 390);
			this.report.TabIndex = 4;
			// 
			// txtTreaty
			// 
			this.txtTreaty.AutoSize = true;
			this.txtTreaty.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.txtTreaty.ForeColor = System.Drawing.Color.White;
			this.txtTreaty.Location = new System.Drawing.Point(109, 3);
			this.txtTreaty.Name = "txtTreaty";
			this.txtTreaty.Size = new System.Drawing.Size(66, 17);
			this.txtTreaty.TabIndex = 1;
			this.txtTreaty.Text = "Unknown";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label1.Location = new System.Drawing.Point(6, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(97, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "Treaty Status:";
			// 
			// tabBudget
			// 
			this.tabBudget.BackColor = System.Drawing.Color.Black;
			this.tabBudget.Controls.Add(this.rqdSpoiled);
			this.tabBudget.Controls.Add(this.rqdStored);
			this.tabBudget.Controls.Add(this.rqdNet);
			this.tabBudget.Controls.Add(this.rqdTributesOut);
			this.tabBudget.Controls.Add(this.rqdMaintenance);
			this.tabBudget.Controls.Add(this.rqdConstruction);
			this.tabBudget.Controls.Add(this.rqExpenses);
			this.tabBudget.Controls.Add(this.rqdTributesIn);
			this.tabBudget.Controls.Add(this.rqdTrade);
			this.tabBudget.Controls.Add(this.rqdExtraction);
			this.tabBudget.Controls.Add(this.rqdIncome);
			this.tabBudget.Controls.Add(this.label12);
			this.tabBudget.Controls.Add(this.label11);
			this.tabBudget.Controls.Add(this.label10);
			this.tabBudget.Controls.Add(this.label6);
			this.tabBudget.Controls.Add(this.label7);
			this.tabBudget.Controls.Add(this.label8);
			this.tabBudget.Controls.Add(this.label9);
			this.tabBudget.Controls.Add(this.label5);
			this.tabBudget.Controls.Add(this.label4);
			this.tabBudget.Controls.Add(this.label3);
			this.tabBudget.Controls.Add(this.label2);
			this.tabBudget.Location = new System.Drawing.Point(4, 29);
			this.tabBudget.Name = "tabBudget";
			this.tabBudget.Size = new System.Drawing.Size(702, 399);
			this.tabBudget.TabIndex = 2;
			this.tabBudget.Text = "Budget";
			// 
			// rqdIncome
			// 
			this.rqdIncome.BackColor = System.Drawing.Color.Black;
			this.rqdIncome.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.rqdIncome.ForeColor = System.Drawing.Color.White;
			this.rqdIncome.Location = new System.Drawing.Point(177, 3);
			this.rqdIncome.Name = "rqdIncome";
			this.rqdIncome.ResourceQuantity = null;
			this.rqdIncome.Size = new System.Drawing.Size(413, 24);
			this.rqdIncome.TabIndex = 12;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label12.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label12.Location = new System.Drawing.Point(3, 330);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(127, 17);
			this.label12.TabIndex = 11;
			this.label12.Text = "Spoiled Resources";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label11.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label11.Location = new System.Drawing.Point(3, 300);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(122, 17);
			this.label11.TabIndex = 10;
			this.label11.Text = "Stored Resources";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label10.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label10.Location = new System.Drawing.Point(3, 270);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(79, 17);
			this.label10.TabIndex = 9;
			this.label10.Text = "Net Income";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label6.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label6.Location = new System.Drawing.Point(18, 228);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(60, 17);
			this.label6.TabIndex = 8;
			this.label6.Text = "Tributes";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label7.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label7.Location = new System.Drawing.Point(18, 198);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(89, 17);
			this.label7.TabIndex = 7;
			this.label7.Text = "Maintenance";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label8.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label8.Location = new System.Drawing.Point(18, 168);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(87, 17);
			this.label8.TabIndex = 6;
			this.label8.Text = "Construction";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label9.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label9.Location = new System.Drawing.Point(3, 138);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(69, 17);
			this.label9.TabIndex = 5;
			this.label9.Text = "Expenses";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label5.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label5.Location = new System.Drawing.Point(18, 93);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(60, 17);
			this.label5.TabIndex = 4;
			this.label5.Text = "Tributes";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label4.Location = new System.Drawing.Point(18, 63);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(46, 17);
			this.label4.TabIndex = 3;
			this.label4.Text = "Trade";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label3.Location = new System.Drawing.Point(18, 33);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(135, 17);
			this.label3.TabIndex = 2;
			this.label3.Text = "Resource Extraction";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label2.Location = new System.Drawing.Point(3, 3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 17);
			this.label2.TabIndex = 1;
			this.label2.Text = "Income";
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.BackColor = System.Drawing.Color.Black;
			this.btnClose.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnClose.Location = new System.Drawing.Point(647, 626);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// gamePanel1
			// 
			this.gamePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gamePanel1.BackColor = System.Drawing.Color.Black;
			this.gamePanel1.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.gamePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.gamePanel1.Controls.Add(this.lstEmpires);
			this.gamePanel1.ForeColor = System.Drawing.Color.White;
			this.gamePanel1.Location = new System.Drawing.Point(12, 12);
			this.gamePanel1.Name = "gamePanel1";
			this.gamePanel1.Padding = new System.Windows.Forms.Padding(3);
			this.gamePanel1.Size = new System.Drawing.Size(710, 169);
			this.gamePanel1.TabIndex = 1;
			// 
			// lstEmpires
			// 
			this.lstEmpires.BackColor = System.Drawing.Color.Black;
			this.lstEmpires.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstEmpires.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstEmpires.ForeColor = System.Drawing.Color.White;
			this.lstEmpires.HideSelection = false;
			this.lstEmpires.Location = new System.Drawing.Point(3, 3);
			this.lstEmpires.Name = "lstEmpires";
			this.lstEmpires.Size = new System.Drawing.Size(702, 161);
			this.lstEmpires.TabIndex = 1;
			this.lstEmpires.UseCompatibleStateImageBehavior = false;
			this.lstEmpires.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstEmpires_ItemSelectionChanged);
			// 
			// rqdExtraction
			// 
			this.rqdExtraction.BackColor = System.Drawing.Color.Black;
			this.rqdExtraction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.rqdExtraction.ForeColor = System.Drawing.Color.White;
			this.rqdExtraction.Location = new System.Drawing.Point(177, 33);
			this.rqdExtraction.Name = "rqdExtraction";
			this.rqdExtraction.ResourceQuantity = null;
			this.rqdExtraction.Size = new System.Drawing.Size(413, 24);
			this.rqdExtraction.TabIndex = 14;
			// 
			// rqdTrade
			// 
			this.rqdTrade.BackColor = System.Drawing.Color.Black;
			this.rqdTrade.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.rqdTrade.ForeColor = System.Drawing.Color.White;
			this.rqdTrade.Location = new System.Drawing.Point(177, 63);
			this.rqdTrade.Name = "rqdTrade";
			this.rqdTrade.ResourceQuantity = null;
			this.rqdTrade.Size = new System.Drawing.Size(413, 24);
			this.rqdTrade.TabIndex = 15;
			// 
			// rqdTributesIn
			// 
			this.rqdTributesIn.BackColor = System.Drawing.Color.Black;
			this.rqdTributesIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.rqdTributesIn.ForeColor = System.Drawing.Color.White;
			this.rqdTributesIn.Location = new System.Drawing.Point(177, 93);
			this.rqdTributesIn.Name = "rqdTributesIn";
			this.rqdTributesIn.ResourceQuantity = null;
			this.rqdTributesIn.Size = new System.Drawing.Size(413, 24);
			this.rqdTributesIn.TabIndex = 16;
			// 
			// rqExpenses
			// 
			this.rqExpenses.BackColor = System.Drawing.Color.Black;
			this.rqExpenses.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.rqExpenses.ForeColor = System.Drawing.Color.White;
			this.rqExpenses.Location = new System.Drawing.Point(177, 138);
			this.rqExpenses.Name = "rqExpenses";
			this.rqExpenses.ResourceQuantity = null;
			this.rqExpenses.Size = new System.Drawing.Size(413, 24);
			this.rqExpenses.TabIndex = 17;
			// 
			// rqdConstruction
			// 
			this.rqdConstruction.BackColor = System.Drawing.Color.Black;
			this.rqdConstruction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.rqdConstruction.ForeColor = System.Drawing.Color.White;
			this.rqdConstruction.Location = new System.Drawing.Point(177, 168);
			this.rqdConstruction.Name = "rqdConstruction";
			this.rqdConstruction.ResourceQuantity = null;
			this.rqdConstruction.Size = new System.Drawing.Size(413, 24);
			this.rqdConstruction.TabIndex = 18;
			// 
			// rqdMaintenance
			// 
			this.rqdMaintenance.BackColor = System.Drawing.Color.Black;
			this.rqdMaintenance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.rqdMaintenance.ForeColor = System.Drawing.Color.White;
			this.rqdMaintenance.Location = new System.Drawing.Point(177, 198);
			this.rqdMaintenance.Name = "rqdMaintenance";
			this.rqdMaintenance.ResourceQuantity = null;
			this.rqdMaintenance.Size = new System.Drawing.Size(413, 24);
			this.rqdMaintenance.TabIndex = 19;
			// 
			// rqdTributesOut
			// 
			this.rqdTributesOut.BackColor = System.Drawing.Color.Black;
			this.rqdTributesOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.rqdTributesOut.ForeColor = System.Drawing.Color.White;
			this.rqdTributesOut.Location = new System.Drawing.Point(177, 228);
			this.rqdTributesOut.Name = "rqdTributesOut";
			this.rqdTributesOut.ResourceQuantity = null;
			this.rqdTributesOut.Size = new System.Drawing.Size(413, 24);
			this.rqdTributesOut.TabIndex = 20;
			// 
			// rqdNet
			// 
			this.rqdNet.BackColor = System.Drawing.Color.Black;
			this.rqdNet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.rqdNet.ForeColor = System.Drawing.Color.White;
			this.rqdNet.Location = new System.Drawing.Point(177, 270);
			this.rqdNet.Name = "rqdNet";
			this.rqdNet.ResourceQuantity = null;
			this.rqdNet.Size = new System.Drawing.Size(413, 24);
			this.rqdNet.TabIndex = 21;
			// 
			// rqdStored
			// 
			this.rqdStored.BackColor = System.Drawing.Color.Black;
			this.rqdStored.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.rqdStored.ForeColor = System.Drawing.Color.White;
			this.rqdStored.Location = new System.Drawing.Point(177, 300);
			this.rqdStored.Name = "rqdStored";
			this.rqdStored.ResourceQuantity = null;
			this.rqdStored.Size = new System.Drawing.Size(413, 24);
			this.rqdStored.TabIndex = 22;
			// 
			// rqdSpoiled
			// 
			this.rqdSpoiled.BackColor = System.Drawing.Color.Black;
			this.rqdSpoiled.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.rqdSpoiled.ForeColor = System.Drawing.Color.White;
			this.rqdSpoiled.Location = new System.Drawing.Point(177, 330);
			this.rqdSpoiled.Name = "rqdSpoiled";
			this.rqdSpoiled.ResourceQuantity = null;
			this.rqdSpoiled.Size = new System.Drawing.Size(413, 24);
			this.rqdSpoiled.TabIndex = 23;
			// 
			// EmpireListForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(734, 661);
			this.Controls.Add(this.tabs);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.gamePanel1);
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "EmpireListForm";
			this.Text = "Empires";
			this.tabs.ResumeLayout(false);
			this.tabDiplomacy.ResumeLayout(false);
			this.tabDiplomacy.PerformLayout();
			this.tabBudget.ResumeLayout(false);
			this.tabBudget.PerformLayout();
			this.gamePanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.GamePanel gamePanel1;
		private System.Windows.Forms.ListView lstEmpires;
		private Controls.GameButton btnClose;
		private System.Windows.Forms.TabPage tabDiplomacy;
		private Controls.GameTabControl tabs;
		private System.Windows.Forms.TabPage tabBudget;
		private System.Windows.Forms.Label txtTreaty;
		private System.Windows.Forms.Label label1;
		private Controls.EmpireReport report;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private Controls.ResourceQuantityDisplay rqdIncome;
		private Controls.ResourceQuantityDisplay rqdStored;
		private Controls.ResourceQuantityDisplay rqdNet;
		private Controls.ResourceQuantityDisplay rqdTributesOut;
		private Controls.ResourceQuantityDisplay rqdMaintenance;
		private Controls.ResourceQuantityDisplay rqdConstruction;
		private Controls.ResourceQuantityDisplay rqExpenses;
		private Controls.ResourceQuantityDisplay rqdTributesIn;
		private Controls.ResourceQuantityDisplay rqdTrade;
		private Controls.ResourceQuantityDisplay rqdExtraction;
		private Controls.ResourceQuantityDisplay rqdSpoiled;
	}
}