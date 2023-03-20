public static class FluentValidator
{
    public static class FluentPlaceholders
    {
        public static string PropertyName = "{PropertyName}";
        public static string PropertyValue = "{PropertyValue}";
        public static string ComparisonValue = "{ComparisonValue}";
        public static string MinLength = "{MinLength}";
        public static string MaxLength = "{MaxLength}";
        public static string TotalLength = "{TotalLength}";
        public static string CollectionIndex = "{CollectionIndex}";
    }

    public static class BuiltInValidators
    {
        public static string NotNull = "NotNullValidator";
        public static string NotEmpty = "NotEmptyValidator";
        public static string NotEqual = "NotEqualValidator";
        public static string Equal = "EqualValidator";
        public static string Length = "LengthValidator";
    }
}
