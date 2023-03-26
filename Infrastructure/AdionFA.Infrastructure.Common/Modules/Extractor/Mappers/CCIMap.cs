using AdionFA.Infrastructure.Common.Extractor.Model;
using CsvHelper.Configuration;

namespace AdionFA.Infrastructure.Common.Extractor.Mappers
{
    public sealed class CCIMap : ClassMap<CCI>
    {
        public CCIMap()
        {
            Map(m => m.OptInTimePeriod).Index(3);
        }
    }
}
