using FrEee.Game.Objects.Space;
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
			FrEee.Utility.Resource resource1 = new FrEee.Utility.Resource();
			FrEee.Utility.Resource resource2 = new FrEee.Utility.Resource();
			FrEee.Utility.Resource resource3 = new FrEee.Utility.Resource();
			FrEee.Utility.Resource resource4 = new FrEee.Utility.Resource();
			FrEee.Utility.Resource resource5 = new FrEee.Utility.Resource();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
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
			this.gridPlanets = new System.Windows.Forms.DataGridView();
			this.iconDataGridViewImageColumn = new System.Windows.Forms.DataGridViewImageColumn();
			this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.sizeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.surfaceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.atmosphereDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.resourceValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ownerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.planetBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.galaxyView = new FrEee.WinForms.Controls.GalaxyView();
			this.gamePanel1 = new FrEee.WinForms.Controls.GamePanel();
			this.btnNewConfig = new FrEee.WinForms.Controls.GameButton();
			this.gamePanel2 = new FrEee.WinForms.Controls.GamePanel();
			this.btnDeleteConfig = new FrEee.WinForms.Controls.GameButton();
			this.btnSaveConfig = new FrEee.WinForms.Controls.GameButton();
			this.label17 = new System.Windows.Forms.Label();
			this.txtConfigName = new System.Windows.Forms.TextBox();
			this.pnlHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridPlanets)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.planetBindingSource)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.gamePanel1.SuspendLayout();
			this.gamePanel2.SuspendLayout();
			this.SuspendLayout();
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
			resource1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			resource1.IsGlobal = true;
			resource1.IsLocal = false;
			resource1.Name = "Radioactives";
			resource1.PictureName = "Resource3";
			this.resStorageRad.Resource = resource1;
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
			resource2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			resource2.IsGlobal = true;
			resource2.IsLocal = false;
			resource2.Name = "Organics";
			resource2.PictureName = "Resource2";
			this.resStorageOrg.Resource = resource2;
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
			resource3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			resource3.IsGlobal = true;
			resource3.IsLocal = false;
			resource3.Name = "Minerals";
			resource3.PictureName = "Resource1";
			this.resStorageMin.Resource = resource3;
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
			resource4.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			resource4.IsGlobal = false;
			resource4.IsLocal = false;
			resource4.Name = "Intelligence";
			resource4.PictureName = "Resource5";
			this.resInt.Resource = resource4;
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
			resource5.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			resource5.IsGlobal = false;
			resource5.IsLocal = false;
			resource5.Name = "Research";
			resource5.PictureName = "Resource4";
			this.resRes.Resource = resource5;
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
			this.resRad.Resource = resource1;
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
			this.resOrg.Resource = resource2;
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
			this.resMin.Resource = resource3;
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
			// gridPlanets
			// 
			this.gridPlanets.AllowUserToAddRows = false;
			this.gridPlanets.AllowUserToDeleteRows = false;
			this.gridPlanets.AllowUserToOrderColumns = true;
			this.gridPlanets.AllowUserToResizeRows = false;
			this.gridPlanets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridPlanets.AutoGenerateColumns = false;
			this.gridPlanets.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.gridPlanets.BackgroundColor = System.Drawing.Color.Black;
			this.gridPlanets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridPlanets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iconDataGridViewImageColumn,
            this.nameDataGridViewTextBoxColumn,
            this.sizeDataGridViewTextBoxColumn,
            this.surfaceDataGridViewTextBoxColumn,
            this.atmosphereDataGridViewTextBoxColumn,
            this.resourceValueDataGridViewTextBoxColumn,
            this.ownerDataGridViewTextBoxColumn});
			this.tableLayoutPanel1.SetColumnSpan(this.gridPlanets, 2);
			this.gridPlanets.DataSource = this.planetBindingSource;
			this.gridPlanets.Location = new System.Drawing.Point(103, 243);
			this.gridPlanets.Name = "gridPlanets";
			this.gridPlanets.ReadOnly = true;
			this.gridPlanets.RowHeadersVisible = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
			this.gridPlanets.RowsDefaultCellStyle = dataGridViewCellStyle1;
			this.gridPlanets.RowTemplate.Height = 32;
			this.gridPlanets.RowTemplate.ReadOnly = true;
			this.gridPlanets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridPlanets.Size = new System.Drawing.Size(654, 291);
			this.gridPlanets.TabIndex = 1;
			this.gridPlanets.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridPlanets_ColumnHeaderMouseClick);
			this.gridPlanets.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gridPlanets_DataError);
			this.gridPlanets.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPlanets_RowEnter);
			this.gridPlanets.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPlanets_RowLeave);
			// 
			// iconDataGridViewImageColumn
			// 
			this.iconDataGridViewImageColumn.DataPropertyName = "Icon";
			this.iconDataGridViewImageColumn.FillWeight = 7.252303F;
			this.iconDataGridViewImageColumn.HeaderText = "Icon";
			this.iconDataGridViewImageColumn.Name = "iconDataGridViewImageColumn";
			this.iconDataGridViewImageColumn.ReadOnly = true;
			this.iconDataGridViewImageColumn.Width = 34;
			// 
			// nameDataGridViewTextBoxColumn
			// 
			this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
			this.nameDataGridViewTextBoxColumn.FillWeight = 16.82389F;
			this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
			this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
			this.nameDataGridViewTextBoxColumn.ReadOnly = true;
			this.nameDataGridViewTextBoxColumn.Width = 60;
			// 
			// sizeDataGridViewTextBoxColumn
			// 
			this.sizeDataGridViewTextBoxColumn.DataPropertyName = "Size";
			this.sizeDataGridViewTextBoxColumn.FillWeight = 21.46247F;
			this.sizeDataGridViewTextBoxColumn.HeaderText = "Size";
			this.sizeDataGridViewTextBoxColumn.Name = "sizeDataGridViewTextBoxColumn";
			this.sizeDataGridViewTextBoxColumn.ReadOnly = true;
			this.sizeDataGridViewTextBoxColumn.Width = 52;
			// 
			// surfaceDataGridViewTextBoxColumn
			// 
			this.surfaceDataGridViewTextBoxColumn.DataPropertyName = "Surface";
			this.surfaceDataGridViewTextBoxColumn.FillWeight = 28.13256F;
			this.surfaceDataGridViewTextBoxColumn.HeaderText = "Surface";
			this.surfaceDataGridViewTextBoxColumn.Name = "surfaceDataGridViewTextBoxColumn";
			this.surfaceDataGridViewTextBoxColumn.ReadOnly = true;
			this.surfaceDataGridViewTextBoxColumn.Width = 69;
			// 
			// atmosphereDataGridViewTextBoxColumn
			// 
			this.atmosphereDataGridViewTextBoxColumn.DataPropertyName = "Atmosphere";
			this.atmosphereDataGridViewTextBoxColumn.FillWeight = 37.72384F;
			this.atmosphereDataGridViewTextBoxColumn.HeaderText = "Atmosphere";
			this.atmosphereDataGridViewTextBoxColumn.Name = "atmosphereDataGridViewTextBoxColumn";
			this.atmosphereDataGridViewTextBoxColumn.ReadOnly = true;
			this.atmosphereDataGridViewTextBoxColumn.Width = 88;
			// 
			// resourceValueDataGridViewTextBoxColumn
			// 
			this.resourceValueDataGridViewTextBoxColumn.DataPropertyName = "ResourceValue";
			this.resourceValueDataGridViewTextBoxColumn.FillWeight = 375.407F;
			this.resourceValueDataGridViewTextBoxColumn.HeaderText = "ResourceValue";
			this.resourceValueDataGridViewTextBoxColumn.Name = "resourceValueDataGridViewTextBoxColumn";
			this.resourceValueDataGridViewTextBoxColumn.ReadOnly = true;
			this.resourceValueDataGridViewTextBoxColumn.Width = 105;
			// 
			// ownerDataGridViewTextBoxColumn
			// 
			this.ownerDataGridViewTextBoxColumn.DataPropertyName = "Owner";
			this.ownerDataGridViewTextBoxColumn.FillWeight = 213.198F;
			this.ownerDataGridViewTextBoxColumn.HeaderText = "Owner";
			this.ownerDataGridViewTextBoxColumn.Name = "ownerDataGridViewTextBoxColumn";
			this.ownerDataGridViewTextBoxColumn.ReadOnly = true;
			this.ownerDataGridViewTextBoxColumn.Width = 63;
			// 
			// planetBindingSource
			// 
			this.planetBindingSource.AllowNew = false;
			this.planetBindingSource.DataSource = typeof(FrEee.Game.Objects.Space.Planet);
			this.planetBindingSource.Sort = "";
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
			this.tableLayoutPanel1.Controls.Add(this.gridPlanets, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.gamePanel1, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.gamePanel2, 1, 1);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(760, 537);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// galaxyView
			// 
			this.galaxyView.BackColor = System.Drawing.Color.Black;
			this.galaxyView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.galaxyView.Location = new System.Drawing.Point(403, 3);
			this.galaxyView.Name = "galaxyView";
			this.galaxyView.SelectedStarSystem = null;
			this.galaxyView.Size = new System.Drawing.Size(354, 194);
			this.galaxyView.TabIndex = 22;
			this.galaxyView.Text = "galaxyView1";
			// 
			// gamePanel1
			// 
			this.gamePanel1.BackColor = System.Drawing.Color.Black;
			this.gamePanel1.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.gamePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.gamePanel1.Controls.Add(this.btnNewConfig);
			this.gamePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gamePanel1.ForeColor = System.Drawing.Color.White;
			this.gamePanel1.Location = new System.Drawing.Point(3, 243);
			this.gamePanel1.Name = "gamePanel1";
			this.gamePanel1.Padding = new System.Windows.Forms.Padding(3);
			this.gamePanel1.Size = new System.Drawing.Size(94, 291);
			this.gamePanel1.TabIndex = 23;
			// 
			// btnNewConfig
			// 
			this.btnNewConfig.BackColor = System.Drawing.Color.Black;
			this.btnNewConfig.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnNewConfig.Location = new System.Drawing.Point(6, 6);
			this.btnNewConfig.Name = "btnNewConfig";
			this.btnNewConfig.Size = new System.Drawing.Size(80, 23);
			this.btnNewConfig.TabIndex = 1;
			this.btnNewConfig.Text = "(New Config)";
			this.btnNewConfig.UseVisualStyleBackColor = false;
			// 
			// gamePanel2
			// 
			this.gamePanel2.BackColor = System.Drawing.Color.Black;
			this.gamePanel2.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.gamePanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tableLayoutPanel1.SetColumnSpan(this.gamePanel2, 2);
			this.gamePanel2.Controls.Add(this.btnDeleteConfig);
			this.gamePanel2.Controls.Add(this.btnSaveConfig);
			this.gamePanel2.Controls.Add(this.label17);
			this.gamePanel2.Controls.Add(this.txtConfigName);
			this.gamePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gamePanel2.ForeColor = System.Drawing.Color.White;
			this.gamePanel2.Location = new System.Drawing.Point(103, 203);
			this.gamePanel2.Name = "gamePanel2";
			this.gamePanel2.Padding = new System.Windows.Forms.Padding(3);
			this.gamePanel2.Size = new System.Drawing.Size(654, 34);
			this.gamePanel2.TabIndex = 25;
			// 
			// btnDeleteConfig
			// 
			this.btnDeleteConfig.BackColor = System.Drawing.Color.Black;
			this.btnDeleteConfig.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnDeleteConfig.Location = new System.Drawing.Point(268, 7);
			this.btnDeleteConfig.Name = "btnDeleteConfig";
			this.btnDeleteConfig.Size = new System.Drawing.Size(75, 19);
			this.btnDeleteConfig.TabIndex = 28;
			this.btnDeleteConfig.Text = "Delete";
			this.btnDeleteConfig.UseVisualStyleBackColor = false;
			this.btnDeleteConfig.Click += new System.EventHandler(this.btnDeleteConfig_Click);
			// 
			// btnSaveConfig
			// 
			this.btnSaveConfig.BackColor = System.Drawing.Color.Black;
			this.btnSaveConfig.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnSaveConfig.Location = new System.Drawing.Point(187, 7);
			this.btnSaveConfig.Name = "btnSaveConfig";
			this.btnSaveConfig.Size = new System.Drawing.Size(75, 19);
			this.btnSaveConfig.TabIndex = 27;
			this.btnSaveConfig.Text = "Save";
			this.btnSaveConfig.UseVisualStyleBackColor = false;
			this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label17.Location = new System.Drawing.Point(6, 9);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(68, 13);
			this.label17.TabIndex = 26;
			this.label17.Text = "Config Name";
			// 
			// txtConfigName
			// 
			this.txtConfigName.Location = new System.Drawing.Point(80, 6);
			this.txtConfigName.Name = "txtConfigName";
			this.txtConfigName.Size = new System.Drawing.Size(100, 20);
			this.txtConfigName.TabIndex = 25;
			// 
			// PlanetListForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(784, 561);
			this.Controls.Add(this.tableLayoutPanel1);
			this.DoubleBuffered = true;
			this.ForeColor = System.Drawing.Color.White;
			this.MinimumSize = new System.Drawing.Size(800, 600);
			this.Name = "PlanetListForm";
			this.ShowInTaskbar = false;
			this.Text = "Planets";
			this.Load += new System.EventHandler(this.PlanetListForm_Load);
			this.pnlHeader.ResumeLayout(false);
			this.pnlHeader.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridPlanets)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.planetBindingSource)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.gamePanel1.ResumeLayout(false);
			this.gamePanel2.ResumeLayout(false);
			this.gamePanel2.PerformLayout();
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
		private System.Windows.Forms.DataGridView gridPlanets;
		private System.Windows.Forms.BindingSource planetBindingSource;
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
		private GamePanel gamePanel1;
		private GameButton btnNewConfig;
		private GamePanel gamePanel2;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.TextBox txtConfigName;
		private GameButton btnDeleteConfig;
		private GameButton btnSaveConfig;
		private System.Windows.Forms.DataGridViewImageColumn iconDataGridViewImageColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn sizeDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn surfaceDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn atmosphereDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn resourceValueDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn ownerDataGridViewTextBoxColumn;
	}
}