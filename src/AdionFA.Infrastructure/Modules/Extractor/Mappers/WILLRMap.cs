using AdionFA.Infrastructure.Extractor.Model;
using CsvHelper.Configuration;

namespace AdionFA.Infrastructure.Extractor.Mappers
{
    public class WILLRMap : ClassMap<WILLR>
    {
        public WILLRMap()
        {
            Map(m => m.OptInTimePeriod).Index(3);
        }
    }
}
