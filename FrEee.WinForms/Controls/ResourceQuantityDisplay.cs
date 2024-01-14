using FrEee.Utility;
using FrEee.WinForms.Interfaces;
using System.Windows.Forms;

namespace FrEee.WinForms.Controls;

public partial class ResourceQuantityDisplay : UserControl, IBindable<ResourceQuantity>
{
	public ResourceQuantityDisplay()
	{
		InitializeComponent();
	}

	public ResourceQuantity ResourceQuantity
	{
		get { return q; }
		set
		{
			q = value;
			Bind();
		}
	}

	private ResourceQuantity q;

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