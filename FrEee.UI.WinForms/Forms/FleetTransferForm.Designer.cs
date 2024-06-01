namespace FrEee.UI.WinForms.Forms;

partial class FleetTransferForm
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
		this.treeFleets = new System.Windows.Forms.TreeView();
		this.panel1 = new System.Windows.Forms.Panel();
		this.lblQuantityUnit = new System.Windows.Forms.Label();
		this.btnCancel = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnOK = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnAdd = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnRemove = new FrEee.UI.WinForms.Controls.GameButton();
		this.label1 = new System.Windows.Forms.Label();
		this.treeVehicles = new System.Windows.Forms.TreeView();
		this.panel2 = new System.Windows.Forms.Panel();
		this.txtFleetName = new System.Windows.Forms.TextBox();
		this.btnDisband = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnCreate = new FrEee.UI.WinForms.Controls.GameButton();
		this.label2 = new System.Windows.Forms.Label();
		this.tableLayoutPanel1.SuspendLayout();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.SuspendLayout();
		// 
		// tableLayoutPanel1
		// 
		this.tableLayoutPanel1.ColumnCount = 4;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
		this.tableLayoutPanel1.Controls.Add(this.treeFleets, 3, 1);
		this.tableLayoutPanel1.Controls.Add(this.panel1, 3, 2);
		this.tableLayoutPanel1.Controls.Add(this.btnAdd, 2, 1);
		this.tableLayoutPanel1.Controls.Add(this.btnRemove, 1, 1);
		this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.treeVehicles, 0, 1);
		this.tableLayoutPanel1.Controls.Add(this.panel2, 3, 0);
		this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.RowCount = 3;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
		this.tableLayoutPanel1.Size = new System.Drawing.Size(982, 561);
		this.tableLayoutPanel1.TabIndex = 0;
		// 
		// treeFleets
		// 
		this.treeFleets.BackColor = System.Drawing.Color.Black;
		this.treeFleets.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.treeFleets.Dock = System.Windows.Forms.DockStyle.Fill;
		this.treeFleets.ForeColor = System.Drawing.Color.White;
		this.treeFleets.HideSelection = false;
		this.treeFleets.Location = new System.Drawing.Point(594, 27);
		this.treeFleets.Name = "treeFleets";
		this.treeFleets.Size = new System.Drawing.Size(385, 507);
		this.treeFleets.TabIndex = 19;
		this.treeFleets.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeFleets_NodeMouseClick);
		this.treeFleets.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeFleets_NodeMouseDoubleClick);
		// 
		// panel1
		// 
		this.panel1.Controls.Add(this.lblQuantityUnit);
		this.panel1.Controls.Add(this.btnCancel);
		this.panel1.Controls.Add(this.btnOK);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(591, 537);
		this.panel1.Margin = new System.Windows.Forms.Padding(0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(391, 24);
		this.panel1.TabIndex = 17;
		// 
		// lblQuantityUnit
		// 
		this.lblQuantityUnit.AutoSize = true;
		this.lblQuantityUnit.Location = new System.Drawing.Point(3, 6);
		this.lblQuantityUnit.Name = "lblQuantityUnit";
		this.lblQuantityUnit.Size = new System.Drawing.Size(0, 13);
		this.lblQuantityUnit.TabIndex = 2;
		// 
		// btnCancel
		// 
		this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.btnCancel.BackColor = System.Drawing.Color.Black;
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnCancel.Location = new System.Drawing.Point(182, 3);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(100, 18);
		this.btnCancel.TabIndex = 1;
		this.btnCancel.Text = "Cancel";
		this.btnCancel.UseVisualStyleBackColor = false;
		this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
		// 
		// btnOK
		// 
		this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.btnOK.BackColor = System.Drawing.Color.Black;
		this.btnOK.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnOK.Location = new System.Drawing.Point(288, 3);
		this.btnOK.Name = "btnOK";
		this.btnOK.Size = new System.Drawing.Size(100, 18);
		this.btnOK.TabIndex = 0;
		this.btnOK.Text = "OK";
		this.btnOK.UseVisualStyleBackColor = false;
		this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
		// 
		// btnAdd
		// 
		this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.btnAdd.BackColor = System.Drawing.Color.Black;
		this.btnAdd.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnAdd.Location = new System.Drawing.Point(494, 27);
		this.btnAdd.Name = "btnAdd";
		this.btnAdd.Size = new System.Drawing.Size(94, 507);
		this.btnAdd.TabIndex = 16;
		this.btnAdd.Text = ">";
		this.btnAdd.UseVisualStyleBackColor = false;
		this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
		// 
		// btnRemove
		// 
		this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.btnRemove.BackColor = System.Drawing.Color.Black;
		this.btnRemove.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnRemove.Location = new System.Drawing.Point(394, 27);
		this.btnRemove.Name = "btnRemove";
		this.btnRemove.Size = new System.Drawing.Size(94, 507);
		this.btnRemove.TabIndex = 15;
		this.btnRemove.Text = "<";
		this.btnRemove.UseVisualStyleBackColor = false;
		this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
		// 
		// label1
		// 
		this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label1.Location = new System.Drawing.Point(3, 8);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(60, 16);
		this.label1.TabIndex = 3;
		this.label1.Text = "Vehicles";
		// 
		// treeVehicles
		// 
		this.treeVehicles.BackColor = System.Drawing.Color.Black;
		this.treeVehicles.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.treeVehicles.Dock = System.Windows.Forms.DockStyle.Fill;
		this.treeVehicles.ForeColor = System.Drawing.Color.White;
		this.treeVehicles.HideSelection = false;
		this.treeVehicles.Location = new System.Drawing.Point(3, 27);
		this.treeVehicles.Name = "treeVehicles";
		this.treeVehicles.Size = new System.Drawing.Size(385, 507);
		this.treeVehicles.TabIndex = 18;
		this.treeVehicles.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeVehicles_NodeMouseClick);
		this.treeVehicles.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeVehicles_NodeMouseDoubleClick);
		// 
		// panel2
		// 
		this.panel2.Controls.Add(this.txtFleetName);
		this.panel2.Controls.Add(this.btnDisband);
		this.panel2.Controls.Add(this.btnCreate);
		this.panel2.Controls.Add(this.label2);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(591, 0);
		this.panel2.Margin = new System.Windows.Forms.Padding(0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(391, 24);
		this.panel2.TabIndex = 20;
		// 
		// txtFleetName
		// 
		this.txtFleetName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtFleetName.Location = new System.Drawing.Point(59, 3);
		this.txtFleetName.Name = "txtFleetName";
		this.txtFleetName.Size = new System.Drawing.Size(199, 20);
		this.txtFleetName.TabIndex = 7;
		this.txtFleetName.Text = "Unnamed Fleet";
		// 
		// btnDisband
		// 
		this.btnDisband.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.btnDisband.BackColor = System.Drawing.Color.Black;
		this.btnDisband.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnDisband.Location = new System.Drawing.Point(329, 3);
		this.btnDisband.Name = "btnDisband";
		this.btnDisband.Size = new System.Drawing.Size(59, 18);
		this.btnDisband.TabIndex = 6;
		this.btnDisband.Text = "Disband";
		this.btnDisband.UseVisualStyleBackColor = false;
		this.btnDisband.Click += new System.EventHandler(this.btnDisband_Click);
		// 
		// btnCreate
		// 
		this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.btnCreate.BackColor = System.Drawing.Color.Black;
		this.btnCreate.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnCreate.Location = new System.Drawing.Point(264, 3);
		this.btnCreate.Name = "btnCreate";
		this.btnCreate.Size = new System.Drawing.Size(59, 18);
		this.btnCreate.TabIndex = 3;
		this.btnCreate.Text = "Create";
		this.btnCreate.UseVisualStyleBackColor = false;
		this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
		// 
		// label2
		// 
		this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label2.Location = new System.Drawing.Point(3, 4);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(45, 16);
		this.label2.TabIndex = 5;
		this.label2.Text = "Fleets";
		// 
		// FleetTransferForm
		// 
		this.AcceptButton = this.btnOK;
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.CancelButton = this.btnCancel;
		this.ClientSize = new System.Drawing.Size(982, 561);
		this.Controls.Add(this.tableLayoutPanel1);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "FleetTransferForm";
		this.Text = "Fleet Transfer";
		this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FleetTransferForm_FormClosing);
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.PerformLayout();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private System.Windows.Forms.Label label1;
	private Controls.GameButton btnRemove;
	private Controls.GameButton btnAdd;
	private System.Windows.Forms.Panel panel1;
	private System.Windows.Forms.Label lblQuantityUnit;
	private Controls.GameButton btnCancel;
	private Controls.GameButton btnOK;
	private System.Windows.Forms.TreeView treeVehicles;
	private System.Windows.Forms.TreeView treeFleets;
	private System.Windows.Forms.Panel panel2;
	private Controls.GameButton btnCreate;
	private System.Windows.Forms.Label label2;
	private Controls.GameButton btnDisband;
	private System.Windows.Forms.TextBox txtFleetName;
}