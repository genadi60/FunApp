using System.ComponentModel.DataAnnotations;

namespace FunApp.Web.Models.Joke
{
    public partial class CreateJokeInputModel
    {
        [Required]
        [MinLength(20)]
        public string Content { get; set; }

        [ValidCategoryId]
        public int CategoryId { get; set; }
    }
}
