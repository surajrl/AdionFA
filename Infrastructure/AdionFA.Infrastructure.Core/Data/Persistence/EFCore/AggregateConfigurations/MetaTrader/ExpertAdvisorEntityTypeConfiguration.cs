using AdionFA.Core.Domain.Aggregates.MetaTrader;
using AdionFA.Infrastructure.Core.Data.Persistence.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdionFA.Infrastructure.Core.Data.Persistence.EFCore.AggregateConfigurations.MetaTrader
{
    public class ExpertAdvisorEntityTypeConfiguration : IEntityTypeConfiguration<ExpertAdvisor>, IAdionFAETC
    {
        public void Configure(EntityTypeBuilder<ExpertAdvisor> builder)
        {
            builder.HasIndex(ea => ea.Name).IsUnique();
            builder.HasIndex(ea => ea.ResponsePort).IsUnique();
            builder.HasIndex(ea => ea.PushPort).IsUnique();
            builder.HasIndex(ea => ea.MagicNumber).IsUnique();
        }
    }
}
