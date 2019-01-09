using FunApp.Services.Mapping;
using FunApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using FunApp.Services.Models.Joke;

namespace FunApp.Services.Models.Category
{
    public class CategoryViewModel : IMapFrom<Data.Models.Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
