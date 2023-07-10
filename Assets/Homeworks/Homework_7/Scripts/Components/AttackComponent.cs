using UnityEngine;

namespace OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Components
{
    public struct AttackComponent
    {
        public float AttackTimer;
        public float AttackPeriod;

        public float AttackDistance;

        public bool AttackIsReady;

        public GameObject AttackTarget; 
    }
}