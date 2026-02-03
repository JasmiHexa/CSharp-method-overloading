namespace CSharpSyntheticRepo.Common;

public readonly record struct Result(bool Success, string Error)
{
    public static Result Ok() => new(true, string.Empty);
    public static Result Fail(string error) => new(false, error);

    public override string ToString() => Success ? "OK" : $"FAIL: {Error}";
}

public readonly record struct Result<T>(bool Success, T? Value, string Error)
{
    public static Result<T> Ok(T value) => new(true, value, string.Empty);
    public static Result<T> Ok() => new(true, default, string.Empty);
    public static Result<T> Fail(string error) => new(false, default, error);

    public override string ToString() => Success ? $"OK: {Value}" : $"FAIL: {Error}";
}


