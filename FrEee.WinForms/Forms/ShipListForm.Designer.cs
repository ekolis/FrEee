using FrEee.Objects.Space;
using FrEee.Utility; using FrEee.Serialization;
using FrEee.WinForms.Controls;

namespace FrEee.WinForms.Forms
{
	partial class ShipListForm
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
			FrEee.WinForms.Objects.GalaxyViewModes.PresenceMode presenceMode1 = new FrEee.WinForms.Objects.GalaxyViewModes.PresenceMode();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.galaxyView = new FrEee.WinForms.Controls.GalaxyView();
			this.pnlHeader = new FrEee.WinForms.Controls.GamePanel();
			this.txtAlienShips = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.txtAllyShips = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.txtFleets = new System.Windows.Forms.Label();
			this.resMaintenanceRad = new FrEee.WinForms.Controls.ResourceDisplay();
			this.resMaintenanceOrg = new FrEee.WinForms.Controls.ResourceDisplay();
			this.resMaintenanaceMin = new FrEee.WinForms.Controls.ResourceDisplay();
			this.label16 = new System.Windows.Forms.Label();
			this.txtShipsOutsideFleets = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.txtNonAlignedShips = new System.Windows.Forms.Label();
			this.txtEnemyShips = new System.Windows.Forms.Label();
			this.txtFleetsOutsideFleets = new System.Windows.Forms.Label();
			this.txtShips = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.grid = new FrEee.WinForms.Controls.GameGridView();
			this.tableLayoutPanel1.SuspendLayout();
			this.pnlHeader.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.galaxyView, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.pnlHeader, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.grid, 0, 1);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1160, 737);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// galaxyView
			// 
			this.galaxyView.BackColor = System.Drawing.Color.Black;
			this.galaxyView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.galaxyView.Dock = System.Windows.Forms.DockStyle.Left;
			this.galaxyView.Location = new System.Drawing.Point(253, 3);
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
			this.pnlHeader.Controls.Add(this.txtAlienShips);
			this.pnlHeader.Controls.Add(this.label9);
			this.pnlHeader.Controls.Add(this.txtAllyShips);
			this.pnlHeader.Controls.Add(this.label7);
			this.pnlHeader.Controls.Add(this.txtFleets);
			this.pnlHeader.Controls.Add(this.resMaintenanceRad);
			this.pnlHeader.Controls.Add(this.resMaintenanceOrg);
			this.pnlHeader.Controls.Add(this.resMaintenanaceMin);
			this.pnlHeader.Controls.Add(this.label16);
			this.pnlHeader.Controls.Add(this.txtShipsOutsideFleets);
			this.pnlHeader.Controls.Add(this.label12);
			this.pnlHeader.Controls.Add(this.txtNonAlignedShips);
			this.pnlHeader.Controls.Add(this.txtEnemyShips);
			this.pnlHeader.Controls.Add(this.txtFleetsOutsideFleets);
			this.pnlHeader.Controls.Add(this.txtShips);
			this.pnlHeader.Controls.Add(this.label6);
			this.pnlHeader.Controls.Add(this.label5);
			this.pnlHeader.Controls.Add(this.label4);
			this.pnlHeader.Controls.Add(this.label3);
			this.pnlHeader.Controls.Add(this.label1);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlHeader.ForeColor = System.Drawing.Color.White;
			this.pnlHeader.Location = new System.Drawing.Point(3, 3);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Padding = new System.Windows.Forms.Padding(3);
			this.pnlHeader.Size = new System.Drawing.Size(244, 194);
			this.pnlHeader.TabIndex = 0;
			// 
			// txtAlienShips
			// 
			this.txtAlienShips.AutoSize = true;
			this.txtAlienShips.Location = new System.Drawing.Point(183, 54);
			this.txtAlienShips.MaximumSize = new System.Drawing.Size(30, 30);
			this.txtAlienShips.MinimumSize = new System.Drawing.Size(30, 0);
			this.txtAlienShips.Name = "txtAlienShips";
			this.txtAlienShips.Size = new System.Drawing.Size(30, 13);
			this.txtAlienShips.TabIndex = 43;
			this.txtAlienShips.Text = "0";
			this.txtAlienShips.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label9.Location = new System.Drawing.Point(15, 82);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(96, 13);
			this.label9.TabIndex = 42;
			this.label9.Text = "owned by Enemies";
			// 
			// txtAllyShips
			// 
			this.txtAllyShips.AutoSize = true;
			this.txtAllyShips.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.txtAllyShips.Location = new System.Drawing.Point(183, 67);
			this.txtAllyShips.MaximumSize = new System.Drawing.Size(30, 30);
			this.txtAllyShips.MinimumSize = new System.Drawing.Size(30, 0);
			this.txtAllyShips.Name = "txtAllyShips";
			this.txtAllyShips.Size = new System.Drawing.Size(30, 13);
			this.txtAllyShips.TabIndex = 41;
			this.txtAllyShips.Text = "0";
			this.txtAllyShips.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label7.Location = new System.Drawing.Point(4, 56);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(158, 13);
			this.label7.TabIndex = 40;
			this.label7.Text = "Known Alien Ships/Bases/Units";
			// 
			// txtFleets
			// 
			this.txtFleets.AutoSize = true;
			this.txtFleets.Location = new System.Drawing.Point(183, 30);
			this.txtFleets.MaximumSize = new System.Drawing.Size(30, 30);
			this.txtFleets.MinimumSize = new System.Drawing.Size(30, 0);
			this.txtFleets.Name = "txtFleets";
			this.txtFleets.Size = new System.Drawing.Size(30, 13);
			this.txtFleets.TabIndex = 39;
			this.txtFleets.Text = "0";
			this.txtFleets.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// resMaintenanceRad
			// 
			this.resMaintenanceRad.Amount = 0;
			this.resMaintenanceRad.BackColor = System.Drawing.Color.Black;
			this.resMaintenanceRad.Change = null;
			this.resMaintenanceRad.ForeColor = System.Drawing.Color.White;
			this.resMaintenanceRad.Location = new System.Drawing.Point(106, 144);
			this.resMaintenanceRad.Margin = new System.Windows.Forms.Padding(0);
			this.resMaintenanceRad.Name = "resMaintenanceRad";
			this.resMaintenanceRad.ResourceName = "Radioactives";
			this.resMaintenanceRad.Size = new System.Drawing.Size(107, 12);
			this.resMaintenanceRad.TabIndex = 37;
			// 
			// resMaintenanceOrg
			// 
			this.resMaintenanceOrg.Amount = 0;
			this.resMaintenanceOrg.BackColor = System.Drawing.Color.Black;
			this.resMaintenanceOrg.Change = null;
			this.resMaintenanceOrg.ForeColor = System.Drawing.Color.White;
			this.resMaintenanceOrg.Location = new System.Drawing.Point(106, 132);
			this.resMaintenanceOrg.Margin = new System.Windows.Forms.Padding(0);
			this.resMaintenanceOrg.Name = "resMaintenanceOrg";
			this.resMaintenanceOrg.ResourceName = "Organics";
			this.resMaintenanceOrg.Size = new System.Drawing.Size(107, 12);
			this.resMaintenanceOrg.TabIndex = 36;
			// 
			// resMaintenanaceMin
			// 
			this.resMaintenanaceMin.Amount = 0;
			this.resMaintenanaceMin.BackColor = System.Drawing.Color.Black;
			this.resMaintenanaceMin.Change = null;
			this.resMaintenanaceMin.ForeColor = System.Drawing.Color.White;
			this.resMaintenanaceMin.Location = new System.Drawing.Point(106, 120);
			this.resMaintenanaceMin.Margin = new System.Windows.Forms.Padding(0);
			this.resMaintenanaceMin.Name = "resMaintenanaceMin";
			this.resMaintenanaceMin.ResourceName = "Minerals";
			this.resMaintenanaceMin.Size = new System.Drawing.Size(107, 12);
			this.resMaintenanaceMin.TabIndex = 35;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label16.Location = new System.Drawing.Point(2, 107);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(120, 13);
			this.label16.TabIndex = 34;
			this.label16.Text = "Total Maintenance Cost";
			// 
			// txtShipsOutsideFleets
			// 
			this.txtShipsOutsideFleets.AutoSize = true;
			this.txtShipsOutsideFleets.Location = new System.Drawing.Point(185, 17);
			this.txtShipsOutsideFleets.MaximumSize = new System.Drawing.Size(30, 30);
			this.txtShipsOutsideFleets.MinimumSize = new System.Drawing.Size(30, 0);
			this.txtShipsOutsideFleets.Name = "txtShipsOutsideFleets";
			this.txtShipsOutsideFleets.Size = new System.Drawing.Size(30, 13);
			this.txtShipsOutsideFleets.TabIndex = 24;
			this.txtShipsOutsideFleets.Text = "0";
			this.txtShipsOutsideFleets.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label12.Location = new System.Drawing.Point(17, 17);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(84, 13);
			this.label12.TabIndex = 23;
			this.label12.Text = "outside of Fleets";
			// 
			// txtNonAlignedShips
			// 
			this.txtNonAlignedShips.AutoSize = true;
			this.txtNonAlignedShips.Location = new System.Drawing.Point(183, 93);
			this.txtNonAlignedShips.MaximumSize = new System.Drawing.Size(30, 30);
			this.txtNonAlignedShips.MinimumSize = new System.Drawing.Size(30, 0);
			this.txtNonAlignedShips.Name = "txtNonAlignedShips";
			this.txtNonAlignedShips.Size = new System.Drawing.Size(30, 13);
			this.txtNonAlignedShips.TabIndex = 14;
			this.txtNonAlignedShips.Text = "0";
			this.txtNonAlignedShips.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// txtEnemyShips
			// 
			this.txtEnemyShips.AutoSize = true;
			this.txtEnemyShips.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.txtEnemyShips.Location = new System.Drawing.Point(183, 80);
			this.txtEnemyShips.MaximumSize = new System.Drawing.Size(30, 30);
			this.txtEnemyShips.MinimumSize = new System.Drawing.Size(30, 0);
			this.txtEnemyShips.Name = "txtEnemyShips";
			this.txtEnemyShips.Size = new System.Drawing.Size(30, 13);
			this.txtEnemyShips.TabIndex = 13;
			this.txtEnemyShips.Text = "0";
			this.txtEnemyShips.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// txtFleetsOutsideFleets
			// 
			this.txtFleetsOutsideFleets.AutoSize = true;
			this.txtFleetsOutsideFleets.Location = new System.Drawing.Point(183, 43);
			this.txtFleetsOutsideFleets.MaximumSize = new System.Drawing.Size(30, 30);
			this.txtFleetsOutsideFleets.MinimumSize = new System.Drawing.Size(30, 0);
			this.txtFleetsOutsideFleets.Name = "txtFleetsOutsideFleets";
			this.txtFleetsOutsideFleets.Size = new System.Drawing.Size(30, 13);
			this.txtFleetsOutsideFleets.TabIndex = 12;
			this.txtFleetsOutsideFleets.Text = "0";
			this.txtFleetsOutsideFleets.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// txtShips
			// 
			this.txtShips.AutoSize = true;
			this.txtShips.Location = new System.Drawing.Point(185, 4);
			this.txtShips.MaximumSize = new System.Drawing.Size(30, 30);
			this.txtShips.MinimumSize = new System.Drawing.Size(30, 0);
			this.txtShips.Name = "txtShips";
			this.txtShips.Size = new System.Drawing.Size(30, 13);
			this.txtShips.TabIndex = 10;
			this.txtShips.Text = "0";
			this.txtShips.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label6.Location = new System.Drawing.Point(15, 93);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(114, 13);
			this.label6.TabIndex = 5;
			this.label6.Text = "owned by Non-Aligned";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label5.Location = new System.Drawing.Point(15, 69);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "owned by Allies";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label4.Location = new System.Drawing.Point(15, 43);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(113, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "outside of Other Fleets";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label3.Location = new System.Drawing.Point(4, 30);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(62, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Total Fleets";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label1.Location = new System.Drawing.Point(4, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(123, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Total Ships/Bases/Units";
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
			// ShipListForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(1184, 761);
			this.Controls.Add(this.tableLayoutPanel1);
			this.DoubleBuffered = true;
			this.ForeColor = System.Drawing.Color.White;
			this.MinimumSize = new System.Drawing.Size(800, 600);
			this.Name = "ShipListForm";
			this.ShowInTaskbar = false;
			this.Text = "Ships/Fleets";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PlanetListForm_FormClosed);
			this.Load += new System.EventHandler(this.ShipListForm_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.pnlHeader.ResumeLayout(false);
			this.pnlHeader.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private GamePanel pnlHeader;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label txtFleetsOutsideFleets;
		private System.Windows.Forms.Label txtShips;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label txtShipsOutsideFleets;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label16;
		private ResourceDisplay resMaintenanceRad;
		private ResourceDisplay resMaintenanceOrg;
		private ResourceDisplay resMaintenanaceMin;
		private GalaxyView galaxyView;
		private GameGridView grid;
		private System.Windows.Forms.Label txtFleets;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label txtAllyShips;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label txtNonAlignedShips;
		private System.Windows.Forms.Label txtEnemyShips;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label txtAlienShips;
	}
}