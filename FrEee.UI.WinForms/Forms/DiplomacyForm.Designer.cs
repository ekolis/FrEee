namespace FrEee.UI.WinForms.Forms;

partial class DiplomacyForm
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
		this.label1 = new System.Windows.Forms.Label();
		this.txtInReplyTo = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txtMessage = new System.Windows.Forms.TextBox();
		this.ddlMessageType = new System.Windows.Forms.ComboBox();
		this.treeWeHave = new System.Windows.Forms.TreeView();
		this.treeTheyHave = new System.Windows.Forms.TreeView();
		this.lblWeHave = new System.Windows.Forms.Label();
		this.lblTheyHave = new System.Windows.Forms.Label();
		this.treeTable = new System.Windows.Forms.TreeView();
		this.lblTable = new System.Windows.Forms.Label();
		this.chkTentative = new System.Windows.Forms.CheckBox();
		this.pnlQuantityLevel = new System.Windows.Forms.Panel();
		this.chkPercent = new System.Windows.Forms.CheckBox();
		this.lblQuantityLevel = new System.Windows.Forms.Label();
		this.txtQuantityLevel = new System.Windows.Forms.TextBox();
		this.ddlAlliance = new System.Windows.Forms.ComboBox();
		this.btnReturn = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnRequest = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnGive = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnCancel = new FrEee.UI.WinForms.Controls.GameButton();
		this.btnSend = new FrEee.UI.WinForms.Controls.GameButton();
		this.picPortrait = new FrEee.UI.WinForms.Controls.GamePictureBox();
		this.pnlQuantityLevel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.picPortrait)).BeginInit();
		this.SuspendLayout();
		// 
		// label1
		// 
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
		this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label1.Location = new System.Drawing.Point(12, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(74, 17);
		this.label1.TabIndex = 1;
		this.label1.Text = "In reply to:";
		// 
		// txtInReplyTo
		// 
		this.txtInReplyTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtInReplyTo.Location = new System.Drawing.Point(121, 30);
		this.txtInReplyTo.Multiline = true;
		this.txtInReplyTo.Name = "txtInReplyTo";
		this.txtInReplyTo.ReadOnly = true;
		this.txtInReplyTo.Size = new System.Drawing.Size(636, 100);
		this.txtInReplyTo.TabIndex = 2;
		this.txtInReplyTo.TabStop = false;
		// 
		// label2
		// 
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
		this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.label2.Location = new System.Drawing.Point(12, 137);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(65, 17);
		this.label2.TabIndex = 4;
		this.label2.Text = "Message";
		// 
		// txtMessage
		// 
		this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.txtMessage.Location = new System.Drawing.Point(15, 163);
		this.txtMessage.Multiline = true;
		this.txtMessage.Name = "txtMessage";
		this.txtMessage.Size = new System.Drawing.Size(742, 100);
		this.txtMessage.TabIndex = 5;
		// 
		// ddlMessageType
		// 
		this.ddlMessageType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.ddlMessageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.ddlMessageType.FormattingEnabled = true;
		this.ddlMessageType.Location = new System.Drawing.Point(83, 136);
		this.ddlMessageType.Name = "ddlMessageType";
		this.ddlMessageType.Size = new System.Drawing.Size(199, 21);
		this.ddlMessageType.TabIndex = 23;
		this.ddlMessageType.SelectedIndexChanged += new System.EventHandler(this.ddlMessageType_SelectedIndexChanged);
		// 
		// treeWeHave
		// 
		this.treeWeHave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
		this.treeWeHave.BackColor = System.Drawing.Color.Black;
		this.treeWeHave.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.treeWeHave.ForeColor = System.Drawing.Color.White;
		this.treeWeHave.HideSelection = false;
		this.treeWeHave.Location = new System.Drawing.Point(15, 290);
		this.treeWeHave.Name = "treeWeHave";
		this.treeWeHave.Size = new System.Drawing.Size(253, 236);
		this.treeWeHave.TabIndex = 25;
		this.treeWeHave.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeWeHave_MouseDoubleClick);
		// 
		// treeTheyHave
		// 
		this.treeTheyHave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.treeTheyHave.BackColor = System.Drawing.Color.Black;
		this.treeTheyHave.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.treeTheyHave.ForeColor = System.Drawing.Color.White;
		this.treeTheyHave.HideSelection = false;
		this.treeTheyHave.Location = new System.Drawing.Point(505, 290);
		this.treeTheyHave.Name = "treeTheyHave";
		this.treeTheyHave.Size = new System.Drawing.Size(252, 236);
		this.treeTheyHave.TabIndex = 26;
		this.treeTheyHave.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeTheyHave_MouseDoubleClick);
		// 
		// lblWeHave
		// 
		this.lblWeHave.AutoSize = true;
		this.lblWeHave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
		this.lblWeHave.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblWeHave.Location = new System.Drawing.Point(12, 270);
		this.lblWeHave.Name = "lblWeHave";
		this.lblWeHave.Size = new System.Drawing.Size(66, 17);
		this.lblWeHave.TabIndex = 27;
		this.lblWeHave.Text = "We Have";
		// 
		// lblTheyHave
		// 
		this.lblTheyHave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.lblTheyHave.AutoSize = true;
		this.lblTheyHave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
		this.lblTheyHave.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblTheyHave.Location = new System.Drawing.Point(502, 270);
		this.lblTheyHave.Name = "lblTheyHave";
		this.lblTheyHave.Size = new System.Drawing.Size(77, 17);
		this.lblTheyHave.TabIndex = 28;
		this.lblTheyHave.Text = "They Have";
		// 
		// treeTable
		// 
		this.treeTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.treeTable.BackColor = System.Drawing.Color.Black;
		this.treeTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.treeTable.ForeColor = System.Drawing.Color.White;
		this.treeTable.HideSelection = false;
		this.treeTable.Location = new System.Drawing.Point(274, 290);
		this.treeTable.Name = "treeTable";
		this.treeTable.Size = new System.Drawing.Size(225, 236);
		this.treeTable.TabIndex = 29;
		this.treeTable.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeTable_AfterSelect);
		this.treeTable.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeTable_MouseDoubleClick);
		// 
		// lblTable
		// 
		this.lblTable.AutoSize = true;
		this.lblTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
		this.lblTable.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblTable.Location = new System.Drawing.Point(271, 270);
		this.lblTable.Name = "lblTable";
		this.lblTable.Size = new System.Drawing.Size(91, 17);
		this.lblTable.TabIndex = 30;
		this.lblTable.Text = "On the Table";
		// 
		// chkTentative
		// 
		this.chkTentative.AutoSize = true;
		this.chkTentative.Location = new System.Drawing.Point(15, 562);
		this.chkTentative.Name = "chkTentative";
		this.chkTentative.Size = new System.Drawing.Size(125, 17);
		this.chkTentative.TabIndex = 35;
		this.chkTentative.Text = "Proposal is Tentative";
		this.chkTentative.UseVisualStyleBackColor = true;
		// 
		// pnlQuantityLevel
		// 
		this.pnlQuantityLevel.Controls.Add(this.chkPercent);
		this.pnlQuantityLevel.Controls.Add(this.lblQuantityLevel);
		this.pnlQuantityLevel.Controls.Add(this.txtQuantityLevel);
		this.pnlQuantityLevel.Location = new System.Drawing.Point(233, 563);
		this.pnlQuantityLevel.Name = "pnlQuantityLevel";
		this.pnlQuantityLevel.Size = new System.Drawing.Size(312, 25);
		this.pnlQuantityLevel.TabIndex = 38;
		this.pnlQuantityLevel.Visible = false;
		// 
		// chkPercent
		// 
		this.chkPercent.AutoSize = true;
		this.chkPercent.Location = new System.Drawing.Point(222, 6);
		this.chkPercent.Name = "chkPercent";
		this.chkPercent.Size = new System.Drawing.Size(34, 17);
		this.chkPercent.TabIndex = 40;
		this.chkPercent.Text = "%";
		this.chkPercent.UseVisualStyleBackColor = true;
		this.chkPercent.CheckedChanged += new System.EventHandler(this.chkPercent_CheckedChanged);
		// 
		// lblQuantityLevel
		// 
		this.lblQuantityLevel.AutoSize = true;
		this.lblQuantityLevel.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.lblQuantityLevel.Location = new System.Drawing.Point(66, 7);
		this.lblQuantityLevel.Name = "lblQuantityLevel";
		this.lblQuantityLevel.Size = new System.Drawing.Size(77, 13);
		this.lblQuantityLevel.TabIndex = 39;
		this.lblQuantityLevel.Text = "Quantity/Level";
		// 
		// txtQuantityLevel
		// 
		this.txtQuantityLevel.Location = new System.Drawing.Point(147, 3);
		this.txtQuantityLevel.Name = "txtQuantityLevel";
		this.txtQuantityLevel.Size = new System.Drawing.Size(67, 20);
		this.txtQuantityLevel.TabIndex = 38;
		this.txtQuantityLevel.TextChanged += new System.EventHandler(this.txtQuantityLevel_TextChanged);
		// 
		// ddlAlliance
		// 
		this.ddlAlliance.DisplayMember = "Name";
		this.ddlAlliance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.ddlAlliance.FormattingEnabled = true;
		this.ddlAlliance.Location = new System.Drawing.Point(336, 565);
		this.ddlAlliance.Name = "ddlAlliance";
		this.ddlAlliance.Size = new System.Drawing.Size(121, 21);
		this.ddlAlliance.TabIndex = 43;
		this.ddlAlliance.ValueMember = "Value";
		this.ddlAlliance.Visible = false;
		this.ddlAlliance.SelectedIndexChanged += new System.EventHandler(this.ddlAlliance_SelectedIndexChanged);
		// 
		// btnReturn
		// 
		this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
		this.btnReturn.BackColor = System.Drawing.Color.Black;
		this.btnReturn.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnReturn.Location = new System.Drawing.Point(274, 532);
		this.btnReturn.Name = "btnReturn";
		this.btnReturn.Size = new System.Drawing.Size(225, 23);
		this.btnReturn.TabIndex = 33;
		this.btnReturn.Text = "X";
		this.btnReturn.UseVisualStyleBackColor = false;
		this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
		// 
		// btnRequest
		// 
		this.btnRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.btnRequest.BackColor = System.Drawing.Color.Black;
		this.btnRequest.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnRequest.Location = new System.Drawing.Point(505, 532);
		this.btnRequest.Name = "btnRequest";
		this.btnRequest.Size = new System.Drawing.Size(250, 23);
		this.btnRequest.TabIndex = 32;
		this.btnRequest.Text = "<";
		this.btnRequest.UseVisualStyleBackColor = false;
		this.btnRequest.Click += new System.EventHandler(this.btnRequest_Click);
		// 
		// btnGive
		// 
		this.btnGive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		this.btnGive.BackColor = System.Drawing.Color.Black;
		this.btnGive.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnGive.Location = new System.Drawing.Point(12, 532);
		this.btnGive.Name = "btnGive";
		this.btnGive.Size = new System.Drawing.Size(256, 23);
		this.btnGive.TabIndex = 31;
		this.btnGive.Text = ">";
		this.btnGive.UseVisualStyleBackColor = false;
		this.btnGive.Click += new System.EventHandler(this.btnGive_Click);
		// 
		// btnCancel
		// 
		this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.btnCancel.BackColor = System.Drawing.Color.Black;
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnCancel.Location = new System.Drawing.Point(551, 563);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(100, 23);
		this.btnCancel.TabIndex = 20;
		this.btnCancel.Text = "Cancel";
		this.btnCancel.UseVisualStyleBackColor = false;
		this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
		// 
		// btnSend
		// 
		this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.btnSend.BackColor = System.Drawing.Color.Black;
		this.btnSend.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnSend.ForeColor = System.Drawing.Color.CornflowerBlue;
		this.btnSend.Location = new System.Drawing.Point(657, 563);
		this.btnSend.Name = "btnSend";
		this.btnSend.Size = new System.Drawing.Size(100, 23);
		this.btnSend.TabIndex = 19;
		this.btnSend.Text = "Send";
		this.btnSend.UseVisualStyleBackColor = false;
		this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
		// 
		// picPortrait
		// 
		this.picPortrait.Location = new System.Drawing.Point(15, 30);
		this.picPortrait.Name = "picPortrait";
		this.picPortrait.Size = new System.Drawing.Size(100, 100);
		this.picPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.picPortrait.TabIndex = 3;
		this.picPortrait.TabStop = false;
		this.picPortrait.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picPortrait_MouseClick);
		// 
		// DiplomacyForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.ClientSize = new System.Drawing.Size(769, 598);
		this.Controls.Add(this.ddlAlliance);
		this.Controls.Add(this.pnlQuantityLevel);
		this.Controls.Add(this.chkTentative);
		this.Controls.Add(this.btnReturn);
		this.Controls.Add(this.btnRequest);
		this.Controls.Add(this.btnGive);
		this.Controls.Add(this.lblTable);
		this.Controls.Add(this.treeTable);
		this.Controls.Add(this.lblTheyHave);
		this.Controls.Add(this.lblWeHave);
		this.Controls.Add(this.treeTheyHave);
		this.Controls.Add(this.treeWeHave);
		this.Controls.Add(this.ddlMessageType);
		this.Controls.Add(this.btnCancel);
		this.Controls.Add(this.btnSend);
		this.Controls.Add(this.txtMessage);
		this.Controls.Add(this.label2);
		this.Controls.Add(this.picPortrait);
		this.Controls.Add(this.txtInReplyTo);
		this.Controls.Add(this.label1);
		this.ForeColor = System.Drawing.Color.White;
		this.Name = "DiplomacyForm";
		this.Text = "Diplomacy";
		this.Load += new System.EventHandler(this.DiplomacyForm_Load);
		this.pnlQuantityLevel.ResumeLayout(false);
		this.pnlQuantityLevel.PerformLayout();
		((System.ComponentModel.ISupportInitialize)(this.picPortrait)).EndInit();
		this.ResumeLayout(false);
		this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.TextBox txtInReplyTo;
	private Controls.GamePictureBox picPortrait;
	private System.Windows.Forms.Label label2;
	private System.Windows.Forms.TextBox txtMessage;
	private Controls.GameButton btnCancel;
	private Controls.GameButton btnSend;
	private System.Windows.Forms.ComboBox ddlMessageType;
	private System.Windows.Forms.TreeView treeWeHave;
	private System.Windows.Forms.TreeView treeTheyHave;
	private System.Windows.Forms.Label lblWeHave;
	private System.Windows.Forms.Label lblTheyHave;
	private System.Windows.Forms.TreeView treeTable;
	private System.Windows.Forms.Label lblTable;
	private Controls.GameButton btnGive;
	private Controls.GameButton btnRequest;
	private Controls.GameButton btnReturn;
	private System.Windows.Forms.CheckBox chkTentative;
	private System.Windows.Forms.Panel pnlQuantityLevel;
	private System.Windows.Forms.Label lblQuantityLevel;
	private System.Windows.Forms.TextBox txtQuantityLevel;
	private System.Windows.Forms.CheckBox chkPercent;
	private System.Windows.Forms.ComboBox ddlAlliance;
}