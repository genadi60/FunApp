namespace FunApp.Services.DataServices
{
    using System.Collections.Generic;

    using Models;

    public interface ICategoriesService
    {
        IEnumerable<IdAndNameViewModel> GetAll();

        bool IsCategoryIdValid(int id);
    }
}
