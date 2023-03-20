using Adion.FA.Core.Domain.Aggregates.Core;
using Adion.FA.Infrastructure.Core.Data.Persistence.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adion.FA.Infrastructure.Core.Data.Persistence.EFCore.AggregateConfigurations.Core
{
    public class CoreUserEntityTypeConfiguration : IEntityTypeConfiguration<CoreUser>
        , IAdionSecurityETC
    {
        public void Configure(EntityTypeBuilder<CoreUser> builder)
        {
            builder.HasIndex(u => u.UserName).IsUnique();
        }
    }
}
