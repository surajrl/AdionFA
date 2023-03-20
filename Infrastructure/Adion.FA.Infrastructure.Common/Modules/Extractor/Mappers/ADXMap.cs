using Adion.FA.Infrastructure.Common.Extractor.Model;
using CsvHelper.Configuration;

namespace Adion.FA.Infrastructure.Common.Extractor.Mappers
{
    public sealed class ADXMap : ClassMap<ADX>
    {
        public ADXMap()
        {
            Map(m => m.OptInTimePeriod).Index(3);
        }
    }
}
