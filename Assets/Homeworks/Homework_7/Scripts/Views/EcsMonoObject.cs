using Leopotam.EcsLite;
using OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Components;
using UnityEngine;

public abstract class EcsMonoObject : MonoBehaviour
{
    public EcsPackedEntity EcsPacked { get; private set; }
    protected EcsWorld _world;

    public void Init(EcsWorld world) => _world = world;

    public void PackEntity(int entity) => EcsPacked = _world.PackEntity(entity);

    public virtual void OnTriggerEnterEvent(EcsMonoObject firstCollide, EcsMonoObject secondCollide)
    {
        if (_world != null)
        {
            var entity = _world.NewEntity();
            var poolHitC = _world.GetPool<HitComponent>();
            ref var hitC = ref poolHitC.Add(entity);
            hitC.FirstCollide = firstCollide;
            hitC.SecondCollide = secondCollide;
        }
    }

    public virtual void OnTriggerStayEvent(EcsMonoObject thisObject)
    {
        if (_world != null)
        {
            var entity = _world.NewEntity();
            var poolCollisionStayC = _world.GetPool<CollisionStayComponent>();
            ref var CollisionStayC = ref poolCollisionStayC.Add(entity);
            CollisionStayC.CollideObject = thisObject;
        }
    }

    public virtual void OnTriggerExitEvent(EcsMonoObject thisObject)
    {
        if (_world != null)
        {
            var entity = _world.NewEntity();
            var poolCollisionExitC = _world.GetPool<CollisionExitComponent>();
            ref var CollisionExitC = ref poolCollisionExitC.Add(entity);
            CollisionExitC.LeaveObject = thisObject;
        }
    }
}
