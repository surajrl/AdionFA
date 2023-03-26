using AdionFA.Infrastructure.Common.Extractor.Model;
using CsvHelper.Configuration;

namespace AdionFA.Infrastructure.Common.Extractor.Mappers
{
    public sealed class ADXRMap : ClassMap<ADXR>
    {
        public ADXRMap()
        {
            Map(m => m.OptInTimePeriod).Index(3);
        }
    }
}
