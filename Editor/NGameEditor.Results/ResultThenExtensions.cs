namespace NGameEditor.Results;



public static class ResultThenExtensions
{
	public static Result Then(this Result result, Action onSuccess)
	{
		if (result.ErrorValue != null) return result;
		onSuccess();
		return Result.Success();
	}


	public static Result Then<TInput>(
		this Result<TInput> result,
		Action<TInput> onSuccess
	)
	{
		if (result.ErrorValue != null) return result;

		onSuccess(result.SuccessValue!);
		return Result.Success();
	}


	public static Result<TReturn> Then<TReturn>(
		this Result result,
		Func<TReturn> onSuccess
	) =>
		result.ErrorValue != null
			? Result.Error(result.ErrorValue)
			: Result.Success(onSuccess());


	public static Result<TReturn> Then<TReturn>(
		this Result result,
		Func<Result<TReturn>> onSuccess
	) =>
		result.ErrorValue != null
			? Result.Error(result.ErrorValue)
			: onSuccess();


	public static Result Then<TInput>(
		this Result<TInput> result,
		Func<TInput, Result> onSuccess
	) =>
		result.ErrorValue != null
			? Result.Error(result.ErrorValue)
			: onSuccess(result.SuccessValue!);


	public static Result<TReturn> Then<TInput, TReturn>(
		this Result<TInput> result,
		Func<TInput, TReturn> onSuccess
	) =>
		result.ErrorValue != null
			? Result.Error(result.ErrorValue)
			: Result.Success(onSuccess(result.SuccessValue!));


	public static Result<TReturn> Then<TInput, TReturn>(
		this Result<TInput> result,
		Func<TInput, Result<TReturn>> onSuccess
	) =>
		result.ErrorValue != null
			? Result.Error(result.ErrorValue)
			: onSuccess(result.SuccessValue!);
}
