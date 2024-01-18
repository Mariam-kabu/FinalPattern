using System.ComponentModel.DataAnnotations;

namespace examPattern.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property for the many-to-many relationship
        public ICollection<UserInsuranceProduct> UserInsuranceProducts { get; set; }
    }

}
