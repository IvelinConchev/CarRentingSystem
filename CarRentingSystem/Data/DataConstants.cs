namespace CarRentingSystem.Data
{
    public class DataConstants
    {
        public class Car
        {
            public const int CarBrandMinLength = 2;
            public const int CarBrandMaxLength = 20;
            public const int CarModelMinLength = 2;
            public const int CarModelMaxLength = 30;
            public const int CarDescriptionMinLength = 10;
            public const int CarYearMinValue = 2000;
            public const int CarYearMaxValue = 2050;
        }
        public class Category
        {
            public const int NameMaxLength = 25;
        }

        public class Dealer
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 25;
            public const int PhoneNumberMinLength = 6;
            public const int PhoneNumberMaxLength = 30;
        }
    }
}
