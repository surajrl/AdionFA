namespace AdionFA.UI.Infrastructure.Model.Base
{
    public class ReferenceDataBaseVM : EntityBaseVM
    {
        private string _code;
        public string Code
        {
            get => _code;
            set => SetProperty(ref _code, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _value;
        public string Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }
    }
}
