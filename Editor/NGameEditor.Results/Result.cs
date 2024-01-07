namespace NGameEditor.Results;



public class Result(Error? errorValue)
{
	public Error? ErrorValue { get; } = errorValue;
	public bool HasError => ErrorValue != null;


	public static Result Success() => new(default);
	public static Result<T> Success<T>(T result) => Result<T>.Success(result);


	public static ErrorResult Error(Error error) =>
		new(error);


	public static ErrorResult Error(string title) =>
		new(new Error(title, ""));


	public static ErrorResult Error(string title, string description) =>
		new(new Error(title, description));


	public static Result Try(Func<Result> action)
	{
		try
		{
			return action();
		}
		catch (Exception e)
		{
			return Error(e.Message, $"StackTrace: {e.StackTrace}");
		}
	}


	public static Result<TResult> Try<TResult>(Func<Result<TResult>> action)
	{
		try
		{
			return action();
		}
		catch (Exception e)
		{
			return Error(e.Message, $"StackTrace: {e.StackTrace}");
		}
	}
}



public class ErrorResult(Error errorValue) : Result(errorValue);



public class Result<T>(Error? errorValue, T? successValue) : Result(errorValue)
{
	public T? SuccessValue { get; } = successValue;


	public static Result<T> Success(T result) => new(default, result);


	public static implicit operator Result<T>(ErrorResult errorResult) =>
		new(errorResult.ErrorValue!, default);


	public bool TryGetValue(out T successValue)
	{
		successValue = SuccessValue!;
		return HasError == false;
	}
}
