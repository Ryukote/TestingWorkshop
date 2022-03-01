using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TestingWorkshop.Core.Contracts;
using TestingWorkshop.Core.Entities;
using TestingWorkshop.Core.ViewModels;

namespace TestingWorkshopAPI.DataAccess.Handlers
{
    public class CameraHandler : IHandler<CameraViewModel>
    {
        private TestingWorkshopContext _context;
        public CameraHandler(TestingWorkshopContext context)
        {
            _context = context;
        }

        public async Task<Guid?> Add(CameraViewModel viewModel)
        {
            try
            {
                var camera = new Camera()
                {
                    Id = Guid.NewGuid(),
                    Model = viewModel.Model,
                    Manufacturer = viewModel.Manufacturer,
                    IsAPSCSensor = viewModel.IsAPSCSensor,
                    YearOfProduction = viewModel.YearOfProduction
                };

                _context.Camera.Add(camera);

                if (await _context.SaveChangesAsync() == 1)
                {
                    return camera.Id;
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int?> Delete(Guid id)
        {
            try
            {
                var camera = await _context.Camera
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (camera != null)
                {
                    _context.Camera.Remove(camera);
                    return await _context.SaveChangesAsync();
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CameraViewModel> GetById(Guid id)
        {
            try
            {
                var camera = await _context.Camera
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                return (camera != null)
                    ? new CameraViewModel()
                        {
                            Id = camera.Id,
                            IsAPSCSensor = camera.IsAPSCSensor,
                            Manufacturer = camera.Manufacturer,
                            Model = camera.Model,
                            YearOfProduction = camera.YearOfProduction
                        }
                    : null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int?> Update(Guid id, CameraViewModel viewModel)
        {
            try
            {
                var camera = await _context.Camera
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (camera != null)
                {
                    camera.Id = id;
                    camera.Model = viewModel.Model;
                    camera.Manufacturer = viewModel.Manufacturer;
                    camera.IsAPSCSensor = viewModel.IsAPSCSensor;
                    camera.YearOfProduction = viewModel.YearOfProduction;

                    _context.Camera.Update(camera);

                    return await _context.SaveChangesAsync();
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
