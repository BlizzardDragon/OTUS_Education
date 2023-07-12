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

    public virtual void OnTriggerExitEvent(EcsMonoObject thisObject)
    {
        if (_world != null)
        {
            var entity = _world.NewEntity();
            var poolLeavingC = _world.GetPool<LeavingComponent>();
            ref var LeavingC = ref poolLeavingC.Add(entity);
            LeavingC.LeavingObject = thisObject;
        }
    }
}
