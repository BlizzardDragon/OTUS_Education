using UnityEngine;

public class CollidingUnit : EcsMonoObject
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EcsMonoObject collide))
        {
            OnTriggerEnterEvent(this, collide);
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }

    private void OnTriggerStay(Collider other)
    {

    }
}
