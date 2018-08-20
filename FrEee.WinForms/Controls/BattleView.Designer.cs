namespace FrEee.WinForms.Controls
{
	partial class BattleView
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
			this.components = new System.ComponentModel.Container();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.roundTimer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// toolTip
			// 
			this.toolTip.AutomaticDelay = 10;
			this.toolTip.AutoPopDelay = 9999999;
			this.toolTip.InitialDelay = 10;
			this.toolTip.ReshowDelay = 99999999;
			this.toolTip.UseAnimation = false;
			// 
			// roundTimer
			// 
			this.roundTimer.Enabled = true;
			this.roundTimer.Interval = 250;
			this.roundTimer.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// BattleView
			// 
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BattleView_MouseDown);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.Timer roundTimer;
	}
}
