using System;

namespace Adion.FA.UI.Station.Infrastructure.Model.Base
{
    public abstract class TimeSensitiveBaseVM : EntityBaseVM
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }


        string description;
        public string Description
        { 
            get => description; 
            set => SetProperty(ref description, value); }
    }
}
