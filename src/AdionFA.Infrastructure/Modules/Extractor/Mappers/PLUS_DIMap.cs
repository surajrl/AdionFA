using AdionFA.Infrastructure.Extractor.Model;
using CsvHelper.Configuration;

namespace AdionFA.Infrastructure.Extractor.Mappers
{
    public sealed class PLUS_DIMap : ClassMap<PLUS_DI>
    {
        public PLUS_DIMap()
        {
            Map(m => m.OptInTimePeriod).Index(3);
        }
    }
}
