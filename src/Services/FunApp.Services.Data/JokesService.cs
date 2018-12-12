using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FunApp.Data.Common;
using FunApp.Data.Models;
using FunApp.Services.Mapping;
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
                .To<IndexJokeViewModel>()
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
                .To<DetailsViewModel>()
                .FirstOrDefault(x => x.Id == id);

            return joke;
        }

        public JokeViewModel ById(int id)
        {
            return _repository.All()
                .To<JokeViewModel>()
                .FirstOrDefault(x => x.Id == id);
        }

        public async Task<int> Edit(JokeViewModel model)
        {
            var joke = _repository.All().FirstOrDefault(x => x.Id == model.Id);

            joke.Content = model.Content;
            joke.CategoryId = model.CategoryId;

            _repository.Update(joke);
            await  _repository.SaveChangesAsync();

            return joke.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var joke = _repository.All().FirstOrDefault(x => x.Id == id);

            if (joke == null)
            {
                return false;
            }

            _repository.Delete(joke);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
