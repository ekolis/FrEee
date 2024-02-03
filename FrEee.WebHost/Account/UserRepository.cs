namespace FrEee.WebHost.Account
{
	public class UserRepository
	{
		public static UserRepository Instance { get; } = new();

		public bool Contains(AccountToken token)
		{
			// TODO: authenticate user
			return true;
		}
	}
}
