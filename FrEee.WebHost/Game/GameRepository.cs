namespace FrEee.WebHost.Game;

public class GameRepository
{
	public static GameRepository Instance { get; } = new();

	public IEnumerable<Game> List()
	{
		// TODO: make a real game repository
		yield return testgame1;
	}

	public IEnumerable<Game> ListByOwner(string name) => List().Where(q => q.OwnerName == name);

	public IEnumerable<Game> ListByPlayer(string name) => List().Where(q => q.PlayerNames.Contains(name));

	public IEnumerable<Game> ListByUser(string name) => ListByOwner(name).Union(ListByPlayer(name));

	private Game testgame1 = new Game("A Barefoot Jaywalk", "bloodninja")
	{ 
		PlayerNames = [ "bloodninja", "lowtax", "conkerthesquirrel", "THX-1138" ]
	};

	private Game testgame2 = new Game("Such A Very Sweet Game", "tescosamoa")
	{
		PlayerNames = ["tescosamoa", "bloodninja", "bananatallyman", "lowtax"]
	};
}
