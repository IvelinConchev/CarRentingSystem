namespace CarRentingSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Dealer;
    public class Dealer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }
        public IEnumerable<Car> Cars { get; set; } = new List<Car>();
    }
}
