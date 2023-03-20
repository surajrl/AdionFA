using FluentValidationProject.Resources;
using static FluentValidator;

namespace FluentValidationProject
{
    internal class FluentValidatorLangManager : FluentValidation.Resources.LanguageManager
    {
        public FluentValidatorLangManager()
        {
            #region NotNull

            AddTranslation("en-US", BuiltInValidators.NotNull, string.Format(ValidationResources.CannotBeNull, FluentPlaceholders.PropertyName));
            AddTranslation("es", BuiltInValidators.NotNull, string.Format(ValidationResources_es.CannotBeNull, FluentPlaceholders.PropertyName));

            #endregion

            #region NotEmpty

            AddTranslation("en-US", BuiltInValidators.NotEmpty, string.Format(ValidationResources.IsRequired, FluentPlaceholders.PropertyName));
            AddTranslation("es", BuiltInValidators.NotEmpty, string.Format(ValidationResources_es.IsRequired, FluentPlaceholders.PropertyName));

            #endregion
        }
    }

    
}
