using Adion.FA.Infrastructure.Common.Extractor.Model;
using CsvHelper.Configuration;

namespace Adion.FA.Infrastructure.Common.Extractor.Mappers
{
    public sealed class DXMap : ClassMap<DX>
    {
        public DXMap()
        {
            Map(m => m.OptInTimePeriod).Index(3);
        }
    }
}
