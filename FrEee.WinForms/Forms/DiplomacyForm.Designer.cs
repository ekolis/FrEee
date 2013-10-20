namespace FrEee.WinForms.Forms
{
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
			this.btnCounter = new FrEee.WinForms.Controls.GameButton();
			this.btnReturn = new FrEee.WinForms.Controls.GameButton();
			this.btnRequest = new FrEee.WinForms.Controls.GameButton();
			this.btnGive = new FrEee.WinForms.Controls.GameButton();
			this.btnGenerate = new FrEee.WinForms.Controls.GameButton();
			this.btnCancel = new FrEee.WinForms.Controls.GameButton();
			this.btnSend = new FrEee.WinForms.Controls.GameButton();
			this.picPortrait = new FrEee.WinForms.Controls.GamePictureBox();
			this.chkTentative = new System.Windows.Forms.CheckBox();
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
			this.txtInReplyTo.Size = new System.Drawing.Size(524, 100);
			this.txtInReplyTo.TabIndex = 2;
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
			this.txtMessage.Size = new System.Drawing.Size(630, 100);
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
			this.ddlMessageType.Size = new System.Drawing.Size(197, 21);
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
			this.treeWeHave.Location = new System.Drawing.Point(15, 290);
			this.treeWeHave.Name = "treeWeHave";
			this.treeWeHave.Size = new System.Drawing.Size(201, 236);
			this.treeWeHave.TabIndex = 25;
			// 
			// treeTheyHave
			// 
			this.treeTheyHave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.treeTheyHave.BackColor = System.Drawing.Color.Black;
			this.treeTheyHave.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.treeTheyHave.ForeColor = System.Drawing.Color.White;
			this.treeTheyHave.Location = new System.Drawing.Point(439, 290);
			this.treeTheyHave.Name = "treeTheyHave";
			this.treeTheyHave.Size = new System.Drawing.Size(206, 236);
			this.treeTheyHave.TabIndex = 26;
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
			this.lblTheyHave.AutoSize = true;
			this.lblTheyHave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.lblTheyHave.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblTheyHave.Location = new System.Drawing.Point(436, 270);
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
			this.treeTable.Location = new System.Drawing.Point(222, 290);
			this.treeTable.Name = "treeTable";
			this.treeTable.Size = new System.Drawing.Size(211, 236);
			this.treeTable.TabIndex = 29;
			// 
			// lblTable
			// 
			this.lblTable.AutoSize = true;
			this.lblTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.lblTable.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblTable.Location = new System.Drawing.Point(219, 270);
			this.lblTable.Name = "lblTable";
			this.lblTable.Size = new System.Drawing.Size(91, 17);
			this.lblTable.TabIndex = 30;
			this.lblTable.Text = "On the Table";
			// 
			// btnCounter
			// 
			this.btnCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCounter.BackColor = System.Drawing.Color.Black;
			this.btnCounter.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnCounter.Location = new System.Drawing.Point(229, 563);
			this.btnCounter.Name = "btnCounter";
			this.btnCounter.Size = new System.Drawing.Size(204, 23);
			this.btnCounter.TabIndex = 34;
			this.btnCounter.Text = "Counter Proposal";
			this.btnCounter.UseVisualStyleBackColor = false;
			this.btnCounter.Click += new System.EventHandler(this.btnCounter_Click);
			// 
			// btnReturn
			// 
			this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnReturn.BackColor = System.Drawing.Color.Black;
			this.btnReturn.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnReturn.Location = new System.Drawing.Point(229, 532);
			this.btnReturn.Name = "btnReturn";
			this.btnReturn.Size = new System.Drawing.Size(204, 23);
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
			this.btnRequest.Location = new System.Drawing.Point(439, 532);
			this.btnRequest.Name = "btnRequest";
			this.btnRequest.Size = new System.Drawing.Size(204, 23);
			this.btnRequest.TabIndex = 32;
			this.btnRequest.Text = "<";
			this.btnRequest.UseVisualStyleBackColor = false;
			this.btnRequest.Click += new System.EventHandler(this.btnRequest_Click);
			// 
			// btnGive
			// 
			this.btnGive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGive.BackColor = System.Drawing.Color.Black;
			this.btnGive.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnGive.Location = new System.Drawing.Point(12, 532);
			this.btnGive.Name = "btnGive";
			this.btnGive.Size = new System.Drawing.Size(204, 23);
			this.btnGive.TabIndex = 31;
			this.btnGive.Text = ">";
			this.btnGive.UseVisualStyleBackColor = false;
			this.btnGive.Click += new System.EventHandler(this.btnGive_Click);
			// 
			// btnGenerate
			// 
			this.btnGenerate.BackColor = System.Drawing.Color.Black;
			this.btnGenerate.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnGenerate.Location = new System.Drawing.Point(286, 134);
			this.btnGenerate.Name = "btnGenerate";
			this.btnGenerate.Size = new System.Drawing.Size(100, 23);
			this.btnGenerate.TabIndex = 24;
			this.btnGenerate.Text = "Generate";
			this.btnGenerate.UseVisualStyleBackColor = false;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.BackColor = System.Drawing.Color.Black;
			this.btnCancel.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnCancel.Location = new System.Drawing.Point(439, 563);
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
			this.btnSend.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnSend.Location = new System.Drawing.Point(545, 563);
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
			// DiplomacyForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(657, 598);
			this.Controls.Add(this.chkTentative);
			this.Controls.Add(this.btnCounter);
			this.Controls.Add(this.btnReturn);
			this.Controls.Add(this.btnRequest);
			this.Controls.Add(this.btnGive);
			this.Controls.Add(this.lblTable);
			this.Controls.Add(this.treeTable);
			this.Controls.Add(this.lblTheyHave);
			this.Controls.Add(this.lblWeHave);
			this.Controls.Add(this.treeTheyHave);
			this.Controls.Add(this.treeWeHave);
			this.Controls.Add(this.btnGenerate);
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
		private Controls.GameButton btnGenerate;
		private System.Windows.Forms.TreeView treeWeHave;
		private System.Windows.Forms.TreeView treeTheyHave;
		private System.Windows.Forms.Label lblWeHave;
		private System.Windows.Forms.Label lblTheyHave;
		private System.Windows.Forms.TreeView treeTable;
		private System.Windows.Forms.Label lblTable;
		private Controls.GameButton btnGive;
		private Controls.GameButton btnRequest;
		private Controls.GameButton btnReturn;
		private Controls.GameButton btnCounter;
		private System.Windows.Forms.CheckBox chkTentative;
	}
}