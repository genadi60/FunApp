using FunApp.Services.Models.Joke;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunApp.Services.Models.Category
{
    public class Details
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<JokeViewModel> Jokes { get; set; }
    }
}
