using UnityEngine;

public class Road : MonoBehaviour
{
    public float RoadLength => _roadScale.z;
    [SerializeField] private bool _isShared;
    [SerializeField] private Vector3 _roadScale = Vector3.one;
    [SerializeField] private BoxCollider _boxCollider;


    private void OnValidate()
    {
        SetScale(_roadScale);
        SetBoxSize(SetTilingForMaterials());
    }

    private void SetBoxSize(int countRoadParts) => _boxCollider.size = new Vector3(countRoadParts, 1, 1);
    private void SetScale(Vector3 scale) => transform.localScale = _roadScale;
    private int SetTilingForMaterials()
    {
        var roadParts = GetComponentsInChildren<RoadPart>();
        Vector2 tiling = new Vector2(_roadScale.x, _roadScale.z);

        foreach (var part in roadParts)
        {
            part.SetTiling(tiling, _isShared);
        }
        
        return roadParts.Length;
    }
}
