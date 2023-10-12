using FrEee.Objects.Space;
using FrEee.Utility;
using FrEee.WinForms.Controls;

namespace FrEee.WinForms.Forms
{
	partial class PlanetListForm
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
			this.components = new System.ComponentModel.Container();
			FrEee.WinForms.Objects.GalaxyViewModes.PresenceMode presenceMode1 = new FrEee.WinForms.Objects.GalaxyViewModes.PresenceMode();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.galaxyView = new FrEee.WinForms.Controls.GalaxyView();
			this.pnlHeader = new FrEee.WinForms.Controls.GamePanel();
			this.txtPopulation = new System.Windows.Forms.Label();
			this.resStorageRad = new FrEee.WinForms.Controls.ResourceDisplay();
			this.resStorageOrg = new FrEee.WinForms.Controls.ResourceDisplay();
			this.resStorageMin = new FrEee.WinForms.Controls.ResourceDisplay();
			this.label16 = new System.Windows.Forms.Label();
			this.resInt = new FrEee.WinForms.Controls.ResourceDisplay();
			this.resRes = new FrEee.WinForms.Controls.ResourceDisplay();
			this.resRad = new FrEee.WinForms.Controls.ResourceDisplay();
			this.resOrg = new FrEee.WinForms.Controls.ResourceDisplay();
			this.resMin = new FrEee.WinForms.Controls.ResourceDisplay();
			this.label15 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.txtUs = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.txtSystemsWithColonies = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.txtBreathableOther = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.txtAvailable = new System.Windows.Forms.Label();
			this.txtShips = new System.Windows.Forms.Label();
			this.txtBreathableUs = new System.Windows.Forms.Label();
			this.txtUncolonized = new System.Windows.Forms.Label();
			this.txtNonAligned = new System.Windows.Forms.Label();
			this.txtAllies = new System.Windows.Forms.Label();
			this.txtEnemies = new System.Windows.Forms.Label();
			this.txtColonizable = new System.Windows.Forms.Label();
			this.txtPlanets = new System.Windows.Forms.Label();
			this.txtSystems = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.grid = new FrEee.WinForms.Controls.GameGridView();
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.colonizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tableLayoutPanel1.SuspendLayout();
			this.pnlHeader.SuspendLayout();
			this.contextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.galaxyView, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.pnlHeader, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.grid, 0, 1);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1160, 737);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// galaxyView
			// 
			this.galaxyView.BackColor = System.Drawing.Color.Black;
			this.galaxyView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.galaxyView.Dock = System.Windows.Forms.DockStyle.Left;
			this.galaxyView.Location = new System.Drawing.Point(403, 3);
			this.galaxyView.Mode = presenceMode1;
			this.galaxyView.Name = "galaxyView";
			this.galaxyView.SelectedStarSystem = null;
			this.galaxyView.Size = new System.Drawing.Size(226, 194);
			this.galaxyView.TabIndex = 22;
			this.galaxyView.Text = "galaxyView1";
			// 
			// pnlHeader
			// 
			this.pnlHeader.BackColor = System.Drawing.Color.Black;
			this.pnlHeader.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.pnlHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tableLayoutPanel1.SetColumnSpan(this.pnlHeader, 2);
			this.pnlHeader.Controls.Add(this.txtPopulation);
			this.pnlHeader.Controls.Add(this.resStorageRad);
			this.pnlHeader.Controls.Add(this.resStorageOrg);
			this.pnlHeader.Controls.Add(this.resStorageMin);
			this.pnlHeader.Controls.Add(this.label16);
			this.pnlHeader.Controls.Add(this.resInt);
			this.pnlHeader.Controls.Add(this.resRes);
			this.pnlHeader.Controls.Add(this.resRad);
			this.pnlHeader.Controls.Add(this.resOrg);
			this.pnlHeader.Controls.Add(this.resMin);
			this.pnlHeader.Controls.Add(this.label15);
			this.pnlHeader.Controls.Add(this.label13);
			this.pnlHeader.Controls.Add(this.txtUs);
			this.pnlHeader.Controls.Add(this.label14);
			this.pnlHeader.Controls.Add(this.txtSystemsWithColonies);
			this.pnlHeader.Controls.Add(this.label12);
			this.pnlHeader.Controls.Add(this.txtBreathableOther);
			this.pnlHeader.Controls.Add(this.label11);
			this.pnlHeader.Controls.Add(this.txtAvailable);
			this.pnlHeader.Controls.Add(this.txtShips);
			this.pnlHeader.Controls.Add(this.txtBreathableUs);
			this.pnlHeader.Controls.Add(this.txtUncolonized);
			this.pnlHeader.Controls.Add(this.txtNonAligned);
			this.pnlHeader.Controls.Add(this.txtAllies);
			this.pnlHeader.Controls.Add(this.txtEnemies);
			this.pnlHeader.Controls.Add(this.txtColonizable);
			this.pnlHeader.Controls.Add(this.txtPlanets);
			this.pnlHeader.Controls.Add(this.txtSystems);
			this.pnlHeader.Controls.Add(this.label10);
			this.pnlHeader.Controls.Add(this.label9);
			this.pnlHeader.Controls.Add(this.label8);
			this.pnlHeader.Controls.Add(this.label7);
			this.pnlHeader.Controls.Add(this.label6);
			this.pnlHeader.Controls.Add(this.label5);
			this.pnlHeader.Controls.Add(this.label4);
			this.pnlHeader.Controls.Add(this.label3);
			this.pnlHeader.Controls.Add(this.label2);
			this.pnlHeader.Controls.Add(this.label1);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlHeader.ForeColor = System.Drawing.Color.White;
			this.pnlHeader.Location = new System.Drawing.Point(3, 3);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Padding = new System.Windows.Forms.Padding(3);
			this.pnlHeader.Size = new System.Drawing.Size(394, 194);
			this.pnlHeader.TabIndex = 0;
			// 
			// txtPopulation
			// 
			this.txtPopulation.AutoSize = true;
			this.txtPopulation.Location = new System.Drawing.Point(348, 4);
			this.txtPopulation.MinimumSize = new System.Drawing.Size(30, 0);
			this.txtPopulation.Name = "txtPopulation";
			this.txtPopulation.Size = new System.Drawing.Size(30, 13);
			this.txtPopulation.TabIndex = 38;
			this.txtPopulation.Text = "0";
			this.txtPopulation.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// resStorageRad
			// 
			this.resStorageRad.Amount = 0;
			this.resStorageRad.BackColor = System.Drawing.Color.Black;
			this.resStorageRad.Change = null;
			this.resStorageRad.ForeColor = System.Drawing.Color.White;
			this.resStorageRad.Location = new System.Drawing.Point(271, 136);
			this.resStorageRad.Margin = new System.Windows.Forms.Padding(0);
			this.resStorageRad.Name = "resStorageRad";
			this.resStorageRad.ResourceName = "Radioactives";
			this.resStorageRad.Size = new System.Drawing.Size(107, 12);
			this.resStorageRad.TabIndex = 37;
			// 
			// resStorageOrg
			// 
			this.resStorageOrg.Amount = 0;
			this.resStorageOrg.BackColor = System.Drawing.Color.Black;
			this.resStorageOrg.Change = null;
			this.resStorageOrg.ForeColor = System.Drawing.Color.White;
			this.resStorageOrg.Location = new System.Drawing.Point(271, 123);
			this.resStorageOrg.Margin = new System.Windows.Forms.Padding(0);
			this.resStorageOrg.Name = "resStorageOrg";
			this.resStorageOrg.ResourceName = "Organics";
			this.resStorageOrg.Size = new System.Drawing.Size(107, 12);
			this.resStorageOrg.TabIndex = 36;
			// 
			// resStorageMin
			// 
			this.resStorageMin.Amount = 0;
			this.resStorageMin.BackColor = System.Drawing.Color.Black;
			this.resStorageMin.Change = null;
			this.resStorageMin.ForeColor = System.Drawing.Color.White;
			this.resStorageMin.Location = new System.Drawing.Point(271, 110);
			this.resStorageMin.Margin = new System.Windows.Forms.Padding(0);
			this.resStorageMin.Name = "resStorageMin";
			this.resStorageMin.ResourceName = "Minerals";
			this.resStorageMin.Size = new System.Drawing.Size(107, 12);
			this.resStorageMin.TabIndex = 35;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label16.Location = new System.Drawing.Point(254, 96);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(88, 13);
			this.label16.TabIndex = 34;
			this.label16.Text = "Storage Capacity";
			// 
			// resInt
			// 
			this.resInt.Amount = 0;
			this.resInt.BackColor = System.Drawing.Color.Black;
			this.resInt.Change = null;
			this.resInt.ForeColor = System.Drawing.Color.White;
			this.resInt.Location = new System.Drawing.Point(274, 84);
			this.resInt.Margin = new System.Windows.Forms.Padding(0);
			this.resInt.Name = "resInt";
			this.resInt.ResourceName = "Intelligence";
			this.resInt.Size = new System.Drawing.Size(107, 12);
			this.resInt.TabIndex = 33;
			// 
			// resRes
			// 
			this.resRes.Amount = 0;
			this.resRes.BackColor = System.Drawing.Color.Black;
			this.resRes.Change = null;
			this.resRes.ForeColor = System.Drawing.Color.White;
			this.resRes.Location = new System.Drawing.Point(274, 71);
			this.resRes.Margin = new System.Windows.Forms.Padding(0);
			this.resRes.Name = "resRes";
			this.resRes.ResourceName = "Research";
			this.resRes.Size = new System.Drawing.Size(107, 12);
			this.resRes.TabIndex = 32;
			// 
			// resRad
			// 
			this.resRad.Amount = 0;
			this.resRad.BackColor = System.Drawing.Color.Black;
			this.resRad.Change = null;
			this.resRad.ForeColor = System.Drawing.Color.White;
			this.resRad.Location = new System.Drawing.Point(274, 59);
			this.resRad.Margin = new System.Windows.Forms.Padding(0);
			this.resRad.Name = "resRad";
			this.resRad.ResourceName = "Radioactives";
			this.resRad.Size = new System.Drawing.Size(107, 12);
			this.resRad.TabIndex = 31;
			// 
			// resOrg
			// 
			this.resOrg.Amount = 0;
			this.resOrg.BackColor = System.Drawing.Color.Black;
			this.resOrg.Change = null;
			this.resOrg.ForeColor = System.Drawing.Color.White;
			this.resOrg.Location = new System.Drawing.Point(274, 47);
			this.resOrg.Margin = new System.Windows.Forms.Padding(0);
			this.resOrg.Name = "resOrg";
			this.resOrg.ResourceName = "Organics";
			this.resOrg.Size = new System.Drawing.Size(107, 12);
			this.resOrg.TabIndex = 30;
			// 
			// resMin
			// 
			this.resMin.Amount = 0;
			this.resMin.BackColor = System.Drawing.Color.Black;
			this.resMin.Change = null;
			this.resMin.ForeColor = System.Drawing.Color.White;
			this.resMin.Location = new System.Drawing.Point(274, 34);
			this.resMin.Margin = new System.Windows.Forms.Padding(0);
			this.resMin.Name = "resMin";
			this.resMin.ResourceName = "Minerals";
			this.resMin.Size = new System.Drawing.Size(107, 12);
			this.resMin.TabIndex = 29;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label15.Location = new System.Drawing.Point(254, 17);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(58, 13);
			this.label15.TabIndex = 28;
			this.label15.Text = "Resources";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label13.Location = new System.Drawing.Point(254, 4);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(57, 13);
			this.label13.TabIndex = 27;
			this.label13.Text = "Population";
			// 
			// txtUs
			// 
			this.txtUs.AutoSize = true;
			this.txtUs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.txtUs.Location = new System.Drawing.Point(185, 43);
			this.txtUs.MaximumSize = new System.Drawing.Size(30, 30);
			this.txtUs.MinimumSize = new System.Drawing.Size(30, 0);
			this.txtUs.Name = "txtUs";
			this.txtUs.Size = new System.Drawing.Size(30, 13);
			this.txtUs.TabIndex = 26;
			this.txtUs.Text = "0";
			this.txtUs.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label14.Location = new System.Drawing.Point(17, 43);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(69, 13);
			this.label14.TabIndex = 25;
			this.label14.Text = "owned by Us";
			// 
			// txtSystemsWithColonies
			// 
			this.txtSystemsWithColonies.AutoSize = true;
			this.txtSystemsWithColonies.Location = new System.Drawing.Point(185, 17);
			this.txtSystemsWithColonies.MaximumSize = new System.Drawing.Size(30, 30);
			this.txtSystemsWithColonies.MinimumSize = new System.Drawing.Size(30, 0);
			this.txtSystemsWithColonies.Name = "txtSystemsWithColonies";
			this.txtSystemsWithColonies.Size = new System.Drawing.Size(30, 13);
			this.txtSystemsWithColonies.TabIndex = 24;
			this.txtSystemsWithColonies.Text = "0";
			this.txtSystemsWithColonies.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label12.Location = new System.Drawing.Point(17, 17);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(69, 13);
			this.label12.TabIndex = 23;
			this.label12.Text = "with Colonies";
			// 
			// txtBreathableOther
			// 
			this.txtBreathableOther.AutoSize = true;
			this.txtBreathableOther.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.txtBreathableOther.Location = new System.Drawing.Point(185, 135);
			this.txtBreathableOther.MaximumSize = new System.Drawing.Size(30, 30);
			this.txtBreathableOther.MinimumSize = new System.Drawing.Size(30, 0);
			this.txtBreathableOther.Name = "txtBreathableOther";
			this.txtBreathableOther.Size = new System.Drawing.Size(30, 13);
			this.txtBreathableOther.TabIndex = 22;
			this.txtBreathableOther.Text = "0";
			this.txtBreathableOther.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label11.Location = new System.Drawing.Point(32, 135);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(87, 13);
			this.label11.TabIndex = 21;
			this.label11.Text = "Other Breathable";
			// 
			// txtAvailable
			// 
			this.txtAvailable.AutoSize = true;
			this.txtAvailable.Location = new System.Drawing.Point(185, 168);
			this.txtAvailable.MaximumSize = new System.Drawing.Size(30, 30);
			this.txtAvailable.MinimumSize = new System.Drawing.Size(30, 0);
			this.txtAvailable.Name = "txtAvailable";
			this.txtAvailable.Size = new System.Drawing.Size(30, 13);
			this.txtAvailable.TabIndex = 19;
			this.txtAvailable.Text = "0";
			this.txtAvailable.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// txtShips
			// 
			this.txtShips.AutoSize = true;
			this.txtShips.Location = new System.Drawing.Point(185, 155);
			this.txtShips.MaximumSize = new System.Drawing.Size(30, 30);
			this.txtShips.MinimumSize = new System.Drawing.Size(30, 0);
			this.txtShips.Name = "txtShips";
			this.txtShips.Size = new System.Drawing.Size(30, 13);
			this.txtShips.TabIndex = 18;
			this.txtShips.Text = "0";
			this.txtShips.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// txtBreathableUs
			// 
			this.txtBreathableUs.AutoSize = true;
			this.txtBreathableUs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.txtBreathableUs.Location = new System.Drawing.Point(185, 122);
			this.txtBreathableUs.MaximumSize = new System.Drawing.Size(30, 30);
			this.txtBreathableUs.MinimumSize = new System.Drawing.Size(30, 0);
			this.txtBreathableUs.Name = "txtBreathableUs";
			this.txtBreathableUs.Size = new System.Drawing.Size(30, 13);
			this.txtBreathableUs.TabIndex = 17;
			this.txtBreathableUs.Text = "0";
			this.txtBreathableUs.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// txtUncolonized
			// 
			this.txtUncolonized.AutoSize = true;
			this.txtUncolonized.Location = new System.Drawing.Point(185, 109);
			this.txtUncolonized.MaximumSize = new System.Drawing.Size(30, 30);
			this.txtUncolonized.MinimumSize = new System.Drawing.Size(30, 0);
			this.txtUncolonized.Name = "txtUncolonized";
			this.txtUncolonized.Size = new System.Drawing.Size(30, 13);
			this.txtUncolonized.TabIndex = 16;
			this.txtUncolonized.Text = "0";
			this.txtUncolonized.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// txtNonAligned
			// 
			this.txtNonAligned.AutoSize = true;
			this.txtNonAligned.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.txtNonAligned.Location = new System.Drawing.Point(185, 96);
			this.txtNonAligned.MaximumSize = new System.Drawing.Size(30, 30);
			this.txtNonAligned.MinimumSize = new System.Drawing.Size(30, 0);
			this.txtNonAligned.Name = "txtNonAligned";
			this.txtNonAligned.Size = new System.Drawing.Size(30, 13);
			this.txtNonAligned.TabIndex = 15;
			this.txtNonAligned.Text = "0";
			this.txtNonAligned.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// txtAllies
			// 
			this.txtAllies.AutoSize = true;
			this.txtAllies.Location = new System.Drawing.Point(185, 83);
			this.txtAllies.MaximumSize = new System.Drawing.Size(30, 30);
			this.txtAllies.MinimumSize = new System.Drawing.Size(30, 0);
			this.txtAllies.Name = "txtAllies";
			this.txtAllies.Size = new System.Drawing.Size(30, 13);
			this.txtAllies.TabIndex = 14;
			this.txtAllies.Text = "0";
			this.txtAllies.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// txtEnemies
			// 
			this.txtEnemies.AutoSize = true;
			this.txtEnemies.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.txtEnemies.Location = new System.Drawing.Point(185, 70);
			this.txtEnemies.MaximumSize = new System.Drawing.Size(30, 30);
			this.txtEnemies.MinimumSize = new System.Drawing.Size(30, 0);
			this.txtEnemies.Name = "txtEnemies";
			this.txtEnemies.Size = new System.Drawing.Size(30, 13);
			this.txtEnemies.TabIndex = 13;
			this.txtEnemies.Text = "0";
			this.txtEnemies.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// txtColonizable
			// 
			this.txtColonizable.AutoSize = true;
			this.txtColonizable.Location = new System.Drawing.Point(185, 56);
			this.txtColonizable.MaximumSize = new System.Drawing.Size(30, 30);
			this.txtColonizable.MinimumSize = new System.Drawing.Size(30, 0);
			this.txtColonizable.Name = "txtColonizable";
			this.txtColonizable.Size = new System.Drawing.Size(30, 13);
			this.txtColonizable.TabIndex = 12;
			this.txtColonizable.Text = "0";
			this.txtColonizable.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// txtPlanets
			// 
			this.txtPlanets.AutoSize = true;
			this.txtPlanets.Location = new System.Drawing.Point(185, 30);
			this.txtPlanets.MaximumSize = new System.Drawing.Size(30, 30);
			this.txtPlanets.MinimumSize = new System.Drawing.Size(30, 0);
			this.txtPlanets.Name = "txtPlanets";
			this.txtPlanets.Size = new System.Drawing.Size(30, 13);
			this.txtPlanets.TabIndex = 11;
			this.txtPlanets.Text = "0";
			this.txtPlanets.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// txtSystems
			// 
			this.txtSystems.AutoSize = true;
			this.txtSystems.Location = new System.Drawing.Point(185, 4);
			this.txtSystems.MaximumSize = new System.Drawing.Size(30, 30);
			this.txtSystems.MinimumSize = new System.Drawing.Size(30, 0);
			this.txtSystems.Name = "txtSystems";
			this.txtSystems.Size = new System.Drawing.Size(30, 13);
			this.txtSystems.TabIndex = 10;
			this.txtSystems.Text = "0";
			this.txtSystems.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label10.Location = new System.Drawing.Point(17, 168);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(50, 13);
			this.label10.TabIndex = 9;
			this.label10.Text = "Available";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label9.Location = new System.Drawing.Point(6, 155);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(84, 13);
			this.label9.TabIndex = 8;
			this.label9.Text = "Colonizing Ships";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label8.Location = new System.Drawing.Point(32, 122);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(121, 13);
			this.label8.TabIndex = 7;
			this.label8.Text = "Breathable by Our Race";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label7.Location = new System.Drawing.Point(17, 109);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(66, 13);
			this.label7.TabIndex = 6;
			this.label7.Text = "Uncolonized";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label6.Location = new System.Drawing.Point(17, 96);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(114, 13);
			this.label6.TabIndex = 5;
			this.label6.Text = "owned by Non-Aligned";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label5.Location = new System.Drawing.Point(17, 83);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "owned by Allies";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label4.Location = new System.Drawing.Point(17, 70);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(96, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "owned by Enemies";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label3.Location = new System.Drawing.Point(6, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(99, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Colonizable Planets";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label2.Location = new System.Drawing.Point(6, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(78, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Known Planets";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label1.Location = new System.Drawing.Point(4, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Known Systems";
			// 
			// grid
			// 
			this.grid.AppendMenuItems = null;
			this.grid.BackColor = System.Drawing.Color.Black;
			this.tableLayoutPanel1.SetColumnSpan(this.grid, 3);
			this.grid.CreateDefaultGridConfig = null;
			this.grid.CurrentGridConfig = null;
			this.grid.Data = new object[0];
			this.grid.DataType = typeof(object);
			this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grid.ForeColor = System.Drawing.Color.White;
			this.grid.GridConfigs = null;
			this.grid.LoadCurrentGridConfig = null;
			this.grid.LoadGridConfigs = null;
			this.grid.Location = new System.Drawing.Point(3, 203);
			this.grid.Name = "grid";
			this.grid.PrependMenuItems = null;
			this.grid.ResetGridConfigs = null;
			this.grid.ShowConfigs = true;
			this.grid.Size = new System.Drawing.Size(1154, 531);
			this.grid.TabIndex = 23;
			this.grid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_RowEnter);
			this.grid.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_RowLeave);
			this.grid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.grid_MouseDoubleClick);
			// 
			// contextMenu
			// 
			this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colonizeToolStripMenuItem});
			this.contextMenu.Name = "contextMenu";
			this.contextMenu.Size = new System.Drawing.Size(121, 26);
			// 
			// colonizeToolStripMenuItem
			// 
			this.colonizeToolStripMenuItem.Name = "colonizeToolStripMenuItem";
			this.colonizeToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
			this.colonizeToolStripMenuItem.Text = "&Colonize";
			this.colonizeToolStripMenuItem.Click += new System.EventHandler(this.colonizeToolStripMenuItem_Click);
			// 
			// PlanetListForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(1184, 761);
			this.Controls.Add(this.tableLayoutPanel1);
			this.DoubleBuffered = true;
			this.ForeColor = System.Drawing.Color.White;
			this.MinimumSize = new System.Drawing.Size(800, 600);
			this.Name = "PlanetListForm";
			this.ShowInTaskbar = false;
			this.Text = "Planets";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PlanetListForm_FormClosed);
			this.Load += new System.EventHandler(this.PlanetListForm_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.pnlHeader.ResumeLayout(false);
			this.pnlHeader.PerformLayout();
			this.contextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private GamePanel pnlHeader;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label txtBreathableUs;
		private System.Windows.Forms.Label txtUncolonized;
		private System.Windows.Forms.Label txtNonAligned;
		private System.Windows.Forms.Label txtAllies;
		private System.Windows.Forms.Label txtEnemies;
		private System.Windows.Forms.Label txtColonizable;
		private System.Windows.Forms.Label txtPlanets;
		private System.Windows.Forms.Label txtSystems;
		private System.Windows.Forms.Label txtAvailable;
		private System.Windows.Forms.Label txtShips;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label txtBreathableOther;
		private System.Windows.Forms.Label txtSystemsWithColonies;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label txtUs;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label15;
		private ResourceDisplay resRad;
		private ResourceDisplay resOrg;
		private ResourceDisplay resMin;
		private ResourceDisplay resInt;
		private ResourceDisplay resRes;
		private System.Windows.Forms.Label label16;
		private ResourceDisplay resStorageRad;
		private ResourceDisplay resStorageOrg;
		private ResourceDisplay resStorageMin;
		private System.Windows.Forms.Label txtPopulation;
		private GalaxyView galaxyView;
		private GameGridView grid;
		private System.Windows.Forms.ContextMenuStrip contextMenu;
		private System.Windows.Forms.ToolStripMenuItem colonizeToolStripMenuItem;
	}
}