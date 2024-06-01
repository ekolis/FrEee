using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FrEee.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FrEee.Data.Contexts;

public class PlayerCommandsContext(
	string gameName,
	int turnNumber,
	int playerNumber
) : DbContext
{
	public string GameName => gameName;

	public int TurnNumber => turnNumber;

	public int PlayerNumber => playerNumber;

	private string FileName => string.Format("{0}_{1}_{2:d4}.plr", GameName, TurnNumber, PlayerNumber);
	
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite($"Data Source={Path.Combine(
			Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
			"Savegame",
			FileName
		)}");
	}


	public DbSet<IGameObject> GameObjects { get; set; }
}
