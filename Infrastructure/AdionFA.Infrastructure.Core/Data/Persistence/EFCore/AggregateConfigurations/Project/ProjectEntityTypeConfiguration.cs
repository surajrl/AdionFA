using AdionFA.Core.Domain.Aggregates.Project;
using AdionFA.Infrastructure.Core.Data.Persistence.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdionFA.Infrastructure.Core.Data.Persistence.EFCore.Configurations
{
    public class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>, IAdionFAETC
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasIndex(p => p.ProjectName).IsUnique();
        }
    }
}
