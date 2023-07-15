using AdionFA.Domain.Attributes;

namespace AdionFA.Domain.Enums
{
    public enum ExceptionMessagesEnum
    {
        [Metadata(descriptionKey: "Directory not found")]
        DirectoryNotFound = 1,

        [Metadata(descriptionKey: "File not found")]
        FileNotFound = 2
    }
}
