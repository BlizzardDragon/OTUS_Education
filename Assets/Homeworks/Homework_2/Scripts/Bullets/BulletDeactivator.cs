using UnityEngine;
using System.Collections.Generic;


namespace ShootEmUp
{
    public class BulletDeactivator : MonoBehaviour
    {
        public void DisableActiveBullets(HashSet<Bullet> activeBullets)
        {
            foreach (var bullet in activeBullets)
            {
                bullet.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
        }
    }
}
