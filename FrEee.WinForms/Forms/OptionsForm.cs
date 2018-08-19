using FrEee.WinForms.Objects;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
    public partial class OptionsForm : Form
    {
        #region Private Fields

        private Controls.GameButton btnCancel;
        private Controls.GameButton btnSave;
        private Controls.GameButton btnSE4;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label7;
        private TrackBar sldEffects;
        private TrackBar sldMaster;
        private TrackBar sldMusic;

        #endregion Private Fields

        #region Public Constructors

        public OptionsForm()
        {
            InitializeComponent();
        }

        #endregion Public Constructors

        #region Private Methods

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ClientSettings.Instance.masterVolume = sldMaster.Value;
            ClientSettings.Instance.musicVolume = sldMusic.Value;
            ClientSettings.Instance.effectsVolume = sldEffects.Value;
            ClientSettings.Save();
            Music.setVolume(ClientSettings.Instance.masterVolume * ClientSettings.Instance.musicVolume * 1.0e-4f);
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
            ((System.ComponentModel.ISupportInitialize)(this.sldMaster)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sldEffects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sldMusic)).BeginInit();
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
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(359, 176);
            this.groupBox1.TabIndex = 18;
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
            this.btnCancel.Location = new System.Drawing.Point(296, 272);
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
            this.btnSave.Location = new System.Drawing.Point(215, 272);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // btnSE4
            //
            this.btnSE4.BackColor = System.Drawing.Color.Black;
            this.btnSE4.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.btnSE4.Location = new System.Drawing.Point(13, 195);
            this.btnSE4.Name = "btnSE4";
            this.btnSE4.Size = new System.Drawing.Size(104, 30);
            this.btnSE4.TabIndex = 3;
            this.btnSE4.Text = "Copy SE4 Assets";
            this.btnSE4.UseVisualStyleBackColor = false;
            this.btnSE4.Click += new System.EventHandler(this.btnSE4_Click);
            //
            // OptionsForm
            //
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(383, 307);
            this.Controls.Add(this.btnSE4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "OptionsForm";
            this.Text = "Options";
            this.Load += new System.EventHandler(this.OptionsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sldMaster)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sldEffects)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sldMusic)).EndInit();
            this.ResumeLayout(false);
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            sldMaster.Value = Math.Max(0, Math.Min(100, ClientSettings.Instance.masterVolume));
            sldMusic.Value = Math.Max(0, Math.Min(100, ClientSettings.Instance.musicVolume));
            sldEffects.Value = Math.Max(0, Math.Min(100, ClientSettings.Instance.effectsVolume));
        }

        #endregion Private Methods
    }
}
