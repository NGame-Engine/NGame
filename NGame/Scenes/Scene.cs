// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org/ & https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Numerics;
using NGame.Ecs;
using NGame.Ecs.Events;

namespace NGame.Scenes;



public sealed class Scene
{
	private readonly IEcsTypeFactory _ecsTypeFactory;
	private readonly ISceneEventBus _sceneEventBus;
	private readonly List<Scene> _children = new();
	private readonly List<Entity> _entities = new();

	// TODO keep updated if parent added to entity
	// TODO keep updated if parent removed from entity
	// Or just straight up remove and always filter?
	private readonly List<Entity> _rootEntities = new();


	/// <summary>
	/// Initializes a new instance of the <see cref="Scene"/> class.
	/// </summary>
	public Guid Id = Guid.NewGuid();


	public Scene(IEcsTypeFactory ecsTypeFactory, ISceneEventBus sceneEventBus)
	{
		_ecsTypeFactory = ecsTypeFactory;
		_sceneEventBus = sceneEventBus;
	}


	public Scene? Parent { get; private set; }


	public IReadOnlyCollection<Scene> Children => _children;
	public IReadOnlyCollection<Entity> Entities => _entities;
	public IReadOnlyCollection<Entity> RootEntities => _rootEntities;


	/// <summary>
	/// An offset applied to all entities of the scene relative to it's parent scene.
	/// </summary>
	public Vector3 Offset;

	/// <summary>
	/// The absolute transform applied to all entities of the scene.
	/// </summary>
	/// <remarks>This field is overwritten by the transform processor each frame.</remarks>
	public Matrix4x4 WorldMatrix;


	public void AddChildScene(Scene child)
	{
		child.Parent = this;
		_children.Add(child);
		_sceneEventBus.NotifyChildSceneAdded(this, child);
	}


	public Entity CreateEntity()
	{
		var entity = _ecsTypeFactory.CreateEntity(this);
		entity.Scene = this;
		_entities.Add(entity);
		_rootEntities.Add(entity);
		_sceneEventBus.NotifyEntityAdded(this, entity);
		return entity;
	}


	// TODO extract out of scene, probably
	/// <summary>
	/// Adding Entity to a Scene will always make it a root Entity.
	/// </summary>
	public void AddEntity(Entity entity)
	{
		if (entity.Scene == this) return;

		entity.Scene._entities.Remove(entity);
		entity.Scene = this;
		_entities.Add(entity);

		entity.Transform.SetParent(null);
		_rootEntities.Add(entity);

		_sceneEventBus.NotifyEntityAdded(this, entity);
	}
}
