namespace FunApp.Data.Models
{
    using System.Collections.Generic;

    using Common;

    public class Category : BaseModel<int>
    {
        public string Name { get; set; }

        public virtual IEnumerable<Joke> Jokes { get; set; } = new HashSet<Joke>();
    }
}
