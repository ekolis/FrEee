using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FrEee.Utility
{
    /// <summary>
    /// Manages dependency injection for the game.
    /// </summary>
    public static class DI
    {
        private static readonly HostApplicationBuilder builder = Host.CreateApplicationBuilder();

        private static IHost? host;

		/// <summary>
		/// Registers a singleton service.
		/// </summary>
		/// <typeparam name="TInterface"></typeparam>
		/// <typeparam name="TImplementation"></typeparam>
		/// <returns></returns>
        public static async Task RegisterSingleton<TInterface, TImplementation>()
            where TInterface : class
			where TImplementation : class, TInterface
		{
            if (host is not null)
            {
                await host.StopAsync();
            }

            builder.Services.AddSingleton<TInterface, TImplementation>();

			if (host is not null)
			{
				await Run();
			}
		}

		/// <summary>
		/// Registers a transient service.
		/// </summary>
		/// <typeparam name="TInterface"></typeparam>
		/// <typeparam name="TImplementation"></typeparam>
		/// <returns></returns>
		public static async Task RegisterTransient<TInterface, TImplementation>()
			where TInterface : class
			where TImplementation : class, TInterface
		{
			if (host is not null)
			{
				await host.StopAsync();
			}

			builder.Services.AddTransient<TInterface, TImplementation>();

			if (host is not null)
			{
				await Run();
			}
		}

		/// <summary>
		/// Runs the dependency injection host.
		/// </summary>
		/// <returns></returns>
		public async static Task Run()
        {
            host = builder.Build();
            await host.RunAsync();
        }

		/// <summary>
		/// Gets an instance of a registered service.
		/// </summary>
		/// <typeparam name="TInterface"></typeparam>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException">if DI is not yet running or no such service is registered.</exception>
		public static TInterface Get<TInterface>()
		{
			if (host is null)
			{
				throw new InvalidOperationException("Can't get items from DI without first calling Run.");
			}
			var result = host.Services.GetService<TInterface>();
			return result ?? throw new InvalidOperationException($"No service of type {typeof(TInterface)} is registered.");
		}
	}
}
