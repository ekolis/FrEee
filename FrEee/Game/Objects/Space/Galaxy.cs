using System.Collections.Generic;
using System.Linq;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Serialization;
using System;
using FrEee.Game.Interfaces;

namespace FrEee.Game.Objects.Space
{
	/// <summary>
	/// A galaxy in which the game is played.
	/// </summary>
	public class Galaxy
	{
		/// <summary>
		/// The current galaxy. Shouldn't change except at loading a game or turn processing.
		/// </summary>
		public static Galaxy Current { get; set; }

		public Galaxy()
		{
			StarSystemLocations = new List<ObjectLocation<StarSystem>>();
			Empires = new List<Empire>();
			TurnNumber = 24000;
		}

		/// <summary>
		/// The locations of the star systems in the galaxy.
		/// </summary>
		public ICollection<ObjectLocation<StarSystem>> StarSystemLocations { get; private set; }

		/// <summary>
		/// The empires participating in the game.
		/// </summary>
		public IList<Empire> Empires { get; private set; }

		/// <summary>
		/// The empire whose turn it is.
		/// </summary>
		public Empire CurrentEmpire { get; set; }

		public int MinX { get { return StarSystemLocations.MinOrDefault(ssl => ssl.Location.X); } }

		public int MinY { get { return StarSystemLocations.MinOrDefault(ssl => ssl.Location.Y); } }

		public int MaxX { get { return StarSystemLocations.MaxOrDefault(ssl => ssl.Location.X); } }

		public int MaxY { get { return StarSystemLocations.MaxOrDefault(ssl => ssl.Location.Y); } }

		public int Width { get { return MaxX - MinX + 1; } }

		public int Height { get { return MaxY - MinY + 1; } }

		/// <summary>
		/// Removes any space objects, etc. that the current empire cannot see.
		/// </summary>
		public void Redact()
		{
			if (CurrentEmpire != null)
			{
				foreach (var ssl in StarSystemLocations)
				{
					ssl.Item.Redact(this);
				}
			}
		}

		/// <summary>
		/// The current turn number.
		/// </summary>
		public int TurnNumber { get; set; }
		
		/// <summary>
		/// The current stardate. Advances 0.1 years per turn.
		/// </summary>
		public string Stardate
		{
			get
			{
				return (TurnNumber / 10.0).ToString("0.0");
			}
		}

		/// <summary>
		/// Serializes the game state.
		/// </summary>
		/// <returns></returns>
		public string SerializeGameState()
		{
			var sw = new StringWriter();
			JsonSerializer.Serialize(sw, this);
			sw.Close();
			return sw.ToString();
		}

		/// <summary>
		/// Serializes the player's commands.
		/// </summary>
		/// <exception cref="InvalidOperationException">if no current empire</exception>
		/// <returns></returns>
		public string SerializeCommands()
		{
			if (CurrentEmpire == null)
				throw new InvalidOperationException("Can't serialize commands if there is no current empire.");

			var sw = new StringWriter();
			JsonSerializer.Serialize(sw, CurrentEmpire.Commands);
			sw.Close();
			return sw.ToString();
		}

		/// <summary>
		/// Deserializes the game state.
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static Galaxy DeserializeGameState(TextReader reader)
		{
			return JsonSerializer.Deserialize<Galaxy>(new JsonTextReader(reader));
		}

		/// <summary>
		/// Deserializes the player's commands.
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static IList<ICommand> DeserializeCommands(TextReader reader)
		{
			return JsonSerializer.Deserialize<IList<ICommand>>(new JsonTextReader(reader));
		}

		private static JsonSerializer jsonSerializer;

		/// <summary>
		/// A JSON serializer used to save game state and commands.
		/// </summary>
		public static JsonSerializer JsonSerializer
		{
			get
			{
				if (jsonSerializer == null)
				{
					var js = new JsonSerializer();
					js.TypeNameHandling = TypeNameHandling.All;
#if DEBUG
					js.Formatting = Formatting.Indented;
#endif
					// TODO - use a variant of this code http://daniel.wertheim.se/2010/11/06/json-net-private-setters/ so I don't have to put [JsonIgnore] everywhere there's a property with no setter
					var cr = new DefaultContractResolver();
					cr.DefaultMembersSearchFlags |= System.Reflection.BindingFlags.NonPublic;
					js.ContractResolver = cr;
					js.PreserveReferencesHandling = PreserveReferencesHandling.All;
					jsonSerializer = js;
				}
				return jsonSerializer;
			}
		}

		/// <summary>
		/// Processes the turn.
		/// </summary>
		/// <exception cref="InvalidOperationException">if the current empire is not null, or this galaxy is not the current galaxy..</exception>
		public void ProcessTurn()
		{
			if (CurrentEmpire != null)
				throw new InvalidOperationException("Can't process the turn if there is a current empire. Load the game host's view of the galaxy instead.");

			if (Galaxy.Current != this)
				throw new InvalidOperationException("Can't process the turn on a galaxy that is not the current galaxy. Set Galaxy.Current = this first.");

			// empire stuff
			foreach (var emp in Empires)
			{
				// give empire its income
				emp.StoredResources += emp.Income;

				// execute commands
				foreach (var cmd in emp.Commands)
					cmd.Execute();
			}

			// TODO - other turn processing stuff
		}
	}
}
