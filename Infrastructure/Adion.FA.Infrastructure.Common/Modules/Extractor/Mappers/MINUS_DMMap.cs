using Adion.FA.Infrastructure.Common.Extractor.Model;
using CsvHelper.Configuration;

namespace Adion.FA.Infrastructure.Common.Extractor.Mappers
{
    public class MINUS_DMMap : ClassMap<MINUS_DM>
    {
        public MINUS_DMMap()
        {
            Map(m => m.OptInTimePeriod).Index(2);
        }
    }
}
