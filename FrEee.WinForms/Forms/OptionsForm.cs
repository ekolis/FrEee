using FrEee.Game.Objects.Civilization;
using FrEee.WinForms.Objects;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
	public partial class OptionsForm : GameForm
	{
		public OptionsForm()
		{
			InitializeComponent();
		}

		private Controls.GameButton btnCancel;
		private Controls.GameButton btnSave;
		private Controls.GameButton btnSE4;
		private GroupBox groupBox1;
		private Label label1;
		private Label label2;
		private Label label7;
		private TrackBar sldEffects;
		private TrackBar sldMaster;
		private GroupBox grpPlayerInfo;
		private Label label9;
		private Label label8;
		private Label label6;
		private Label label5;
		private Label label4;
		private Label label3;
		private TextBox txtNotes;
		private TextBox txtDiscord;
		private TextBox txtIrc;
		private TextBox txtEmail;
		private TextBox txtPbw;
		private TextBox txtName;
		private TextBox txtWebsite;
		private Label label10;
		private TrackBar sldMusic;

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			ClientSettings.Instance.MasterVolume = sldMaster.Value;
			ClientSettings.Instance.MusicVolume = sldMusic.Value;
			ClientSettings.Instance.EffectsVolume = sldEffects.Value;
			if (ClientSettings.Instance.PlayerInfo == null)
				ClientSettings.Instance.PlayerInfo = new PlayerInfo();
			ClientSettings.Instance.PlayerInfo.Name = txtName.Text;
			ClientSettings.Instance.PlayerInfo.Pbw = txtPbw.Text;
			ClientSettings.Instance.PlayerInfo.Email = txtEmail.Text;
			ClientSettings.Instance.PlayerInfo.Irc = txtIrc.Text;
			ClientSettings.Instance.PlayerInfo.Discord = txtDiscord.Text;
			ClientSettings.Instance.PlayerInfo.Notes = txtNotes.Text;
			ClientSettings.Instance.PlayerInfo.Website = txtWebsite.Text;
			ClientSettings.Save();
			Music.setVolume(ClientSettings.Instance.MasterVolume * ClientSettings.Instance.MusicVolume * 1.0e-4f);
			Music.StartNewTrack();
			Close();
		}

		private void btnSE4_Click(object sender, EventArgs e)
		{
			// find SE4 folder
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			fbd.Description = "Locate SE4 root folder";
			DialogResult result = fbd.ShowDialog();
			if (result == DialogResult.Cancel)
			{
				return;
			}
			string se4root = fbd.SelectedPath;

			DirectoryInfo dir = null;
			string output = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures");
			// create the output pictures folder if it doesn't exist
			if (!Directory.Exists(output))
			{
				Directory.CreateDirectory(output);
			}

			// === MOST OF THE ART IS HERE ===
			string[] folders = { "Planets", "Components", "Facilities", "Systems" };
			foreach (string f in folders)
			{
				dir = new DirectoryInfo(se4root + "/Pictures/" + f);
				if (dir.Exists)
				{
					output = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures/" + f);
					if (!Directory.Exists(output))
					{
						Directory.CreateDirectory(output);
					}

					FileInfo[] files = dir.GetFiles();
					foreach (FileInfo file in files)
					{
						string temppath = Path.Combine(output, file.Name);
						string conflict = temppath.Replace(".BMP", ".png").Replace(".bmp", ".png");
						//if (File.Exists(conflict))
						//{
						//  File.Delete(conflict);
						//}
						file.CopyTo(temppath, true);
					}
				}
			}

			// === RACES AND THEIR SHIPSETS ===
			output = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures/Races");
			dir = new DirectoryInfo(se4root + "/Pictures/Races");
			foreach (DirectoryInfo subdir in dir.GetDirectories())
			{
				string subOutput = Path.Combine(output, subdir.Name);
				if (!Directory.Exists(subOutput))
				{
					Directory.CreateDirectory(subOutput);
				}
				FileInfo[] files = (new DirectoryInfo(se4root + "/Pictures/Races/" + subdir.Name)).GetFiles();
				foreach (FileInfo file in files)
				{
					string temppath = Path.Combine(subOutput, file.Name);
					file.CopyTo(temppath, true);
				}
			}

			/* // TEMPORARILY DISABLED: we need to classify all the tracks to put them in the right sub folder
            // === MUSIC ===
            foreach (string f in folders) {
              dir = new DirectoryInfo(se4root + "/Music/");
              if (dir.Exists) {
                output = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Music");
                if (!Directory.Exists(output)) {
                  Directory.CreateDirectory(output);
                }

                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files) {
                  string temppath = Path.Combine(output, file.Name);
                  file.CopyTo(temppath, true);
                }
              }
            }
            */
		}

		private void InitializeComponent()
		{
			this.sldMaster = new System.Windows.Forms.TrackBar();
			this.label7 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.sldEffects = new System.Windows.Forms.TrackBar();
			this.label1 = new System.Windows.Forms.Label();
			this.sldMusic = new System.Windows.Forms.TrackBar();
			this.btnCancel = new FrEee.WinForms.Controls.GameButton();
			this.btnSave = new FrEee.WinForms.Controls.GameButton();
			this.btnSE4 = new FrEee.WinForms.Controls.GameButton();
			this.grpPlayerInfo = new System.Windows.Forms.GroupBox();
			this.txtWebsite = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.txtNotes = new System.Windows.Forms.TextBox();
			this.txtDiscord = new System.Windows.Forms.TextBox();
			this.txtIrc = new System.Windows.Forms.TextBox();
			this.txtEmail = new System.Windows.Forms.TextBox();
			this.txtPbw = new System.Windows.Forms.TextBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.sldMaster)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.sldEffects)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sldMusic)).BeginInit();
			this.grpPlayerInfo.SuspendLayout();
			this.SuspendLayout();
			// 
			// sldMaster
			// 
			this.sldMaster.LargeChange = 10;
			this.sldMaster.Location = new System.Drawing.Point(140, 19);
			this.sldMaster.Maximum = 100;
			this.sldMaster.Name = "sldMaster";
			this.sldMaster.Size = new System.Drawing.Size(213, 45);
			this.sldMaster.TabIndex = 0;
			this.sldMaster.TickFrequency = 10;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label7.Location = new System.Drawing.Point(6, 19);
			this.label7.Margin = new System.Windows.Forms.Padding(3);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(39, 13);
			this.label7.TabIndex = 17;
			this.label7.Text = "Master";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.sldEffects);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.sldMusic);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.sldMaster);
			this.groupBox1.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.groupBox1.Location = new System.Drawing.Point(12, 319);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(359, 176);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Volume";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label2.Location = new System.Drawing.Point(6, 121);
			this.label2.Margin = new System.Windows.Forms.Padding(3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 13);
			this.label2.TabIndex = 21;
			this.label2.Text = "Effects";
			// 
			// sldEffects
			// 
			this.sldEffects.Enabled = false;
			this.sldEffects.LargeChange = 10;
			this.sldEffects.Location = new System.Drawing.Point(140, 121);
			this.sldEffects.Maximum = 100;
			this.sldEffects.Name = "sldEffects";
			this.sldEffects.Size = new System.Drawing.Size(213, 45);
			this.sldEffects.TabIndex = 2;
			this.sldEffects.TickFrequency = 10;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label1.Location = new System.Drawing.Point(6, 70);
			this.label1.Margin = new System.Windows.Forms.Padding(3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 19;
			this.label1.Text = "Music";
			// 
			// sldMusic
			// 
			this.sldMusic.LargeChange = 10;
			this.sldMusic.Location = new System.Drawing.Point(140, 70);
			this.sldMusic.Maximum = 100;
			this.sldMusic.Name = "sldMusic";
			this.sldMusic.Size = new System.Drawing.Size(213, 45);
			this.sldMusic.TabIndex = 1;
			this.sldMusic.TickFrequency = 10;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.BackColor = System.Drawing.Color.Black;
			this.btnCancel.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnCancel.Location = new System.Drawing.Point(295, 508);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.BackColor = System.Drawing.Color.Black;
			this.btnSave.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnSave.Location = new System.Drawing.Point(214, 508);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 4;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = false;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnSE4
			// 
			this.btnSE4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSE4.BackColor = System.Drawing.Color.Black;
			this.btnSE4.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnSE4.Location = new System.Drawing.Point(12, 501);
			this.btnSE4.Name = "btnSE4";
			this.btnSE4.Size = new System.Drawing.Size(104, 30);
			this.btnSE4.TabIndex = 3;
			this.btnSE4.Text = "Copy SE4 Assets";
			this.btnSE4.UseVisualStyleBackColor = false;
			this.btnSE4.Click += new System.EventHandler(this.btnSE4_Click);
			// 
			// grpPlayerInfo
			// 
			this.grpPlayerInfo.Controls.Add(this.txtWebsite);
			this.grpPlayerInfo.Controls.Add(this.label10);
			this.grpPlayerInfo.Controls.Add(this.txtNotes);
			this.grpPlayerInfo.Controls.Add(this.txtDiscord);
			this.grpPlayerInfo.Controls.Add(this.txtIrc);
			this.grpPlayerInfo.Controls.Add(this.txtEmail);
			this.grpPlayerInfo.Controls.Add(this.txtPbw);
			this.grpPlayerInfo.Controls.Add(this.txtName);
			this.grpPlayerInfo.Controls.Add(this.label9);
			this.grpPlayerInfo.Controls.Add(this.label8);
			this.grpPlayerInfo.Controls.Add(this.label6);
			this.grpPlayerInfo.Controls.Add(this.label5);
			this.grpPlayerInfo.Controls.Add(this.label4);
			this.grpPlayerInfo.Controls.Add(this.label3);
			this.grpPlayerInfo.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.grpPlayerInfo.Location = new System.Drawing.Point(12, 12);
			this.grpPlayerInfo.Name = "grpPlayerInfo";
			this.grpPlayerInfo.Size = new System.Drawing.Size(359, 301);
			this.grpPlayerInfo.TabIndex = 1;
			this.grpPlayerInfo.TabStop = false;
			this.grpPlayerInfo.Text = "Player Info";
			// 
			// txtWebsite
			// 
			this.txtWebsite.Location = new System.Drawing.Point(57, 146);
			this.txtWebsite.Name = "txtWebsite";
			this.txtWebsite.Size = new System.Drawing.Size(190, 20);
			this.txtWebsite.TabIndex = 5;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label10.Location = new System.Drawing.Point(7, 149);
			this.label10.Margin = new System.Windows.Forms.Padding(3);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(46, 13);
			this.label10.TabIndex = 30;
			this.label10.Text = "Website";
			// 
			// txtNotes
			// 
			this.txtNotes.Location = new System.Drawing.Point(57, 174);
			this.txtNotes.Multiline = true;
			this.txtNotes.Name = "txtNotes";
			this.txtNotes.Size = new System.Drawing.Size(296, 98);
			this.txtNotes.TabIndex = 6;
			// 
			// txtDiscord
			// 
			this.txtDiscord.Location = new System.Drawing.Point(57, 120);
			this.txtDiscord.Name = "txtDiscord";
			this.txtDiscord.Size = new System.Drawing.Size(190, 20);
			this.txtDiscord.TabIndex = 4;
			// 
			// txtIrc
			// 
			this.txtIrc.Location = new System.Drawing.Point(57, 94);
			this.txtIrc.Name = "txtIrc";
			this.txtIrc.Size = new System.Drawing.Size(190, 20);
			this.txtIrc.TabIndex = 3;
			// 
			// txtEmail
			// 
			this.txtEmail.Location = new System.Drawing.Point(57, 68);
			this.txtEmail.Name = "txtEmail";
			this.txtEmail.Size = new System.Drawing.Size(190, 20);
			this.txtEmail.TabIndex = 2;
			// 
			// txtPbw
			// 
			this.txtPbw.Location = new System.Drawing.Point(57, 42);
			this.txtPbw.Name = "txtPbw";
			this.txtPbw.Size = new System.Drawing.Size(190, 20);
			this.txtPbw.TabIndex = 1;
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(57, 16);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(190, 20);
			this.txtName.TabIndex = 0;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label9.Location = new System.Drawing.Point(7, 174);
			this.label9.Margin = new System.Windows.Forms.Padding(3);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(35, 13);
			this.label9.TabIndex = 23;
			this.label9.Text = "Notes";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label8.Location = new System.Drawing.Point(6, 123);
			this.label8.Margin = new System.Windows.Forms.Padding(3);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(43, 13);
			this.label8.TabIndex = 22;
			this.label8.Text = "Discord";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label6.Location = new System.Drawing.Point(7, 97);
			this.label6.Margin = new System.Windows.Forms.Padding(3);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(25, 13);
			this.label6.TabIndex = 21;
			this.label6.Text = "IRC";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label5.Location = new System.Drawing.Point(6, 71);
			this.label5.Margin = new System.Windows.Forms.Padding(3);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(32, 13);
			this.label5.TabIndex = 20;
			this.label5.Text = "Email";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label4.Location = new System.Drawing.Point(7, 45);
			this.label4.Margin = new System.Windows.Forms.Padding(3);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(32, 13);
			this.label4.TabIndex = 19;
			this.label4.Text = "PBW";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label3.Location = new System.Drawing.Point(7, 19);
			this.label3.Margin = new System.Windows.Forms.Padding(3);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(35, 13);
			this.label3.TabIndex = 18;
			this.label3.Text = "Name";
			// 
			// OptionsForm
			// 
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(382, 543);
			this.Controls.Add(this.grpPlayerInfo);
			this.Controls.Add(this.btnSE4);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "OptionsForm";
			this.Text = "Options";
			this.Load += new System.EventHandler(this.OptionsForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.sldMaster)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.sldEffects)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sldMusic)).EndInit();
			this.grpPlayerInfo.ResumeLayout(false);
			this.grpPlayerInfo.PerformLayout();
			this.ResumeLayout(false);

		}

		private void OptionsForm_Load(object sender, EventArgs e)
		{
			txtName.Text = ClientSettings.Instance.PlayerInfo.Name;
			txtPbw.Text = ClientSettings.Instance.PlayerInfo.Pbw;
			txtEmail.Text = ClientSettings.Instance.PlayerInfo.Email;
			txtIrc.Text = ClientSettings.Instance.PlayerInfo.Irc;
			txtDiscord.Text = ClientSettings.Instance.PlayerInfo.Discord;
			txtNotes.Text = ClientSettings.Instance.PlayerInfo.Notes;
			txtWebsite.Text = ClientSettings.Instance.PlayerInfo.Website;
			sldMaster.Value = Math.Max(0, Math.Min(100, ClientSettings.Instance.MasterVolume));
			sldMusic.Value = Math.Max(0, Math.Min(100, ClientSettings.Instance.MusicVolume));
			sldEffects.Value = Math.Max(0, Math.Min(100, ClientSettings.Instance.EffectsVolume));
		}
	}
}