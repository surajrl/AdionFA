using AdionFA.Infrastructure.Extractor.Model;
using CsvHelper.Configuration;

namespace AdionFA.Infrastructure.Extractor.Mappers
{
    public class PLUS_DMMap : ClassMap<PLUS_DM>
    {
        public PLUS_DMMap()
        {
            Map(m => m.OptInTimePeriod).Index(2);
        }
    }
}
