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

    public static class FluentValidator
    {
        public static class FluentPlaceholders
        {
            public static string PropertyName => "{PropertyName}";
            public static string PropertyValue => "{PropertyValue}";
            public static string ComparisonValue => "{ComparisonValue}";
            public static string MinLength => "{MinLength}";
            public static string MaxLength => "{MaxLength}";
            public static string TotalLength => "{TotalLength}";
        }

        public static class BuiltInValidators
        {
            public static string NotNull => "NotNullValidator";
            public static string NotEmpty => "NotEmptyValidator";
            public static string NotEqual => "NotEqualValidator";
            public static string Equal => "EqualValidator";
            public static string Length => "LengthValidator";
        }
    }
}
