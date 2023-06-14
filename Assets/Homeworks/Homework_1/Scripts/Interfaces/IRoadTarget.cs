using UnityEngine;


namespace Homework_1
{
    public interface IRoadTarget
    {
        Transform Transform { get; }
        void InstallTarget();
    }
}
