using Adion.FA.Infrastructure.Enums.Attributes;
using Adion.FA.Infrastructure.I18n.Resources;

namespace Adion.FA.Infrastructure.Enums
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
