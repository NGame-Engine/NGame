using NGame.Components.Physics2D;
using NGame.NGameSystem;
using NGame.UpdateSchedulers;
using nkast.Aether.Physics2D.Dynamics;
using NgTransform = NGame.Components.Transforms.Transform;

namespace NGamePlugin.Physics2D.Aether;



public class AetherPhysicsEngine2D : IPhysicsEngine2D
{
	private readonly World _world;
	private readonly Dictionary<PhysicsBody2D, Body> _bodies = new();
	private readonly BiDirectionalLookup<Collider2D, Fixture> _colliderLookup = new();


	public AetherPhysicsEngine2D(World world)
	{
		_world = world;
	}


	public void CreateBody(PhysicsBody2D physicsBody2D, NgTransform transform)
	{
		var position = transform.Position.ToAetherVector2();
		var rotation = transform.Rotation.ToEulerAngles().Z;
		var bodyType = physicsBody2D.BodyType2D.ToAetherBodyType();
		var body = _world.CreateBody(position, rotation, bodyType);

		_bodies.Add(physicsBody2D, body);
	}


	public void RemoveBody(PhysicsBody2D physicsBody2D)
	{
		var body = _bodies[physicsBody2D];
		var fixtures = body.FixtureList.ToList();

		_world.Remove(body);

		_bodies.Remove(physicsBody2D);
		foreach (var fixture in fixtures)
		{
			_colliderLookup.Remove(fixture);
		}
	}


	public void RemoveAllBodies()
	{
		var bodies = _bodies.Keys.ToList();
		foreach (var body in bodies)
		{
			RemoveBody(body);
		}
	}


	public void Subscribe(
		PhysicsBody2D physicsBody2D,
		Action<Collision2DEventArgs> onCollision,
		Action<Collision2DEventArgs> onSeparation
	)
	{
		var body = _bodies[physicsBody2D];

		body.OnCollision += (sender, other, _) =>
		{
			var eventArgs =
				new Collision2DEventArgs(
					_colliderLookup[sender],
					_colliderLookup[other]
				);

			onCollision(eventArgs);
			return false;
		};

		body.OnSeparation += (sender, other, _) =>
		{
			var eventArgs =
				new Collision2DEventArgs(
					_colliderLookup[sender],
					_colliderLookup[other]
				);

			onSeparation(eventArgs);
		};
	}


	public void AddBoxCollider(PhysicsBody2D physicsBody2D, BoxCollider2D collider2D)
	{
		var body = _bodies[physicsBody2D];
		var fixture = body.CreateRectangle(
			collider2D.Size.X,
			collider2D.Size.Y,
			collider2D.Density,
			collider2D.Offset.ToAetherVector2()
		);

		fixture.Restitution = collider2D.Bounce;
		fixture.Friction = collider2D.Friction;

		_colliderLookup.Add(collider2D, fixture);
	}


	public void AddCircleCollider(PhysicsBody2D physicsBody2D, CircleCollider2D collider2D)
	{
		var body = _bodies[physicsBody2D];
		var fixture = body.CreateCircle(
			collider2D.Radius,
			collider2D.Density,
			collider2D.Offset.ToAetherVector2()
		);

		fixture.Restitution = collider2D.Bounce;
		fixture.Friction = collider2D.Friction;

		_colliderLookup.Add(collider2D, fixture);
	}


	public void RemoveCollider(PhysicsBody2D physicsBody2D, CircleCollider2D collider2D)
	{
		var body = _bodies[physicsBody2D];
		var fixture = _colliderLookup[collider2D];
		body.Remove(fixture);
		_colliderLookup.Remove(collider2D);
	}


	// TODO 
	// User scripts
	// TransformProcessor update matrices from TRS
	// Physics update bodies from Worlds
	// Physics run
	// Physics update Worlds from bodies
	// Physics (?) update TRS from worlds
	public void Tick(GameTime gameTime)
	{
		var seconds = (float)gameTime.Elapsed.TotalSeconds;
		_world.Step(seconds);
	}


