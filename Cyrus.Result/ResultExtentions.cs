namespace Cyrus.Results;

public static class ResultExtentions
{
	public static Result<T> ToResult<T>(this T data)
	{
		var res = new Result<T>();
		res.WithData(data);
		return res;
	}


}
