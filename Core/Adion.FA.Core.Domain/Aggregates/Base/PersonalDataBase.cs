using System;
using System.ComponentModel.DataAnnotations;

namespace Adion.FA.Core.Domain.Aggregates.Base
{
    public class PersonalDataBase : EntityBase
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        public string JobTitle { get; set; }
        public string Organization { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Picture { get; set; }
        
        public string FullName => FirstName + " " + LastName;
    }
}
