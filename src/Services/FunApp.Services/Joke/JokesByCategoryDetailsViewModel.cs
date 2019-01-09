using System;
using System.Collections.Generic;
using System.Text;

namespace FunApp.Services.Models.Joke
{
    public class JokesByCategoryDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<DetailsViewModel> Jokes { get; set; }
    }
}
