namespace FrEee.WinForms.Controls;

partial class MountReport
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
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.txtVehicleTypes = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.label18 = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.txtSize = new System.Windows.Forms.Label();
		this.txtDurability = new System.Windows.Forms.Label();
		this.txtSupply = new System.Windows.Forms.Label();
		this.txtCost = new System.Windows.Forms.Label();
		this.label26 = new System.Windows.Forms.Label();
		this.txtDamage = new System.Windows.Forms.Label();
		this.label28 = new System.Windows.Forms.Label();
		this.txtRange = new System.Windows.Forms.Label();
		this.label30 = new System.Windows.Forms.Label();
		this.txtAccuracy = new System.Windows.Forms.Label();
		this.label32 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtVehicleSizes = new System.Windows.Forms.Label();
		this.txtDescription = new System.Windows.Forms.Label();
		this.txtWeaponTypes = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.txtComponentFamily = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.gamePanel1 = new FrEee.WinForms.Controls.GamePanel();
		this.lstAbilities = new System.Windows.Forms.ListView();
		this.colAbility = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
		this.colModifier = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
		this.picPortrait = new FrEee.WinForms.Controls.GamePictureBox();
		this.gamePanel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.picPortrait)).BeginInit();
		this.SuspendLayout();
		// 
		// txtName
		// 
		this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtName.Location = new System.Drawing.Point(140, 4);
		this.txtName.Name = "txtName";
		this.txtName.Size = new System.Drawing.Size(236, 45);
		this.txtName.TabIndex = 1;
		this.txtName.Text = "Name";
		// 
		// label6
		// 
		this.label6.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label6.Location = new System.Drawing.Point(19, 152);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(112, 18);
		this.label6.TabIndex = 7;
		this.label6.Text = "Vehicle Types";
		// 
		// label7
		// 
		this.label7.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label7.Location = new System.Drawing.Point(21, 188);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(112, 18);
		this.label7.TabIndex = 9;
		this.label7.Text = "Weapon Types";
		// 
		// txtVehicleTypes
		// 
		this.txtVehicleTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtVehicleTypes.Location = new System.Drawing.Point(141, 152);
		this.txtVehicleTypes.Name = "txtVehicleTypes";
		this.txtVehicleTypes.Size = new System.Drawing.Size(235, 18);
		this.txtVehicleTypes.TabIndex = 15;
		this.txtVehicleTypes.Text = "Vehicle Types";
		// 
		// label17
		// 
		this.label17.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label17.Location = new System.Drawing.Point(3, 134);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(128, 18);
		this.label17.TabIndex = 24;
		this.label17.Text = "Requirements";
		// 
		// label18
		// 
		this.label18.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label18.Location = new System.Drawing.Point(21, 245);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(128, 18);
		this.label18.TabIndex = 3;
		this.label18.Text = "Cost";
		// 
		// label19
		// 
		this.label19.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label19.Location = new System.Drawing.Point(21, 263);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(128, 18);
		this.label19.TabIndex = 4;
		this.label19.Text = "Size";
		// 
		// label20
		// 
		this.label20.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label20.Location = new System.Drawing.Point(21, 281);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(128, 18);
		this.label20.TabIndex = 5;
		this.label20.Text = "Durability";
		// 
		// label21
		// 
		this.label21.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label21.Location = new System.Drawing.Point(21, 352);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(128, 18);
		this.label21.TabIndex = 6;
		this.label21.Text = "Supply Usage";
		// 
		// txtSize
		// 
		this.txtSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtSize.Location = new System.Drawing.Point(141, 263);
		this.txtSize.Name = "txtSize";
		this.txtSize.Size = new System.Drawing.Size(234, 18);
		this.txtSize.TabIndex = 13;
		this.txtSize.Text = "100%";
		// 
		// txtDurability
		// 
		this.txtDurability.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtDurability.Location = new System.Drawing.Point(141, 279);
		this.txtDurability.Name = "txtDurability";
		this.txtDurability.Size = new System.Drawing.Size(234, 18);
		this.txtDurability.TabIndex = 14;
		this.txtDurability.Text = "100%";
		// 
		// txtSupply
		// 
		this.txtSupply.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtSupply.Location = new System.Drawing.Point(143, 350);
		this.txtSupply.Name = "txtSupply";
		this.txtSupply.Size = new System.Drawing.Size(234, 18);
		this.txtSupply.TabIndex = 15;
		this.txtSupply.Text = "100%";
		// 
		// txtCost
		// 
		this.txtCost.Location = new System.Drawing.Point(141, 246);
		this.txtCost.Name = "txtCost";
		this.txtCost.Size = new System.Drawing.Size(234, 17);
		this.txtCost.TabIndex = 16;
		this.txtCost.Text = "100%";
		// 
		// label26
		// 
		this.label26.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label26.Location = new System.Drawing.Point(21, 299);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(128, 18);
		this.label26.TabIndex = 17;
		this.label26.Text = "Damage";
		// 
		// txtDamage
		// 
		this.txtDamage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtDamage.Location = new System.Drawing.Point(141, 297);
		this.txtDamage.Name = "txtDamage";
		this.txtDamage.Size = new System.Drawing.Size(234, 18);
		this.txtDamage.TabIndex = 18;
		this.txtDamage.Text = "100%";
		// 
		// label28
		// 
		this.label28.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label28.Location = new System.Drawing.Point(21, 317);
		this.label28.Name = "label28";
		this.label28.Size = new System.Drawing.Size(128, 18);
		this.label28.TabIndex = 19;
		this.label28.Text = "Range Modifier";
		// 
		// txtRange
		// 
		this.txtRange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtRange.Location = new System.Drawing.Point(142, 315);
		this.txtRange.Name = "txtRange";
		this.txtRange.Size = new System.Drawing.Size(234, 18);
		this.txtRange.TabIndex = 20;
		this.txtRange.Text = "0";
		// 
		// label30
		// 
		this.label30.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label30.Location = new System.Drawing.Point(21, 334);
		this.label30.Name = "label30";
		this.label30.Size = new System.Drawing.Size(128, 18);
		this.label30.TabIndex = 21;
		this.label30.Text = "Accuracy Modifier";
		// 
		// txtAccuracy
		// 
		this.txtAccuracy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtAccuracy.Location = new System.Drawing.Point(142, 332);
		this.txtAccuracy.Name = "txtAccuracy";
		this.txtAccuracy.Size = new System.Drawing.Size(234, 18);
		this.txtAccuracy.TabIndex = 22;
		this.txtAccuracy.Text = "0%";
		// 
		// label32
		// 
		this.label32.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label32.Location = new System.Drawing.Point(3, 227);
		this.label32.Name = "label32";
		this.label32.Size = new System.Drawing.Size(128, 18);
		this.label32.TabIndex = 23;
		this.label32.Text = "Basic Modifiers";
		// 
		// label2
		// 
		this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label2.Location = new System.Drawing.Point(19, 170);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(112, 18);
		this.label2.TabIndex = 25;
		this.label2.Text = "Vehicle Sizes";
		// 
		// txtVehicleSizes
		// 
		this.txtVehicleSizes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtVehicleSizes.Location = new System.Drawing.Point(141, 170);
		this.txtVehicleSizes.Name = "txtVehicleSizes";
		this.txtVehicleSizes.Size = new System.Drawing.Size(235, 18);
		this.txtVehicleSizes.TabIndex = 26;
		this.txtVehicleSizes.Text = "Vehicle Sizes";
		// 
		// txtDescription
		// 
		this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtDescription.Location = new System.Drawing.Point(141, 49);
		this.txtDescription.Name = "txtDescription";
		this.txtDescription.Size = new System.Drawing.Size(235, 82);
		this.txtDescription.TabIndex = 27;
		this.txtDescription.Text = "Description";
		// 
		// txtWeaponTypes
		// 
		this.txtWeaponTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtWeaponTypes.Location = new System.Drawing.Point(141, 188);
		this.txtWeaponTypes.Name = "txtWeaponTypes";
		this.txtWeaponTypes.Size = new System.Drawing.Size(235, 18);
		this.txtWeaponTypes.TabIndex = 28;
		this.txtWeaponTypes.Text = "Weapon Types";
		// 
		// label1
		// 
		this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label1.Location = new System.Drawing.Point(21, 206);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(112, 18);
		this.label1.TabIndex = 29;
		this.label1.Text = "Component Family";
		// 
		// txtComponentFamily
		// 
		this.txtComponentFamily.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtComponentFamily.Location = new System.Drawing.Point(141, 206);
		this.txtComponentFamily.Name = "txtComponentFamily";
		this.txtComponentFamily.Size = new System.Drawing.Size(235, 18);
		this.txtComponentFamily.TabIndex = 30;
		this.txtComponentFamily.Text = "Component Family";
		// 
		// label4
		// 
		this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label4.Location = new System.Drawing.Point(5, 370);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(128, 18);
		this.label4.TabIndex = 31;
		this.label4.Text = "Ability Modifiers";
		// 
		// gamePanel1
		// 
		this.gamePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.gamePanel1.BackColor = System.Drawing.Color.Black;
		this.gamePanel1.BorderColor = System.Drawing.Color.CornflowerBlue;
		this.gamePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.gamePanel1.Controls.Add(this.lstAbilities);
		this.gamePanel1.ForeColor = System.Drawing.Color.White;
		this.gamePanel1.Location = new System.Drawing.Point(24, 391);
		this.gamePanel1.Name = "gamePanel1";
		this.gamePanel1.Padding = new System.Windows.Forms.Padding(3);
		this.gamePanel1.Size = new System.Drawing.Size(348, 160);
		this.gamePanel1.TabIndex = 33;
		// 
		// lstAbilities
		// 
		this.lstAbilities.BackColor = System.Drawing.Color.Black;
		this.lstAbilities.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.lstAbilities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colAbility,
            this.colModifier});
		this.lstAbilities.Dock = System.Windows.Forms.DockStyle.Fill;
		this.lstAbilities.ForeColor = System.Drawing.Color.White;
		this.lstAbilities.Location = new System.Drawing.Point(3, 3);
		this.lstAbilities.Name = "lstAbilities";
		this.lstAbilities.Size = new System.Drawing.Size(340, 152);
		this.lstAbilities.TabIndex = 33;
		this.lstAbilities.UseCompatibleStateImageBehavior = false;
		this.lstAbilities.View = System.Windows.Forms.View.Details;
		// 
		// colAbility
		// 
		this.colAbility.Text = "Ability";
		this.colAbility.Width = 120;
		// 
		// colModifier
		// 
		this.colModifier.Text = "Modifier";
		this.colModifier.Width = 120;
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
		// MountReport
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.Controls.Add(this.gamePanel1);
		this.Controls.Add(this.label4);
		this.Controls.Add(this.txtComponentFamily);
		this.Controls.Add(this.label1);
		this.Controls.Add(this.txtWeaponTypes);
		this.Controls.Add(this.txtDescription);
		this.Controls.Add(this.txtVehicleSizes);
		this.Controls.Add(this.label2);
		this.Controls.Add(this.label17);
		this.Controls.Add(this.label32);
		this.Controls.Add(this.txtAccuracy);
		this.Controls.Add(this.label30);
		this.Controls.Add(this.label7);
		this.Controls.Add(this.txtRange);
		this.Controls.Add(this.label28);
		this.Controls.Add(this.txtDamage);
		this.Controls.Add(this.label26);
		this.Controls.Add(this.txtCost);
		this.Controls.Add(this.txtSupply);
		this.Controls.Add(this.txtVehicleTypes);
		this.Controls.Add(this.txtDurability);
		this.Controls.Add(this.txtSize);
		this.Controls.Add(this.label21);
		this.Controls.Add(this.label6);
		this.Controls.Add(this.label20);
		this.Controls.Add(this.label19);
		this.Controls.Add(this.label18);
		this.Controls.Add(this.txtName);
		this.Controls.Add(this.picPortrait);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "MountReport";
		this.Size = new System.Drawing.Size(379, 554);
		this.gamePanel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)(this.picPortrait)).EndInit();
		this.ResumeLayout(false);

	}

	#endregion

	private GamePictureBox picPortrait;
	private System.Windows.Forms.Label txtName;
	private System.Windows.Forms.Label label6;
	private System.Windows.Forms.Label txtVehicleTypes;
	private System.Windows.Forms.Label label7;
	private System.Windows.Forms.Label label17;
	private System.Windows.Forms.Label label18;
	private System.Windows.Forms.Label label19;
	private System.Windows.Forms.Label label20;
	private System.Windows.Forms.Label label21;
	private System.Windows.Forms.Label txtSize;
	private System.Windows.Forms.Label txtDurability;
	private System.Windows.Forms.Label txtSupply;
	private System.Windows.Forms.Label txtCost;
	private System.Windows.Forms.Label label26;
	private System.Windows.Forms.Label txtDamage;
	private System.Windows.Forms.Label label28;
	private System.Windows.Forms.Label txtRange;
	private System.Windows.Forms.Label label30;
	private System.Windows.Forms.Label txtAccuracy;
	private System.Windows.Forms.Label label32;
	private System.Windows.Forms.Label label2;
	private System.Windows.Forms.Label txtVehicleSizes;
	private System.Windows.Forms.Label txtDescription;
	private System.Windows.Forms.Label txtWeaponTypes;
	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.Label txtComponentFamily;
	private System.Windows.Forms.Label label4;
	private GamePanel gamePanel1;
	private System.Windows.Forms.ListView lstAbilities;
	private System.Windows.Forms.ColumnHeader colAbility;
	private System.Windows.Forms.ColumnHeader colModifier;
}
