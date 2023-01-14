namespace Cyrus.DateTime;

public static class DateTime
{

	public static long GetCurrentUnixUTCTimeMilliseconds()
	{
		DateTimeOffset now = DateTimeOffset.UtcNow;
		long unixTimeMilliseconds = now.ToUnixTimeMilliseconds();
		return unixTimeMilliseconds;
	}

	public static long GetCurrentUnixUTCTimeSeconds()
	{
		var now = DateTimeOffset.UtcNow;
		long unixTimeMilliseconds = now.ToUnixTimeSeconds();
		return unixTimeMilliseconds;
	}

	public static System.DateTime FromUnixUtcTimeMilliseconds(this long time)
	{
		if (time < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(time));
		}

		var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(time);

		return dateTimeOffset.DateTime;
	}

	public static System.DateTime FromUnixUtcTimeSeconds(this long time)
	{
		if (time < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(time));
		}

		var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(time);

		return dateTimeOffset.DateTime;
	}

	public static System.DateTime ToLocalTimeMilliseconds(this long time)
	{
		if (time < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(time));
		}

		var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(time);


		return dateTimeOffset.DateTime.ToLocalTime();
	}

	public static System.DateTime ToLocalTimeSeconds(this long time)
	{
		if (time < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(time));
		}

		var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(time);

		return dateTimeOffset.DateTime.ToLocalTime();
	}

	public static System.DateTime ConvertToTimeZoneMilliseconds(this long time, double UTCOffset)
	{
		if (time < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(time));
		}

		var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(time);


		return dateTimeOffset.DateTime.AddHours(UTCOffset);
	}

	public static System.DateTime ConvertToTimeZoneSeconds(this long time, double UTCOffset)
	{
		if (time < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(time));
		}

		var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(time);


		return dateTimeOffset.DateTime.AddHours(UTCOffset);
	}


}