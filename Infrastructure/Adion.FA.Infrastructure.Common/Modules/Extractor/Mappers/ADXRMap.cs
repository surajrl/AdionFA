using Adion.FA.Infrastructure.Common.Extractor.Model;
using CsvHelper.Configuration;

namespace Adion.FA.Infrastructure.Common.Extractor.Mappers
{
    public sealed class ADXRMap : ClassMap<ADXR>
    {
        public ADXRMap()
        {
            Map(m => m.OptInTimePeriod).Index(3);
        }
    }
}
