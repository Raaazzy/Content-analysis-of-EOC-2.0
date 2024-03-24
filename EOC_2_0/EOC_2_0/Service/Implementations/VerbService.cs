using EOC_2_0.Data.Enum;
using EOC_2_0.Data.Interfaces;
using EOC_2_0.Data.Models;
using EOC_2_0.Data.Response;
using EOC_2_0.Service.Interfaces;

namespace Automarket.Service.Implementations
{
    public class VerbService : IVerbService
    {
        private readonly IBaseRepository<Verb> _verbRepository;
        private readonly IBaseRepository<NewVerb> _newVerbRepository;

        public VerbService(IBaseRepository<Verb> verbRepository, IBaseRepository<NewVerb> newVerbRepository)
        {
            _verbRepository = verbRepository;
            _newVerbRepository = newVerbRepository;
        }


        // ЭТО ПОИСКОВИК
        public IBaseResponse<List<Verb>> GetVerb(string term, int level)
        {
            try
            {
                var verbs = _verbRepository.GetAll().Where(x => x.Level == level && x.Word.StartsWith(term) && x.Word.Contains(term));

                return new BaseResponse<List<Verb>>()
                {
                    Data = verbs.ToList(),
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Verb>>()
                {
                    Description = $"[GetVerbs] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<List<Verb>> GetVerbs(int level)
        {
            try
            {
                var products = _verbRepository.GetAll().Where(x => x.Level == level).ToList();
                if (!products.Any())
                {
                    return new BaseResponse<List<Verb>>()
                    {
                        Description = "Найдено 0 глаголов",
                        StatusCode = StatusCode.Success
                    };
                }

                return new BaseResponse<List<Verb>>()
                {
                    Data = products,
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Verb>>()
                {
                    Description = $"[GetVerbs] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}