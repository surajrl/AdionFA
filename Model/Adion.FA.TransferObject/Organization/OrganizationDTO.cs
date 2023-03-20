using Adion.FA.TransferObject.Base;

namespace Adion.FA.TransferObject.Organization
{
    public class OrganizationDTO : EntityBaseDTO
    {
        public string OrganizationId { get; set; }

        public string ParentOrganizationId { get; set; }
        public OrganizationDTO ParentOrganization { get; set; }

        public string Name { get; set; }
        public string LegalName { get; set; }
    }
}
