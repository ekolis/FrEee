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
        private static HostApplicationBuilder builder = Host.CreateApplicationBuilder();

        private static IHost? host;

		/// <summary>
		/// Registers a singleton service.
		/// </summary>
		/// <typeparam name="TInterface"></typeparam>
		/// <typeparam name="TImplementation"></typeparam>
		/// <returns></returns>
        public static void RegisterSingleton<TInterface, TImplementation>()
            where TInterface : class
			where TImplementation : class, TInterface
		{
			if (host is not null)
			{
				throw new InvalidOperationException("Can't register a service while the service host is running.");
			}
			builder.Services.AddSingleton<TInterface, TImplementation>();
		}

		/// <summary>
		/// Registers a transient service.
		/// </summary>
		/// <typeparam name="TInterface"></typeparam>
		/// <typeparam name="TImplementation"></typeparam>
		/// <returns></returns>
		public static void RegisterTransient<TInterface, TImplementation>()
			where TInterface : class
			where TImplementation : class, TInterface
		{
			if (host is not null)
			{
				throw new InvalidOperationException("Can't register a service while the service host is running.");
			}
			builder.Services.AddTransient<TInterface, TImplementation>();
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
		/// Resets the dependency injection host.
		/// Unregisters all services.
		/// </summary>
		/// <returns></returns>
		public async static Task Reset()
		{
			if (host is not null)
			{
				await host.StopAsync();
				host = null;
				builder = new HostApplicationBuilder();
			}
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
				// start services (don't await, run in background)
				Run();
			}

			TInterface? result = host.Services.GetService<TInterface>();

			return result ?? throw new InvalidOperationException($"No service of type {typeof(TInterface)} is registered.");
		}
	}
}
