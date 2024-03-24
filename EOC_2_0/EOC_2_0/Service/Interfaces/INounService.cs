using EOC_2_0.Data.Interfaces;
using EOC_2_0.Data.Models;
using EOC_2_0.Data.Response;
using EOC_2_0.ViewModels;

namespace EOC_2_0.Service.Interfaces
{
    public interface INounService
    {
        public IBaseResponse<List<Noun>> GetNouns(int verbId);

        public IBaseResponse<List<Noun>> GetNoun(string term, int verbId);
    }
}
