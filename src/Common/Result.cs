namespace CSharpSyntheticRepo.Common
{
    public sealed class Result
    {
        public bool Success { get; private set; }
        public string Error { get; private set; }

        private Result(bool success, string error)
        {
            Success = success;
            Error = error;
        }

        public static Result Ok()
        {
            return new Result(true, string.Empty);
        }

        public static Result Fail(string error)
        {
            return new Result(false, error);
        }

        public override string ToString()
        {
            return Success ? "OK" : "FAIL: " + Error;
        }
    }

    public sealed class Result<T>
    {
        public bool Success { get; private set; }
        public T Value { get; private set; }
        public string Error { get; private set; }

        private Result(bool success, T value, string error)
        {
            Success = success;
            Value = value;
            Error = error;
        }

        public static Result<T> Ok(T value)
        {
            return new Result<T>(true, value, string.Empty);
        }

        public static Result<T> Ok()
        {
            return new Result<T>(true, default(T), string.Empty);
        }

        public static Result<T> Fail(string error)
        {
            return new Result<T>(false, default(T), error);
        }

        public override string ToString()
        {
            return Success ? ("OK: " + Value) : ("FAIL: " + Error);
        }
    }
}


