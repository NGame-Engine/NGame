using NGame.Parallelism;
using NGame.UpdateLoop;

namespace NGame.Core.Parallelism;



public class OperationRequestScheduler : IOperationRequestScheduler, IUpdatable
{
	private readonly List<IUpdatingOperation> _operationsToStart = [];
	private readonly List<IUpdatingOperation> _operationsToWatch = [];


	public int Order { get; set; }


	public IRunningOperation<TProgress, TResult> Schedule<TProgress, TResult>(
		IOperationRequest<TProgress, TResult> operationRequest
	)
	{
		var backgroundOperation =
			new UpdatingRunningOperation<TProgress, TResult>(operationRequest);

		_operationsToStart.Add(backgroundOperation);

		return backgroundOperation;
	}


	public void Update(GameTime gameTime)
	{
		for (int i = _operationsToStart.Count - 1; i >= 0; i--)
		{
			var operation = _operationsToStart[i];
			_operationsToStart.RemoveAt(i);
			_operationsToWatch.Add(operation);
			operation.Start();
		}


		for (int i = _operationsToWatch.Count - 1; i >= 0; i--)
		{
			var operation = _operationsToWatch[i];
			operation.UpdateStatus();

			if (operation.IsDone)
			{
				_operationsToWatch.RemoveAt(i);
			}
		}
	}
}
