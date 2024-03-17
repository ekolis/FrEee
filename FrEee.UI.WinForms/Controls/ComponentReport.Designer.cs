using FrEee.UI.WinForms.Controls.Blazor;

namespace FrEee.UI.WinForms.Controls;

partial class ComponentReport
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
		this.txtName = new System.Windows.Forms.Label();
		this.txtDescription = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.table = new System.Windows.Forms.TableLayoutPanel();
		this.txtSize = new System.Windows.Forms.Label();
		this.txtDurability = new System.Windows.Forms.Label();
		this.txtSupplyUsage = new System.Windows.Forms.Label();
		this.txtVehicleTypes = new System.Windows.Forms.Label();
		this.pnlWeapon = new System.Windows.Forms.Panel();
		this.label7 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.txtWeaponType = new System.Windows.Forms.Label();
		this.txtTargets = new System.Windows.Forms.Label();
		this.txtReload = new System.Windows.Forms.Label();
		this.txtDamageType = new System.Windows.Forms.Label();
		this.pnlSeeker = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.txtSeekerSpeed = new System.Windows.Forms.Label();
		this.txtSeekerDurability = new System.Windows.Forms.Label();
		this.pnlAccuracy = new System.Windows.Forms.Panel();
		this.txtAccuracy = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.resRad = new FrEee.UI.WinForms.Controls.Blazor.ResourceDisplay();
		this.resOrg = new FrEee.UI.WinForms.Controls.Blazor.ResourceDisplay();
		this.resMin = new FrEee.UI.WinForms.Controls.Blazor.ResourceDisplay();
		this.abilityTree = new FrEee.UI.WinForms.Controls.AbilityTreeView();
		this.picPortrait = new FrEee.UI.WinForms.Controls.Blazor.GamePictureBox();
		this.pnlDamage = new System.Windows.Forms.Panel();
		this.damageGraph = new FrEee.UI.WinForms.Controls.LineGraph();
		this.label11 = new System.Windows.Forms.Label();
		this.table.SuspendLayout();
		this.pnlWeapon.SuspendLayout();
		this.pnlSeeker.SuspendLayout();
		this.pnlAccuracy.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.picPortrait)).BeginInit();
		this.pnlDamage.SuspendLayout();
		this.SuspendLayout();
		// 
		// txtName
		// 
		this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtName.Location = new System.Drawing.Point(138, 4);
		this.txtName.Name = "txtName";
		this.txtName.Size = new System.Drawing.Size(224, 45);
		this.txtName.TabIndex = 1;
		this.txtName.Text = "Name";
		// 
		// txtDescription
		// 
		this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtDescription.Location = new System.Drawing.Point(137, 49);
		this.txtDescription.Name = "txtDescription";
		this.txtDescription.Size = new System.Drawing.Size(225, 82);
		this.txtDescription.TabIndex = 2;
		this.txtDescription.Text = "Description";
		// 
		// label2
		// 
		this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label2.Location = new System.Drawing.Point(3, 134);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(128, 18);
		this.label2.TabIndex = 3;
		this.label2.Text = "Cost";
		// 
		// label3
		// 
		this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label3.Location = new System.Drawing.Point(3, 152);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(128, 18);
		this.label3.TabIndex = 4;
		this.label3.Text = "Size";
		// 
		// label4
		// 
		this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label4.Location = new System.Drawing.Point(3, 170);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(128, 18);
		this.label4.TabIndex = 5;
		this.label4.Text = "Durability";
		// 
		// label5
		// 
		this.label5.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label5.Location = new System.Drawing.Point(3, 188);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(128, 18);
		this.label5.TabIndex = 6;
		this.label5.Text = "Supply Usage";
		// 
		// label6
		// 
		this.label6.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label6.Location = new System.Drawing.Point(3, 206);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(128, 18);
		this.label6.TabIndex = 7;
		this.label6.Text = "Vehicle Types";
		// 
		// table
		// 
		this.table.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.table.ColumnCount = 1;
		this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
		this.table.Controls.Add(this.pnlDamage, 0, 3);
		this.table.Controls.Add(this.pnlAccuracy, 0, 1);
		this.table.Controls.Add(this.pnlSeeker, 0, 2);
		this.table.Controls.Add(this.abilityTree, 0, 4);
		this.table.Controls.Add(this.pnlWeapon, 0, 0);
		this.table.Location = new System.Drawing.Point(3, 227);
		this.table.Name = "table";
		this.table.RowCount = 5;
		this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
		this.table.Size = new System.Drawing.Size(359, 324);
		this.table.TabIndex = 9;
		// 
		// txtSize
		// 
		this.txtSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtSize.Location = new System.Drawing.Point(137, 154);
		this.txtSize.Name = "txtSize";
		this.txtSize.Size = new System.Drawing.Size(223, 18);
		this.txtSize.TabIndex = 13;
		this.txtSize.Text = "0 kT";
		// 
		// txtDurability
		// 
		this.txtDurability.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtDurability.Location = new System.Drawing.Point(137, 170);
		this.txtDurability.Name = "txtDurability";
		this.txtDurability.Size = new System.Drawing.Size(223, 18);
		this.txtDurability.TabIndex = 14;
		this.txtDurability.Text = "0 HP";
		// 
		// txtSupplyUsage
		// 
		this.txtSupplyUsage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtSupplyUsage.Location = new System.Drawing.Point(135, 188);
		this.txtSupplyUsage.Name = "txtSupplyUsage";
		this.txtSupplyUsage.Size = new System.Drawing.Size(223, 18);
		this.txtSupplyUsage.TabIndex = 15;
		this.txtSupplyUsage.Text = "0 supplies";
		// 
		// txtVehicleTypes
		// 
		this.txtVehicleTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtVehicleTypes.Location = new System.Drawing.Point(135, 206);
		this.txtVehicleTypes.Name = "txtVehicleTypes";
		this.txtVehicleTypes.Size = new System.Drawing.Size(223, 18);
		this.txtVehicleTypes.TabIndex = 15;
		this.txtVehicleTypes.Text = "Vehicle Types";
		// 
		// pnlWeapon
		// 
		this.pnlWeapon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.pnlWeapon.Controls.Add(this.txtDamageType);
		this.pnlWeapon.Controls.Add(this.txtReload);
		this.pnlWeapon.Controls.Add(this.txtTargets);
		this.pnlWeapon.Controls.Add(this.txtWeaponType);
		this.pnlWeapon.Controls.Add(this.label10);
		this.pnlWeapon.Controls.Add(this.label9);
		this.pnlWeapon.Controls.Add(this.label8);
		this.pnlWeapon.Controls.Add(this.label7);
		this.pnlWeapon.Location = new System.Drawing.Point(0, 0);
		this.pnlWeapon.Margin = new System.Windows.Forms.Padding(0);
		this.pnlWeapon.Name = "pnlWeapon";
		this.pnlWeapon.Size = new System.Drawing.Size(359, 72);
		this.pnlWeapon.TabIndex = 1;
		// 
		// label7
		// 
		this.label7.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label7.Location = new System.Drawing.Point(0, 0);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(128, 18);
		this.label7.TabIndex = 9;
		this.label7.Text = "Weapon Type";
		// 
		// label8
		// 
		this.label8.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label8.Location = new System.Drawing.Point(0, 18);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(128, 18);
		this.label8.TabIndex = 10;
		this.label8.Text = "Can Target";
		// 
		// label9
		// 
		this.label9.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label9.Location = new System.Drawing.Point(0, 36);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(128, 18);
		this.label9.TabIndex = 11;
		this.label9.Text = "Reload Rate";
		// 
		// label10
		// 
		this.label10.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label10.Location = new System.Drawing.Point(0, 54);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(128, 18);
		this.label10.TabIndex = 12;
		this.label10.Text = "Damage Type";
		// 
		// txtWeaponType
		// 
		this.txtWeaponType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtWeaponType.Location = new System.Drawing.Point(132, 0);
		this.txtWeaponType.Name = "txtWeaponType";
		this.txtWeaponType.Size = new System.Drawing.Size(223, 18);
		this.txtWeaponType.TabIndex = 16;
		this.txtWeaponType.Text = "Weapon Type";
		// 
		// txtTargets
		// 
		this.txtTargets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtTargets.Location = new System.Drawing.Point(132, 18);
		this.txtTargets.Name = "txtTargets";
		this.txtTargets.Size = new System.Drawing.Size(223, 18);
		this.txtTargets.TabIndex = 17;
		this.txtTargets.Text = "Targets";
		// 
		// txtReload
		// 
		this.txtReload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtReload.Location = new System.Drawing.Point(132, 36);
		this.txtReload.Name = "txtReload";
		this.txtReload.Size = new System.Drawing.Size(223, 18);
		this.txtReload.TabIndex = 18;
		this.txtReload.Text = "0 rounds";
		// 
		// txtDamageType
		// 
		this.txtDamageType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtDamageType.Location = new System.Drawing.Point(132, 54);
		this.txtDamageType.Name = "txtDamageType";
		this.txtDamageType.Size = new System.Drawing.Size(223, 18);
		this.txtDamageType.TabIndex = 19;
		this.txtDamageType.Text = "Damage Type";
		// 
		// pnlSeeker
		// 
		this.pnlSeeker.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.pnlSeeker.Controls.Add(this.txtSeekerDurability);
		this.pnlSeeker.Controls.Add(this.txtSeekerSpeed);
		this.pnlSeeker.Controls.Add(this.label12);
		this.pnlSeeker.Controls.Add(this.label1);
		this.pnlSeeker.Location = new System.Drawing.Point(0, 90);
		this.pnlSeeker.Margin = new System.Windows.Forms.Padding(0);
		this.pnlSeeker.Name = "pnlSeeker";
		this.pnlSeeker.Size = new System.Drawing.Size(359, 36);
		this.pnlSeeker.TabIndex = 24;
		// 
		// label1
		// 
		this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label1.Location = new System.Drawing.Point(0, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(128, 18);
		this.label1.TabIndex = 13;
		this.label1.Text = "Seeker Speed";
		// 
		// label12
		// 
		this.label12.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label12.Location = new System.Drawing.Point(0, 18);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(128, 18);
		this.label12.TabIndex = 14;
		this.label12.Text = "Seeker Durability";
		// 
		// txtSeekerSpeed
		// 
		this.txtSeekerSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtSeekerSpeed.Location = new System.Drawing.Point(132, 0);
		this.txtSeekerSpeed.Name = "txtSeekerSpeed";
		this.txtSeekerSpeed.Size = new System.Drawing.Size(223, 18);
		this.txtSeekerSpeed.TabIndex = 20;
		this.txtSeekerSpeed.Text = "0 squares / round";
		// 
		// txtSeekerDurability
		// 
		this.txtSeekerDurability.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtSeekerDurability.Location = new System.Drawing.Point(132, 16);
		this.txtSeekerDurability.Name = "txtSeekerDurability";
		this.txtSeekerDurability.Size = new System.Drawing.Size(223, 18);
		this.txtSeekerDurability.TabIndex = 21;
		this.txtSeekerDurability.Text = "0 HP";
		// 
		// pnlAccuracy
		// 
		this.pnlAccuracy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.pnlAccuracy.Controls.Add(this.txtAccuracy);
		this.pnlAccuracy.Controls.Add(this.label16);
		this.pnlAccuracy.Location = new System.Drawing.Point(0, 72);
		this.pnlAccuracy.Margin = new System.Windows.Forms.Padding(0);
		this.pnlAccuracy.Name = "pnlAccuracy";
		this.pnlAccuracy.Size = new System.Drawing.Size(359, 18);
		this.pnlAccuracy.TabIndex = 26;
		// 
		// txtAccuracy
		// 
		this.txtAccuracy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtAccuracy.Location = new System.Drawing.Point(132, 0);
		this.txtAccuracy.Name = "txtAccuracy";
		this.txtAccuracy.Size = new System.Drawing.Size(223, 18);
		this.txtAccuracy.TabIndex = 20;
		this.txtAccuracy.Text = "0%";
		// 
		// label16
		// 
		this.label16.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label16.Location = new System.Drawing.Point(0, 0);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(128, 18);
		this.label16.TabIndex = 13;
		this.label16.Text = "Accuracy Modifier";
		// 
		// resRad
		// 
		this.resRad.Amount = 0;
		this.resRad.BackColor = System.Drawing.Color.Black;
		this.resRad.Change = null;
		this.resRad.ForeColor = System.Drawing.Color.White;
		this.resRad.Location = new System.Drawing.Point(287, 134);
		this.resRad.Margin = new System.Windows.Forms.Padding(0);
		this.resRad.Name = "resRad";
		this.resRad.ResourceName = "Radioactives";
		this.resRad.Size = new System.Drawing.Size(73, 20);
		this.resRad.TabIndex = 12;
		// 
		// resOrg
		// 
		this.resOrg.Amount = 0;
		this.resOrg.BackColor = System.Drawing.Color.Black;
		this.resOrg.Change = null;
		this.resOrg.ForeColor = System.Drawing.Color.White;
		this.resOrg.Location = new System.Drawing.Point(211, 134);
		this.resOrg.Margin = new System.Windows.Forms.Padding(0);
		this.resOrg.Name = "resOrg";
		this.resOrg.ResourceName = "Organics";
		this.resOrg.Size = new System.Drawing.Size(73, 20);
		this.resOrg.TabIndex = 11;
		// 
		// resMin
		// 
		this.resMin.Amount = 0;
		this.resMin.BackColor = System.Drawing.Color.Black;
		this.resMin.Change = null;
		this.resMin.ForeColor = System.Drawing.Color.White;
		this.resMin.Location = new System.Drawing.Point(138, 134);
		this.resMin.Margin = new System.Windows.Forms.Padding(0);
		this.resMin.Name = "resMin";
		this.resMin.ResourceName = "Minerals";
		this.resMin.Size = new System.Drawing.Size(73, 20);
		this.resMin.TabIndex = 10;
		// 
		// abilityTree
		// 
		this.abilityTree.Abilities = null;
		this.abilityTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.abilityTree.AutoSize = true;
		this.abilityTree.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.abilityTree.BackColor = System.Drawing.Color.Black;
		this.abilityTree.ForeColor = System.Drawing.Color.White;
		this.abilityTree.IntrinsicAbilities = null;
		this.abilityTree.Location = new System.Drawing.Point(3, 237);
		this.abilityTree.Name = "abilityTree";
		this.abilityTree.Size = new System.Drawing.Size(353, 84);
		this.abilityTree.TabIndex = 3;
		// 
		// picPortrait
		// 
		this.picPortrait.Location = new System.Drawing.Point(3, 3);
		this.picPortrait.Name = "picPortrait";
		this.picPortrait.Size = new System.Drawing.Size(128, 128);
		this.picPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.picPortrait.TabIndex = 0;
		this.picPortrait.TabStop = false;
		this.picPortrait.Click += new System.EventHandler(this.picPortrait_Click);
		// 
		// pnlDamage
		// 
		this.pnlDamage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.pnlDamage.Controls.Add(this.damageGraph);
		this.pnlDamage.Controls.Add(this.label11);
		this.pnlDamage.Location = new System.Drawing.Point(0, 126);
		this.pnlDamage.Margin = new System.Windows.Forms.Padding(0);
		this.pnlDamage.Name = "pnlDamage";
		this.pnlDamage.Size = new System.Drawing.Size(359, 108);
		this.pnlDamage.TabIndex = 27;
		// 
		// damageGraph
		// 
		this.damageGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.damageGraph.Location = new System.Drawing.Point(125, 0);
		this.damageGraph.Name = "damageGraph";
		this.damageGraph.Size = new System.Drawing.Size(234, 105);
		this.damageGraph.TabIndex = 22;
		this.damageGraph.Text = "lineGraph1";
		this.damageGraph.Title = null;
		// 
		// label11
		// 
		this.label11.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label11.Location = new System.Drawing.Point(0, 0);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(122, 18);
		this.label11.TabIndex = 21;
		this.label11.Text = "Damage at Range";
		// 
		// ComponentReport
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.Controls.Add(this.txtVehicleTypes);
		this.Controls.Add(this.txtSupplyUsage);
		this.Controls.Add(this.txtDurability);
		this.Controls.Add(this.txtSize);
		this.Controls.Add(this.resRad);
		this.Controls.Add(this.resOrg);
		this.Controls.Add(this.resMin);
		this.Controls.Add(this.table);
		this.Controls.Add(this.label6);
		this.Controls.Add(this.label5);
		this.Controls.Add(this.label4);
		this.Controls.Add(this.label3);
		this.Controls.Add(this.label2);
		this.Controls.Add(this.txtDescription);
		this.Controls.Add(this.txtName);
		this.Controls.Add(this.picPortrait);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "ComponentReport";
		this.Size = new System.Drawing.Size(365, 554);
		this.table.ResumeLayout(false);
		this.table.PerformLayout();
		this.pnlWeapon.ResumeLayout(false);
		this.pnlSeeker.ResumeLayout(false);
		this.pnlAccuracy.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)(this.picPortrait)).EndInit();
		this.pnlDamage.ResumeLayout(false);
		this.ResumeLayout(false);

	}

	#endregion

	private GamePictureBox picPortrait;
	private System.Windows.Forms.Label txtName;
	private System.Windows.Forms.Label txtDescription;
	private System.Windows.Forms.Label label2;
	private System.Windows.Forms.Label label3;
	private System.Windows.Forms.Label label4;
	private System.Windows.Forms.Label label5;
	private System.Windows.Forms.Label label6;
	private System.Windows.Forms.TableLayoutPanel table;
	private AbilityTreeView abilityTree;
	private ResourceDisplay resMin;
	private ResourceDisplay resOrg;
	private ResourceDisplay resRad;
	private System.Windows.Forms.Label txtSize;
	private System.Windows.Forms.Label txtDurability;
	private System.Windows.Forms.Label txtSupplyUsage;
	private System.Windows.Forms.Label txtVehicleTypes;
	private System.Windows.Forms.Panel pnlSeeker;
	private System.Windows.Forms.Label txtSeekerDurability;
	private System.Windows.Forms.Label txtSeekerSpeed;
	private System.Windows.Forms.Label label12;
	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.Panel pnlWeapon;
	private System.Windows.Forms.Label txtDamageType;
	private System.Windows.Forms.Label txtReload;
	private System.Windows.Forms.Label txtTargets;
	private System.Windows.Forms.Label txtWeaponType;
	private System.Windows.Forms.Label label10;
	private System.Windows.Forms.Label label9;
	private System.Windows.Forms.Label label8;
	private System.Windows.Forms.Label label7;
	private System.Windows.Forms.Panel pnlAccuracy;
	private System.Windows.Forms.Label txtAccuracy;
	private System.Windows.Forms.Label label16;
	private System.Windows.Forms.Panel pnlDamage;
	private LineGraph damageGraph;
	private System.Windows.Forms.Label label11;
}
