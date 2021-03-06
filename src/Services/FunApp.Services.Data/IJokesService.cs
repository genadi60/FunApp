﻿using FunApp.Data.Models;
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

        TViewModel ById<TViewModel>(int id);

        IEnumerable<TViewModel> ByCategory<TViewModel>(int id);

        Task<int> Edit(JokeViewModel model);

        Task<bool> Delete(int id);
    }
}