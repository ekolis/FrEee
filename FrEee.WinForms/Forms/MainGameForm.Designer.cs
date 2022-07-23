using FrEee.WinForms.Controls;

namespace FrEee.WinForms.Forms
{
	partial class MainGameForm
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
			FrEee.WinForms.Objects.GalaxyViewModes.PresenceMode presenceMode1 = new FrEee.WinForms.Objects.GalaxyViewModes.PresenceMode();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.btnWaypoint = new FrEee.WinForms.Controls.GameButton();
			this.btnMovementLog = new FrEee.WinForms.Controls.GameButton();
			this.btnDecloak = new FrEee.WinForms.Controls.GameButton();
			this.btnCloak = new FrEee.WinForms.Controls.GameButton();
			this.btnActivate = new FrEee.WinForms.Controls.GameButton();
			this.btnToggleMinister = new FrEee.WinForms.Controls.GameButton();
			this.btnRename = new FrEee.WinForms.Controls.GameButton();
			this.btnRecycle = new FrEee.WinForms.Controls.GameButton();
			this.btnRepeatOrders = new FrEee.WinForms.Controls.GameButton();
			this.btnNextIdle = new FrEee.WinForms.Controls.GameButton();
			this.btnRepair = new FrEee.WinForms.Controls.GameButton();
			this.btnPrevIdle = new FrEee.WinForms.Controls.GameButton();
			this.btnResupply = new FrEee.WinForms.Controls.GameButton();
			this.btnSentry = new FrEee.WinForms.Controls.GameButton();
			this.btnClearOrders = new FrEee.WinForms.Controls.GameButton();
			this.btnFleetTransfer = new FrEee.WinForms.Controls.GameButton();
			this.btnTransferCargo = new FrEee.WinForms.Controls.GameButton();
			this.btnConstructionQueue = new FrEee.WinForms.Controls.GameButton();
			this.btnColonize = new FrEee.WinForms.Controls.GameButton();
			this.btnEvade = new FrEee.WinForms.Controls.GameButton();
			this.btnWarp = new FrEee.WinForms.Controls.GameButton();
			this.btnPursue = new FrEee.WinForms.Controls.GameButton();
			this.btnMove = new FrEee.WinForms.Controls.GameButton();
			this.btnEndTurn = new FrEee.WinForms.Controls.GameButton();
			this.btnLog = new FrEee.WinForms.Controls.GameButton();
			this.btnQueues = new FrEee.WinForms.Controls.GameButton();
			this.btnShips = new FrEee.WinForms.Controls.GameButton();
			this.btnEmpires = new FrEee.WinForms.Controls.GameButton();
			this.btnPlanets = new FrEee.WinForms.Controls.GameButton();
			this.btnDesigns = new FrEee.WinForms.Controls.GameButton();
			this.btnMenu = new FrEee.WinForms.Controls.GameButton();
			this.progResearch = new FrEee.WinForms.Controls.GameProgressBar();
			this.btnCommands = new FrEee.WinForms.Controls.GameButton();
			this.pnlLayout = new FrEee.WinForms.Controls.GamePanel();
			this.pnlLeft = new System.Windows.Forms.Panel();
			this.pnlSystemMap = new FrEee.WinForms.Controls.GamePanel();
			this.starSystemView = new FrEee.WinForms.Controls.StarSystemView();
			this.pnlSearch = new FrEee.WinForms.Controls.GamePanel();
			this.searchBox = new FrEee.WinForms.Controls.SearchBox();
			this.pnlSystemTabs = new FrEee.WinForms.Controls.GamePanel();
			this.pnlTabs = new System.Windows.Forms.FlowLayoutPanel();
			this.btnNewTab = new FrEee.WinForms.Controls.GameButton();
			this.pnlSubCommands = new FrEee.WinForms.Controls.GamePanel();
			this.pnlMainCommands = new FrEee.WinForms.Controls.GamePanel();
			this.pnlHeader = new FrEee.WinForms.Controls.GamePanel();
			this.resInt = new FrEee.WinForms.Controls.ResourceDisplay();
			this.resRes = new FrEee.WinForms.Controls.ResourceDisplay();
			this.resRad = new FrEee.WinForms.Controls.ResourceDisplay();
			this.resOrg = new FrEee.WinForms.Controls.ResourceDisplay();
			this.resMin = new FrEee.WinForms.Controls.ResourceDisplay();
			this.picEmpireFlag = new System.Windows.Forms.PictureBox();
			this.pnlRight = new System.Windows.Forms.Panel();
			this.ddlGalaxyViewMode = new System.Windows.Forms.ComboBox();
			this.pnlGalaxyMap = new FrEee.WinForms.Controls.GamePanel();
			this.galaxyView = new FrEee.WinForms.Controls.GalaxyView();
			this.pnlDetailReport = new FrEee.WinForms.Controls.GamePanel();
			this.pnlLayout.SuspendLayout();
			this.pnlLeft.SuspendLayout();
			this.pnlSystemMap.SuspendLayout();
			this.pnlSearch.SuspendLayout();
			this.pnlSystemTabs.SuspendLayout();
			this.pnlTabs.SuspendLayout();
			this.pnlSubCommands.SuspendLayout();
			this.pnlMainCommands.SuspendLayout();
			this.pnlHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picEmpireFlag)).BeginInit();
			this.pnlRight.SuspendLayout();
			this.pnlGalaxyMap.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnWaypoint
			// 
			this.btnWaypoint.BackColor = System.Drawing.Color.Black;
			this.btnWaypoint.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnWaypoint.Location = new System.Drawing.Point(49, 6);
			this.btnWaypoint.Name = "btnWaypoint";
			this.btnWaypoint.Size = new System.Drawing.Size(36, 36);
			this.btnWaypoint.TabIndex = 23;
			this.btnWaypoint.TabStop = false;
			this.btnWaypoint.Text = "Way";
			this.toolTip.SetToolTip(this.btnWaypoint, "(Ctrl-W) Move to Waypoint");
			this.btnWaypoint.UseVisualStyleBackColor = false;
			// 
			// btnMovementLog
			// 
			this.btnMovementLog.BackColor = System.Drawing.Color.Black;
			this.btnMovementLog.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnMovementLog.Location = new System.Drawing.Point(427, 48);
			this.btnMovementLog.Name = "btnMovementLog";
			this.btnMovementLog.Size = new System.Drawing.Size(36, 36);
			this.btnMovementLog.TabIndex = 22;
			this.btnMovementLog.TabStop = false;
			this.btnMovementLog.Text = "ML";
			this.toolTip.SetToolTip(this.btnMovementLog, "(Ctrl-L) Movement Log");
			this.btnMovementLog.UseVisualStyleBackColor = false;
			// 
			// btnDecloak
			// 
			this.btnDecloak.BackColor = System.Drawing.Color.Black;
			this.btnDecloak.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnDecloak.Location = new System.Drawing.Point(301, 49);
			this.btnDecloak.Name = "btnDecloak";
			this.btnDecloak.Size = new System.Drawing.Size(36, 36);
			this.btnDecloak.TabIndex = 21;
			this.btnDecloak.TabStop = false;
			this.btnDecloak.Text = "Dclk";
			this.toolTip.SetToolTip(this.btnDecloak, "(X) Decloak");
			this.btnDecloak.UseVisualStyleBackColor = false;
			// 
			// btnCloak
			// 
			this.btnCloak.BackColor = System.Drawing.Color.Black;
			this.btnCloak.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnCloak.Location = new System.Drawing.Point(301, 7);
			this.btnCloak.Name = "btnCloak";
			this.btnCloak.Size = new System.Drawing.Size(36, 36);
			this.btnCloak.TabIndex = 20;
			this.btnCloak.TabStop = false;
			this.btnCloak.Text = "Clk";
			this.toolTip.SetToolTip(this.btnCloak, "(Z) Cloak");
			this.btnCloak.UseVisualStyleBackColor = false;
			// 
			// btnActivate
			// 
			this.btnActivate.BackColor = System.Drawing.Color.Black;
			this.btnActivate.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnActivate.Location = new System.Drawing.Point(343, 7);
			this.btnActivate.Name = "btnActivate";
			this.btnActivate.Size = new System.Drawing.Size(36, 36);
			this.btnActivate.TabIndex = 19;
			this.btnActivate.TabStop = false;
			this.btnActivate.Text = "Act";
			this.toolTip.SetToolTip(this.btnActivate, "(Ctrl-A) Activate Ability");
			this.btnActivate.UseVisualStyleBackColor = false;
			this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
			// 
			// btnToggleMinister
			// 
			this.btnToggleMinister.BackColor = System.Drawing.Color.Black;
			this.btnToggleMinister.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnToggleMinister.Location = new System.Drawing.Point(385, 49);
			this.btnToggleMinister.Name = "btnToggleMinister";
			this.btnToggleMinister.Size = new System.Drawing.Size(36, 36);
			this.btnToggleMinister.TabIndex = 18;
			this.btnToggleMinister.TabStop = false;
			this.btnToggleMinister.Text = "Min";
			this.toolTip.SetToolTip(this.btnToggleMinister, "(T) Toggle Minister Control");
			this.btnToggleMinister.UseVisualStyleBackColor = false;
			// 
			// btnRename
			// 
			this.btnRename.BackColor = System.Drawing.Color.Black;
			this.btnRename.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnRename.Location = new System.Drawing.Point(385, 7);
			this.btnRename.Name = "btnRename";
			this.btnRename.Size = new System.Drawing.Size(36, 36);
			this.btnRename.TabIndex = 17;
			this.btnRename.TabStop = false;
			this.btnRename.Text = "Nam";
			this.toolTip.SetToolTip(this.btnRename, "(N) Rename/Notes");
			this.btnRename.UseVisualStyleBackColor = false;
			// 
			// btnRecycle
			// 
			this.btnRecycle.BackColor = System.Drawing.Color.Black;
			this.btnRecycle.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnRecycle.Location = new System.Drawing.Point(217, 48);
			this.btnRecycle.Name = "btnRecycle";
			this.btnRecycle.Size = new System.Drawing.Size(36, 36);
			this.btnRecycle.TabIndex = 16;
			this.btnRecycle.TabStop = false;
			this.btnRecycle.Text = "Rcy";
			this.toolTip.SetToolTip(this.btnRecycle, "(Ctrl-R) Recycle/Scuttle");
			this.btnRecycle.UseVisualStyleBackColor = false;
			this.btnRecycle.Click += new System.EventHandler(this.btnRecycle_Click);
			// 
			// btnRepeatOrders
			// 
			this.btnRepeatOrders.BackColor = System.Drawing.Color.Black;
			this.btnRepeatOrders.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnRepeatOrders.Location = new System.Drawing.Point(49, 48);
			this.btnRepeatOrders.Name = "btnRepeatOrders";
			this.btnRepeatOrders.Size = new System.Drawing.Size(36, 36);
			this.btnRepeatOrders.TabIndex = 15;
			this.btnRepeatOrders.TabStop = false;
			this.btnRepeatOrders.Text = "Rept";
			this.toolTip.SetToolTip(this.btnRepeatOrders, "(P) Repeat Orders");
			this.btnRepeatOrders.UseVisualStyleBackColor = false;
			// 
			// btnNextIdle
			// 
			this.btnNextIdle.BackColor = System.Drawing.Color.Black;
			this.btnNextIdle.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnNextIdle.Location = new System.Drawing.Point(469, 7);
			this.btnNextIdle.Name = "btnNextIdle";
			this.btnNextIdle.Size = new System.Drawing.Size(36, 36);
			this.btnNextIdle.TabIndex = 6;
			this.btnNextIdle.TabStop = false;
			this.btnNextIdle.Text = "Next";
			this.toolTip.SetToolTip(this.btnNextIdle, "(Tab) Next Idle Space Object");
			this.btnNextIdle.UseVisualStyleBackColor = false;
			this.btnNextIdle.Click += new System.EventHandler(this.btnNextIdle_Click);
			// 
			// btnRepair
			// 
			this.btnRepair.BackColor = System.Drawing.Color.Black;
			this.btnRepair.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnRepair.Location = new System.Drawing.Point(175, 49);
			this.btnRepair.Name = "btnRepair";
			this.btnRepair.Size = new System.Drawing.Size(36, 36);
			this.btnRepair.TabIndex = 14;
			this.btnRepair.TabStop = false;
			this.btnRepair.Text = "Repr";
			this.toolTip.SetToolTip(this.btnRepair, "(R) Repair");
			this.btnRepair.UseVisualStyleBackColor = false;
			// 
			// btnPrevIdle
			// 
			this.btnPrevIdle.BackColor = System.Drawing.Color.Black;
			this.btnPrevIdle.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnPrevIdle.Location = new System.Drawing.Point(427, 7);
			this.btnPrevIdle.Name = "btnPrevIdle";
			this.btnPrevIdle.Size = new System.Drawing.Size(36, 36);
			this.btnPrevIdle.TabIndex = 5;
			this.btnPrevIdle.TabStop = false;
			this.btnPrevIdle.Text = "Prev";
			this.toolTip.SetToolTip(this.btnPrevIdle, "(Shift-Tab) Previous Idle Space Object");
			this.btnPrevIdle.UseVisualStyleBackColor = false;
			this.btnPrevIdle.Click += new System.EventHandler(this.btnPrevIdle_Click);
			// 
			// btnResupply
			// 
			this.btnResupply.BackColor = System.Drawing.Color.Black;
			this.btnResupply.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnResupply.Location = new System.Drawing.Point(175, 7);
			this.btnResupply.Name = "btnResupply";
			this.btnResupply.Size = new System.Drawing.Size(36, 36);
			this.btnResupply.TabIndex = 13;
			this.btnResupply.TabStop = false;
			this.btnResupply.Text = "Sply";
			this.toolTip.SetToolTip(this.btnResupply, "(S) Resupply");
			this.btnResupply.UseVisualStyleBackColor = false;
			// 
			// btnSentry
			// 
			this.btnSentry.BackColor = System.Drawing.Color.Black;
			this.btnSentry.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnSentry.Location = new System.Drawing.Point(133, 49);
			this.btnSentry.Name = "btnSentry";
			this.btnSentry.Size = new System.Drawing.Size(36, 36);
			this.btnSentry.TabIndex = 12;
			this.btnSentry.TabStop = false;
			this.btnSentry.Text = "Stry";
			this.toolTip.SetToolTip(this.btnSentry, "(Y) Sentry");
			this.btnSentry.UseVisualStyleBackColor = false;
			this.btnSentry.Click += new System.EventHandler(this.btnSentry_Click);
			// 
			// btnClearOrders
			// 
			this.btnClearOrders.BackColor = System.Drawing.Color.Black;
			this.btnClearOrders.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnClearOrders.Location = new System.Drawing.Point(343, 49);
			this.btnClearOrders.Name = "btnClearOrders";
			this.btnClearOrders.Size = new System.Drawing.Size(36, 36);
			this.btnClearOrders.TabIndex = 11;
			this.btnClearOrders.TabStop = false;
			this.btnClearOrders.Text = "Clr";
			this.toolTip.SetToolTip(this.btnClearOrders, "(Bksp) Clear Orders");
			this.btnClearOrders.UseVisualStyleBackColor = false;
			this.btnClearOrders.Click += new System.EventHandler(this.btnClearOrders_Click);
			// 
			// btnFleetTransfer
			// 
			this.btnFleetTransfer.BackColor = System.Drawing.Color.Black;
			this.btnFleetTransfer.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnFleetTransfer.Location = new System.Drawing.Point(259, 49);
			this.btnFleetTransfer.Name = "btnFleetTransfer";
			this.btnFleetTransfer.Size = new System.Drawing.Size(36, 36);
			this.btnFleetTransfer.TabIndex = 10;
			this.btnFleetTransfer.TabStop = false;
			this.btnFleetTransfer.Text = "Flt";
			this.toolTip.SetToolTip(this.btnFleetTransfer, "(F) Fleet Transfer");
			this.btnFleetTransfer.UseVisualStyleBackColor = false;
			this.btnFleetTransfer.Click += new System.EventHandler(this.btnFleetTransfer_Click);
			// 
			// btnTransferCargo
			// 
			this.btnTransferCargo.BackColor = System.Drawing.Color.Black;
			this.btnTransferCargo.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnTransferCargo.Location = new System.Drawing.Point(259, 7);
			this.btnTransferCargo.Name = "btnTransferCargo";
			this.btnTransferCargo.Size = new System.Drawing.Size(36, 36);
			this.btnTransferCargo.TabIndex = 9;
			this.btnTransferCargo.TabStop = false;
			this.btnTransferCargo.Text = "TC";
			this.toolTip.SetToolTip(this.btnTransferCargo, "(T) Transfer Cargo");
			this.btnTransferCargo.UseVisualStyleBackColor = false;
			this.btnTransferCargo.Click += new System.EventHandler(this.btnTransferCargo_Click);
			// 
			// btnConstructionQueue
			// 
			this.btnConstructionQueue.BackColor = System.Drawing.Color.Black;
			this.btnConstructionQueue.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnConstructionQueue.Location = new System.Drawing.Point(217, 7);
			this.btnConstructionQueue.Name = "btnConstructionQueue";
			this.btnConstructionQueue.Size = new System.Drawing.Size(36, 36);
			this.btnConstructionQueue.TabIndex = 8;
			this.btnConstructionQueue.TabStop = false;
			this.btnConstructionQueue.Text = "CQ";
			this.toolTip.SetToolTip(this.btnConstructionQueue, "(Q) Construction Queue");
			this.btnConstructionQueue.UseVisualStyleBackColor = false;
			this.btnConstructionQueue.Click += new System.EventHandler(this.btnConstructionQueue_Click);
			// 
			// btnColonize
			// 
			this.btnColonize.BackColor = System.Drawing.Color.Black;
			this.btnColonize.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnColonize.Location = new System.Drawing.Point(133, 7);
			this.btnColonize.Name = "btnColonize";
			this.btnColonize.Size = new System.Drawing.Size(36, 36);
			this.btnColonize.TabIndex = 7;
			this.btnColonize.TabStop = false;
			this.btnColonize.Text = "Col";
			this.toolTip.SetToolTip(this.btnColonize, "(C) Colonize");
			this.btnColonize.UseVisualStyleBackColor = false;
			this.btnColonize.Click += new System.EventHandler(this.btnColonize_Click);
			// 
			// btnEvade
			// 
			this.btnEvade.BackColor = System.Drawing.Color.Black;
			this.btnEvade.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnEvade.Location = new System.Drawing.Point(91, 47);
			this.btnEvade.Name = "btnEvade";
			this.btnEvade.Size = new System.Drawing.Size(36, 36);
			this.btnEvade.TabIndex = 6;
			this.btnEvade.TabStop = false;
			this.btnEvade.Text = "Ev";
			this.toolTip.SetToolTip(this.btnEvade, "(E) Evade");
			this.btnEvade.UseVisualStyleBackColor = false;
			this.btnEvade.Click += new System.EventHandler(this.btnEvade_Click);
			// 
			// btnWarp
			// 
			this.btnWarp.BackColor = System.Drawing.Color.Black;
			this.btnWarp.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnWarp.Location = new System.Drawing.Point(7, 48);
			this.btnWarp.Name = "btnWarp";
			this.btnWarp.Size = new System.Drawing.Size(36, 36);
			this.btnWarp.TabIndex = 5;
			this.btnWarp.TabStop = false;
			this.btnWarp.Text = "Wp";
			this.toolTip.SetToolTip(this.btnWarp, "(W) Warp");
			this.btnWarp.UseVisualStyleBackColor = false;
			this.btnWarp.Click += new System.EventHandler(this.btnWarp_Click);
			// 
			// btnPursue
			// 
			this.btnPursue.BackColor = System.Drawing.Color.Black;
			this.btnPursue.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnPursue.Location = new System.Drawing.Point(91, 6);
			this.btnPursue.Name = "btnPursue";
			this.btnPursue.Size = new System.Drawing.Size(36, 36);
			this.btnPursue.TabIndex = 4;
			this.btnPursue.TabStop = false;
			this.btnPursue.Text = "Pur";
			this.toolTip.SetToolTip(this.btnPursue, "(A) Attack / Pursue");
			this.btnPursue.UseVisualStyleBackColor = false;
			this.btnPursue.Click += new System.EventHandler(this.btnPursue_Click);
			// 
			// btnMove
			// 
			this.btnMove.BackColor = System.Drawing.Color.Black;
			this.btnMove.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnMove.Location = new System.Drawing.Point(7, 6);
			this.btnMove.Name = "btnMove";
			this.btnMove.Size = new System.Drawing.Size(36, 36);
			this.btnMove.TabIndex = 2;
			this.btnMove.TabStop = false;
			this.btnMove.Text = "Mov";
			this.toolTip.SetToolTip(this.btnMove, "(M) Move");
			this.btnMove.UseVisualStyleBackColor = false;
			this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
			// 
			// btnEndTurn
			// 
			this.btnEndTurn.BackColor = System.Drawing.Color.Black;
			this.btnEndTurn.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnEndTurn.Location = new System.Drawing.Point(296, 6);
			this.btnEndTurn.Name = "btnEndTurn";
			this.btnEndTurn.Size = new System.Drawing.Size(36, 36);
			this.btnEndTurn.TabIndex = 7;
			this.btnEndTurn.TabStop = false;
			this.toolTip.SetToolTip(this.btnEndTurn, "(F12) Save Commands");
			this.btnEndTurn.UseVisualStyleBackColor = false;
			this.btnEndTurn.Click += new System.EventHandler(this.btnEndTurn_Click);
			// 
			// btnLog
			// 
			this.btnLog.BackColor = System.Drawing.Color.Black;
			this.btnLog.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnLog.Location = new System.Drawing.Point(256, 6);
			this.btnLog.Name = "btnLog";
			this.btnLog.Size = new System.Drawing.Size(36, 36);
			this.btnLog.TabIndex = 6;
			this.btnLog.TabStop = false;
			this.toolTip.SetToolTip(this.btnLog, "(F10 / Shift-L) Log");
			this.btnLog.UseVisualStyleBackColor = false;
			this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
			// 
			// btnQueues
			// 
			this.btnQueues.BackColor = System.Drawing.Color.Black;
			this.btnQueues.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnQueues.Location = new System.Drawing.Point(214, 6);
			this.btnQueues.Name = "btnQueues";
			this.btnQueues.Size = new System.Drawing.Size(36, 36);
			this.btnQueues.TabIndex = 5;
			this.btnQueues.TabStop = false;
			this.toolTip.SetToolTip(this.btnQueues, "(F7 / Shift-Q) Construction Queues");
			this.btnQueues.UseVisualStyleBackColor = false;
			this.btnQueues.Click += new System.EventHandler(this.btnQueues_Click);
			// 
			// btnShips
			// 
			this.btnShips.BackColor = System.Drawing.Color.Black;
			this.btnShips.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnShips.Location = new System.Drawing.Point(172, 6);
			this.btnShips.Name = "btnShips";
			this.btnShips.Size = new System.Drawing.Size(36, 36);
			this.btnShips.TabIndex = 4;
			this.btnShips.TabStop = false;
			this.toolTip.SetToolTip(this.btnShips, "(F6 / Shift-S) Ships");
			this.btnShips.UseVisualStyleBackColor = false;
			this.btnShips.Click += new System.EventHandler(this.btnShips_Click);
			// 
			// btnEmpires
			// 
			this.btnEmpires.BackColor = System.Drawing.Color.Black;
			this.btnEmpires.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnEmpires.Location = new System.Drawing.Point(130, 6);
			this.btnEmpires.Name = "btnEmpires";
			this.btnEmpires.Size = new System.Drawing.Size(36, 36);
			this.btnEmpires.TabIndex = 3;
			this.btnEmpires.TabStop = false;
			this.toolTip.SetToolTip(this.btnEmpires, "(F9 / Shift-E) Empires");
			this.btnEmpires.UseVisualStyleBackColor = false;
			this.btnEmpires.Click += new System.EventHandler(this.btnEmpires_Click);
			// 
			// btnPlanets
			// 
			this.btnPlanets.BackColor = System.Drawing.Color.Black;
			this.btnPlanets.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnPlanets.Location = new System.Drawing.Point(88, 6);
			this.btnPlanets.Name = "btnPlanets";
			this.btnPlanets.Size = new System.Drawing.Size(36, 36);
			this.btnPlanets.TabIndex = 0;
			this.btnPlanets.TabStop = false;
			this.toolTip.SetToolTip(this.btnPlanets, "(F4 / F5 / Shift-P / Shift-C) Planets/Colonies");
			this.btnPlanets.UseVisualStyleBackColor = false;
			this.btnPlanets.Click += new System.EventHandler(this.btnPlanets_Click);
			// 
			// btnDesigns
			// 
			this.btnDesigns.BackColor = System.Drawing.Color.Black;
			this.btnDesigns.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnDesigns.Location = new System.Drawing.Point(47, 6);
			this.btnDesigns.Name = "btnDesigns";
			this.btnDesigns.Size = new System.Drawing.Size(36, 36);
			this.btnDesigns.TabIndex = 2;
			this.btnDesigns.TabStop = false;
			this.toolTip.SetToolTip(this.btnDesigns, "(F3 / Shift-D) Designs");
			this.btnDesigns.UseVisualStyleBackColor = false;
			this.btnDesigns.Click += new System.EventHandler(this.btnDesigns_Click);
			// 
			// btnMenu
			// 
			this.btnMenu.BackColor = System.Drawing.Color.Black;
			this.btnMenu.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnMenu.Location = new System.Drawing.Point(5, 6);
			this.btnMenu.Name = "btnMenu";
			this.btnMenu.Size = new System.Drawing.Size(36, 36);
			this.btnMenu.TabIndex = 1;
			this.btnMenu.TabStop = false;
			this.toolTip.SetToolTip(this.btnMenu, "(F2) Menu");
			this.btnMenu.UseVisualStyleBackColor = false;
			this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
			// 
			// progResearch
			// 
			this.progResearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progResearch.BackColor = System.Drawing.Color.Black;
			this.progResearch.BarColor = System.Drawing.Color.Magenta;
			this.progResearch.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.progResearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.progResearch.ForeColor = System.Drawing.Color.White;
			this.progResearch.IncrementalProgress = ((long)(0));
			this.progResearch.LeftText = "Ice Planet Colonization";
			this.progResearch.Location = new System.Drawing.Point(3, -3);
			this.progResearch.Margin = new System.Windows.Forms.Padding(0);
			this.progResearch.Maximum = ((long)(500000));
			this.progResearch.Name = "progResearch";
			this.progResearch.Padding = new System.Windows.Forms.Padding(5);
			this.progResearch.ProgressDisplayType = FrEee.WinForms.Controls.ProgressDisplayType.Numeric;
			this.progResearch.RightText = "0.2 years";
			this.progResearch.Size = new System.Drawing.Size(411, 38);
			this.progResearch.TabIndex = 11;
			this.toolTip.SetToolTip(this.progResearch, "(F8 / Shift-R) Research");
			this.progResearch.Value = ((long)(350000));
			this.progResearch.Click += new System.EventHandler(this.progResearch_Click);
			// 
			// btnCommands
			// 
			this.btnCommands.BackColor = System.Drawing.Color.Black;
			this.btnCommands.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnCommands.Location = new System.Drawing.Point(469, 47);
			this.btnCommands.Name = "btnCommands";
			this.btnCommands.Size = new System.Drawing.Size(36, 36);
			this.btnCommands.TabIndex = 24;
			this.btnCommands.TabStop = false;
			this.btnCommands.Text = "Cmd";
			this.toolTip.SetToolTip(this.btnCommands, "View Commands");
			this.btnCommands.UseVisualStyleBackColor = false;
			this.btnCommands.Click += new System.EventHandler(this.btnCommands_Click);
			// 
			// pnlLayout
			// 
			this.pnlLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlLayout.BackColor = System.Drawing.Color.Black;
			this.pnlLayout.BorderColor = System.Drawing.Color.CornflowerBlue;
			this.pnlLayout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlLayout.Controls.Add(this.pnlLeft);
			this.pnlLayout.Controls.Add(this.pnlRight);
			this.pnlLayout.ForeColor = System.Drawing.Color.White;
			this.pnlLayout.Location = new System.Drawing.Point(0, 0);
			this.pnlLayout.Margin = new System.Windows.Forms.Padding(0);
			this.pnlLayout.Name = "pnlLayout";
			this.pnlLayout.Padding = new System.Windows.Forms.Padding(3);
			this.pnlLayout.Size = new System.Drawing.Size(1270, 779);
			this.pnlLayout.TabIndex = 8;
			// 
			// pnlLeft
			// 
			this.pnlLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlLeft.Controls.Add(this.pnlSystemMap);
			this.pnlLeft.Controls.Add(this.pnlSearch);
			this.pnlLeft.Controls.Add(this.pnlSystemTabs);
			this.pnlLeft.Controls.Add(this.pnlSubCommands);
			this.pnlLeft.Controls.Add(this.pnlMainCommands);
			this.pnlLeft.Controls.Add(this.pnlHeader);
			this.pnlLeft.Location = new System.Drawing.Point(1, 0);
			this.pnlLeft.Margin = new System.Windows.Forms.Padding(1, 1, 0, 1);
			this.pnlLeft.Name = "pnlLeft";
			this.pnlLeft.Size = new System.Drawing.Size(857, 777);
			this.pnlLeft.TabIndex = 6;
			// 
			// pnlSystemMap
			// 
			this.pnlSystemMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlSystemMap.BackColor = System.Drawing.Color.Black;
			this.pnlSystemMap.BorderColor = System.Drawing.Color.RoyalBlue;
			this.pnlSystemMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlSystemMap.Controls.Add(this.starSystemView);
			this.pnlSystemMap.ForeColor = System.Drawing.Color.White;
			this.pnlSystemMap.Location = new System.Drawing.Point(145, 134);
			this.pnlSystemMap.Margin = new System.Windows.Forms.Padding(0);
			this.pnlSystemMap.Name = "pnlSystemMap";
			this.pnlSystemMap.Padding = new System.Windows.Forms.Padding(3);
			this.pnlSystemMap.Size = new System.Drawing.Size(712, 642);
			this.pnlSystemMap.TabIndex = 9;
			// 
			// starSystemView
			// 
			this.starSystemView.BackColor = System.Drawing.Color.Black;
			this.starSystemView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.starSystemView.DrawText = true;
			this.starSystemView.Location = new System.Drawing.Point(3, 3);
			this.starSystemView.Name = "starSystemView";
			this.starSystemView.SelectedSector = null;
			this.starSystemView.SelectedSpaceObject = null;
			this.starSystemView.Size = new System.Drawing.Size(704, 634);
			this.starSystemView.StarSystem = null;
			this.starSystemView.TabIndex = 0;
			this.starSystemView.SectorClicked += new FrEee.WinForms.Controls.StarSystemView.SectorSelectionDelegate(this.starSystemView_SectorClicked);
			this.starSystemView.SectorSelected += new FrEee.WinForms.Controls.StarSystemView.SectorSelectionDelegate(this.starSystemView_SectorSelected);
			// 
			// pnlSearch
			// 
			this.pnlSearch.BackColor = System.Drawing.Color.Black;
			this.pnlSearch.BorderColor = System.Drawing.Color.RoyalBlue;
			this.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlSearch.Controls.Add(this.searchBox);
			this.pnlSearch.ForeColor = System.Drawing.Color.White;
			this.pnlSearch.Location = new System.Drawing.Point(-2, 85);
			this.pnlSearch.Margin = new System.Windows.Forms.Padding(2);
			this.pnlSearch.Name = "pnlSearch";
			this.pnlSearch.Padding = new System.Windows.Forms.Padding(3);
			this.pnlSearch.Size = new System.Drawing.Size(342, 49);
			this.pnlSearch.TabIndex = 8;
			// 
			// searchBox
			// 
			this.searchBox.BackColor = System.Drawing.Color.Black;
			this.searchBox.ForeColor = System.Drawing.Color.White;
			this.searchBox.Location = new System.Drawing.Point(12, 8);
			this.searchBox.Margin = new System.Windows.Forms.Padding(6);
			this.searchBox.Name = "searchBox";
			this.searchBox.ObjectsToSearch = null;
			this.searchBox.ResultsPopupHeight = 128;
			this.searchBox.Size = new System.Drawing.Size(320, 31);
			this.searchBox.StarSystem = null;
			this.searchBox.TabIndex = 0;
			this.searchBox.TabStop = false;
			this.searchBox.ObjectSelected += new FrEee.WinForms.Controls.SearchBox.ObjectSelectedDelegate(this.searchBox_ObjectSelected);
			// 
			// pnlSystemTabs
			// 
			this.pnlSystemTabs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.pnlSystemTabs.BackColor = System.Drawing.Color.Black;
			this.pnlSystemTabs.BorderColor = System.Drawing.Color.RoyalBlue;
			this.pnlSystemTabs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlSystemTabs.Controls.Add(this.pnlTabs);
			this.pnlSystemTabs.ForeColor = System.Drawing.Color.White;
			this.pnlSystemTabs.Location = new System.Drawing.Point(0, 134);
			this.pnlSystemTabs.Margin = new System.Windows.Forms.Padding(0);
			this.pnlSystemTabs.Name = "pnlSystemTabs";
			this.pnlSystemTabs.Padding = new System.Windows.Forms.Padding(3);
			this.pnlSystemTabs.Size = new System.Drawing.Size(145, 642);
			this.pnlSystemTabs.TabIndex = 7;
			// 
			// pnlTabs
			// 
			this.pnlTabs.Controls.Add(this.btnNewTab);
			this.pnlTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlTabs.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.pnlTabs.Location = new System.Drawing.Point(3, 3);
			this.pnlTabs.Name = "pnlTabs";
			this.pnlTabs.Size = new System.Drawing.Size(137, 634);
			this.pnlTabs.TabIndex = 0;
			this.pnlTabs.DoubleClick += new System.EventHandler(this.btnNewTab_Click);
			// 
			// btnNewTab
			// 
			this.btnNewTab.BackColor = System.Drawing.Color.Black;
			this.btnNewTab.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnNewTab.Location = new System.Drawing.Point(3, 3);
			this.btnNewTab.Name = "btnNewTab";
			this.btnNewTab.Size = new System.Drawing.Size(128, 23);
			this.btnNewTab.TabIndex = 0;
			this.btnNewTab.TabStop = false;
			this.btnNewTab.Text = "(New Tab)";
			this.btnNewTab.UseVisualStyleBackColor = false;
			this.btnNewTab.Click += new System.EventHandler(this.btnNewTab_Click);
			// 
			// pnlSubCommands
			// 
			this.pnlSubCommands.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlSubCommands.BackColor = System.Drawing.Color.Black;
			this.pnlSubCommands.BorderColor = System.Drawing.Color.RoyalBlue;
			this.pnlSubCommands.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlSubCommands.Controls.Add(this.btnCommands);
			this.pnlSubCommands.Controls.Add(this.btnWaypoint);
			this.pnlSubCommands.Controls.Add(this.btnMovementLog);
			this.pnlSubCommands.Controls.Add(this.btnDecloak);
			this.pnlSubCommands.Controls.Add(this.btnCloak);
			this.pnlSubCommands.Controls.Add(this.btnActivate);
			this.pnlSubCommands.Controls.Add(this.btnToggleMinister);
			this.pnlSubCommands.Controls.Add(this.btnRename);
			this.pnlSubCommands.Controls.Add(this.btnRecycle);
			this.pnlSubCommands.Controls.Add(this.btnRepeatOrders);
			this.pnlSubCommands.Controls.Add(this.btnNextIdle);
			this.pnlSubCommands.Controls.Add(this.btnRepair);
			this.pnlSubCommands.Controls.Add(this.btnPrevIdle);
			this.pnlSubCommands.Controls.Add(this.btnResupply);
			this.pnlSubCommands.Controls.Add(this.btnSentry);
			this.pnlSubCommands.Controls.Add(this.btnClearOrders);
			this.pnlSubCommands.Controls.Add(this.btnFleetTransfer);
			this.pnlSubCommands.Controls.Add(this.btnTransferCargo);
			this.pnlSubCommands.Controls.Add(this.btnConstructionQueue);
			this.pnlSubCommands.Controls.Add(this.btnColonize);
			this.pnlSubCommands.Controls.Add(this.btnEvade);
			this.pnlSubCommands.Controls.Add(this.btnWarp);
			this.pnlSubCommands.Controls.Add(this.btnPursue);
			this.pnlSubCommands.Controls.Add(this.btnMove);
			this.pnlSubCommands.ForeColor = System.Drawing.Color.White;
			this.pnlSubCommands.Location = new System.Drawing.Point(340, 37);
			this.pnlSubCommands.Margin = new System.Windows.Forms.Padding(0);
			this.pnlSubCommands.Name = "pnlSubCommands";
			this.pnlSubCommands.Padding = new System.Windows.Forms.Padding(4);
			this.pnlSubCommands.Size = new System.Drawing.Size(517, 97);
			this.pnlSubCommands.TabIndex = 6;
			// 
			// pnlMainCommands
			// 
			this.pnlMainCommands.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.pnlMainCommands.BackColor = System.Drawing.Color.Black;
			this.pnlMainCommands.BorderColor = System.Drawing.Color.RoyalBlue;
			this.pnlMainCommands.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlMainCommands.Controls.Add(this.btnEndTurn);
			this.pnlMainCommands.Controls.Add(this.btnLog);
			this.pnlMainCommands.Controls.Add(this.btnQueues);
			this.pnlMainCommands.Controls.Add(this.btnShips);
			this.pnlMainCommands.Controls.Add(this.btnEmpires);
			this.pnlMainCommands.Controls.Add(this.btnPlanets);
			this.pnlMainCommands.Controls.Add(this.btnDesigns);
			this.pnlMainCommands.Controls.Add(this.btnMenu);
			this.pnlMainCommands.ForeColor = System.Drawing.Color.White;
			this.pnlMainCommands.Location = new System.Drawing.Point(-2, 37);
			this.pnlMainCommands.Margin = new System.Windows.Forms.Padding(0);
			this.pnlMainCommands.Name = "pnlMainCommands";
			this.pnlMainCommands.Padding = new System.Windows.Forms.Padding(6);
			this.pnlMainCommands.Size = new System.Drawing.Size(342, 50);
			this.pnlMainCommands.TabIndex = 3;
			// 
			// pnlHeader
			// 
			this.pnlHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlHeader.BackColor = System.Drawing.Color.Black;
			this.pnlHeader.BorderColor = System.Drawing.Color.RoyalBlue;
			this.pnlHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlHeader.Controls.Add(this.resInt);
			this.pnlHeader.Controls.Add(this.resRes);
			this.pnlHeader.Controls.Add(this.resRad);
			this.pnlHeader.Controls.Add(this.resOrg);
			this.pnlHeader.Controls.Add(this.resMin);
			this.pnlHeader.Controls.Add(this.picEmpireFlag);
			this.pnlHeader.ForeColor = System.Drawing.Color.White;
			this.pnlHeader.Location = new System.Drawing.Point(-2, 0);
			this.pnlHeader.Margin = new System.Windows.Forms.Padding(2);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Padding = new System.Windows.Forms.Padding(3);
			this.pnlHeader.Size = new System.Drawing.Size(859, 37);
			this.pnlHeader.TabIndex = 5;
			// 
			// resInt
			// 
			this.resInt.Amount = 0;
			this.resInt.BackColor = System.Drawing.Color.Black;
			this.resInt.Change = null;
			this.resInt.ForeColor = System.Drawing.Color.White;
			this.resInt.Location = new System.Drawing.Point(638, 6);
			this.resInt.Margin = new System.Windows.Forms.Padding(0);
			this.resInt.Name = "resInt";
			this.resInt.ResourceName = "Intelligence";
			this.resInt.Size = new System.Drawing.Size(147, 20);
			this.resInt.TabIndex = 14;
			// 
			// resRes
			// 
			this.resRes.Amount = 0;
			this.resRes.BackColor = System.Drawing.Color.Black;
			this.resRes.Change = null;
			this.resRes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.resRes.Location = new System.Drawing.Point(488, 6);
			this.resRes.Margin = new System.Windows.Forms.Padding(0);
			this.resRes.Name = "resRes";
			this.resRes.ResourceName = "Research";
			this.resRes.Size = new System.Drawing.Size(147, 20);
			this.resRes.TabIndex = 13;
			// 
			// resRad
			// 
			this.resRad.Amount = 0;
			this.resRad.BackColor = System.Drawing.Color.Black;
			this.resRad.Change = null;
			this.resRad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.resRad.Location = new System.Drawing.Point(341, 7);
			this.resRad.Margin = new System.Windows.Forms.Padding(0);
			this.resRad.Name = "resRad";
			this.resRad.ResourceName = "Radioactives";
			this.resRad.Size = new System.Drawing.Size(147, 20);
			this.resRad.TabIndex = 12;
			// 
			// resOrg
			// 
			this.resOrg.Amount = 0;
			this.resOrg.BackColor = System.Drawing.Color.Black;
			this.resOrg.Change = null;
			this.resOrg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.resOrg.Location = new System.Drawing.Point(194, 6);
			this.resOrg.Margin = new System.Windows.Forms.Padding(0);
			this.resOrg.Name = "resOrg";
			this.resOrg.ResourceName = "Organics";
			this.resOrg.Size = new System.Drawing.Size(147, 20);
			this.resOrg.TabIndex = 11;
			// 
			// resMin
			// 
			this.resMin.Amount = 0;
			this.resMin.BackColor = System.Drawing.Color.Black;
			this.resMin.Change = null;
			this.resMin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.resMin.Location = new System.Drawing.Point(47, 6);
			this.resMin.Margin = new System.Windows.Forms.Padding(0);
			this.resMin.Name = "resMin";
			this.resMin.ResourceName = "Minerals";
			this.resMin.Size = new System.Drawing.Size(147, 20);
			this.resMin.TabIndex = 10;
			// 
			// picEmpireFlag
			// 
			this.picEmpireFlag.Location = new System.Drawing.Point(4, 5);
			this.picEmpireFlag.Margin = new System.Windows.Forms.Padding(4);
			this.picEmpireFlag.Name = "picEmpireFlag";
			this.picEmpireFlag.Size = new System.Drawing.Size(38, 21);
			this.picEmpireFlag.TabIndex = 0;
			this.picEmpireFlag.TabStop = false;
			// 
			// pnlRight
			// 
			this.pnlRight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.pnlRight.Controls.Add(this.ddlGalaxyViewMode);
			this.pnlRight.Controls.Add(this.pnlGalaxyMap);
			this.pnlRight.Controls.Add(this.pnlDetailReport);
			this.pnlRight.Controls.Add(this.progResearch);
			this.pnlRight.Location = new System.Drawing.Point(853, 2);
			this.pnlRight.Margin = new System.Windows.Forms.Padding(0, 1, 1, 1);
			this.pnlRight.Name = "pnlRight";
			this.pnlRight.Size = new System.Drawing.Size(414, 775);
			this.pnlRight.TabIndex = 4;
			// 
			// ddlGalaxyViewMode
			// 
			this.ddlGalaxyViewMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ddlGalaxyViewMode.DisplayMember = "Name";
			this.ddlGalaxyViewMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlGalaxyViewMode.FormattingEnabled = true;
			this.ddlGalaxyViewMode.Location = new System.Drawing.Point(3, 495);
			this.ddlGalaxyViewMode.Margin = new System.Windows.Forms.Padding(0);
			this.ddlGalaxyViewMode.Name = "ddlGalaxyViewMode";
			this.ddlGalaxyViewMode.Size = new System.Drawing.Size(411, 33);
			this.ddlGalaxyViewMode.TabIndex = 1;
			this.ddlGalaxyViewMode.SelectedIndexChanged += new System.EventHandler(this.ddlGalaxyViewMode_SelectedIndexChanged);
			// 
			// pnlGalaxyMap
			// 
			this.pnlGalaxyMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlGalaxyMap.BackColor = System.Drawing.Color.Black;
			this.pnlGalaxyMap.BorderColor = System.Drawing.Color.RoyalBlue;
			this.pnlGalaxyMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlGalaxyMap.Controls.Add(this.galaxyView);
			this.pnlGalaxyMap.ForeColor = System.Drawing.Color.White;
			this.pnlGalaxyMap.Location = new System.Drawing.Point(4, 518);
			this.pnlGalaxyMap.Margin = new System.Windows.Forms.Padding(0);
			this.pnlGalaxyMap.Name = "pnlGalaxyMap";
			this.pnlGalaxyMap.Padding = new System.Windows.Forms.Padding(3);
			this.pnlGalaxyMap.Size = new System.Drawing.Size(411, 257);
			this.pnlGalaxyMap.TabIndex = 14;
			// 
			// galaxyView
			// 
			this.galaxyView.BackColor = System.Drawing.Color.Black;
			this.galaxyView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.galaxyView.Location = new System.Drawing.Point(3, 3);
			this.galaxyView.Mode = presenceMode1;
			this.galaxyView.Name = "galaxyView";
			this.galaxyView.SelectedStarSystem = null;
			this.galaxyView.Size = new System.Drawing.Size(403, 249);
			this.galaxyView.TabIndex = 0;
			this.galaxyView.Text = "galaxyView1";
			this.galaxyView.StarSystemClicked += new FrEee.WinForms.Controls.GalaxyView.StarSystemSelectionDelegate(this.galaxyView_StarSystemClicked);
			this.galaxyView.StarSystemSelected += new FrEee.WinForms.Controls.GalaxyView.StarSystemSelectionDelegate(this.galaxyView_StarSystemSelected);
			// 
			// pnlDetailReport
			// 
			this.pnlDetailReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlDetailReport.BackColor = System.Drawing.Color.Black;
			this.pnlDetailReport.BorderColor = System.Drawing.Color.RoyalBlue;
			this.pnlDetailReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlDetailReport.ForeColor = System.Drawing.Color.White;
			this.pnlDetailReport.Location = new System.Drawing.Point(4, 35);
			this.pnlDetailReport.Margin = new System.Windows.Forms.Padding(0);
			this.pnlDetailReport.Name = "pnlDetailReport";
			this.pnlDetailReport.Padding = new System.Windows.Forms.Padding(3);
			this.pnlDetailReport.Size = new System.Drawing.Size(412, 460);
			this.pnlDetailReport.TabIndex = 12;
			// 
			// MainGameForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(1270, 779);
			this.Controls.Add(this.pnlLayout);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MinimumSize = new System.Drawing.Size(900, 700);
			this.Name = "MainGameForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameForm_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameForm_FormClosed);
			this.Load += new System.EventHandler(this.GameForm_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyUp);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GameForm_MouseDown);
			this.pnlLayout.ResumeLayout(false);
			this.pnlLeft.ResumeLayout(false);
			this.pnlSystemMap.ResumeLayout(false);
			this.pnlSearch.ResumeLayout(false);
			this.pnlSystemTabs.ResumeLayout(false);
			this.pnlTabs.ResumeLayout(false);
			this.pnlSubCommands.ResumeLayout(false);
			this.pnlMainCommands.ResumeLayout(false);
			this.pnlHeader.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picEmpireFlag)).EndInit();
			this.pnlRight.ResumeLayout(false);
			this.pnlGalaxyMap.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolTip toolTip;
		private GamePanel pnlLayout;
		private System.Windows.Forms.Panel pnlRight;
		private GamePanel pnlGalaxyMap;
		private GalaxyView galaxyView;
		private GamePanel pnlDetailReport;
		private GameProgressBar progResearch;
		private System.Windows.Forms.Panel pnlLeft;
		private GamePanel pnlSystemMap;
		private StarSystemView starSystemView;
		private GamePanel pnlSearch;
		private SearchBox searchBox;
		private GamePanel pnlSystemTabs;
		private System.Windows.Forms.FlowLayoutPanel pnlTabs;
		private GameButton btnNewTab;
		private GamePanel pnlSubCommands;
		private GameButton btnSentry;
		private GameButton btnClearOrders;
		private GameButton btnFleetTransfer;
		private GameButton btnTransferCargo;
		private GameButton btnConstructionQueue;
		private GameButton btnColonize;
		private GameButton btnEvade;
		private GameButton btnWarp;
		private GameButton btnPursue;
		private GameButton btnMove;
		private GamePanel pnlMainCommands;
		private GameButton btnEndTurn;
		private GameButton btnLog;
		private GameButton btnQueues;
		private GameButton btnShips;
		private GameButton btnEmpires;
		private GameButton btnPlanets;
		private GameButton btnDesigns;
		private GameButton btnMenu;
		private GamePanel pnlHeader;
		private ResourceDisplay resInt;
		private ResourceDisplay resRes;
		private ResourceDisplay resRad;
		private ResourceDisplay resOrg;
		private ResourceDisplay resMin;
		private System.Windows.Forms.PictureBox picEmpireFlag;
		private GameButton btnNextIdle;
		private GameButton btnPrevIdle;
		private GameButton btnRepeatOrders;
		private GameButton btnRepair;
		private GameButton btnResupply;
		private GameButton btnRecycle;
		private GameButton btnRename;
		private GameButton btnToggleMinister;
		private GameButton btnActivate;
		private GameButton btnCloak;
		private GameButton btnDecloak;
		private GameButton btnMovementLog;
		private GameButton btnWaypoint;
		private System.Windows.Forms.ComboBox ddlGalaxyViewMode;
		private GameButton btnCommands;
	}
}

