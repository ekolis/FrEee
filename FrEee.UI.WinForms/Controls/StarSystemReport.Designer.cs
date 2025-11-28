using FrEee.UI.WinForms.Controls.Blazor;
using FrEee.UI.WinForms.Controls;

namespace FrEee.UI.WinForms.Controls;

partial class StarSystemReport
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
		this.abilityTreeView = new FrEee.UI.WinForms.Controls.AbilityTreeView();
		this.picPortrait = new FrEee.UI.WinForms.Controls.GamePictureBox();
		this.txtDescription = new System.Windows.Forms.Label();
		this.txtName = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.txtOurFacilities = new System.Windows.Forms.Label();
		this.txtOurVehicles = new System.Windows.Forms.Label();
		this.txtAllyFacilities = new System.Windows.Forms.Label();
		this.txtAllyVehicles = new System.Windows.Forms.Label();
		this.txtEnemyFacilities = new System.Windows.Forms.Label();
		this.txtEnemyVehicles = new System.Windows.Forms.Label();
		this.txtUncolonizedPlanets = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.txtBreathableUs = new System.Windows.Forms.Label();
		this.txtBreathableOthers = new System.Windows.Forms.Label();
		this.txtNonbreathable = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.txtNeutralFacilities = new System.Windows.Forms.Label();
		this.txtNeutralVehicles = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)(this.picPortrait)).BeginInit();
		this.SuspendLayout();
		// 
		// abilityTreeView
		// 
		this.abilityTreeView.Abilities = null;
		this.abilityTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.abilityTreeView.BackColor = System.Drawing.Color.Black;
		this.abilityTreeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.abilityTreeView.ForeColor = System.Drawing.Color.White;
		this.abilityTreeView.IntrinsicAbilities = null;
		this.abilityTreeView.Location = new System.Drawing.Point(3, 199);
		this.abilityTreeView.Name = "abilityTreeView";
		this.abilityTreeView.Size = new System.Drawing.Size(314, 257);
		this.abilityTreeView.TabIndex = 59;
		// 
		// picPortrait
		// 
		this.picPortrait.Location = new System.Drawing.Point(3, 29);
		this.picPortrait.Name = "picPortrait";
		this.picPortrait.Size = new System.Drawing.Size(128, 128);
		this.picPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.picPortrait.TabIndex = 58;
		this.picPortrait.TabStop = false;
		// 
		// txtDescription
		// 
		this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtDescription.Location = new System.Drawing.Point(7, 160);
		this.txtDescription.Name = "txtDescription";
		this.txtDescription.Size = new System.Drawing.Size(296, 36);
		this.txtDescription.TabIndex = 57;
		this.txtDescription.Text = "A standard star system.";
		// 
		// txtName
		// 
		this.txtName.AutoSize = true;
		this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtName.Location = new System.Drawing.Point(140, 3);
		this.txtName.Name = "txtName";
		this.txtName.Size = new System.Drawing.Size(59, 20);
		this.txtName.TabIndex = 55;
		this.txtName.Text = "Tudran";
		// 
		// label1
		// 
		this.label1.AutoSize = true;
		this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label1.Location = new System.Drawing.Point(141, 48);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(20, 13);
		this.label1.TabIndex = 60;
		this.label1.Text = "Us";
		// 
		// label2
		// 
		this.label2.AutoSize = true;
		this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label2.Location = new System.Drawing.Point(141, 61);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(31, 13);
		this.label2.TabIndex = 61;
		this.label2.Text = "Allies";
		// 
		// label3
		// 
		this.label3.AutoSize = true;
		this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label3.Location = new System.Drawing.Point(141, 87);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(47, 13);
		this.label3.TabIndex = 62;
		this.label3.Text = "Enemies";
		// 
		// label4
		// 
		this.label4.AutoSize = true;
		this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label4.Location = new System.Drawing.Point(198, 29);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(47, 13);
		this.label4.TabIndex = 63;
		this.label4.Text = "Facilities";
		// 
		// label5
		// 
		this.label5.AutoSize = true;
		this.label5.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label5.Location = new System.Drawing.Point(141, 108);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(104, 13);
		this.label5.TabIndex = 64;
		this.label5.Text = "Uncolonized Planets";
		// 
		// label6
		// 
		this.label6.AutoSize = true;
		this.label6.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label6.Location = new System.Drawing.Point(264, 29);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(47, 13);
		this.label6.TabIndex = 65;
		this.label6.Text = "Vehicles";
		// 
		// txtOurFacilities
		// 
		this.txtOurFacilities.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
		this.txtOurFacilities.Location = new System.Drawing.Point(201, 48);
		this.txtOurFacilities.Name = "txtOurFacilities";
		this.txtOurFacilities.Size = new System.Drawing.Size(41, 13);
		this.txtOurFacilities.TabIndex = 66;
		this.txtOurFacilities.Text = "0";
		this.txtOurFacilities.TextAlign = System.Drawing.ContentAlignment.TopRight;
		// 
		// txtOurVehicles
		// 
		this.txtOurVehicles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
		this.txtOurVehicles.Location = new System.Drawing.Point(261, 48);
		this.txtOurVehicles.Name = "txtOurVehicles";
		this.txtOurVehicles.Size = new System.Drawing.Size(50, 13);
		this.txtOurVehicles.TabIndex = 67;
		this.txtOurVehicles.Text = "0";
		this.txtOurVehicles.TextAlign = System.Drawing.ContentAlignment.TopRight;
		// 
		// txtAllyFacilities
		// 
		this.txtAllyFacilities.Location = new System.Drawing.Point(201, 61);
		this.txtAllyFacilities.Name = "txtAllyFacilities";
		this.txtAllyFacilities.Size = new System.Drawing.Size(41, 13);
		this.txtAllyFacilities.TabIndex = 68;
		this.txtAllyFacilities.Text = "0";
		this.txtAllyFacilities.TextAlign = System.Drawing.ContentAlignment.TopRight;
		// 
		// txtAllyVehicles
		// 
		this.txtAllyVehicles.Location = new System.Drawing.Point(261, 61);
		this.txtAllyVehicles.Name = "txtAllyVehicles";
		this.txtAllyVehicles.Size = new System.Drawing.Size(50, 13);
		this.txtAllyVehicles.TabIndex = 69;
		this.txtAllyVehicles.Text = "0";
		this.txtAllyVehicles.TextAlign = System.Drawing.ContentAlignment.TopRight;
		// 
		// txtEnemyFacilities
		// 
		this.txtEnemyFacilities.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
		this.txtEnemyFacilities.Location = new System.Drawing.Point(201, 87);
		this.txtEnemyFacilities.Name = "txtEnemyFacilities";
		this.txtEnemyFacilities.Size = new System.Drawing.Size(41, 13);
		this.txtEnemyFacilities.TabIndex = 70;
		this.txtEnemyFacilities.Text = "0";
		this.txtEnemyFacilities.TextAlign = System.Drawing.ContentAlignment.TopRight;
		// 
		// txtEnemyVehicles
		// 
		this.txtEnemyVehicles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
		this.txtEnemyVehicles.Location = new System.Drawing.Point(261, 87);
		this.txtEnemyVehicles.Name = "txtEnemyVehicles";
		this.txtEnemyVehicles.Size = new System.Drawing.Size(50, 13);
		this.txtEnemyVehicles.TabIndex = 71;
		this.txtEnemyVehicles.Text = "0";
		this.txtEnemyVehicles.TextAlign = System.Drawing.ContentAlignment.TopRight;
		// 
		// txtUncolonizedPlanets
		// 
		this.txtUncolonizedPlanets.Location = new System.Drawing.Point(270, 108);
		this.txtUncolonizedPlanets.Name = "txtUncolonizedPlanets";
		this.txtUncolonizedPlanets.Size = new System.Drawing.Size(41, 13);
		this.txtUncolonizedPlanets.TabIndex = 72;
		this.txtUncolonizedPlanets.Text = "0";
		this.txtUncolonizedPlanets.TextAlign = System.Drawing.ContentAlignment.TopRight;
		// 
		// label8
		// 
		this.label8.AutoSize = true;
		this.label8.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label8.Location = new System.Drawing.Point(141, 121);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(88, 13);
		this.label8.TabIndex = 74;
		this.label8.Text = "Breathable by Us";
		// 
		// label9
		// 
		this.label9.AutoSize = true;
		this.label9.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label9.Location = new System.Drawing.Point(141, 134);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(106, 13);
		this.label9.TabIndex = 75;
		this.label9.Text = "Breathable by Others";
		// 
		// txtBreathableUs
		// 
		this.txtBreathableUs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
		this.txtBreathableUs.Location = new System.Drawing.Point(270, 121);
		this.txtBreathableUs.Name = "txtBreathableUs";
		this.txtBreathableUs.Size = new System.Drawing.Size(41, 13);
		this.txtBreathableUs.TabIndex = 77;
		this.txtBreathableUs.Text = "0";
		this.txtBreathableUs.TextAlign = System.Drawing.ContentAlignment.TopRight;
		// 
		// txtBreathableOthers
		// 
		this.txtBreathableOthers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
		this.txtBreathableOthers.Location = new System.Drawing.Point(270, 134);
		this.txtBreathableOthers.Name = "txtBreathableOthers";
		this.txtBreathableOthers.Size = new System.Drawing.Size(41, 13);
		this.txtBreathableOthers.TabIndex = 78;
		this.txtBreathableOthers.Text = "0";
		this.txtBreathableOthers.TextAlign = System.Drawing.ContentAlignment.TopRight;
		// 
		// txtNonbreathable
		// 
		this.txtNonbreathable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
		this.txtNonbreathable.Location = new System.Drawing.Point(270, 147);
		this.txtNonbreathable.Name = "txtNonbreathable";
		this.txtNonbreathable.Size = new System.Drawing.Size(41, 13);
		this.txtNonbreathable.TabIndex = 80;
		this.txtNonbreathable.Text = "0";
		this.txtNonbreathable.TextAlign = System.Drawing.ContentAlignment.TopRight;
		// 
		// label7
		// 
		this.label7.AutoSize = true;
		this.label7.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label7.Location = new System.Drawing.Point(141, 147);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(77, 13);
		this.label7.TabIndex = 79;
		this.label7.Text = "Nonbreathable";
		// 
		// label10
		// 
		this.label10.AutoSize = true;
		this.label10.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label10.Location = new System.Drawing.Point(141, 74);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(41, 13);
		this.label10.TabIndex = 81;
		this.label10.Text = "Neutral";
		// 
		// txtNeutralFacilities
		// 
		this.txtNeutralFacilities.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
		this.txtNeutralFacilities.Location = new System.Drawing.Point(201, 74);
		this.txtNeutralFacilities.Name = "txtNeutralFacilities";
		this.txtNeutralFacilities.Size = new System.Drawing.Size(41, 13);
		this.txtNeutralFacilities.TabIndex = 82;
		this.txtNeutralFacilities.Text = "0";
		this.txtNeutralFacilities.TextAlign = System.Drawing.ContentAlignment.TopRight;
		// 
		// txtNeutralVehicles
		// 
		this.txtNeutralVehicles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
		this.txtNeutralVehicles.Location = new System.Drawing.Point(261, 74);
		this.txtNeutralVehicles.Name = "txtNeutralVehicles";
		this.txtNeutralVehicles.Size = new System.Drawing.Size(50, 13);
		this.txtNeutralVehicles.TabIndex = 83;
		this.txtNeutralVehicles.Text = "0";
		this.txtNeutralVehicles.TextAlign = System.Drawing.ContentAlignment.TopRight;
		// 
		// StarSystemReport
		// 
		this.AutoScroll = true;
		this.BackColor = System.Drawing.Color.Black;
		this.Controls.Add(this.txtNeutralVehicles);
		this.Controls.Add(this.txtNeutralFacilities);
		this.Controls.Add(this.label10);
		this.Controls.Add(this.txtNonbreathable);
		this.Controls.Add(this.label7);
		this.Controls.Add(this.txtBreathableOthers);
		this.Controls.Add(this.txtBreathableUs);
		this.Controls.Add(this.label9);
		this.Controls.Add(this.label8);
		this.Controls.Add(this.txtUncolonizedPlanets);
		this.Controls.Add(this.txtEnemyVehicles);
		this.Controls.Add(this.txtEnemyFacilities);
		this.Controls.Add(this.txtAllyVehicles);
		this.Controls.Add(this.txtAllyFacilities);
		this.Controls.Add(this.txtOurVehicles);
		this.Controls.Add(this.txtOurFacilities);
		this.Controls.Add(this.label6);
		this.Controls.Add(this.label5);
		this.Controls.Add(this.label4);
		this.Controls.Add(this.label3);
		this.Controls.Add(this.label2);
		this.Controls.Add(this.label1);
		this.Controls.Add(this.abilityTreeView);
		this.Controls.Add(this.picPortrait);
		this.Controls.Add(this.txtDescription);
		this.Controls.Add(this.txtName);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "StarSystemReport";
		this.Size = new System.Drawing.Size(320, 459);
		((System.ComponentModel.ISupportInitialize)(this.picPortrait)).EndInit();
		this.ResumeLayout(false);
		this.PerformLayout();

	}

	#endregion

	private AbilityTreeView abilityTreeView;
	private GamePictureBox picPortrait;
	private System.Windows.Forms.Label txtDescription;
	private System.Windows.Forms.Label txtName;
	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.Label label2;
	private System.Windows.Forms.Label label3;
	private System.Windows.Forms.Label label4;
	private System.Windows.Forms.Label label5;
	private System.Windows.Forms.Label label6;
	private System.Windows.Forms.Label txtOurFacilities;
	private System.Windows.Forms.Label txtOurVehicles;
	private System.Windows.Forms.Label txtAllyFacilities;
	private System.Windows.Forms.Label txtAllyVehicles;
	private System.Windows.Forms.Label txtEnemyFacilities;
	private System.Windows.Forms.Label txtEnemyVehicles;
	private System.Windows.Forms.Label txtUncolonizedPlanets;
	private System.Windows.Forms.Label label8;
	private System.Windows.Forms.Label label9;
	private System.Windows.Forms.Label txtBreathableUs;
	private System.Windows.Forms.Label txtBreathableOthers;
	private System.Windows.Forms.Label txtNonbreathable;
	private System.Windows.Forms.Label label7;
	private System.Windows.Forms.Label label10;
	private System.Windows.Forms.Label txtNeutralFacilities;
	private System.Windows.Forms.Label txtNeutralVehicles;

}
