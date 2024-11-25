using System;
namespace Presentation.Contracts
{
    public class ApiResult
    {
        public string Message { get; set; } = string.Empty;
    }

    public class ApiResult<TData> : ApiResult //where TData : class
    {
        public TData Data { get; set; }
    }
}

