using Cyrus.Logging.Adapter;
using Cyrus.Logging.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Cyrus.Logging.DIConfigurations;

public static class DIRegister
{
	public static IServiceCollection
	AddNLogServer(this IServiceCollection services)
	{
		services.AddTransient
		   (serviceType: typeof(ILogger<>),
		   implementationType: typeof(NLogAdapter<>));

		return services;

	}
}
