using System.Text.RegularExpressions;

namespace AdionFA.Infrastructure.Helpers
{
    public static class RegexHelper
    {
        public static string GetValidFileName(string fileName, string replaceWithChar = null)
        {
            // remove any invalid character from the filename.
            var ret = Regex.Replace(fileName.Trim(), "[^A-Za-z0-9_. ]+", replaceWithChar ?? "");
            return ret.Replace(" ", string.Empty);
        }
    }
}
