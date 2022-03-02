using Bogus;
using TestingWorkshop.Core.ViewModels;

namespace TestingWorskshopTests.Setup
{
    public class FakeCamera
    {
        public CameraViewModel Create()
        {
            return new Faker<CameraViewModel>()
                .RuleFor(x => x.IsAPSCSensor, y => y.Random.Bool())
                .RuleFor(x => x.Manufacturer, y => y.Company.CompanyName())
                .RuleFor(x => x.Model, y => y.Commerce.ProductName())
                .RuleFor(x => x.YearOfProduction, y => y.Random.Number(min: 1950, max: 2022))
                .Generate();
        }
    }
}
