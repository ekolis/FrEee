using System.Security.Principal;

namespace FrEee.WebHost.Account;

public record AccountToken(string Name, byte[] PasswordHash)
	: IIdentity
{
	public string? AuthenticationType { get; } = "Bearer";
	public bool IsAuthenticated => UserRepository.Instance.Contains(this);
}
