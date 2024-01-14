namespace FrEee.WinForms.Forms;

partial class EditorForm
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
		this.tree = new System.Windows.Forms.TreeView();
		this.btnCancel = new FrEee.WinForms.Controls.GameButton();
		this.btnSave = new FrEee.WinForms.Controls.GameButton();
		this.propertyGrid = new FrEee.WinForms.GamePropertyGrid();
		this.SuspendLayout();
		// 
		// tree
		// 
		this.tree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
		this.tree.BackColor = System.Drawing.Color.Black;
		this.tree.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.tree.ForeColor = System.Drawing.Color.White;
		this.tree.Location = new System.Drawing.Point(0, 0);
		this.tree.Name = "tree";
		this.tree.Size = new System.Drawing.Size(318, 577);
		this.tree.TabIndex = 3;
		this.tree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tree_NodeMouseClick);
		// 
		// btnCancel
		// 
		this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.btnCancel.BackColor = System.Drawing.Color.Black;
		this.btnCancel.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnCancel.Location = new System.Drawing.Point(640, 550);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(75, 23);
		this.btnCancel.TabIndex = 6;
		this.btnCancel.Text = "Cancel";
		this.btnCancel.UseVisualStyleBackColor = false;
		this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
		// 
		// btnSave
		// 
		this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.btnSave.BackColor = System.Drawing.Color.Black;
		this.btnSave.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnSave.Location = new System.Drawing.Point(721, 550);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(75, 23);
		this.btnSave.TabIndex = 5;
		this.btnSave.Text = "Save";
		this.btnSave.UseVisualStyleBackColor = false;
		this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
		// 
		// propertyGrid
		// 
		this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.propertyGrid.Location = new System.Drawing.Point(324, 0);
		this.propertyGrid.Name = "propertyGrid";
		this.propertyGrid.Size = new System.Drawing.Size(484, 544);
		this.propertyGrid.TabIndex = 4;
		// 
		// EditorForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.ClientSize = new System.Drawing.Size(808, 577);
		this.Controls.Add(this.btnCancel);
		this.Controls.Add(this.btnSave);
		this.Controls.Add(this.propertyGrid);
		this.Controls.Add(this.tree);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "EditorForm";
		this.Text = "Editor";
		this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.TreeView tree;
	private GamePropertyGrid propertyGrid;
	private Controls.GameButton btnSave;
	private Controls.GameButton btnCancel;

}