using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommandLine.Entities
{
    [Table(nameof(MasterData))]
    public class MasterData
    {
        [Key]
        public int MasterDataId { get; set; }

        public string PolicyNumber { get; set; }
        public int PolicyId { get; set; }
        public int? IssuerId { get; set; }
        public string Code { get; set; }
        public string PolicyStatusName { get; set; }
        public int? MemberId { get; set; }
        public int? ContactId { get; set; }
        public int? MemberTypeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? ContactTypeId { get; set; }
        public string ContactType { get; set; }
        public int? LineOfBusinessId { get; set; }

        public int? CityId { get; set; }
        public string Street { get; set; }
        public int? ProvinceId { get; set; }
        public int? CountryId { get; set; }
        public string AddresType { get; set; }
        public string Country { get; set; }
        public string CountryName { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Pais { get; set; }
    }
}
