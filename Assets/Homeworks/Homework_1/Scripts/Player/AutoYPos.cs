using UnityEngine;


namespace Homework_1
{
    public class AutoYPos : MonoBehaviour
    {
        private void Awake()
        {
            Vector3 newPosition = new Vector3(transform.position.x, transform.localScale.x / 2, transform.position.z);
            transform.position = newPosition;
        }
    }
}
