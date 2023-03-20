using Adion.FA.Infrastructure.Common.Extractor.Model;
using CsvHelper.Configuration;

namespace Adion.FA.Infrastructure.Common.Extractor.Mappers
{
    public class PLUS_DMMap : ClassMap<PLUS_DM>
    {
        public PLUS_DMMap()
        {
            Map(m => m.OptInTimePeriod).Index(2);
        }
    }
}
