namespace FrEee.WinForms.Controls;

partial class Pager
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
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.lblPager = new System.Windows.Forms.Label();
		this.pnlContent = new System.Windows.Forms.Panel();
		this.btnNext = new GameButton();
		this.btnPrev = new GameButton();
		this.tableLayoutPanel1.SuspendLayout();
		this.SuspendLayout();
		// 
		// tableLayoutPanel1
		// 
		this.tableLayoutPanel1.ColumnCount = 3;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
		this.tableLayoutPanel1.Controls.Add(this.btnNext, 2, 1);
		this.tableLayoutPanel1.Controls.Add(this.lblPager, 1, 0);
		this.tableLayoutPanel1.Controls.Add(this.pnlContent, 1, 1);
		this.tableLayoutPanel1.Controls.Add(this.btnPrev, 0, 1);
		this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.RowCount = 2;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
		this.tableLayoutPanel1.Size = new System.Drawing.Size(327, 83);
		this.tableLayoutPanel1.TabIndex = 6;
		// 
		// lblPager
		// 
		this.lblPager.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.lblPager.Location = new System.Drawing.Point(43, 0);
		this.lblPager.Name = "lblPager";
		this.lblPager.Size = new System.Drawing.Size(241, 15);
		this.lblPager.TabIndex = 5;
		this.lblPager.Text = "0 of 0";
		this.lblPager.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		// 
		// pnlContent
		// 
		this.pnlContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.pnlContent.Location = new System.Drawing.Point(40, 20);
		this.pnlContent.Margin = new System.Windows.Forms.Padding(0);
		this.pnlContent.Name = "pnlContent";
		this.pnlContent.Size = new System.Drawing.Size(247, 63);
		this.pnlContent.TabIndex = 6;
		// 
		// btnNext
		// 
		this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.btnNext.BackColor = System.Drawing.Color.Black;
		this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.btnNext.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnNext.Location = new System.Drawing.Point(290, 23);
		this.btnNext.Name = "btnNext";
		this.btnNext.Size = new System.Drawing.Size(34, 57);
		this.btnNext.TabIndex = 1;
		this.btnNext.Text = ">";
		this.btnNext.UseVisualStyleBackColor = false;
		this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
		// 
		// btnPrev
		// 
		this.btnPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.btnPrev.BackColor = System.Drawing.Color.Black;
		this.btnPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.btnPrev.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnPrev.Location = new System.Drawing.Point(3, 23);
		this.btnPrev.Name = "btnPrev";
		this.btnPrev.Size = new System.Drawing.Size(34, 57);
		this.btnPrev.TabIndex = 7;
		this.btnPrev.Text = "<";
		this.btnPrev.UseVisualStyleBackColor = false;
		this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
		// 
		// GamePager
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.Controls.Add(this.tableLayoutPanel1);
		this.ForeColor = System.Drawing.Color.White;
		this.Margin = new System.Windows.Forms.Padding(0);
		this.Name = "GamePager";
		this.Size = new System.Drawing.Size(327, 83);
		this.tableLayoutPanel1.ResumeLayout(false);
		this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private System.Windows.Forms.Label lblPager;
	private GameButton btnNext;
	private System.Windows.Forms.Panel pnlContent;
	private GameButton btnPrev;


}
