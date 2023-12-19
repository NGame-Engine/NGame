namespace NGameEditor.Results;



public static class ResultIfErrorExtensions
{
	public static Result IfError(this Result result, Action<Error> onError)
	{
		if (result.ErrorValue != null) onError(result.ErrorValue!);
		return result;
	}


	public static Result<TResult> IfError<TResult>(this Result<TResult> result, Action<Error> onError)
	{
		if (result.ErrorValue != null) onError(result.ErrorValue!);
		return result;
	}
}
