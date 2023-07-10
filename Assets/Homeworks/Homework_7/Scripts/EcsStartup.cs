using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Services;
using OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Systems;
using UnityEngine;

namespace OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Components
{
    sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private SharedData _sharedData;
        EcsWorld _world;
        IEcsSystems _systems;

        void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems
                .Add(new UnitInitializer())
                .Add(new UnitSpawner())
                .Add(new UnitMoveSystem())
                .Add(new EnemySearchSystem())

                // register additional worlds here, for example:
                // .AddWorld (new EcsWorld (), "events")
#if UNITY_EDITOR
                // add debug systems for custom worlds here, for example:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Inject(_sharedData)
                .Init();
        }

        void Update()
        {
            // process systems here.
            _systems?.Run();
        }

        void OnDestroy()
        {
            if (_systems != null)
            {
                // list of custom worlds will be cleared
                // during IEcsSystems.Destroy(). so, you
                // need to save it here if you need.
                _systems.Destroy();
                _systems = null;
            }

            // cleanup custom worlds here.

            // cleanup default world.
            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}

[Serializable]
public class SharedData
{
    public int TeamCount { get; private set; } = 2;

    public int UnitsPerTeam;
    public int ColumnCount;

    public int UnitHealth;
    public float UnitMoveSpeed;
    public float UnitAttackPeriod;
    public float UnitAttackDistance;
    public float UnitAttackPeriodRandomMultiplier;

    public string UnitPrefabPath;

    public float UnitSpawnOffset;

    public Color ColorTeam1;
    public Color ColorTeam2;

    public Transform SpawnPointTeam_1;
    public Transform SpawnPointTeam_2;
}