using System.Numerics;
using NGame.Components.Transforms;
using NGame.Ecs;
using NGame.NGameSystem;
using NGame.UpdateSchedulers;

namespace NGame.Components.Physics2D;



internal class Physics2DSystem : DataListSystem<Physics2DSystem.Data>, IUpdatable
{
	private readonly IPhysicsEngine2D _physicsEngine2D;


	public Physics2DSystem(IPhysicsEngine2D physicsEngine2D)
	{
		_physicsEngine2D = physicsEngine2D;
	}


	protected override ICollection<Type> RequiredComponents =>
		new[] { typeof(Collider2D) };


	protected override Data CreateDataFromEntity(Entity entity)
	{
		var transform = entity.Transform;
		var collider2D = entity.GetRequiredSubTypeComponent<Collider2D>();
		var physicsBody2D =
			entity.GetComponent<PhysicsBody2D>()
			?? new PhysicsBody2D { BodyType2D = BodyType2D.Kinematic };

		_physicsEngine2D.CreateBody(physicsBody2D, transform);

		if (collider2D is BoxCollider2D boxCollider2D)
		{
			_physicsEngine2D.AddBoxCollider(physicsBody2D, boxCollider2D);
		}
		else if (collider2D is CircleCollider2D circleCollider2D)
		{
			_physicsEngine2D.AddCircleCollider(physicsBody2D, circleCollider2D);
		}
		else
		{
			throw new NotSupportedException($"Collider type '{collider2D.GetType()}' not supported");
		}

		return new Data(transform, physicsBody2D, collider2D);
	}


	void IUpdatable.Update(GameTime gameTime)
	{
		_physicsEngine2D.Tick(gameTime);

		foreach (var data in DataEntries)
		{
			var transformResult = _physicsEngine2D.GetTransform(data.PhysicsBody2D);

			data.Transform.Position =
				new Vector3(
					transformResult.Position.X,
					transformResult.Position.Y,
					data.Transform.Position.Z
				);

			var eulerAngles = data.Transform.Rotation.ToEulerAngles();
			eulerAngles.Z = transformResult.Rotation;
			data.Transform.Rotation = eulerAngles.ToQuaternion();
		}
	}



	internal class Data
	{
		public readonly Transform Transform;
		public readonly PhysicsBody2D PhysicsBody2D;
		public readonly Collider2D Collider2D;


		public Data(Transform transform, PhysicsBody2D physicsBody2D, Collider2D collider2D)
		{
			Transform = transform;
			PhysicsBody2D = physicsBody2D;
			Collider2D = collider2D;
		}
	}
}
