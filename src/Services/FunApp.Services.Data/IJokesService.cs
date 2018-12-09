using FunApp.Data.Models;
using System.Collections.Generic;
using FunApp.Services.Models.Home;

namespace FunApp.Services.DataServices
{
    public interface IJokesService
    {
        IEnumerable<IndexJokeViewModel> GetRandomJokes(int count);

        int GetCount();
    }
}