using Adion.FA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Organization
{

    [Table(nameof(OrganizationUnit))]
    public class OrganizationUnit : EntityBase
    {
        [Key]
        public int OrganizationUnitId { get; set; }

        public int? OrganizationId { get; set; }
        [ForeignKey(nameof(OrganizationId))]
        public Organization Organization { get; set; }

        public int? ParentOrganizationUnitId { get; set; }
        [ForeignKey(nameof(ParentOrganizationUnitId))]
        public OrganizationUnit ParentOrganizationUnit { get; set; }


        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
