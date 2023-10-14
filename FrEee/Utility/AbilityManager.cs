using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Interfaces;
using FrEee.Objects.Abilities;
using FrEee.Objects.Civilization;

namespace FrEee.Utility
{
	public class AbilityManager
	{
		/// <summary>
		/// Is the ability cache enabled?
		/// Always enabled on the client side; only when a flag is set on the server side.
		/// </summary>
		public bool IsCacheEnabled
		{
			get
			{
				return The.Empire is not null || IsServerSideCacheEnabled;
			}
		}

		/// <summary>
		/// Is the ability cache enabled server side?
		/// </summary>
		public bool IsServerSideCacheEnabled { get; private set; }

		/// <summary>
		/// Disables the server side ability cache.
		/// </summary>
		public void DisableServerSideCache()
		{
			IsServerSideCacheEnabled = false;
			Cache.Clear();
			CommonCache.Clear();
			SharedCache.Clear();
		}

		/// <summary>
		/// Enables the server side ability cache.
		/// </summary>
		public void EnableServerSideCache()
		{
			IsServerSideCacheEnabled = true;
		}

		/// <summary>
		/// Cache of abilities that are shared to empires from other objects due to treaties.
		/// </summary>
		[DoNotSerialize]
		internal SafeDictionary<Tuple<IOwnableAbilityObject, Empire>, IEnumerable<Ability>> SharedCache { get; } = new();

		/// <summary>
		/// Cache of abilities belonging to game objects.
		/// </summary>
		[DoNotSerialize]
		internal SafeDictionary<IAbilityObject, IEnumerable<Ability>> Cache { get; } = new();

		/// <summary>
		/// Cache of abilities belonging to common game objects that can have different abilities for each empire.
		/// </summary>
		[DoNotSerialize]
		internal SafeDictionary<Tuple<ICommonAbilityObject, Empire>, IEnumerable<Ability>> CommonCache { get; } = new();
	}
}
