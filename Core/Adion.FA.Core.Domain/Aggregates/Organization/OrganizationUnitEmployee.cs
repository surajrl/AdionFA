using Adion.FA.Core.Domain.Aggregates.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adion.FA.Core.Domain.Aggregates.Organization
{

    [Table(nameof(OrganizationUnitEmployee))]
    public class OrganizationUnitEmployee : EntityBase
    {
        [Key]
        public int OrganizationUnitEmployeeId { get; set; }

        public int OrganizationUnitId { get; set; }
        [ForeignKey(nameof(OrganizationUnitId))]
        public OrganizationUnit OrganizationUnit { get; set; }

        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; }

        public int? ManagerId { get; set; }
        [ForeignKey(nameof(ManagerId))]
        public Employee Manager { get; set; }

        public string JobTitle { get; set; }
    }
}
