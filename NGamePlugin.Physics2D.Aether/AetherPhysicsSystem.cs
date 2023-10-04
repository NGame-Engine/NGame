using System.Numerics;
using NGame.Components.Physics2D;
using NGame.Components.Transforms;
using NGame.Ecs;
using NGame.NGameSystem;
using NGame.UpdateSchedulers;
using nkast.Aether.Physics2D.Dynamics;

namespace NGamePlugin.Physics2D.Aether;



public class AetherPhysicsSystem : ISystem, IUpdatable
{
	private readonly World _world;
	private readonly Dictionary<Entity, Data> _datas = new();


	public AetherPhysicsSystem(World world)
	{
		_world = world;
	}


	private TimeSpan DataUpdateFrequency => TimeSpan.FromMilliseconds(1000);
	private TimeSpan _nextDataUpdate;


	public int Order { get; set; } = 50000;


	public bool EntityIsMatch(IEnumerable<Type> componentTypes) =>
		componentTypes.Any(x => x.IsAssignableTo(typeof(Collider2D)));


	public void Add(Entity entity)
	{
		var transform = entity.Transform;
		var collider2D = entity.GetRequiredSubTypeComponent<Collider2D>();
		var physicsBody2D =
			entity.GetComponent<PhysicsBody2D>() ??
			new PhysicsBody2D { BodyType2D = BodyType2D.Static };

		var position = transform.Position.ToAetherVector2();
		var rotation = transform.Rotation.ToEulerAngles().Z;
		var bodyType = physicsBody2D.BodyType2D.ToAetherBodyType();
		var body = _world.CreateBody(position, rotation, bodyType);


		if (collider2D is BoxCollider2D boxCollider2D)
		{
			var fixture = body.CreateRectangle(
				boxCollider2D.Size.X,
				boxCollider2D.Size.Y,
				boxCollider2D.Density,
				boxCollider2D.Offset.ToAetherVector2()
			);

			fixture.Restitution = boxCollider2D.Bounce;
			fixture.Friction = boxCollider2D.Friction;
		}
		else if (collider2D is CircleCollider2D circleCollider2D)
		{
			var fixture = body.CreateCircle(
				circleCollider2D.Radius,
				circleCollider2D.Density,
				circleCollider2D.Offset.ToAetherVector2()
			);

			fixture.Restitution = circleCollider2D.Bounce;
			fixture.Friction = circleCollider2D.Friction;
		}
		else
		{
			throw new NotSupportedException($"Collider type '{collider2D.GetType()}' not supported");
		}

		var data = new Data(transform, physicsBody2D, body);
		_datas.Add(entity, data);
	}


	public void Remove(Entity entity)
	{
		var data = _datas[entity];
		var body = data.Body;
		_world.Remove(body);
	}


	public bool Contains(Entity entity) => _datas.ContainsKey(entity);


	public void Update(GameTime gameTime)
	{
		if (gameTime.Total > _nextDataUpdate)
		{
			foreach (var data in _datas.Values)
			{
				var body = data.Body;
				body.BodyType = data.PhysicsBody2D.BodyType2D.ToAetherBodyType();
			}

			_nextDataUpdate = gameTime.Total + DataUpdateFrequency;
		}


		var seconds = (float)gameTime.Elapsed.TotalSeconds;
		_world.Step(seconds);


		foreach (var data in _datas.Values)
		{
			var body = data.Body;
			var transform = body.GetTransform();


			var oldPosition = data.Transform.Position;

			var newPosition = new Vector3(
				transform.p.X,
				transform.p.Y,
				oldPosition.Z
			);

			data.Transform.Position = newPosition;


			var oldRotation = data.Transform.EulerAngles;

			var newRotation = oldRotation with
			{
				Z = transform.q.Phase
			};

			data.Transform.EulerAngles = newRotation;
		}
	}



	private class Data
	{
		public readonly Transform Transform;
		public readonly PhysicsBody2D PhysicsBody2D;
		public readonly Body Body;


		public Data(Transform transform, PhysicsBody2D physicsBody2D, Body body)
		{
			PhysicsBody2D = physicsBody2D;
			Transform = transform;
			Body = body;
		}
	}
}
