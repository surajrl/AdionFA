using Adion.FA.Infrastructure.Enums.Attributes;

namespace Adion.FA.Infrastructure.Enums
{
    public enum ExceptionMessagesEnum
    {
        [Metadata(descriptionKey:"Directory not found")]
        DirectoryNotFound = 1,
        
        [Metadata(descriptionKey: "File not found")]
        FileNotFound = 2
    }
}
