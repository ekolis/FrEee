using FrEee.Objects.Civilization;
using FrEee.UI.WinForms.Objects;
using FrEee.UI.WinForms.Utility.Extensions;
using FrEee.Utility;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace FrEee.UI.WinForms.Forms;

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
	private GroupBox groupBox2;
	private CheckBox chkQuitToMainMenu;
	private Button btnBlazorTest;
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
		ClientSettings.Instance.QuitToMainMenu = chkQuitToMainMenu.Checked;
		DIRoot.Gui.SaveClientSettings();
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
		sldMaster = new TrackBar();
		label7 = new Label();
		groupBox1 = new GroupBox();
		label2 = new Label();
		sldEffects = new TrackBar();
		label1 = new Label();
		sldMusic = new TrackBar();
		btnCancel = new Controls.GameButton();
		btnSave = new Controls.GameButton();
		btnSE4 = new Controls.GameButton();
		grpPlayerInfo = new GroupBox();
		txtWebsite = new TextBox();
		label10 = new Label();
		txtNotes = new TextBox();
		txtDiscord = new TextBox();
		txtIrc = new TextBox();
		txtEmail = new TextBox();
		txtPbw = new TextBox();
		txtName = new TextBox();
		label9 = new Label();
		label8 = new Label();
		label6 = new Label();
		label5 = new Label();
		label4 = new Label();
		label3 = new Label();
		groupBox2 = new GroupBox();
		chkQuitToMainMenu = new CheckBox();
		btnBlazorTest = new Button();
		((System.ComponentModel.ISupportInitialize)sldMaster).BeginInit();
		groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)sldEffects).BeginInit();
		((System.ComponentModel.ISupportInitialize)sldMusic).BeginInit();
		grpPlayerInfo.SuspendLayout();
		groupBox2.SuspendLayout();
		SuspendLayout();
		// 
		// sldMaster
		// 
		sldMaster.LargeChange = 10;
		sldMaster.Location = new System.Drawing.Point(140, 19);
		sldMaster.Maximum = 100;
		sldMaster.Name = "sldMaster";
		sldMaster.Size = new System.Drawing.Size(213, 45);
		sldMaster.TabIndex = 0;
		sldMaster.TickFrequency = 10;
		// 
		// label7
		// 
		label7.AutoSize = true;
		label7.ForeColor = System.Drawing.Color.CornflowerBlue;
		label7.Location = new System.Drawing.Point(6, 19);
		label7.Margin = new Padding(3);
		label7.Name = "label7";
		label7.Size = new System.Drawing.Size(43, 15);
		label7.TabIndex = 17;
		label7.Text = "Master";
		// 
		// groupBox1
		// 
		groupBox1.Controls.Add(label2);
		groupBox1.Controls.Add(sldEffects);
		groupBox1.Controls.Add(label1);
		groupBox1.Controls.Add(sldMusic);
		groupBox1.Controls.Add(label7);
		groupBox1.Controls.Add(sldMaster);
		groupBox1.ForeColor = System.Drawing.Color.CornflowerBlue;
		groupBox1.Location = new System.Drawing.Point(12, 319);
		groupBox1.Name = "groupBox1";
		groupBox1.Size = new System.Drawing.Size(359, 176);
		groupBox1.TabIndex = 2;
		groupBox1.TabStop = false;
		groupBox1.Text = "Volume";
		// 
		// label2
		// 
		label2.AutoSize = true;
		label2.ForeColor = System.Drawing.Color.CornflowerBlue;
		label2.Location = new System.Drawing.Point(6, 121);
		label2.Margin = new Padding(3);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(42, 15);
		label2.TabIndex = 21;
		label2.Text = "Effects";
		// 
		// sldEffects
		// 
		sldEffects.Enabled = false;
		sldEffects.LargeChange = 10;
		sldEffects.Location = new System.Drawing.Point(140, 121);
		sldEffects.Maximum = 100;
		sldEffects.Name = "sldEffects";
		sldEffects.Size = new System.Drawing.Size(213, 45);
		sldEffects.TabIndex = 2;
		sldEffects.TickFrequency = 10;
		// 
		// label1
		// 
		label1.AutoSize = true;
		label1.ForeColor = System.Drawing.Color.CornflowerBlue;
		label1.Location = new System.Drawing.Point(6, 70);
		label1.Margin = new Padding(3);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(39, 15);
		label1.TabIndex = 19;
		label1.Text = "Music";
		// 
		// sldMusic
		// 
		sldMusic.LargeChange = 10;
		sldMusic.Location = new System.Drawing.Point(140, 70);
		sldMusic.Maximum = 100;
		sldMusic.Name = "sldMusic";
		sldMusic.Size = new System.Drawing.Size(213, 45);
		sldMusic.TabIndex = 1;
		sldMusic.TickFrequency = 10;
		// 
		// btnCancel
		// 
		btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		btnCancel.BackColor = System.Drawing.Color.Black;
		btnCancel.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnCancel.Location = new System.Drawing.Point(295, 585);
		btnCancel.Name = "btnCancel";
		btnCancel.Size = new System.Drawing.Size(75, 23);
		btnCancel.TabIndex = 5;
		btnCancel.Text = "Cancel";
		btnCancel.UseVisualStyleBackColor = false;
		btnCancel.Click += btnCancel_Click;
		// 
		// btnSave
		// 
		btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		btnSave.BackColor = System.Drawing.Color.Black;
		btnSave.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnSave.Location = new System.Drawing.Point(214, 585);
		btnSave.Name = "btnSave";
		btnSave.Size = new System.Drawing.Size(75, 23);
		btnSave.TabIndex = 4;
		btnSave.Text = "Save";
		btnSave.UseVisualStyleBackColor = false;
		btnSave.Click += btnSave_Click;
		// 
		// btnSE4
		// 
		btnSE4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		btnSE4.BackColor = System.Drawing.Color.Black;
		btnSE4.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnSE4.Location = new System.Drawing.Point(12, 578);
		btnSE4.Name = "btnSE4";
		btnSE4.Size = new System.Drawing.Size(104, 30);
		btnSE4.TabIndex = 3;
		btnSE4.Text = "Copy SE4 Assets";
		btnSE4.UseVisualStyleBackColor = false;
		btnSE4.Click += btnSE4_Click;
		// 
		// grpPlayerInfo
		// 
		grpPlayerInfo.Controls.Add(btnBlazorTest);
		grpPlayerInfo.Controls.Add(txtWebsite);
		grpPlayerInfo.Controls.Add(label10);
		grpPlayerInfo.Controls.Add(txtNotes);
		grpPlayerInfo.Controls.Add(txtDiscord);
		grpPlayerInfo.Controls.Add(txtIrc);
		grpPlayerInfo.Controls.Add(txtEmail);
		grpPlayerInfo.Controls.Add(txtPbw);
		grpPlayerInfo.Controls.Add(txtName);
		grpPlayerInfo.Controls.Add(label9);
		grpPlayerInfo.Controls.Add(label8);
		grpPlayerInfo.Controls.Add(label6);
		grpPlayerInfo.Controls.Add(label5);
		grpPlayerInfo.Controls.Add(label4);
		grpPlayerInfo.Controls.Add(label3);
		grpPlayerInfo.ForeColor = System.Drawing.Color.CornflowerBlue;
		grpPlayerInfo.Location = new System.Drawing.Point(12, 12);
		grpPlayerInfo.Name = "grpPlayerInfo";
		grpPlayerInfo.Size = new System.Drawing.Size(359, 301);
		grpPlayerInfo.TabIndex = 1;
		grpPlayerInfo.TabStop = false;
		grpPlayerInfo.Text = "Player Info";
		// 
		// txtWebsite
		// 
		txtWebsite.Location = new System.Drawing.Point(57, 146);
		txtWebsite.Name = "txtWebsite";
		txtWebsite.Size = new System.Drawing.Size(190, 23);
		txtWebsite.TabIndex = 5;
		// 
		// label10
		// 
		label10.AutoSize = true;
		label10.ForeColor = System.Drawing.Color.CornflowerBlue;
		label10.Location = new System.Drawing.Point(7, 149);
		label10.Margin = new Padding(3);
		label10.Name = "label10";
		label10.Size = new System.Drawing.Size(49, 15);
		label10.TabIndex = 30;
		label10.Text = "Website";
		// 
		// txtNotes
		// 
		txtNotes.Location = new System.Drawing.Point(57, 174);
		txtNotes.Multiline = true;
		txtNotes.Name = "txtNotes";
		txtNotes.Size = new System.Drawing.Size(296, 98);
		txtNotes.TabIndex = 6;
		// 
		// txtDiscord
		// 
		txtDiscord.Location = new System.Drawing.Point(57, 120);
		txtDiscord.Name = "txtDiscord";
		txtDiscord.Size = new System.Drawing.Size(190, 23);
		txtDiscord.TabIndex = 4;
		// 
		// txtIrc
		// 
		txtIrc.Location = new System.Drawing.Point(57, 94);
		txtIrc.Name = "txtIrc";
		txtIrc.Size = new System.Drawing.Size(190, 23);
		txtIrc.TabIndex = 3;
		// 
		// txtEmail
		// 
		txtEmail.Location = new System.Drawing.Point(57, 68);
		txtEmail.Name = "txtEmail";
		txtEmail.Size = new System.Drawing.Size(190, 23);
		txtEmail.TabIndex = 2;
		// 
		// txtPbw
		// 
		txtPbw.Location = new System.Drawing.Point(57, 42);
		txtPbw.Name = "txtPbw";
		txtPbw.Size = new System.Drawing.Size(190, 23);
		txtPbw.TabIndex = 1;
		// 
		// txtName
		// 
		txtName.Location = new System.Drawing.Point(57, 16);
		txtName.Name = "txtName";
		txtName.Size = new System.Drawing.Size(190, 23);
		txtName.TabIndex = 0;
		// 
		// label9
		// 
		label9.AutoSize = true;
		label9.ForeColor = System.Drawing.Color.CornflowerBlue;
		label9.Location = new System.Drawing.Point(7, 174);
		label9.Margin = new Padding(3);
		label9.Name = "label9";
		label9.Size = new System.Drawing.Size(38, 15);
		label9.TabIndex = 23;
		label9.Text = "Notes";
		// 
		// label8
		// 
		label8.AutoSize = true;
		label8.ForeColor = System.Drawing.Color.CornflowerBlue;
		label8.Location = new System.Drawing.Point(6, 123);
		label8.Margin = new Padding(3);
		label8.Name = "label8";
		label8.Size = new System.Drawing.Size(47, 15);
		label8.TabIndex = 22;
		label8.Text = "Discord";
		// 
		// label6
		// 
		label6.AutoSize = true;
		label6.ForeColor = System.Drawing.Color.CornflowerBlue;
		label6.Location = new System.Drawing.Point(7, 97);
		label6.Margin = new Padding(3);
		label6.Name = "label6";
		label6.Size = new System.Drawing.Size(25, 15);
		label6.TabIndex = 21;
		label6.Text = "IRC";
		// 
		// label5
		// 
		label5.AutoSize = true;
		label5.ForeColor = System.Drawing.Color.CornflowerBlue;
		label5.Location = new System.Drawing.Point(6, 71);
		label5.Margin = new Padding(3);
		label5.Name = "label5";
		label5.Size = new System.Drawing.Size(36, 15);
		label5.TabIndex = 20;
		label5.Text = "Email";
		// 
		// label4
		// 
		label4.AutoSize = true;
		label4.ForeColor = System.Drawing.Color.CornflowerBlue;
		label4.Location = new System.Drawing.Point(7, 45);
		label4.Margin = new Padding(3);
		label4.Name = "label4";
		label4.Size = new System.Drawing.Size(32, 15);
		label4.TabIndex = 19;
		label4.Text = "PBW";
		// 
		// label3
		// 
		label3.AutoSize = true;
		label3.ForeColor = System.Drawing.Color.CornflowerBlue;
		label3.Location = new System.Drawing.Point(7, 19);
		label3.Margin = new Padding(3);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(39, 15);
		label3.TabIndex = 18;
		label3.Text = "Name";
		// 
		// groupBox2
		// 
		groupBox2.Controls.Add(chkQuitToMainMenu);
		groupBox2.ForeColor = System.Drawing.Color.CornflowerBlue;
		groupBox2.Location = new System.Drawing.Point(12, 501);
		groupBox2.Name = "groupBox2";
		groupBox2.Size = new System.Drawing.Size(359, 57);
		groupBox2.TabIndex = 22;
		groupBox2.TabStop = false;
		groupBox2.Text = "UI";
		groupBox2.Enter += groupBox2_Enter;
		// 
		// chkQuitToMainMenu
		// 
		chkQuitToMainMenu.AutoSize = true;
		chkQuitToMainMenu.Location = new System.Drawing.Point(15, 24);
		chkQuitToMainMenu.Name = "chkQuitToMainMenu";
		chkQuitToMainMenu.Size = new System.Drawing.Size(127, 19);
		chkQuitToMainMenu.TabIndex = 0;
		chkQuitToMainMenu.Text = "Quit to Main Menu";
		chkQuitToMainMenu.UseVisualStyleBackColor = true;
		// 
		// btnBlazorTest
		// 
		btnBlazorTest.Location = new System.Drawing.Point(268, 67);
		btnBlazorTest.Name = "btnBlazorTest";
		btnBlazorTest.Size = new System.Drawing.Size(75, 23);
		btnBlazorTest.TabIndex = 31;
		btnBlazorTest.Text = "blazor test";
		btnBlazorTest.UseVisualStyleBackColor = true;
		btnBlazorTest.Click += btnBlazorTest_Click;
		// 
		// OptionsForm
		// 
		BackColor = System.Drawing.Color.Black;
		ClientSize = new System.Drawing.Size(382, 620);
		Controls.Add(groupBox2);
		Controls.Add(grpPlayerInfo);
		Controls.Add(btnSE4);
		Controls.Add(btnCancel);
		Controls.Add(btnSave);
		Controls.Add(groupBox1);
		MaximizeBox = false;
		Name = "OptionsForm";
		Text = "Options";
		Load += OptionsForm_Load;
		((System.ComponentModel.ISupportInitialize)sldMaster).EndInit();
		groupBox1.ResumeLayout(false);
		groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)sldEffects).EndInit();
		((System.ComponentModel.ISupportInitialize)sldMusic).EndInit();
		grpPlayerInfo.ResumeLayout(false);
		grpPlayerInfo.PerformLayout();
		groupBox2.ResumeLayout(false);
		groupBox2.PerformLayout();
		ResumeLayout(false);
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
		chkQuitToMainMenu.Checked = ClientSettings.Instance.QuitToMainMenu;
	}

	private void groupBox2_Enter(object sender, EventArgs e)
	{

	}

	private void btnBlazorTest_Click(object sender, EventArgs e)
	{
		this.ShowChildForm(new BlazorTestForm());
	}
}
