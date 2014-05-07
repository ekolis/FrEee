using System;
using System.Drawing;
using System.Windows.Forms;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System.ComponentModel;

namespace FrEee.WinForms.Controls
{
	public partial class ResourceDisplay : UserControl
	{
		public ResourceDisplay()
		{
			InitializeComponent();
		}

		private Resource resource;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Resource Resource
		{
			get { return resource; }
			set
			{
				resource = value;
				Invalidate();
			}
		}

		public string ResourceName
		{
			get { return Resource == null ? null : Resource.Name; }
			set { Resource = Resource.Find(value); }
		}

		public Color ResourceColor
		{
			get
			{
				return Resource == null ? Color.White : Resource.Color;
			}
		}

		public Image ResourceIcon
		{
			get
			{
				try
				{
					return Resource == null ? null : Resource.Icon;
				}
				catch (NullReferenceException)
				{
					// HACK - stupid forms designer thinks it's null and not null at the same time, WTF?!
					var icon = new Bitmap(1, 1);
					var color = Color.Gray;
					if (Resource != null)
						color = ResourceColor;
					var g = Graphics.FromImage(icon);
					g.Clear(color);
					return icon;
				}
			}
		}

		private int amount;
		public int Amount { get { return amount; } set { amount = value; Invalidate(); } }

		private int? change;
		public int? Change { get { return change; } set { change = value; Invalidate(); } }
		
		private void ResourceDisplay_Paint(object sender, PaintEventArgs e)
		{
			var g = e.Graphics;
			var rpos = Width;
			if (ResourceIcon != null)
			{
				// draw resource icon on the right
				g.DrawImage(ResourceIcon, Width - Height, 0, Height, Height);
				rpos -= Height + 5;
			}

			// draw text right aligned in the remaining space
			var text = Amount.ToUnitString();
			if (Change != null)
			{
				text += " (";
				if (Change.Value >= 0)
					text += "+";
				text += Change.Value.ToUnitString();
				text += ")";
			}
			var brush = new SolidBrush(ResourceColor);
			var rect = new Rectangle(0, 0, rpos, Height);
			var sf = new StringFormat();
			sf.Alignment = StringAlignment.Far;
			sf.LineAlignment = StringAlignment.Center;
			g.DrawString(text, Font, brush, rect, sf);
		}
	}
}
