using UnityEngine;

public class MeshRendererComponent : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    public MeshRenderer MeshRenderer { get => _meshRenderer; set => _meshRenderer = value; }
}
