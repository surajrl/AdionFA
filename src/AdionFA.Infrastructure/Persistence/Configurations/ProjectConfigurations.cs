using AdionFA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdionFA.Infrastructure.Persistence.Configurations
{
    public class ProjectConfigurations : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasIndex(project => project.ProjectName).IsUnique();
        }
    }
}
