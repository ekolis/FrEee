using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrEee.WebHost.Authentication
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
		public User? Login([FromBody] User user)
		{
			// TODO: validate username and password
			return user;
		}
	}
}
