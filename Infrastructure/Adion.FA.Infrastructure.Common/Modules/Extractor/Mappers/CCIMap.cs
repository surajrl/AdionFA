using Adion.FA.Infrastructure.Common.Extractor.Model;
using CsvHelper.Configuration;

namespace Adion.FA.Infrastructure.Common.Extractor.Mappers
{
    public sealed class CCIMap : ClassMap<CCI>
    {
        public CCIMap()
        {
            Map(m => m.OptInTimePeriod).Index(3);
        }
    }
}
