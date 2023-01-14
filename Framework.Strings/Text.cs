namespace Cyrus.Strings
{
	public class Text
	{
		public static string Fix(string text)
		{
			if (text == null)
			{
				return null;
			}

			text = text.Trim();
			if (text == string.Empty)
			{
				return null;
			}

			while (text.Contains("  "))
			{
				text = text.Replace("  ", " ");
			}



			return text;
		}

	}
}