using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EOC_2_0.Data.Enum;
using EOC_2_0.Data.Extensions;
using EOC_2_0.Data.Interfaces;
using EOC_2_0.Data.Models;
using EOC_2_0.Data.Repository;
using EOC_2_0.Data.Response;
using EOC_2_0.Service.Interfaces;
using EOC_2_0.ViewModels;

namespace Automarket.Service.Implementations
{
    public class VerbService : IVerbService
    {
        private readonly IBaseRepository<Verb> _verbRepository;

        public VerbService(IBaseRepository<Verb> verbRepository)
        {
            _verbRepository = verbRepository;
        }

        public async Task<BaseResponse<Dictionary<long, string>>> GetVerb(string term)
        {
            var baseResponse = new BaseResponse<Dictionary<long, string>>();
            try
            {
                var products = await _verbRepository.GetAll()
                    .Select(x => new VerbViewModel()
                    {
                        Id = x.Id,
                        Word = x.Word,
                        Level = x.Level
                    })
                    .Where(x => EF.Functions.Like(x.Word, $"%{term}%"))
                    .ToDictionaryAsync(x => x.Id, t => t.Word);

                baseResponse.Data = products;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<long, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<VerbViewModel>> GetVerb(long id)
        {
            try
            {
                var product = await _verbRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                {
                    return new BaseResponse<VerbViewModel>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new VerbViewModel()
                {
                    Word = product.Word,
                    Level = product.Level
                };

                return new BaseResponse<VerbViewModel>()
                {
                    StatusCode = StatusCode.Success,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<VerbViewModel>()
                {
                    Description = $"[GetProduct] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteVerb(long id)
        {
            try
            {
                var product = await _verbRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }

                await _verbRepository.Delete(product);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteVerb] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Verb>> Edit(long id, VerbViewModel model)
        {
            try
            {
                var product = await _verbRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                {
                    return new BaseResponse<Verb>()
                    {
                        Description = "Verb not found",
                        StatusCode = StatusCode.ProductNotFound
                    };
                }

                product.Word = model.Word;
                product.Level = model.Level;

                await _verbRepository.Update(product);


                return new BaseResponse<Verb>()
                {
                    Data = product,
                    StatusCode = StatusCode.Success,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Verb>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<List<Verb>> GetVerbs()
        {
            try
            {
                var products = _verbRepository.GetAll().ToList();
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

        public async Task<BaseResponse<Verb>> Save(VerbViewModel model)
        {
            try
            {
                var product = await _verbRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == model.Id);

                product.Word = model.Word;
                product.Level = model.Level;

                await _verbRepository.Update(product);

                return new BaseResponse<Verb>()
                {
                    Data = product,
                    Description = "Данные обновлены",
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Verb>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}