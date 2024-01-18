namespace examPattern.Models
{
    public class UserInsuranceProduct
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int InsuranceProductId { get; set; }
        public InsuranceProduct InsuranceProduct { get; set; }
    }

}
