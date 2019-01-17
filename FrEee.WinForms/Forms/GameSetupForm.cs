using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Vehicles;
using FrEee.Game.Objects.VictoryConditions;
using FrEee.Game.Setup;
using FrEee.Game.Setup.WarpPointPlacementStrategies;
using FrEee.Modding;
using FrEee.Modding.Templates;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
	public partial class GameSetupForm : GameForm
	{
		public GameSetupForm()
		{
			InitializeComponent();
			setup = new GameSetup();
			if (Mod.Current == null)
				Mod.Load(null);
			Galaxy.Current = null;

			spnSeed.Value = DateTime.Now.Millisecond + 1000 * DateTime.Now.Second + 60000 * DateTime.Now.Minute;

			// bind data
			galaxyTemplateBindingSource.DataSource = Mod.Current.GalaxyTemplates;
			warpPointPlacementStrategyBindingSource.DataSource = WarpPointPlacementStrategy.All;
			lstTechs.Items.AddRange(Mod.Current.Technologies.Where(t => t.CanBeRemoved).ToArray());
			foreach (StellarSize item in Enum.GetValues(typeof(StellarSize)))
				ddlHomeworldSize.Items.Add(item);
			// TODO - set step-amount for racial points spinbox to the greatest common factor of the mod's racial trait costs? or maybe based on aptitudes too?
			ddlAllowedTrades.DataSource = Enum.GetValues(typeof(AllowedTrades)).Cast<AllowedTrades>().Select(e => new { Name = e.ToSpacedString(), Value = e }).ToList();
			ddlAllowedTrades.SelectedValue = AllowedTrades.All;

			// initialize data
			ddlGalaxyType_SelectedIndexChanged(ddlGalaxyType, new EventArgs());
			spnWidth_ValueChanged(spnWidth, new EventArgs());
			spnHeight_ValueChanged(spnHeight, new EventArgs());
			spnStarSystems_ValueChanged(spnStarSystems, new EventArgs());
			ddlWarpPointLocation_SelectedIndexChanged(ddlWarpPointLocation, new EventArgs());
			ddlStartTech.SelectedIndex = 0;
			for (int i = 0; i < lstTechs.Items.Count; i++)
			{
				lstTechs.SetItemChecked(i, true);
			}
			ddlHomeworldSize.SelectedItem = ddlHomeworldSize.Items.Cast<object>().Last();
			ddlEmpirePlacement.SelectedIndex = 2; // equidistant
			ddlScoreDisplay.SelectedIndex = 0; // own only, no ranking
			ddlTechCost.SelectedIndex = 0; // low tech cost

			ddlEventFrequency.Items.Add(new { Name = $"None", Value = 0 });
			ddlEventFrequency.Items.Add(new { Name = $"Low ({Mod.Current.Settings.EventFrequencyLow})", Value = Mod.Current.Settings.EventFrequencyLow });
			ddlEventFrequency.Items.Add(new { Name = $"Medum ({Mod.Current.Settings.EventFrequencyMedium})", Value = Mod.Current.Settings.EventFrequencyMedium });
			ddlEventFrequency.Items.Add(new { Name = $"High ({Mod.Current.Settings.EventFrequencyHigh})", Value = Mod.Current.Settings.EventFrequencyHigh });
			ddlEventFrequency.Items.Add(new { Name = $"Ludicrous (1000)", Value = 1000 });
			ddlEventFrequency.SelectedItem = ddlEventFrequency.Items.Cast<dynamic>().ElementAt(0);

			foreach (var item in Enum.GetValues(typeof(EventSeverity)))
				ddlMaximumEventSeverity.Items.Add(item);

			ddlMaximumEventSeverity.SelectedItem = EventSeverity.None;

			ddlTechUniqueness.Items.Add(new { Name = "Very Easy (-2)", Value = -2 });
			ddlTechUniqueness.Items.Add(new { Name = "Easy (-1)", Value = -1 });
			ddlTechUniqueness.Items.Add(new { Name = "Normal (0)", Value = 0 });
			ddlTechUniqueness.Items.Add(new { Name = "Hard (+1)", Value = 1 });
			ddlTechUniqueness.Items.Add(new { Name = "Very Hard (+2)", Value = 2 });
			ddlTechUniqueness.SelectedIndex = 2; // zero, normal cost 

			try { this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); } catch { }
		}

		public bool IsBusy
		{
			get
			{
				return _isBusy;
			}
			set
			{
				_isBusy = value;
				tabs.Enabled = btnCancel.Enabled = btnStart.Enabled = btnLoadSetup.Enabled = btnSaveSetup.Enabled = !IsBusy;
				progressBar.Visible = IsBusy;
			}
		}

		private bool _isBusy;

		private int miningRate = 1000;

		private bool remote = false;

		private GameSetup setup;

		private int startValue = 120;

		private void Bind()
		{
			txtGalaxyName.Text = setup.GameName;
			spnStarSystems.Value = setup.StarSystemCount;
			spnSeed.Value = setup.Seed;
			spnWidth.ValueChanged -= spnWidth_ValueChanged;
			spnHeight.ValueChanged -= spnHeight_ValueChanged;
			spnWidth.Value = setup.GalaxySize.Width;
			spnHeight.Value = setup.GalaxySize.Height;
			spnWidth.ValueChanged += spnWidth_ValueChanged;
			spnHeight.ValueChanged += spnHeight_ValueChanged;
			spnSystemGroups.Value = setup.StarSystemGroups;
			chkAllSystemsExplored.Checked = setup.AllSystemsExplored;
			chkOmniscient.Checked = setup.OmniscientView;
			ddlEventFrequency.SelectedItem = ddlEventFrequency.Items.Cast<dynamic>().SingleOrDefault(q => q.Value == setup.EventFrequency);
			ddlMaximumEventSeverity.SelectedItem = setup.MaximumEventSeverity;

			spnRateStandard.Value = (decimal)setup.StandardMiningModel.RatePercentage;
			spnBonusStandard.Value = (decimal)setup.StandardMiningModel.ValuePercentageBonus;
			spnDepletionResourceStandard.Value = (decimal)setup.StandardMiningModel.ValueDepletionPerResource;
			chkBonusDepletionStandard.Checked = setup.StandardMiningModel.BonusAffectsDepletion;
			spnDepletionResourceStandard.Value = setup.StandardMiningModel.ValueDepletionPerTurn;

			spnRateRemote.Value = (decimal)setup.RemoteMiningModel.RatePercentage;
			spnBonusRemote.Value = (decimal)setup.RemoteMiningModel.ValuePercentageBonus;
			spnDepletionResourceRemote.Value = (decimal)setup.RemoteMiningModel.ValueDepletionPerResource;
			chkBonusDepletionRemote.Checked = setup.RemoteMiningModel.BonusAffectsDepletion;
			spnDepletionTurnRemote.Value = setup.RemoteMiningModel.ValueDepletionPerTurn;

			spnMinValuePlanet.Value = setup.MinPlanetValue;
			spnMinSpawnValuePlanet.Value = setup.MinSpawnedPlanetValue;
			spnHomeworldValue.Value = setup.HomeworldValue;
			spnMaxSpawnValuePlanet.Value = setup.MaxSpawnedPlanetValue;
			spnMaxValuePlanet.Value = setup.MaxPlanetValue;

			spnMinValueAsteroid.Value = setup.MinAsteroidValue;
			spnMinSpawnValueAsteroid.Value = setup.MinSpawnedAsteroidValue;
			spnMaxSpawnValueAsteroid.Value = setup.MaxSpawnedAsteroidValue;

			switch (setup.StartingTechnologyLevel)
			{
				case StartingTechnologyLevel.Low:
					ddlStartTech.SelectedIndex = 0;
					break;

				case StartingTechnologyLevel.Medium:
					ddlStartTech.SelectedIndex = 1;
					break;

				case StartingTechnologyLevel.High:
					ddlStartTech.SelectedIndex = 2;
					break;
			}
			ddlStartTech.SelectedIndex = (int)setup.TechnologyCost;
			for (int i = 0; i < lstTechs.Items.Count; i++)
				lstTechs.SetItemChecked(i, !setup.ForbiddenTechnologyNames.Contains(((Technology)lstTechs.Items[i]).Name));
			ddlTechUniqueness.SelectedItem = ddlTechUniqueness.Items.Cast<dynamic>().SingleOrDefault(x => x.Value == setup.TechnologyUniqueness);
			spnResearchPerUnspentEmpirePoint.Value = setup.ResearchPointsPerUnspentEmpirePoint;

			spnStartResources.Value = setup.StartingResources;
			spnResourceStorage.Value = setup.ResourceStorage;
			spnStartResearch.Value = setup.StartingResearch;
			spnHomeworlds.Value = setup.HomeworldsPerEmpire;
			ddlHomeworldSize.SelectedItem = setup.HomeworldSize;
			ddlEmpirePlacement.SelectedIndex = (int)setup.EmpirePlacement;
			spnMaxDispersion.Value = setup.MaxHomeworldDispersion;
			ddlScoreDisplay.SelectedIndex = (int)Enum.GetValues(typeof(ScoreDisplay)).Cast<ScoreDisplay>().IndexOf(setup.ScoreDisplay);
			spnEmpirePoints.Value = setup.EmpirePoints;
			spnRandomAIs.Value = setup.RandomAIs;
			spnMinorEmpires.Value = setup.MinorEmpires;

			BindEmpires();

			if (setup.VictoryConditions.OfType<MajorEmpireEliminationVictoryCondition>().Any())
				chkVictoryEliminateMajorEmpires.Checked = true;
			else
				chkVictoryEliminateMajorEmpires.Checked = false;
			if (setup.VictoryConditions.OfType<ScoreVictoryCondition>().Any())
			{
				chkVictoryScore.Checked = true;
				var vc = setup.VictoryConditions.OfType<ScoreVictoryCondition>().First();
				spnVictoryScore.Value = vc.Score;
			}
			else
				chkVictoryScore.Checked = false;
			if (setup.VictoryConditions.OfType<ScorePercentageVictoryCondition>().Any())
			{
				chkVictoryScorePercent.Checked = true;
				var vc = setup.VictoryConditions.OfType<ScorePercentageVictoryCondition>().First();
				spnVictoryScorePercent.Value = vc.Percentage;
			}
			else
				chkVictoryScorePercent.Checked = false;
			if (setup.VictoryConditions.OfType<SurvivalVictoryCondition>().Any())
			{
				chkVictoryTurns.Checked = true;
				var vc = setup.VictoryConditions.OfType<SurvivalVictoryCondition>().First();
				spnVictoryTurns.Value = vc.Turns;
			}
			else
				chkVictoryTurns.Checked = false;
			if (setup.VictoryConditions.OfType<TechnologyVictoryCondition>().Any())
			{
				chkVictoryTech.Checked = true;
				var vc = setup.VictoryConditions.OfType<TechnologyVictoryCondition>().First();
				spnVictoryTech.Value = vc.Percentage;
			}
			else
				chkVictoryTech.Checked = false;
			if (setup.VictoryConditions.OfType<PeaceVictoryCondition>().Any())
			{
				chkVictoryPeace.Checked = true;
				var vc = setup.VictoryConditions.OfType<PeaceVictoryCondition>().First();
				spnVictoryPeace.Value = vc.Turns;
			}
			else
				chkVictoryPeace.Checked = false;
			spnVictoryDelay.Value = setup.VictoryDelay;

			chkHumansVsAI.Checked = setup.IsHumansVsAI;
			ddlAllowedTrades.SelectedValue = setup.AllowedTrades;
			chkAllowSurrender.Checked = setup.IsSurrenderAllowed;
			chkAllowIntel.Checked = setup.IsIntelligenceAllowed;
			chkAllowAnalysis.Checked = setup.IsAnalysisAllowed;
			chkRandomRuins.Checked = setup.GenerateRandomRuins;
			chkUniqueRuins.Checked = setup.GenerateUniqueRuins;
			chkColonizeOnlyBreathable.Checked = setup.CanColonizeOnlyBreathable;
			chkColonizeOnlyHWSurface.Checked = setup.CanColonizeOnlyHomeworldSurface;
		}

		private void BindEmpires()
		{
			var selected = lstEmpires.SelectedItems.Cast<ListViewItem>().Select(i => (EmpireTemplate)i.Tag);
			lstEmpires.Initialize(32, 32);
			foreach (var et in setup.EmpireTemplates)
				lstEmpires.AddItemWithImage(null, et.IsPlayerEmpire ? et.Name : "(AI) " + et.Name, et, et.Insignia);
			foreach (ListViewItem item in lstEmpires.Items)
			{
				if (selected.Contains((EmpireTemplate)item.Tag))
					item.Selected = true;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnCreateEmpire_Click(object sender, EventArgs e)
		{
			var et = new EmpireTemplate();
			et.IsPlayerEmpire = true;
			var form = new EmpireSetupForm();
			form.PointsToSpend = (int)spnEmpirePoints.Value;
			form.EmpireTemplate = et;
			this.ShowChildForm(form);
			if (form.DialogResult == DialogResult.OK)
			{
				setup.EmpireTemplates.Add(et);
				BindEmpires();
			}
		}

		private void btnEditEmpire_Click(object sender, EventArgs e)
		{
			if (lstEmpires.SelectedItems.Count == 1)
			{
				var et = (EmpireTemplate)lstEmpires.SelectedItems[0].Tag;
				var form = new EmpireSetupForm();
				form.PointsToSpend = (int)spnEmpirePoints.Value;
				form.EmpireTemplate = et;
				this.ShowChildForm(form);
				if (form.DialogResult == DialogResult.OK)
					BindEmpires();
			}
		}

		private void btnEmpireBottom_Click(object sender, EventArgs e)
		{
			if (lstEmpires.SelectedItems.Count == 1)
			{
				var item = lstEmpires.SelectedItems[0];
				var et = (EmpireTemplate)item.Tag;
				setup.EmpireTemplates.Remove(et);
				setup.EmpireTemplates.Add(et);
				BindEmpires();
			}
		}

		private void btnEmpireDown_Click(object sender, EventArgs e)
		{
			if (lstEmpires.SelectedItems.Count == 1)
			{
				var item = lstEmpires.SelectedItems[0];
				var et = (EmpireTemplate)item.Tag;
				int idx = setup.EmpireTemplates.IndexOf(et);
				if (idx < setup.EmpireTemplates.Count - 1)
				{
					setup.EmpireTemplates.Remove(et);
					setup.EmpireTemplates.Insert(idx + 1, et);
					BindEmpires();
				}
			}
		}

		private void btnEmpireTop_Click(object sender, EventArgs e)
		{
			if (lstEmpires.SelectedItems.Count == 1)
			{
				var item = lstEmpires.SelectedItems[0];
				var et = (EmpireTemplate)item.Tag;
				setup.EmpireTemplates.Remove(et);
				setup.EmpireTemplates.Insert(0, et);
				BindEmpires();
			}
		}

		private void btnEmpireUp_Click(object sender, EventArgs e)
		{
			if (lstEmpires.SelectedItems.Count == 1)
			{
				var item = lstEmpires.SelectedItems[0];
				var et = (EmpireTemplate)item.Tag;
				int idx = setup.EmpireTemplates.IndexOf(et);
				if (idx > 0)
				{
					setup.EmpireTemplates.Remove(et);
					setup.EmpireTemplates.Insert(idx - 1, et);
					BindEmpires();
				}
			}
		}

		private void btnLoadEmpire_Click(object sender, EventArgs e)
		{
			var dlg = new OpenFileDialog();
			dlg.InitialDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Empires");
			dlg.Filter = "Empires (*.emp)|*.emp";
			var result = dlg.ShowDialog();
			if (result == DialogResult.OK)
			{
				var et = EmpireTemplate.Load(dlg.FileName);
				setup.EmpireTemplates.Add(et);
				BindEmpires();
			}
		}

		private void btnLoadResourcePreset_Click(object sender, EventArgs e)
		{
			// TODO - customizable presets?
			if (ddlPresets.SelectedIndex == 0)
			{
				// standard, remote mining depletes
				spnRateStandard.Value = spnRateRemote.Value = 0;
				chkLimitStandard.Checked = chkLimitRemote.Checked = false;
				spnBonusStandard.Value = spnBonusRemote.Value = 1;
				spnDepletionResourceStandard.Value = spnDepletionResourceRemote.Value = 0;
				chkBonusDepletionStandard.Checked = chkBonusDepletionRemote.Checked = false;
				spnDepletionTurnStandard.Value = 0;
				spnDepletionTurnRemote.Value = 1;
				spnMinValuePlanet.Value = spnMinValueAsteroid.Value = 0;
				spnMinSpawnValuePlanet.Value = 0;
				spnMinSpawnValueAsteroid.Value = 50;
				spnHomeworldValue.Value = 120;
				spnMaxSpawnValuePlanet.Value = 150;
				spnMaxSpawnValueAsteroid.Value = 300;
				spnMaxValuePlanet.Value = 250;
				spnStartValue.Value = 100;
			}
			else if (ddlPresets.SelectedIndex == 1)
			{
				// standard, remote mining doesn't deplete
				spnRateStandard.Value = spnRateRemote.Value = 0;
				chkLimitStandard.Checked = chkLimitRemote.Checked = false;
				spnBonusStandard.Value = spnBonusRemote.Value = 1;
				spnDepletionResourceStandard.Value = spnDepletionResourceRemote.Value = 0;
				chkBonusDepletionStandard.Checked = chkBonusDepletionRemote.Checked = false;
				spnDepletionTurnStandard.Value = 0;
				spnDepletionTurnRemote.Value = 0;
				spnMinValuePlanet.Value = spnMinValueAsteroid.Value = 0;
				spnMinSpawnValuePlanet.Value = 0;
				spnMinSpawnValueAsteroid.Value = 50;
				spnStartValue.Value = spnHomeworldValue.Value = 120;
				spnMaxSpawnValuePlanet.Value = 150;
				spnMaxSpawnValueAsteroid.Value = 300;
				spnMaxValuePlanet.Value = 250;
				spnStartValue.Value = 100;
			}
			else if (ddlPresets.SelectedIndex == 2)
			{
				// finite
				spnRateStandard.Value = spnRateRemote.Value = 100;
				chkLimitStandard.Checked = chkLimitRemote.Checked = true;
				spnBonusStandard.Value = spnBonusRemote.Value = 0;
				spnDepletionResourceStandard.Value = spnDepletionResourceRemote.Value = 1;
				chkBonusDepletionStandard.Checked = chkBonusDepletionRemote.Checked = true;
				spnDepletionTurnStandard.Value = 0;
				spnDepletionTurnRemote.Value = 0;
				spnMinValuePlanet.Value = spnMinValueAsteroid.Value = 0;
				spnMinSpawnValuePlanet.Value = 0;
				spnMinSpawnValueAsteroid.Value = 100e3m;
				spnStartValue.Value = spnHomeworldValue.Value = 2e6m;
				spnMaxSpawnValuePlanet.Value = 500e3m;
				spnMaxSpawnValueAsteroid.Value = 800e3m;
				spnMaxValuePlanet.Value = 10e6m;
				spnStartValue.Value = 500000;
			}
			btnRefreshGraphs_Click(btnLoadResourcePreset, new EventArgs());
		}

		private void btnLoadSetup_Click(object sender, EventArgs e)
		{
			var dlg = new OpenFileDialog();
			dlg.InitialDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "GameSetups");
			dlg.Filter = "Game Setups (*.gsu)|*.gsu";
			var result = dlg.ShowDialog();
			if (result == DialogResult.OK)
			{
				setup = GameSetup.Load(dlg.FileName);
				Bind();
			}
		}

		private void btnRefreshGraphs_Click(object sender, EventArgs e)
		{
			picMiningGraph.Invalidate();
			picValueGraph.Invalidate();
		}

		private void btnRemoveEmpire_Click(object sender, EventArgs e)
		{
			if (lstEmpires.SelectedItems.Count > 0)
			{
				foreach (ListViewItem item in lstEmpires.SelectedItems)
					setup.EmpireTemplates.Remove((EmpireTemplate)item.Tag);
				BindEmpires();
			}
		}

		private void btnSaveEmpire_Click(object sender, EventArgs e)
		{
			if (lstEmpires.SelectedItems.Count == 1)
			{
				var dlg = new SaveFileDialog();
				dlg.InitialDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Empires");
				dlg.Filter = "Empires (*.emp)|*.emp";
				var result = dlg.ShowDialog();
				if (result == DialogResult.OK)
				{
					var et = (EmpireTemplate)lstEmpires.SelectedItems[0].Tag;
					et.Save(dlg.FileName);
				}
			}
		}

		private void btnSaveSetup_Click(object sender, EventArgs e)
		{
			SaveChanges();
			var dlg = new SaveFileDialog();
			dlg.InitialDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "GameSetups");
			dlg.Filter = "Game Setups (*.gsu)|*.gsu";
			var result = dlg.ShowDialog();
			if (result == DialogResult.OK)
				setup.Save(dlg.FileName);
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			SaveChanges();

			if (setup.Warnings.Any())
			{
				MessageBox.Show(setup.Warnings.First(), "FrEee");
				return;
			}

			progressBar.Visible = true;
			var status = new Status
			{
				Progress = 0d,
				Message = "Initializing",
				Exception = null,
			};

			// TODO - why are we loading the galaxy twice?
			Thread t = new Thread(new ThreadStart(() =>
			{
				try
				{
					status.Message = "Setting up galaxy";
					Galaxy.Initialize(setup, new PRNG((int)spnSeed.Value), status, 1d);
					if (Galaxy.Current.IsSinglePlayer)
					{
						var name = Galaxy.Current.Name;
						var turn = Galaxy.Current.TurnNumber;
						status.Message = "Loading game";
						Galaxy.Load(name + "_" + turn + "_0001.gam");
					}
				}
				catch (Exception ex)
				{
					status.Exception = ex;
				}
			}));
			t.Name = "Game Setup";
			this.ShowChildForm(new StatusForm(t, status));

			if (status.Exception == null)
			{
				if (Galaxy.Current.IsSinglePlayer)
				{
					var name = Galaxy.Current.Name;
					var turn = Galaxy.Current.TurnNumber;
					status.Message = "Loading game";
					Galaxy.Load(name + "_" + turn + "_0001.gam");
					Design.ImportFromLibrary();
					Hide();
					MainMenuForm.GetInstance().ShowChildForm(new MainGameForm(false, true));
				}
				else
				{
					MessageBox.Show("The game \"" + Galaxy.Current.Name + "\" has been created. Please distribute the GAM files to appropriate players.");
					Close();
				}
			}
		}

		private void btnToggleAI_Click(object sender, EventArgs e)
		{
			if (lstEmpires.SelectedItems.Count > 0)
			{
				foreach (ListViewItem item in lstEmpires.SelectedItems)
				{
					var et = (EmpireTemplate)item.Tag;
					et.IsPlayerEmpire = !et.IsPlayerEmpire;
				}
				BindEmpires();
			}
		}

		private void chkRemote_CheckedChanged(object sender, EventArgs e)
		{
			remote = chkRemote.Checked;
		}

		private void ddlGalaxyType_SelectedIndexChanged(object sender, EventArgs e)
		{
			var gt = (GalaxyTemplate)ddlGalaxyType.SelectedItem;
			if (gt == null)
				txtGalaxyTypeDescription.Text = "Please select a galaxy type.";
			else
				txtGalaxyTypeDescription.Text = gt.Description;
			setup.GalaxyTemplate = gt;
		}

		private void ddlWarpPointLocation_SelectedIndexChanged(object sender, EventArgs e)
		{
			setup.WarpPointPlacementStrategy = (WarpPointPlacementStrategy)ddlWarpPointLocation.SelectedItem;
			if (setup.WarpPointPlacementStrategy == null)
				txtWarpPointLocation.Text = "Choose a warp point placement option.";
			else
				txtWarpPointLocation.Text = setup.WarpPointPlacementStrategy.Description;
		}

		private void DrawLineGraph(Graphics g, RectangleF bounds, string label, Color lineColor, IEnumerable<int> dataPoints)
		{
			var max = dataPoints.Max();
			if (max <= 0)
				return;
			var xScale = bounds.Width / (dataPoints.Count() - 1);
			var yScale = -bounds.Height / max;

			// draw Y-axis
			var pen = Pens.White;
			g.DrawLine(pen, bounds.Left, bounds.Top, bounds.Left, bounds.Bottom);
			var font = new Font(FontFamily.GenericSansSerif, 9);
			var brush = Brushes.White;
			g.DrawString(label, font, brush, 0, 0);

			// draw horizontals
			var sf = new StringFormat { Alignment = StringAlignment.Far };
			for (int line = 0; line <= 5; line++)
			{
				var ypos = line * bounds.Height / -5 + bounds.Bottom;
				g.DrawLine(pen, bounds.Left, ypos, bounds.Right, ypos);
				g.DrawString((max * line / 5).ToString(), font, Brushes.White, bounds.Right, ypos, sf);
			}

			// draw chart
			int prev = dataPoints.First();
			var linePen = new Pen(lineColor);
			for (int i = 1; i < dataPoints.Count(); i++)
			{
				var cur = dataPoints.ElementAt(i);
				var xpos1 = bounds.Left + (i - 1) * xScale;
				var xpos2 = bounds.Left + i * xScale;
				var ypos1 = bounds.Bottom + prev * yScale;
				var ypos2 = bounds.Bottom + cur * yScale;
				g.DrawLine(linePen, xpos1, ypos1, xpos2, ypos2);
				prev = cur;
			}
		}

		private void GameSetupForm_Load(object sender, EventArgs e)
		{
			if (Mod.Errors.Any())
			{
				var doOrDie = this.ShowChildForm(new ModErrorsForm());
				if (doOrDie == DialogResult.Cancel)
					Close();
			}
		}

		private void picMiningGraph_Paint(object sender, PaintEventArgs e)
		{
			var data = RunMiningSimulation(100).Select(t => t.Item1);
			DrawLineGraph(e.Graphics, new RectangleF(Point.Empty, picMiningGraph.Size), "Mining", Color.Blue, data);
		}

		private void picValueGraph_Paint(object sender, PaintEventArgs e)
		{
			var data = RunMiningSimulation(100).Select(t => t.Item2);
			DrawLineGraph(e.Graphics, new RectangleF(Point.Empty, picValueGraph.Size), "Value", Color.Green, data);
		}

		private IEnumerable<Tuple<int, int>> RunMiningSimulation(int turns)
		{
			var model = new MiningModel();
			if (remote)
			{
				model.RatePercentage = (double)spnRateRemote.Value;
				model.LimitRateToValue = chkLimitRemote.Checked;
				model.ValuePercentageBonus = (double)spnBonusRemote.Value;
				model.ValueDepletionPerResource = (double)spnDepletionResourceRemote.Value;
				model.BonusAffectsDepletion = chkBonusDepletionRemote.Checked;
				model.ValueDepletionPerTurn = (int)spnDepletionTurnRemote.Value;
			}
			else
			{
				model.RatePercentage = (double)spnRateStandard.Value;
				model.LimitRateToValue = chkLimitStandard.Checked;
				model.ValuePercentageBonus = (double)spnBonusStandard.Value;
				model.ValueDepletionPerResource = (double)spnDepletionResourceStandard.Value;
				model.BonusAffectsDepletion = chkBonusDepletionStandard.Checked;
				model.ValueDepletionPerTurn = (int)spnDepletionTurnStandard.Value;
			}
			int mined;
			int value = startValue;
			for (int i = 0; i < turns; i++)
			{
				mined = model.GetRate(miningRate, value, 1d);
				yield return Tuple.Create(mined, value);
				value -= model.GetDecay(miningRate, value);
			}
		}

		private void SaveChanges()
		{
			setup.GameName = txtGalaxyName.Text;
			setup.StarSystemCount = (int)spnStarSystems.Value;
			setup.Seed = (int)spnSeed.Value;
			setup.GalaxySize = new Size((int)spnWidth.Value, (int)spnHeight.Value);
			setup.StarSystemGroups = (int)spnSystemGroups.Value;
			setup.AllSystemsExplored = chkAllSystemsExplored.Checked;
			setup.OmniscientView = chkOmniscient.Checked;
			setup.EventFrequency = (double)((dynamic)ddlEventFrequency.SelectedItem).Value;
			setup.MaximumEventSeverity = (EventSeverity)ddlMaximumEventSeverity.SelectedItem;
			setup.StandardMiningModel = new MiningModel
			{
				RatePercentage = (double)spnRateStandard.Value,
				ValuePercentageBonus = (double)spnBonusStandard.Value,
				ValueDepletionPerResource = (double)spnDepletionResourceStandard.Value,
				BonusAffectsDepletion = chkBonusDepletionStandard.Checked,
				ValueDepletionPerTurn = (int)spnDepletionTurnStandard.Value,
			};
			setup.RemoteMiningModel = new MiningModel
			{
				RatePercentage = (double)spnRateRemote.Value,
				ValuePercentageBonus = (double)spnBonusRemote.Value,
				ValueDepletionPerResource = (double)spnDepletionResourceRemote.Value,
				BonusAffectsDepletion = chkBonusDepletionRemote.Checked,
				ValueDepletionPerTurn = (int)spnDepletionTurnRemote.Value,
			};
			setup.MinPlanetValue = (int)spnMinValuePlanet.Value;
			setup.MinSpawnedPlanetValue = (int)spnMinSpawnValuePlanet.Value;
			setup.HomeworldValue = (int)spnHomeworldValue.Value;
			setup.MaxSpawnedPlanetValue = (int)spnMaxSpawnValuePlanet.Value;
			setup.MaxPlanetValue = (int)spnMaxValuePlanet.Value;
			setup.MinAsteroidValue = (int)spnMinValueAsteroid.Value;
			setup.MinSpawnedAsteroidValue = (int)spnMinSpawnValueAsteroid.Value;
			setup.MaxSpawnedAsteroidValue = (int)spnMaxSpawnValueAsteroid.Value;
			switch (ddlStartTech.SelectedIndex)
			{
				case 0:
					setup.StartingTechnologyLevel = StartingTechnologyLevel.Low;
					break;

				case 1:
					setup.StartingTechnologyLevel = StartingTechnologyLevel.Medium;
					break;

				case 2:
					setup.StartingTechnologyLevel = StartingTechnologyLevel.High;
					break;
			}
			setup.TechnologyCost = (TechnologyCost)ddlTechCost.SelectedIndex;
			setup.TechnologyUniqueness = (int)((dynamic)ddlTechUniqueness.SelectedItem).Value;
			setup.ResearchPointsPerUnspentEmpirePoint = spnResearchPerUnspentEmpirePoint.Value;
			lblMaxBonusResearchFromEmpirePoints.Text = $"Empires can earn up to {((double)(setup.EmpirePoints * setup.ResearchPointsPerUnspentEmpirePoint)).ToUnitString(true)} bonus research.";
			for (int i = 0; i < lstTechs.Items.Count; i++)
			{
				if (!lstTechs.GetItemChecked(i))
					setup.ForbiddenTechnologyNames.Add(((Technology)lstTechs.Items[i]).Name);
			}
			setup.StartingResources = (int)spnStartResources.Value;
			setup.ResourceStorage = (int)spnResourceStorage.Value;
			setup.StartingResearch = (int)spnStartResearch.Value;
			setup.HomeworldsPerEmpire = (int)spnHomeworlds.Value;
			setup.HomeworldSize = (StellarSize)ddlHomeworldSize.SelectedItem;
			setup.EmpirePlacement = (EmpirePlacement)(ddlEmpirePlacement.SelectedIndex);
			setup.MaxHomeworldDispersion = (int)spnMaxDispersion.Value;
			setup.ScoreDisplay = Enum.GetValues(typeof(ScoreDisplay)).Cast<ScoreDisplay>().ElementAt(ddlScoreDisplay.SelectedIndex);
			setup.EmpirePoints = (int)spnEmpirePoints.Value;
			setup.RandomAIs = (int)spnRandomAIs.Value;
			setup.MinorEmpires = (int)spnMinorEmpires.Value;
			if (chkVictoryEliminateMajorEmpires.Checked)
				setup.VictoryConditions.Add(new MajorEmpireEliminationVictoryCondition());
			if (chkVictoryScore.Checked)
				setup.VictoryConditions.Add(new ScoreVictoryCondition((long)spnVictoryScore.Value));
			if (chkVictoryScorePercent.Checked)
				setup.VictoryConditions.Add(new ScorePercentageVictoryCondition((int)spnVictoryScorePercent.Value));
			if (chkVictoryTurns.Checked)
				setup.VictoryConditions.Add(new SurvivalVictoryCondition((int)spnVictoryTurns.Value));
			if (chkVictoryTech.Checked)
				setup.VictoryConditions.Add(new TechnologyVictoryCondition((int)spnVictoryTech.Value));
			if (chkVictoryPeace.Checked)
				setup.VictoryConditions.Add(new PeaceVictoryCondition((int)spnVictoryPeace.Value));
			setup.VictoryDelay = (int)spnVictoryDelay.Value;
			setup.IsHumansVsAI = chkHumansVsAI.Checked;
			setup.AllowedTrades = (AllowedTrades)ddlAllowedTrades.SelectedValue;
			setup.IsSurrenderAllowed = chkAllowSurrender.Checked;
			setup.IsIntelligenceAllowed = chkAllowIntel.Checked;
			setup.IsAnalysisAllowed = chkAllowAnalysis.Checked;
			setup.GenerateRandomRuins = chkRandomRuins.Checked;
			setup.GenerateUniqueRuins = chkUniqueRuins.Checked;
			setup.CanColonizeOnlyBreathable = chkColonizeOnlyBreathable.Checked;
			setup.CanColonizeOnlyHomeworldSurface = chkColonizeOnlyHWSurface.Checked;
		}

		private void spnHeight_ValueChanged(object sender, EventArgs e)
		{
			spnStarSystems.Maximum = spnWidth.Value * spnHeight.Value;
			setup.GalaxySize = new Size((int)spnWidth.Value, (int)spnHeight.Value);
		}

		private void spnMiningRate_ValueChanged(object sender, EventArgs e)
		{
			miningRate = (int)spnMiningRate.Value;
		}

		private void spnResourceStorage_ValueChanged(object sender, EventArgs e)
		{
			spnResourceStorage.Maximum = spnResourceStorage.Value;
		}

		private void spnStarSystems_ValueChanged(object sender, EventArgs e)
		{
			setup.StarSystemCount = (int)spnStarSystems.Value;
			spnSystemGroups.Maximum = spnStarSystems.Value;
		}

		private void spnStartValue_ValueChanged(object sender, EventArgs e)
		{
			startValue = (int)spnStartValue.Value;
		}

		private void spnSystemGroups_ValueChanged(object sender, EventArgs e)
		{
			setup.StarSystemGroups = (int)spnSystemGroups.Value;
		}

		private void spnWidth_ValueChanged(object sender, EventArgs e)
		{
			spnStarSystems.Maximum = spnWidth.Value * spnHeight.Value;
			setup.GalaxySize = new Size((int)spnWidth.Value, (int)spnHeight.Value);
		}

		private void btnPreviewMap_Click(object sender, EventArgs e)
		{
			progressBar.Visible = true;
			var status = new Status
			{
				Progress = 0d,
				Message = "Initializing",
				Exception = null,
			};
			Thread t = new Thread(new ThreadStart(() =>
			{
				try
				{
					// create the galaxy
					var galtemp = setup.GalaxyTemplate;
					galtemp.GameSetup = setup;
					Galaxy.Current = galtemp.Instantiate(status, 1, new PRNG((int)spnSeed.Value));
				}
				catch (Exception ex)
				{
					status.Exception = ex;
				}
			}));
			t.Name = "Map Generation";
			this.ShowChildForm(new StatusForm(t, status));
			galaxyView.ComputeWarpPointConnectivity();
			galaxyView.Invalidate();
		}

		private void spnResearchPerUnspentEmpirePoint_ValueChanged(object sender, EventArgs e)
		{
			lblMaxBonusResearchFromEmpirePoints.Text = $"Empires can earn up to {((double)(spnEmpirePoints.Value * spnResearchPerUnspentEmpirePoint.Value)).ToUnitString(true)} bonus research.";
		}

		private void spnEmpirePoints_ValueChanged(object sender, EventArgs e)
		{
			lblMaxBonusResearchFromEmpirePoints.Text = $"Empires can earn up to {((double)(spnEmpirePoints.Value * spnResearchPerUnspentEmpirePoint.Value)).ToUnitString(true)} bonus research.";
		}

		private void btnGenerateSeed_Click(object sender, EventArgs e)
		{
			spnSeed.Value = DateTime.Now.Millisecond + 1000 * DateTime.Now.Second + 60000 * DateTime.Now.Minute;
		}
	}
}