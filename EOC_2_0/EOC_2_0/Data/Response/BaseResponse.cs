using Microsoft.AspNetCore.Http;
using System.Collections;
using EOC_2_0.Data.Enum;

namespace EOC_2_0.Data.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public string Description { get; set; }

        public StatusCode StatusCode { get; set; }

        public T Data { get; set; }
    }

    public interface IBaseResponse<T>
    {
        public string Description { get; set; }
        public T Data { get; set;  }
        public StatusCode StatusCode { get; set; }
    }
}
