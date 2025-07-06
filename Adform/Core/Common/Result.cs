namespace Core.Common
{
    public class Result<TData, TError> 
    {
        public TData? Data { get; set; }

        public TError? Error { get; set; }

        public bool IsSuccess => Data != null;

        public static implicit operator Result<TData, TError>(TData data)
        {
            return new Result<TData, TError> { Data = data };
        }

        public static implicit operator Result<TData, TError>(TError error)
        {
            return new Result<TData, TError> { Error = error };
        }

        public T Match<T>(Func<Result<TData, TError>, T> onSuccess, Func<Result<TData, TError>, T> onError)
        {
            return IsSuccess ? onSuccess(this) : onError(this);
        }
    }

    public class Result<TData> : Result<TData, string>
    {
        public T Match<T>(Func<Result<TData>, T> onSuccess, Func<Result<TData>, T> onError)
        {
            return IsSuccess ? onSuccess(this) : onError(this);
        }

        public static implicit operator Result<TData>(TData data)
        {
            return new Result<TData> { Data = data };
        }

        public static implicit operator Result<TData>(string error)
        {
            return new Result<TData> { Error = error };
        }
    }
}
