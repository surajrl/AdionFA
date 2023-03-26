using AdionFA.Infrastructure.Common.Extractor.Model;
using CsvHelper.Configuration;

namespace AdionFA.Infrastructure.Common.Extractor.Mappers
{
    public sealed class DXMap : ClassMap<DX>
    {
        public DXMap()
        {
            Map(m => m.OptInTimePeriod).Index(3);
        }
    }
}
