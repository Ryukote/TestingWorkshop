using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TestingWorkshop.Core.Contracts;
using TestingWorkshop.Core.Entities;
using TestingWorkshop.Core.ViewModels;

namespace TestingWorkshopAPI.DataAccess.Handlers
{
    public class LensHandler : IHandler<LensesViewModel>
    {
        private TestingWorkshopContext _context;
        public LensHandler(TestingWorkshopContext context)
        {
            _context = context;
        }

        public async Task<Guid?> Add(LensesViewModel viewModel)
        {
            try
            {
                var lens = new Lenses()
                {
                    Id = Guid.NewGuid(),
                    Model = viewModel.Model,
                    Manufacturer = viewModel.Manufacturer,
                    IsAPSCSensor = viewModel.IsAPSCSensor,
                    CompatibleCameraName = viewModel.CompatibleCameraName,
                    Diameter = viewModel.Diameter,
                    FocalLength = viewModel.FocalLength
                };

                _context.Lenses.Add(lens);

                if (await _context.SaveChangesAsync() == 1)
                {
                    return lens.Id;
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
                var lens = await _context.Lenses
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (lens != null)
                {
                    _context.Lenses.Remove(lens);
                    return await _context.SaveChangesAsync();
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<LensesViewModel> GetById(Guid id)
        {
            try
            {
                var lens = await _context.Lenses
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                return (lens != null)
                    ? new LensesViewModel()
                    {
                        Id = lens.Id,
                        IsAPSCSensor = lens.IsAPSCSensor,
                        Manufacturer = lens.Manufacturer,
                        Model = lens.Model,
                        CompatibleCameraName = lens.CompatibleCameraName,
                        Diameter = lens.Diameter,
                        FocalLength = lens.FocalLength
                    }
                    : null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int?> Update(Guid id, LensesViewModel viewModel)
        {
            try
            {
                var lens = await _context.Lenses
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (lens != null)
                {
                    lens.Id = id;
                    lens.Model = viewModel.Model;
                    lens.Manufacturer = viewModel.Manufacturer;
                    lens.IsAPSCSensor = viewModel.IsAPSCSensor;
                    lens.CompatibleCameraName = viewModel.CompatibleCameraName;
                    lens.Diameter = viewModel.Diameter;
                    lens.FocalLength = viewModel.FocalLength;

                    _context.Lenses.Update(lens);

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
