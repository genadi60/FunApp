using System;
using System.Collections.Generic;
using System.Linq;
using FunApp.Data.Common;
using FunApp.Data.Models;
using FunApp.Services.Models.Home;

namespace FunApp.Services.DataServices
{
    public class JokesService : IJokesService
    {
        private readonly IRepository<Joke> _repository;

        public JokesService(IRepository<Joke> repository)
        {
            _repository = repository;
        }

        public IEnumerable<IndexJokeViewModel> GetRandomJokes(int count)
        {
            var jokes = _repository.All()
                .OrderBy(j => Guid.NewGuid())
                .Select(j => new IndexJokeViewModel
                {
                    Content = j.Content,
                    CategoryName = j.Category.Name
                })
                .Take(count)
                .ToList();

            return jokes;
        }

        public int GetCount()
        {
            return _repository.All().Count();
        }
    }
}
