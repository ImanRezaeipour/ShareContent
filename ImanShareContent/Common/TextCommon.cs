using System.Linq;

namespace ImanShareContent.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class TextCommon
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Clear()
        {
            return "";
        }

        public string RemoveTags(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, @"<[^>]+>|&nbsp;", "").Trim();
        }

        public string RemoveSpace(string input)
        {
            return new System.Text.RegularExpressions.Regex(@"[ ]{2,}", System.Text.RegularExpressions.RegexOptions.None).Replace(System.Text.RegularExpressions.Regex.Replace(input, @"\t|\n|\r", ""), @" ").Trim();
        }

        public string WordCount(string input)
        {
            return input.Split(' ').Count().ToString();
        }

        public string UniqueId(string name)
        {
            return name + "-" + System.Guid.NewGuid().ToString();
        }
    }
}
