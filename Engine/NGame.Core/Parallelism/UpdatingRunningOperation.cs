using NGame.Parallelism;

namespace NGame.Core.Parallelism;



public class UpdatingRunningOperation<TProgress, TResult> :
	IRunningOperation<TProgress, TResult>,
	IUpdatingOperation
{
	private readonly IOperationRequest<TProgress, TResult> _operationRequest;
	private readonly object _lock = new();

	private bool _hasUpdates;
	private TProgress _progress;
	private bool _isDone;
	private TResult? _result;


	public UpdatingRunningOperation(IOperationRequest<TProgress, TResult> operationRequest)
	{
		_progress = operationRequest.InitialProgress;
		_operationRequest = operationRequest;
	}


	public event Action<TProgress>? ProgressUpdated;
	public event Action<TResult>? Completed;

	public TProgress Progress
	{
		get
		{
			lock (_lock)
			{
				return _progress;
			}
		}
		private set
		{
			lock (_lock)
			{
				_progress = value;
				_hasUpdates = true;
			}
		}
	}

	public bool IsDone
	{
		get
		{
			lock (_lock)
			{
				return _isDone;
			}
		}
	}

	public TResult? Result
	{
		get
		{
			lock (_lock)
			{
				return _result;
			}
		}
	}


	/// <summary>
	/// Might run in a background thread
	/// </summary>
	public void Start()
	{
		var result = _operationRequest.Execute(x => Progress = x);

		lock (_lock)
		{
			_result = result;
			_isDone = true;
			_hasUpdates = true;
		}
	}


	public void UpdateStatus()
	{
		lock (_lock)
		{
			if (!_hasUpdates) return;

			_hasUpdates = false;
			ProgressUpdated?.Invoke(_progress);
			if (!_isDone) return;

			Completed?.Invoke(_result!);
		}
	}
}
