using Adion.FA.UI.Station.Infrastructure.Base;

namespace Adion.FA.UI.Station.Project.Model.Extractor
{
    public class ExtractionProcessModel : ViewModelBase
    {
        public string Path { get; set; }

        private string templateName;
        public string TemplateName 
        { 
            get => templateName; 
            set => SetProperty(ref templateName, value);
        }


        private string status;
        public string Status 
        { 
            get => status; 
            set => SetProperty(ref status, value);
        }


        private string extractionName;
        public string ExtractionName 
        { 
            get => extractionName; 
            set => SetProperty(ref extractionName, value); 
        }


        private string message;
        public string Message 
        {
            get => message; 
            set => SetProperty(ref message, value);
        }
    }

}
