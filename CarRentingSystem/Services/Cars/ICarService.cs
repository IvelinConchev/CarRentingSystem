namespace CarRentingSystem.Services.Cars
{
    using CarRentingSystem.Models.Cars;

    public interface ICarService
    {
        CarQueryServiceModel All(
            string brand,
            string searchTerm,
            CarSorting sorting,
            int currentPage,
            int carsPerPage);
        //string userId = null);

        CarDetailsServiceModel Details(int carId);

        int Create(
                string brand,
                string model,
                string description,
                string imageUrl,
                int year,
                int categoryId,
                int dealerId);

        bool Edit(
               int carId,
               string brand,
               string model,
               string description,
               string imageUrl,
               int year,
               int categoryIdd);
        IEnumerable<CarServiceModel> ByUser(string userId);

        bool IsByDealer(int carId, int dealerId);

        IEnumerable<string> AllBrands();

        IEnumerable<CarCategoryServiceModel> AllCategories();

        bool CategoryExists(int categoryId);
    }
}
