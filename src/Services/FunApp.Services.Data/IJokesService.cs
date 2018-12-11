using FunApp.Data.Models;
using System.Collections.Generic;
using FunApp.Services.Models.Home;
using FunApp.Services.Models.Joke;
using System.Threading.Tasks;

namespace FunApp.Services.DataServices
{
    public interface IJokesService
    {
        IEnumerable<IndexJokeViewModel> GetRandomJokes(int count);

        int GetCount();

        Task<int> Create(int categoryId, string content);

        DetailsViewModel Details(int id);
    }
}