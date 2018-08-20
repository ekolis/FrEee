using FrEee.Game.Interfaces;
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
		#region Private Fields

		/// <summary>
		/// Border in pixels between sectors and around the entire map.
		/// </summary>
		private const int SectorBorderSize = 1;

		private Battle battle;

		private List<Boom> booms = new List<Boom>();

		private bool combatPhase = false;

		private SafeDictionary<ICombatant, IntVector2> locations = new SafeDictionary<ICombatant, IntVector2>();

		private List<Pewpew> pewpews = new List<Pewpew>();

		/// <summary>
		/// Current round of battle.
		/// </summary>
		private int round = 0;

		#endregion Private Fields

		#region Public Constructors

		public BattleView()
		{
			InitializeComponent();
			BackColor = Color.Black;
			SizeChanged += BattleView_SizeChanged;
			DoubleBuffered = true;
		}

		#endregion Public Constructors

		#region Public Properties

		private bool autoZoom;

		/// <summary>
		/// Automatically zoom to show the entire battle?
		/// </summary>
		public bool AutoZoom
		{
			get => autoZoom;
			set
			{
				autoZoom = value;
				Invalidate();
			}
		}

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

		private IntVector2 focusedLocation;

		/// <summary>
		/// The combat sector which is focused.
		/// </summary>
		public IntVector2 FocusedLocation
		{
			get => focusedLocation;
			set
			{
				focusedLocation = value;
				Invalidate();
			}
		}

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
				if (AutoZoom)
					return Math.Min(Width - SectorBorderSize, Height - SectorBorderSize) / Battle.GetDiameter(round) - SectorBorderSize;
				return 36; // se4 shipsets have 36 pixel minis :)
			}
		}

		private bool useSquares;

		/// <summary>
		/// Display everything as a square rather than an icon?
		/// </summary>
		public bool UseSquares
		{
			get => useSquares;
			set
			{
				useSquares = value;
				Invalidate();
			}
		}

		#endregion Public Properties

		#region Protected Methods

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);

			pe.Graphics.Clear(BackColor);

			if (Battle == null)
				return;

			pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			var drawsize = SectorDrawSize;
			var bigFontSize = Math.Max(SectorDrawSize / 6, 1);
			var bigFont = new Font("Sans Serif", bigFontSize);
			var littleFontSize = Math.Max(SectorDrawSize / 8, 1);
			var littleFont = new Font("Sans Serif", littleFontSize);

			/*pe.Graphics.DrawRectangle(Pens.White, 0, 0,
				(Battle.LowerRight[round].X - Battle.UpperLeft[round].X) * (SectorDrawSize + SectorBorderSize) + SectorBorderSize,
				(Battle.LowerRight[round].Y - Battle.UpperLeft[round].Y) * (SectorDrawSize + SectorBorderSize) + SectorBorderSize);*/

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
						var here = Battle.Combatants.Where(q => q.IsAlive && locations.Any(w => w.Key == q && w.Value == pos));
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
								if (useSquares)
									pe.Graphics.FillRectangle(new SolidBrush(largest.Owner.Color), drawx - drawsize / 2f, drawy - drawsize / 2f, drawsize, drawsize);
								else
									pe.Graphics.DrawImage(pic, drawx - drawsize / 2f, drawy - drawsize / 2f, drawsize, drawsize);
							}
							// also draw seekers on top
							if (here.OfType<Seeker>().Any())
							{
								// TODO - draw seeker icons
								pe.Graphics.FillEllipse(new SolidBrush(here.OfType<Seeker>().First().Owner.Color), drawx - drawsize / 4f, drawy - drawsize / 4f, drawsize / 2f, drawsize / 2f);
							}

							// TODO - draw owner flag & objet name?
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

		#endregion Protected Methods

		#region Private Methods

		private void BattleView_SizeChanged(object sender, EventArgs e)
		{
			Invalidate();
		}

		private PointF GetDrawPoint(int x, int y)
		{
			if (AutoZoom)
			{
				var drawx = (x - Battle.UpperLeft[round].X) * (SectorDrawSize + SectorBorderSize) + SectorBorderSize;
				var drawy = (y - Battle.UpperLeft[round].Y) * (SectorDrawSize + SectorBorderSize) + SectorBorderSize;
				return new PointF(drawx, drawy);
			}
			else
			{
				if (FocusedLocation == null)
					FocusedLocation = (Battle.LowerRight[round] - Battle.UpperLeft[round]) / 2;
				var drawx = (x - FocusedLocation.X) * (SectorDrawSize + SectorBorderSize) + SectorBorderSize + Width / 2;
				var drawy = (y - FocusedLocation.Y) * (SectorDrawSize + SectorBorderSize) + SectorBorderSize + Height / 2;
				return new PointF(drawx, drawy);
			}
		}

		private IntVector2 GetClickPoint(int x, int y)
		{
			if (AutoZoom)
			{
				var clickx = (x - SectorBorderSize) / (SectorDrawSize + SectorBorderSize) + Battle.UpperLeft[round].X;
				var clicky = (y - SectorBorderSize) / (SectorDrawSize + SectorBorderSize) + Battle.UpperLeft[round].Y;
				return new IntVector2(clickx, clicky);
			}
			else
			{
				var clickx = (x - (SectorBorderSize - Width / 2)) / (SectorDrawSize + SectorBorderSize) + SectorBorderSize;
				var clicky = (y - (SectorBorderSize - Height / 2)) / (SectorDrawSize + SectorBorderSize) + SectorBorderSize;
				return new IntVector2(clickx, clicky);
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (Battle == null)
				return;
			if (combatPhase)
			{
				round++;
				if (round >= Battle.Duration)
					round = 0;
				UpdateData();
				combatPhase = false;
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
			foreach (var e in Battle.Events[round])
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

		#endregion Private Methods

		#region Private Classes

		private class Boom
		{
			#region Public Constructors

			public Boom(IntVector2 pos, float size)
			{
				Position = pos;
				Size = size;
			}

			#endregion Public Constructors

			#region Public Properties

			public IntVector2 Position { get; set; }
			public float Size { get; set; }

			#endregion Public Properties
		}

		private class Pewpew
		{
			#region Public Constructors

			public Pewpew(IntVector2 start, IntVector2 end)
			{
				Start = start;
				End = end;
			}

			#endregion Public Constructors

			#region Public Properties

			public IntVector2 End { get; set; }
			public IntVector2 Start { get; set; }

			#endregion Public Properties
		}

		#endregion Private Classes

		private void BattleView_MouseDown(object sender, MouseEventArgs e)
		{
			ClickLocation = GetClickPoint(e.X, e.Y);
		}

		public IntVector2 ClickLocation { get; private set; }
	}
}