using System.ComponentModel.DataAnnotations;

namespace examPattern.Models
{
    public class InsuranceProduct
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(2), MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public InsuranceCategory Category { get; set; }

        [Required]
        public InsuranceType Type { get; set; }

        [Required]
        public InsuranceUserType UserType { get; set; }

        [Required]
        public decimal InsurancePremium { get; set; }

        [Required, MinLength(1000)]
        public string TermsOfService { get; set; }


        // Navigation property for the many-to-many relationship
        public ICollection<UserInsuranceProduct> UserInsuranceProducts { get; set; }
    }

}
