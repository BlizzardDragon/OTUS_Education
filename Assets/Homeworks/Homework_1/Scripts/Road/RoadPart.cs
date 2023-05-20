using UnityEngine;

public class RoadPart : MonoBehaviour
{
    public void SetTiling(Vector2 value, bool isShared)
    {
        Material material;
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        
        material = isShared ? renderer.sharedMaterial : renderer.material;
        material.mainTextureScale = value;
    }
}
