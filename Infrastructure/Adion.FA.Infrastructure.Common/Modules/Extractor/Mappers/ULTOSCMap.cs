using Adion.FA.Infrastructure.Common.Extractor.Model;
using CsvHelper.Configuration;

namespace Adion.FA.Infrastructure.Common.Extractor.Mappers
{
    public class ULTOSCMap : ClassMap<ULTOSC>
    {
        public ULTOSCMap()
        {
            Map(m => m.OptInTimePeriod1).Index(3);
            Map(m => m.OptInTimePeriod2).Index(4);
            Map(m => m.OptInTimePeriod3).Index(5);
        }
    }
}
