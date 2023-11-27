using System.Numerics;

namespace NGame.Ecs;



public sealed class Entity
{
	internal readonly List<Entity> InternalChildren = new();
	internal readonly List<EntityComponent> InternalComponents = new();


	public Entity(Scene scene)
	{
		Scene = scene;
	}


	public readonly Guid Id = Guid.NewGuid();
	public string Name { get; set; } = "Entity";
	public string Tag { get; set; } = "";


	public Scene Scene { get; internal set; }
	public Entity? Parent { get; internal set; }


	public IReadOnlyCollection<Entity> Children => InternalChildren;
	public IReadOnlyCollection<EntityComponent> Components => InternalComponents;


	public Matrix4x4 WorldMatrix = Matrix4x4.Identity;
	public Matrix4x4 LocalMatrix = Matrix4x4.Identity;


	/// <summary>
	/// The translation relative to the parent transformation.
	/// </summary>
	public Vector3 Position
	{
		get => LocalMatrix.Translation;
		set => LocalMatrix.Translation = value;
	}


	/// <summary>
	/// The rotation relative to the parent transformation.
	/// </summary>
	public Quaternion Rotation
	{
		get => Quaternion.CreateFromRotationMatrix(LocalMatrix);
		set => LocalMatrix = GenerateMatrix(Position, value, Scale);
	}


	/// <summary>
	/// The rotation relative to the parent transformation in Euler angles.
	/// </summary>
	public Vector3 EulerAngles
	{
		// TODO Too much implementation for being in an .Abstractions project
		get => Rotation.ToEulerAngles();
		set => Rotation = value.ToQuaternion();
	}


	/// <summary>
	/// The scaling relative to the parent transformation.
	/// </summary>
	public Vector3 Scale
	{
		get
		{
			Matrix4x4.Decompose(LocalMatrix, out var scale, out _, out _);
			return scale;
		}
		set => LocalMatrix = GenerateMatrix(Position, Rotation, value);
	}


	private Matrix4x4 GenerateMatrix(Vector3 position, Quaternion rotation, Vector3 scale)
	{
		var scaled = Matrix4x4.CreateScale(scale);
		var rotated = Matrix4x4.Transform(scaled, rotation);
		var translation = Matrix4x4.CreateTranslation(position);
		return rotated * translation;
	}
}



public static class EntityExtensions
{
	public static T GetComponent<T>(this Entity entity) where T : EntityComponent =>
		entity
			.InternalComponents
			.Where(x => x.GetType() == typeof(T))
			.Cast<T>()
			.First();


	public static T? TryGetComponent<T>(this Entity entity) where T : EntityComponent =>
		entity
			.InternalComponents
			.Where(x => x.GetType() == typeof(T))
			.Cast<T>()
			.FirstOrDefault();


	public static IEnumerable<T> GetComponents<T>(this Entity entity) where T : EntityComponent =>
		entity
			.InternalComponents
			.Where(x => x.GetType() == typeof(T))
			.Cast<T>();


	public static IEnumerable<T> GetComponentsInChildren<T>(this Entity entity) where T : EntityComponent =>
		entity
			.InternalComponents
			.Where(x => x.GetType() == typeof(T))
			.Cast<T>()
			.Concat(
				entity
					.InternalChildren
					.SelectMany(t => t.GetComponentsInChildren<T>())
			);


	public static T GetRequiredSubTypeComponent<T>(this Entity entity) where T : EntityComponent =>
		entity
			.InternalComponents
			.Where(x => x.GetType().IsAssignableTo(typeof(T)))
			.Cast<T>()
			.First();
}
