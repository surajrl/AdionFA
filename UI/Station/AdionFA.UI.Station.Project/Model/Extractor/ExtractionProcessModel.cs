using AdionFA.UI.Station.Infrastructure.Base;

namespace AdionFA.UI.Station.Project.Model.Extractor
{
    public class ExtractionProcessModel : ViewModelBase
    {
        public string Path { get; set; }

        private string _templateName;

        public string TemplateName
        {
            get => _templateName;
            set => SetProperty(ref _templateName, value);
        }

        private string _status;

        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        private string _extractionName;

        public string ExtractionName
        {
            get => _extractionName;
            set => SetProperty(ref _extractionName, value);
        }

        private string _message;

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }
    }
}