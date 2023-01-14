using Cyrus.Logging.Enums;
using Cyrus.Logging.Models;
using System.Collections;

namespace Cyrus.Logging.Interfaces
{
	public interface ILog
	{

		LogLevel Level { get; set; }


		string Namespace { get; set; }

		string ClassName { get; set; }

		string MethodName { get; set; }

		string TraceId { get; set; }

		string RemoteIP { get; set; }

		string Username { get; set; }

		string RequestPath { get; set; }

		string HttpReferrer { get; set; }


		string Message { get; set; }

		Hashtable Parameters { get; set; }

		ExceptionModel Exceptions { get; set; }
	}
}
