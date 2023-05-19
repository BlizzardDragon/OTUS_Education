using UnityEngine;

public class RoadPart : MonoBehaviour
{
    public void SetTiling(Vector2 value)
    {
        Material material = GetComponent<MeshRenderer>().material;
        material.mainTextureScale = value;
    }
}
