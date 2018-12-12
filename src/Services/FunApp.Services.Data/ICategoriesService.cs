namespace FunApp.Services.DataServices
{
    using System.Collections.Generic;

    using Models;

    public interface ICategoriesService
    {
        IEnumerable<CategoryIdAndNameViewModel> GetAll();

        bool IsCategoryIdValid(int id);
    }
}
