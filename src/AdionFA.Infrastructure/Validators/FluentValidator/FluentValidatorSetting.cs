using AdionFA.Domain.Properties;
using static AdionFA.Infrastructure.Validators.FluentValidator.FluentValidator;
namespace AdionFA.Infrastructure.Validators.FluentValidator
{
    public class FluentValidatorLanguageManager : FluentValidation.Resources.LanguageManager
    {
        public FluentValidatorLanguageManager()
        {

            // NotNull

            AddTranslation("en", BuiltInValidators.NotNull, string.Format(Resources.NotNull, FluentPlaceholders.PropertyName));
            AddTranslation("en-US", BuiltInValidators.NotNull, string.Format(Resources.NotNull, FluentPlaceholders.PropertyName));
            AddTranslation("en-GB", BuiltInValidators.NotNull, string.Format(Resources.NotNull, FluentPlaceholders.PropertyName));

            // NotEmpty

            AddTranslation("en", BuiltInValidators.NotEmpty, string.Format(Resources.NotEmpty, FluentPlaceholders.PropertyName));
            AddTranslation("en-US", BuiltInValidators.NotEmpty, string.Format(Resources.NotEmpty, FluentPlaceholders.PropertyName));
            AddTranslation("en-GB", BuiltInValidators.NotEmpty, string.Format(Resources.NotEmpty, FluentPlaceholders.PropertyName));
        }
    }

    internal static class FluentValidator
    {
        public static class FluentPlaceholders
        {
            public static readonly string PropertyName = "{PropertyName}";
            public static readonly string PropertyValue = "{PropertyValue}";
            public static readonly string ComparisonValue = "{ComparisonValue}";
            public static readonly string MinLength = "{MinLength}";
            public static readonly string MaxLength = "{MaxLength}";
            public static readonly string TotalLength = "{TotalLength}";
        }

        public static class BuiltInValidators
        {
            public static readonly string NotNull = "NotNullValidator";
            public static readonly string NotEmpty = "NotEmptyValidator";
            public static readonly string NotEqual = "NotEqualValidator";
            public static readonly string Equal = "EqualValidator";
            public static readonly string Length = "LengthValidator";
        }
    }
}
