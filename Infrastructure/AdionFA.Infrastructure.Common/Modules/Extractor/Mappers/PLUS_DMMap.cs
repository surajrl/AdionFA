using AdionFA.Infrastructure.Common.Extractor.Model;
using CsvHelper.Configuration;

namespace AdionFA.Infrastructure.Common.Extractor.Mappers
{
    public class PLUS_DMMap : ClassMap<PLUS_DM>
    {
        public PLUS_DMMap()
        {
            Map(m => m.OptInTimePeriod).Index(2);
        }
    }
}
