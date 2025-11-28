using FrEee.UI.WinForms.Controls;

namespace FrEee.UI.WinForms.Forms;

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
		components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainGameForm));
		Objects.GalaxyViewModes.PresenceMode presenceMode1 = new Objects.GalaxyViewModes.PresenceMode();
		toolTip = new System.Windows.Forms.ToolTip(components);
		btnWaypoint = new GameButton();
		btnMovementLog = new GameButton();
		btnDecloak = new GameButton();
		btnCloak = new GameButton();
		btnActivate = new GameButton();
		btnToggleMinister = new GameButton();
		btnRename = new GameButton();
		btnRecycle = new GameButton();
		btnRepeatOrders = new GameButton();
		btnNextIdle = new GameButton();
		btnRepair = new GameButton();
		btnPrevIdle = new GameButton();
		btnResupply = new GameButton();
		btnSentry = new GameButton();
		btnClearOrders = new GameButton();
		btnFleetTransfer = new GameButton();
		btnTransferCargo = new GameButton();
		btnConstructionQueue = new GameButton();
		btnColonize = new GameButton();
		btnEvade = new GameButton();
		btnWarp = new GameButton();
		btnPursue = new GameButton();
		btnMove = new GameButton();
		btnEndTurn = new GameButton();
		btnLog = new GameButton();
		btnQueues = new GameButton();
		btnShips = new GameButton();
		btnEmpires = new GameButton();
		btnPlanets = new GameButton();
		btnDesigns = new GameButton();
		btnMenu = new GameButton();
		progResearch = new GameProgressBar();
		btnCommands = new GameButton();
		pnlLayout = new GamePanel();
		pnlLeft = new System.Windows.Forms.Panel();
		pnlSystemMap = new GamePanel();
		starSystemView = new StarSystemView();
		pnlSearch = new GamePanel();
		searchBox = new SearchBox();
		pnlSystemTabs = new GamePanel();
		pnlTabs = new System.Windows.Forms.FlowLayoutPanel();
		btnNewTab = new GameButton();
		pnlSubCommands = new GamePanel();
		pnlMainCommands = new GamePanel();
		pnlHeader = new GamePanel();
		rqdInventory = new ResourceQuantityDisplay();
		picEmpireFlag = new System.Windows.Forms.PictureBox();
		pnlRight = new System.Windows.Forms.Panel();
		ddlGalaxyViewMode = new System.Windows.Forms.ComboBox();
		pnlGalaxyMap = new GamePanel();
		galaxyView = new GalaxyView();
		pnlDetailReport = new GamePanel();
		pnlLayout.SuspendLayout();
		pnlLeft.SuspendLayout();
		pnlSystemMap.SuspendLayout();
		pnlSearch.SuspendLayout();
		pnlSystemTabs.SuspendLayout();
		pnlTabs.SuspendLayout();
		pnlSubCommands.SuspendLayout();
		pnlMainCommands.SuspendLayout();
		pnlHeader.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)picEmpireFlag).BeginInit();
		pnlRight.SuspendLayout();
		pnlGalaxyMap.SuspendLayout();
		SuspendLayout();
		// 
		// btnWaypoint
		// 
		btnWaypoint.BackColor = System.Drawing.Color.Black;
		btnWaypoint.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnWaypoint.Location = new System.Drawing.Point(49, 6);
		btnWaypoint.Name = "btnWaypoint";
		btnWaypoint.Size = new System.Drawing.Size(36, 36);
		btnWaypoint.TabIndex = 23;
		btnWaypoint.TabStop = false;
		btnWaypoint.Text = "Way";
		toolTip.SetToolTip(btnWaypoint, "(Ctrl-W) Move to Waypoint");
		btnWaypoint.UseVisualStyleBackColor = false;
		// 
		// btnMovementLog
		// 
		btnMovementLog.BackColor = System.Drawing.Color.Black;
		btnMovementLog.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnMovementLog.Location = new System.Drawing.Point(427, 48);
		btnMovementLog.Name = "btnMovementLog";
		btnMovementLog.Size = new System.Drawing.Size(36, 36);
		btnMovementLog.TabIndex = 22;
		btnMovementLog.TabStop = false;
		btnMovementLog.Text = "ML";
		toolTip.SetToolTip(btnMovementLog, "(Ctrl-L) Movement Log");
		btnMovementLog.UseVisualStyleBackColor = false;
		// 
		// btnDecloak
		// 
		btnDecloak.BackColor = System.Drawing.Color.Black;
		btnDecloak.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnDecloak.Location = new System.Drawing.Point(301, 49);
		btnDecloak.Name = "btnDecloak";
		btnDecloak.Size = new System.Drawing.Size(36, 36);
		btnDecloak.TabIndex = 21;
		btnDecloak.TabStop = false;
		btnDecloak.Text = "Dclk";
		toolTip.SetToolTip(btnDecloak, "(X) Decloak");
		btnDecloak.UseVisualStyleBackColor = false;
		// 
		// btnCloak
		// 
		btnCloak.BackColor = System.Drawing.Color.Black;
		btnCloak.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnCloak.Location = new System.Drawing.Point(301, 7);
		btnCloak.Name = "btnCloak";
		btnCloak.Size = new System.Drawing.Size(36, 36);
		btnCloak.TabIndex = 20;
		btnCloak.TabStop = false;
		btnCloak.Text = "Clk";
		toolTip.SetToolTip(btnCloak, "(Z) Cloak");
		btnCloak.UseVisualStyleBackColor = false;
		// 
		// btnActivate
		// 
		btnActivate.BackColor = System.Drawing.Color.Black;
		btnActivate.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnActivate.Location = new System.Drawing.Point(343, 7);
		btnActivate.Name = "btnActivate";
		btnActivate.Size = new System.Drawing.Size(36, 36);
		btnActivate.TabIndex = 19;
		btnActivate.TabStop = false;
		btnActivate.Text = "Act";
		toolTip.SetToolTip(btnActivate, "(Ctrl-A) Activate Ability");
		btnActivate.UseVisualStyleBackColor = false;
		btnActivate.Click += btnActivate_Click;
		// 
		// btnToggleMinister
		// 
		btnToggleMinister.BackColor = System.Drawing.Color.Black;
		btnToggleMinister.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnToggleMinister.Location = new System.Drawing.Point(385, 49);
		btnToggleMinister.Name = "btnToggleMinister";
		btnToggleMinister.Size = new System.Drawing.Size(36, 36);
		btnToggleMinister.TabIndex = 18;
		btnToggleMinister.TabStop = false;
		btnToggleMinister.Text = "Min";
		toolTip.SetToolTip(btnToggleMinister, "(T) Toggle Minister Control");
		btnToggleMinister.UseVisualStyleBackColor = false;
		// 
		// btnRename
		// 
		btnRename.BackColor = System.Drawing.Color.Black;
		btnRename.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnRename.Location = new System.Drawing.Point(385, 7);
		btnRename.Name = "btnRename";
		btnRename.Size = new System.Drawing.Size(36, 36);
		btnRename.TabIndex = 17;
		btnRename.TabStop = false;
		btnRename.Text = "Nam";
		toolTip.SetToolTip(btnRename, "(N) Rename/Notes");
		btnRename.UseVisualStyleBackColor = false;
		// 
		// btnRecycle
		// 
		btnRecycle.BackColor = System.Drawing.Color.Black;
		btnRecycle.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnRecycle.Location = new System.Drawing.Point(217, 48);
		btnRecycle.Name = "btnRecycle";
		btnRecycle.Size = new System.Drawing.Size(36, 36);
		btnRecycle.TabIndex = 16;
		btnRecycle.TabStop = false;
		btnRecycle.Text = "Rcy";
		toolTip.SetToolTip(btnRecycle, "(Ctrl-R) Recycle/Scuttle");
		btnRecycle.UseVisualStyleBackColor = false;
		btnRecycle.Click += btnRecycle_Click;
		// 
		// btnRepeatOrders
		// 
		btnRepeatOrders.BackColor = System.Drawing.Color.Black;
		btnRepeatOrders.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnRepeatOrders.Location = new System.Drawing.Point(49, 48);
		btnRepeatOrders.Name = "btnRepeatOrders";
		btnRepeatOrders.Size = new System.Drawing.Size(36, 36);
		btnRepeatOrders.TabIndex = 15;
		btnRepeatOrders.TabStop = false;
		btnRepeatOrders.Text = "Rept";
		toolTip.SetToolTip(btnRepeatOrders, "(P) Repeat Orders");
		btnRepeatOrders.UseVisualStyleBackColor = false;
		// 
		// btnNextIdle
		// 
		btnNextIdle.BackColor = System.Drawing.Color.Black;
		btnNextIdle.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnNextIdle.Location = new System.Drawing.Point(469, 7);
		btnNextIdle.Name = "btnNextIdle";
		btnNextIdle.Size = new System.Drawing.Size(36, 36);
		btnNextIdle.TabIndex = 6;
		btnNextIdle.TabStop = false;
		btnNextIdle.Text = "Next";
		toolTip.SetToolTip(btnNextIdle, "(Tab) Next Idle Space Object");
		btnNextIdle.UseVisualStyleBackColor = false;
		btnNextIdle.Click += btnNextIdle_Click;
		// 
		// btnRepair
		// 
		btnRepair.BackColor = System.Drawing.Color.Black;
		btnRepair.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnRepair.Location = new System.Drawing.Point(175, 49);
		btnRepair.Name = "btnRepair";
		btnRepair.Size = new System.Drawing.Size(36, 36);
		btnRepair.TabIndex = 14;
		btnRepair.TabStop = false;
		btnRepair.Text = "Repr";
		toolTip.SetToolTip(btnRepair, "(R) Repair");
		btnRepair.UseVisualStyleBackColor = false;
		// 
		// btnPrevIdle
		// 
		btnPrevIdle.BackColor = System.Drawing.Color.Black;
		btnPrevIdle.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnPrevIdle.Location = new System.Drawing.Point(427, 7);
		btnPrevIdle.Name = "btnPrevIdle";
		btnPrevIdle.Size = new System.Drawing.Size(36, 36);
		btnPrevIdle.TabIndex = 5;
		btnPrevIdle.TabStop = false;
		btnPrevIdle.Text = "Prev";
		toolTip.SetToolTip(btnPrevIdle, "(Shift-Tab) Previous Idle Space Object");
		btnPrevIdle.UseVisualStyleBackColor = false;
		btnPrevIdle.Click += btnPrevIdle_Click;
		// 
		// btnResupply
		// 
		btnResupply.BackColor = System.Drawing.Color.Black;
		btnResupply.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnResupply.Location = new System.Drawing.Point(175, 7);
		btnResupply.Name = "btnResupply";
		btnResupply.Size = new System.Drawing.Size(36, 36);
		btnResupply.TabIndex = 13;
		btnResupply.TabStop = false;
		btnResupply.Text = "Sply";
		toolTip.SetToolTip(btnResupply, "(S) Resupply");
		btnResupply.UseVisualStyleBackColor = false;
		// 
		// btnSentry
		// 
		btnSentry.BackColor = System.Drawing.Color.Black;
		btnSentry.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnSentry.Location = new System.Drawing.Point(133, 49);
		btnSentry.Name = "btnSentry";
		btnSentry.Size = new System.Drawing.Size(36, 36);
		btnSentry.TabIndex = 12;
		btnSentry.TabStop = false;
		btnSentry.Text = "Stry";
		toolTip.SetToolTip(btnSentry, "(Y) Sentry");
		btnSentry.UseVisualStyleBackColor = false;
		btnSentry.Click += btnSentry_Click;
		// 
		// btnClearOrders
		// 
		btnClearOrders.BackColor = System.Drawing.Color.Black;
		btnClearOrders.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnClearOrders.Location = new System.Drawing.Point(343, 49);
		btnClearOrders.Name = "btnClearOrders";
		btnClearOrders.Size = new System.Drawing.Size(36, 36);
		btnClearOrders.TabIndex = 11;
		btnClearOrders.TabStop = false;
		btnClearOrders.Text = "Clr";
		toolTip.SetToolTip(btnClearOrders, "(Bksp) Clear Orders");
		btnClearOrders.UseVisualStyleBackColor = false;
		btnClearOrders.Click += btnClearOrders_Click;
		// 
		// btnFleetTransfer
		// 
		btnFleetTransfer.BackColor = System.Drawing.Color.Black;
		btnFleetTransfer.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnFleetTransfer.Location = new System.Drawing.Point(259, 49);
		btnFleetTransfer.Name = "btnFleetTransfer";
		btnFleetTransfer.Size = new System.Drawing.Size(36, 36);
		btnFleetTransfer.TabIndex = 10;
		btnFleetTransfer.TabStop = false;
		btnFleetTransfer.Text = "Flt";
		toolTip.SetToolTip(btnFleetTransfer, "(F) Fleet Transfer");
		btnFleetTransfer.UseVisualStyleBackColor = false;
		btnFleetTransfer.Click += btnFleetTransfer_Click;
		// 
		// btnTransferCargo
		// 
		btnTransferCargo.BackColor = System.Drawing.Color.Black;
		btnTransferCargo.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnTransferCargo.Location = new System.Drawing.Point(259, 7);
		btnTransferCargo.Name = "btnTransferCargo";
		btnTransferCargo.Size = new System.Drawing.Size(36, 36);
		btnTransferCargo.TabIndex = 9;
		btnTransferCargo.TabStop = false;
		btnTransferCargo.Text = "TC";
		toolTip.SetToolTip(btnTransferCargo, "(T) Transfer Cargo");
		btnTransferCargo.UseVisualStyleBackColor = false;
		btnTransferCargo.Click += btnTransferCargo_Click;
		// 
		// btnConstructionQueue
		// 
		btnConstructionQueue.BackColor = System.Drawing.Color.Black;
		btnConstructionQueue.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnConstructionQueue.Location = new System.Drawing.Point(217, 7);
		btnConstructionQueue.Name = "btnConstructionQueue";
		btnConstructionQueue.Size = new System.Drawing.Size(36, 36);
		btnConstructionQueue.TabIndex = 8;
		btnConstructionQueue.TabStop = false;
		btnConstructionQueue.Text = "CQ";
		toolTip.SetToolTip(btnConstructionQueue, "(Q) Construction Queue");
		btnConstructionQueue.UseVisualStyleBackColor = false;
		btnConstructionQueue.Click += btnConstructionQueue_Click;
		// 
		// btnColonize
		// 
		btnColonize.BackColor = System.Drawing.Color.Black;
		btnColonize.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnColonize.Location = new System.Drawing.Point(133, 7);
		btnColonize.Name = "btnColonize";
		btnColonize.Size = new System.Drawing.Size(36, 36);
		btnColonize.TabIndex = 7;
		btnColonize.TabStop = false;
		btnColonize.Text = "Col";
		toolTip.SetToolTip(btnColonize, "(C) Colonize");
		btnColonize.UseVisualStyleBackColor = false;
		btnColonize.Click += btnColonize_Click;
		// 
		// btnEvade
		// 
		btnEvade.BackColor = System.Drawing.Color.Black;
		btnEvade.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnEvade.Location = new System.Drawing.Point(91, 47);
		btnEvade.Name = "btnEvade";
		btnEvade.Size = new System.Drawing.Size(36, 36);
		btnEvade.TabIndex = 6;
		btnEvade.TabStop = false;
		btnEvade.Text = "Ev";
		toolTip.SetToolTip(btnEvade, "(E) Evade");
		btnEvade.UseVisualStyleBackColor = false;
		btnEvade.Click += btnEvade_Click;
		// 
		// btnWarp
		// 
		btnWarp.BackColor = System.Drawing.Color.Black;
		btnWarp.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnWarp.Location = new System.Drawing.Point(7, 48);
		btnWarp.Name = "btnWarp";
		btnWarp.Size = new System.Drawing.Size(36, 36);
		btnWarp.TabIndex = 5;
		btnWarp.TabStop = false;
		btnWarp.Text = "Wp";
		toolTip.SetToolTip(btnWarp, "(W) Warp");
		btnWarp.UseVisualStyleBackColor = false;
		btnWarp.Click += btnWarp_Click;
		// 
		// btnPursue
		// 
		btnPursue.BackColor = System.Drawing.Color.Black;
		btnPursue.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnPursue.Location = new System.Drawing.Point(91, 6);
		btnPursue.Name = "btnPursue";
		btnPursue.Size = new System.Drawing.Size(36, 36);
		btnPursue.TabIndex = 4;
		btnPursue.TabStop = false;
		btnPursue.Text = "Pur";
		toolTip.SetToolTip(btnPursue, "(A) Attack / Pursue");
		btnPursue.UseVisualStyleBackColor = false;
		btnPursue.Click += btnPursue_Click;
		// 
		// btnMove
		// 
		btnMove.BackColor = System.Drawing.Color.Black;
		btnMove.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnMove.Location = new System.Drawing.Point(7, 6);
		btnMove.Name = "btnMove";
		btnMove.Size = new System.Drawing.Size(36, 36);
		btnMove.TabIndex = 2;
		btnMove.TabStop = false;
		btnMove.Text = "Mov";
		toolTip.SetToolTip(btnMove, "(M) Move");
		btnMove.UseVisualStyleBackColor = false;
		btnMove.Click += btnMove_Click;
		// 
		// btnEndTurn
		// 
		btnEndTurn.BackColor = System.Drawing.Color.Black;
		btnEndTurn.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnEndTurn.Location = new System.Drawing.Point(296, 6);
		btnEndTurn.Name = "btnEndTurn";
		btnEndTurn.Size = new System.Drawing.Size(36, 36);
		btnEndTurn.TabIndex = 7;
		btnEndTurn.TabStop = false;
		toolTip.SetToolTip(btnEndTurn, "(F12) Save Commands");
		btnEndTurn.UseVisualStyleBackColor = false;
		btnEndTurn.Click += btnEndTurn_Click;
		// 
		// btnLog
		// 
		btnLog.BackColor = System.Drawing.Color.Black;
		btnLog.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnLog.Location = new System.Drawing.Point(256, 6);
		btnLog.Name = "btnLog";
		btnLog.Size = new System.Drawing.Size(36, 36);
		btnLog.TabIndex = 6;
		btnLog.TabStop = false;
		toolTip.SetToolTip(btnLog, "(F10 / Shift-L) Log");
		btnLog.UseVisualStyleBackColor = false;
		btnLog.Click += btnLog_Click;
		// 
		// btnQueues
		// 
		btnQueues.BackColor = System.Drawing.Color.Black;
		btnQueues.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnQueues.Location = new System.Drawing.Point(214, 6);
		btnQueues.Name = "btnQueues";
		btnQueues.Size = new System.Drawing.Size(36, 36);
		btnQueues.TabIndex = 5;
		btnQueues.TabStop = false;
		toolTip.SetToolTip(btnQueues, "(F7 / Shift-Q) Construction Queues");
		btnQueues.UseVisualStyleBackColor = false;
		btnQueues.Click += btnQueues_Click;
		// 
		// btnShips
		// 
		btnShips.BackColor = System.Drawing.Color.Black;
		btnShips.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnShips.Location = new System.Drawing.Point(172, 6);
		btnShips.Name = "btnShips";
		btnShips.Size = new System.Drawing.Size(36, 36);
		btnShips.TabIndex = 4;
		btnShips.TabStop = false;
		toolTip.SetToolTip(btnShips, "(F6 / Shift-S) Ships");
		btnShips.UseVisualStyleBackColor = false;
		btnShips.Click += btnShips_Click;
		// 
		// btnEmpires
		// 
		btnEmpires.BackColor = System.Drawing.Color.Black;
		btnEmpires.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnEmpires.Location = new System.Drawing.Point(130, 6);
		btnEmpires.Name = "btnEmpires";
		btnEmpires.Size = new System.Drawing.Size(36, 36);
		btnEmpires.TabIndex = 3;
		btnEmpires.TabStop = false;
		toolTip.SetToolTip(btnEmpires, "(F9 / Shift-E) Empires");
		btnEmpires.UseVisualStyleBackColor = false;
		btnEmpires.Click += btnEmpires_Click;
		// 
		// btnPlanets
		// 
		btnPlanets.BackColor = System.Drawing.Color.Black;
		btnPlanets.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnPlanets.Location = new System.Drawing.Point(88, 6);
		btnPlanets.Name = "btnPlanets";
		btnPlanets.Size = new System.Drawing.Size(36, 36);
		btnPlanets.TabIndex = 0;
		btnPlanets.TabStop = false;
		toolTip.SetToolTip(btnPlanets, "(F4 / F5 / Shift-P / Shift-C) Planets/Colonies");
		btnPlanets.UseVisualStyleBackColor = false;
		btnPlanets.Click += btnPlanets_Click;
		// 
		// btnDesigns
		// 
		btnDesigns.BackColor = System.Drawing.Color.Black;
		btnDesigns.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnDesigns.Location = new System.Drawing.Point(47, 6);
		btnDesigns.Name = "btnDesigns";
		btnDesigns.Size = new System.Drawing.Size(36, 36);
		btnDesigns.TabIndex = 2;
		btnDesigns.TabStop = false;
		toolTip.SetToolTip(btnDesigns, "(F3 / Shift-D) Designs");
		btnDesigns.UseVisualStyleBackColor = false;
		btnDesigns.Click += btnDesigns_Click;
		// 
		// btnMenu
		// 
		btnMenu.BackColor = System.Drawing.Color.Black;
		btnMenu.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnMenu.Location = new System.Drawing.Point(5, 6);
		btnMenu.Name = "btnMenu";
		btnMenu.Size = new System.Drawing.Size(36, 36);
		btnMenu.TabIndex = 1;
		btnMenu.TabStop = false;
		toolTip.SetToolTip(btnMenu, "(F2) Menu");
		btnMenu.UseVisualStyleBackColor = false;
		btnMenu.Click += btnMenu_Click;
		// 
		// progResearch
		// 
		progResearch.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		progResearch.BackColor = System.Drawing.Color.Black;
		progResearch.BarColor = System.Drawing.Color.Magenta;
		progResearch.ForeColor = System.Drawing.Color.White;
		progResearch.IncrementalProgress = 0L;
		progResearch.LeftText = "Ice Planet Colonization";
		progResearch.Location = new System.Drawing.Point(3, -3);
		progResearch.Margin = new System.Windows.Forms.Padding(0);
		progResearch.Maximum = 500000L;
		progResearch.Name = "progResearch";
		progResearch.Padding = new System.Windows.Forms.Padding(5);
		progResearch.ProgressDisplayType = ProgressDisplayType.Numeric;
		progResearch.RightText = "0.2 years";
		progResearch.Size = new System.Drawing.Size(411, 38);
		progResearch.TabIndex = 11;
		toolTip.SetToolTip(progResearch, "(F8 / Shift-R) Research");
		progResearch.Value = 350000L;
		progResearch.Click += progResearch_Click;
		// 
		// btnCommands
		// 
		btnCommands.BackColor = System.Drawing.Color.Black;
		btnCommands.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnCommands.Location = new System.Drawing.Point(469, 47);
		btnCommands.Name = "btnCommands";
		btnCommands.Size = new System.Drawing.Size(36, 36);
		btnCommands.TabIndex = 24;
		btnCommands.TabStop = false;
		btnCommands.Text = "Cmd";
		toolTip.SetToolTip(btnCommands, "View Commands");
		btnCommands.UseVisualStyleBackColor = false;
		btnCommands.Click += btnCommands_Click;
		// 
		// pnlLayout
		// 
		pnlLayout.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		pnlLayout.BackColor = System.Drawing.Color.Black;
		pnlLayout.BorderColor = System.Drawing.Color.CornflowerBlue;
		pnlLayout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		pnlLayout.Controls.Add(pnlLeft);
		pnlLayout.Controls.Add(pnlRight);
		pnlLayout.ForeColor = System.Drawing.Color.White;
		pnlLayout.Location = new System.Drawing.Point(0, 0);
		pnlLayout.Margin = new System.Windows.Forms.Padding(0);
		pnlLayout.Name = "pnlLayout";
		pnlLayout.Padding = new System.Windows.Forms.Padding(3);
		pnlLayout.Size = new System.Drawing.Size(1270, 779);
		pnlLayout.TabIndex = 8;
		// 
		// pnlLeft
		// 
		pnlLeft.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		pnlLeft.Controls.Add(pnlSystemMap);
		pnlLeft.Controls.Add(pnlSearch);
		pnlLeft.Controls.Add(pnlSystemTabs);
		pnlLeft.Controls.Add(pnlSubCommands);
		pnlLeft.Controls.Add(pnlMainCommands);
		pnlLeft.Controls.Add(pnlHeader);
		pnlLeft.Location = new System.Drawing.Point(1, 0);
		pnlLeft.Margin = new System.Windows.Forms.Padding(1, 1, 0, 1);
		pnlLeft.Name = "pnlLeft";
		pnlLeft.Size = new System.Drawing.Size(857, 777);
		pnlLeft.TabIndex = 6;
		// 
		// pnlSystemMap
		// 
		pnlSystemMap.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		pnlSystemMap.BackColor = System.Drawing.Color.Black;
		pnlSystemMap.BorderColor = System.Drawing.Color.RoyalBlue;
		pnlSystemMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		pnlSystemMap.Controls.Add(starSystemView);
		pnlSystemMap.ForeColor = System.Drawing.Color.White;
		pnlSystemMap.Location = new System.Drawing.Point(145, 134);
		pnlSystemMap.Margin = new System.Windows.Forms.Padding(0);
		pnlSystemMap.Name = "pnlSystemMap";
		pnlSystemMap.Padding = new System.Windows.Forms.Padding(3);
		pnlSystemMap.Size = new System.Drawing.Size(712, 642);
		pnlSystemMap.TabIndex = 9;
		// 
		// starSystemView
		// 
		starSystemView.BackColor = System.Drawing.Color.Black;
		starSystemView.Dock = System.Windows.Forms.DockStyle.Fill;
		starSystemView.DrawText = true;
		starSystemView.Location = new System.Drawing.Point(3, 3);
		starSystemView.Name = "starSystemView";
		starSystemView.SelectedSector = null;
		starSystemView.SelectedSpaceObject = null;
		starSystemView.Size = new System.Drawing.Size(704, 634);
		starSystemView.StarSystem = null;
		starSystemView.TabIndex = 0;
		starSystemView.SectorClicked += starSystemView_SectorClicked;
		starSystemView.SectorSelected += starSystemView_SectorSelected;
		// 
		// pnlSearch
		// 
		pnlSearch.BackColor = System.Drawing.Color.Black;
		pnlSearch.BorderColor = System.Drawing.Color.RoyalBlue;
		pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		pnlSearch.Controls.Add(searchBox);
		pnlSearch.ForeColor = System.Drawing.Color.White;
		pnlSearch.Location = new System.Drawing.Point(-2, 85);
		pnlSearch.Margin = new System.Windows.Forms.Padding(2);
		pnlSearch.Name = "pnlSearch";
		pnlSearch.Padding = new System.Windows.Forms.Padding(3);
		pnlSearch.Size = new System.Drawing.Size(342, 49);
		pnlSearch.TabIndex = 8;
		// 
		// searchBox
		// 
		searchBox.BackColor = System.Drawing.Color.Black;
		searchBox.ForeColor = System.Drawing.Color.White;
		searchBox.Location = new System.Drawing.Point(12, 8);
		searchBox.Margin = new System.Windows.Forms.Padding(6);
		searchBox.Name = "searchBox";
		searchBox.ObjectsToSearch = null;
		searchBox.ResultsPopupHeight = 128;
		searchBox.Size = new System.Drawing.Size(320, 21);
		searchBox.StarSystem = null;
		searchBox.TabIndex = 0;
		searchBox.TabStop = false;
		searchBox.ObjectSelected += searchBox_ObjectSelected;
		// 
		// pnlSystemTabs
		// 
		pnlSystemTabs.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		pnlSystemTabs.BackColor = System.Drawing.Color.Black;
		pnlSystemTabs.BorderColor = System.Drawing.Color.RoyalBlue;
		pnlSystemTabs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		pnlSystemTabs.Controls.Add(pnlTabs);
		pnlSystemTabs.ForeColor = System.Drawing.Color.White;
		pnlSystemTabs.Location = new System.Drawing.Point(0, 134);
		pnlSystemTabs.Margin = new System.Windows.Forms.Padding(0);
		pnlSystemTabs.Name = "pnlSystemTabs";
		pnlSystemTabs.Padding = new System.Windows.Forms.Padding(3);
		pnlSystemTabs.Size = new System.Drawing.Size(145, 642);
		pnlSystemTabs.TabIndex = 7;
		// 
		// pnlTabs
		// 
		pnlTabs.Controls.Add(btnNewTab);
		pnlTabs.Dock = System.Windows.Forms.DockStyle.Fill;
		pnlTabs.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
		pnlTabs.Location = new System.Drawing.Point(3, 3);
		pnlTabs.Name = "pnlTabs";
		pnlTabs.Size = new System.Drawing.Size(137, 634);
		pnlTabs.TabIndex = 0;
		pnlTabs.DoubleClick += btnNewTab_Click;
		// 
		// btnNewTab
		// 
		btnNewTab.BackColor = System.Drawing.Color.Black;
		btnNewTab.ForeColor = System.Drawing.Color.CornflowerBlue;
		btnNewTab.Location = new System.Drawing.Point(3, 3);
		btnNewTab.Name = "btnNewTab";
		btnNewTab.Size = new System.Drawing.Size(128, 23);
		btnNewTab.TabIndex = 0;
		btnNewTab.TabStop = false;
		btnNewTab.Text = "(New Tab)";
		btnNewTab.UseVisualStyleBackColor = false;
		btnNewTab.Click += btnNewTab_Click;
		// 
		// pnlSubCommands
		// 
		pnlSubCommands.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		pnlSubCommands.BackColor = System.Drawing.Color.Black;
		pnlSubCommands.BorderColor = System.Drawing.Color.RoyalBlue;
		pnlSubCommands.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		pnlSubCommands.Controls.Add(btnCommands);
		pnlSubCommands.Controls.Add(btnWaypoint);
		pnlSubCommands.Controls.Add(btnMovementLog);
		pnlSubCommands.Controls.Add(btnDecloak);
		pnlSubCommands.Controls.Add(btnCloak);
		pnlSubCommands.Controls.Add(btnActivate);
		pnlSubCommands.Controls.Add(btnToggleMinister);
		pnlSubCommands.Controls.Add(btnRename);
		pnlSubCommands.Controls.Add(btnRecycle);
		pnlSubCommands.Controls.Add(btnRepeatOrders);
		pnlSubCommands.Controls.Add(btnNextIdle);
		pnlSubCommands.Controls.Add(btnRepair);
		pnlSubCommands.Controls.Add(btnPrevIdle);
		pnlSubCommands.Controls.Add(btnResupply);
		pnlSubCommands.Controls.Add(btnSentry);
		pnlSubCommands.Controls.Add(btnClearOrders);
		pnlSubCommands.Controls.Add(btnFleetTransfer);
		pnlSubCommands.Controls.Add(btnTransferCargo);
		pnlSubCommands.Controls.Add(btnConstructionQueue);
		pnlSubCommands.Controls.Add(btnColonize);
		pnlSubCommands.Controls.Add(btnEvade);
		pnlSubCommands.Controls.Add(btnWarp);
		pnlSubCommands.Controls.Add(btnPursue);
		pnlSubCommands.Controls.Add(btnMove);
		pnlSubCommands.ForeColor = System.Drawing.Color.White;
		pnlSubCommands.Location = new System.Drawing.Point(340, 37);
		pnlSubCommands.Margin = new System.Windows.Forms.Padding(0);
		pnlSubCommands.Name = "pnlSubCommands";
		pnlSubCommands.Padding = new System.Windows.Forms.Padding(4);
		pnlSubCommands.Size = new System.Drawing.Size(517, 97);
		pnlSubCommands.TabIndex = 6;
		// 
		// pnlMainCommands
		// 
		pnlMainCommands.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		pnlMainCommands.BackColor = System.Drawing.Color.Black;
		pnlMainCommands.BorderColor = System.Drawing.Color.RoyalBlue;
		pnlMainCommands.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		pnlMainCommands.Controls.Add(btnEndTurn);
		pnlMainCommands.Controls.Add(btnLog);
		pnlMainCommands.Controls.Add(btnQueues);
		pnlMainCommands.Controls.Add(btnShips);
		pnlMainCommands.Controls.Add(btnEmpires);
		pnlMainCommands.Controls.Add(btnPlanets);
		pnlMainCommands.Controls.Add(btnDesigns);
		pnlMainCommands.Controls.Add(btnMenu);
		pnlMainCommands.ForeColor = System.Drawing.Color.White;
		pnlMainCommands.Location = new System.Drawing.Point(-2, 37);
		pnlMainCommands.Margin = new System.Windows.Forms.Padding(0);
		pnlMainCommands.Name = "pnlMainCommands";
		pnlMainCommands.Padding = new System.Windows.Forms.Padding(6);
		pnlMainCommands.Size = new System.Drawing.Size(342, 50);
		pnlMainCommands.TabIndex = 3;
		// 
		// pnlHeader
		// 
		pnlHeader.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		pnlHeader.BackColor = System.Drawing.Color.Black;
		pnlHeader.BorderColor = System.Drawing.Color.RoyalBlue;
		pnlHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		pnlHeader.Controls.Add(rqdInventory);
		pnlHeader.Controls.Add(picEmpireFlag);
		pnlHeader.ForeColor = System.Drawing.Color.White;
		pnlHeader.Location = new System.Drawing.Point(-2, 0);
		pnlHeader.Margin = new System.Windows.Forms.Padding(2);
		pnlHeader.Name = "pnlHeader";
		pnlHeader.Padding = new System.Windows.Forms.Padding(3);
		pnlHeader.Size = new System.Drawing.Size(859, 37);
		pnlHeader.TabIndex = 5;
		// 
		// rqdInventory
		// 
		rqdInventory.BackColor = System.Drawing.Color.Black;
		rqdInventory.ForeColor = System.Drawing.Color.White;
		rqdInventory.Location = new System.Drawing.Point(49, 3);
		rqdInventory.Margin = new System.Windows.Forms.Padding(0);
		rqdInventory.Name = "rqdInventory";
		//rqdInventory.ResourcesToShow = new FrEee.Utility.Resource[]
		rqdInventory.Size = new System.Drawing.Size(798, 24);
		rqdInventory.TabIndex = 1;
		rqdInventory.Text = "resourceQuantityDisplay1";
		// 
		// picEmpireFlag
		// 
		picEmpireFlag.Location = new System.Drawing.Point(4, 5);
		picEmpireFlag.Margin = new System.Windows.Forms.Padding(4);
		picEmpireFlag.Name = "picEmpireFlag";
		picEmpireFlag.Size = new System.Drawing.Size(38, 21);
		picEmpireFlag.TabIndex = 0;
		picEmpireFlag.TabStop = false;
		// 
		// pnlRight
		// 
		pnlRight.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		pnlRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		pnlRight.Controls.Add(ddlGalaxyViewMode);
		pnlRight.Controls.Add(pnlGalaxyMap);
		pnlRight.Controls.Add(pnlDetailReport);
		pnlRight.Controls.Add(progResearch);
		pnlRight.Location = new System.Drawing.Point(853, 2);
		pnlRight.Margin = new System.Windows.Forms.Padding(0, 1, 1, 1);
		pnlRight.Name = "pnlRight";
		pnlRight.Size = new System.Drawing.Size(414, 775);
		pnlRight.TabIndex = 4;
		// 
		// ddlGalaxyViewMode
		// 
		ddlGalaxyViewMode.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		ddlGalaxyViewMode.DisplayMember = "Name";
		ddlGalaxyViewMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		ddlGalaxyViewMode.FormattingEnabled = true;
		ddlGalaxyViewMode.Location = new System.Drawing.Point(3, 495);
		ddlGalaxyViewMode.Margin = new System.Windows.Forms.Padding(0);
		ddlGalaxyViewMode.Name = "ddlGalaxyViewMode";
		ddlGalaxyViewMode.Size = new System.Drawing.Size(411, 23);
		ddlGalaxyViewMode.TabIndex = 1;
		ddlGalaxyViewMode.SelectedIndexChanged += ddlGalaxyViewMode_SelectedIndexChanged;
		// 
		// pnlGalaxyMap
		// 
		pnlGalaxyMap.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		pnlGalaxyMap.BackColor = System.Drawing.Color.Black;
		pnlGalaxyMap.BorderColor = System.Drawing.Color.RoyalBlue;
		pnlGalaxyMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		pnlGalaxyMap.Controls.Add(galaxyView);
		pnlGalaxyMap.ForeColor = System.Drawing.Color.White;
		pnlGalaxyMap.Location = new System.Drawing.Point(4, 518);
		pnlGalaxyMap.Margin = new System.Windows.Forms.Padding(0);
		pnlGalaxyMap.Name = "pnlGalaxyMap";
		pnlGalaxyMap.Padding = new System.Windows.Forms.Padding(3);
		pnlGalaxyMap.Size = new System.Drawing.Size(411, 257);
		pnlGalaxyMap.TabIndex = 14;
		// 
		// galaxyView
		// 
		galaxyView.BackColor = System.Drawing.Color.Black;
		galaxyView.Dock = System.Windows.Forms.DockStyle.Fill;
		galaxyView.Location = new System.Drawing.Point(3, 3);
		//galaxyView.Mode = presenceMode1;
		galaxyView.Name = "galaxyView";
		galaxyView.SelectedStarSystem = null;
		galaxyView.Size = new System.Drawing.Size(403, 249);
		galaxyView.TabIndex = 0;
		galaxyView.Text = "galaxyView1";
		galaxyView.StarSystemClicked += galaxyView_StarSystemClicked;
		galaxyView.StarSystemSelected += galaxyView_StarSystemSelected;
		// 
		// pnlDetailReport
		// 
		pnlDetailReport.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		pnlDetailReport.BackColor = System.Drawing.Color.Black;
		pnlDetailReport.BorderColor = System.Drawing.Color.RoyalBlue;
		pnlDetailReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		pnlDetailReport.ForeColor = System.Drawing.Color.White;
		pnlDetailReport.Location = new System.Drawing.Point(4, 35);
		pnlDetailReport.Margin = new System.Windows.Forms.Padding(0);
		pnlDetailReport.Name = "pnlDetailReport";
		pnlDetailReport.Padding = new System.Windows.Forms.Padding(3);
		pnlDetailReport.Size = new System.Drawing.Size(412, 460);
		pnlDetailReport.TabIndex = 12;
		// 
		// MainGameForm
		// 
		AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
		BackColor = System.Drawing.Color.Black;
		ClientSize = new System.Drawing.Size(1270, 779);
		Controls.Add(pnlLayout);
		DoubleBuffered = true;
		Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
		KeyPreview = true;
		Margin = new System.Windows.Forms.Padding(4);
		MinimumSize = new System.Drawing.Size(900, 700);
		Name = "MainGameForm";
		FormClosing += GameForm_FormClosing;
		FormClosed += GameForm_FormClosed;
		Load += GameForm_Load;
		KeyDown += GameForm_KeyDown;
		KeyUp += GameForm_KeyUp;
		MouseDown += GameForm_MouseDown;
		pnlLayout.ResumeLayout(false);
		pnlLeft.ResumeLayout(false);
		pnlSystemMap.ResumeLayout(false);
		pnlSearch.ResumeLayout(false);
		pnlSystemTabs.ResumeLayout(false);
		pnlTabs.ResumeLayout(false);
		pnlSubCommands.ResumeLayout(false);
		pnlMainCommands.ResumeLayout(false);
		pnlHeader.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)picEmpireFlag).EndInit();
		pnlRight.ResumeLayout(false);
		pnlGalaxyMap.ResumeLayout(false);
		ResumeLayout(false);
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
	private ResourceQuantityDisplay rqdInventory;
}

