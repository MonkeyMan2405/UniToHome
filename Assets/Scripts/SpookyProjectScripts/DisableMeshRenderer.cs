using UnityEngine;

public class DisableMeshRenderer : MonoBehaviour
{
    private MeshRenderer objectMeshRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectMeshRenderer = GetComponent<MeshRenderer>();
        objectMeshRenderer.enabled = false;
    }

}
