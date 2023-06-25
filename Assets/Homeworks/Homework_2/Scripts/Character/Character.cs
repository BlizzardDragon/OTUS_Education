using UnityEngine;

// Готово.
namespace ShootEmUp
{
    [RequireComponent(typeof(HitPointsComponent), typeof(WeaponComponent), typeof(TeamComponent))]
    [RequireComponent(typeof(MoveComponent))]
    public class Character : MonoBehaviour
    {
        [field: SerializeField] public BulletConfig BulletConfig { get; private set; }
        
        public HitPointsComponent HitPointsComponent { get; private set; }
        public WeaponComponent WeaponComponent { get; private set; }
        public TeamComponent TeamComponent { get; private set; }
        public MoveComponent MoveComponent { get; private set; }


        private void Awake()
        {
            HitPointsComponent = GetComponent<HitPointsComponent>();
            WeaponComponent = GetComponent<WeaponComponent>();
            TeamComponent = GetComponent<TeamComponent>();
            MoveComponent = GetComponent<MoveComponent>();
        }
    }
}
