using UnityEngine;

public class CollidingObject : EcsMonoObject
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out EcsMonoObject collide))
        {
            OnTriggerAction(this, collide);
        }
    }
}
