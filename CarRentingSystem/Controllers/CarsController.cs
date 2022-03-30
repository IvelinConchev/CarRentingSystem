namespace CarRentingSystem.Controllers
{
    using CarRentingSystem.Infrastructure;
    using CarRentingSystem.Models.Cars;
    using CarRentingSystem.Services.Cars;
    using CarRentingSystem.Services.Dealers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CarsController : Controller
    {
        private readonly ICarService cars;
        private readonly IDealerService dealers;
        //private readonly CarRentingDbContext data;

        public CarsController(
            ICarService _cars,
            IDealerService _dealers)
        {
            //this.data = data;
            this.cars = _cars;
            this.dealers = _dealers;
        }


        public IActionResult All([FromQuery] AllCarsQueryModel query)
        {
            var queryResult = this.cars.All(
                query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllCarsQueryModel.CarsPerPage);

            var carBrands = this.cars.AllBrands();

            query.Brands = carBrands;
            query.TotalCars = queryResult.TotalCars;
            query.Cars = queryResult.Cars;

            return View(query);
            //var carsQuery = this.data.Cars.AsQueryable();

            //if (!string.IsNullOrWhiteSpace(query.Brand))
            //{
            //    carsQuery = carsQuery.Where(c => c.Brand == query.Brand);
            //}

            //if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            //{
            //    carsQuery = carsQuery.Where(c =>
            //        (c.Brand + " " + c.Model).ToLower().Contains(query.SearchTerm.ToLower()) ||
            //        c.Description.ToLower().Contains(query.SearchTerm.ToLower()));
            //}

            //carsQuery = query.Sorting switch
            //{
            //    CarSorting.Year => carsQuery.OrderByDescending(c => c.Year),
            //    CarSorting.BrandAndModel => carsQuery.OrderBy(c => c.Brand).ThenBy(c => c.Model),
            //    CarSorting.DateCreated or _ => carsQuery.OrderByDescending(c => c.Id)
            //};

            //var totalCars = carsQuery.Count();

            //var cars = carsQuery
            //    .Skip((query.CurrentPage - 1) * AllCarsQueryModel.CarsPerPage)
            //    .Take(AllCarsQueryModel.CarsPerPage)
            //    .Select(c => new CarListingViewModel
            //    {
            //        Id = c.Id,
            //        Brand = c.Brand,
            //        Model = c.Model,
            //        Year = c.Year,
            //        ImageUrl = c.ImageUrl,
            //        Category = c.Category.Name
            //    })
            //    .ToList();

            //var carBrands = this.data
            //    .Cars
            //    .Select(c => c.Brand)
            //    .Distinct()
            //    .OrderBy(br => br)
            //    .ToList();

            //query.TotalCars = totalCars;
            //query.Brands = carBrands;
            //query.Cars = cars;

            //return View(query);
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myCars = this.cars.ByUser(this.User.Id());

            return View(myCars);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.dealers.IsDealer(this.User.Id()))
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            return View(new CarFormModel
            {
                Categories = this.cars.AllCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(CarFormModel car)
        {
            var dealerId = this.dealers.IdByUser(this.User.Id());

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            if (!this.cars.CategoryExists(car.CategoryId))
            {
                this.ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist.");
            }

            if (ModelState.IsValid)
            {
                car.Categories = this.cars.AllCategories();

                return View(car);
            }

            this.cars.Create(
                  car.Brand,
                  car.Model,
                  car.Description,
                  car.ImageUrl,
                  car.Year,
                  car.CategoryId,
                  dealerId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!this.dealers.IsDealer(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            var car = this.cars.Details(id);

            if (car.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            return View(new CarFormModel
            {
                Brand = car.Brand,
                Model = car.Model,
                Description = car.Description,
                ImageUrl = car.ImageUrl,
                Year = car.Year,
                CategoryId = car.CategoryId,
                Categories = this.cars.AllCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, CarFormModel car)
        {
            var dealerId = this.dealers.IdByUser(this.User.Id());

            if (dealerId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            if (!this.cars.CategoryExists(car.CategoryId))
            {
                this.ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist.");
            }

            if (ModelState.IsValid)
            {
                car.Categories = this.cars.AllCategories();

                return View(car);
            }

            if (!this.cars.IsByDealer(id, dealerId) && !User.IsAdmin())
            {
                return BadRequest();
            }

             this.cars.Edit(
                  id,
                  car.Brand,
                  car.Model,
                  car.Description,
                  car.ImageUrl,
                  car.Year,
                  car.CategoryId);

            return RedirectToAction(nameof(All));
        }
        //private bool UserIsDealer()
        //    => this.data
        //        .Dealers
        //        .Any(d => d.UserId == this.User.GetId());

        //private IEnumerable<CarCategoryServiceModel> GetCarCategories()
        //    => this.data
        //        .Categories
        //        .Select(c => new CarCategoryServiceModel
        //        {
        //            Id = c.Id,
        //            Name = c.Name
        //        })
        //        .ToList();
    }
}
