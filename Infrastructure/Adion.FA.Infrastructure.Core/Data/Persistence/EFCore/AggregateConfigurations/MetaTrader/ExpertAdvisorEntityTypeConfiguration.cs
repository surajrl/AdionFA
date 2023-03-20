using Adion.FA.Core.Domain.Aggregates.MetaTrader;
using Adion.FA.Infrastructure.Core.Data.Persistence.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adion.FA.Infrastructure.Core.Data.Persistence.EFCore.AggregateConfigurations.MetaTrader
{
    public class ExpertAdvisorEntityTypeConfiguration : IEntityTypeConfiguration<ExpertAdvisor>, IAdionFAETC
    {
        public void Configure(EntityTypeBuilder<ExpertAdvisor> builder)
        {
            builder.HasIndex(ea => ea.Name).IsUnique();
            builder.HasIndex(ea => ea.REPPort).IsUnique();
            builder.HasIndex(ea => ea.PUSHPort).IsUnique();
            builder.HasIndex(ea => ea.MagicNumber).IsUnique();
        }
    }
}
