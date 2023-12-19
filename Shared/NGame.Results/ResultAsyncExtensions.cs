namespace NGameEditor.Results;



public static class ResultAsyncExtensions
{
	public static async Task<Result> ThenAsync<TInput>(
		this Result<TInput> result,
		Func<TInput, Task> onSuccess
	)
	{
		if (result.ErrorValue != null) return Result.Error(result.ErrorValue);
		await onSuccess(result.SuccessValue!);
		return Result.Success();
	}


	public static async Task<Result<TReturn>> ThenAsync<TInput, TReturn>(
		this Result<TInput> result,
		Func<TInput, Task<Result<TReturn>>> onSuccess
	)
	{
		if (result.ErrorValue != null) return Result.Error(result.ErrorValue);
		return await onSuccess(result.SuccessValue!);
	}


	public static async Task FinallyAsync<TInput>(
		this Task<Result<TInput>> inputTask,
		Action<TInput> onSuccess,
		Action<Error> onError
	)
	{
		var result = await inputTask;
		if (result.ErrorValue != null) onError(result.ErrorValue!);
		else onSuccess(result.SuccessValue!);
	}


	public static async Task<Result> FinallyAsync(
		this Task<Result> inputTask,
		Func<Task> onSuccess,
		Action<Error> onError
	)
	{
		var result = await inputTask;
		if (result.ErrorValue != null) onError(result.ErrorValue!);
		else await onSuccess();
		return Result.Success();
	}


	public static async Task FinallyAsync(
		this Task<Result> inputTask,
		Action onSuccess,
		Action<Error> onError
	)
	{
		var result = await inputTask;
		if (result.ErrorValue != null) onError(result.ErrorValue!);
		onSuccess();
	}
}
