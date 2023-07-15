using AdionFA.Domain.Entities;
using AdionFA.Infrastructure.Persistance.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdionFA.Infrastructure.Persistence.EFCore.AggregateConfigurations
{
    public class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>, IAdionFAETC
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasIndex(project => project.ProjectName).IsUnique();
        }
    }
}
