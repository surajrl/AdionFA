using AdionFA.Infrastructure.Extractor.Model;
using CsvHelper.Configuration;

namespace AdionFA.Infrastructure.Extractor.Mappers
{
    public class MINUS_DMMap : ClassMap<MINUS_DM>
    {
        public MINUS_DMMap()
        {
            Map(m => m.OptInTimePeriod).Index(2);
        }
    }
}
