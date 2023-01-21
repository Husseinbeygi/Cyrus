namespace Cyrus.Types.String
{
    public class Text
    {

        /// <summary>
        ///  Trim spaces in the middle of Text 
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns></returns>
        public static string TrimExtra(string text)
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