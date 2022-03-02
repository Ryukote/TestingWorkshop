using System.Collections.Generic;
using TestingWorkshop.Core.Contracts;
using TestingWorkshop.Core.Entities;
using TestingWorkshop.Core.ViewModels;

namespace TestingWorkshopAPI.DataAccess.Mappers
{
    public class CameraMapper : IMapper<Camera, CameraViewModel>
    {
        public Camera ToModel(CameraViewModel viewModel)
        {
            return new Camera()
            {
                Id = viewModel.Id,
                Manufacturer = viewModel.Manufacturer,
                Model = viewModel.Model,
                IsAPSCSensor = viewModel.IsAPSCSensor,
                YearOfProduction = viewModel.YearOfProduction
            };
        }

        public CameraViewModel ToViewModel(Camera model)
        {
            return new CameraViewModel()
            {
                Id = model.Id,
                Manufacturer = model.Manufacturer,
                Model = model.Model,
                IsAPSCSensor = model.IsAPSCSensor,
                YearOfProduction = model.YearOfProduction
            };
        }

        public ICollection<CameraViewModel> ToViewModelCollection(ICollection<Camera> modelCollection)
        {
            var cameraCollection = new List<CameraViewModel>();

            foreach (var item in modelCollection)
            {
                cameraCollection.Add(this.ToViewModel(item));
            }

            return cameraCollection;
        }
    }
}
