using AdionFA.Infrastructure.Extractor.Model;
using CsvHelper.Configuration;

namespace AdionFA.Infrastructure.Extractor.Mappers
{
    public sealed class ADXRMap : ClassMap<ADXR>
    {
        public ADXRMap()
        {
            Map(m => m.OptInTimePeriod).Index(3);
        }
    }
}
