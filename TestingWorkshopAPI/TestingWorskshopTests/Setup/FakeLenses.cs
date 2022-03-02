using Bogus;
using TestingWorkshop.Core.ViewModels;

namespace TestingWorskshopTests.Setup
{
    public class FakeLenses
    {
        public LensesViewModel Create()
        {
            return new Faker<LensesViewModel>()
                .RuleFor(x => x.IsAPSCSensor, y => y.Random.Bool())
                .RuleFor(x => x.Manufacturer, y => y.Company.CompanyName())
                .RuleFor(x => x.Model, y => y.Commerce.ProductName())
                .RuleFor(x => x.CompatibleCameraName, y => y.Company.CompanyName())
                .RuleFor(x => x.Diameter, y => y.Random.Number(min: 40, max: 120))
                .RuleFor(x => x.FocalLength, y => y.Random.Number(min: 14, max: 1800))
                .Generate();
        }
    }
}
