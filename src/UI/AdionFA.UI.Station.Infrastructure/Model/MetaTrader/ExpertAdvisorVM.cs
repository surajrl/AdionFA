using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.Project;

namespace AdionFA.UI.Station.Infrastructure.Model.MetaTrader
{
    public class ExpertAdvisorVM : EntityBaseVM
    {
        public int ExpertAdvisorId { get; set; }

        public string Name { get; set; }

        private string _magicNumber;
        public string MagicNumber
        {
            get => _magicNumber;
            set => SetProperty(ref _magicNumber, value);
        }

        private string _host;
        public string Host
        {
            get => _host;
            set => SetProperty(ref _host, value);
        }

        private string _responsePort;
        public string ResponsePort
        {
            get => _responsePort;
            set => SetProperty(ref _responsePort, value);
        }

        private string _publisherPort;
        public string PublisherPort
        {
            get => _publisherPort;
            set => SetProperty(ref _publisherPort, value);
        }

        public ProjectVM Project { get; set; }
        public int ProjectId { get; set; }
    }
}
