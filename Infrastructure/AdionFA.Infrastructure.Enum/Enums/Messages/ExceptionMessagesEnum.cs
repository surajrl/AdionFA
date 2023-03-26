using AdionFA.Infrastructure.Enums.Attributes;

namespace AdionFA.Infrastructure.Enums
{
    public enum ExceptionMessagesEnum
    {
        [Metadata(descriptionKey:"Directory not found")]
        DirectoryNotFound = 1,
        
        [Metadata(descriptionKey: "File not found")]
        FileNotFound = 2
    }
}
