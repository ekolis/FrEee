namespace FrEee.UI.WinForms.Forms;

partial class ActivateAbilityForm
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		this.btnOK = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnCancel = new FrEee.UI.WinForms.Controls.GameButton();
		this.gridAbilities = new System.Windows.Forms.DataGridView();
		this.colAbility = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colDestroyedOnUse = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		((System.ComponentModel.ISupportInitialize)(this.gridAbilities)).BeginInit();
		this.SuspendLayout();
		// 
		// btnOK
		// 
		this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.btnOK.BackColor = System.Drawing.Color.Black;
		this.btnOK.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnOK.Location = new System.Drawing.Point(610, 464);
		this.btnOK.Name = "btnOK";
		this.btnOK.Size = new System.Drawing.Size(75, 23);
		this.btnOK.TabIndex = 0;
		this.btnOK.Text = "OK";
		this.btnOK.UseVisualStyleBackColor = false;
		this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
		// 
		// btnCancel
		// 
		this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.btnCancel.BackColor = System.Drawing.Color.Black;
		this.btnCancel.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnCancel.Location = new System.Drawing.Point(610, 435);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(75, 23);
		this.btnCancel.TabIndex = 1;
		this.btnCancel.Text = "Cancel";
		this.btnCancel.UseVisualStyleBackColor = false;
		this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
		// 
		// gridAbilities
		// 
		this.gridAbilities.AllowUserToResizeRows = false;
		this.gridAbilities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.gridAbilities.BackgroundColor = System.Drawing.Color.Black;
		this.gridAbilities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gridAbilities.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAbility,
            this.colSource,
            this.colDestroyedOnUse});
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.gridAbilities.DefaultCellStyle = dataGridViewCellStyle3;
		this.gridAbilities.Location = new System.Drawing.Point(13, 13);
		this.gridAbilities.Name = "gridAbilities";
		this.gridAbilities.RowHeadersVisible = false;
		this.gridAbilities.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Black;
		this.gridAbilities.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
		this.gridAbilities.Size = new System.Drawing.Size(591, 474);
		this.gridAbilities.TabIndex = 2;
		// 
		// colAbility
		// 
		this.colAbility.DataPropertyName = "Ability";
		this.colAbility.FillWeight = 300F;
		this.colAbility.HeaderText = "Ability";
		this.colAbility.Name = "colAbility";
		this.colAbility.ReadOnly = true;
		this.colAbility.Width = 300;
		// 
		// colSource
		// 
		this.colSource.DataPropertyName = "Source";
		this.colSource.FillWeight = 150F;
		this.colSource.HeaderText = "Source";
		this.colSource.Name = "colSource";
		this.colSource.ReadOnly = true;
		this.colSource.Width = 150;
		// 
		// colDestroyedOnUse
		// 
		this.colDestroyedOnUse.DataPropertyName = "IsDestroyedOnUse";
		this.colDestroyedOnUse.HeaderText = "Destroyed On Use";
		this.colDestroyedOnUse.Name = "colDestroyedOnUse";
		this.colDestroyedOnUse.ReadOnly = true;
		// 
		// ActivateAbilityForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.ClientSize = new System.Drawing.Size(697, 499);
		this.Controls.Add(this.gridAbilities);
		this.Controls.Add(this.btnCancel);
		this.Controls.Add(this.btnOK);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "ActivateAbilityForm";
		this.Text = "Activate Ability";
		((System.ComponentModel.ISupportInitialize)(this.gridAbilities)).EndInit();
		this.ResumeLayout(false);

	}

	#endregion

	private Controls.GameButton btnOK;
	private Controls.GameButton btnCancel;
	private System.Windows.Forms.DataGridView gridAbilities;
	private System.Windows.Forms.DataGridViewTextBoxColumn colAbility;
	private System.Windows.Forms.DataGridViewTextBoxColumn colSource;
	private System.Windows.Forms.DataGridViewCheckBoxColumn colDestroyedOnUse;
}