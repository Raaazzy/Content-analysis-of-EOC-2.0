using EOC_2_0.Data.Models;
using EOC_2_0.Data.Response;

namespace EOC_2_0.ViewModels
{
    public class HomeViewModel
    {
        public List<IEnumerable<Verb>> allVerbs { get; set; }

        public List<IEnumerable<Noun>> allNouns { get; set; }

        public List<string> inputText { get; set; }
    }
}