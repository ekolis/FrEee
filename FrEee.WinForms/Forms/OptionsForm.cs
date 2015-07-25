using FrEee.WinForms.Objects;
using System.Windows.Forms;
using System;

namespace FrEee.WinForms.Forms
{
  public partial class OptionsForm : Form
  {
    private Label label7;
    private GroupBox groupBox1;
    private Label label2;
    private TrackBar sldEffects;
    private Label label1;
    private TrackBar sldMusic;
    private Controls.GameButton btnCancel;
    private Controls.GameButton btnSave;
    private TrackBar sldMaster;
  
    public OptionsForm()
    {
      InitializeComponent();
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
      this.groupBox1.Size = new System.Drawing.Size(359, 202);
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
      this.sldEffects.TabIndex = 20;
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
      this.sldMusic.TabIndex = 18;
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
      this.btnCancel.TabIndex = 21;
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
      this.btnSave.TabIndex = 20;
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = false;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // OptionsForm
      // 
      this.BackColor = System.Drawing.Color.Black;
      this.ClientSize = new System.Drawing.Size(383, 307);
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

    private void btnCancel_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void OptionsForm_Load(object sender, EventArgs e) {
      sldMaster.Value = Math.Max(0, Math.Min(100, ClientSettings.Instance.masterVolume));
      sldMusic.Value = Math.Max(0, Math.Min(100, ClientSettings.Instance.musicVolume));
      sldEffects.Value = Math.Max(0, Math.Min(100, ClientSettings.Instance.effectsVolume));
    }
  }
}