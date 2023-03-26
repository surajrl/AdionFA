using System;
using System.IO;
using System.Linq;

namespace AdionFA.Infrastructure.Common.Extensions
{
    public static class AssembleExtension
    {
        public static Type GetAssembleType(this string sourceFilePath)
        {
            if (!string.IsNullOrEmpty(sourceFilePath))
            {
                string typeName = Path.GetFileNameWithoutExtension(sourceFilePath); 
                Type type = AppDomain.CurrentDomain.GetAssemblies().SelectMany(ass => ass.GetTypes()).Where(t => t.Name == typeName).FirstOrDefault();

                return type;
            }

            return null;
        }
    }
}
