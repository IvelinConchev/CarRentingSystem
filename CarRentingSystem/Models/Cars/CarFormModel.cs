namespace CarRentingSystem.Models.Cars
{
    using CarRentingSystem.Services.Cars;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Car;
    public class CarFormModel
    {
        [Required]
        [StringLength(CarBrandMaxLength, MinimumLength = CarBrandMinLength, ErrorMessage = "Minimum: {2}, Maximum: {1}")]
        public string Brand { get; init; }

        [Required]
        [StringLength(CarModelMaxLength, MinimumLength = CarBrandMinLength, ErrorMessage = "Minimum: {2}, Maximum: {1}")]
        public string Model { get; init; }

        [Required()]
        [StringLength(int.MaxValue,
            MinimumLength = CarDescriptionMinLength,
            ErrorMessage = "The field Description must be a string with a minimum length of {2}.")]
        public string Description { get; init; }

        [Display(Name = "Image URL")]
        [Required]
        [Url]
        public string ImageUrl { get; init; }

        [Range(CarYearMinValue, CarYearMaxValue)]
        public int Year { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<CarCategoryServiceModel> Categories { get; set; }
    }
}
