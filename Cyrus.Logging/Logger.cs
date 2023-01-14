using Cyrus.Logging.Enums;
using Cyrus.Logging.Interfaces;
using Cyrus.Logging.Models;
using Microsoft.AspNetCore.Http;
using System.Collections;

namespace Cyrus.Logging
{
	public abstract class Logger<T> : object, ILogger<T>
	{
		protected Logger(IHttpContextAccessor httpContextAccessor = null) : base()
		{
			HttpContextAccessor = httpContextAccessor;
		}

		protected IHttpContextAccessor HttpContextAccessor { get; init; }

		protected ExceptionModel GetExceptions(System.Exception exception)
		{

			if (exception == null)
			{
				return default;
			}

			var oex = new ExceptionModel()
			{
				Exception = exception?.Message,
				InnerException = exception?.InnerException?.Message,
			};

			return oex;

		}

		protected Hashtable GetParameters(Hashtable parameters)
		{
			if (parameters != null)
			{
				return parameters;
			}
			return default;
		}

		protected void Log
			(LogLevel level,
			System.Reflection.MethodBase methodBase,
			string message,
			System.Exception exception = null,
			Hashtable parameters = null)
		{
			if (exception == null && string.IsNullOrWhiteSpace(message))
			{
				return;
			}

			// **************************************************
			string currentCultureName =
				System.Threading.Thread.CurrentThread.CurrentCulture.Name;

			System.Globalization.CultureInfo newCultureInfo =
				new System.Globalization.CultureInfo(name: "en-US");

			System.Globalization.CultureInfo currentCultureInfo =
				new System.Globalization.CultureInfo(currentCultureName);

			System.Threading.Thread.CurrentThread.CurrentCulture = newCultureInfo;
			// **************************************************

			Log log = new Log();

			log.Level = level;

			log.ClassName = typeof(T).Name;
			log.MethodName = methodBase.Name;
			log.Namespace = typeof(T).Namespace;

			if (HttpContextAccessor != null &&
				HttpContextAccessor.HttpContext != null &&
				HttpContextAccessor.HttpContext.Connection != null &&
				HttpContextAccessor.HttpContext.Connection.RemoteIpAddress != null)
			{
				log.RemoteIP =
					HttpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
			}

			if (HttpContextAccessor != null &&
				HttpContextAccessor.HttpContext != null &&
				HttpContextAccessor.HttpContext.User != null &&
				HttpContextAccessor.HttpContext.User.Identity != null)
			{
				log.Username =
					HttpContextAccessor.HttpContext.User.Identity.Name;
			}

			if (HttpContextAccessor != null &&
				HttpContextAccessor.HttpContext != null &&
				HttpContextAccessor.HttpContext.Request != null)
			{
				log.RequestPath =
					HttpContextAccessor.HttpContext.Request.Path;

				log.HttpReferrer =
					HttpContextAccessor.HttpContext.Request.Headers["Referer"];
			}

			log.Message = message;

			log.Exceptions =
				GetExceptions(exception: exception);

			log.Parameters =
				GetParameters(parameters: parameters);

			LogByFavoriteLibrary(log: log, exception: exception);

			// **************************************************
			System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureInfo;
			// **************************************************
		}

		protected abstract void LogByFavoriteLibrary(Log log, System.Exception exception);

		public void LogTrace
			(string message, Hashtable parameters = null)
		{
			System.Diagnostics.StackTrace
				stackTrace = new System.Diagnostics.StackTrace();

			System.Reflection.MethodBase
				methodBase = stackTrace.GetFrame(1).GetMethod();

			Log(methodBase: methodBase,
				level: LogLevel.Trace,
				message: message,
				exception: null,
				parameters: parameters);
		}

		public void LogDebug
			(string message, Hashtable parameters = null)
		{
			System.Diagnostics.StackTrace
				stackTrace = new System.Diagnostics.StackTrace();

			System.Reflection.MethodBase
				methodBase = stackTrace.GetFrame(1).GetMethod();

			Log(methodBase: methodBase,
				level: LogLevel.Debug,
				message: message,
				exception: null,
				parameters: parameters);
		}

		public void LogInformation
			(string message, Hashtable parameters = null)
		{
			System.Diagnostics.StackTrace
				stackTrace = new System.Diagnostics.StackTrace();

			System.Reflection.MethodBase
				methodBase = stackTrace.GetFrame(1).GetMethod();

			Log(methodBase: methodBase,
				level: LogLevel.Information,
				message: message,
				exception: null,
				parameters: parameters);
		}

		public void LogWarning
			(string message, Hashtable parameters = null)
		{
			System.Diagnostics.StackTrace
				stackTrace = new System.Diagnostics.StackTrace();

			System.Reflection.MethodBase
				methodBase = stackTrace.GetFrame(1).GetMethod();

			Log(methodBase: methodBase,
				level: LogLevel.Warning,
				message: message,
				exception: null,
				parameters: parameters);
		}

		public void LogError
			(System.Exception exception = null, string message = null, Hashtable parameters = null)
		{
			System.Diagnostics.StackTrace
				stackTrace = new System.Diagnostics.StackTrace();

			System.Reflection.MethodBase
				methodBase = stackTrace.GetFrame(1).GetMethod();

			Log(methodBase: methodBase,
				level: LogLevel.Error,
				message: message,
				exception: exception,
				parameters: parameters);
		}

		public void LogCritical
			(System.Exception exception = null, string message = null, Hashtable parameters = null)
		{
			System.Diagnostics.StackTrace
				stackTrace = new System.Diagnostics.StackTrace();

			System.Reflection.MethodBase
				methodBase = stackTrace.GetFrame(1).GetMethod();

			Log(methodBase: methodBase,
				level: LogLevel.Critical,
				message: message,
				exception: exception,
				parameters: parameters);
		}
	}
}
