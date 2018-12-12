using AutoMapper;

namespace FunApp.Services.Models.Home
{
    using Data.Models;
    using Mapping;

    public class IndexJokeViewModel : IMapFrom<Joke>, IHaveCustomMappings
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public string HtmlContent => Content.Replace("\n", "</br>\n");
        
        public string CategoryName { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            ////configuration.CreateMap<Joke, IndexJokeViewModel>()
            ////    .ForMember(x => x.CategoryName, x => x.MapFrom(j => j.Category.Name));
        }
    }
}
