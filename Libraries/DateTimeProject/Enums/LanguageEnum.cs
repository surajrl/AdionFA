using DateTimeProject.Attributes;

namespace DateTimeProject.Enums
{
    public enum LanguageEnum
    {
        [CodeLang("en")]
        English = 1,

        [CodeLang("es")]
        Spanish = 2,

        [CodeLang("pt")]
        Portuguese = 3,

        [CodeLang("jp")]
        Japanese = 4,

        [CodeLang("zh")]
        Chinese = 5
    }
}
