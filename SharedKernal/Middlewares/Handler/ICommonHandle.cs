using Microsoft.AspNetCore.Http.Extensions;
using System.Threading.Tasks;
using SharedKernal.Common.Enum;

namespace SharedKernal.Middlewares.Handler
{
    public interface ICommonHandle
    {
        Task<TResponse> Handle<TResponse, TRequest>(TRequest body, string methodUrl, SharedKernal.Common.Enum.HttpMethod methodType, QueryBuilder qs);
    }
}
