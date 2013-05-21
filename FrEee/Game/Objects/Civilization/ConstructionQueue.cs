using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization
{
	public class ConstructionQueue : IOrderable<ConstructionQueue>
	{
		public ConstructionQueue()
		{
			Orders = new List<IOrder<ConstructionQueue>>();
			Galaxy.Current.Register(this);
		}

		/// <summary>
		/// Is this a space yard queue?
		/// </summary>
		public bool IsSpaceYardQueue { get; set; }

		/// <summary>
		/// Is this a colony queue?
		/// </summary>
		public bool IsColonyQueue { get; set; }

		/// <summary>
		/// Can this queue construct something?
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool CanConstruct(IConstructable item)
		{
			return (IsSpaceYardQueue || !item.RequiresSpaceYardQueue) && (IsColonyQueue || !item.RequiresColonyQueue);
		}

		/// <summary>
		/// The rate at which this queue can construct.
		/// </summary>
		public Resources Rate { get; set; }

		public IList<IOrder<ConstructionQueue>> Orders
		{
			get;
			private set;
		}

		public int ID
		{
			get;
			set;
		}

		public Empire Owner
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string IconPath
		{
			get;
			set;
		}

		[JsonIgnore]
		public Image Icon
		{
			get
			{
				return Pictures.GetCachedImage(IconPath).GetThumbnailImage(32, 32, () => false, IntPtr.Zero);
			}
		}
	}
}
