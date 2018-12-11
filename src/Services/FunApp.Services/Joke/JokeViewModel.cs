﻿namespace FunApp.Services.Models.Joke
{
    using Data.Models;

    public class JokeViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
