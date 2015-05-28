﻿using FrEee.Game.Interfaces;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using FrEee.Game.Objects.Civilization;

namespace FrEee.Utility
{
	/// <summary>
	/// A resource in the game.
	/// </summary>
	[Serializable]
	public class Resource : INamed, IPictorial
	{
		static Resource()
		{
			all = new Resource[]
			{
				Minerals,
				Organics,
				Radioactives,
				Research,
				Intelligence,
				Supply
			};
		}

		private static IEnumerable<Resource> all;

		public static readonly Resource Minerals = new Resource
			{
				Name = "Minerals",
				Color = Color.FromArgb(128, 128, 255),
				IsGlobal = true,
				IsLocal = false,
				HasValue = true,
				PictureName = "Resource1",
				Aptitude = Aptitude.Mining,
				CultureModifier = c => c.Production,
			};

		public static readonly Resource Organics = new Resource
		{
			Name = "Organics",
			Color = Color.FromArgb(0, 192, 0),
			IsGlobal = true,
			IsLocal = false,
			HasValue = true,
			PictureName = "Resource2",
			Aptitude = Aptitude.Farming,
			CultureModifier = c => c.Production,
		};

		public static readonly Resource Radioactives = new Resource
		{
			Name = "Radioactives",
			Color = Color.FromArgb(192, 0, 0),
			IsGlobal = true,
			IsLocal = false,
			HasValue = true,
			PictureName = "Resource3",
			Aptitude = Aptitude.Refining,
			CultureModifier = c => c.Production,
		};

		public static readonly Resource Research = new Resource
		{
			Name = "Research",
			Color = Color.FromArgb(192, 0, 192),
			IsGlobal = false,
			IsLocal = false,
			HasValue = false,
			PictureName = "Resource4",
			Aptitude = Aptitude.Intelligence, // no, seriously
			CultureModifier = c => c.Research,
		};

		public static readonly Resource Intelligence = new Resource
		{
			Name = "Intelligence",
			Color = Color.FromArgb(255, 255, 255),
			IsGlobal = false,
			IsLocal = false,
			HasValue = false,
			PictureName = "Resource5",
			Aptitude = Aptitude.Cunning,
			CultureModifier = c => c.Intelligence,
		};

		public static readonly Resource Supply = new Resource
		{
			Name = "Supply",
			Color = Color.FromArgb(255, 255, 0),
			IsGlobal = false,
			IsLocal = true,
			HasValue = false,
			PictureName = "Resource6",
			Aptitude = null, // TODO - supply aptitude?
			CultureModifier = c => 0, // TODO - supply culture modifier?
		};
		
		/// <summary>
		/// All resources in the game.
		/// TODO - moddable resources?
		/// </summary>
		public static IEnumerable<Resource> All { get { return all; } }

		public static Resource Find(string name)
		{
			return All.SingleOrDefault(r => r.Name == name);
		}

		/// <summary>
		/// The name of the resource.
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// A color used to represent the resource.
		/// </summary>
		public Color Color { get; set; }

		/// <summary>
		/// Can this resource be stored empire-wide?
		/// </summary>
		public bool IsGlobal { get; set; }

		/// <summary>
		/// Can this resource be stored on a space object?
		/// </summary>
		public bool IsLocal { get; set; }

		/// <summary>
		/// Does this resource have a "value" assigned to planets and asteroids?
		/// </summary>
		public bool HasValue { get; set; }

		/// <summary>
		/// The name of the picture to use for this resource.
		/// </summary>
		public string PictureName { get; set; }

		/// <summary>
		/// An icon used to represent this resource.
		/// </summary>
		public Image Icon
		{
			get { return Pictures.GetIcon(this); }
		}

		/// <summary>
		/// Just use the icon image.
		/// </summary>
		public Image Portrait
		{
			get { return Icon; }
		}

		/// <summary>
		/// The aptitude (if any) which affects the generation rate of this resource.
		/// </summary>
		public Aptitude Aptitude { get; set; }

		/// <summary>
		/// Function to compute the cultural modifier. A modifier of zero means 100%.
		/// </summary>
		public Func<Culture, int> CultureModifier { get; set; }

		public override string ToString()
		{
			return Name;
		}

		public static bool operator ==(Resource r1, Resource r2)
		{
			if (r1.IsNull() && r2.IsNull())
				return true;
			if (r1.IsNull() || r2.IsNull())
				return false;
			return r1.Name == r2.Name && r1.Color == r2.Color && r1.IsGlobal == r2.IsGlobal && r1.IsLocal == r2.IsLocal && r1.PictureName == r2.PictureName;
		}

		public static bool operator !=(Resource r1, Resource r2)
		{
			return !(r1 == r2);
		}

		public override bool Equals(object obj)
		{
			if (obj is Resource)
				return this == (Resource)obj;
			return false;
		}

		public override int GetHashCode()
		{
			return HashCodeMasher.Mash(IsGlobal, IsLocal, Name, Color, PictureName);
		}

		public static ResourceQuantity operator *(int quantity, Resource resource)
		{
			var q = new ResourceQuantity();
			q.Add(resource, quantity);
			return q;
		}

		public static ResourceQuantity operator *(Resource r, int quantity)
		{
			return quantity * r;
		}
	}
}
