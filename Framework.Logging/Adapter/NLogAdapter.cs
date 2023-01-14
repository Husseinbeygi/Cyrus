using Cyrus.Logging.Enums;
using Cyrus.Logging.Models;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace Cyrus.Logging.Adapter
{
	public class NLogAdapter<T> : Logger<T>
	{
		private readonly IHttpContextAccessor httpContextAccessor;

		public NLogAdapter
			(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
			this.httpContextAccessor = httpContextAccessor;
		}

		protected override void LogByFavoriteLibrary(Log log, System.Exception exception)
		{
			log.TraceId = Activity.Current?.Id ?? httpContextAccessor.HttpContext.TraceIdentifier;

			string loggerMessage = log.ToString();

			NLog.Logger logger =
				NLog.LogManager.GetLogger(name: typeof(T).ToString());

			switch (log.Level)
			{
				case LogLevel.Trace:
					{
						logger.Trace
							(exception, message: loggerMessage);

						break;
					}

				case LogLevel.Debug:
					{
						logger.Debug
							(exception, message: loggerMessage);

						break;
					}

				case LogLevel.Information:
					{
						logger.Info
							(exception, message: loggerMessage);

						break;
					}

				case LogLevel.Warning:
					{
						logger.Warn
							(exception, message: loggerMessage);

						break;
					}

				case LogLevel.Error:
					{
						logger.Error
							(exception, message: loggerMessage);

						break;
					}

				case LogLevel.Critical:
					{
						logger.Fatal
							(exception, message: loggerMessage);

						break;
					}
			}
		}
	}
}
