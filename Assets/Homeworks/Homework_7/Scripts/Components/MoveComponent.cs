using UnityEngine;

namespace OTUS_Education.Assets.Homeworks.Homework_7.Scripts.Components
{
    public enum MoveDirections
    {
        Null,
        Left,
        Right
    }

    public struct MoveComponent
    {
        public float MoveSpeed;
        public MoveDirections MoveDirection;
        public bool MoveAlloved;
    }
}