	public void ApplyForce(
		PhysicsBody2D physicsBody2D,
		System.Numerics.Vector2 force,
		System.Numerics.Vector2? point = null
	)
	{
		var body = _bodies[physicsBody2D];
		var aetherForce = force.ToAetherVector2();

		if (point == null)
		{
			body.ApplyForce(aetherForce);
			return;
		}

		var aetherPoint = point.Value.ToAetherVector2();
		body.ApplyForce(aetherForce, aetherPoint);
	}


	public void ApplyTorque(PhysicsBody2D physicsBody2D, float torque)
	{
		var body = _bodies[physicsBody2D];
		body.ApplyTorque(torque);
	}


	public void ApplyLinearImpulse(
		PhysicsBody2D physicsBody2D,
		System.Numerics.Vector2 impulse,
		System.Numerics.Vector2? point = null
	)
	{
		var body = _bodies[physicsBody2D];
		var aetherImpulse = impulse.ToAetherVector2();

		if (point == null)
		{
			body.ApplyLinearImpulse(aetherImpulse);
			return;
		}

		var aetherPoint = point.Value.ToAetherVector2();
		body.ApplyLinearImpulse(aetherImpulse, aetherPoint);
	}


	public void ApplyAngularImpulse(PhysicsBody2D physicsBody2D, float impulse)
	{
		var body = _bodies[physicsBody2D];
		body.ApplyAngularImpulse(impulse);
	}


	public void SetMass(PhysicsBody2D physicsBody2D, float mass)
	{
		var body = _bodies[physicsBody2D];
		body.Mass = mass;
	}


	public void SetInertia(PhysicsBody2D physicsBody2D, float inertia)
	{
		var body = _bodies[physicsBody2D];
		body.Inertia = inertia;
	}


	public void SetGravityIsIgnored(PhysicsBody2D physicsBody2D, bool gravityIsIgnored)
	{
		var body = _bodies[physicsBody2D];
		body.IgnoreGravity = gravityIsIgnored;
	}


	public void SetRotationIsFixed(PhysicsBody2D physicsBody2D, bool rotationIsFixed)
	{
		var body = _bodies[physicsBody2D];
		body.FixedRotation = rotationIsFixed;
	}


	public void SetContinuousCollisionDetection(
		PhysicsBody2D physicsBody2D,
		bool continuousCollisionDetectionIsActive
	)
	{
		var body = _bodies[physicsBody2D];
		body.IsBullet = continuousCollisionDetectionIsActive;
	}


	public void SetRestitution(PhysicsBody2D physicsBody2D, float restitution)
	{
		var body = _bodies[physicsBody2D];
		foreach (var fixture in body.FixtureList)
		{
			fixture.Restitution = restitution;
		}
	}


	public void SetFriction(PhysicsBody2D physicsBody2D, float friction)
	{
		var body = _bodies[physicsBody2D];
		foreach (var fixture in body.FixtureList)
		{
			fixture.Friction = friction;
		}
	}


	public void SetCollisionCategories(PhysicsBody2D physicsBody2D, Layer2D layer2D)
	{
		var body = _bodies[physicsBody2D];
		var category = layer2D.ToAetherCategory();
		foreach (var fixture in body.FixtureList)
		{
			fixture.CollisionCategories = category;
		}
	}


	public void SetCollidesWith(PhysicsBody2D physicsBody2D, Layer2D layer2D)
	{
		var body = _bodies[physicsBody2D];
		var category = layer2D.ToAetherCategory();
		foreach (var fixture in body.FixtureList)
		{
			fixture.CollidesWith = category;
		}
	}


	public void SetCollisionGroup(PhysicsBody2D physicsBody2D, short collisionGroup)
	{
		var body = _bodies[physicsBody2D];
		foreach (var fixture in body.FixtureList)
		{
			fixture.CollisionGroup = collisionGroup;
		}
	}


	public void SetIsSensor(PhysicsBody2D physicsBody2D, bool isSensor)
	{
		var body = _bodies[physicsBody2D];
		foreach (var fixture in body.FixtureList)
		{
			fixture.IsSensor = isSensor;
		}
	}


	public PhysicsBody2DTransformResult GetTransform(PhysicsBody2D physicsBody2D)
	{
		var body = _bodies[physicsBody2D];
		var transform = body.GetTransform();
		return transform.ToPhysicsBodyTransformResult();
	}
}
