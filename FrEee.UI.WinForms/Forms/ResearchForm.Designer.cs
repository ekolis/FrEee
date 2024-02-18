namespace FrEee.WinForms.Forms;

partial class ResearchForm
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		this.gridTechs = new System.Windows.Forms.DataGridView();
		this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colNextLevelCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colProgress = new FrEee.WinForms.DataGridView.DataGridViewProgressColumn();
		this.colSpending = new FrEee.WinForms.DataGridView.DataGridViewProgressColumn();
		this.technologyBindingSource = new System.Windows.Forms.BindingSource(this.components);
		this.ddlGroup = new System.Windows.Forms.ComboBox();
		this.lblPoints = new System.Windows.Forms.Label();
		this.txtTechName = new System.Windows.Forms.Label();
		this.lblSpending = new System.Windows.Forms.Label();
		this.sldSpending = new System.Windows.Forms.TrackBar();
		this.lblResults = new System.Windows.Forms.Label();
		this.btnDown = new FrEee.WinForms.Controls.GameButton();
		this.btnBottom = new FrEee.WinForms.Controls.GameButton();
		this.btnUp = new FrEee.WinForms.Controls.GameButton();
		this.btnTop = new FrEee.WinForms.Controls.GameButton();
		this.gamePanel2 = new FrEee.WinForms.Controls.GamePanel();
		this.lstQueue = new System.Windows.Forms.ListBox();
		this.btnAddToQueue = new FrEee.WinForms.Controls.GameButton();
		this.gamePanel1 = new FrEee.WinForms.Controls.GamePanel();
		this.lstResults = new System.Windows.Forms.ListView();
		this.dataGridViewProgressColumn1 = new FrEee.WinForms.DataGridView.DataGridViewProgressColumn();
		this.btnCancel = new FrEee.WinForms.Controls.GameButton();
		this.resRes = new FrEee.WinForms.Controls.ResourceDisplay();
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.btnClear = new FrEee.WinForms.Controls.GameButton();
		this.btnDelete = new FrEee.WinForms.Controls.GameButton();
		this.txtTechDiscription = new System.Windows.Forms.Label();
		this.btnTree = new FrEee.WinForms.Controls.GameButton();
		this.btnSave = new FrEee.WinForms.Controls.GameButton();
		((System.ComponentModel.ISupportInitialize)(this.gridTechs)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.technologyBindingSource)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.sldSpending)).BeginInit();
		this.gamePanel2.SuspendLayout();
		this.gamePanel1.SuspendLayout();
		this.tableLayoutPanel1.SuspendLayout();
		this.SuspendLayout();
		// 
		// gridTechs
		// 
		this.gridTechs.AllowUserToAddRows = false;
		this.gridTechs.AllowUserToDeleteRows = false;
		this.gridTechs.AllowUserToOrderColumns = true;
		this.gridTechs.AllowUserToResizeRows = false;
		this.gridTechs.AutoGenerateColumns = false;
		this.gridTechs.BackgroundColor = System.Drawing.Color.Black;
		this.gridTechs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gridTechs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colLevel,
            this.colNextLevelCost,
            this.colProgress,
            this.colSpending});
		this.tableLayoutPanel1.SetColumnSpan(this.gridTechs, 3);
		this.gridTechs.DataSource = this.technologyBindingSource;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.gridTechs.DefaultCellStyle = dataGridViewCellStyle1;
		this.gridTechs.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridTechs.Location = new System.Drawing.Point(3, 31);
		this.gridTechs.Name = "gridTechs";
		this.gridTechs.ReadOnly = true;
		this.gridTechs.RowHeadersVisible = false;
		this.tableLayoutPanel1.SetRowSpan(this.gridTechs, 10);
		this.gridTechs.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Black;
		this.gridTechs.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
		this.gridTechs.RowTemplate.Height = 32;
		this.gridTechs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gridTechs.Size = new System.Drawing.Size(494, 539);
		this.gridTechs.TabIndex = 1;
		this.gridTechs.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridQueues_CellMouseDoubleClick);
		this.gridTechs.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridTechs_ColumnHeaderMouseClick);
		this.gridTechs.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridQueues_RowEnter);
		// 
		// colName
		// 
		this.colName.DataPropertyName = "Name";
		this.colName.HeaderText = "Name";
		this.colName.Name = "colName";
		this.colName.ReadOnly = true;
		this.colName.Width = 150;
		// 
		// colLevel
		// 
		this.colLevel.DataPropertyName = "CurrentLevel";
		this.colLevel.HeaderText = "Level";
		this.colLevel.Name = "colLevel";
		this.colLevel.ReadOnly = true;
		this.colLevel.Width = 50;
		// 
		// colNextLevelCost
		// 
		this.colNextLevelCost.DataPropertyName = "NextLevelCost";
		this.colNextLevelCost.HeaderText = "Cost";
		this.colNextLevelCost.Name = "colNextLevelCost";
		this.colNextLevelCost.ReadOnly = true;
		this.colNextLevelCost.Width = 75;
		// 
		// colProgress
		// 
		this.colProgress.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
		this.colProgress.DataPropertyName = "Progress";
		this.colProgress.HeaderText = "Progress";
		this.colProgress.MinimumWidth = 100;
		this.colProgress.Name = "colProgress";
		this.colProgress.ProgressDisplayMode = FrEee.WinForms.DataGridView.ProgressDisplayMode.Eta;
		this.colProgress.ReadOnly = true;
		this.colProgress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		// 
		// colSpending
		// 
		this.colSpending.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
		this.colSpending.DataPropertyName = "Spending";
		this.colSpending.HeaderText = "Spending";
		this.colSpending.MinimumWidth = 100;
		this.colSpending.Name = "colSpending";
		this.colSpending.ProgressDisplayMode = FrEee.WinForms.DataGridView.ProgressDisplayMode.Percentage;
		this.colSpending.ReadOnly = true;
		this.colSpending.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		// 
		// technologyBindingSource
		// 
		this.technologyBindingSource.AllowNew = false;
		this.technologyBindingSource.DataSource = typeof(FrEee.Objects.Technology.Technology);
		// 
		// ddlGroup
		// 
		this.ddlGroup.DisplayMember = "Text";
		this.ddlGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.ddlGroup.FormattingEnabled = true;
		this.ddlGroup.Location = new System.Drawing.Point(353, 3);
		this.ddlGroup.Name = "ddlGroup";
		this.ddlGroup.Size = new System.Drawing.Size(132, 21);
		this.ddlGroup.TabIndex = 2;
		this.ddlGroup.ValueMember = "GroupName";
		this.ddlGroup.SelectedIndexChanged += new System.EventHandler(this.ddlGroup_SelectedIndexChanged);
		// 
		// lblPoints
		// 
		this.lblPoints.AutoSize = true;
		this.lblPoints.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblPoints.Location = new System.Drawing.Point(3, 0);
		this.lblPoints.Name = "lblPoints";
		this.lblPoints.Size = new System.Drawing.Size(134, 13);
		this.lblPoints.TabIndex = 3;
		this.lblPoints.Text = "Research Points Available:";
		// 
		// txtTechName
		// 
		this.txtTechName.AutoSize = true;
		this.tableLayoutPanel1.SetColumnSpan(this.txtTechName, 4);
		this.txtTechName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtTechName.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.txtTechName.Location = new System.Drawing.Point(503, 0);
		this.txtTechName.Name = "txtTechName";
		this.txtTechName.Size = new System.Drawing.Size(124, 20);
		this.txtTechName.TabIndex = 5;
		this.txtTechName.Text = "(No Technology)";
		// 
		// lblSpending
		// 
		this.lblSpending.AutoSize = true;
		this.tableLayoutPanel1.SetColumnSpan(this.lblSpending, 4);
		this.lblSpending.Location = new System.Drawing.Point(503, 90);
		this.lblSpending.Name = "lblSpending";
		this.lblSpending.Size = new System.Drawing.Size(52, 13);
		this.lblSpending.TabIndex = 6;
		this.lblSpending.Text = "Spending";
		// 
		// sldSpending
		// 
		this.sldSpending.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.tableLayoutPanel1.SetColumnSpan(this.sldSpending, 4);
		this.sldSpending.LargeChange = 10;
		this.sldSpending.Location = new System.Drawing.Point(503, 111);
		this.sldSpending.Maximum = 100;
		this.sldSpending.Name = "sldSpending";
		this.sldSpending.Size = new System.Drawing.Size(261, 22);
		this.sldSpending.TabIndex = 7;
		this.sldSpending.TickFrequency = 10;
		this.sldSpending.Scroll += new System.EventHandler(this.sldSpending_Scroll);
		// 
		// lblResults
		// 
		this.lblResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.lblResults.AutoSize = true;
		this.tableLayoutPanel1.SetColumnSpan(this.lblResults, 4);
		this.lblResults.Location = new System.Drawing.Point(503, 136);
		this.lblResults.Name = "lblResults";
		this.lblResults.Size = new System.Drawing.Size(261, 18);
		this.lblResults.TabIndex = 8;
		this.lblResults.Text = "Expected Results";
		// 
		// btnDown
		// 
		this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.btnDown.BackColor = System.Drawing.Color.Black;
		this.tableLayoutPanel1.SetColumnSpan(this.btnDown, 2);
		this.btnDown.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnDown.Location = new System.Drawing.Point(587, 497);
		this.btnDown.Name = "btnDown";
		this.btnDown.Size = new System.Drawing.Size(78, 28);
		this.btnDown.TabIndex = 22;
		this.btnDown.Text = "Down";
		this.btnDown.UseVisualStyleBackColor = false;
		this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
		// 
		// btnBottom
		// 
		this.btnBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.btnBottom.BackColor = System.Drawing.Color.Black;
		this.btnBottom.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnBottom.Location = new System.Drawing.Point(503, 497);
		this.btnBottom.Name = "btnBottom";
		this.btnBottom.Size = new System.Drawing.Size(78, 28);
		this.btnBottom.TabIndex = 21;
		this.btnBottom.Text = "Bottom";
		this.btnBottom.UseVisualStyleBackColor = false;
		this.btnBottom.Click += new System.EventHandler(this.btnBottom_Click);
		// 
		// btnUp
		// 
		this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.btnUp.BackColor = System.Drawing.Color.Black;
		this.tableLayoutPanel1.SetColumnSpan(this.btnUp, 2);
		this.btnUp.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnUp.Location = new System.Drawing.Point(587, 463);
		this.btnUp.Name = "btnUp";
		this.btnUp.Size = new System.Drawing.Size(78, 28);
		this.btnUp.TabIndex = 19;
		this.btnUp.Text = "Up";
		this.btnUp.UseVisualStyleBackColor = false;
		this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
		// 
		// btnTop
		// 
		this.btnTop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.btnTop.BackColor = System.Drawing.Color.Black;
		this.btnTop.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnTop.Location = new System.Drawing.Point(503, 463);
		this.btnTop.Name = "btnTop";
		this.btnTop.Size = new System.Drawing.Size(78, 28);
		this.btnTop.TabIndex = 18;
		this.btnTop.Text = "Top";
		this.btnTop.UseVisualStyleBackColor = false;
		this.btnTop.Click += new System.EventHandler(this.btnTop_Click);
		// 
		// gamePanel2
		// 
		this.gamePanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.gamePanel2.BackColor = System.Drawing.Color.Black;
		this.gamePanel2.BorderColor = System.Drawing.Color.CornflowerBlue;
		this.gamePanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tableLayoutPanel1.SetColumnSpan(this.gamePanel2, 4);
		this.gamePanel2.Controls.Add(this.lstQueue);
		this.gamePanel2.ForeColor = System.Drawing.Color.White;
		this.gamePanel2.Location = new System.Drawing.Point(503, 329);
		this.gamePanel2.Name = "gamePanel2";
		this.gamePanel2.Padding = new System.Windows.Forms.Padding(3);
		this.gamePanel2.Size = new System.Drawing.Size(261, 128);
		this.gamePanel2.TabIndex = 12;
		// 
		// lstQueue
		// 
		this.lstQueue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.lstQueue.BackColor = System.Drawing.Color.Black;
		this.lstQueue.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.lstQueue.ForeColor = System.Drawing.Color.White;
		this.lstQueue.FormattingEnabled = true;
		this.lstQueue.Location = new System.Drawing.Point(3, 5);
		this.lstQueue.Name = "lstQueue";
		this.lstQueue.Size = new System.Drawing.Size(253, 117);
		this.lstQueue.TabIndex = 0;
		this.lstQueue.DoubleClick += new System.EventHandler(this.lstQueue_DoubleClick);
		// 
		// btnAddToQueue
		// 
		this.btnAddToQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.btnAddToQueue.BackColor = System.Drawing.Color.Black;
		this.tableLayoutPanel1.SetColumnSpan(this.btnAddToQueue, 4);
		this.btnAddToQueue.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnAddToQueue.Location = new System.Drawing.Point(503, 295);
		this.btnAddToQueue.Name = "btnAddToQueue";
		this.btnAddToQueue.Size = new System.Drawing.Size(261, 28);
		this.btnAddToQueue.TabIndex = 11;
		this.btnAddToQueue.Text = "Add to Queue";
		this.btnAddToQueue.UseVisualStyleBackColor = false;
		this.btnAddToQueue.Click += new System.EventHandler(this.btnAddToQueue_Click);
		// 
		// gamePanel1
		// 
		this.gamePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.gamePanel1.BackColor = System.Drawing.Color.Black;
		this.gamePanel1.BorderColor = System.Drawing.Color.CornflowerBlue;
		this.gamePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tableLayoutPanel1.SetColumnSpan(this.gamePanel1, 4);
		this.gamePanel1.Controls.Add(this.lstResults);
		this.gamePanel1.ForeColor = System.Drawing.Color.White;
		this.gamePanel1.Location = new System.Drawing.Point(503, 157);
		this.gamePanel1.Name = "gamePanel1";
		this.gamePanel1.Padding = new System.Windows.Forms.Padding(3);
		this.gamePanel1.Size = new System.Drawing.Size(261, 132);
		this.gamePanel1.TabIndex = 10;
		// 
		// lstResults
		// 
		this.lstResults.BackColor = System.Drawing.Color.Black;
		this.lstResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.lstResults.Dock = System.Windows.Forms.DockStyle.Fill;
		this.lstResults.ForeColor = System.Drawing.Color.White;
		this.lstResults.Location = new System.Drawing.Point(3, 3);
		this.lstResults.Name = "lstResults";
		this.lstResults.Size = new System.Drawing.Size(253, 124);
		this.lstResults.TabIndex = 10;
		this.lstResults.UseCompatibleStateImageBehavior = false;
		this.lstResults.View = System.Windows.Forms.View.List;
		this.lstResults.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstResults_MouseClick);
		// 
		// dataGridViewProgressColumn1
		// 
		this.dataGridViewProgressColumn1.BarColor = System.Drawing.Color.Silver;
		this.dataGridViewProgressColumn1.DataPropertyName = "Progress";
		this.dataGridViewProgressColumn1.HeaderText = "Progress";
		this.dataGridViewProgressColumn1.MinimumWidth = 100;
		this.dataGridViewProgressColumn1.Name = "dataGridViewProgressColumn1";
		// 
		// btnCancel
		// 
		this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.btnCancel.BackColor = System.Drawing.Color.Black;
		this.btnCancel.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnCancel.Location = new System.Drawing.Point(671, 531);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(93, 39);
		this.btnCancel.TabIndex = 25;
		this.btnCancel.Text = "Cancel";
		this.btnCancel.UseVisualStyleBackColor = false;
		this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
		// 
		// resRes
		// 
		this.resRes.Amount = 0;
		this.resRes.BackColor = System.Drawing.Color.Black;
		this.resRes.Change = null;
		this.resRes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
		this.resRes.Location = new System.Drawing.Point(150, 0);
		this.resRes.Margin = new System.Windows.Forms.Padding(0);
		this.resRes.Name = "resRes";
		this.resRes.ResourceName = "Research";
		this.resRes.Size = new System.Drawing.Size(152, 20);
		this.resRes.TabIndex = 26;
		// 
		// tableLayoutPanel1
		// 
		this.tableLayoutPanel1.ColumnCount = 7;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84F));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42F));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42F));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 99F));
		this.tableLayoutPanel1.Controls.Add(this.ddlGroup, 2, 0);
		this.tableLayoutPanel1.Controls.Add(this.btnBottom, 3, 9);
		this.tableLayoutPanel1.Controls.Add(this.btnDown, 4, 9);
		this.tableLayoutPanel1.Controls.Add(this.resRes, 1, 0);
		this.tableLayoutPanel1.Controls.Add(this.txtTechName, 3, 0);
		this.tableLayoutPanel1.Controls.Add(this.sldSpending, 3, 3);
		this.tableLayoutPanel1.Controls.Add(this.btnUp, 4, 8);
		this.tableLayoutPanel1.Controls.Add(this.lblResults, 3, 4);
		this.tableLayoutPanel1.Controls.Add(this.btnTop, 3, 8);
		this.tableLayoutPanel1.Controls.Add(this.gamePanel1, 3, 5);
		this.tableLayoutPanel1.Controls.Add(this.gamePanel2, 3, 7);
		this.tableLayoutPanel1.Controls.Add(this.btnAddToQueue, 3, 6);
		this.tableLayoutPanel1.Controls.Add(this.btnClear, 6, 8);
		this.tableLayoutPanel1.Controls.Add(this.btnDelete, 6, 9);
		this.tableLayoutPanel1.Controls.Add(this.btnCancel, 6, 10);
		this.tableLayoutPanel1.Controls.Add(this.gridTechs, 0, 1);
		this.tableLayoutPanel1.Controls.Add(this.lblSpending, 3, 2);
		this.tableLayoutPanel1.Controls.Add(this.txtTechDiscription, 3, 1);
		this.tableLayoutPanel1.Controls.Add(this.lblPoints, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.btnTree, 4, 10);
		this.tableLayoutPanel1.Controls.Add(this.btnSave, 3, 10);
		this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.RowCount = 11;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 138F));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 134F));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
		this.tableLayoutPanel1.Size = new System.Drawing.Size(759, 573);
		this.tableLayoutPanel1.TabIndex = 27;
		// 
		// btnClear
		// 
		this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.btnClear.BackColor = System.Drawing.Color.Black;
		this.btnClear.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnClear.Location = new System.Drawing.Point(671, 463);
		this.btnClear.Name = "btnClear";
		this.btnClear.Size = new System.Drawing.Size(93, 28);
		this.btnClear.TabIndex = 20;
		this.btnClear.Text = "Clear";
		this.btnClear.UseVisualStyleBackColor = false;
		this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
		// 
		// btnDelete
		// 
		this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.btnDelete.BackColor = System.Drawing.Color.Black;
		this.btnDelete.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnDelete.Location = new System.Drawing.Point(671, 497);
		this.btnDelete.Name = "btnDelete";
		this.btnDelete.Size = new System.Drawing.Size(93, 28);
		this.btnDelete.TabIndex = 23;
		this.btnDelete.Text = "Delete";
		this.btnDelete.UseVisualStyleBackColor = false;
		this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
		// 
		// txtTechDiscription
		// 
		this.txtTechDiscription.AutoSize = true;
		this.tableLayoutPanel1.SetColumnSpan(this.txtTechDiscription, 4);
		this.txtTechDiscription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.txtTechDiscription.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.txtTechDiscription.Location = new System.Drawing.Point(503, 28);
		this.txtTechDiscription.Name = "txtTechDiscription";
		this.txtTechDiscription.Size = new System.Drawing.Size(97, 15);
		this.txtTechDiscription.TabIndex = 27;
		this.txtTechDiscription.Text = "(No Technology)";
		// 
		// btnTree
		// 
		this.btnTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.btnTree.BackColor = System.Drawing.Color.Black;
		this.btnTree.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnTree.Location = new System.Drawing.Point(629, 531);
		this.btnTree.Name = "btnTree";
		this.btnTree.Size = new System.Drawing.Size(36, 39);
		this.btnTree.TabIndex = 28;
		this.btnTree.Text = "Tree";
		this.btnTree.UseVisualStyleBackColor = false;
		this.btnTree.Click += new System.EventHandler(this.btnTree_Click);
		// 
		// btnSave
		// 
		this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.btnSave.BackColor = System.Drawing.Color.Black;
		this.tableLayoutPanel1.SetColumnSpan(this.btnSave, 2);
		this.btnSave.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnSave.Location = new System.Drawing.Point(503, 531);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(120, 39);
		this.btnSave.TabIndex = 24;
		this.btnSave.Text = "Save";
		this.btnSave.UseVisualStyleBackColor = false;
		this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
		// 
		// ResearchForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.ClientSize = new System.Drawing.Size(759, 573);
		this.Controls.Add(this.tableLayoutPanel1);
		this.ForeColor = System.Drawing.Color.White;
		this.MaximumSize = new System.Drawing.Size(775, 9999);
		this.MinimumSize = new System.Drawing.Size(775, 600);
		this.Name = "ResearchForm";
		this.ShowInTaskbar = false;
		this.Text = "Research";
		this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ResearchForm_FormClosing);
		this.Load += new System.EventHandler(this.ResearchForm_Load);
		this.MouseEnter += new System.EventHandler(this.ResearchForm_MouseEnter);
		((System.ComponentModel.ISupportInitialize)(this.gridTechs)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.technologyBindingSource)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.sldSpending)).EndInit();
		this.gamePanel2.ResumeLayout(false);
		this.gamePanel1.ResumeLayout(false);
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.PerformLayout();
		this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.DataGridView gridTechs;
	private System.Windows.Forms.BindingSource technologyBindingSource;
	private DataGridView.DataGridViewProgressColumn dataGridViewProgressColumn1;
	private System.Windows.Forms.ComboBox ddlGroup;
	private System.Windows.Forms.Label lblPoints;
	private System.Windows.Forms.Label txtTechName;
	private System.Windows.Forms.Label lblSpending;
	private System.Windows.Forms.TrackBar sldSpending;
	private System.Windows.Forms.Label lblResults;
	private Controls.GamePanel gamePanel1;
	private System.Windows.Forms.ListView lstResults;
	private Controls.GameButton btnAddToQueue;
	private Controls.GamePanel gamePanel2;
        private System.Windows.Forms.ListBox lstQueue;
	private Controls.GameButton btnDown;
        private Controls.GameButton btnBottom;
	private Controls.GameButton btnUp;
        private Controls.GameButton btnTop;
	private Controls.GameButton btnCancel;
	private Controls.ResourceDisplay resRes;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Controls.GameButton btnClear;
        private Controls.GameButton btnDelete;
        private Controls.GameButton btnSave;
        private System.Windows.Forms.Label txtTechDiscription;
	private Controls.GameButton btnTree;
	private System.Windows.Forms.DataGridViewTextBoxColumn colName;
	private System.Windows.Forms.DataGridViewTextBoxColumn colLevel;
	private System.Windows.Forms.DataGridViewTextBoxColumn colNextLevelCost;
	private DataGridView.DataGridViewProgressColumn colProgress;
	private DataGridView.DataGridViewProgressColumn colSpending;

}