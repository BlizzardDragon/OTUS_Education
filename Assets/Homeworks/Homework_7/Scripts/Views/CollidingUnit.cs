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
        if (other.GetComponent<CollidingUnit>())
        {
            OnTriggerExitEvent(this);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CollidingUnit>())
        {
            if (other.TryGetComponent(out EcsMonoObject collide))
            {
                OnTriggerStayEvent(this, collide);
            }
        }
    }
}
