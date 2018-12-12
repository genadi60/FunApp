using System.Linq;
using AutoMapper;
using FunApp.Data.Models;
using FunApp.Services.Mapping;

namespace FunApp.Services.Models
{
    public class CategoryIdAndNameViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AllJokesCount { get; set; }

        public string NameAndCount => $"{Name} ({AllJokesCount})";

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Category, CategoryIdAndNameViewModel>()
                .ForMember(x => x.AllJokesCount, m => m.MapFrom(c => c.Jokes.Count()));
        }
    }
}
