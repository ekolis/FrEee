using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FrEee.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FrEee.Data.Contexts;

public class GameStateContext(
	string gameName,
	int turnNumber,
	int playerNumber
) : DbContext
{
	public string GameName => gameName;

	public int TurnNumber => turnNumber;

	public int PlayerNumber => playerNumber;

	private string FileName
	{
		get
		{
			if (PlayerNumber > 0)
			{
				// player game state
				return string.Format("{0}_{1}_{2:d4}.gam", GameName, TurnNumber, PlayerNumber);
			}
			else
			{
				// host game state
				return string.Format("{0}_{1}.gam", GameName, TurnNumber);
			}
		}
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite($"Data Source={Path.Combine(
			Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
			"Savegame",
			FileName
		)}");
	}

	public DbSet<GameObject> GameObjects { get; set; }
}
