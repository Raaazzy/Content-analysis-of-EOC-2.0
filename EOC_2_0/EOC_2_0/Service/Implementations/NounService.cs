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
using System.Runtime.CompilerServices;

namespace Automarket.Service.Implementations
{
    public class NounService : INounService
    {
        private readonly IBaseRepository<Noun> _nounRepository;

        public NounService(IBaseRepository<Noun> nounRepository)
        {
            _nounRepository = nounRepository;
        }


        // ЭТО ПОИСКОВИК
        public IBaseResponse<List<Noun>> GetNoun(string term)
        {
            try
            {
                var products = _nounRepository.GetAll().Where(x => x.Word.StartsWith(term) && x.Word.Contains(term));

                return new BaseResponse<List<Noun>>()
                {
                    Data = products.ToList(),
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Noun>>()
                {
                    Description = $"[GetNoun] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<List<Noun>> GetNouns()
        {
            try
            {
                var products = _nounRepository.GetAll().ToList();
                if (!products.Any())
                {
                    return new BaseResponse<List<Noun>>()
                    {
                        Description = "Найдено 0 существительных",
                        StatusCode = StatusCode.Success
                    };
                }

                return new BaseResponse<List<Noun>>()
                {
                    Data = products,
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Noun>>()
                {
                    Description = $"[GetNouns] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}