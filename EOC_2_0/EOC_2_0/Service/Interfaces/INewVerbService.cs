using EOC_2_0.Data.Models;
using EOC_2_0.Data.Response;

namespace EOC_2_0.Service.Interfaces
{
    public interface INewVerbService
    {
        public Task<IBaseResponse<NewVerb>> Create(string term, int level);
    }
}
