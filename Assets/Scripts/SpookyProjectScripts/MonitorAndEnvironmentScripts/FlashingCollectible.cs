using UnityEngine;

public class FlashingCollectible : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public float frequency;
    public float speed;
    public float amplitude;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the emission color based on a sine wave to create a flashing effect. The color will oscillate between black and white
        meshRenderer.material.SetColor("_EmissionColor", Color.white * (Mathf.Sin(Time.time * frequency) * amplitude + speed));
    }
}
