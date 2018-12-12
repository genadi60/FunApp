namespace FunApp.Services.Models.Joke
{
    using Data.Models;
    using Mapping;

    public class DetailsViewModel : IMapFrom<Joke>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string HtmlContent => Content.Replace("\n", "</br>\n");

        public string CategoryName { get; set; }
    }
}
