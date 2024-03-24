using EOC_2_0.Data.Enum;
using EOC_2_0.Data.Interfaces;
using EOC_2_0.Data.Models;
using EOC_2_0.Data.Repository;
using EOC_2_0.Data.Response;
using EOC_2_0.Service.Interfaces;

namespace EOC_2_0.Service.Implementations
{
    public class NewVerbService: INewVerbService
    {
        private readonly IBaseRepository<NewVerb> _newVerbRepository;

        public NewVerbService(IBaseRepository<NewVerb> newVerbRepository)
        {
            _newVerbRepository = newVerbRepository;
        }

        public async Task<IBaseResponse<NewVerb>> Create(string term, int level)
        {
            try
            {
                var verb = new NewVerb()
                {
                    Word = term,
                    Level = level,
                };
                await _newVerbRepository.Create(verb);

                return new BaseResponse<NewVerb>()
                {
                    StatusCode = StatusCode.Success,
                    Data = verb
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<NewVerb>()
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
