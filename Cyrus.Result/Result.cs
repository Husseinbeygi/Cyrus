using System.Collections;

namespace Cyrus.Results
{
	public class Result<T> : ResultMessages
	{
		public Result()
		{
			SetStatusSucceeded();
		}

		public T Data { get; private set; }

		public int Count
		{
			get
			{
				if (Data is null)
				{
					return 0;
				}

				if (Data as IList is not null)
				{
					var d = Data as IList;
					if (d is not null)
					{
						return d.Count;
					}
					return 1;
				}
				else if (Data as ICollection is not null)
				{
					var d = Data as ICollection;
					if (d is not null)
					{
						return d.Count;
					}
					return 1;
				}
				return 1;
			}
		}

		public int Page
		{
			get
			{
				if (Count > 200)
				{
					return (int)Math.Ceiling(Count / (double)200);
				}
				return 1;
			}
		}

		public bool HasNextPage
		{
			get
			{
				if (Page != 1)
				{
					return true;
				}
				return false;
			}
		}

		public void WithData(T data)
		{
			Data = data;
		}

	}
}