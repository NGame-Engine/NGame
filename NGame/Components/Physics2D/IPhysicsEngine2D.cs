using System.Numerics;
using NGame.Components.Transforms;
using NGame.UpdateSchedulers;

namespace NGame.Components.Physics2D;



public interface IPhysicsEngine2D
{
	void CreateBody(PhysicsBody2D physicsBody2D, Transform transform);
	void RemoveBody(PhysicsBody2D physicsBody2D);
	void RemoveAllBodies();


	void Subscribe(
		PhysicsBody2D physicsBody2D,
		Action<Collision2DEventArgs> onCollision,
		Action<Collision2DEventArgs> onSeparation
	);


	void AddBoxCollider(PhysicsBody2D physicsBody2D, BoxCollider2D collider2D);
	void AddCircleCollider(PhysicsBody2D physicsBody2D, CircleCollider2D collider2D);
	void RemoveCollider(PhysicsBody2D physicsBody2D, CircleCollider2D collider2D);
	void Tick(GameTime gameTime);


	/// <summary>
	/// Apply a force at a world point. If the force is not
	/// applied at the center of mass, it will generate a torque and
	/// affect the angular velocity. This wakes up the body.
	/// </summary>
	/// <param name="physicsBody2D">The NGame component.</param>
	/// <param name="force">The world force vector, usually in Newtons (N).</param>
	/// <param name="point">The world position of the point of application.</param>
	void ApplyForce(
		PhysicsBody2D physicsBody2D,
		Vector2 force,
		Vector2? point = null
	);


	/// <summary>
	/// Apply a torque. This affects the angular velocity
	/// without affecting the linear velocity of the center of mass.
	/// This wakes up the body.
	/// </summary>
	/// <param name="physicsBody2D">The NGame component.</param>
	/// <param name="torque">The torque about the z-axis (out of the screen), usually in N-m.</param>
	void ApplyTorque(PhysicsBody2D physicsBody2D, float torque);


	/// <summary>
	/// Apply an impulse at a point. This immediately modifies the velocity.
	/// It also modifies the angular velocity if the point of application
	/// is not at the center of mass.
	/// This wakes up the body.
	/// </summary>
	/// <param name="physicsBody2D">The NGame component.</param>
	/// <param name="impulse">The world impulse vector, usually in N-seconds or kg-m/s.</param>
	/// <param name="point">The world position of the point of application.</param>
	void ApplyLinearImpulse(
		PhysicsBody2D physicsBody2D,
		Vector2 impulse,
		Vector2? point = null
	);


	/// <summary>
	/// Apply an impulse at a point. This immediately modifies the velocity.
	/// This wakes up the body.
	/// </summary>
	/// <param name="physicsBody2D">The NGame component.</param>
	/// <param name="impulse">The world impulse vector, usually in N-seconds or kg-m/s.</param>
	void ApplyAngularImpulse(PhysicsBody2D physicsBody2D, float impulse);


	void SetMass(PhysicsBody2D physicsBody2D, float mass);
	void SetInertia(PhysicsBody2D physicsBody2D, float inertia);
	void SetGravityIsIgnored(PhysicsBody2D physicsBody2D, bool gravityIsIgnored);
	void SetRotationIsFixed(PhysicsBody2D physicsBody2D, bool rotationIsFixed);


	void SetContinuousCollisionDetection(
		PhysicsBody2D physicsBody2D,
		bool continuousCollisionDetectionIsActive
	);


	void SetRestitution(PhysicsBody2D physicsBody2D, float restitution);
	void SetFriction(PhysicsBody2D physicsBody2D, float friction);
	void SetCollisionCategories(PhysicsBody2D physicsBody2D, Layer2D layer2D);
	void SetCollidesWith(PhysicsBody2D physicsBody2D, Layer2D layer2D);
	void SetCollisionGroup(PhysicsBody2D physicsBody2D, short collisionGroup);
	void SetIsSensor(PhysicsBody2D physicsBody2D, bool isSensor);
	PhysicsBody2DTransformResult GetTransform(PhysicsBody2D physicsBody2D);
}
