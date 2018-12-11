using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FunApp.Data.Common;
using FunApp.Data.Models;
using FunApp.Services.Models.Home;
using FunApp.Services.Models.Joke;

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

        public async Task<int> Create(int categoryId, string content)
        {
            var joke = new Joke
            {
                CategoryId = categoryId,
                Content = content
            };

            await _repository.AddAsync(joke);
            await _repository.SaveChangesAsync();

            return joke.Id;
        }

        public DetailsViewModel Details(int id)
        {
            var joke = _repository.All()
                .Select(x => new DetailsViewModel
                {
                    Id = x.Id,
                    Content = x.Content,
                    CategoryName = x.Category.Name
                })
                .FirstOrDefault(x => x.Id == id);

            return joke;
        }
    }
}
