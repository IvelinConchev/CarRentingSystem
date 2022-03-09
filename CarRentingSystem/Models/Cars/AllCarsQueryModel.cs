namespace CarRentingSystem.Models.Cars
{
    using System.ComponentModel.DataAnnotations;

    public class AllCarsQueryModel
    {
        public IEnumerable<string> Brands { get; init; }

        [Display(Name = "Search")]
        public string SearchTerm { get; init; }

        public CarSorting Sorting { get; init; }

        public IEnumerable<CarListingViewModel> Cars { get; init; }
    }
}
