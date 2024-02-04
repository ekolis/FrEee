namespace FrEee.WebHost.Game;

public class Game(string name, string ownerName)
{
	public string Name { get; set; } = name;

	public string OwnerName { get; set;} = ownerName;

	public List<string> PlayerNames { get; set; } = [];

	public int CurrentTurnNumber { get; set; } = 0;
}
