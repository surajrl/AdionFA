using Adion.FA.Infrastructure.Common.Extractor.Model;
using CsvHelper.Configuration;

namespace Adion.FA.Infrastructure.Common.Extractor.Mappers
{
    public sealed class PLUS_DIMap : ClassMap<PLUS_DI>
    {
        public PLUS_DIMap()
        {
            Map(m => m.OptInTimePeriod).Index(3);
        }
    }
}
