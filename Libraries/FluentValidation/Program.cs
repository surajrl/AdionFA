using FluentValidationProject;
using System.Globalization;

//var newCulture = CultureInfo.GetCultureInfo("es");
//Thread.CurrentThread.CurrentUICulture = newCulture;


FluentValidation.ValidatorOptions.Global.LanguageManager = new FluentValidatorLangManager();

var model = new EntityModel
{
    Id = 0,
    Firstname = null,
    Models = new List<EntityModel> { null, null },
};

var v = model.Validate<EntityModelValidator>();

if (v.IsValid)
    Console.WriteLine("No errors");

foreach (var e in v.Errors)
{
    Console.WriteLine(e.ErrorMessage);
}    
