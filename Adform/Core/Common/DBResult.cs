namespace Core.Common
{
    public class DBResult<T>
    {
        public T? Data { get; set; }

        public DBError DBError { get; set; } = DBError.None;

        public bool IsSuccess => DBError == DBError.None;

        public static implicit operator DBResult<T>(T data)
        {
            return new DBResult<T> { Data = data };
        }

        public static implicit operator DBResult<T>(DBError error)
        {
            return new DBResult<T> { DBError = error };
        }
    }
}
