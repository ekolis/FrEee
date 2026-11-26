using FrEee.UI.Blazor.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace FrEee.UI.Blazor;

public static class SessionExtensions
{
	extension(ProtectedSessionStorage session)
	{
		public async Task<LoadableTurn?> GetCurrentTurn()
		{
			return (await session.GetAsync<LoadableTurn?>(Keys.CurrentTurn)).Value;
		}

		public async Task SetCurrentTurn(LoadableTurn turn)
		{
			await session.SetAsync(Keys.CurrentTurn, turn);
		}

		public async Task ClearCurrentTurn()
		{
			await session.DeleteAsync(Keys.CurrentTurn);
		}
	}

	private static class Keys
	{
		public const string CurrentTurn = "CurrentTurn";	
	}
}