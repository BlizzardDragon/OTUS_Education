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
        public MoveDirections MoveDirection;
        public float MoveSpeed;
        public bool MoveAlloved;
    }
}