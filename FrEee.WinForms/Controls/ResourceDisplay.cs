using System;
using System.Drawing;
using System.Windows.Forms;
using FrEee.Utility;
using FrEee.Utility.Extensions;

namespace FrEee.WinForms.Controls
{
	public partial class ResourceDisplay : UserControl
	{
		public ResourceDisplay()
		{
			InitializeComponent();
		}

		public void Bind()
		{
			lblAmount.ForeColor = ResourceColor;
			if (ResourceIcon != null)
				picIcon.Image = ResourceIcon;
			lblAmount.Text = Amount.ToUnitString();
			if (Change != null)
			{
				lblAmount.Text += " (";
				if (Change.Value >= 0)
					lblAmount.Text += "+";
				lblAmount.Text += Change.Value.ToUnitString();
				lblAmount.Text += ")";
			}
		}

		private Resource resource;
		public Resource Resource
		{
			get { return resource; }
			set
			{
				resource = value;
				Bind();
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
				catch (NullReferenceException ex)
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
		public int Amount { get { return amount; } set { amount = value; Bind(); } }

		private int? change;
		public int? Change { get { return change; } set { change = value; Bind(); } }

		private void ResourceDisplay_SizeChanged(object sender, EventArgs e)
		{
			lblAmount.MaximumSize = lblAmount.Size;
			Invalidate();
		}
	}
}
