using System;

namespace AdionFA.UI.Station.Infrastructure.Model.Base
{
    public abstract class TimeSensitiveBaseVM : EntityBaseVM
    {
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        private string _description;

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
    }
}