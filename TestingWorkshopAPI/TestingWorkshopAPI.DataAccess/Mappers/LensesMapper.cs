using System.Collections.Generic;
using TestingWorkshop.Core.Contracts;
using TestingWorkshop.Core.Entities;
using TestingWorkshop.Core.ViewModels;

namespace TestingWorkshopAPI.DataAccess.Mappers
{
    public class LensesMapper : IMapper<Lenses, LensesViewModel>
    {
        public Lenses ToModel(LensesViewModel viewModel)
        {
            return new Lenses()
            {
                Id = default,
                Manufacturer = viewModel.Manufacturer,
                Model = viewModel.Model,
                IsAPSCSensor = viewModel.IsAPSCSensor,
                CompatibleCameraName = viewModel.CompatibleCameraName,
                Diameter = viewModel.Diameter,
                FocalLength = viewModel.FocalLength
            };
        }

        public LensesViewModel ToViewModel(Lenses model)
        {
            return new LensesViewModel()
            {
                Id = model.Id,
                Manufacturer = model.Manufacturer,
                Model = model.Model,
                IsAPSCSensor = model.IsAPSCSensor,
                CompatibleCameraName = model.CompatibleCameraName,
                Diameter = model.Diameter,
                FocalLength = model.FocalLength
            };
        }

        public ICollection<LensesViewModel> ToViewModelCollection(ICollection<Lenses> modelCollection)
        {
            var cameraCollection = new List<LensesViewModel>();

            foreach (var item in modelCollection)
            {
                cameraCollection.Add(this.ToViewModel(item));
            }

            return cameraCollection;
        }
    }
}
