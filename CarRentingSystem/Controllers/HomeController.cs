namespace CarRentingSystem.Controllers
{
    using System.Diagnostics;
    using CarRentingSystem.Data;
    using CarRentingSystem.Models;
    using CarRentingSystem.Models.Cars;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private readonly CarRentingDbContext data;

        public HomeController(CarRentingDbContext _data)
      => data = _data;

        public IActionResult Index()
        {
            var cars = this.data
                .Cars
                .OrderByDescending(c => c.Id)
                .Select(c => new CarListingViewModel
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    ImageUrl = c.ImageUrl,
                    Year = c.Year,
                    Category = c.Category.Name
                })
                .Take(3)
                .ToList();

            return View(cars);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}