namespace Core.Common
{
    public enum DBError
    {
        None = 0,
        Duplicate = 1,
        Timeout = 2,
        ForeignKeyViolation = 3,
        ConnectionError = 4,
        UnknownError = 5
    }
}
