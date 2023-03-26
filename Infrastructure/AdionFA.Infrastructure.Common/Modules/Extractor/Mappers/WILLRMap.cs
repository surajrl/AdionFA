using AdionFA.Infrastructure.Common.Extractor.Model;
using CsvHelper.Configuration;

namespace AdionFA.Infrastructure.Common.Extractor.Mappers
{
    public class WILLRMap : ClassMap<WILLR>
    {
        public WILLRMap()
        {
            Map(m => m.OptInTimePeriod).Index(3);
        }
    }
}
