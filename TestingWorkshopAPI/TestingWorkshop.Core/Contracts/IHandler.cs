using System;
using System.Threading.Tasks;

namespace TestingWorkshop.Core.Contracts
{
    public interface IHandler<TViewModel>
        where TViewModel : IViewModel
    {
        Task<Guid?> Add(TViewModel viewModel);
        Task<int?> Update(Guid id, TViewModel viewModel);
        Task<int?> Delete(Guid id);
        Task<TViewModel> GetById(Guid id);
    }
}
