using AdionFA.Domain.Entities;
using AdionFA.Infrastructure.Persistance.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdionFA.Infrastructure.Persistence.EFCore.AggregateConfigurations
{
    public class ExpertAdvisorEntityTypeConfiguration : IEntityTypeConfiguration<ExpertAdvisor>, IAdionFAETC
    {
        public void Configure(EntityTypeBuilder<ExpertAdvisor> builder)
        {
            builder.HasIndex(ea => ea.Name).IsUnique();
            builder.HasIndex(ea => ea.ResponsePort).IsUnique();
            builder.HasIndex(ea => ea.PublisherPort).IsUnique();
            builder.HasIndex(ea => ea.MagicNumber).IsUnique();
        }
    }
}
