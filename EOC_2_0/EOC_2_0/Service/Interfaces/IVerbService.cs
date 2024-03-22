using EOC_2_0.Data.Interfaces;
using EOC_2_0.Data.Models;
using EOC_2_0.Data.Response;
using EOC_2_0.ViewModels;

namespace EOC_2_0.Service.Interfaces
{
    public interface IVerbService
    {
        public IBaseResponse<List<Verb>> GetVerbs(int level);

        public Task<IBaseResponse<VerbViewModel>> GetVerb(long id);

        public IBaseResponse<List<Verb>> GetVerb(string term, int level);


        public Task<IBaseResponse<bool>> DeleteVerb(long id);

        public Task<IBaseResponse<Verb>> Edit(long id, VerbViewModel model);

        public Task<BaseResponse<Verb>> Save(VerbViewModel model);
    }
}
