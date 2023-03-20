using Adion.FA.Infrastructure.Common.Extractor.Model;
using CsvHelper.Configuration;

namespace Adion.FA.Infrastructure.Common.Extractor.Mappers
{
    public class ATRMap : ClassMap<ATR>
    {
        public ATRMap()
        {
            Map(m => m.OptInTimePeriod).Index(3);
        }
    }
}
