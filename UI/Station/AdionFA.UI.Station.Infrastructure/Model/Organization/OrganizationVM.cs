using AdionFA.UI.Station.Infrastructure.Model.Base;

namespace AdionFA.UI.Station.Infrastructure.Model.Organization
{
    public class OrganizationVM : EntityBaseVM
    {
        public string OrganizationId { get; set; }

        public string ParentOrganizationId { get; set; }
        public OrganizationVM ParentOrganization { get; set; }

        public string Name { get; set; }
        public string LegalName { get; set; }
    }
}
