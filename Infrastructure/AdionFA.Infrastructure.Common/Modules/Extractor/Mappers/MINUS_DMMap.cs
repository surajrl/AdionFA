using AdionFA.Infrastructure.Common.Extractor.Model;
using CsvHelper.Configuration;

namespace AdionFA.Infrastructure.Common.Extractor.Mappers
{
    public class MINUS_DMMap : ClassMap<MINUS_DM>
    {
        public MINUS_DMMap()
        {
            Map(m => m.OptInTimePeriod).Index(2);
        }
    }
}
