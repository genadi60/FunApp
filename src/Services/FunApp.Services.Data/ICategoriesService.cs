namespace FunApp.Services.DataServices
{
    using System.Collections.Generic;
    using FunApp.Services.Models.Category;
    using Models;

    public interface ICategoriesService
    {
        IEnumerable<CategoryIdAndNameViewModel> GetAll();

        IEnumerable<CategoryViewModel> GetAllViewModels();

        bool IsCategoryIdValid(int id);

        int? GetCategoryId(string categoryName);
    }
}
