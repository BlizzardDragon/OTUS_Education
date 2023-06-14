using UnityEngine;
using DG.Tweening;


namespace Homework_1
{
    public class RoadPart : MonoBehaviour
    {
        private const float END_VALUE = 1f;
        private const float DURATION = 0.5f;


        private void Awake()
        {
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            renderer.material.DOFade(END_VALUE, DURATION);
        }

        public void SetTiling(Vector2 value, bool isShared)
        {
            Material material;
            MeshRenderer renderer = GetComponent<MeshRenderer>();

            material = isShared ? renderer.sharedMaterial : renderer.material;
            material.mainTextureScale = value;
        }
    }
}
