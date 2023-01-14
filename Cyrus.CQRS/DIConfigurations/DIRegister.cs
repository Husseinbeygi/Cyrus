using Cyrus.CQRS.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Cyrus.CQRS.DIConfigurations
{
	public static class DIRegister
	{
		public static IServiceCollection AddCQRS(this IServiceCollection services)
		{
			services.AddTransient<IMessages, Messages>();

			return services;
		}

		public static void AddHandlers(this IServiceCollection services, Assembly assembly)
		{
			List<Type> handlerTypes = assembly.GetTypes()
				.Where(x => x.GetInterfaces()
				.Any(y => IsHandlerInterface(y)))
				.Where(x => x.Name.EndsWith("Handler"))
				.ToList();

			foreach (Type type in handlerTypes)
			{
				services.AddHandler(type);
			}
		}

		public static void AddHandler(this IServiceCollection services, Type type)
		{
			object[] attributes = type.GetCustomAttributes(false);

			List<Type> pipeline = attributes
				.Select(x => ToDecorator(x))
				.Where(x => x.Name.StartsWith("Nullable") == false)
				.Concat(new[] { type })
				.Reverse()
				.ToList();

			Type interfaceType = type.GetInterfaces().Single(y => IsHandlerInterface(y));
			Func<IServiceProvider, object> factory = BuildPipeline(pipeline, interfaceType);

			services.AddTransient(interfaceType, factory);
		}

		private static Func<IServiceProvider, object> BuildPipeline(List<Type> pipeline, Type interfaceType)
		{
			List<ConstructorInfo> ctors = pipeline
				.Select(x =>
				{

					Type type =
					x.IsGenericType
					?
					x.MakeGenericType(interfaceType.GenericTypeArguments)
					:
					x;

					return type.GetConstructors().SingleOrDefault();
				})
				.ToList();

			Func<IServiceProvider, object> func = provider =>
			{
				object current = null;

				foreach (ConstructorInfo ctor in ctors)
				{
					List<ParameterInfo> parameterInfos = ctor.GetParameters().ToList();

					object[] parameters = GetParameters(parameterInfos, current, provider);

					current = ctor.Invoke(parameters);
				}

				return current;
			};

			return func;
		}

		private static object[] GetParameters(List<ParameterInfo> parameterInfos, object current, IServiceProvider provider)
		{
			var result = new object[parameterInfos.Count];

			for (int i = 0; i < parameterInfos.Count; i++)
			{
				result[i] = GetParameter(parameterInfos[i], current, provider);
			}

			return result;
		}

		private static object GetParameter(ParameterInfo parameterInfo, object current, IServiceProvider provider)
		{
			Type parameterType = parameterInfo.ParameterType;

			if (IsHandlerInterface(parameterType))
				return current;

			object service = provider.GetService(parameterType);
			if (service != null)
				return service;

			throw new ArgumentException($"Type {parameterType} not found");
		}

		private static Type ToDecorator(object attribute)
		{
			Type type = attribute.GetType();


			return type;
			//if (type == typeof(DatabaseRetryAttribute))
			//	return typeof(DatabaseRetryDecorator<>);

			//if (type == typeof(AuditLogAttribute))
			//	return typeof(AuditLoggingDecorator<>);

			// other attributes go here

			//throw new ArgumentException(attribute.ToString());
		}

		private static bool IsHandlerInterface(Type type)
		{
			if (!type.IsGenericType)
				return false;

			Type typeDefinition = type.GetGenericTypeDefinition();

			return
				typeDefinition == typeof(IEventHandlerAsync<>) ||
				typeDefinition == typeof(ICommandHandler<,>) ||
				typeDefinition == typeof(ICommandHandlerAsync<,>) ||
				typeDefinition == typeof(IQueryHandler<,>) ||
				typeDefinition == typeof(IQueryHandlerAsync<,>);
		}

	}
}
