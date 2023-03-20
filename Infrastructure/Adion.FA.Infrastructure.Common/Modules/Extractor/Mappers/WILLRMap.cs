using Adion.FA.Infrastructure.Common.Extractor.Model;
using CsvHelper.Configuration;

namespace Adion.FA.Infrastructure.Common.Extractor.Mappers
{
    public class WILLRMap : ClassMap<WILLR>
    {
        public WILLRMap()
        {
            Map(m => m.OptInTimePeriod).Index(3);
        }
    }
}
