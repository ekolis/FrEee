using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrEee.WinForms.Interfaces;
using FrEee.Utility;

namespace FrEee.WinForms.Controls
{
	public partial class ResourceQuantityDisplay : UserControl, IBindable<ResourceQuantity>
	{
		public ResourceQuantityDisplay()
		{
			InitializeComponent();
		}

		private ResourceQuantity q;

		public ResourceQuantity ResourceQuantity
		{
			get { return q; }
			set
			{
				q = value;
				Bind();
			}
		}

		public void Bind(ResourceQuantity data)
		{
			ResourceQuantity = data;
		}

		public void Bind()
		{
			if (ResourceQuantity == null)
			{
				min.Amount = 0;
				org.Amount = 0;
				rad.Amount = 0;
			}
			else
			{
				min.Amount = ResourceQuantity[Resource.Minerals];
				org.Amount = ResourceQuantity[Resource.Organics];
				rad.Amount = ResourceQuantity[Resource.Radioactives];
			}
		}
	}
}
