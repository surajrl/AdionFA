using AdionFA.Infrastructure.Common.Extractor.Model;
using CsvHelper.Configuration;

namespace AdionFA.Infrastructure.Common.Extractor.Mappers
{
    public sealed class MINUS_DIMap : ClassMap<MINUS_DI>
    {
        public MINUS_DIMap()
        {
            Map(m => m.OptInTimePeriod).Index(3);
        }
    }
}
