using UnityEngine;

public interface IRoadTarget
{
    Transform Transform { get; }
    void InstallTarget();
}
