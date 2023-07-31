using AdionFA.Infrastructure.Extractor.Model;
using CsvHelper.Configuration;

namespace AdionFA.Infrastructure.Extractor.Mappers
{
    public class ATRMap : ClassMap<ATR>
    {
        public ATRMap()
        {
            Map(m => m.OptInTimePeriod).Index(3);
        }
    }
}
