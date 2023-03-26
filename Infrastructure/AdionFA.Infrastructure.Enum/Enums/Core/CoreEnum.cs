using AdionFA.Infrastructure.Enums.Attributes;
using AdionFA.Infrastructure.I18n.Resources;

namespace AdionFA.Infrastructure.Enums
{
    public static class CoreEnum
    {
        public enum UserType
        {
            [Metadata("Employee", "Employee", "Employee", resourceType: typeof(EnumResources))]
            Employee = 1
        }
    }
}
