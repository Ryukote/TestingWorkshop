using System.Collections.Generic;

namespace TestingWorkshop.Core.Contracts
{
    public interface IMapper<TModel, TViewModel>
        where TModel : IModel
        where TViewModel : IViewModel
    {
        /// <summary>
        /// Convert view model data to model data.
        /// </summary>
        /// <param name="viewModel">Valid view model data.</param>
        /// <returns>Converted view model data to model data.</returns>
        TModel ToModel(TViewModel viewModel);
        /// <summary>
        /// Convert model data to view model data.
        /// </summary>
        /// <param name="model">Valid model data.</param>
        /// <returns>Converted model data to view model data.</returns>
        TViewModel ToViewModel(TModel model);
        /// <summary>
        /// Convert model collection to view model collection.
        /// </summary>
        /// <param name="modelCollection">Valid model collection data.</param>
        /// <returns>Converted model collection data to view model collection data.</returns>
        ICollection<TViewModel> ToViewModelCollection(ICollection<TModel> modelCollection);
    }
}
