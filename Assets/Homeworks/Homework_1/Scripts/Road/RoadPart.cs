using UnityEngine;
using DG.Tweening;

public class RoadPart : MonoBehaviour
{
    private void Awake()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material.DOFade(1, 0.5f);
    }

    public void SetTiling(Vector2 value, bool isShared)
    {
        Material material;
        MeshRenderer renderer = GetComponent<MeshRenderer>();

        material = isShared ? renderer.sharedMaterial : renderer.material;
        material.mainTextureScale = value;
    }
}
