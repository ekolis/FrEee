using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrEee.WebHost.Game;

[ApiController]
[Route("[controller]/[action]")]
public class GameController : ControllerBase
{
	private readonly ILogger<GameController> _logger;

	public GameController(ILogger<GameController> logger)
	{
		_logger = logger;
	}

	[HttpGet]
	[Authorize]
	public IEnumerable<Game> List()
	{
		var username = HttpContext.User.Identity.Name;

		if (username == null)
		{
			return [];
		}

		return GameRepository.Instance.ListByUser(username);
	}
}
