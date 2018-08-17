using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Combat.Grid;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FrEee.WinForms.Controls
{
	/// <summary>
	/// Displays a map of a star system.
	/// </summary>
	public partial class BattleView : Control
	{
		public BattleView()
		{
			InitializeComponent();
			BackColor = Color.Black;
			SizeChanged += BattleView_SizeChanged;
			DoubleBuffered = true;
		}

		private Battle battle;

		public Battle Battle
		{
			get => battle;
			set
			{
				battle = value;
				UpdateData();
				Invalidate();
			}
		}

		/// <summary>
		/// Current round of battle.
		/// </summary>
		private int round = 0;

		/// <summary>
		/// The size at which each sector will be drawn, in pixels.
		/// </summary>
		public int SectorDrawSize
		{
			get
			{
				if (Battle == null)
					return 0;
				// TODO - scale differently on X and Y axes for non-square views
				return Math.Min(Width - SectorBorderSize, Height - SectorBorderSize) / Battle.GetDiameter(round) - SectorBorderSize;
			}
		}

		/// <summary>
		/// Border in pixels between sectors and around the entire map.
		/// </summary>
		private const int SectorBorderSize = 1;

		void BattleView_SizeChanged(object sender, EventArgs e)
		{
			Invalidate();
		}

		private SafeDictionary<ICombatant, IntVector2> locations = new SafeDictionary<ICombatant, IntVector2>();

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);

			pe.Graphics.Clear(BackColor);

			pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			var drawsize = SectorDrawSize;
			var bigFontSize = Math.Max(SectorDrawSize / 6, 1);
			var bigFont = new Font("Sans Serif", bigFontSize);
			var littleFontSize = Math.Max(SectorDrawSize / 8, 1);
			var littleFont = new Font("Sans Serif", littleFontSize);

			if (Battle != null)
			{
				// draw combat sectors
				for (var x = Battle.UpperLeft[round].X; x <= Battle.LowerRight[round].X; x++)
				{
					for (var y = Battle.UpperLeft[round].Y; y <= Battle.LowerRight[round].Y; y++)
					{
						// where and how big will we draw the sector?
						var drawPoint = GetDrawPoint(x, y);
						var drawx = drawPoint.X;
						var drawy = drawPoint.Y;

						var pos = new IntVector2(x, y);

						// draw image, owner flag, and name of largest space object (if any)
						var here = Battle.Combatants.Where(q => q.IsAlive && locations.Any(w => w.Key == q));
						if (here.Any())
						{
							Image pic;
							if (here.OfType<ISpaceObject>().Any())
							{
								var largest = here.OfType<ISpaceObject>().Largest();
								if (largest is SpaceVehicle v)
									pic = v.Icon.Resize(drawsize); // spacecraft get the icon, not the portrait, drawn, since the icon is topdown
								else
									pic = largest.Portrait.Resize(drawsize);
								if (largest is Planet p)
								{
									if (p.Colony != null)
										p.DrawPopulationBars(pic);
									else
										p.DrawStatusIcons(pic);
								}
								pe.Graphics.DrawImage(pic, drawx - drawsize / 2f, drawy - drawsize / 2f, drawsize, drawsize);
							}
							// also draw seekers on top
							if (here.OfType<Seeker>().Any())
							{
								// TODO - draw seeker icons
								pe.Graphics.FillEllipse(new SolidBrush(here.OfType<Seeker>().First().Owner.Color), drawx - drawsize / 4f, drawy - drawsize / 4f, drawsize / 2f, drawsize / 2f);
							}

							// TODO - draw owner flag
						}


						var availForFlagsAndNums = Math.Min(drawsize - 21, 24);
						var top = 0;
						if (availForFlagsAndNums > 0)
						{
							var cornerx = drawx - drawsize / 2;
							var cornery = drawy - drawsize / 2;
							if (here.Count() > 1)
							{
								foreach (var g in here.Except(here.OfType<Planet>()).Where(sobj => sobj.Owner != null).GroupBy(sobj => sobj.Owner))
								{
									// draw empire insignia and space object count
									var owner = g.Key;
									var fleetedCount = g.OfType<Fleet>().Sum(f => f.LeafVehicles.Count());
									var unfleetedCount = g.Except(g.OfType<Fleet>()).Count();
									var count = fleetedCount + unfleetedCount;
									var aspect = owner.Icon;
									if (owner.Icon != null)
									{
										var mode = pe.Graphics.InterpolationMode;
										if (owner.Icon.Width == 1 && owner.Icon.Height == 1)
										{
											pe.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
											pe.Graphics.DrawImage(owner.Icon, cornerx, cornery + top, bigFontSize * 2, bigFontSize * 2);
											pe.Graphics.InterpolationMode = mode;
										}
										else
											pe.Graphics.DrawImage(owner.Icon, cornerx, cornery + top, bigFontSize, bigFontSize);

									}
									top += bigFontSize;
								}
							}
						}

						if (combatPhase)
						{
							// draw pewpews and booms
							foreach (var pewpew in pewpews.Where(q => q.Start == pos))
								pe.Graphics.DrawLine(Pens.White, drawPoint, GetDrawPoint(pewpew.End.X, pewpew.End.Y));
							foreach (var boom in booms.Where(q => q.Position == pos))
								pe.Graphics.FillEllipse(Brushes.White, drawx - drawsize * boom.Size / 2f, drawy - drawsize * boom.Size / 2f, drawsize * boom.Size, drawsize * boom.Size);
						}
					}
				}
			}
		}

		private PointF GetDrawPoint(int x, int y)
		{
			var drawx = x * (SectorDrawSize + SectorBorderSize) + Width / 2f + SectorBorderSize;
			var drawy = y * (SectorDrawSize + SectorBorderSize) + Height / 2f + SectorBorderSize;
			return new PointF(drawx, drawy);
		}

		bool combatPhase = false;

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (combatPhase)
			{
				round++;
				if (round >= Mod.Current.Settings.SpaceCombatTurns)
					round = 0;
				UpdateData();
			}
			else
				combatPhase = true;
			Invalidate();
		}

		private void UpdateData()
		{
			if (Battle == null)
				return;
			pewpews.Clear();
			booms.Clear();
			foreach (var e in Battle.Events)
			{
				switch (e)
				{
					case CombatantAppearsEvent ca:
						locations[ca.Combatant] = ca.EndPosition;
						break;
					case CombatantDisappearsEvent cd:
						locations.Remove(cd.Combatant);
						booms.Add(new Boom(cd.EndPosition, 1));
						break;
					case CombatantMovesEvent cm:
						locations[cm.Combatant] = cm.EndPosition;
						break;
					case WeaponFiresEvent wf:
						pewpews.Add(new Pewpew(wf.StartPosition, wf.EndPosition));
						booms.Add(new Boom(wf.EndPosition, 0.5f));
						break;
				}
			}
		}

		private List<Pewpew> pewpews = new List<Pewpew>();

		private List<Boom> booms = new List<Boom>();

		private class Pewpew
		{
			public Pewpew(IntVector2 start, IntVector2 end)
			{
				Start = start;
				End = end;
			}

			public IntVector2 Start { get; set; }
			public IntVector2 End { get; set; }
		}

		private class Boom
		{
			public Boom(IntVector2 pos, float size)
			{
				Position = pos;
				Size = size;
			}

			public IntVector2 Position { get; set; }
			public float Size { get; set; }
		}
	}
}
