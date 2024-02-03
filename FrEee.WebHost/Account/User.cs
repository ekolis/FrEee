using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace FrEee.WebHost.Account;

public record User(string Username, string Password)
	: IPrincipal
{
   public AccountToken AccountToken
	{
		get
		{
			var bytes = Encoding.UTF8.GetBytes(Password + "NaCl" + Username[..1]);
			return new AccountToken(Username, SHA512.Create().ComputeHash(bytes));
		}
	}

	public IIdentity? Identity => AccountToken;

	public bool IsInRole(string role)
	{
		// TODO: implement roles
		return false;
	}
}
