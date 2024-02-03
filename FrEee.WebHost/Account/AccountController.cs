using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace FrEee.WebHost.Account
{
    [ApiController]
	[Route("[controller]/[action]")]
	public class AccountController : ControllerBase
	{
		private readonly ILogger<AccountController> _logger;

		public AccountController(ILogger<AccountController> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		public AccountToken? Login([FromBody] User user)
		{
			var token = user.AccountToken;
			if (token.IsAuthenticated)
			{
				Thread.CurrentPrincipal = user;
				if (HttpContext != null)
				{
					HttpContext.User = new ClaimsPrincipal(user);
				}
				return token;
			}
			else
			{
				return null;
			}
		}
	}
}
