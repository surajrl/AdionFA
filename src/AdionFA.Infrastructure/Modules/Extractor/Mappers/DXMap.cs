using AdionFA.Infrastructure.Extractor.Model;
using CsvHelper.Configuration;

namespace AdionFA.Infrastructure.Extractor.Mappers
{
    public sealed class DXMap : ClassMap<DX>
    {
        public DXMap()
        {
            Map(m => m.OptInTimePeriod).Index(3);
        }
    }
}
